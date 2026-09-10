// -------------------------------------------------------------------------------------------------
// <copyright file="EnumerationExtensionsTestFixture.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Tests.PSM
{
    using System;

    using NUnit.Framework;

    using SysML2.NET.PSM.Enumerations;

    [TestFixture]
    public class EnumerationExtensionsTestFixture
    {
        [Test]
        public void VerifyToWireValue()
        {
            using (Assert.EnterMultipleScope())
            {
                Assert.That(PrimitiveConstraintOperator.lessthan.ToWireValue(), Is.EqualTo("<"));
                Assert.That(PrimitiveConstraintOperator.lessthanorequalto.ToWireValue(), Is.EqualTo("<="));
                Assert.That(PrimitiveConstraintOperator.equalto.ToWireValue(), Is.EqualTo("="));
                Assert.That(PrimitiveConstraintOperator.greaterthan.ToWireValue(), Is.EqualTo(">"));
                Assert.That(PrimitiveConstraintOperator.greaterthanorequalto.ToWireValue(), Is.EqualTo(">="));
                Assert.That(PrimitiveConstraintOperator.@in.ToWireValue(), Is.EqualTo("in"));
                Assert.That(PrimitiveConstraintOperator.instanceOf.ToWireValue(), Is.EqualTo("instanceOf"));

                Assert.That(PrimitiveConstraintRequestOperator.lessthan.ToWireValue(), Is.EqualTo("<"));
                Assert.That(PrimitiveConstraintRequestOperator.instanceOf.ToWireValue(), Is.EqualTo("instanceOf"));

                Assert.That(CompositeConstraintOperator.and.ToWireValue(), Is.EqualTo("and"));
                Assert.That(CompositeConstraintOperator.or.ToWireValue(), Is.EqualTo("or"));
                Assert.That(CompositeConstraintRequestOperator.and.ToWireValue(), Is.EqualTo("and"));
                Assert.That(CompositeConstraintRequestOperator.or.ToWireValue(), Is.EqualTo("or"));
            }

            using (Assert.EnterMultipleScope())
            {
                Assert.That(() => ((PrimitiveConstraintOperator)999).ToWireValue(), Throws.TypeOf<ArgumentOutOfRangeException>());
                Assert.That(() => ((CompositeConstraintOperator)999).ToWireValue(), Throws.TypeOf<ArgumentOutOfRangeException>());
                Assert.That(() => ((PrimitiveConstraintRequestOperator)999).ToWireValue(), Throws.TypeOf<ArgumentOutOfRangeException>());
                Assert.That(() => ((CompositeConstraintRequestOperator)999).ToWireValue(), Throws.TypeOf<ArgumentOutOfRangeException>());
            }
        }
    }
}
