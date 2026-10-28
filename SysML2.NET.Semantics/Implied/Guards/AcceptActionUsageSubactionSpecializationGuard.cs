// ------------------------------------------------------------------------------------------------
// <copyright file="AcceptActionUsageSubactionSpecializationGuard.cs" company="Starion Group S.A.">
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
