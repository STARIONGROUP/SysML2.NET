// -------------------------------------------------------------------------------------------------
// <copyright file="IntersectingExtensionsTestFixture.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Exceptions;

    using Type = SysML2.NET.Core.POCO.Core.Types.Type;

    [TestFixture]
    public class IntersectingExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeTypeIntersected()
        {
            // Null subject → ArgumentNullException.
            Assert.That(() => ((IIntersecting)null).ComputeTypeIntersected(), Throws.TypeOf<ArgumentNullException>());

            // Empty Intersecting (OwningRelatedElement is null) → [1..1] violation: IncompleteModelException.
            var emptyIntersecting = new Intersecting();

            Assert.That(() => emptyIntersecting.ComputeTypeIntersected(), Throws.TypeOf<IncompleteModelException>());

            // OwningRelatedElement is an IType → returns the same instance.
            var type = new Type();
            var intersectingWithType = new Intersecting();

            ((IContainedRelationship)intersectingWithType).OwningRelatedElement = type;

            Assert.That(intersectingWithType.ComputeTypeIntersected(), Is.SameAs(type));

            // OwningRelatedElement is a non-IType (Namespace) → [1..1] type violation: IncompleteModelException.
            var namespaceObj = new Namespace();
            var intersectingWithNamespace = new Intersecting();

            ((IContainedRelationship)intersectingWithNamespace).OwningRelatedElement = namespaceObj;

            Assert.That(() => intersectingWithNamespace.ComputeTypeIntersected(), Throws.TypeOf<IncompleteModelException>());
        }
    }
}
