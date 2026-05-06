// -------------------------------------------------------------------------------------------------
// <copyright file="FramedConcernMembershipTextualNotationBuilder.cs" company="Starion Group S.A.">
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
    /// The <see cref="FramedConcernMembershipTextualNotationBuilder" /> provides Textual Notation Builder for the <see cref="SysML2.NET.Core.POCO.Systems.Requirements.IFramedConcernMembership" /> element
    /// </summary>
    public static partial class FramedConcernMembershipTextualNotationBuilder
    {
        /// <summary>
        /// Builds the Textual Notation string for the rule FramedConcernMember
        /// <para>FramedConcernMember:FramedConcernMembership=MemberPrefix?'frame'ownedRelatedElement+=FramedConcernUsage</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Requirements.IFramedConcernMembership" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildFramedConcernMember(SysML2.NET.Core.POCO.Systems.Requirements.IFramedConcernMembership poco, TextualNotationWriterContext writerContext, StringBuilder stringBuilder)
        {
            var ownedRelatedElementCursor = writerContext.CursorCache.GetOrCreateCursor(poco.Id, "ownedRelatedElement", poco.OwnedRelatedElement);

            if (poco.Visibility != SysML2.NET.Core.Root.Namespaces.VisibilityKind.Public)
            {
                MembershipTextualNotationBuilder.BuildMemberPrefix(poco, writerContext, stringBuilder);
            }
            stringBuilder.Append("frame ");

            if (ownedRelatedElementCursor.Current != null)
            {

                if (ownedRelatedElementCursor.Current is SysML2.NET.Core.POCO.Systems.Requirements.IConcernUsage elementAsConcernUsage)
                {
                    ConcernUsageTextualNotationBuilder.BuildFramedConcernUsage(elementAsConcernUsage, writerContext, stringBuilder);
                }
            }
            ownedRelatedElementCursor.Move();


        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
