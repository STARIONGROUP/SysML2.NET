// -------------------------------------------------------------------------------------------------
// <copyright file="FloatingPointComparer.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
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

namespace SysML2.NET.Extensions.Comparers
{
    using System;

    /// <summary>
    /// Provides robust equality comparison helpers for floating-point values
    /// using a combination of absolute and relative tolerance.
    /// </summary>
    public static class FloatingPointComparer
    {
        /// <summary>
        /// Default epsilon for double-precision comparisons.
        /// </summary>
        public const double DefaultDoubleEpsilon = 1e-9;

        /// <summary>
        /// Default epsilon for single-precision comparisons.
        /// </summary>
        public const float DefaultFloatEpsilon = 1e-6f;

        /// <summary>
        /// Determines whether two <see cref="double"/> values are nearly equal.
        /// </summary>
        public static bool NearlyEqual(double left, double right, double epsilon = DefaultDoubleEpsilon)
        {
            if (left == right)
            {
                return true;
            }

            var difference = Math.Abs(left - right);
            var scale = Math.Min(
                Math.Abs(left) + Math.Abs(right),
                double.MaxValue);

            return difference <= Math.Max(epsilon, epsilon * scale);
        }

        /// <summary>
        /// Determines whether two <see cref="float"/> values are nearly equal.
        /// </summary>
        public static bool NearlyEqual(float left, float right, float epsilon = DefaultFloatEpsilon)
        {
            if (left == right)
            {
                return true;
            }

            var difference = Math.Abs(left - right);
            var scale = Math.Min(
                Math.Abs(left) + Math.Abs(right),
                float.MaxValue);

            return difference <= Math.Max(epsilon, epsilon * scale);
        }
    }
}
