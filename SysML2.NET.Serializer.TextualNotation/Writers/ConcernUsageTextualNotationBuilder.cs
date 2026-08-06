// -------------------------------------------------------------------------------------------------
// <copyright file="ConcernUsageTextualNotationBuilder.cs" company="Starion Group S.A.">
// 
//    Copyright (C) 2022-2026 Starion Group S.A.
// 
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
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
    using System.Linq;

    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Core.POCO.Systems.Metadata;
    using SysML2.NET.Core.POCO.Systems.Requirements;

    /// <summary>
    /// Hand-coded part of the <see cref="ConcernUsageTextualNotationBuilder" />
    /// </summary>
    public static partial class ConcernUsageTextualNotationBuilder
    {
        /// <summary>
        /// Builds the Textual Notation string for the <c>FramedConcernUsage</c> rule.
        /// <para><c>FramedConcernUsage : ConcernUsage =
        /// ownedRelationship += OwnedReferenceSubsetting FeatureSpecializationPart? RequirementBody
        /// | ( UsageExtensionKeyword* 'concern' | UsageExtensionKeyword+ )
        /// ConstraintUsageDeclaration RequirementBody</c></para>
        /// <para>Alt 1 consumes one <see cref="IReferenceSubsetting"/>; if any further
        /// <see cref="ISpecialization"/> follows the cursor, the optional <c>FeatureSpecializationPart?</c>
        /// is emitted via a single call to <c>BuildFeatureSpecialization</c>. Alt 2 consumes a run
        /// of <c>UsageExtensionKeyword</c>, then the <c>'concern'</c> keyword, then
        /// <c>ConstraintUsageDeclaration</c>.</para>
        /// <para>BOTH alternatives close with <c>RequirementBody</c>, so the body is emitted once
        /// after the alternation — unlike the sibling <c>RequirementConstraintUsage</c>, whose
        /// alternatives end in <c>RequirementBody</c> and <c>CalculationBody</c> respectively and
        /// therefore emit their own. This matches the generated sibling <c>BuildConcernUsage</c>
        /// (<c>ConcernUsage = OccurrenceUsagePrefix 'concern' ConstraintUsageDeclaration
        /// RequirementBody</c>).</para>
        /// </summary>
        /// <param name="poco">The <see cref="IConcernUsage"/> being serialised</param>
        /// <param name="writerContext">The <see cref="ICursorCache"/> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="IndentedStringBuilder"/> that contains the entire textual notation</param>
        private static void BuildFramedConcernUsageHandCoded(IConcernUsage poco, TextualNotationWriterContext writerContext, IndentedStringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = writerContext.CursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);

            if (poco.OwnedRelationship.OfType<IReferenceSubsetting>().Any())
            {
                // Alt 1: OwnedReferenceSubsetting FeatureSpecializationPart?
                if (ownedRelationshipCursor.Current is IReferenceSubsetting referenceSubsetting)
                {
                    ReferenceSubsettingTextualNotationBuilder.BuildOwnedReferenceSubsetting(referenceSubsetting, writerContext, stringBuilder);
                    ownedRelationshipCursor.Move();
                }

                if (ownedRelationshipCursor.Current is ISpecialization)
                {
                    FeatureTextualNotationBuilder.BuildFeatureSpecializationPart(poco, writerContext, stringBuilder);
                }
            }
            else
            {
                // Alt 2: UsageExtensionKeyword* 'concern' ConstraintUsageDeclaration
                while (ownedRelationshipCursor.Current is IOwningMembership membership
                       && membership.OwnedRelatedElement.OfType<IMetadataUsage>().Any())
                {
                    UsageTextualNotationBuilder.BuildUsageExtensionKeyword(poco, writerContext, stringBuilder);
                }

                stringBuilder.Append("concern ");
                ConstraintUsageTextualNotationBuilder.BuildConstraintUsageDeclaration(poco, writerContext, stringBuilder);
            }

            TypeTextualNotationBuilder.BuildRequirementBody(poco, writerContext, stringBuilder);
        }
    }
}
