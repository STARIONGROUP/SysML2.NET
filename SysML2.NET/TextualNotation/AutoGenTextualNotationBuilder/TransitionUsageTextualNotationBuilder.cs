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
            throw new System.NotSupportedException("Assigment of enumerable not supported yet");
            stringBuilder.Append("then ");
            throw new System.NotSupportedException("Assigment of enumerable not supported yet");

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule DefaultTargetSuccession
        /// <para>DefaultTargetSuccession:TransitionUsage='else'ownedRelationship+=TransitionSuccessionMember</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.States.ITransitionUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildDefaultTargetSuccession(SysML2.NET.Core.POCO.Systems.States.ITransitionUsage poco, StringBuilder stringBuilder)
        {
            stringBuilder.Append("else ");
            throw new System.NotSupportedException("Assigment of enumerable not supported yet");

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule GuardedSuccession
        /// <para>GuardedSuccession:TransitionUsage=('succession'UsageDeclaration)?'first'ownedRelationship+=FeatureChainMemberownedRelationship+=GuardExpressionMember'then'ownedRelationship+=TransitionSuccessionMemberUsageBody</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.States.ITransitionUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildGuardedSuccession(SysML2.NET.Core.POCO.Systems.States.ITransitionUsage poco, StringBuilder stringBuilder)
        {

            stringBuilder.Append("first ");
            throw new System.NotSupportedException("Assigment of enumerable not supported yet");
            throw new System.NotSupportedException("Assigment of enumerable not supported yet");
            stringBuilder.Append("then ");
            throw new System.NotSupportedException("Assigment of enumerable not supported yet");
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
            throw new System.NotSupportedException("Assigment of enumerable not supported yet");
            throw new System.NotSupportedException("Multiple alternatives not implemented yet");
            stringBuilder.Append("then ");
            throw new System.NotSupportedException("Assigment of enumerable not supported yet");
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
            stringBuilder.Append("transition ");

            throw new System.NotSupportedException("Assigment of enumerable not supported yet");
            throw new System.NotSupportedException("Assigment of enumerable not supported yet");
            if (poco.OwnedRelationship.Count != 0)
            {
                throw new System.NotSupportedException("Assigment of enumerable not supported yet");
                throw new System.NotSupportedException("Assigment of enumerable not supported yet");
                stringBuilder.Append(' ');
            }

            if (poco.OwnedRelationship.Count != 0)
            {
                throw new System.NotSupportedException("Assigment of enumerable not supported yet");
                stringBuilder.Append(' ');
            }

            if (poco.OwnedRelationship.Count != 0)
            {
                throw new System.NotSupportedException("Assigment of enumerable not supported yet");
                stringBuilder.Append(' ');
            }

            stringBuilder.Append("then ");
            throw new System.NotSupportedException("Assigment of enumerable not supported yet");
            TypeTextualNotationBuilder.BuildActionBody(poco, stringBuilder);

        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
