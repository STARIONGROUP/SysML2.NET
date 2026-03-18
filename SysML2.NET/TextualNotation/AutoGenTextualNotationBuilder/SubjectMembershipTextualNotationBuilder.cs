// -------------------------------------------------------------------------------------------------
// <copyright file="SubjectMembershipTextualNotationBuilder.cs" company="Starion Group S.A.">
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
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using SysML2.NET.Core.POCO.Root.Elements;

    /// <summary>
    /// The <see cref="SubjectMembershipTextualNotationBuilder" /> provides Textual Notation Builder for the <see cref="SysML2.NET.Core.POCO.Systems.Requirements.ISubjectMembership" /> element
    /// </summary>
    public static partial class SubjectMembershipTextualNotationBuilder
    {
        /// <summary>
        /// Builds the Textual Notation string for the rule SubjectMember
        /// <para>SubjectMember:SubjectMembership=MemberPrefixownedRelatedElement+=SubjectUsage</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Requirements.ISubjectMembership" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildSubjectMember(SysML2.NET.Core.POCO.Systems.Requirements.ISubjectMembership poco, StringBuilder stringBuilder)
        {
            using var ownedRelatedElementOfReferenceUsageIterator = poco.OwnedRelatedElement.OfType<SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.ReferenceUsage>().GetEnumerator();
            MembershipTextualNotationBuilder.BuildMemberPrefix(poco, stringBuilder);
            ownedRelatedElementOfReferenceUsageIterator.MoveNext();

            if (ownedRelatedElementOfReferenceUsageIterator.Current != null)
            {
                ReferenceUsageTextualNotationBuilder.BuildSubjectUsage(ownedRelatedElementOfReferenceUsageIterator.Current, stringBuilder);
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule SatisfactionSubjectMember
        /// <para>SatisfactionSubjectMember:SubjectMembership=ownedRelatedElement+=SatisfactionParameter</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Requirements.ISubjectMembership" /> from which the rule should be build</param>
        /// <param name="elementIndex">The index of the <see cref="IElement" /> to process inside the <paramref name="elements" /> collection</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        /// <returns>The index of the next <see cref="IElement" /> to be processed inside the collection</returns>
        public static int BuildSatisfactionSubjectMember(SysML2.NET.Core.POCO.Systems.Requirements.ISubjectMembership poco, int elementIndex, StringBuilder stringBuilder)
        {
            if (elementIndex < poco.OwnedRelatedElement.Count)
            {
                var elementForOwnedRelatedElement = poco.OwnedRelatedElement[elementIndex];

                if (elementForOwnedRelatedElement is SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IReferenceUsage elementAsReferenceUsage)
                {
                    ReferenceUsageTextualNotationBuilder.BuildSatisfactionParameter(elementAsReferenceUsage, 0, stringBuilder);
                }
            }

            return elementIndex;
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
