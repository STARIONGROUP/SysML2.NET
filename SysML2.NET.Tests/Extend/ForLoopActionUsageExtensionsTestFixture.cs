// -------------------------------------------------------------------------------------------------
// <copyright file="ForLoopActionUsageExtensionsTestFixture.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Core.POCO.Kernel.FeatureValues;
    using SysML2.NET.Core.POCO.Systems.Actions;
    using SysML2.NET.Core.POCO.Systems.Attributes;
    using SysML2.NET.Core.POCO.Systems.DefinitionAndUsage;
    using SysML2.NET.Extensions;

    [TestFixture]
    public class ForLoopActionUsageExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeLoopVariable()
        {
            Assert.That(() => ((IForLoopActionUsage)null).ComputeLoopVariable(), Throws.TypeOf<ArgumentNullException>());

            // No ownedFeature -> ownedFeature.FirstOrDefault() is null -> null.
            Assert.That(new ForLoopActionUsage().ComputeLoopVariable(), Is.Null);

            // First ownedFeature IS a ReferenceUsage -> returned via the as-cast.
            var subjectWithLoopVariable = new ForLoopActionUsage();
            var loopVariable = new ReferenceUsage();
            subjectWithLoopVariable.AssignOwnership(new FeatureMembership(), loopVariable);

            // First ownedFeature is NOT a ReferenceUsage -> the as-cast yields null.
            var subjectWithNonReferenceFirstFeature = new ForLoopActionUsage();
            subjectWithNonReferenceFirstFeature.AssignOwnership(new FeatureMembership(), new AttributeUsage());

            using (Assert.EnterMultipleScope())
            {
                // loopVariable = ownedFeature->first() as ReferenceUsage
                Assert.That(subjectWithLoopVariable.ComputeLoopVariable(), Is.SameAs(loopVariable));
                Assert.That(subjectWithNonReferenceFirstFeature.ComputeLoopVariable(), Is.Null);
            }
        }

        [Test]
        public void VerifyComputeSeqArgument()
        {
            Assert.That(() => ((IForLoopActionUsage)null).ComputeSeqArgument(), Throws.TypeOf<ArgumentNullException>());

            // No input parameter -> argument(1) is out of range -> null.
            Assert.That(new ForLoopActionUsage().ComputeSeqArgument(), Is.Null);

            var seqExpression = new LiteralInteger();
            var subjectWithSeqArgument = new ForLoopActionUsage();
            var inputParameter = new ReferenceUsage { Direction = FeatureDirectionKind.In };
            subjectWithSeqArgument.AssignOwnership(new FeatureMembership(), inputParameter);
            inputParameter.AssignOwnership(new FeatureValue(), seqExpression);

            // seqArgument = argument(1)
            Assert.That(subjectWithSeqArgument.ComputeSeqArgument(), Is.SameAs(seqExpression));
        }
    }
}
