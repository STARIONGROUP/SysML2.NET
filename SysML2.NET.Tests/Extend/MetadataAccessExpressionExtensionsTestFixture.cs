// -------------------------------------------------------------------------------------------------
// <copyright file="MetadataAccessExpressionExtensionsTestFixture.cs" company="Starion Group S.A.">
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
    using System.Collections.Generic;

    using NUnit.Framework;

    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Kernel.Expressions;
    using SysML2.NET.Core.POCO.Kernel.Packages;
    using SysML2.NET.Core.POCO.Root.Annotations;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Exceptions;
    using SysML2.NET.Extensions;

    [TestFixture]
    public class MetadataAccessExpressionExtensionsTestFixture
    {
        [Test]
        public void ComputeMetaclassFeatureOperation_ThrowsNotSupportedException()
        {
            // Blocked: requires MOF metaclass reflection registry (out of scope for this batch).
            Assert.That(
                () => ((IMetadataAccessExpression)null).ComputeMetaclassFeatureOperation(),
                Throws.TypeOf<NotSupportedException>());
        }

        [Test]
        public void Verify_ComputeRedefinedEvaluateOperation()
        {
            Assert.That(
                () => ((IMetadataAccessExpression)null).ComputeRedefinedEvaluateOperation(null),
                Throws.TypeOf<ArgumentNullException>());

            // referencedElement is [1..1] derived; an empty subject propagates the IncompleteModelException
            // from ComputeReferencedElement before the metaclassFeature() tail is reached.
            var emptySubject = new MetadataAccessExpression();

            Assert.That(() => emptySubject.ComputeRedefinedEvaluateOperation(null), Throws.TypeOf<IncompleteModelException>());

            // For any well-formed subject, the trailing `->including(metaclassFeature())` step calls
            // ComputeMetaclassFeatureOperation, which is intentionally left as a stub (MOF reflection
            // infrastructure is out of scope). The call therefore propagates NotSupportedException —
            // this verifies the calling chain is wired correctly per OCL.
            var validSubject = new MetadataAccessExpression();
            validSubject.AssignOwnership(new OwningMembership(), new Package());

            Assert.That(() => validSubject.ComputeRedefinedEvaluateOperation(null), Throws.TypeOf<NotSupportedException>());
        }

        [Test]
        public void Verify_ComputeRedefinedModelLevelEvaluableOperation()
        {
            Assert.That(
                () => ((IMetadataAccessExpression)null).ComputeRedefinedModelLevelEvaluableOperation(null),
                Throws.TypeOf<ArgumentNullException>());

            var subject = new MetadataAccessExpression();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(subject.ComputeRedefinedModelLevelEvaluableOperation(null), Is.True);
                Assert.That(subject.ComputeRedefinedModelLevelEvaluableOperation(new List<IFeature>()), Is.True);
            }
        }

        [Test]
        public void Verify_ComputeReferencedElement()
        {
            Assert.That(
                () => ((IMetadataAccessExpression)null).ComputeReferencedElement(),
                Throws.TypeOf<ArgumentNullException>());

            var emptySubject = new MetadataAccessExpression();

            Assert.That(() => emptySubject.ComputeReferencedElement(), Throws.TypeOf<IncompleteModelException>());

            var singleOwningMembershipSubject = new MetadataAccessExpression();
            var referenced = new Comment();
            singleOwningMembershipSubject.AssignOwnership(new OwningMembership(), referenced);

            Assert.That(singleOwningMembershipSubject.ComputeReferencedElement(), Is.SameAs(referenced));

            var featureMembershipOnlySubject = new MetadataAccessExpression();
            featureMembershipOnlySubject.AssignOwnership(new FeatureMembership(), new Feature());

            Assert.That(() => featureMembershipOnlySubject.ComputeReferencedElement(), Throws.TypeOf<IncompleteModelException>());

            var mixedSubject = new MetadataAccessExpression();
            mixedSubject.AssignOwnership(new FeatureMembership(), new Feature());
            var mixedReferenced = new Package();
            mixedSubject.AssignOwnership(new OwningMembership(), mixedReferenced);

            // Proves the filter is "not IFeatureMembership", not positional [0].
            Assert.That(mixedSubject.ComputeReferencedElement(), Is.SameAs(mixedReferenced));
        }
    }
}
