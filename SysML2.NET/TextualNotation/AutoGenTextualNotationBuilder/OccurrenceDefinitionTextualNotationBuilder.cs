// -------------------------------------------------------------------------------------------------
// <copyright file="OccurrenceDefinitionTextualNotationBuilder.cs" company="Starion Group S.A.">
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
    /// The <see cref="OccurrenceDefinitionTextualNotationBuilder" /> provides Textual Notation Builder for the <see cref="SysML2.NET.Core.POCO.Systems.Occurrences.IOccurrenceDefinition" /> element
    /// </summary>
    public static partial class OccurrenceDefinitionTextualNotationBuilder
    {
        /// <summary>
        /// Builds the Textual Notation string for the rule OccurrenceDefinitionPrefix
        /// <para>OccurrenceDefinitionPrefix:OccurrenceDefinition=BasicDefinitionPrefix?(isIndividual?='individual'ownedRelationship+=EmptyMultiplicityMember)?DefinitionExtensionKeyword*</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Occurrences.IOccurrenceDefinition" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildOccurrenceDefinitionPrefix(SysML2.NET.Core.POCO.Systems.Occurrences.IOccurrenceDefinition poco, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("Multiple alternatives not implemented yet");
            if (poco.IsIndividual && poco.OwnedRelationship.Count != 0)
            {
                stringBuilder.Append("individual");
                throw new System.NotSupportedException("Assigment of enumerable not supported yet");
                stringBuilder.Append(' ');
            }

            DefinitionTextualNotationBuilder.BuildDefinitionExtensionKeyword(poco, stringBuilder);

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule IndividualDefinition
        /// <para>IndividualDefinition:OccurrenceDefinition=BasicDefinitionPrefix?isIndividual?='individual'DefinitionExtensionKeyword*'def'DefinitionownedRelationship+=EmptyMultiplicityMember</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Occurrences.IOccurrenceDefinition" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildIndividualDefinition(SysML2.NET.Core.POCO.Systems.Occurrences.IOccurrenceDefinition poco, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("Multiple alternatives not implemented yet");
            stringBuilder.Append("individual");
            DefinitionTextualNotationBuilder.BuildDefinitionExtensionKeyword(poco, stringBuilder);
            stringBuilder.Append("def ");
            DefinitionTextualNotationBuilder.BuildDefinition(poco, stringBuilder);
            throw new System.NotSupportedException("Assigment of enumerable not supported yet");

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule OccurrenceDefinition
        /// <para>OccurrenceDefinition=OccurrenceDefinitionPrefix'occurrence''def'Definition</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Occurrences.IOccurrenceDefinition" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildOccurrenceDefinition(SysML2.NET.Core.POCO.Systems.Occurrences.IOccurrenceDefinition poco, StringBuilder stringBuilder)
        {
            BuildOccurrenceDefinitionPrefix(poco, stringBuilder);
            stringBuilder.Append("occurrence ");
            stringBuilder.Append("def ");
            DefinitionTextualNotationBuilder.BuildDefinition(poco, stringBuilder);

        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
