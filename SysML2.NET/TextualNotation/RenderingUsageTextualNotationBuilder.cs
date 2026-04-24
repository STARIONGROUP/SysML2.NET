// -------------------------------------------------------------------------------------------------
// <copyright file="RenderingUsageTextualNotationBuilder.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Core.POCO.Systems.Metadata;
    using SysML2.NET.Core.POCO.Systems.Views;

    /// <summary>
    /// Hand-coded part of the <see cref="RenderingUsageTextualNotationBuilder" />
    /// </summary>
    public static partial class RenderingUsageTextualNotationBuilder
    {
        /// <summary>
        /// Builds the Textual Notation string for the <c>ViewRenderingUsage</c> rule.
        /// <para><c>ViewRenderingUsage : RenderingUsage =
        /// ownedRelationship += OwnedReferenceSubsetting FeatureSpecializationPart? UsageBody
        /// | ( UsageExtensionKeyword* 'rendering' | UsageExtensionKeyword+ ) Usage</c></para>
        /// <para>Alt 1 terminates with <c>UsageBody</c>; Alt 2 terminates with a full <c>Usage</c>
        /// which already includes its own body, so each alternative emits its own tail.</para>
        /// </summary>
        /// <param name="poco">The <see cref="IRenderingUsage"/> being serialised</param>
        /// <param name="cursorCache">The <see cref="ICursorCache"/> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder"/> that contains the entire textual notation</param>
        private static void BuildViewRenderingUsageHandCoded(IRenderingUsage poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);

            if (poco.OwnedRelationship.OfType<IReferenceSubsetting>().Any())
            {
                // Alt 1: OwnedReferenceSubsetting FeatureSpecializationPart? UsageBody
                if (ownedRelationshipCursor.Current is IReferenceSubsetting referenceSubsetting)
                {
                    ReferenceSubsettingTextualNotationBuilder.BuildOwnedReferenceSubsetting(referenceSubsetting, cursorCache, stringBuilder);
                    ownedRelationshipCursor.Move();
                }

                if (ownedRelationshipCursor.Current is ISpecialization)
                {
                    FeatureTextualNotationBuilder.BuildFeatureSpecialization(poco, cursorCache, stringBuilder);
                }

                UsageTextualNotationBuilder.BuildUsageBody(poco, cursorCache, stringBuilder);
            }
            else
            {
                // Alt 2: UsageExtensionKeyword* 'rendering' Usage
                while (ownedRelationshipCursor.Current is IOwningMembership membership
                       && membership.OwnedRelatedElement.OfType<IMetadataUsage>().Any())
                {
                    UsageTextualNotationBuilder.BuildUsageExtensionKeyword(poco, cursorCache, stringBuilder);
                }

                stringBuilder.Append("rendering ");
                UsageTextualNotationBuilder.BuildUsage(poco, cursorCache, stringBuilder);
            }
        }
    }
}
