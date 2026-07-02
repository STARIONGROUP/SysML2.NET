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
    using SysML2.NET.Core.POCO.Systems.Cases;
    using SysML2.NET.Core.POCO.Systems.DefinitionAndUsage;
    using SysML2.NET.Exceptions;
    using SysML2.NET.Extensions;

    [TestFixture]
    public class AnalysisCaseUsageExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeAnalysisCaseDefinition()
        {
            // Null subject: guard clause throws ArgumentNullException.
            Assert.That(() => ((IAnalysisCaseUsage)null).ComputeAnalysisCaseDefinition(), Throws.TypeOf<ArgumentNullException>());

            var analysisCaseUsage = new AnalysisCaseUsage();

            // Empty case: no FeatureTyping owned by the subject → null.
            Assert.That(analysisCaseUsage.ComputeAnalysisCaseDefinition(), Is.Null);

            // Discrimination: FeatureTyping targets a plain CaseDefinition, which is a superclass of
            // IAnalysisCaseDefinition and therefore NOT an IAnalysisCaseDefinition — filtered out by
            // OfType<IAnalysisCaseDefinition>() → still null.
            var caseDefinition = new CaseDefinition();
            analysisCaseUsage.AssignOwnership(new FeatureTyping { Type = caseDefinition });
            Assert.That(analysisCaseUsage.ComputeAnalysisCaseDefinition(), Is.Null);

            // Populated case: adding a FeatureTyping whose Type is an IAnalysisCaseDefinition → returned.
            // The prior non-matching CaseDefinition typing must continue to be filtered out.
            var analysisCaseDefinition = new AnalysisCaseDefinition();
            analysisCaseUsage.AssignOwnership(new FeatureTyping { Type = analysisCaseDefinition });
            Assert.That(analysisCaseUsage.ComputeAnalysisCaseDefinition(), Is.SameAs(analysisCaseDefinition));

            // [0..1] upper-bound violation: two FeatureTypings each targeting an IAnalysisCaseDefinition
            // → SingleOrDefaultStrict throws MultiplicityViolationException per the derived property's
            //   [0..1] multiplicity.
            var secondAnalysisCaseDefinition = new AnalysisCaseDefinition();
            analysisCaseUsage.AssignOwnership(new FeatureTyping { Type = secondAnalysisCaseDefinition });
            Assert.That(() => analysisCaseUsage.ComputeAnalysisCaseDefinition(), Throws.TypeOf<MultiplicityViolationException>());
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
