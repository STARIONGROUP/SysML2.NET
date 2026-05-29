// -------------------------------------------------------------------------------------------------
// <copyright file="AnalysisCaseUsageExtensionsTestFixture.cs" company="Starion Group S.A.">
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

    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Kernel.Functions;
    using SysML2.NET.Core.POCO.Systems.AnalysisCases;
    using SysML2.NET.Core.POCO.Systems.DefinitionAndUsage;
    using SysML2.NET.Extensions;

    [TestFixture]
    public class AnalysisCaseUsageExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeAnalysisCaseDefinition()
        {
            Assert.That(() => ((IAnalysisCaseUsage)null).ComputeAnalysisCaseDefinition(), Throws.TypeOf<ArgumentNullException>());

            var analysisCaseUsage = new AnalysisCaseUsage();

            // Empty case: no FeatureTyping whose Type is an IAnalysisCaseDefinition → null.
            Assert.That(analysisCaseUsage.ComputeAnalysisCaseDefinition(), Is.Null);

            // Negative case: FeatureTyping whose Type is a Usage (not IAnalysisCaseDefinition) — no match → null.
            var nonDefinitionTyping = new FeatureTyping { Type = new Usage() };
            analysisCaseUsage.AssignOwnership(nonDefinitionTyping);

            Assert.That(analysisCaseUsage.ComputeAnalysisCaseDefinition(), Is.Null);

            // Populated case: FeatureTyping whose Type is an AnalysisCaseDefinition → returns the AnalysisCaseDefinition.
            var analysisCaseDefinition = new AnalysisCaseDefinition();
            var analysisCaseDefinitionTyping = new FeatureTyping { Type = analysisCaseDefinition };
            analysisCaseUsage.AssignOwnership(analysisCaseDefinitionTyping);

            Assert.That(analysisCaseUsage.ComputeAnalysisCaseDefinition(), Is.SameAs(analysisCaseDefinition));
        }

        [Test]
        public void VerifyComputeResultExpression()
        {
            Assert.That(() => ((IAnalysisCaseUsage)null).ComputeResultExpression(), Throws.TypeOf<ArgumentNullException>());

            var analysisCaseUsage = new AnalysisCaseUsage();

            // Empty case: no ResultExpressionMembership in featureMembership → null.
            Assert.That(analysisCaseUsage.ComputeResultExpression(), Is.Null);

            // Populated case: ResultExpressionMembership owns an Expression → returns the Expression.
            var expression = new Expression();
            var resultExpressionMembership = new ResultExpressionMembership();
            analysisCaseUsage.AssignOwnership(resultExpressionMembership, expression);

            Assert.That(analysisCaseUsage.ComputeResultExpression(), Is.SameAs(expression));
        }
    }
}
