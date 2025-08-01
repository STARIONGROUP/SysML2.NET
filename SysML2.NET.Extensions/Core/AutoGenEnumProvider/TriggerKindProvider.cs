// -------------------------------------------------------------------------------------------------
// <copyright file="TriggerKindProvider.cs" company="Starion Group S.A.">
//
//   Copyright 2022-2025 Starion Group S.A.
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//       http://www.apache.org/licenses/LICENSE-2.0
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.
//
// </copyright>
// ------------------------------------------------------------------------------------------------

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Extensions.Core
{
    using System;

    using SysML2.NET.Core;

    /// <summary>
    /// The purpose of the <see cref="TriggerKindProvider"/> is to provide conversion methods
    /// to the <see cref="TriggerKind"/> enum
    /// </summary>
    public static class TriggerKindProvider
    {
        /// <summary>
        /// Parses the <see cref="ReadOnlySpan{Byte}"/> to a <see cref="TriggerKind"/>
        /// </summary>
        /// <param name="value">
        /// The <see cref="ReadOnlySpan{Char}"/> that is to be parsed
        /// </param>
        /// <returns>
        /// A <see cref="TriggerKind"/> enumeration literal
        /// </returns>
        /// <exception cref="ArgumentException">
        /// thrown when the content of the <see cref="ReadOnlySpan{Char}"/> cannot be
        /// parsed into a valid <see cref="TriggerKind"/> enumeration literal
        /// </exception>
        /// <remarks>
        /// This method is suited for  string parsing
        /// There are zero allocations, no boxing, Fast short-circuit evaluation
        /// JIT friendly (no more than 5 cases)
        /// </remarks>
        public static TriggerKind Parse(ReadOnlySpan<char> value)
        {
            if (value.Length == 4 && value.Equals("when".AsSpan(), StringComparison.OrdinalIgnoreCase))
            {
                return TriggerKind.When;
            }

            if (value.Length == 2 && value.Equals("at".AsSpan(), StringComparison.OrdinalIgnoreCase))
            {
                return TriggerKind.At;
            }

            if (value.Length == 5 && value.Equals("after".AsSpan(), StringComparison.OrdinalIgnoreCase))
            {
                return TriggerKind.After;
            }


            throw new ArgumentException($"'{new string(value)}' is not a valid TriggerKind", nameof(value));
        }

        /// <summary>
        /// Parses the <see cref="ReadOnlySpan{Byte}"/> to a <see cref="TriggerKind"/>
        /// </summary>
        /// <param name="value">
        /// The <see cref="ReadOnlySpan{Byte}"/> that is to be parsed
        /// </param>
        /// <returns>
        /// A <see cref="TriggerKind"/> enumeration literal
        /// </returns>
        /// <exception cref="ArgumentException">
        /// thrown when the content of the <see cref="ReadOnlySpan{Byte}"/> cannot be
        /// parsed into a valid <see cref="TriggerKind"/> enumeration literal
        /// </exception>
        /// <remarks>
        /// This method is suited for streaming parsing
        /// There are zero allocations, no boxing, Fast short-circuit evaluation
        /// JIT friendly (no more than 5 cases)
        /// </remarks>
        public static TriggerKind Parse(ReadOnlySpan<byte> value)
        {
            if (value.Length == 4 && value.SequenceEqual("when"u8))
            {
                return TriggerKind.When;
            }

            if (value.Length == 2 && value.SequenceEqual("at"u8))
            {
                return TriggerKind.At;
            }

            if (value.Length == 5 && value.SequenceEqual("after"u8))
            {
                return TriggerKind.After;
            }


            throw new ArgumentException($"'{System.Text.Encoding.UTF8.GetString(value)}' is not a valid TriggerKind", nameof(value));
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
