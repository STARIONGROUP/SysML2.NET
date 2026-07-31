// -------------------------------------------------------------------------------------------------
// <copyright file="WhileLoopActionUsageExtensionsTestFixture.cs" company="Starion Group S.A.">
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

    using SysML2.NET.Core.Core.Types;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Kernel.Expressions;
    using SysML2.NET.Core.POCO.Systems.Actions;
    using SysML2.NET.Core.POCO.Systems.DefinitionAndUsage;
    using SysML2.NET.Extensions;

    [TestFixture]
    public class WhileLoopActionUsageExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeWhileArgument()
        {
            Assert.That(() => ((IWhileLoopActionUsage)null).ComputeWhileArgument(), Throws.TypeOf<ArgumentNullException>());

            // No input parameters -> inputParameter(1) is out of range -> null.
            Assert.That(new WhileLoopActionUsage().ComputeWhileArgument(), Is.Null);

            // Position-1 input parameter that IS an Expression -> returned via the as-cast.
            var whileExpression = new LiteralInteger { Direction = FeatureDirectionKind.In };
            var subjectWithWhileArgument = new WhileLoopActionUsage();
            subjectWithWhileArgument.AssignOwnership(new FeatureMembership(), whileExpression);

            // Position-1 input parameter that is NOT an Expression -> the as-cast yields null.
            var subjectWithNonExpressionFirstParameter = new WhileLoopActionUsage();
            subjectWithNonExpressionFirstParameter.AssignOwnership(new FeatureMembership(), new ReferenceUsage { Direction = FeatureDirectionKind.In });

            using (Assert.EnterMultipleScope())
            {
                // whileArgument = inputParameter(1) as Expression
                Assert.That(subjectWithWhileArgument.ComputeWhileArgument(), Is.SameAs(whileExpression));
                Assert.That(subjectWithNonExpressionFirstParameter.ComputeWhileArgument(), Is.Null);
            }
        }

        [Test]
        public void VerifyComputeUntilArgument()
        {
            Assert.That(() => ((IWhileLoopActionUsage)null).ComputeUntilArgument(), Throws.TypeOf<ArgumentNullException>());

            // No input parameters -> inputParameter(3) is out of range -> null.
            Assert.That(new WhileLoopActionUsage().ComputeUntilArgument(), Is.Null);

            // Only two input parameters -> inputParameter(3) is out of range -> null.
            var subjectWithTwoParameters = new WhileLoopActionUsage();
            subjectWithTwoParameters.AssignOwnership(new FeatureMembership(), new LiteralInteger { Direction = FeatureDirectionKind.In });
            subjectWithTwoParameters.AssignOwnership(new FeatureMembership(), new LiteralInteger { Direction = FeatureDirectionKind.In });

            // Three input parameters where position-3 IS an Expression -> returned via the as-cast.
            var untilExpression = new LiteralInteger { Direction = FeatureDirectionKind.In };
            var subjectWithUntilArgument = new WhileLoopActionUsage();
            subjectWithUntilArgument.AssignOwnership(new FeatureMembership(), new LiteralInteger { Direction = FeatureDirectionKind.In });
            subjectWithUntilArgument.AssignOwnership(new FeatureMembership(), new LiteralInteger { Direction = FeatureDirectionKind.In });
            subjectWithUntilArgument.AssignOwnership(new FeatureMembership(), untilExpression);

            // Three input parameters where position-3 is NOT an Expression -> the as-cast yields null.
            var subjectWithNonExpressionThirdParameter = new WhileLoopActionUsage();
            subjectWithNonExpressionThirdParameter.AssignOwnership(new FeatureMembership(), new LiteralInteger { Direction = FeatureDirectionKind.In });
            subjectWithNonExpressionThirdParameter.AssignOwnership(new FeatureMembership(), new LiteralInteger { Direction = FeatureDirectionKind.In });
            subjectWithNonExpressionThirdParameter.AssignOwnership(new FeatureMembership(), new ReferenceUsage { Direction = FeatureDirectionKind.In });

            using (Assert.EnterMultipleScope())
            {
                // untilArgument = inputParameter(3) as Expression
                Assert.That(subjectWithUntilArgument.ComputeUntilArgument(), Is.SameAs(untilExpression));
                Assert.That(subjectWithTwoParameters.ComputeUntilArgument(), Is.Null);
                Assert.That(subjectWithNonExpressionThirdParameter.ComputeUntilArgument(), Is.Null);
            }
        }
    }
}
