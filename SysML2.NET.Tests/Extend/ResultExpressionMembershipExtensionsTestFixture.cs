// -------------------------------------------------------------------------------------------------
// <copyright file="ResultExpressionMembershipExtensionsTestFixture.cs" company="Starion Group S.A.">
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

    using SysML2.NET.Core.POCO.Kernel.Expressions;
    using SysML2.NET.Core.POCO.Kernel.Functions;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Exceptions;
    using SysML2.NET.Extensions;

    using Type = SysML2.NET.Core.POCO.Core.Types.Type;

    [TestFixture]
    public class ResultExpressionMembershipExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeOwnedResultExpression()
        {
            Assert.That(() => ((IResultExpressionMembership)null).ComputeOwnedResultExpression(), Throws.TypeOf<ArgumentNullException>());

            // Empty OwnedRelatedElement → [1..1] violation: throws IncompleteModelException.
            var emptyMembership = new ResultExpressionMembership();

            Assert.That(() => emptyMembership.ComputeOwnedResultExpression(), Throws.TypeOf<IncompleteModelException>());

            // Single IExpression wired via the public API → returned.
            var owningType = new Type();
            var resultExpressionMembership = new ResultExpressionMembership();
            var literalBoolean = new LiteralBoolean();

            owningType.AssignOwnership(resultExpressionMembership, literalBoolean);

            Assert.That(resultExpressionMembership.ComputeOwnedResultExpression(), Is.SameAs(literalBoolean));

            // Two IExpressions in OwnedRelatedElement → [1..1] violation: throws IncompleteModelException.
            var twoExprMembership = new ResultExpressionMembership();
            var firstExpression = new LiteralBoolean();
            var secondExpression = new LiteralBoolean();

            ((IContainedRelationship)twoExprMembership).OwnedRelatedElement.Add(firstExpression);
            ((IContainedRelationship)twoExprMembership).OwnedRelatedElement.Add(secondExpression);

            Assert.That(() => twoExprMembership.ComputeOwnedResultExpression(), Throws.TypeOf<IncompleteModelException>());

            // Mixed-type owned related elements: exactly one IExpression alongside a non-IExpression (Type).
            // The OfType<IExpression>() projection MUST pick out the IExpression regardless of its position
            // (this is the core robustness guarantee — never positionally index the unfiltered collection).
            var mixedMembership = new ResultExpressionMembership();
            var siblingNonExpression = new Type();
            var mixedExpression = new LiteralBoolean();

            ((IContainedRelationship)mixedMembership).OwnedRelatedElement.Add(siblingNonExpression);
            ((IContainedRelationship)mixedMembership).OwnedRelatedElement.Add(mixedExpression);

            Assert.That(mixedMembership.ComputeOwnedResultExpression(), Is.SameAs(mixedExpression));

            // OwnedRelatedElement populated with non-IExpression element(s) only → no IExpression match:
            // [1..1] violation, throws IncompleteModelException.
            var nonExprMembership = new ResultExpressionMembership();
            var nonExprElement = new Type();

            ((IContainedRelationship)nonExprMembership).OwnedRelatedElement.Add(nonExprElement);

            Assert.That(() => nonExprMembership.ComputeOwnedResultExpression(), Throws.TypeOf<IncompleteModelException>());
        }
    }
}
