// -------------------------------------------------------------------------------------------------
// <copyright file="IndentedStringBuilder.cs" company="Starion Group S.A.">
//
//    Copyright (C) 2022-2026 Starion Group S.A.
//
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
//
//        http://www.apache.org/licenses/LICENSE-2.0
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Serializer.TextualNotation.Writers
{
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// A thin wrapper around <see cref="StringBuilder"/> that produces an indentation-aware,
    /// whitespace-normalised textual stream. The wrapper exposes the same <c>Append</c> /
    /// <c>AppendLine</c> surface the textual-notation builders rely on, and adds an integer
    /// indentation level via <see cref="IncreaseIndent"/> and <see cref="DecreaseIndent"/>.
    /// Whenever an <c>Append</c> call writes the first non-newline content of a new logical
    /// line, the wrapper transparently prepends <see cref="IndentLevel"/> repetitions of a
    /// four-space indent unit. <see cref="AppendLine()"/> and <see cref="AppendLine(string)"/>
    /// close the current line and arm the wrapper to emit the indent on the next non-newline
    /// write.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Introduced as part of issue STARIONGROUP/SysML2.NET#281: the previous textual-notation
    /// writers emitted a flat stream of tokens with all block bodies at column 0. The
    /// indent push/pop calls are emitted exclusively by the textual-notation code generator
    /// (<c>SysML2.NET.CodeGenerator/HandleBarHelpers/TerminalWriter.cs</c>) around the
    /// grammar's block-delimiter terminals <c>{</c> and <c>}</c>; hand-coded writers never
    /// adjust the indent level directly.
    /// </para>
    /// <para>
    /// Whitespace normalisation (added post-#281): the SST tutorial (Release 2026-03)
    /// codifies a canonical textual style via worked examples but the KEBNF grammar itself
    /// (KerML §8.2.2.1) treats white space purely as an ignored separator. The wrapper
    /// encodes the SST conventions through three rules that apply to every <c>Append</c>
    /// regardless of call-site:
    /// </para>
    /// <list type="number">
    /// <item><description>
    /// <b>Leading whitespace at logical line start is suppressed</b> — when the wrapper is
    /// armed to emit the indent prefix, any leading space characters in the payload are
    /// dropped before the first real content character triggers the indent emission. This
    /// removes the leading-space defect (<c> standard library package</c>).
    /// </description></item>
    /// <item><description>
    /// <b>Consecutive ASCII spaces are collapsed</b> — never emit two spaces in a row outside
    /// the indent prefix. This removes every double-space defect
    /// (<c>standard  library</c>, <c>Array  {</c>, <c>[1..  *]</c>, <c>=  mRef</c>, etc.).
    /// </description></item>
    /// <item><description>
    /// <b>Tight-left and tight-both terminals strip the preceding trailing space</b> — when
    /// the payload is recognised as one of the SST-canonical tight punctuation tokens
    /// (<c>,</c>, <c>)</c>, <c>]</c>, <c>;</c>, <c>.</c>, <c>::</c>, <c>..</c>), any trailing
    /// space already in the buffer is removed before the token is emitted. Tight-both tokens
    /// additionally have their own trailing space stripped from the payload, so
    /// <c>::</c> emits as <c>::</c> not <c>::&#32;</c>. This removes
    /// <c>Collections::* ;</c>, <c>mRef .dimensions</c>, <c>[1..&#32;*]</c> and friends.
    /// </description></item>
    /// </list>
    /// <para>
    /// The classification is intentionally minimal — only the punctuation tokens whose
    /// canonical form is unambiguous in the SST examples are listed. Operators like
    /// <c>:&gt;</c>, <c>:&gt;&gt;</c>, <c>:=</c>, <c>=</c> remain infix with a space on both
    /// sides; they emit through the regular character-level path.
    /// </para>
    /// </remarks>
    public sealed class IndentedStringBuilder
    {
        /// <summary>
        /// The unit of indentation prepended once per <see cref="IndentLevel"/> at the start
        /// of each new logical line. Four spaces, per the SysML v2 textual-notation tutorial
        /// in <c>Resources/specification/Intro to the SysML v2 Language-Textual Notation.pdf.txt</c>.
        /// </summary>
        private const string IndentUnit = "    ";

        /// <summary>
        /// Terminals whose canonical SST form has NO space before but DO have a space after
        /// (the trailing space is part of the inter-token separator the next emission needs).
        /// <c>;</c> is included here so <see cref="AppendLine(string)"/> with the statement
        /// terminator also strips the preceding trailing space.
        /// <c>[</c> is included so multiplicity / indexer brackets attach directly to the
        /// preceding identifier (<c>Number[1..*]</c>, <c>array[i]</c>) per the SST tutorial
        /// convention; at logical line start (e.g. the multiplicity-prefix <c>[1] wheel</c>)
        /// there is no preceding space to strip, so this remains backward-compatible.
        /// <c>#</c> is included so index / select expressions attach directly to the
        /// preceding identifier (<c>frontWheel#(1)</c>); at logical line start (e.g.
        /// <c>#metadata</c> annotations) there is no preceding space to strip.
        /// <c>(</c> is intentionally NOT in this set — it is contextual (<c>foo(x)</c> tight
        /// vs <c>not (x or y)</c> separated) and a runtime distinction would require either
        /// grammar-aware emission or a dedicated helper.
        /// </summary>
        private static readonly HashSet<string> TightLeftTerminals = [",", ")", "]", ";", "[", "#"];

        /// <summary>
        /// Terminals whose canonical SST form has NO space on either side: qualified-name
        /// separator <c>::</c>, range separator <c>..</c>, and dotted-access <c>.</c>.
        /// </summary>
        private static readonly HashSet<string> TightBothTerminals = [".", "::", ".."];

        /// <summary>
        /// Characters that, when they appear as the last buffered character, suppress any
        /// leading space in the next payload — the SST tutorial shows no space between an
        /// opening bracket / prefix operator and the content that follows
        /// (<c>[1..5]</c>, <c>foo(x)</c>, <c>~negative</c>, <c>#metadata</c>). Tracked at
        /// character granularity so it composes naturally with the consecutive-space
        /// collapse in <see cref="AppendCharNormalized"/>.
        /// </summary>
        private static readonly HashSet<char> TightRightChars = ['[', '(', '~', '#'];

        /// <summary>
        /// The underlying <see cref="StringBuilder"/> all writes are forwarded to.
        /// </summary>
        private readonly StringBuilder builder = new();

        /// <summary>
        /// Tracks whether the next <c>Append</c> call is the first non-newline content of a
        /// new logical line. Set to <c>true</c> initially and after every
        /// <see cref="AppendLine()"/> / <see cref="AppendLine(string)"/>; cleared by
        /// <see cref="EmitIndentIfNeeded"/> once the indent prefix has been emitted.
        /// </summary>
        private bool atLineStart = true;

        /// <summary>
        /// Depth counter for the inline-block mode. When non-zero, line-terminating writes
        /// (<see cref="AppendLine()"/>, <see cref="AppendLine(string)"/>) emit their payload
        /// followed by a single ASCII space INSTEAD of a line terminator, and the indent
        /// push/pop calls (<see cref="IncreaseIndent"/>, <see cref="DecreaseIndent"/>) are
        /// no-ops. Used by the textual-notation code generator to render constraint bodies
        /// (<c>{ expr }</c>) on a single line per the SST tutorial convention, while leaving
        /// multi-statement bodies (calculations, packages, definitions) on their multi-line
        /// canonical form.
        /// </summary>
        private int inlineBlockDepth;

        /// <summary>
        /// Single-shot flag set the moment a <see cref="TightBothTerminals"/> token has just
        /// been emitted. The next <see cref="AppendCharNormalized"/> invocation that
        /// receives an ASCII space character drops it and clears the flag, ensuring no
        /// inter-token space appears immediately after a tight-both terminal (e.g.
        /// <c>[1..*]</c>, not <c>[1.. *]</c>; <c>Collections::*</c>, not
        /// <c>Collections:: *</c>). Cleared by any non-space character emission, so it never
        /// affects content beyond the very next emission.
        /// </summary>
        private bool suppressNextLeadingSpace;

        /// <summary>
        /// Gets the current indentation level. A level of <c>0</c> means no prefix is emitted
        /// at the start of new lines; each unit increment adds one <see cref="IndentUnit"/>
        /// (four spaces) of prefix.
        /// </summary>
        public int IndentLevel { get; private set; }

        /// <summary>
        /// Increments <see cref="IndentLevel"/> by one. Called by the textual-notation code
        /// generator immediately after a block-opening <c>{</c> terminal has been emitted, so
        /// that all subsequent lines inside the block are prefixed by an additional indent
        /// unit. No-op while inside an inline block (see <see cref="EnterInlineBlock"/>).
        /// </summary>
        public void IncreaseIndent()
        {
            if (this.inlineBlockDepth > 0)
            {
                return;
            }

            this.IndentLevel++;
        }

        /// <summary>
        /// Decrements <see cref="IndentLevel"/> by one. Called by the textual-notation code
        /// generator immediately before a block-closing <c>}</c> terminal is emitted, so that
        /// the closing brace itself aligns with the level of the block's owning declaration
        /// rather than the level of the block's contents. Guards against underflow: if the
        /// level is already <c>0</c> the call is a no-op, ensuring a malformed grammar with
        /// an unmatched closing brace cannot push the level negative. No-op while inside an
        /// inline block (see <see cref="EnterInlineBlock"/>).
        /// </summary>
        public void DecreaseIndent()
        {
            if (this.inlineBlockDepth > 0)
            {
                return;
            }

            if (this.IndentLevel > 0)
            {
                this.IndentLevel--;
            }
        }

        /// <summary>
        /// Enters inline-block mode. While inline-block depth is non-zero, every
        /// line-terminating write (<see cref="AppendLine()"/>, <see cref="AppendLine(string)"/>)
        /// emits its payload followed by a single ASCII space INSTEAD of a line terminator,
        /// and <see cref="IncreaseIndent"/> / <see cref="DecreaseIndent"/> are no-ops.
        /// Used by the textual-notation code generator to render constraint bodies
        /// (<c>{ expr }</c>) on a single line per the SST tutorial convention. Calls nest:
        /// the wrapper exits inline-block mode only when every <see cref="EnterInlineBlock"/>
        /// has been matched by an <see cref="ExitInlineBlock"/>.
        /// </summary>
        public void EnterInlineBlock()
        {
            this.inlineBlockDepth++;
        }

        /// <summary>
        /// Exits one level of inline-block mode previously entered via
        /// <see cref="EnterInlineBlock"/>. Guards against underflow: a call at depth zero is
        /// a no-op.
        /// </summary>
        public void ExitInlineBlock()
        {
            if (this.inlineBlockDepth > 0)
            {
                this.inlineBlockDepth--;
            }
        }

        /// <summary>
        /// Appends a single <see cref="char"/> to the underlying buffer, applying the
        /// leading-whitespace-at-line-start and consecutive-space-collapse normalisation
        /// rules described on the type.
        /// </summary>
        /// <param name="value">The character to append.</param>
        /// <returns>The current <see cref="IndentedStringBuilder"/> instance, to allow chaining.</returns>
        public IndentedStringBuilder Append(char value)
        {
            this.AppendCharNormalized(value);
            return this;
        }

        /// <summary>
        /// Appends a <see cref="string"/> to the underlying buffer, applying tight-left /
        /// tight-both stripping for recognised punctuation tokens and the per-character
        /// normalisation rules described on the type. A <c>null</c> or empty
        /// <paramref name="value"/> is a no-op.
        /// </summary>
        /// <param name="value">The string to append; may be <c>null</c> or empty.</param>
        /// <returns>The current <see cref="IndentedStringBuilder"/> instance, to allow chaining.</returns>
        public IndentedStringBuilder Append(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return this;
            }

            var isTightBoth = this.ApplyTightTokenNormalisation(ref value);

            foreach (var character in value)
            {
                this.AppendCharNormalized(character);
            }

            if (isTightBoth)
            {
                this.suppressNextLeadingSpace = true;
            }

            return this;
        }

        /// <summary>
        /// Appends the default line terminator to the underlying buffer and arms the wrapper
        /// to emit the indent prefix on the next non-newline write. While inside an inline
        /// block (see <see cref="EnterInlineBlock"/>), emits a single ASCII space instead of
        /// the line terminator, so the next emission stays on the same logical line.
        /// </summary>
        /// <returns>The current <see cref="IndentedStringBuilder"/> instance, to allow chaining.</returns>
        public IndentedStringBuilder AppendLine()
        {
            if (this.inlineBlockDepth > 0)
            {
                this.AppendCharNormalized(' ');
                return this;
            }

            this.StripTrailingSpace();
            this.builder.AppendLine();
            this.atLineStart = true;
            return this;
        }

        /// <summary>
        /// Appends <paramref name="value"/> followed by the default line terminator to the
        /// underlying buffer. If <paramref name="value"/> is a recognised tight-left or
        /// tight-both punctuation token (e.g. <c>;</c>, <c>}</c>), any trailing space already
        /// in the buffer is stripped first. Per-character normalisation applies to the
        /// payload regardless. The wrapper is then armed to emit the indent prefix on the
        /// next non-newline write. While inside an inline block (see
        /// <see cref="EnterInlineBlock"/>), emits the payload followed by a single ASCII
        /// space instead of the line terminator, so the next emission stays on the same
        /// logical line.
        /// </summary>
        /// <param name="value">The string to append before the line terminator.</param>
        /// <returns>The current <see cref="IndentedStringBuilder"/> instance, to allow chaining.</returns>
        public IndentedStringBuilder AppendLine(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return this.AppendLine();
            }

            var isTightBoth = this.ApplyTightTokenNormalisation(ref value);

            foreach (var character in value)
            {
                this.AppendCharNormalized(character);
            }

            if (isTightBoth)
            {
                this.suppressNextLeadingSpace = true;
            }

            if (this.inlineBlockDepth > 0)
            {
                this.AppendCharNormalized(' ');
                return this;
            }

            this.StripTrailingSpace();
            this.builder.AppendLine();
            this.atLineStart = true;
            return this;
        }

        /// <summary>
        /// Converts the accumulated content of the underlying <see cref="StringBuilder"/> to
        /// a <see cref="string"/>.
        /// </summary>
        /// <returns>The textual notation accumulated so far.</returns>
        public override string ToString()
        {
            return this.builder.ToString();
        }

        /// <summary>
        /// Applies the tight-token preamble: when <paramref name="value"/> is a recognised
        /// tight-left or tight-both terminal (optionally with a single trailing space, as the
        /// code generator emits e.g. <c>", "</c> / <c>":: "</c>), strips any trailing space
        /// already in the buffer. For tight-both terminals the trailing space of
        /// <paramref name="value"/> is also stripped from the payload so the next content
        /// sits directly against the terminal.
        /// </summary>
        /// <param name="value">
        /// The payload string. Passed by reference so the trailing space can be removed
        /// in-place for tight-both terminals.
        /// </param>
        private bool ApplyTightTokenNormalisation(ref string value)
        {
            var coreToken = value.Length > 1 && value[^1] == ' ' ? value[..^1] : value;

            if (TightLeftTerminals.Contains(coreToken) || TightBothTerminals.Contains(coreToken))
            {
                this.StripTrailingSpace();
            }

            if (TightBothTerminals.Contains(coreToken))
            {
                value = coreToken;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Appends a single character with the leading-whitespace-at-line-start and
        /// consecutive-space-collapse normalisation rules applied.
        /// </summary>
        /// <param name="character">The character to append.</param>
        private void AppendCharNormalized(char character)
        {
            if (character == ' ')
            {
                if (this.atLineStart)
                {
                    return;
                }

                if (this.suppressNextLeadingSpace)
                {
                    this.suppressNextLeadingSpace = false;
                    return;
                }

                if (this.builder.Length == 0)
                {
                    return;
                }

                var previousCharacter = this.builder[this.builder.Length - 1];

                if (previousCharacter == ' ')
                {
                    return;
                }

                if (TightRightChars.Contains(previousCharacter))
                {
                    return;
                }

                this.builder.Append(' ');
                return;
            }

            this.suppressNextLeadingSpace = false;
            this.EmitIndentIfNeeded();
            this.builder.Append(character);
        }

        /// <summary>
        /// Removes any trailing ASCII space characters from the underlying buffer. Used by
        /// the tight-token normalisation to ensure punctuation like <c>;</c> or <c>::</c> is
        /// emitted immediately after the previous token without an interposed space.
        /// </summary>
        private void StripTrailingSpace()
        {
            while (this.builder.Length > 0 && this.builder[this.builder.Length - 1] == ' ')
            {
                this.builder.Length--;
            }
        }

        /// <summary>
        /// Emits <see cref="IndentLevel"/> repetitions of <see cref="IndentUnit"/> to the
        /// underlying buffer if the wrapper is positioned at the start of a new line, then
        /// clears the <see cref="atLineStart"/> flag. Called from every content-emitting
        /// overload; <see cref="AppendLine()"/> (the no-argument overload) bypasses it
        /// because emitting an indent followed by nothing but a line terminator would
        /// produce trailing whitespace on an otherwise-empty line.
        /// </summary>
        private void EmitIndentIfNeeded()
        {
            if (this.atLineStart && this.IndentLevel > 0)
            {
                for (var level = 0; level < this.IndentLevel; level++)
                {
                    this.builder.Append(IndentUnit);
                }
            }

            this.atLineStart = false;
        }
    }
}
