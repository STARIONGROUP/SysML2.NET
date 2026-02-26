// -------------------------------------------------------------------------------------------------
// <copyright file="ConnectorTextualNotationBuilder.cs" company="Starion Group S.A.">
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
    /// The <see cref="ConnectorTextualNotationBuilder" /> provides Textual Notation Builder for the <see cref="SysML2.NET.Core.POCO.Kernel.Connectors.IConnector" /> element
    /// </summary>
    public static partial class ConnectorTextualNotationBuilder
    {
        /// <summary>
        /// Builds the Textual Notation string for the rule ConnectorDeclaration
        /// <para>ConnectorDeclaration:Connector=BinaryConnectorDeclaration|NaryConnectorDeclaration</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Kernel.Connectors.IConnector" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildConnectorDeclaration(SysML2.NET.Core.POCO.Kernel.Connectors.IConnector poco, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("Multiple alternatives not implemented yet");
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule BinaryConnectorDeclaration
        /// <para>BinaryConnectorDeclaration:Connector=(FeatureDeclaration?'from'|isSufficient?='all''from'?)?ownedRelationship+=ConnectorEndMember'to'ownedRelationship+=ConnectorEndMember</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Kernel.Connectors.IConnector" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildBinaryConnectorDeclaration(SysML2.NET.Core.POCO.Kernel.Connectors.IConnector poco, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("Multiple alternatives not implemented yet");
            throw new System.NotSupportedException("Assigment of enumerable not supported yet");
            stringBuilder.Append("to ");
            throw new System.NotSupportedException("Assigment of enumerable not supported yet");

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule NaryConnectorDeclaration
        /// <para>NaryConnectorDeclaration:Connector=FeatureDeclaration?'('ownedRelationship+=ConnectorEndMember','ownedRelationship+=ConnectorEndMember(','ownedRelationship+=ConnectorEndMember)*')'</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Kernel.Connectors.IConnector" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildNaryConnectorDeclaration(SysML2.NET.Core.POCO.Kernel.Connectors.IConnector poco, StringBuilder stringBuilder)
        {
            FeatureTextualNotationBuilder.BuildFeatureDeclaration(poco, stringBuilder);
            stringBuilder.Append("(");
            throw new System.NotSupportedException("Assigment of enumerable not supported yet");
            stringBuilder.Append(",");
            throw new System.NotSupportedException("Assigment of enumerable not supported yet");
            if (poco.OwnedRelationship.Count != 0)
            {
                stringBuilder.Append(",");
                throw new System.NotSupportedException("Assigment of enumerable not supported yet");
                stringBuilder.Append(' ');
            }

            stringBuilder.Append(")");

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule Connector
        /// <para>Connector=FeaturePrefix'connector'(FeatureDeclaration?ValuePart?|ConnectorDeclaration)TypeBody</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Kernel.Connectors.IConnector" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildConnector(SysML2.NET.Core.POCO.Kernel.Connectors.IConnector poco, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("Multiple alternatives not implemented yet");
            stringBuilder.Append(' ');
            if (poco.OwnedRelationship.Count != 0)
            {
                throw new System.NotSupportedException("Assigment of enumerable not supported yet");
                stringBuilder.Append(' ');
            }


            stringBuilder.Append("connector ");
            throw new System.NotSupportedException("Multiple alternatives not implemented yet");
            stringBuilder.Append(' ');
            TypeTextualNotationBuilder.BuildTypeBody(poco, stringBuilder);

        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
