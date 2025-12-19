// -------------------------------------------------------------------------------------------------
// <copyright file="TransitionFeatureKindProvider.cs" company="Starion Group S.A.">
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

    using SysML2.NET.Core.Systems.States;

    /// <summary>
    /// The purpose of the <see cref="TransitionFeatureKindProvider"/> is to provide conversion methods
    /// to the <see cref="TransitionFeatureKind"/> enum
    /// </summary>
    public static class TransitionFeatureKindProvider
    {
        /// <summary>
        /// Parses the <see cref="ReadOnlySpan{Byte}"/> to a <see cref="TransitionFeatureKind"/>
        /// </summary>
        /// <param name="value">
        /// The <see cref="ReadOnlySpan{Char}"/> that is to be parsed
        /// </param>
        /// <returns>
        /// A <see cref="TransitionFeatureKind"/> enumeration literal
        /// </returns>
        /// <exception cref="ArgumentException">
        /// thrown when the content of the <see cref="ReadOnlySpan{Char}"/> cannot be
        /// parsed into a valid <see cref="TransitionFeatureKind"/> enumeration literal
        /// </exception>
        /// <remarks>
        /// This method is suited for  string parsing
        /// There are zero allocations, no boxing, Fast short-circuit evaluation
        /// JIT friendly (no more than 5 cases)
        /// </remarks>
        public static TransitionFeatureKind Parse(ReadOnlySpan<char> value)
        {
            if (value.Length == 7 && value.Equals("trigger".AsSpan(), StringComparison.OrdinalIgnoreCase))
            {
                return TransitionFeatureKind.Trigger;
            }

            if (value.Length == 5 && value.Equals("guard".AsSpan(), StringComparison.OrdinalIgnoreCase))
            {
                return TransitionFeatureKind.Guard;
            }

            if (value.Length == 6 && value.Equals("effect".AsSpan(), StringComparison.OrdinalIgnoreCase))
            {
                return TransitionFeatureKind.Effect;
            }

            throw new ArgumentException($"'{new string(value)}' is not a valid TransitionFeatureKind", nameof(value));
        }

        /// <summary>
        /// Parses the <see cref="ReadOnlySpan{Byte}"/> to a <see cref="TransitionFeatureKind"/>
        /// </summary>
        /// <param name="value">
        /// The <see cref="ReadOnlySpan{Byte}"/> that is to be parsed
        /// </param>
        /// <returns>
        /// A <see cref="TransitionFeatureKind"/> enumeration literal
        /// </returns>
        /// <exception cref="ArgumentException">
        /// thrown when the content of the <see cref="ReadOnlySpan{Byte}"/> cannot be
        /// parsed into a valid <see cref="TransitionFeatureKind"/> enumeration literal
        /// </exception>
        /// <remarks>
        /// This method is suited for streaming parsing
        /// There are zero allocations, no boxing, Fast short-circuit evaluation
        /// JIT friendly (no more than 5 cases)
        /// </remarks>
        public static TransitionFeatureKind Parse(ReadOnlySpan<byte> value)
        {
            if (value.Length == 7 && value.SequenceEqual("trigger"u8))
            {
                return TransitionFeatureKind.Trigger;
            }

            if (value.Length == 5 && value.SequenceEqual("guard"u8))
            {
                return TransitionFeatureKind.Guard;
            }

            if (value.Length == 6 && value.SequenceEqual("effect"u8))
            {
                return TransitionFeatureKind.Effect;
            }

            throw new ArgumentException($"'{System.Text.Encoding.UTF8.GetString(value)}' is not a valid TransitionFeatureKind", nameof(value));
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
