// -------------------------------------------------------------------------------------------------
// <copyright file="InvocationExpressionExtensionsTestFixture.cs" company="Starion Group S.A.">
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

    using SysML2.NET.Core.Core.Types;
    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Kernel.Behaviors;
    using SysML2.NET.Core.POCO.Kernel.Expressions;
    using SysML2.NET.Core.POCO.Kernel.FeatureValues;
    using SysML2.NET.Core.POCO.Kernel.Functions;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Extensions;

    [TestFixture]
    public class InvocationExpressionExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeRedefinedEvaluateOperation()
        {
            var subject = new InvocationExpression();

            // For later: deferred — needs Function-application engine (no OCL).
            Assert.That(
                () => subject.ComputeRedefinedEvaluateOperation(null),
                Throws.TypeOf<NotSupportedException>());
        }

        [Test]
        public void VerifyComputeRedefinedModelLevelEvaluableOperation()
        {
            // Null guard.
            Assert.That(
                () => ((IInvocationExpression)null).ComputeRedefinedModelLevelEvaluableOperation([]),
                Throws.TypeOf<ArgumentNullException>());

            // False branch: a single argument whose ModelLevelEvaluable is deterministically false
            // (a base Expression carrying a non-implied Specialization). All(...) is false, so the
            // && short-circuits before function.isModelLevelEvaluable -> returns false, no throw.
            var falseArgument = new Expression();
            falseArgument.AssignOwnership(new Specialization { IsImplied = false, Specific = falseArgument });

            var falseSubject = new InvocationExpression();

            var invokedBehavior = new Behavior();
            falseSubject.AssignOwnership(new OwningMembership(), invokedBehavior);

            var inputParameter = new Feature { Direction = FeatureDirectionKind.In };
            invokedBehavior.AssignOwnership(new FeatureMembership(), inputParameter);

            var argumentFeature = new Feature();
            argumentFeature.AssignOwnership(new Redefinition { RedefinedFeature = inputParameter });
            argumentFeature.AssignOwnership(new FeatureValue(), falseArgument);
            falseSubject.AssignOwnership(new FeatureMembership(), argumentFeature);

            // #322 stub-blocker branch: empty arguments -> All(...) over an empty source -> true, so the
            // && evaluates the right operand and reaches function.isModelLevelEvaluable. A FeatureTyping to
            // a Function is wired so function resolves non-null (else the access would NRE, not reach the
            // stub); the Function's ComputeIsModelLevelEvaluable is still a NotSupportedException stub.
            var stubSubject = new InvocationExpression();
            stubSubject.AssignOwnership(new FeatureTyping { Type = new Function() });

            using (Assert.EnterMultipleScope())
            {
                Assert.That(falseSubject.ComputeRedefinedModelLevelEvaluableOperation([]), Is.False);

                // For later: reaches function.isModelLevelEvaluable, deferred (GitHub #322).
                Assert.That(
                    () => stubSubject.ComputeRedefinedModelLevelEvaluableOperation([]),
                    Throws.TypeOf<NotSupportedException>());
            }
        }
    }
}
