// -------------------------------------------------------------------------------------------------
// <copyright file="FloatingPointComparerTestFixture.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Extensions.Tests.Comparers
{
    using NUnit.Framework;

    using SysML2.NET.Extensions.Comparers;

    [TestFixture]
    public class FloatingPointComparerTestFixture
    {
        [Test]
        public void NearlyEqual_Double_WhenValuesAreExactlyEqual_ReturnsTrue()
        {
            Assert.That(FloatingPointComparer.NearlyEqual(1.234, 1.234), Is.True);
        }

        [Test]
        public void NearlyEqual_Double_WhenValuesDifferWithinAbsoluteEpsilonNearZero_ReturnsTrue()
        {
            var epsilon = FloatingPointComparer.DefaultDoubleEpsilon;

            Assert.That(FloatingPointComparer.NearlyEqual(0.0, epsilon * 0.5, epsilon), Is.True);
            Assert.That(FloatingPointComparer.NearlyEqual(0.0, -epsilon * 0.5, epsilon), Is.True);
        }

        [Test]
        public void NearlyEqual_Double_WhenValuesDifferMoreThanAbsoluteEpsilonNearZero_ReturnsFalse()
        {
            var epsilon = FloatingPointComparer.DefaultDoubleEpsilon;

            Assert.That(FloatingPointComparer.NearlyEqual(0.0, epsilon * 2.0, epsilon), Is.False);
            Assert.That(FloatingPointComparer.NearlyEqual(0.0, -epsilon * 2.0, epsilon), Is.False);
        }

        [Test]
        public void NearlyEqual_Double_WhenValuesDifferWithinRelativeTolerance_ReturnsTrue()
        {
            // Ensure we are in the relative regime: epsilon*scale dominates epsilon.
            // For left=1e9, right=1e9+0.5, scale≈2e9 => epsilon*scale≈2, so diff=0.5 should be true.
            var epsilon = FloatingPointComparer.DefaultDoubleEpsilon;

            var left = 1_000_000_000d;
            var right = left + 0.5d;

            Assert.That(FloatingPointComparer.NearlyEqual(left, right, epsilon), Is.True);
        }

        [Test]
        public void NearlyEqual_Double_WhenValuesDifferOutsideRelativeTolerance_ReturnsFalse()
        {
            // For left=1e9, scale≈2e9 => epsilon*scale≈2, so diff=3 should be false.
            var epsilon = FloatingPointComparer.DefaultDoubleEpsilon;

            var left = 1_000_000_000d;
            var right = left + 3d;

            Assert.That(FloatingPointComparer.NearlyEqual(left, right, epsilon), Is.False);
        }

        [Test]
        public void NearlyEqual_Double_WhenOneValueIsInfinityAndOtherIsSameInfinity_ReturnsTrue()
        {
            Assert.That(FloatingPointComparer.NearlyEqual(double.PositiveInfinity, double.PositiveInfinity), Is.True);
            Assert.That(FloatingPointComparer.NearlyEqual(double.NegativeInfinity, double.NegativeInfinity), Is.True);
        }

        [Test]
        public void NearlyEqual_Double_WhenOneValueIsInfinityAndOtherIsFinite_ReturnsFalse()
        {
            Assert.That(FloatingPointComparer.NearlyEqual(double.PositiveInfinity, 1.0), Is.False);
            Assert.That(FloatingPointComparer.NearlyEqual(double.NegativeInfinity, -1.0), Is.False);
        }

        [Test]
        public void NearlyEqual_Double_WhenEitherIsNaN_ReturnsFalse()
        {
            Assert.That(FloatingPointComparer.NearlyEqual(double.NaN, double.NaN), Is.False);
            Assert.That(FloatingPointComparer.NearlyEqual(double.NaN, 0.0), Is.False);
            Assert.That(FloatingPointComparer.NearlyEqual(0.0, double.NaN), Is.False);
        }

        [Test]
        public void NearlyEqual_Float_WhenValuesAreExactlyEqual_ReturnsTrue()
        {
            Assert.That(FloatingPointComparer.NearlyEqual(1.234f, 1.234f), Is.True);
        }

        [Test]
        public void NearlyEqual_Float_WhenValuesDifferWithinAbsoluteEpsilonNearZero_ReturnsTrue()
        {
            var epsilon = FloatingPointComparer.DefaultFloatEpsilon;

            Assert.That(FloatingPointComparer.NearlyEqual(0f, epsilon * 0.5f, epsilon), Is.True);
            Assert.That(FloatingPointComparer.NearlyEqual(0f, -epsilon * 0.5f, epsilon), Is.True);
        }

        [Test]
        public void NearlyEqual_Float_WhenValuesDifferMoreThanAbsoluteEpsilonNearZero_ReturnsFalse()
        {
            var epsilon = FloatingPointComparer.DefaultFloatEpsilon;

            Assert.That(FloatingPointComparer.NearlyEqual(0f, epsilon * 2f, epsilon), Is.False);
            Assert.That(FloatingPointComparer.NearlyEqual(0f, -epsilon * 2f, epsilon), Is.False);
        }

        [Test]
        public void NearlyEqual_Float_WhenValuesDifferWithinRelativeTolerance_ReturnsTrue()
        {
            // For left=1e6, scale≈2e6 => epsilon*scale≈2 (with epsilon=1e-6) so diff=0.5 should be true.
            var epsilon = FloatingPointComparer.DefaultFloatEpsilon;

            var left = 1_000_000f;
            var right = left + 0.5f;

            Assert.That(FloatingPointComparer.NearlyEqual(left, right, epsilon), Is.True);
        }

        [Test]
        public void NearlyEqual_Float_WhenValuesDifferOutsideRelativeTolerance_ReturnsFalse()
        {
            // For left=1e6, scale≈2e6 => epsilon*scale≈2 so diff=3 should be false.
            var epsilon = FloatingPointComparer.DefaultFloatEpsilon;

            var left = 1_000_000f;
            var right = left + 3f;

            Assert.That(FloatingPointComparer.NearlyEqual(left, right, epsilon), Is.False);
        }

        [Test]
        public void NearlyEqual_Float_WhenOneValueIsInfinityAndOtherIsSameInfinity_ReturnsTrue()
        {
            Assert.That(FloatingPointComparer.NearlyEqual(float.PositiveInfinity, float.PositiveInfinity), Is.True);
            Assert.That(FloatingPointComparer.NearlyEqual(float.NegativeInfinity, float.NegativeInfinity), Is.True);
        }

        [Test]
        public void NearlyEqual_Float_WhenOneValueIsInfinityAndOtherIsFinite_ReturnsFalse()
        {
            Assert.That(FloatingPointComparer.NearlyEqual(float.PositiveInfinity, 1f), Is.False);
            Assert.That(FloatingPointComparer.NearlyEqual(float.NegativeInfinity, -1f), Is.False);
        }

        [Test]
        public void NearlyEqual_Float_WhenEitherIsNaN_ReturnsFalse()
        {
            Assert.That(FloatingPointComparer.NearlyEqual(float.NaN, float.NaN), Is.False);
            Assert.That(FloatingPointComparer.NearlyEqual(float.NaN, 0f), Is.False);
            Assert.That(FloatingPointComparer.NearlyEqual(0f, float.NaN), Is.False);
        }

        [Test]
        public void NearlyEqual_UsesProvidedEpsilon()
        {
            // Make epsilon big on purpose so the comparison returns true.
            const double epsilon = 1e-3;

            Assert.That(FloatingPointComparer.NearlyEqual(1.0, 1.0005, epsilon), Is.True);

            // And small so it returns false.
            const double smallEpsilon = 1e-9;

            Assert.That(FloatingPointComparer.NearlyEqual(1.0, 1.0005, smallEpsilon), Is.False);
        }

        [Test]
        public void NearlyEqual_IsSymmetric_ForRepresentativeValues()
        {
            var epsilonD = FloatingPointComparer.DefaultDoubleEpsilon;
            var aD = 1_000_000_000d;
            var bD = aD + 0.5d;

            Assert.That(FloatingPointComparer.NearlyEqual(aD, bD, epsilonD), Is.EqualTo(FloatingPointComparer.NearlyEqual(bD, aD, epsilonD)));

            var epsilonF = FloatingPointComparer.DefaultFloatEpsilon;
            var aF = 1_000_000f;
            var bF = aF + 0.5f;

            Assert.That(FloatingPointComparer.NearlyEqual(aF, bF, epsilonF), Is.EqualTo(FloatingPointComparer.NearlyEqual(bF, aF, epsilonF)));
        }
    }
}
