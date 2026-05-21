// -------------------------------------------------------------------------------------------------
// <copyright file="FeatureMembershipTextualNotationBuilder.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Serializer.TextualNotation.Writers
{
    using System.Text;

    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;

    /// <summary>
    /// Hand-coded part of the <see cref="FeatureMembershipTextualNotationBuilder"/>
    /// </summary>
    public static partial  class FeatureMembershipTextualNotationBuilder
    {
        /// <summary>
        /// Build the memberFeature=[QualifiedName] of the rule
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IFeatureMembership" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        private static void BuildMemberFeature(IFeatureMembership poco, TextualNotationWriterContext writerContext, StringBuilder stringBuilder)
        {
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule InitialNodeMember.
        /// <remarks>InitialNodeMember:FeatureMembership=MemberPrefix'first'memberFeature=[QualifiedName]RelationshipBody</remarks>
        /// <para>The grammar's <c>memberFeature</c> property does not resolve against
        /// <see cref="IFeatureMembership"/> — the metamodel only exposes
        /// <c>ownedMemberFeature</c> on <c>IFeatureMembership</c> and <c>MemberElement</c>
        /// on its parent <c>IMembership</c>. This hand-coded sibling carries the
        /// remaining <c>[QualifiedName]</c> emission as a stub; the surrounding
        /// <c>MemberPrefix</c>, <c>'first'</c>, and <c>RelationshipBody</c> tokens are
        /// already emitted by the autogen wrapper.</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IFeatureMembership" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        private static void BuildInitialNodeMemberHandCoded(IFeatureMembership poco, TextualNotationWriterContext writerContext, StringBuilder stringBuilder)
        {
            // Preserves the previous empty-stub behavior of BuildMemberFeature for this
            // call site. The full QualifiedName-emission still requires a dedicated
            // implementation; left as a follow-up.
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule OwnedExpressionMember.
        /// <remarks>OwnedExpressionMember:FeatureMembership=ownedFeatureMember=OwnedExpression</remarks>
        /// <para>The grammar property name <c>ownedFeatureMember</c> does not exist on
        /// <see cref="IFeatureMembership"/> — the OMG kebnf carries a one-off typo and
        /// the real metamodel property is <c>ownedMemberFeature</c>. This hand-coded
        /// sibling resolves the typo at emission time, dispatching the membership's
        /// <c>OwnedMemberFeature</c> through <c>BuildOwnedExpression</c>.</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IFeatureMembership" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        private static void BuildOwnedExpressionMemberHandCoded(IFeatureMembership poco, TextualNotationWriterContext writerContext, StringBuilder stringBuilder)
        {
            if (poco.ownedMemberFeature is SysML2.NET.Core.POCO.Kernel.Functions.IExpression elementAsExpression)
            {
                ExpressionTextualNotationBuilder.BuildOwnedExpression(elementAsExpression, writerContext, stringBuilder);
            }
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule EntryTransitionMember
        /// <remarks>EntryTransitionMember:FeatureMembership=MemberPrefix(ownedRelatedElement+=GuardedTargetSuccession|'then'ownedRelatedElement+=TargetSuccession)';'</remarks>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IFeatureMembership" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        private static void BuildEntryTransitionMemberHandCoded(IFeatureMembership poco, TextualNotationWriterContext writerContext, StringBuilder stringBuilder)
        {
            // Dispatch on cursor element type:
            //   - ITransitionUsage → GuardedTargetSuccession (no 'then' keyword)
            //   - ISuccessionAsUsage → TargetSuccession (emit 'then' first)
            var ownedRelatedElementCursor = writerContext.CursorCache.GetOrCreateCursor(poco.Id, "ownedRelatedElement", poco.OwnedRelatedElement);

            if (ownedRelatedElementCursor.Current is SysML2.NET.Core.POCO.Systems.States.ITransitionUsage guardedTarget)
            {
                TransitionUsageTextualNotationBuilder.BuildGuardedTargetSuccession(guardedTarget, writerContext, stringBuilder);
                ownedRelatedElementCursor.Move();
            }
            else if (ownedRelatedElementCursor.Current is SysML2.NET.Core.POCO.Systems.Connections.ISuccessionAsUsage targetSuccession)
            {
                stringBuilder.Append("then ");
                SuccessionAsUsageTextualNotationBuilder.BuildTargetSuccession(targetSuccession, writerContext, stringBuilder);
                ownedRelatedElementCursor.Move();
            }
        }

    }
}
