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

namespace SysML2.NET.Serializer.TextualNotation.Writers
{
    using System.Text;

    using SysML2.NET.Core.POCO.Kernel.Connectors;

    /// <summary>
    /// Hand-coded part of the <see cref="BindingConnectorTextualNotationBuilder" />
    /// </summary>
    public static partial class BindingConnectorTextualNotationBuilder
    {
        /// <summary>
        /// Builds the Textual Notation string for the rule BindingConnectorDeclaration
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Kernel.Connectors.IBindingConnector" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        /// <remarks>
        /// BindingConnectorDeclaration : BindingConnector =
        ///     FeatureDeclaration ( 'of' ownedRelationship += ConnectorEndMember '=' ownedRelationship += ConnectorEndMember )?
        ///   | ( isSufficient ?= 'all' )? ( 'of'? ownedRelationship += ConnectorEndMember '=' ownedRelationship += ConnectorEndMember )?
        ///
        /// Auto-gen delegates entirely to this method, which in turn delegates to the shared
        /// <see cref="SharedTextualNotationBuilder.BuildTwoEndedConnectorDeclarationHandCoded"/> helper —
        /// the rule shares its body with <c>SuccessionDeclaration</c> save for the two end-keyword literals.
        /// </remarks>
        private static void BuildBindingConnectorDeclarationHandCoded(IBindingConnector poco, TextualNotationWriterContext writerContext, StringBuilder stringBuilder)
        {
            SharedTextualNotationBuilder.BuildTwoEndedConnectorDeclarationHandCoded(poco, "of ", "= ", writerContext, stringBuilder);
        }
    }
}
