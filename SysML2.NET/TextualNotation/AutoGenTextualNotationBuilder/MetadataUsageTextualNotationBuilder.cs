// -------------------------------------------------------------------------------------------------
// <copyright file="MetadataUsageTextualNotationBuilder.cs" company="Starion Group S.A.">
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
    /// The <see cref="MetadataUsageTextualNotationBuilder" /> provides Textual Notation Builder for the <see cref="SysML2.NET.Core.POCO.Systems.Metadata.IMetadataUsage" /> element
    /// </summary>
    public static partial class MetadataUsageTextualNotationBuilder
    {
        /// <summary>
        /// Builds the Textual Notation string for the rule PrefixMetadataUsage
        /// <para>PrefixMetadataUsage:MetadataUsage=ownedRelationship+=OwnedFeatureTyping</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Metadata.IMetadataUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildPrefixMetadataUsage(SysML2.NET.Core.POCO.Systems.Metadata.IMetadataUsage poco, StringBuilder stringBuilder)
        {
            // Assignment Element : ownedRelationship += SysML2.NET.CodeGenerator.Grammar.Model.NonTerminalElement
            // If property ownedRelationship value is set, print SysML2.NET.CodeGenerator.Grammar.Model.NonTerminalElement

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule MetadataUsageDeclaration
        /// <para>MetadataUsageDeclaration:MetadataUsage=(Identification(':'|'typed''by'))?ownedRelationship+=OwnedFeatureTyping</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Metadata.IMetadataUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildMetadataUsageDeclaration(SysML2.NET.Core.POCO.Systems.Metadata.IMetadataUsage poco, StringBuilder stringBuilder)
        {
            // Group Element
            // non Terminal : Identification; Found rule Identification:Element=('<'declaredShortName=NAME'>')?(declaredName=NAME)? 
            ElementTextualNotationBuilder.BuildIdentification(poco, stringBuilder);
            // Group Element
            throw new System.NotSupportedException("Multiple alternatives not implemented yet");

            // Assignment Element : ownedRelationship += SysML2.NET.CodeGenerator.Grammar.Model.NonTerminalElement
            // If property ownedRelationship value is set, print SysML2.NET.CodeGenerator.Grammar.Model.NonTerminalElement

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule MetadataUsage
        /// <para>MetadataUsage=UsageExtensionKeyword*('@'|'metadata')MetadataUsageDeclaration('about'ownedRelationship+=Annotation(','ownedRelationship+=Annotation)*)?MetadataBody</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Metadata.IMetadataUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildMetadataUsage(SysML2.NET.Core.POCO.Systems.Metadata.IMetadataUsage poco, StringBuilder stringBuilder)
        {
            // non Terminal : UsageExtensionKeyword; Found rule UsageExtensionKeyword:Usage=ownedRelationship+=PrefixMetadataMember 
            UsageTextualNotationBuilder.BuildUsageExtensionKeyword(poco, stringBuilder);
            // Group Element
            throw new System.NotSupportedException("Multiple alternatives not implemented yet");
            // non Terminal : MetadataUsageDeclaration; Found rule MetadataUsageDeclaration:MetadataUsage=(Identification(':'|'typed''by'))?ownedRelationship+=OwnedFeatureTyping 
            BuildMetadataUsageDeclaration(poco, stringBuilder);
            // Group Element
            stringBuilder.Append("about ");
            // Assignment Element : ownedRelationship += SysML2.NET.CodeGenerator.Grammar.Model.NonTerminalElement
            // If property ownedRelationship value is set, print SysML2.NET.CodeGenerator.Grammar.Model.NonTerminalElement
            // Group Element
            stringBuilder.Append(", ");
            // Assignment Element : ownedRelationship += SysML2.NET.CodeGenerator.Grammar.Model.NonTerminalElement
            // If property ownedRelationship value is set, print SysML2.NET.CodeGenerator.Grammar.Model.NonTerminalElement


            // non Terminal : MetadataBody; Found rule MetadataBody:Type=';'|'{'(ownedRelationship+=DefinitionMember|ownedRelationship+=MetadataBodyUsageMember|ownedRelationship+=AliasMember|ownedRelationship+=Import)*'}' 
            TypeTextualNotationBuilder.BuildMetadataBody(poco, stringBuilder);

        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
