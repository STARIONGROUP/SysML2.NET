// -------------------------------------------------------------------------------------------------
// <copyright file="PortUsageTextualNotationBuilder.cs" company="Starion Group S.A.">
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
    /// The <see cref="PortUsageTextualNotationBuilder" /> provides Textual Notation Builder for the <see cref="SysML2.NET.Core.POCO.Systems.Ports.IPortUsage" /> element
    /// </summary>
    public static partial class PortUsageTextualNotationBuilder
    {
        /// <summary>
        /// Builds the Textual Notation string for the rule DefaultInterfaceEnd
        /// <para>DefaultInterfaceEnd:PortUsage=isEnd?='end'Usage</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Ports.IPortUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildDefaultInterfaceEnd(SysML2.NET.Core.POCO.Systems.Ports.IPortUsage poco, StringBuilder stringBuilder)
        {
            // Assignment Element : isEnd ?= SysML2.NET.CodeGenerator.Grammar.Model.TerminalElement
            // If property isEnd value is set, print SysML2.NET.CodeGenerator.Grammar.Model.TerminalElement
            // non Terminal : Usage; Found rule Usage=UsageDeclarationUsageCompletion 
            UsageTextualNotationBuilder.BuildUsage(poco, stringBuilder);

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule InterfaceEnd
        /// <para>InterfaceEnd:PortUsage=(ownedRelationship+=OwnedCrossMultiplicityMember)?(declaredName=NAMEREFERENCES)?ownedRelationship+=OwnedReferenceSubsetting</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Ports.IPortUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildInterfaceEnd(SysML2.NET.Core.POCO.Systems.Ports.IPortUsage poco, StringBuilder stringBuilder)
        {
            // Group Element
            // Assignment Element : ownedRelationship += SysML2.NET.CodeGenerator.Grammar.Model.NonTerminalElement
            // If property ownedRelationship value is set, print SysML2.NET.CodeGenerator.Grammar.Model.NonTerminalElement

            // Group Element
            // Assignment Element : declaredName = SysML2.NET.CodeGenerator.Grammar.Model.NonTerminalElement
            // If property declaredName value is set, print SysML2.NET.CodeGenerator.Grammar.Model.NonTerminalElement
            // non Terminal : REFERENCES; Found rule REFERENCES='::>'|'references' 
            throw new System.NotSupportedException("Multiple alternatives not implemented yet");

            // Assignment Element : ownedRelationship += SysML2.NET.CodeGenerator.Grammar.Model.NonTerminalElement
            // If property ownedRelationship value is set, print SysML2.NET.CodeGenerator.Grammar.Model.NonTerminalElement

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule PortUsage
        /// <para>PortUsage=OccurrenceUsagePrefix'port'Usage</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Ports.IPortUsage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildPortUsage(SysML2.NET.Core.POCO.Systems.Ports.IPortUsage poco, StringBuilder stringBuilder)
        {
            // non Terminal : OccurrenceUsagePrefix; Found rule OccurrenceUsagePrefix:OccurrenceUsage=BasicUsagePrefix(isIndividual?='individual')?(portionKind=PortionKind{isPortion=true})?UsageExtensionKeyword* 
            OccurrenceUsageTextualNotationBuilder.BuildOccurrenceUsagePrefix(poco, stringBuilder);
            stringBuilder.Append("port ");
            // non Terminal : Usage; Found rule Usage=UsageDeclarationUsageCompletion 
            UsageTextualNotationBuilder.BuildUsage(poco, stringBuilder);

        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
