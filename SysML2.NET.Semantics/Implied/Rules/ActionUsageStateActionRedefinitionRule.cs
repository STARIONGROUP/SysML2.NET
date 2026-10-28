// ------------------------------------------------------------------------------------------------
// <copyright file="ActionUsageStateActionRedefinitionRule.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Semantics.Implied.Rules
{
    using SysML2.NET.Core.Systems.States;
    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Systems.Actions;
    using SysML2.NET.Core.POCO.Systems.States;

    /// <summary>
    /// Implements checkActionUsageStateActionRedefinition: an ActionUsage owned as a state subaction
    /// redefines the entry, do or exit action of States::StateAction, according to its kind.
    /// </summary>
    /// <remarks>
    /// OCL: <c>owningFeatureMembership &lt;&gt; null and
    /// owningFeatureMembership.oclIsKindOf(StateSubactionMembership) implies … if kind = entry then
    /// redefinesFromLibrary('States::StateAction::entryAction') else if kind = do then … else …
    /// exitAction</c>. The kind selects the target, so all three branches are covered here rather than
    /// split across three rules.
    /// </remarks>
    public class ActionUsageStateActionRedefinitionRule : LibraryRedefinitionRule
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ActionUsageStateActionRedefinitionRule" /> class.
        /// </summary>
        /// <param name="libraryTypeIndex">The index resolving the library Feature by qualified name.</param>
        /// <param name="factory">The factory creating the detached Redefinition.</param>
        public ActionUsageStateActionRedefinitionRule(ILibraryTypeIndex libraryTypeIndex, IImpliedRelationshipFactory factory)
            : base(libraryTypeIndex, factory)
        {
        }

        /// <summary>
        /// Gets the name of the semantic constraint this rule implements.
        /// </summary>
        public override string ConstraintName => "checkActionUsageStateActionRedefinition";

        /// <summary>
        /// Returns the ActionUsage together with the library action its subaction kind selects.
        /// </summary>
        /// <param name="element">The Element under evaluation.</param>
        /// <returns>The Element and the library qualified name, or <c>null</c> when it is not a state subaction.</returns>
        protected override (IFeature RedefiningFeature, string LibraryQualifiedName)? QueryRedefinition(IElement element)
        {
            if (element is not IActionUsage { owningFeatureMembership: IStateSubactionMembership membership } actionUsage)
            {
                return null;
            }

            return (actionUsage, membership.Kind switch
            {
                StateSubactionKind.Entry => "States::StateAction::entryAction",
                StateSubactionKind.Do => "States::StateAction::doAction",
                _ => "States::StateAction::exitAction"
            });
        }
    }
}
