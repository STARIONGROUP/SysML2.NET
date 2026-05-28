// -------------------------------------------------------------------------------------------------
// <copyright file="DisjoiningExtensionsTestFixture.cs" company="Starion Group S.A.">
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

    using NUnit.Framework;

    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Extensions;

    using Type = SysML2.NET.Core.POCO.Core.Types.Type;

    [TestFixture]
    public class DisjoiningExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeOwningType()
        {
            Assert.That(() => ((IDisjoining)null).ComputeOwningType(), Throws.TypeOf<ArgumentNullException>());

            var emptyDisjoining = new Disjoining();

            Assert.That(emptyDisjoining.ComputeOwningType(), Is.Null);

            var owningType = new Type();
            var disjoining = new Disjoining();

            owningType.AssignOwnership(disjoining);

            Assert.That(disjoining.ComputeOwningType(), Is.SameAs(owningType));

            var nonTypeDisjoining = new Disjoining();
            var nonTypeOwner = new Namespace();

            ((IContainedRelationship)nonTypeDisjoining).OwningRelatedElement = nonTypeOwner;

            Assert.That(nonTypeDisjoining.ComputeOwningType(), Is.Null);
        }
    }
}
