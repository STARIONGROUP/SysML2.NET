// ------------------------------------------------------------------------------------------------
// <copyright file="TransitionUsageActionSpecializationGuard.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Semantics.Implied.Guards
{
    using System;

    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Systems.Actions;
    using SysML2.NET.Core.POCO.Systems.States;

    /// <summary>
    /// Guards checkTransitionUsageActionSpecialization: a composite TransitionUsage owned by an action, whose source is not a StateUsage, specializes Actions::Action::decisionTransitions.
    /// </summary>
    /// <remarks>
    /// OCL: <c>isComposite and owningType &lt;&gt; null and (owningType.oclIsKindOf(ActionDefinition) or owningType.oclIsKindOf(ActionUsage)) and source &lt;&gt; null and not source.oclIsKindOf(StateUsage)</c>
    /// <para>Hand written because of the trailing <c>source</c> conjunct, which is outside the generator's
    /// translatable shapes. It is what separates this constraint from its state counterpart.</para>
    /// </remarks>
    public class TransitionUsageActionSpecializationGuard : IImpliedRuleGuard
    {
        /// <summary>
        /// Gets the name of the semantic constraint this guard decides.
        /// </summary>
        public string ConstraintName => "checkTransitionUsageActionSpecialization";

        /// <summary>
        /// Asserts whether the constraint applies to the supplied Element.
        /// </summary>
        /// <param name="element">The Element under evaluation.</param>
        /// <returns>True when every conjunct of the constraint holds.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="element" /> is null.</exception>
        public bool Applies(IElement element)
        {
            return element == null
                ? throw new ArgumentNullException(nameof(element))
                : element is ITransitionUsage
                  {
                      IsComposite: true,
                      owningType: IActionDefinition or IActionUsage,
                      source: not null and not IStateUsage
                  };
        }
    }
}
