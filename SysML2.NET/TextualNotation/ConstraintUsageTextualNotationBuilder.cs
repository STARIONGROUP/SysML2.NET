// -------------------------------------------------------------------------------------------------
// <copyright file="ConstraintUsageTextualNotationBuilder.cs" company="Starion Group S.A.">
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
    using System.Linq;
    using System.Text;

    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Core.POCO.Systems.Constraints;
    using SysML2.NET.Core.POCO.Systems.Metadata;

    /// <summary>
    /// Hand-coded part of the <see cref="ConstraintUsageTextualNotationBuilder" />
    /// </summary>
    public static partial class ConstraintUsageTextualNotationBuilder
    {
        /// <summary>
        /// Builds the Textual Notation string for the <c>RequirementConstraintUsage</c> rule.
        /// <para><c>RequirementConstraintUsage : ConstraintUsage =
        /// ownedRelationship += OwnedReferenceSubsetting FeatureSpecializationPart? RequirementBody
        /// | ( UsageExtensionKeyword* 'constraint' | UsageExtensionKeyword+ )
        /// ConstraintUsageDeclaration CalculationBody</c></para>
        /// <para>Uses the shared <c>ownedRelationship</c> cursor so each child element is consumed
        /// exactly once. Alt 1 consumes one <see cref="IReferenceSubsetting"/>, then a run of
        /// <see cref="ISpecialization"/>. Alt 2 consumes a run of
        /// <c>UsageExtensionKeyword</c> (a <see cref="IOwningMembership"/> wrapping an
        /// <see cref="IMetadataUsage"/>), emits the <c>'constraint'</c> keyword, then the inline
        /// <c>ConstraintUsageDeclaration</c>. The trailing body differs per alternative
        /// (<c>RequirementBody</c> vs. <c>CalculationBody</c>), so each alternative emits its
        /// own body.</para>
        /// </summary>
        /// <param name="poco">The <see cref="IConstraintUsage"/> being serialised</param>
        /// <param name="cursorCache">The <see cref="ICursorCache"/> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder"/> that contains the entire textual notation</param>
        private static void BuildRequirementConstraintUsageHandCoded(IConstraintUsage poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);

            if (poco.OwnedRelationship.OfType<IReferenceSubsetting>().Any())
            {
                // Alt 1: OwnedReferenceSubsetting FeatureSpecializationPart? RequirementBody
                if (ownedRelationshipCursor.Current is IReferenceSubsetting referenceSubsetting)
                {
                    ReferenceSubsettingTextualNotationBuilder.BuildOwnedReferenceSubsetting(referenceSubsetting, cursorCache, stringBuilder);
                    ownedRelationshipCursor.Move();
                }

                if (ownedRelationshipCursor.Current is ISpecialization)
                {
                    FeatureTextualNotationBuilder.BuildFeatureSpecializationPart(poco, cursorCache, stringBuilder);
                }

                TypeTextualNotationBuilder.BuildRequirementBody(poco, cursorCache, stringBuilder);
            }
            else
            {
                // Alt 2: UsageExtensionKeyword* 'constraint' ConstraintUsageDeclaration CalculationBody
                while (ownedRelationshipCursor.Current is IOwningMembership membership
                       && membership.OwnedRelatedElement.OfType<IMetadataUsage>().Any())
                {
                    UsageTextualNotationBuilder.BuildUsageExtensionKeyword(poco, cursorCache, stringBuilder);
                }

                stringBuilder.Append("constraint ");
                BuildConstraintUsageDeclaration(poco, cursorCache, stringBuilder);
                TypeTextualNotationBuilder.BuildCalculationBody(poco, cursorCache, stringBuilder);
            }
        }
    }
}
