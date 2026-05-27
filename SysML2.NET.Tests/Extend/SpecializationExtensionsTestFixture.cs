// -------------------------------------------------------------------------------------------------
// <copyright file="SpecializationExtensionsTestFixture.cs" company="Starion Group S.A.">
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

    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Extensions;

    using Type = SysML2.NET.Core.POCO.Core.Types.Type;

    [TestFixture]
    public class SpecializationExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeOwningType()
        {
            Assert.That(() => ((ISpecialization)null).ComputeOwningType(), Throws.TypeOf<ArgumentNullException>());

            var emptySpecialization = new Specialization();

            Assert.That(emptySpecialization.ComputeOwningType(), Is.Null);

            var owningType = new Type();
            var specialization = new Specialization();

            owningType.AssignOwnership(specialization);

            Assert.That(specialization.ComputeOwningType(), Is.SameAs(owningType));

            // NOTE: assigning a non-IType as OwningRelatedElement is not guarded by the public
            // AssignOwnership API for Specialization, so we directly set the backing field via
            // the IContainedRelationship explicit interface to exercise the as-cast-returns-null
            // path, which proves ComputeOwningType returns null when the owner is not an IType.
            var nonTypeSpecialization = new Specialization();
            var nonTypeOwner = new Namespace();

            ((IContainedRelationship)nonTypeSpecialization).OwningRelatedElement = nonTypeOwner;

            Assert.That(nonTypeSpecialization.ComputeOwningType(), Is.Null);
        }
    }
}
