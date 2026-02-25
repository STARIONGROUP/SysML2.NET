// -------------------------------------------------------------------------------------------------
// <copyright file="ImportTextualNotationBuilder.cs" company="Starion Group S.A.">
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
    /// The <see cref="ImportTextualNotationBuilder" /> provides Textual Notation Builder for the <see cref="SysML2.NET.Core.POCO.Root.Namespaces.IImport" /> element
    /// </summary>
    public static partial class ImportTextualNotationBuilder
    {
        /// <summary>
        /// Builds the Textual Notation string for the rule ImportDeclaration
        /// <para>ImportDeclaration:Import=MembershipImport|NamespaceImport</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Root.Namespaces.IImport" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildImportDeclaration(SysML2.NET.Core.POCO.Root.Namespaces.IImport poco, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("Multiple alternatives not implemented yet");
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule Import
        /// <para>Import=visibility=VisibilityIndicator'import'(isImportAll?='all')?ImportDeclarationRelationshipBody</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Root.Namespaces.IImport" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildImport(SysML2.NET.Core.POCO.Root.Namespaces.IImport poco, StringBuilder stringBuilder)
        {
            // Assignment Element : visibility = SysML2.NET.CodeGenerator.Grammar.Model.NonTerminalElement
            // If property visibility value is set, print SysML2.NET.CodeGenerator.Grammar.Model.NonTerminalElement
            stringBuilder.Append("import ");
            // Group Element
            // Assignment Element : isImportAll ?= SysML2.NET.CodeGenerator.Grammar.Model.TerminalElement
            // If property isImportAll value is set, print SysML2.NET.CodeGenerator.Grammar.Model.TerminalElement

            // non Terminal : ImportDeclaration; Found rule ImportDeclaration:Import=MembershipImport|NamespaceImport 
            BuildImportDeclaration(poco, stringBuilder);
            // non Terminal : RelationshipBody; Found rule RelationshipBody:Relationship=';'|'{'(ownedRelationship+=OwnedAnnotation)*'}' 
            RelationshipTextualNotationBuilder.BuildRelationshipBody(poco, stringBuilder);

        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
