// -------------------------------------------------------------------------------------------------
// <copyright file="AcceptActionUsageSubactionSpecializationGuard.cs" company="Starion Group S.A.">
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

    /// <summary>
    /// Guards checkAcceptActionUsageSubactionSpecialization: a subaction AcceptActionUsage that is not a trigger action specializes Actions::Action::acceptSubactions.
    /// </summary>
    /// <remarks>
    /// OCL: <c>isSubactionUsage() and not isTriggerAction() implies specializesFromLibrary('Actions::Action::acceptSubactions')</c>
    /// </remarks>
    public class AcceptActionUsageSubactionSpecializationGuard : IImpliedRuleGuard
    {
        /// <summary>
        /// Gets the name of the semantic constraint this guard decides.
        /// </summary>
        public string ConstraintName => "checkAcceptActionUsageSubactionSpecialization";

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
                : element is IAcceptActionUsage acceptActionUsage && acceptActionUsage.IsSubactionUsage() && !acceptActionUsage.IsTriggerAction();
        }
    }
}
