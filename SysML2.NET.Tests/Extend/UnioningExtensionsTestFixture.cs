// -------------------------------------------------------------------------------------------------
// <copyright file="UnioningExtensionsTestFixture.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Core.POCO.Root.Annotations;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Exceptions;

    using SysMLType = SysML2.NET.Core.POCO.Core.Types.Type;

    [TestFixture]
    public class UnioningExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeTypeUnioned()
        {
            // Null subject → ArgumentNullException.
            Assert.That(() => ((IUnioning)null).ComputeTypeUnioned(), Throws.TypeOf<ArgumentNullException>());

            // Empty Unioning (OwningRelatedElement is null) → [1..1] violation: IncompleteModelException.
            var emptyUnioning = new Unioning();

            Assert.That(() => emptyUnioning.ComputeTypeUnioned(), Throws.TypeOf<IncompleteModelException>());

            // OwningRelatedElement is an IType → returns the same instance.
            var type = new SysMLType();
            var unioningWithType = new Unioning();

            ((IContainedRelationship)unioningWithType).OwningRelatedElement = type;

            Assert.That(unioningWithType.ComputeTypeUnioned(), Is.SameAs(type));

            // OwningRelatedElement is a non-IType element (Annotation) → [1..1] type violation: IncompleteModelException.
            var annotation = new Annotation();
            var unioningWithAnnotation = new Unioning();

            ((IContainedRelationship)unioningWithAnnotation).OwningRelatedElement = annotation;

            Assert.That(() => unioningWithAnnotation.ComputeTypeUnioned(), Throws.TypeOf<IncompleteModelException>());
        }
    }
}
