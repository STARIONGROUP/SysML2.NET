// ------------------------------------------------------------------------------------------------
// <copyright file="IsolatedAssertions.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SySML2.NET.REST.Tests.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using NUnit.Framework;
    using NUnit.Framework.Interfaces;
    using NUnit.Framework.Internal;

    /// <summary>
    /// Runs a check in an isolated test context, so that the assertions it makes are collected instead of deciding
    /// the outcome of the test that runs it
    /// </summary>
    public static class IsolatedAssertions
    {
        /// <summary>
        /// Runs a check and collects the failures and warnings it recorded
        /// </summary>
        /// <param name="check">
        /// The check to run
        /// </param>
        /// <returns>
        /// The failed and warning assertion results, in the order they were recorded
        /// </returns>
        public static async Task<IReadOnlyList<AssertionResult>> RunAsync(Func<Task> check)
        {
            using (new TestExecutionContext.IsolatedContext())
            {
                try
                {
                    await check();
                }
                catch (AssertionException)
                {
                }
                catch (MultipleAssertException)
                {
                }

                return TestExecutionContext.CurrentContext.CurrentResult.AssertionResults
                    .Where(result => result.Status is AssertionStatus.Failed or AssertionStatus.Warning)
                    .ToList();
            }
        }
    }
}
