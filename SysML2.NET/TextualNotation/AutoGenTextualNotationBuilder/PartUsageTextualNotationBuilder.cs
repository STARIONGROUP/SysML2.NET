// -------------------------------------------------------------------------------------------------
// <copyright file="PartUsageTextualNotationBuilder.cs" company="Starion Group S.A.">
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
    /// The <see cref="PartUsageTextualNotationBuilder" /> provides Textual Notation Builder for the <see cref="SysML2.NET.Core.POCO.Systems.Parts.IPartUsage" /> element
    /// </summary>
    public static partial class PartUsageTextualNotationBuilder
    {
        /// <summary>
        /// Builds the Textual Notation string for the rule ActorUsage
        /// <para>ActorUsage:PartUsage='actor'UsageExtensionKeyword*Usage</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Parts.IPartUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildActorUsage(SysML2.NET.Core.POCO.Systems.Parts.IPartUsage poco, StringBuilder stringBuilder)
        {
            stringBuilder.Append("actor ");
            // non Terminal : UsageExtensionKeyword; Found rule UsageExtensionKeyword:Usage=ownedRelationship+=PrefixMetadataMember 
            UsageTextualNotationBuilder.BuildUsageExtensionKeyword(poco, stringBuilder);
            // non Terminal : Usage; Found rule Usage=UsageDeclarationUsageCompletion 
            UsageTextualNotationBuilder.BuildUsage(poco, stringBuilder);

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule StakeholderUsage
        /// <para>StakeholderUsage:PartUsage='stakeholder'UsageExtensionKeyword*Usage</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Parts.IPartUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildStakeholderUsage(SysML2.NET.Core.POCO.Systems.Parts.IPartUsage poco, StringBuilder stringBuilder)
        {
            stringBuilder.Append("stakeholder ");
            // non Terminal : UsageExtensionKeyword; Found rule UsageExtensionKeyword:Usage=ownedRelationship+=PrefixMetadataMember 
            UsageTextualNotationBuilder.BuildUsageExtensionKeyword(poco, stringBuilder);
            // non Terminal : Usage; Found rule Usage=UsageDeclarationUsageCompletion 
            UsageTextualNotationBuilder.BuildUsage(poco, stringBuilder);

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule PartUsage
        /// <para>PartUsage=OccurrenceUsagePrefix'part'Usage</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Parts.IPartUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildPartUsage(SysML2.NET.Core.POCO.Systems.Parts.IPartUsage poco, StringBuilder stringBuilder)
        {
            // non Terminal : OccurrenceUsagePrefix; Found rule OccurrenceUsagePrefix:OccurrenceUsage=BasicUsagePrefix(isIndividual?='individual')?(portionKind=PortionKind{isPortion=true})?UsageExtensionKeyword* 
            OccurrenceUsageTextualNotationBuilder.BuildOccurrenceUsagePrefix(poco, stringBuilder);
            stringBuilder.Append("part ");
            // non Terminal : Usage; Found rule Usage=UsageDeclarationUsageCompletion 
            UsageTextualNotationBuilder.BuildUsage(poco, stringBuilder);

        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
