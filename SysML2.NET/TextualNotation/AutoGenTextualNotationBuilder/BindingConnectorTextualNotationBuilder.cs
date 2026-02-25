// -------------------------------------------------------------------------------------------------
// <copyright file="BindingConnectorTextualNotationBuilder.cs" company="Starion Group S.A.">
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
    /// The <see cref="BindingConnectorTextualNotationBuilder" /> provides Textual Notation Builder for the <see cref="SysML2.NET.Core.POCO.Kernel.Connectors.IBindingConnector" /> element
    /// </summary>
    public static partial class BindingConnectorTextualNotationBuilder
    {
        /// <summary>
        /// Builds the Textual Notation string for the rule BindingConnectorDeclaration
        /// <para>BindingConnectorDeclaration:BindingConnector=FeatureDeclaration('of'ownedRelationship+=ConnectorEndMember'='ownedRelationship+=ConnectorEndMember)?|(isSufficient?='all')?('of'?ownedRelationship+=ConnectorEndMember'='ownedRelationship+=ConnectorEndMember)?</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Kernel.Connectors.IBindingConnector" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildBindingConnectorDeclaration(SysML2.NET.Core.POCO.Kernel.Connectors.IBindingConnector poco, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("Multiple alternatives not implemented yet");
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule BindingConnector
        /// <para>BindingConnector=FeaturePrefix'binding'BindingConnectorDeclarationTypeBody</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Kernel.Connectors.IBindingConnector" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildBindingConnector(SysML2.NET.Core.POCO.Kernel.Connectors.IBindingConnector poco, StringBuilder stringBuilder)
        {
            // non Terminal : FeaturePrefix; Found rule FeaturePrefix=(EndFeaturePrefix(ownedRelationship+=OwnedCrossFeatureMember)?|BasicFeaturePrefix)(ownedRelationship+=PrefixMetadataMember)* 
            // Group Element
            throw new System.NotSupportedException("Multiple alternatives not implemented yet");
            // Group Element
            // Assignment Element : ownedRelationship += SysML2.NET.CodeGenerator.Grammar.Model.NonTerminalElement
            // If property ownedRelationship value is set, print SysML2.NET.CodeGenerator.Grammar.Model.NonTerminalElement


            stringBuilder.Append("binding ");
            // non Terminal : BindingConnectorDeclaration; Found rule BindingConnectorDeclaration:BindingConnector=FeatureDeclaration('of'ownedRelationship+=ConnectorEndMember'='ownedRelationship+=ConnectorEndMember)?|(isSufficient?='all')?('of'?ownedRelationship+=ConnectorEndMember'='ownedRelationship+=ConnectorEndMember)? 
            BuildBindingConnectorDeclaration(poco, stringBuilder);
            // non Terminal : TypeBody; Found rule TypeBody:Type=';'|'{'TypeBodyElement*'}' 
            TypeTextualNotationBuilder.BuildTypeBody(poco, stringBuilder);

        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
