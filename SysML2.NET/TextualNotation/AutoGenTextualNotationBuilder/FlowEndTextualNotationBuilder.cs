// -------------------------------------------------------------------------------------------------
// <copyright file="FlowEndTextualNotationBuilder.cs" company="Starion Group S.A.">
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
    /// The <see cref="FlowEndTextualNotationBuilder" /> provides Textual Notation Builder for the <see cref="SysML2.NET.Core.POCO.Kernel.Interactions.IFlowEnd" /> element
    /// </summary>
    public static partial class FlowEndTextualNotationBuilder
    {
        /// <summary>
        /// Builds the Textual Notation string for the rule FlowEnd
        /// <para>FlowEnd=(ownedRelationship+=FlowEndSubsetting)?ownedRelationship+=FlowFeatureMember</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Kernel.Interactions.IFlowEnd" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildFlowEnd(SysML2.NET.Core.POCO.Kernel.Interactions.IFlowEnd poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);

            if (ownedRelationshipCursor.Current != null)
            {

                if (ownedRelationshipCursor.Current != null)
                {

                    if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Features.IReferenceSubsetting elementAsReferenceSubsetting)
                    {
                        ReferenceSubsettingTextualNotationBuilder.BuildFlowEndSubsetting(elementAsReferenceSubsetting, cursorCache, stringBuilder);
                    }
                }
                stringBuilder.Append(' ');
            }


            if (ownedRelationshipCursor.Current != null)
            {

                if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Types.IFeatureMembership elementAsFeatureMembership)
                {
                    FeatureMembershipTextualNotationBuilder.BuildFlowFeatureMember(elementAsFeatureMembership, cursorCache, stringBuilder);
                }
            }
            ownedRelationshipCursor.Move();


        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
