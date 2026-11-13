// ------------------------------------------------------------------------------------------------
// <copyright file="SystemsUpdateModel.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SySML2.NET.REST.Tests.Helpers.Models
{
    using System;
    using System.Linq;

    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Kernel.FeatureValues;
    using SysML2.NET.Core.POCO.Systems.Actions;
    using SysML2.NET.Core.POCO.Systems.Occurrences;
    using SysML2.NET.Core.POCO.Systems.Ports;
    using SysML2.NET.Core.POCO.Systems.Requirements;
    using SysML2.NET.Core.POCO.Systems.States;
    using SysML2.NET.Core.Systems.Actions;
    using SysML2.NET.Core.Systems.Occurrences;
    using SysML2.NET.Core.Systems.Requirements;
    using SysML2.NET.Core.Systems.States;

    /// <summary>
    /// The changes of scenario step 8 to the SysML model of steps 6 and 7, after plan §6.1: U1 renames one instance of
    /// every metaclass that steps 6 and 7 created, and U2 changes the SysML attributes that a well-formed model lets
    /// change, and retargets a conjugated port typing
    /// </summary>
    /// <remarks>
    /// Each change keeps the constraints it touches: a variation is abstract and owns no feature memberships
    /// (<c>validateDefinitionVariationIsAbstract</c>, <c>…OwnedFeatureMembership</c>), an initial value needs a variable
    /// feature (<c>validateFeatureValueIsInitial</c>), the subaction kinds of a state stay unique, and an accept action
    /// as an effect is still an action (<c>validateTransitionFeatureMembershipEffectAction</c>).
    /// </remarks>
    public sealed class SystemsUpdateModel : StepModel
    {
        /// <summary>
        /// The scopes of the steps whose content this update changes
        /// </summary>
        private static readonly string[] SystemsScopes = ["systems structure/", "systems behaviour/"];

        /// <summary>
        /// Initializes a new instance of the <see cref="SystemsUpdateModel"/> class and changes the model of steps 6 and 7
        /// </summary>
        /// <param name="structure">
        /// The model of step 6
        /// </param>
        /// <param name="behavior">
        /// The model of step 7
        /// </param>
        public SystemsUpdateModel(SystemsStructureModel structure, SystemsBehaviorModel behavior) : base((behavior ?? throw new ArgumentNullException(nameof(behavior))).Builder, "systems update")
        {
            ArgumentNullException.ThrowIfNull(structure);

            this.Rename();
            this.ChangeStructure(structure);
            this.ChangeBehavior();
        }

        /// <summary>
        /// U2 on the content of step 7: a variable feature, the requirement constraint kinds, the states, a transition,
        /// and the trigger of <c>accept at</c> made a <c>when</c> trigger, the kind its Boolean argument fits
        /// </summary>
        private void ChangeBehavior()
        {
            var limit = this.Builder.Find<Feature>("systems behaviour/limit");
            limit.OwnedRelationship.OfType<FeatureValue>().Single().IsInitial = false;
            limit.IsConstant = false;
            limit.IsVariable = false;

            this.Builder.Find<RequirementConstraintMembership>("systems behaviour/R/assumed/membership").Kind = RequirementConstraintKind.Requirement;
            this.Builder.Find<RequirementConstraintMembership>("systems behaviour/R/required/membership").Kind = RequirementConstraintKind.Assumption;

            this.Builder.Find<StateDefinition>("systems behaviour/Par").IsParallel = false;
            this.Builder.Find<StateUsage>("systems behaviour/pu").IsParallel = false;
            this.Builder.Find<StateSubactionMembership>("systems behaviour/St/entry/membership").Kind = StateSubactionKind.Exit;
            this.Builder.Find<StateSubactionMembership>("systems behaviour/St/exit/membership").Kind = StateSubactionKind.Entry;
            this.Builder.Find<TransitionFeatureMembership>("systems behaviour/t/Trigger/membership").Kind = TransitionFeatureKind.Effect;

            this.Builder.Find<TriggerInvocationExpression>("systems behaviour/accept at/trigger").Kind = TriggerKind.When;
        }

        /// <summary>
        /// U2 on the content of step 6: the individual occurrences, the plain definition and usage made variations, a
        /// portion kind, and the conjugated typing of the spare port retargeted
        /// </summary>
        /// <param name="structure">
        /// The model of step 6
        /// </param>
        private void ChangeStructure(SystemsStructureModel structure)
        {
            structure.IndividualDefinition.IsIndividual = false;
            structure.IndividualUsage.IsIndividual = false;

            structure.PlainDefinition.IsVariation = true;
            structure.PlainDefinition.IsAbstract = true;
            structure.PlainUsage.IsVariation = true;
            structure.PlainUsage.IsAbstract = true;

            this.Builder.Find<OccurrenceUsage>("systems structure/ts").PortionKind = PortionKind.Snapshot;

            structure.SparePort.OwnedRelationship.OfType<ConjugatedPortTyping>().Single().ConjugatedPortDefinition = structure.OtherConjugatedPortDefinition;
        }

        /// <summary>
        /// U1: renames the first instance of every metaclass that steps 6 and 7 created
        /// </summary>
        private void Rename()
        {
            var firstInstances = this.Builder.Elements
                .Where(entry => SystemsScopes.Any(scope => entry.Name.StartsWith(scope, StringComparison.Ordinal)))
                .GroupBy(entry => entry.Element.GetType())
                .Select(group => group.First().Element);

            foreach (var element in firstInstances)
            {
                element.DeclaredName = $"{element.DeclaredName ?? element.GetType().Name} renamed";
                element.DeclaredShortName = $"{element.DeclaredShortName ?? element.GetType().Name.ToLowerInvariant()}2";
            }
        }
    }
}
