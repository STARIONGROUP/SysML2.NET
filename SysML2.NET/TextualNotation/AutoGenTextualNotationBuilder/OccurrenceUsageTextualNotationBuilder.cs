// -------------------------------------------------------------------------------------------------
// <copyright file="OccurrenceUsageTextualNotationBuilder.cs" company="Starion Group S.A.">
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
    /// The <see cref="OccurrenceUsageTextualNotationBuilder" /> provides Textual Notation Builder for the <see cref="SysML2.NET.Core.POCO.Systems.Occurrences.IOccurrenceUsage" /> element
    /// </summary>
    public static partial class OccurrenceUsageTextualNotationBuilder
    {
        /// <summary>
        /// Builds the Textual Notation string for the rule OccurrenceUsagePrefix
        /// <para>OccurrenceUsagePrefix:OccurrenceUsage=BasicUsagePrefix(isIndividual?='individual')?(portionKind=PortionKind{isPortion=true})?UsageExtensionKeyword*</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Occurrences.IOccurrenceUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildOccurrenceUsagePrefix(SysML2.NET.Core.POCO.Systems.Occurrences.IOccurrenceUsage poco, StringBuilder stringBuilder)
        {
            // non Terminal : BasicUsagePrefix; Found rule BasicUsagePrefix:Usage=RefPrefix(isReference?='ref')? 
            UsageTextualNotationBuilder.BuildBasicUsagePrefix(poco, stringBuilder);
            // Group Element
            // Assignment Element : isIndividual ?= SysML2.NET.CodeGenerator.Grammar.Model.TerminalElement
            // If property isIndividual value is set, print SysML2.NET.CodeGenerator.Grammar.Model.TerminalElement

            // Group Element
            // Assignment Element : portionKind = SysML2.NET.CodeGenerator.Grammar.Model.NonTerminalElement
            // If property portionKind value is set, print SysML2.NET.CodeGenerator.Grammar.Model.NonTerminalElement
            // Assignment Element : isPortion = true

            // non Terminal : UsageExtensionKeyword; Found rule UsageExtensionKeyword:Usage=ownedRelationship+=PrefixMetadataMember 
            UsageTextualNotationBuilder.BuildUsageExtensionKeyword(poco, stringBuilder);

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule IndividualUsage
        /// <para>IndividualUsage:OccurrenceUsage=BasicUsagePrefixisIndividual?='individual'UsageExtensionKeyword*Usage</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Occurrences.IOccurrenceUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildIndividualUsage(SysML2.NET.Core.POCO.Systems.Occurrences.IOccurrenceUsage poco, StringBuilder stringBuilder)
        {
            // non Terminal : BasicUsagePrefix; Found rule BasicUsagePrefix:Usage=RefPrefix(isReference?='ref')? 
            UsageTextualNotationBuilder.BuildBasicUsagePrefix(poco, stringBuilder);
            // Assignment Element : isIndividual ?= SysML2.NET.CodeGenerator.Grammar.Model.TerminalElement
            // If property isIndividual value is set, print SysML2.NET.CodeGenerator.Grammar.Model.TerminalElement
            // non Terminal : UsageExtensionKeyword; Found rule UsageExtensionKeyword:Usage=ownedRelationship+=PrefixMetadataMember 
            UsageTextualNotationBuilder.BuildUsageExtensionKeyword(poco, stringBuilder);
            // non Terminal : Usage; Found rule Usage=UsageDeclarationUsageCompletion 
            UsageTextualNotationBuilder.BuildUsage(poco, stringBuilder);

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule PortionUsage
        /// <para>PortionUsage:OccurrenceUsage=BasicUsagePrefix(isIndividual?='individual')?portionKind=PortionKindUsageExtensionKeyword*Usage{isPortion=true}</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Occurrences.IOccurrenceUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildPortionUsage(SysML2.NET.Core.POCO.Systems.Occurrences.IOccurrenceUsage poco, StringBuilder stringBuilder)
        {
            // non Terminal : BasicUsagePrefix; Found rule BasicUsagePrefix:Usage=RefPrefix(isReference?='ref')? 
            UsageTextualNotationBuilder.BuildBasicUsagePrefix(poco, stringBuilder);
            // Group Element
            // Assignment Element : isIndividual ?= SysML2.NET.CodeGenerator.Grammar.Model.TerminalElement
            // If property isIndividual value is set, print SysML2.NET.CodeGenerator.Grammar.Model.TerminalElement

            // Assignment Element : portionKind = SysML2.NET.CodeGenerator.Grammar.Model.NonTerminalElement
            // If property portionKind value is set, print SysML2.NET.CodeGenerator.Grammar.Model.NonTerminalElement
            // non Terminal : UsageExtensionKeyword; Found rule UsageExtensionKeyword:Usage=ownedRelationship+=PrefixMetadataMember 
            UsageTextualNotationBuilder.BuildUsageExtensionKeyword(poco, stringBuilder);
            // non Terminal : Usage; Found rule Usage=UsageDeclarationUsageCompletion 
            UsageTextualNotationBuilder.BuildUsage(poco, stringBuilder);
            // Assignment Element : isPortion = true

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule ControlNodePrefix
        /// <para>ControlNodePrefix:OccurrenceUsage=RefPrefix(isIndividual?='individual')?(portionKind=PortionKind{isPortion=true})?UsageExtensionKeyword*</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Occurrences.IOccurrenceUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildControlNodePrefix(SysML2.NET.Core.POCO.Systems.Occurrences.IOccurrenceUsage poco, StringBuilder stringBuilder)
        {
            // non Terminal : RefPrefix; Found rule RefPrefix:Usage=(direction=FeatureDirection)?(isDerived?='derived')?(isAbstract?='abstract'|isVariation?='variation')?(isConstant?='constant')? 
            UsageTextualNotationBuilder.BuildRefPrefix(poco, stringBuilder);
            // Group Element
            // Assignment Element : isIndividual ?= SysML2.NET.CodeGenerator.Grammar.Model.TerminalElement
            // If property isIndividual value is set, print SysML2.NET.CodeGenerator.Grammar.Model.TerminalElement

            // Group Element
            // Assignment Element : portionKind = SysML2.NET.CodeGenerator.Grammar.Model.NonTerminalElement
            // If property portionKind value is set, print SysML2.NET.CodeGenerator.Grammar.Model.NonTerminalElement
            // Assignment Element : isPortion = true

            // non Terminal : UsageExtensionKeyword; Found rule UsageExtensionKeyword:Usage=ownedRelationship+=PrefixMetadataMember 
            UsageTextualNotationBuilder.BuildUsageExtensionKeyword(poco, stringBuilder);

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule OccurrenceUsage
        /// <para>OccurrenceUsage=OccurrenceUsagePrefix'occurrence'Usage</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Occurrences.IOccurrenceUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildOccurrenceUsage(SysML2.NET.Core.POCO.Systems.Occurrences.IOccurrenceUsage poco, StringBuilder stringBuilder)
        {
            // non Terminal : OccurrenceUsagePrefix; Found rule OccurrenceUsagePrefix:OccurrenceUsage=BasicUsagePrefix(isIndividual?='individual')?(portionKind=PortionKind{isPortion=true})?UsageExtensionKeyword* 
            BuildOccurrenceUsagePrefix(poco, stringBuilder);
            stringBuilder.Append("occurrence ");
            // non Terminal : Usage; Found rule Usage=UsageDeclarationUsageCompletion 
            UsageTextualNotationBuilder.BuildUsage(poco, stringBuilder);

        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
