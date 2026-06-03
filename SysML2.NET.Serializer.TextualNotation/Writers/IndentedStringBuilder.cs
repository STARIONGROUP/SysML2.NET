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
    using System.Text;

    /// <summary>
    /// A thin wrapper around <see cref="StringBuilder"/> that produces an indentation-aware
    /// textual stream. The wrapper exposes the same <c>Append</c> / <c>AppendLine</c> surface
    /// the textual-notation builders rely on, and adds an integer indentation level via
    /// <see cref="IncreaseIndent"/> and <see cref="DecreaseIndent"/>. Whenever an
    /// <c>Append</c> call writes the first non-newline content of a new logical line, the
    /// wrapper transparently prepends <see cref="IndentLevel"/> repetitions of a four-space
    /// indent unit. <see cref="AppendLine()"/> and <see cref="AppendLine(string)"/> close the
    /// current line and arm the wrapper to emit the indent on the next non-newline write.
    /// </summary>
    /// <remarks>
    /// Introduced as part of issue STARIONGROUP/SysML2.NET#281: the previous textual-notation
    /// writers emitted a flat stream of tokens with all block bodies at column 0. The
    /// indent push/pop calls are emitted exclusively by the textual-notation code generator
    /// (<c>SysML2.NET.CodeGenerator/HandleBarHelpers/TerminalWriter.cs</c>) around the
    /// grammar's block-delimiter terminals <c>{</c> and <c>}</c>; hand-coded writers never
    /// adjust the indent level directly.
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
        /// Gets the current indentation level. A level of <c>0</c> means no prefix is emitted
        /// at the start of new lines; each unit increment adds one <see cref="IndentUnit"/>
        /// (four spaces) of prefix.
        /// </summary>
        public int IndentLevel { get; private set; }

        /// <summary>
        /// Increments <see cref="IndentLevel"/> by one. Called by the textual-notation code
        /// generator immediately after a block-opening <c>{</c> terminal has been emitted, so
        /// that all subsequent lines inside the block are prefixed by an additional indent
        /// unit.
        /// </summary>
        public void IncreaseIndent()
        {
            this.IndentLevel++;
        }

        /// <summary>
        /// Decrements <see cref="IndentLevel"/> by one. Called by the textual-notation code
        /// generator immediately before a block-closing <c>}</c> terminal is emitted, so that
        /// the closing brace itself aligns with the level of the block's owning declaration
        /// rather than the level of the block's contents. Guards against underflow: if the
        /// level is already <c>0</c> the call is a no-op, ensuring a malformed grammar with
        /// an unmatched closing brace cannot push the level negative.
        /// </summary>
        public void DecreaseIndent()
        {
            if (this.IndentLevel > 0)
            {
                this.IndentLevel--;
            }
        }

        /// <summary>
        /// Appends a single <see cref="char"/> to the underlying buffer. If this is the
        /// first non-newline content of a new logical line, the configured indent prefix is
        /// emitted first.
        /// </summary>
        /// <param name="value">The character to append.</param>
        /// <returns>The current <see cref="IndentedStringBuilder"/> instance, to allow chaining.</returns>
        public IndentedStringBuilder Append(char value)
        {
            this.EmitIndentIfNeeded();
            this.builder.Append(value);
            return this;
        }

        /// <summary>
        /// Appends a <see cref="string"/> to the underlying buffer. If this is the first
        /// non-newline content of a new logical line, the configured indent prefix is emitted
        /// first. A <c>null</c> or empty <paramref name="value"/> is a no-op and does not
        /// arm the indent prefix.
        /// </summary>
        /// <param name="value">The string to append; may be <c>null</c> or empty.</param>
        /// <returns>The current <see cref="IndentedStringBuilder"/> instance, to allow chaining.</returns>
        public IndentedStringBuilder Append(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return this;
            }

            this.EmitIndentIfNeeded();
            this.builder.Append(value);
            return this;
        }

        /// <summary>
        /// Appends the default line terminator to the underlying buffer and arms the wrapper
        /// to emit the indent prefix on the next non-newline write.
        /// </summary>
        /// <returns>The current <see cref="IndentedStringBuilder"/> instance, to allow chaining.</returns>
        public IndentedStringBuilder AppendLine()
        {
            this.builder.AppendLine();
            this.atLineStart = true;
            return this;
        }

        /// <summary>
        /// Appends <paramref name="value"/> followed by the default line terminator to the
        /// underlying buffer. If this is the first non-newline content of a new logical line,
        /// the configured indent prefix is emitted first; the wrapper is then armed to emit
        /// the indent prefix on the next non-newline write.
        /// </summary>
        /// <param name="value">The string to append before the line terminator.</param>
        /// <returns>The current <see cref="IndentedStringBuilder"/> instance, to allow chaining.</returns>
        public IndentedStringBuilder AppendLine(string value)
        {
            this.EmitIndentIfNeeded();
            this.builder.AppendLine(value);
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
