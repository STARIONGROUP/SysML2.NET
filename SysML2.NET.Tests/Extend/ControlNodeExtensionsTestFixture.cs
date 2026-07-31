// -------------------------------------------------------------------------------------------------
// <copyright file="ControlNodeExtensionsTestFixture.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Tests.Extend
{
    using System;

    using Moq;

    using NUnit.Framework;

    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Kernel.Multiplicities;
    using SysML2.NET.Core.POCO.Systems.Actions;

    [TestFixture]
    public class ControlNodeExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeMultiplicityHasBoundsOperation()
        {
            // ControlNode is abstract; ForkNode is a concrete IControlNode used as the subject.
            var controlNode = new ForkNode();

            Assert.That(() => ((IControlNode)null).ComputeMultiplicityHasBoundsOperation(null, 1, "1"), Throws.TypeOf<ArgumentNullException>());

            // mult == null -> false (the OCL "mult <> null" guard, not a throw).
            Assert.That(controlNode.ComputeMultiplicityHasBoundsOperation(null, 1, "1"), Is.False);

            // mult IS a MultiplicityRange -> the direct branch delegates to HasBounds(lower, upper).
            var range = new Mock<IMultiplicityRange>();
            range.Setup(x => x.HasBounds(1, "5")).Returns(true);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(controlNode.ComputeMultiplicityHasBoundsOperation(range.Object, 1, "5"), Is.True);

                // Bounds that were not set up return Moq's default (false).
                Assert.That(controlNode.ComputeMultiplicityHasBoundsOperation(range.Object, 0, "5"), Is.False);
            }

            // mult is a plain Multiplicity (NOT a MultiplicityRange) -> the supertype branch searches
            // AllSupertypes() for a MultiplicityRange whose HasBounds(lower, upper) is true.
            var rangeSupertype = new Mock<IMultiplicityRange>();
            rangeSupertype.Setup(x => x.HasBounds(1, "5")).Returns(true);

            var multiplicityWithRangeSupertype = new Mock<IMultiplicity>();
            multiplicityWithRangeSupertype.Setup(x => x.AllSupertypes()).Returns([rangeSupertype.Object]);

            var multiplicityWithoutRangeSupertype = new Mock<IMultiplicity>();
            multiplicityWithoutRangeSupertype.Setup(x => x.AllSupertypes()).Returns([]);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(controlNode.ComputeMultiplicityHasBoundsOperation(multiplicityWithRangeSupertype.Object, 1, "5"), Is.True);

                // A matching MultiplicityRange supertype exists, but the requested bounds do not match.
                Assert.That(controlNode.ComputeMultiplicityHasBoundsOperation(multiplicityWithRangeSupertype.Object, 0, "5"), Is.False);

                // No MultiplicityRange among the supertypes -> false.
                Assert.That(controlNode.ComputeMultiplicityHasBoundsOperation(multiplicityWithoutRangeSupertype.Object, 1, "5"), Is.False);
            }
        }
    }
}
