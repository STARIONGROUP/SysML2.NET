// -------------------------------------------------------------------------------------------------
// <copyright file="TransitionUsageTextualNotationBuilder.cs" company="Starion Group S.A.">
// 
//   Copyright 2022-2026 Starion Group S.A.
// 
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
// 
//        http://www.apache.org/licenses/LICENSE-2.0
// 
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
// 
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.TextualNotation
{
    using System.Text;

    using SysML2.NET.Core.POCO.Kernel.Behaviors;
    using SysML2.NET.Core.POCO.Systems.States;

    /// <summary>
    /// Hand-coded part of the <see cref="TransitionUsageTextualNotationBuilder" />
    /// </summary>
    public static partial class TransitionUsageTextualNotationBuilder
    {
        /// <summary>
        /// Builds the Textual Notation string for the rule TargetTransitionUsage
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.States.ITransitionUsage" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        /// <remarks>
        /// TargetTransitionUsage : TransitionUsage =
        ///     ownedRelationship += EmptyParameterMember         — emitted by auto-gen BEFORE this
        ///     ( 'transition'
        ///       ( ownedRelationship += EmptyParameterMember
        ///         ownedRelationship += TriggerActionMember )?
        ///       ( ownedRelationship += GuardExpressionMember )?
        ///       ( ownedRelationship += EffectBehaviorMember )?
        ///     | ownedRelationship += EmptyParameterMember
        ///       ownedRelationship += TriggerActionMember
        ///       ( ownedRelationship += GuardExpressionMember )?
        ///       ( ownedRelationship += EffectBehaviorMember )?
        ///     | ownedRelationship += GuardExpressionMember
        ///       ( ownedRelationship += EffectBehaviorMember )?
        ///     )?
        ///     'then' ...                                        — emitted by auto-gen AFTER this
        ///
        /// Auto-gen handles: first EmptyParameterMember + Move() before, and 'then' +
        /// TransitionSuccessionMember + ActionBody after. This method handles only the
        /// optional middle section.
        /// </remarks>
        private static void BuildTargetTransitionUsageHandCoded(ITransitionUsage poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);

            if (ownedRelationshipCursor.Current is ITransitionFeatureMembership { Kind: SysML2.NET.Core.Systems.States.TransitionFeatureKind.Guard } guardMember)
            {
                // Alt 3: GuardExpressionMember (EffectBehaviorMember)?
                TransitionFeatureMembershipTextualNotationBuilder.BuildGuardExpressionMember(guardMember, cursorCache, stringBuilder);
                ownedRelationshipCursor.Move();

                if (ownedRelationshipCursor.Current is ITransitionFeatureMembership { Kind: SysML2.NET.Core.Systems.States.TransitionFeatureKind.Effect } effectMember)
                {
                    TransitionFeatureMembershipTextualNotationBuilder.BuildEffectBehaviorMember(effectMember, cursorCache, stringBuilder);
                    ownedRelationshipCursor.Move();
                }
            }
            else if (ownedRelationshipCursor.Current is IParameterMembership emptyParam)
            {
                // Discriminate Alt 1 vs Alt 2: peek after the EmptyParameterMember
                // Alt 2 mandates a TriggerActionMember immediately after EmptyParameterMember.
                // Alt 1 makes the trigger optional — if no trigger follows, it must be Alt 1
                // (which requires the 'transition' keyword).
                var nextAfterEmpty = ownedRelationshipCursor.GetNext(1);
                var hasTrigger = nextAfterEmpty is ITransitionFeatureMembership { Kind: SysML2.NET.Core.Systems.States.TransitionFeatureKind.Trigger };

                if (!hasTrigger)
                {
                    // Alt 1: 'transition' (EmptyParameterMember TriggerActionMember)? GuardExpressionMember? EffectBehaviorMember?
                    // No trigger follows → the optional sub-group (EmptyParam TriggerAction)? is absent,
                    // but the 'transition' keyword is required.
                    stringBuilder.Append("transition ");
                }

                // Both Alt 1 and Alt 2: consume EmptyParameterMember
                ParameterMembershipTextualNotationBuilder.BuildEmptyParameterMember(emptyParam, cursorCache, stringBuilder);
                ownedRelationshipCursor.Move();

                // Consume TriggerActionMember if present (mandatory in Alt 2, optional in Alt 1)
                if (ownedRelationshipCursor.Current is ITransitionFeatureMembership { Kind: SysML2.NET.Core.Systems.States.TransitionFeatureKind.Trigger } triggerMember)
                {
                    TransitionFeatureMembershipTextualNotationBuilder.BuildTriggerActionMember(triggerMember, cursorCache, stringBuilder);
                    ownedRelationshipCursor.Move();
                }

                if (ownedRelationshipCursor.Current is ITransitionFeatureMembership { Kind: SysML2.NET.Core.Systems.States.TransitionFeatureKind.Guard } guardAfterTrigger)
                {
                    TransitionFeatureMembershipTextualNotationBuilder.BuildGuardExpressionMember(guardAfterTrigger, cursorCache, stringBuilder);
                    ownedRelationshipCursor.Move();
                }

                if (ownedRelationshipCursor.Current is ITransitionFeatureMembership { Kind: SysML2.NET.Core.Systems.States.TransitionFeatureKind.Effect } effectAfterGuard)
                {
                    TransitionFeatureMembershipTextualNotationBuilder.BuildEffectBehaviorMember(effectAfterGuard, cursorCache, stringBuilder);
                    ownedRelationshipCursor.Move();
                }
            }
            // Otherwise: the entire middle section is optional — do nothing
        }
    }
}
