// -------------------------------------------------------------------------------------------------
// <copyright file="ForkNodeTextualNotationBuilder.cs" company="Starion Group S.A.">
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
    /// The <see cref="ForkNodeTextualNotationBuilder" /> provides Textual Notation Builder for the <see cref="SysML2.NET.Core.POCO.Systems.Actions.IForkNode" /> element
    /// </summary>
    public static partial class ForkNodeTextualNotationBuilder
    {
        /// <summary>
        /// Builds the Textual Notation string for the rule ForkNode
        /// <para>ForkNode=ControlNodePrefixisComposite?='fork'UsageDeclarationActionBody</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Actions.IForkNode" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildForkNode(SysML2.NET.Core.POCO.Systems.Actions.IForkNode poco, StringBuilder stringBuilder)
        {
            // non Terminal : ControlNodePrefix; Found rule ControlNodePrefix:OccurrenceUsage=RefPrefix(isIndividual?='individual')?(portionKind=PortionKind{isPortion=true})?UsageExtensionKeyword* 
            OccurrenceUsageTextualNotationBuilder.BuildControlNodePrefix(poco, stringBuilder);
            // Assignment Element : isComposite ?= SysML2.NET.CodeGenerator.Grammar.Model.TerminalElement
            // If property isComposite value is set, print SysML2.NET.CodeGenerator.Grammar.Model.TerminalElement
            // non Terminal : UsageDeclaration; Found rule UsageDeclaration:Usage=IdentificationFeatureSpecializationPart? 
            UsageTextualNotationBuilder.BuildUsageDeclaration(poco, stringBuilder);
            // non Terminal : ActionBody; Found rule ActionBody:Type=';'|'{'ActionBodyItem*'}' 
            TypeTextualNotationBuilder.BuildActionBody(poco, stringBuilder);

        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
