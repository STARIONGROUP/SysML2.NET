// -------------------------------------------------------------------------------------------------
// <copyright file="ElementFilterMembershipExtensionsTestFixture.cs" company="Starion Group S.A.">
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

    using SysML2.NET.Core.POCO.Kernel.Expressions;
    using SysML2.NET.Core.POCO.Kernel.Packages;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Exceptions;
    using SysML2.NET.Extensions;

    [TestFixture]
    public class ElementFilterMembershipExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeCondition()
        {
            Assert.That(() => ((IElementFilterMembership)null).ComputeCondition(), Throws.TypeOf<ArgumentNullException>());

            // Empty OwnedRelatedElement → [1..1] violation: throws IncompleteModelException.
            var emptyMembership = new ElementFilterMembership();

            Assert.That(() => emptyMembership.ComputeCondition(), Throws.TypeOf<IncompleteModelException>());

            // Single IExpression wired via the public API → returned.
            var owningNamespace = new Namespace();
            var membership = new ElementFilterMembership();
            var literalBoolean = new LiteralBoolean();

            owningNamespace.AssignOwnership(membership, literalBoolean);

            Assert.That(membership.ComputeCondition(), Is.SameAs(literalBoolean));

            // Two IExpressions in OwnedRelatedElement → [1..1] violation: throws IncompleteModelException.
            var twoExprMembership = new ElementFilterMembership();
            var firstExpression = new LiteralBoolean();
            var secondExpression = new LiteralBoolean();

            ((IContainedRelationship)twoExprMembership).OwnedRelatedElement.Add(firstExpression);
            ((IContainedRelationship)twoExprMembership).OwnedRelatedElement.Add(secondExpression);

            Assert.That(() => twoExprMembership.ComputeCondition(), Throws.TypeOf<IncompleteModelException>());

            // Mixed-type owned related elements: exactly one IExpression alongside a non-IExpression (Namespace).
            // The OfType<IExpression>() projection MUST pick out the IExpression regardless of its position
            // (this is the core robustness guarantee — never positionally index the unfiltered collection).
            var mixedMembership = new ElementFilterMembership();
            var siblingNonExpression = new Namespace();
            var mixedExpression = new LiteralBoolean();

            ((IContainedRelationship)mixedMembership).OwnedRelatedElement.Add(siblingNonExpression);
            ((IContainedRelationship)mixedMembership).OwnedRelatedElement.Add(mixedExpression);

            Assert.That(mixedMembership.ComputeCondition(), Is.SameAs(mixedExpression));

            // OwnedRelatedElement populated with non-IExpression element(s) only → no IExpression match:
            // [1..1] violation, throws IncompleteModelException.
            var nonExprMembership = new ElementFilterMembership();
            var nonExprElement = new Namespace();

            ((IContainedRelationship)nonExprMembership).OwnedRelatedElement.Add(nonExprElement);

            Assert.That(() => nonExprMembership.ComputeCondition(), Throws.TypeOf<IncompleteModelException>());
        }
    }
}
