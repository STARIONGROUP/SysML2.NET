// -------------------------------------------------------------------------------------------------
// <copyright file="TransitionFeatureMembershipTextualNotationBuilder.cs" company="Starion Group S.A.">
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
    /// The <see cref="TransitionFeatureMembershipTextualNotationBuilder" /> provides Textual Notation Builder for the <see cref="SysML2.NET.Core.POCO.Systems.States.ITransitionFeatureMembership" /> element
    /// </summary>
    public static partial class TransitionFeatureMembershipTextualNotationBuilder
    {
        /// <summary>
        /// Builds the Textual Notation string for the rule TriggerActionMember
        /// <para>TriggerActionMember:TransitionFeatureMembership='accept'{kind='trigger'}ownedRelatedElement+=TriggerAction</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.States.ITransitionFeatureMembership" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildTriggerActionMember(SysML2.NET.Core.POCO.Systems.States.ITransitionFeatureMembership poco, TextualNotationWriterContext writerContext, StringBuilder stringBuilder)
        {
            var ownedRelatedElementCursor = writerContext.CursorCache.GetOrCreateCursor(poco.Id, "ownedRelatedElement", poco.OwnedRelatedElement);
            stringBuilder.Append("accept ");
            // NonParsing Assignment Element : kind = 'trigger' => Does not have to be process

            if (ownedRelatedElementCursor.Current != null)
            {

                if (ownedRelatedElementCursor.Current is SysML2.NET.Core.POCO.Systems.Actions.IAcceptActionUsage elementAsAcceptActionUsage)
                {
                    AcceptActionUsageTextualNotationBuilder.BuildTriggerAction(elementAsAcceptActionUsage, writerContext, stringBuilder);
                }
            }
            ownedRelatedElementCursor.Move();


        }

        /// <summary>
        /// Builds the Textual Notation string for the rule GuardExpressionMember
        /// <para>GuardExpressionMember:TransitionFeatureMembership='if'{kind='guard'}ownedRelatedElement+=OwnedExpression</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.States.ITransitionFeatureMembership" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildGuardExpressionMember(SysML2.NET.Core.POCO.Systems.States.ITransitionFeatureMembership poco, TextualNotationWriterContext writerContext, StringBuilder stringBuilder)
        {
            var ownedRelatedElementCursor = writerContext.CursorCache.GetOrCreateCursor(poco.Id, "ownedRelatedElement", poco.OwnedRelatedElement);
            stringBuilder.Append("if ");
            // NonParsing Assignment Element : kind = 'guard' => Does not have to be process

            if (ownedRelatedElementCursor.Current != null)
            {

                if (ownedRelatedElementCursor.Current is SysML2.NET.Core.POCO.Kernel.Functions.IExpression elementAsExpression)
                {
                    ExpressionTextualNotationBuilder.BuildOwnedExpression(elementAsExpression, writerContext, stringBuilder);
                }
            }
            ownedRelatedElementCursor.Move();


        }

        /// <summary>
        /// Builds the Textual Notation string for the rule EffectBehaviorMember
        /// <para>EffectBehaviorMember:TransitionFeatureMembership='do'{kind='effect'}ownedRelatedElement+=EffectBehaviorUsage</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.States.ITransitionFeatureMembership" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildEffectBehaviorMember(SysML2.NET.Core.POCO.Systems.States.ITransitionFeatureMembership poco, TextualNotationWriterContext writerContext, StringBuilder stringBuilder)
        {
            var ownedRelatedElementCursor = writerContext.CursorCache.GetOrCreateCursor(poco.Id, "ownedRelatedElement", poco.OwnedRelatedElement);
            stringBuilder.Append("do ");
            // NonParsing Assignment Element : kind = 'effect' => Does not have to be process

            if (ownedRelatedElementCursor.Current != null)
            {

                if (ownedRelatedElementCursor.Current is SysML2.NET.Core.POCO.Systems.Actions.IActionUsage elementAsActionUsage)
                {
                    ActionUsageTextualNotationBuilder.BuildEffectBehaviorUsage(elementAsActionUsage, writerContext, stringBuilder);
                }
            }
            ownedRelatedElementCursor.Move();


        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
