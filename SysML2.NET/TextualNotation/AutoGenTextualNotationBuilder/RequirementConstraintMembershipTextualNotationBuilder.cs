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
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildRequirementConstraintMember(SysML2.NET.Core.POCO.Systems.Requirements.IRequirementConstraintMembership poco, StringBuilder stringBuilder)
        {
            MembershipTextualNotationBuilder.BuildMemberPrefix(poco, stringBuilder);
            BuildRequirementKind(poco, stringBuilder);
            throw new System.NotSupportedException("Assigment of enumerable not supported yet");

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule RequirementKind
        /// <para>RequirementKind:RequirementConstraintMembership='assume'{kind='assumption'}|'require'{kind='requirement'}</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Requirements.IRequirementConstraintMembership" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildRequirementKind(SysML2.NET.Core.POCO.Systems.Requirements.IRequirementConstraintMembership poco, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("Multiple alternatives not implemented yet");
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
