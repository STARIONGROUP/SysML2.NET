// -------------------------------------------------------------------------------------------------
// <copyright file="PortDefinitionTextualNotationBuilder.cs" company="Starion Group S.A.">
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
    /// The <see cref="PortDefinitionTextualNotationBuilder" /> provides Textual Notation Builder for the <see cref="SysML2.NET.Core.POCO.Systems.Ports.IPortDefinition" /> element
    /// </summary>
    public static partial class PortDefinitionTextualNotationBuilder
    {
        /// <summary>
        /// Builds the Textual Notation string for the rule PortDefinition
        /// <para>PortDefinition=DefinitionPrefix'port''def'DefinitionownedRelationship+=ConjugatedPortDefinitionMember{conjugatedPortDefinition.ownedPortConjugator.originalPortDefinition=this}</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Ports.IPortDefinition" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildPortDefinition(SysML2.NET.Core.POCO.Systems.Ports.IPortDefinition poco, StringBuilder stringBuilder)
        {
            // non Terminal : DefinitionPrefix; Found rule DefinitionPrefix:Definition=BasicDefinitionPrefix?DefinitionExtensionKeyword* 
            DefinitionTextualNotationBuilder.BuildDefinitionPrefix(poco, stringBuilder);
            stringBuilder.Append("port ");
            stringBuilder.Append("def ");
            // non Terminal : Definition; Found rule Definition=DefinitionDeclarationDefinitionBody 
            DefinitionTextualNotationBuilder.BuildDefinition(poco, stringBuilder);
            // Assignment Element : ownedRelationship += SysML2.NET.CodeGenerator.Grammar.Model.NonTerminalElement
            // If property ownedRelationship value is set, print SysML2.NET.CodeGenerator.Grammar.Model.NonTerminalElement
            // Assignment Element : conjugatedPortDefinition.ownedPortConjugator.originalPortDefinition = this

        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
