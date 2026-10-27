// ------------------------------------------------------------------------------------------------
// <copyright file="AnalysisCaseDefinitionExtensionsTestFixture.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
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
