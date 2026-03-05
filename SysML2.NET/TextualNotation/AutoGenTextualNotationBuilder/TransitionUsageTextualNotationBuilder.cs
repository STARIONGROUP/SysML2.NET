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

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.TextualNotation
{
    using System.Linq;
    using System.Text;

    using SysML2.NET.Core.POCO.Root.Elements;

    /// <summary>
    /// The <see cref="TransitionUsageTextualNotationBuilder" /> provides Textual Notation Builder for the <see cref="SysML2.NET.Core.POCO.Systems.States.ITransitionUsage" /> element
    /// </summary>
    public static partial class TransitionUsageTextualNotationBuilder
    {
        /// <summary>
        /// Builds the Textual Notation string for the rule GuardedTargetSuccession
        /// <para>GuardedTargetSuccession:TransitionUsage=ownedRelationship+=GuardExpressionMember'then'ownedRelationship+=TransitionSuccessionMember</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.States.ITransitionUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildGuardedTargetSuccession(SysML2.NET.Core.POCO.Systems.States.ITransitionUsage poco, StringBuilder stringBuilder)
        {
            using var ownedRelationshipOfTransitionFeatureMembershipIterator = poco.OwnedRelationship.OfType<SysML2.NET.Core.POCO.Systems.States.TransitionFeatureMembership>().GetEnumerator();
            using var ownedRelationshipOfOwningMembershipIterator = poco.OwnedRelationship.OfType<SysML2.NET.Core.POCO.Root.Namespaces.OwningMembership>().GetEnumerator();
            ownedRelationshipOfTransitionFeatureMembershipIterator.MoveNext();

            if (ownedRelationshipOfTransitionFeatureMembershipIterator.Current != null)
            {
                TransitionFeatureMembershipTextualNotationBuilder.BuildGuardExpressionMember(ownedRelationshipOfTransitionFeatureMembershipIterator.Current, stringBuilder);
            }
            stringBuilder.Append("then ");
            ownedRelationshipOfOwningMembershipIterator.MoveNext();

            if (ownedRelationshipOfOwningMembershipIterator.Current != null)
            {
                OwningMembershipTextualNotationBuilder.BuildTransitionSuccessionMember(ownedRelationshipOfOwningMembershipIterator.Current, stringBuilder);
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule DefaultTargetSuccession
        /// <para>DefaultTargetSuccession:TransitionUsage='else'ownedRelationship+=TransitionSuccessionMember</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.States.ITransitionUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildDefaultTargetSuccession(SysML2.NET.Core.POCO.Systems.States.ITransitionUsage poco, StringBuilder stringBuilder)
        {
            using var ownedRelationshipOfOwningMembershipIterator = poco.OwnedRelationship.OfType<SysML2.NET.Core.POCO.Root.Namespaces.OwningMembership>().GetEnumerator();
            stringBuilder.Append("else ");
            ownedRelationshipOfOwningMembershipIterator.MoveNext();

            if (ownedRelationshipOfOwningMembershipIterator.Current != null)
            {
                OwningMembershipTextualNotationBuilder.BuildTransitionSuccessionMember(ownedRelationshipOfOwningMembershipIterator.Current, stringBuilder);
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule GuardedSuccession
        /// <para>GuardedSuccession:TransitionUsage=('succession'UsageDeclaration)?'first'ownedRelationship+=FeatureChainMemberownedRelationship+=GuardExpressionMember'then'ownedRelationship+=TransitionSuccessionMemberUsageBody</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.States.ITransitionUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildGuardedSuccession(SysML2.NET.Core.POCO.Systems.States.ITransitionUsage poco, StringBuilder stringBuilder)
        {
            using var ownedRelationshipOfMembershipIterator = poco.OwnedRelationship.OfType<SysML2.NET.Core.POCO.Root.Namespaces.Membership>().GetEnumerator();
            using var ownedRelationshipOfTransitionFeatureMembershipIterator = poco.OwnedRelationship.OfType<SysML2.NET.Core.POCO.Systems.States.TransitionFeatureMembership>().GetEnumerator();
            using var ownedRelationshipOfOwningMembershipIterator = poco.OwnedRelationship.OfType<SysML2.NET.Core.POCO.Root.Namespaces.OwningMembership>().GetEnumerator();

            if (BuildGroupConditionForGuardedSuccession(poco))
            {
                stringBuilder.Append("succession ");
                UsageTextualNotationBuilder.BuildUsageDeclaration(poco, stringBuilder);
                stringBuilder.Append(' ');
            }

            stringBuilder.Append("first ");
            ownedRelationshipOfMembershipIterator.MoveNext();

            if (ownedRelationshipOfMembershipIterator.Current != null)
            {
                MembershipTextualNotationBuilder.BuildFeatureChainMember(ownedRelationshipOfMembershipIterator.Current, stringBuilder);
            }
            ownedRelationshipOfTransitionFeatureMembershipIterator.MoveNext();

            if (ownedRelationshipOfTransitionFeatureMembershipIterator.Current != null)
            {
                TransitionFeatureMembershipTextualNotationBuilder.BuildGuardExpressionMember(ownedRelationshipOfTransitionFeatureMembershipIterator.Current, stringBuilder);
            }
            stringBuilder.Append("then ");
            ownedRelationshipOfOwningMembershipIterator.MoveNext();

            if (ownedRelationshipOfOwningMembershipIterator.Current != null)
            {
                OwningMembershipTextualNotationBuilder.BuildTransitionSuccessionMember(ownedRelationshipOfOwningMembershipIterator.Current, stringBuilder);
            }
            UsageTextualNotationBuilder.BuildUsageBody(poco, stringBuilder);

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule TargetTransitionUsage
        /// <para>TargetTransitionUsage:TransitionUsage=ownedRelationship+=EmptyParameterMember('transition'(ownedRelationship+=EmptyParameterMemberownedRelationship+=TriggerActionMember)?(ownedRelationship+=GuardExpressionMember)?(ownedRelationship+=EffectBehaviorMember)?|ownedRelationship+=EmptyParameterMemberownedRelationship+=TriggerActionMember(ownedRelationship+=GuardExpressionMember)?(ownedRelationship+=EffectBehaviorMember)?|ownedRelationship+=GuardExpressionMember(ownedRelationship+=EffectBehaviorMember)?)?'then'ownedRelationship+=TransitionSuccessionMemberActionBody</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.States.ITransitionUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildTargetTransitionUsage(SysML2.NET.Core.POCO.Systems.States.ITransitionUsage poco, StringBuilder stringBuilder)
        {
            using var ownedRelationshipOfParameterMembershipIterator = poco.OwnedRelationship.OfType<SysML2.NET.Core.POCO.Kernel.Behaviors.ParameterMembership>().GetEnumerator();
            using var ownedRelationshipOfTransitionFeatureMembershipIterator = poco.OwnedRelationship.OfType<SysML2.NET.Core.POCO.Systems.States.TransitionFeatureMembership>().GetEnumerator();
            using var ownedRelationshipOfOwningMembershipIterator = poco.OwnedRelationship.OfType<SysML2.NET.Core.POCO.Root.Namespaces.OwningMembership>().GetEnumerator();
            ownedRelationshipOfParameterMembershipIterator.MoveNext();

            if (ownedRelationshipOfParameterMembershipIterator.Current != null)
            {
                ParameterMembershipTextualNotationBuilder.BuildEmptyParameterMember(ownedRelationshipOfParameterMembershipIterator.Current, stringBuilder);
            }
            throw new System.NotSupportedException("Multiple alternatives not implemented yet");
            stringBuilder.Append("then ");
            ownedRelationshipOfOwningMembershipIterator.MoveNext();

            if (ownedRelationshipOfOwningMembershipIterator.Current != null)
            {
                OwningMembershipTextualNotationBuilder.BuildTransitionSuccessionMember(ownedRelationshipOfOwningMembershipIterator.Current, stringBuilder);
            }
            TypeTextualNotationBuilder.BuildActionBody(poco, stringBuilder);

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule TransitionUsage
        /// <para>TransitionUsage='transition'(UsageDeclaration'first')?ownedRelationship+=FeatureChainMemberownedRelationship+=EmptyParameterMember(ownedRelationship+=EmptyParameterMemberownedRelationship+=TriggerActionMember)?(ownedRelationship+=GuardExpressionMember)?(ownedRelationship+=EffectBehaviorMember)?'then'ownedRelationship+=TransitionSuccessionMemberActionBody</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.States.ITransitionUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildTransitionUsage(SysML2.NET.Core.POCO.Systems.States.ITransitionUsage poco, StringBuilder stringBuilder)
        {
            using var ownedRelationshipOfMembershipIterator = poco.OwnedRelationship.OfType<SysML2.NET.Core.POCO.Root.Namespaces.Membership>().GetEnumerator();
            using var ownedRelationshipOfParameterMembershipIterator = poco.OwnedRelationship.OfType<SysML2.NET.Core.POCO.Kernel.Behaviors.ParameterMembership>().GetEnumerator();
            using var ownedRelationshipOfTransitionFeatureMembershipIterator = poco.OwnedRelationship.OfType<SysML2.NET.Core.POCO.Systems.States.TransitionFeatureMembership>().GetEnumerator();
            using var ownedRelationshipOfOwningMembershipIterator = poco.OwnedRelationship.OfType<SysML2.NET.Core.POCO.Root.Namespaces.OwningMembership>().GetEnumerator();
            stringBuilder.Append("transition ");

            if (BuildGroupConditionForTransitionUsage(poco))
            {
                UsageTextualNotationBuilder.BuildUsageDeclaration(poco, stringBuilder);
                stringBuilder.Append("first ");
                stringBuilder.Append(' ');
            }

            ownedRelationshipOfMembershipIterator.MoveNext();

            if (ownedRelationshipOfMembershipIterator.Current != null)
            {
                MembershipTextualNotationBuilder.BuildFeatureChainMember(ownedRelationshipOfMembershipIterator.Current, stringBuilder);
            }
            ownedRelationshipOfParameterMembershipIterator.MoveNext();

            if (ownedRelationshipOfParameterMembershipIterator.Current != null)
            {
                ParameterMembershipTextualNotationBuilder.BuildEmptyParameterMember(ownedRelationshipOfParameterMembershipIterator.Current, stringBuilder);
            }

            if (ownedRelationshipOfParameterMembershipIterator.MoveNext())
            {

                if (ownedRelationshipOfParameterMembershipIterator.Current != null)
                {
                    ParameterMembershipTextualNotationBuilder.BuildEmptyParameterMember(ownedRelationshipOfParameterMembershipIterator.Current, stringBuilder);
                }

                if (ownedRelationshipOfTransitionFeatureMembershipIterator.Current != null)
                {
                    TransitionFeatureMembershipTextualNotationBuilder.BuildTriggerActionMember(ownedRelationshipOfTransitionFeatureMembershipIterator.Current, stringBuilder);
                }
                stringBuilder.Append(' ');
            }


            if (ownedRelationshipOfTransitionFeatureMembershipIterator.MoveNext())
            {

                if (ownedRelationshipOfTransitionFeatureMembershipIterator.Current != null)
                {
                    TransitionFeatureMembershipTextualNotationBuilder.BuildGuardExpressionMember(ownedRelationshipOfTransitionFeatureMembershipIterator.Current, stringBuilder);
                }
                stringBuilder.Append(' ');
            }


            if (ownedRelationshipOfTransitionFeatureMembershipIterator.MoveNext())
            {

                if (ownedRelationshipOfTransitionFeatureMembershipIterator.Current != null)
                {
                    TransitionFeatureMembershipTextualNotationBuilder.BuildEffectBehaviorMember(ownedRelationshipOfTransitionFeatureMembershipIterator.Current, stringBuilder);
                }
                stringBuilder.Append(' ');
            }

            stringBuilder.Append("then ");
            ownedRelationshipOfOwningMembershipIterator.MoveNext();

            if (ownedRelationshipOfOwningMembershipIterator.Current != null)
            {
                OwningMembershipTextualNotationBuilder.BuildTransitionSuccessionMember(ownedRelationshipOfOwningMembershipIterator.Current, stringBuilder);
            }
            TypeTextualNotationBuilder.BuildActionBody(poco, stringBuilder);

        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
