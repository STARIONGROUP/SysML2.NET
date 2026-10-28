// ------------------------------------------------------------------------------------------------
// <copyright file="StepSubperformanceSpecializationGuard.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Semantics.Implied.Guards
{
    using System;

    using SysML2.NET.Core.POCO.Kernel.Behaviors;
    using SysML2.NET.Core.POCO.Root.Elements;

    /// <summary>
    /// Guards checkStepSubperformanceSpecialization: a composite Step owned by a Behavior or Step specializes Performances::Performance::subperformance.
    /// </summary>
    /// <remarks>
    /// OCL: <c>owningType &lt;&gt; null and (owningType.oclIsKindOf(Behavior) or owningType.oclIsKindOf(Step)) and self.isComposite implies specializesFromLibrary('Performances::Performance::subperformance')</c>
    /// </remarks>
    public class StepSubperformanceSpecializationGuard : IImpliedRuleGuard
    {
        /// <summary>
        /// Gets the name of the semantic constraint this guard decides.
        /// </summary>
        public string ConstraintName => "checkStepSubperformanceSpecialization";

        /// <summary>
        /// Asserts whether the constraint applies to the supplied Element.
        /// </summary>
        /// <param name="element">The Element under evaluation.</param>
        /// <returns>True when the constraint applies.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="element" /> is null.</exception>
        public bool Applies(IElement element)
        {
            return element == null
                ? throw new ArgumentNullException(nameof(element))
                : element is IStep { IsComposite: true, owningType: IBehavior or IStep };
        }
    }
}
