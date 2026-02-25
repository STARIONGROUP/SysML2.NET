// -------------------------------------------------------------------------------------------------
// <copyright file="InterfaceUsageTextualNotationBuilder.cs" company="Starion Group S.A.">
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
    /// The <see cref="InterfaceUsageTextualNotationBuilder" /> provides Textual Notation Builder for the <see cref="SysML2.NET.Core.POCO.Systems.Interfaces.IInterfaceUsage" /> element
    /// </summary>
    public static partial class InterfaceUsageTextualNotationBuilder
    {
        /// <summary>
        /// Builds the Textual Notation string for the rule InterfaceUsageDeclaration
        /// <para>InterfaceUsageDeclaration:InterfaceUsage=UsageDeclarationValuePart?('connect'InterfacePart)?|InterfacePart</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Interfaces.IInterfaceUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildInterfaceUsageDeclaration(SysML2.NET.Core.POCO.Systems.Interfaces.IInterfaceUsage poco, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("Multiple alternatives not implemented yet");
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule InterfacePart
        /// <para>InterfacePart:InterfaceUsage=BinaryInterfacePart|NaryInterfacePart</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Interfaces.IInterfaceUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildInterfacePart(SysML2.NET.Core.POCO.Systems.Interfaces.IInterfaceUsage poco, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("Multiple alternatives not implemented yet");
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule BinaryInterfacePart
        /// <para>BinaryInterfacePart:InterfaceUsage=ownedRelationship+=InterfaceEndMember'to'ownedRelationship+=InterfaceEndMember</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Interfaces.IInterfaceUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildBinaryInterfacePart(SysML2.NET.Core.POCO.Systems.Interfaces.IInterfaceUsage poco, StringBuilder stringBuilder)
        {
            // Assignment Element : ownedRelationship += SysML2.NET.CodeGenerator.Grammar.Model.NonTerminalElement
            // If property ownedRelationship value is set, print SysML2.NET.CodeGenerator.Grammar.Model.NonTerminalElement
            stringBuilder.Append("to ");
            // Assignment Element : ownedRelationship += SysML2.NET.CodeGenerator.Grammar.Model.NonTerminalElement
            // If property ownedRelationship value is set, print SysML2.NET.CodeGenerator.Grammar.Model.NonTerminalElement

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule NaryInterfacePart
        /// <para>NaryInterfacePart:InterfaceUsage='('ownedRelationship+=InterfaceEndMember','ownedRelationship+=InterfaceEndMember(','ownedRelationship+=InterfaceEndMember)*')'</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Interfaces.IInterfaceUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildNaryInterfacePart(SysML2.NET.Core.POCO.Systems.Interfaces.IInterfaceUsage poco, StringBuilder stringBuilder)
        {
            stringBuilder.Append("( ");
            // Assignment Element : ownedRelationship += SysML2.NET.CodeGenerator.Grammar.Model.NonTerminalElement
            // If property ownedRelationship value is set, print SysML2.NET.CodeGenerator.Grammar.Model.NonTerminalElement
            stringBuilder.Append(", ");
            // Assignment Element : ownedRelationship += SysML2.NET.CodeGenerator.Grammar.Model.NonTerminalElement
            // If property ownedRelationship value is set, print SysML2.NET.CodeGenerator.Grammar.Model.NonTerminalElement
            // Group Element
            stringBuilder.Append(", ");
            // Assignment Element : ownedRelationship += SysML2.NET.CodeGenerator.Grammar.Model.NonTerminalElement
            // If property ownedRelationship value is set, print SysML2.NET.CodeGenerator.Grammar.Model.NonTerminalElement

            stringBuilder.Append(") ");

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule InterfaceUsage
        /// <para>InterfaceUsage=OccurrenceUsagePrefix'interface'InterfaceUsageDeclarationInterfaceBody</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Interfaces.IInterfaceUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildInterfaceUsage(SysML2.NET.Core.POCO.Systems.Interfaces.IInterfaceUsage poco, StringBuilder stringBuilder)
        {
            // non Terminal : OccurrenceUsagePrefix; Found rule OccurrenceUsagePrefix:OccurrenceUsage=BasicUsagePrefix(isIndividual?='individual')?(portionKind=PortionKind{isPortion=true})?UsageExtensionKeyword* 
            OccurrenceUsageTextualNotationBuilder.BuildOccurrenceUsagePrefix(poco, stringBuilder);
            stringBuilder.Append("interface ");
            // non Terminal : InterfaceUsageDeclaration; Found rule InterfaceUsageDeclaration:InterfaceUsage=UsageDeclarationValuePart?('connect'InterfacePart)?|InterfacePart 
            BuildInterfaceUsageDeclaration(poco, stringBuilder);
            // non Terminal : InterfaceBody; Found rule InterfaceBody:Type=';'|'{'InterfaceBodyItem*'}' 
            TypeTextualNotationBuilder.BuildInterfaceBody(poco, stringBuilder);

        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
