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
    using System.Collections.Generic;
    using System.Linq;
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
        public static void BuildRootNamespace(SysML2.NET.Core.POCO.Root.Namespaces.INamespace poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            // Should call the PackageBodyElement but since the rule focus on a Package type, have to grab the rule body
            // Getting cursor since assignment with += on the ownedRelationship
            var cursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);
            
            // While loop since calling with '*'

            while (cursor.Current != null)
            {
                switch (cursor.Current)
                {
                    // Order based on inheritance
                    case SysML2.NET.Core.POCO.Kernel.Packages.IElementFilterMembership elementFilterMembership:
                        ElementFilterMembershipTextualNotationBuilder.BuildElementFilterMember(elementFilterMembership, cursorCache, stringBuilder);
                        // Cursor.Move since we do have += 
                        cursor.Move();
                        break;
                    case SysML2.NET.Core.POCO.Root.Namespaces.IOwningMembership owningMembership:
                        OwningMembershipTextualNotationBuilder.BuildPackageMember(owningMembership, cursorCache, stringBuilder);
                        cursor.Move();
                        break;
                    case SysML2.NET.Core.POCO.Root.Namespaces.IMembership membership:
                        MembershipTextualNotationBuilder.BuildAliasMember(membership,cursorCache, stringBuilder);
                        cursor.Move();
                        break;
                    case SysML2.NET.Core.POCO.Root.Namespaces.IImport import:
                        ImportTextualNotationBuilder.BuildImport(import, cursorCache, stringBuilder);
                        cursor.Move();
                        break;                        
                }
            }
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
        /// <param name="elementIndex">The index of the <see cref="IElement" /> to process inside the <paramref name="elements" /> collection</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        /// <returns>The index of the next <see cref="IElement" /> to be processed inside the collection</returns>
        public static int BuildNamespaceBodyElement(SysML2.NET.Core.POCO.Root.Namespaces.INamespace poco, int elementIndex, StringBuilder stringBuilder)
        {
            switch (elementInOwnedRelationship)
            {
                case SysML2.NET.Core.POCO.Root.Namespaces.OwningMembership owningMembership:
                    OwningMembershipTextualNotationBuilder.BuildNamespaceMember(owningMembership, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Root.Namespaces.Membership membership:
                    MembershipTextualNotationBuilder.BuildAliasMember(membership, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Root.Namespaces.IImport import:
                    ImportTextualNotationBuilder.BuildImport(import, stringBuilder);
                    break;
            }

            return elementIndex;
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule Namespace
        /// <para>Namespace=(ownedRelationship+=PrefixMetadataMember)*NamespaceDeclarationNamespaceBody</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Root.Namespaces.INamespace" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildNamespace(SysML2.NET.Core.POCO.Root.Namespaces.INamespace poco, StringBuilder stringBuilder)
        {
            using var ownedRelationshipOfOwningMembershipIterator = poco.OwnedRelationship.OfType<SysML2.NET.Core.POCO.Root.Namespaces.OwningMembership>().GetEnumerator();

            while (ownedRelationshipOfOwningMembershipIterator.MoveNext())
            {

                if (ownedRelationshipOfOwningMembershipIterator.Current != null)
                {
                    OwningMembershipTextualNotationBuilder.BuildPrefixMetadataMember(ownedRelationshipOfOwningMembershipIterator.Current, stringBuilder);
                }

            }
            BuildNamespaceDeclaration(poco, stringBuilder);
            BuildNamespaceBody(poco, stringBuilder);

        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
