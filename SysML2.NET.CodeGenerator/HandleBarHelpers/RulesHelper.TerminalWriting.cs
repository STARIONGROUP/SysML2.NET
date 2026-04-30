// -------------------------------------------------------------------------------------------------
// <copyright file="RulesHelper.TerminalWriting.cs" company="Starion Group S.A.">
// 
//   Copyright 2022-2026 Starion Group S.A.
// 
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
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

namespace SysML2.NET.CodeGenerator.HandleBarHelpers
{
    using System;
    using System.Collections.Generic;

    using HandlebarsDotNet;

    /// <summary>
    /// Terminal writing helpers for textual notation code generation
    /// </summary>
    public static partial class RulesHelper
    {
        /// <summary>
        /// Terminals that emit with <c>AppendLine</c> producing a newline after the token.
        /// </summary>
        private static readonly HashSet<string> NewLineTerminals = ["{", "}", ";"];

        /// <summary>
        /// Terminals after which no trailing space should be emitted,
        /// because the next element is directly adjacent (e.g., content inside angle brackets, closing angle bracket, or the
        /// <c>~</c> prefix operator).
        /// </summary>
        private static readonly HashSet<string> NoTrailingSpaceTerminals = ["<", ">", "~", "[", "(", "#", ".", "'", "*"];

        /// <summary>
        /// Writes the <c>stringBuilder.Append</c> or <c>stringBuilder.AppendLine</c> call for a terminal value,
        /// applying formatting rules derived from the SysML v2 textual notation conventions:
        /// <list type="bullet">
        ///  <item><c>{</c>, <c>}</c>, <c>;</c> are emitted with <c>AppendLine</c> (newline after)</item>
        ///  <item><c>&lt;</c>, <c>&gt;</c>, and <c>~</c> are emitted with no trailing space (adjacent to surrounding content)</item>
        ///  <item><c>,</c> is emitted with a trailing space</item>
        ///  <item>Multi-character terminals (keywords) are emitted with a trailing space</item>
        ///  <item>Other single-character terminals are emitted as-is</item>
        /// </list>
        /// </summary>
        /// <param name="writer">The <see cref="EncodedTextWriter" /> used to write into output content</param>
        /// <param name="terminalValue">The terminal string value to emit</param>
        private static void WriteTerminalAppend(EncodedTextWriter writer, string terminalValue)
        {
            if (NewLineTerminals.Contains(terminalValue))
            {
                if (terminalValue == "{")
                {
                    writer.WriteSafeString($"stringBuilder.Append(' ');{Environment.NewLine}");
                }

                writer.WriteSafeString($"stringBuilder.AppendLine(\"{terminalValue}\");");
                return;
            }

            if (NoTrailingSpaceTerminals.Contains(terminalValue))
            {
                writer.WriteSafeString($"stringBuilder.Append(\"{terminalValue}\");");
                return;
            }

            if (terminalValue.Length > 1 || terminalValue == ",")
            {
                writer.WriteSafeString($"stringBuilder.Append(\"{terminalValue} \");");
                return;
            }

            writer.WriteSafeString($"stringBuilder.Append(\"{terminalValue} \");");
        }

        /// <summary>
        /// Writes a terminal value with a leading space, used for keyword-like terminals that appear
        /// as alternatives in untyped multi-alternative rules (e.g., <c>'~' | 'conjugates'</c>).
        /// The leading space ensures visual separation from the preceding element.
        /// Structural terminals (<c>{</c>, <c>}</c>, <c>;</c>) still use <c>AppendLine</c>.
        /// </summary>
        /// <param name="writer">The <see cref="EncodedTextWriter" /> used to write into output content</param>
        /// <param name="terminalValue">The terminal string value to emit</param>
        private static void WriteTerminalAppendWithLeadingSpace(EncodedTextWriter writer, string terminalValue)
        {
            if (NewLineTerminals.Contains(terminalValue))
            {
                writer.WriteSafeString($"stringBuilder.AppendLine(\"{terminalValue}\");");
                return;
            }

            if (NoTrailingSpaceTerminals.Contains(terminalValue))
            {
                writer.WriteSafeString($"stringBuilder.Append(\" {terminalValue}\");");
                return;
            }

            writer.WriteSafeString($"stringBuilder.Append(\" {terminalValue} \");");
        }
    }
}
