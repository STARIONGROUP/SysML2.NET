// -------------------------------------------------------------------------------------------------
// <copyright file="StepSubperformanceSpecializationGuard.cs" company="Starion Group S.A.">
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
