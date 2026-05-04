// -------------------------------------------------------------------------------------------------
// <copyright file="RequirementConstraintMembershipTextualNotationBuilder.cs" company="Starion Group S.A.">
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
    /// The <see cref="RequirementConstraintMembershipTextualNotationBuilder" /> provides Textual Notation Builder for the <see cref="SysML2.NET.Core.POCO.Systems.Requirements.IRequirementConstraintMembership" /> element
    /// </summary>
    public static partial class RequirementConstraintMembershipTextualNotationBuilder
    {
        /// <summary>
        /// Builds the Textual Notation string for the rule RequirementConstraintMember
        /// <para>RequirementConstraintMember:RequirementConstraintMembership=MemberPrefix?RequirementKindownedRelatedElement+=RequirementConstraintUsage</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Requirements.IRequirementConstraintMembership" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildRequirementConstraintMember(SysML2.NET.Core.POCO.Systems.Requirements.IRequirementConstraintMembership poco, TextualNotationWriterContext writerContext, StringBuilder stringBuilder)
        {
            var ownedRelatedElementCursor = writerContext.CursorCache.GetOrCreateCursor(poco.Id, "ownedRelatedElement", poco.OwnedRelatedElement);

            if (poco.Visibility != SysML2.NET.Core.Root.Namespaces.VisibilityKind.Public)
            {
                MembershipTextualNotationBuilder.BuildMemberPrefix(poco, writerContext, stringBuilder);
            }
            BuildRequirementKind(poco, writerContext, stringBuilder);

            if (ownedRelatedElementCursor.Current != null)
            {

                if (ownedRelatedElementCursor.Current is SysML2.NET.Core.POCO.Systems.Constraints.IConstraintUsage elementAsConstraintUsage)
                {
                    ConstraintUsageTextualNotationBuilder.BuildRequirementConstraintUsage(elementAsConstraintUsage, writerContext, stringBuilder);
                }
            }
            ownedRelatedElementCursor.Move();


        }

        /// <summary>
        /// Builds the Textual Notation string for the rule RequirementKind
        /// <para>RequirementKind:RequirementConstraintMembership='assume'{kind='assumption'}|'require'{kind='requirement'}</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Requirements.IRequirementConstraintMembership" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildRequirementKind(SysML2.NET.Core.POCO.Systems.Requirements.IRequirementConstraintMembership poco, TextualNotationWriterContext writerContext, StringBuilder stringBuilder)
        {
            switch (poco.Kind)
            {
                case SysML2.NET.Core.Systems.Requirements.RequirementConstraintKind.Assumption:
                    stringBuilder.Append("assume ");
                    break;
                case SysML2.NET.Core.Systems.Requirements.RequirementConstraintKind.Requirement:
                    stringBuilder.Append("require ");
                    break;
            }

        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
