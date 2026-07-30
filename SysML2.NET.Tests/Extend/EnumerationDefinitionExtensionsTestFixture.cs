// -------------------------------------------------------------------------------------------------
// <copyright file="EnumerationDefinitionExtensionsTestFixture.cs" company="Starion Group S.A.">
// 
//   Copyright (C) 2022-2026 Starion Group S.A.
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

    using NUnit.Framework;

    using SysML2.NET.Core.POCO.Systems.Attributes;
    using SysML2.NET.Core.POCO.Systems.DefinitionAndUsage;
    using SysML2.NET.Core.POCO.Systems.Enumerations;
    using SysML2.NET.Extensions;

    [TestFixture]
    public class EnumerationDefinitionExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeEnumeratedValue()
        {
            Assert.That(() => ((IEnumerationDefinition)null).ComputeEnumeratedValue(), Throws.TypeOf<ArgumentNullException>());

            // Empty: no variant memberships → empty list.
            var emptySubject = new EnumerationDefinition();

            Assert.That(emptySubject.ComputeEnumeratedValue(), Is.Empty);

            // Populated: two VariantMemberships, one owning an EnumerationUsage variant, the other owning a
            // non-EnumerationUsage variant (AttributeUsage). Only the EnumerationUsage survives the kind filter,
            // and the variantMembership order is preserved.
            var subject = new EnumerationDefinition();
            var enumeratedValue = new EnumerationUsage();
            var nonEnumeratedVariant = new AttributeUsage();
            subject.AssignOwnership(new VariantMembership(), enumeratedValue);
            subject.AssignOwnership(new VariantMembership(), nonEnumeratedVariant);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(subject.ComputeEnumeratedValue(), Is.EqualTo([enumeratedValue]));
                Assert.That(subject.ComputeEnumeratedValue(), Does.Not.Contain(nonEnumeratedVariant));
            }
        }
    }
}
