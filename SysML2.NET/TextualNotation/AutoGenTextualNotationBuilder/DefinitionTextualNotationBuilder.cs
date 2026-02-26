// -------------------------------------------------------------------------------------------------
// <copyright file="DefinitionTextualNotationBuilder.cs" company="Starion Group S.A.">
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
    /// The <see cref="DefinitionTextualNotationBuilder" /> provides Textual Notation Builder for the <see cref="SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IDefinition" /> element
    /// </summary>
    public static partial class DefinitionTextualNotationBuilder
    {
        /// <summary>
        /// Builds the Textual Notation string for the rule DefinitionExtensionKeyword
        /// <para>DefinitionExtensionKeyword:Definition=ownedRelationship+=PrefixMetadataMember</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IDefinition" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildDefinitionExtensionKeyword(SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IDefinition poco, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("Assigment of enumerable not supported yet");

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule DefinitionPrefix
        /// <para>DefinitionPrefix:Definition=BasicDefinitionPrefix?DefinitionExtensionKeyword*</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IDefinition" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildDefinitionPrefix(SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IDefinition poco, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("Multiple alternatives not implemented yet");
            BuildDefinitionExtensionKeyword(poco, stringBuilder);

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule DefinitionDeclaration
        /// <para>DefinitionDeclaration:Definition=IdentificationSubclassificationPart?</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IDefinition" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildDefinitionDeclaration(SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IDefinition poco, StringBuilder stringBuilder)
        {
            ElementTextualNotationBuilder.BuildIdentification(poco, stringBuilder);
            ClassifierTextualNotationBuilder.BuildSubclassificationPart(poco, stringBuilder);

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule ExtendedDefinition
        /// <para>ExtendedDefinition:Definition=BasicDefinitionPrefix?DefinitionExtensionKeyword+'def'Definition</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IDefinition" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildExtendedDefinition(SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IDefinition poco, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("Multiple alternatives not implemented yet");
            BuildDefinitionExtensionKeyword(poco, stringBuilder);
            stringBuilder.Append("def ");
            BuildDefinition(poco, stringBuilder);

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule Definition
        /// <para>Definition=DefinitionDeclarationDefinitionBody</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IDefinition" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildDefinition(SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IDefinition poco, StringBuilder stringBuilder)
        {
            BuildDefinitionDeclaration(poco, stringBuilder);
            TypeTextualNotationBuilder.BuildDefinitionBody(poco, stringBuilder);

        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
