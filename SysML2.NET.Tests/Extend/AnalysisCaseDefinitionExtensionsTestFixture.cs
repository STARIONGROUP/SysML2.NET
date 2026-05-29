// -------------------------------------------------------------------------------------------------
// <copyright file="AnalysisCaseDefinitionExtensionsTestFixture.cs" company="Starion Group S.A.">
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

    using SysML2.NET.Core.POCO.Kernel.Functions;
    using SysML2.NET.Core.POCO.Systems.AnalysisCases;
    using SysML2.NET.Extensions;

    [TestFixture]
    public class AnalysisCaseDefinitionExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeResultExpression()
        {
            Assert.That(() => ((IAnalysisCaseDefinition)null).ComputeResultExpression(), Throws.TypeOf<ArgumentNullException>());

            var analysisCaseDefinition = new AnalysisCaseDefinition();

            // Empty case: no ResultExpressionMembership in featureMembership → null.
            Assert.That(analysisCaseDefinition.ComputeResultExpression(), Is.Null);

            // Populated case: ResultExpressionMembership owns an Expression → returns the Expression.
            var expression = new Expression();
            var resultExpressionMembership = new ResultExpressionMembership();
            analysisCaseDefinition.AssignOwnership(resultExpressionMembership, expression);

            Assert.That(analysisCaseDefinition.ComputeResultExpression(), Is.SameAs(expression));
        }
    }
}
