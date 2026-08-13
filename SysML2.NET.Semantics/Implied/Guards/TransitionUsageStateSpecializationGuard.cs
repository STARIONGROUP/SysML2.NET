// -------------------------------------------------------------------------------------------------
// <copyright file="TransitionUsageStateSpecializationGuard.cs" company="Starion Group S.A.">
//
//    Copyright (C) 2022-2026 Starion Group S.A.
//
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
//
//         http://www.apache.org/licenses/LICENSE-2.0
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
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
    /// Guards checkTransitionUsageStateSpecialization: a composite TransitionUsage owned by a state, whose source is a StateUsage, specializes States::StateAction::stateTransitions.
    /// </summary>
    /// <remarks>
    /// OCL: <c>isComposite and owningType &lt;&gt; null and (owningType.oclIsKindOf(StateDefinition) or owningType.oclIsKindOf(StateUsage)) and source &lt;&gt; null and source.oclIsKindOf(StateUsage)</c>
    /// <para>Hand written because of the trailing <c>source</c> conjunct, which is outside the generator's
    /// translatable shapes. It is what separates this constraint from its action counterpart.</para>
    /// </remarks>
    public class TransitionUsageStateSpecializationGuard : IImpliedRuleGuard
    {
        /// <summary>
        /// Gets the name of the semantic constraint this guard decides.
        /// </summary>
        public string ConstraintName => "checkTransitionUsageStateSpecialization";

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
                : element is ITransitionUsage { IsComposite: true, owningType: IStateDefinition or IStateUsage, source: IStateUsage };
        }
    }
}
