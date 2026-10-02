// -------------------------------------------------------------------------------------------------
// <copyright file="ConstraintValue.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.PSM.DTO
{
    using System;

    /// <summary>
    /// A single value of a PrimitiveConstraint, which is a boolean, a number, a string, the unique identifier of a
    /// referenced element, or null
    /// </summary>
    /// <remarks>
    /// The alternatives occupy distinct JSON token types, so a serializer discriminates on the token alone.
    /// </remarks>
    public readonly struct ConstraintValue : IEquatable<ConstraintValue>
    {
        /// <summary>
        /// The boolean that is held when <see cref="Kind"/> is <see cref="ConstraintValueKind.Boolean"/>
        /// </summary>
        private readonly bool booleanValue;

        /// <summary>
        /// The number that is held when <see cref="Kind"/> is <see cref="ConstraintValueKind.Number"/>
        /// </summary>
        private readonly double numberValue;

        /// <summary>
        /// The string that is held when <see cref="Kind"/> is <see cref="ConstraintValueKind.String"/>
        /// </summary>
        private readonly string stringValue;

        /// <summary>
        /// The unique identifier that is held when <see cref="Kind"/> is <see cref="ConstraintValueKind.Guid"/>
        /// </summary>
        private readonly Guid guidValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConstraintValue"/> struct
        /// </summary>
        /// <param name="kind">The alternative that is held.</param>
        /// <param name="booleanValue">The boolean that is held.</param>
        /// <param name="numberValue">The number that is held.</param>
        /// <param name="stringValue">The string that is held.</param>
        /// <param name="guidValue">The unique identifier that is held.</param>
        private ConstraintValue(ConstraintValueKind kind, bool booleanValue = default, double numberValue = default, string stringValue = null, Guid guidValue = default)
        {
            this.Kind = kind;
            this.booleanValue = booleanValue;
            this.numberValue = numberValue;
            this.stringValue = stringValue;
            this.guidValue = guidValue;
        }

        /// <summary>
        /// Gets the alternative that is held
        /// </summary>
        public ConstraintValueKind Kind { get; }

        /// <summary>
        /// Gets a value indicating whether the value is null
        /// </summary>
        public bool IsNull => this.Kind == ConstraintValueKind.Null;

        /// <summary>
        /// Gets the null value
        /// </summary>
        public static ConstraintValue Null => default;

        /// <summary>
        /// Creates a <see cref="ConstraintValue"/> that holds a boolean
        /// </summary>
        /// <param name="value">The boolean to hold.</param>
        /// <returns>The created <see cref="ConstraintValue"/>.</returns>
        public static ConstraintValue From(bool value) => new(ConstraintValueKind.Boolean, booleanValue: value);

        /// <summary>
        /// Creates a <see cref="ConstraintValue"/> that holds a number
        /// </summary>
        /// <param name="value">The number to hold.</param>
        /// <returns>The created <see cref="ConstraintValue"/>.</returns>
        public static ConstraintValue From(double value) => new(ConstraintValueKind.Number, numberValue: value);

        /// <summary>
        /// Creates a <see cref="ConstraintValue"/> that holds the unique identifier of a referenced element
        /// </summary>
        /// <param name="value">The unique identifier to hold.</param>
        /// <returns>The created <see cref="ConstraintValue"/>.</returns>
        public static ConstraintValue From(Guid value) => new(ConstraintValueKind.Guid, guidValue: value);

        /// <summary>
        /// Creates a <see cref="ConstraintValue"/> that holds a string
        /// </summary>
        /// <param name="value">The string to hold, which yields <see cref="Null"/> when it is <c>null</c>.</param>
        /// <returns>The created <see cref="ConstraintValue"/>.</returns>
        public static ConstraintValue From(string value) => value is null ? Null : new ConstraintValue(ConstraintValueKind.String, stringValue: value);

        /// <summary>
        /// Converts a boolean to a <see cref="ConstraintValue"/>
        /// </summary>
        /// <param name="value">The boolean to convert.</param>
        /// <returns>The converted <see cref="ConstraintValue"/>.</returns>
        public static implicit operator ConstraintValue(bool value) => From(value);

        /// <summary>
        /// Converts a number to a <see cref="ConstraintValue"/>
        /// </summary>
        /// <param name="value">The number to convert.</param>
        /// <returns>The converted <see cref="ConstraintValue"/>.</returns>
        public static implicit operator ConstraintValue(double value) => From(value);

        /// <summary>
        /// Converts a unique identifier to a <see cref="ConstraintValue"/>
        /// </summary>
        /// <param name="value">The unique identifier to convert.</param>
        /// <returns>The converted <see cref="ConstraintValue"/>.</returns>
        public static implicit operator ConstraintValue(Guid value) => From(value);

        /// <summary>
        /// Converts a string to a <see cref="ConstraintValue"/>
        /// </summary>
        /// <param name="value">The string to convert.</param>
        /// <returns>The converted <see cref="ConstraintValue"/>.</returns>
        public static implicit operator ConstraintValue(string value) => From(value);

        /// <summary>
        /// Asserts whether two <see cref="ConstraintValue"/> hold the same alternative and value
        /// </summary>
        /// <param name="left">The left <see cref="ConstraintValue"/>.</param>
        /// <param name="right">The right <see cref="ConstraintValue"/>.</param>
        /// <returns>True when both are equal.</returns>
        public static bool operator ==(ConstraintValue left, ConstraintValue right) => left.Equals(right);

        /// <summary>
        /// Asserts whether two <see cref="ConstraintValue"/> hold a different alternative or value
        /// </summary>
        /// <param name="left">The left <see cref="ConstraintValue"/>.</param>
        /// <param name="right">The right <see cref="ConstraintValue"/>.</param>
        /// <returns>True when both are different.</returns>
        public static bool operator !=(ConstraintValue left, ConstraintValue right) => !left.Equals(right);

        /// <summary>
        /// Queries the boolean that is held
        /// </summary>
        /// <param name="value">The boolean that is held, which is <c>false</c> when another alternative is held.</param>
        /// <returns>True when a boolean is held.</returns>
        public bool TryGetBoolean(out bool value)
        {
            value = this.booleanValue;

            return this.Kind == ConstraintValueKind.Boolean;
        }

        /// <summary>
        /// Queries the number that is held
        /// </summary>
        /// <param name="value">The number that is held, which is <c>0</c> when another alternative is held.</param>
        /// <returns>True when a number is held.</returns>
        public bool TryGetNumber(out double value)
        {
            value = this.numberValue;

            return this.Kind == ConstraintValueKind.Number;
        }

        /// <summary>
        /// Queries the string that is held
        /// </summary>
        /// <param name="value">The string that is held, which is <c>null</c> when another alternative is held.</param>
        /// <returns>True when a string is held.</returns>
        public bool TryGetString(out string value)
        {
            value = this.stringValue;

            return this.Kind == ConstraintValueKind.String;
        }

        /// <summary>
        /// Queries the unique identifier that is held
        /// </summary>
        /// <param name="value">The unique identifier that is held, which is empty when another alternative is held.</param>
        /// <returns>True when a unique identifier is held.</returns>
        public bool TryGetGuid(out Guid value)
        {
            value = this.guidValue;

            return this.Kind == ConstraintValueKind.Guid;
        }

        /// <summary>
        /// Applies the function that corresponds to the alternative that is held
        /// </summary>
        /// <typeparam name="T">The type that the functions return.</typeparam>
        /// <param name="onNull">The function applied when the value is null.</param>
        /// <param name="onBoolean">The function applied to the boolean that is held.</param>
        /// <param name="onNumber">The function applied to the number that is held.</param>
        /// <param name="onString">The function applied to the string that is held.</param>
        /// <param name="onGuid">The function applied to the unique identifier that is held.</param>
        /// <returns>The result of the applied function.</returns>
        /// <exception cref="ArgumentNullException">Thrown when any of the functions is null.</exception>
        public T Match<T>(Func<T> onNull, Func<bool, T> onBoolean, Func<double, T> onNumber, Func<string, T> onString, Func<Guid, T> onGuid)
        {
            if (onNull is null)
            {
                throw new ArgumentNullException(nameof(onNull));
            }

            if (onBoolean is null)
            {
                throw new ArgumentNullException(nameof(onBoolean));
            }

            if (onNumber is null)
            {
                throw new ArgumentNullException(nameof(onNumber));
            }

            if (onString is null)
            {
                throw new ArgumentNullException(nameof(onString));
            }

            if (onGuid is null)
            {
                throw new ArgumentNullException(nameof(onGuid));
            }

            return this.Kind switch
            {
                ConstraintValueKind.Boolean => onBoolean(this.booleanValue),
                ConstraintValueKind.Number => onNumber(this.numberValue),
                ConstraintValueKind.String => onString(this.stringValue),
                ConstraintValueKind.Guid => onGuid(this.guidValue),
                _ => onNull()
            };
        }

        /// <summary>
        /// Asserts whether the provided <see cref="ConstraintValue"/> holds the same alternative and value
        /// </summary>
        /// <param name="other">The <see cref="ConstraintValue"/> to compare with.</param>
        /// <returns>True when both are equal.</returns>
        public bool Equals(ConstraintValue other)
        {
            if (this.Kind != other.Kind)
            {
                return false;
            }

            return this.Kind switch
            {
                ConstraintValueKind.Boolean => this.booleanValue == other.booleanValue,
                ConstraintValueKind.Number => this.numberValue.CompareTo(other.numberValue) == 0,
                ConstraintValueKind.String => string.Equals(this.stringValue, other.stringValue, StringComparison.Ordinal),
                ConstraintValueKind.Guid => this.guidValue == other.guidValue,
                _ => true
            };
        }

        /// <summary>
        /// Asserts whether the provided object is a <see cref="ConstraintValue"/> that holds the same alternative and value
        /// </summary>
        /// <param name="obj">The object to compare with.</param>
        /// <returns>True when both are equal.</returns>
        public override bool Equals(object obj) => obj is ConstraintValue other && this.Equals(other);

        /// <summary>
        /// Computes the hash code of the value
        /// </summary>
        /// <returns>The hash code.</returns>
        public override int GetHashCode()
        {
            return this.Kind switch
            {
                ConstraintValueKind.Boolean => HashCode.Combine(this.Kind, this.booleanValue),
                ConstraintValueKind.Number => HashCode.Combine(this.Kind, this.numberValue),
                ConstraintValueKind.String => HashCode.Combine(this.Kind, StringComparer.Ordinal.GetHashCode(this.stringValue)),
                ConstraintValueKind.Guid => HashCode.Combine(this.Kind, this.guidValue),
                _ => HashCode.Combine(this.Kind)
            };
        }

        /// <summary>
        /// Returns the string representation of the value
        /// </summary>
        /// <returns>The string representation.</returns>
        public override string ToString()
        {
            return this.Match(
                () => "null",
                boolean => boolean.ToString(),
                number => number.ToString(System.Globalization.CultureInfo.InvariantCulture),
                text => text,
                identifier => identifier.ToString("D"));
        }
    }
}
