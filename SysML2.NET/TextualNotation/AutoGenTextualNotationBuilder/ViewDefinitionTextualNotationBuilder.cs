// -------------------------------------------------------------------------------------------------
// <copyright file="ViewDefinitionTextualNotationBuilder.cs" company="Starion Group S.A.">
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
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using SysML2.NET.Core.POCO.Root.Elements;

    /// <summary>
    /// The <see cref="ViewDefinitionTextualNotationBuilder" /> provides Textual Notation Builder for the <see cref="SysML2.NET.Core.POCO.Systems.Views.IViewDefinition" /> element
    /// </summary>
    public static partial class ViewDefinitionTextualNotationBuilder
    {
        /// <summary>
        /// Builds the Textual Notation string for the rule ViewDefinitionBody
        /// <para>ViewDefinitionBody:ViewDefinition=';'|'{'ViewDefinitionBodyItem*'}'</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Views.IViewDefinition" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildViewDefinitionBody(SysML2.NET.Core.POCO.Systems.Views.IViewDefinition poco, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("Multiple alternatives not implemented yet");
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule ViewDefinitionBodyItem
        /// <para>ViewDefinitionBodyItem:ViewDefinition=DefinitionBodyItem|ownedRelationship+=ElementFilterMember|ownedRelationship+=ViewRenderingMember</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Views.IViewDefinition" /> from which the rule should be build</param>
        /// <param name="elementIndex">The index of the <see cref="IElement" /> to process inside the <paramref name="elements" /> collection</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        /// <returns>The index of the next <see cref="IElement" /> to be processed inside the collection</returns>
        public static int BuildViewDefinitionBodyItem(SysML2.NET.Core.POCO.Systems.Views.IViewDefinition poco, int elementIndex, StringBuilder stringBuilder)
        {
            var elementsElement = poco.OwnedRelationship[elementIndex];

            switch (elementsElement)
            {
                case SysML2.NET.Core.POCO.Kernel.Packages.ElementFilterMembership elementFilterMembership:
                    ElementFilterMembershipTextualNotationBuilder.BuildElementFilterMember(elementFilterMembership, stringBuilder); break;
                case SysML2.NET.Core.POCO.Systems.Views.ViewRenderingMembership viewRenderingMembership:
                    ViewRenderingMembershipTextualNotationBuilder.BuildViewRenderingMember(viewRenderingMembership, stringBuilder); break;
                case SysML2.NET.Core.POCO.Core.Types.IType type:
                    elementIndex = TypeTextualNotationBuilder.BuildDefinitionBodyItem(type, elementIndex, stringBuilder);
                    break;

            }
            return elementIndex;
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule ViewDefinition
        /// <para>ViewDefinition=OccurrenceDefinitionPrefix'view''def'DefinitionDeclarationViewDefinitionBody</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Views.IViewDefinition" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildViewDefinition(SysML2.NET.Core.POCO.Systems.Views.IViewDefinition poco, StringBuilder stringBuilder)
        {
            OccurrenceDefinitionTextualNotationBuilder.BuildOccurrenceDefinitionPrefix(poco, stringBuilder);
            stringBuilder.Append("view ");
            stringBuilder.Append("def ");
            DefinitionTextualNotationBuilder.BuildDefinitionDeclaration(poco, stringBuilder);
            BuildViewDefinitionBody(poco, stringBuilder);

        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
