// -------------------------------------------------------------------------------------------------
// <copyright file="NamespaceTextualNotationBuilder.cs" company="Starion Group S.A.">
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
    /// The <see cref="NamespaceTextualNotationBuilder" /> provides Textual Notation Builder for the <see cref="SysML2.NET.Core.POCO.Root.Namespaces.INamespace" /> element
    /// </summary>
    public static partial class NamespaceTextualNotationBuilder
    {
        /// <summary>
        /// Builds the Textual Notation string for the rule RootNamespace
        /// <para>RootNamespace:Namespace=PackageBodyElement*</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Root.Namespaces.INamespace" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildRootNamespace(SysML2.NET.Core.POCO.Root.Namespaces.INamespace poco, StringBuilder stringBuilder)
        {
            // non Terminal : PackageBodyElement; Found rule PackageBodyElement:Package=ownedRelationship+=PackageMember|ownedRelationship+=ElementFilterMember|ownedRelationship+=AliasMember|ownedRelationship+=Import 
            throw new System.NotSupportedException("Multiple alternatives not implemented yet");

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule NamespaceDeclaration
        /// <para>NamespaceDeclaration:Namespace='namespace'Identification</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Root.Namespaces.INamespace" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildNamespaceDeclaration(SysML2.NET.Core.POCO.Root.Namespaces.INamespace poco, StringBuilder stringBuilder)
        {
            stringBuilder.Append("namespace ");
            // non Terminal : Identification; Found rule Identification:Element=('<'declaredShortName=NAME'>')?(declaredName=NAME)? 
            ElementTextualNotationBuilder.BuildIdentification(poco, stringBuilder);

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule NamespaceBody
        /// <para>NamespaceBody:Namespace=';'|'{'NamespaceBodyElement*'}'</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Root.Namespaces.INamespace" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildNamespaceBody(SysML2.NET.Core.POCO.Root.Namespaces.INamespace poco, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("Multiple alternatives not implemented yet");
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule NamespaceBodyElement
        /// <para>NamespaceBodyElement:Namespace=ownedRelationship+=NamespaceMember|ownedRelationship+=AliasMember|ownedRelationship+=Import</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Root.Namespaces.INamespace" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildNamespaceBodyElement(SysML2.NET.Core.POCO.Root.Namespaces.INamespace poco, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("Multiple alternatives not implemented yet");
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule Namespace
        /// <para>Namespace=(ownedRelationship+=PrefixMetadataMember)*NamespaceDeclarationNamespaceBody</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Root.Namespaces.INamespace" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildNamespace(SysML2.NET.Core.POCO.Root.Namespaces.INamespace poco, StringBuilder stringBuilder)
        {
            // Group Element
            // Assignment Element : ownedRelationship += SysML2.NET.CodeGenerator.Grammar.Model.NonTerminalElement
            // If property ownedRelationship value is set, print SysML2.NET.CodeGenerator.Grammar.Model.NonTerminalElement

            // non Terminal : NamespaceDeclaration; Found rule NamespaceDeclaration:Namespace='namespace'Identification 
            BuildNamespaceDeclaration(poco, stringBuilder);
            // non Terminal : NamespaceBody; Found rule NamespaceBody:Namespace=';'|'{'NamespaceBodyElement*'}' 
            BuildNamespaceBody(poco, stringBuilder);

        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
