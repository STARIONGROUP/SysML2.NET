// ------------------------------------------------------------------------------------------------
// <copyright file="WhileLoopActionUsageExtensions.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Core.POCO.Systems.Actions
{
    using System;
    using System.Collections.Generic;

    using SysML2.NET.Decorators;

    using SysML2.NET.Core.Core.Types;
    using SysML2.NET.Core.Root.Namespaces;
    using SysML2.NET.Core.Systems.Occurrences;
    using SysML2.NET.Core.POCO.Core.Classifiers;
    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Kernel.Behaviors;
    using SysML2.NET.Core.POCO.Kernel.Classes;
    using SysML2.NET.Core.POCO.Kernel.Functions;
    using SysML2.NET.Core.POCO.Root.Annotations;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Core.POCO.Systems.Allocations;
    using SysML2.NET.Core.POCO.Systems.AnalysisCases;
    using SysML2.NET.Core.POCO.Systems.Attributes;
    using SysML2.NET.Core.POCO.Systems.Calculations;
    using SysML2.NET.Core.POCO.Systems.Cases;
    using SysML2.NET.Core.POCO.Systems.Connections;
    using SysML2.NET.Core.POCO.Systems.Constraints;
    using SysML2.NET.Core.POCO.Systems.DefinitionAndUsage;
    using SysML2.NET.Core.POCO.Systems.Enumerations;
    using SysML2.NET.Core.POCO.Systems.Flows;
    using SysML2.NET.Core.POCO.Systems.Interfaces;
    using SysML2.NET.Core.POCO.Systems.Items;
    using SysML2.NET.Core.POCO.Systems.Metadata;
    using SysML2.NET.Core.POCO.Systems.Occurrences;
    using SysML2.NET.Core.POCO.Systems.Parts;
    using SysML2.NET.Core.POCO.Systems.Ports;
    using SysML2.NET.Core.POCO.Systems.Requirements;
    using SysML2.NET.Core.POCO.Systems.States;
    using SysML2.NET.Core.POCO.Systems.UseCases;
    using SysML2.NET.Core.POCO.Systems.VerificationCases;
    using SysML2.NET.Core.POCO.Systems.Views;

    /// <summary>
    /// The <see cref="WhileLoopActionUsageExtensions"/> class provides extensions methods for
    /// the <see cref="IWhileLoopActionUsage"/> interface
    /// </summary>
    internal static class WhileLoopActionUsageExtensions
    {
        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// untilArgument =
        ///                             let parameter : Feature = inputParameter(3) in
        ///                             if parameter &lt;&gt; null and parameter.oclIsKindOf(Expression) then
        ///                             parameter.oclAsType(Expression)
        ///                             else
        ///                             null
        ///                             endif
        /// </code>
        /// </remarks>
        /// <param name="whileLoopActionUsageSubject">
        /// The subject <see cref="IWhileLoopActionUsage"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [DerivedProperty(name: nameof(IWhileLoopActionUsage.untilArgument))]
        internal static IExpression ComputeUntilArgument(this IWhileLoopActionUsage whileLoopActionUsageSubject)
        {
            return whileLoopActionUsageSubject == null
                ? throw new ArgumentNullException(nameof(whileLoopActionUsageSubject))
                : whileLoopActionUsageSubject.InputParameter(3) as IExpression;
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// whileArgument =
        ///                             let parameter : Feature = inputParameter(1) in
        ///                             if parameter &lt;&gt; null and parameter.oclIsKindOf(Expression) then
        ///                             parameter.oclAsType(Expression)
        ///                             else
        ///                             null
        ///                             endif
        /// </code>
        /// </remarks>
        /// <param name="whileLoopActionUsageSubject">
        /// The subject <see cref="IWhileLoopActionUsage"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [DerivedProperty(name: nameof(IWhileLoopActionUsage.whileArgument))]
        internal static IExpression ComputeWhileArgument(this IWhileLoopActionUsage whileLoopActionUsageSubject)
        {
            return whileLoopActionUsageSubject == null
                ? throw new ArgumentNullException(nameof(whileLoopActionUsageSubject))
                : whileLoopActionUsageSubject.InputParameter(1) as IExpression;
        }

    }
}
