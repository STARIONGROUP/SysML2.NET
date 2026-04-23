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
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildRootNamespace(SysML2.NET.Core.POCO.Root.Namespaces.INamespace poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);
            while (ownedRelationshipCursor.Current != null)
            {
                switch (ownedRelationshipCursor.Current)
                {
                    case SysML2.NET.Core.POCO.Kernel.Packages.IElementFilterMembership elementFilterMembership:
                        ElementFilterMembershipTextualNotationBuilder.BuildElementFilterMember(elementFilterMembership, cursorCache, stringBuilder);
                        ownedRelationshipCursor.Move();
                        break;
                    case SysML2.NET.Core.POCO.Root.Namespaces.IOwningMembership owningMembership:
                        OwningMembershipTextualNotationBuilder.BuildPackageMember(owningMembership, cursorCache, stringBuilder);
                        ownedRelationshipCursor.Move();
                        break;
                    case SysML2.NET.Core.POCO.Root.Namespaces.IMembership membership:
                        MembershipTextualNotationBuilder.BuildAliasMember(membership, cursorCache, stringBuilder);
                        ownedRelationshipCursor.Move();
                        break;
                    case SysML2.NET.Core.POCO.Root.Namespaces.IImport import:
                        ImportTextualNotationBuilder.BuildImport(import, cursorCache, stringBuilder);
                        ownedRelationshipCursor.Move();
                        break;
                    default:
                        ownedRelationshipCursor.Move();
                        break;
                }

            }


        }

        /// <summary>
        /// Builds the Textual Notation string for the rule NamespaceDeclaration
        /// <para>NamespaceDeclaration:Namespace='namespace'Identification</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Root.Namespaces.INamespace" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildNamespaceDeclaration(SysML2.NET.Core.POCO.Root.Namespaces.INamespace poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            stringBuilder.Append("namespace ");
            ElementTextualNotationBuilder.BuildIdentification(poco, cursorCache, stringBuilder);

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule NamespaceBody
        /// <para>NamespaceBody:Namespace=';'|'{'NamespaceBodyElement*'}'</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Root.Namespaces.INamespace" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildNamespaceBody(SysML2.NET.Core.POCO.Root.Namespaces.INamespace poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            if (poco.OwnedRelationship.Count == 0)
            {
                stringBuilder.AppendLine(";");
            }
            else
            {
                stringBuilder.AppendLine("{");
                var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);
                while (ownedRelationshipCursor.Current != null)
                {
                    BuildNamespaceBodyElement(poco, cursorCache, stringBuilder);
                }
                stringBuilder.AppendLine("}");
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule NamespaceBodyElement
        /// <para>NamespaceBodyElement:Namespace=ownedRelationship+=NamespaceMember|ownedRelationship+=AliasMember|ownedRelationship+=Import</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Root.Namespaces.INamespace" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildNamespaceBodyElement(SysML2.NET.Core.POCO.Root.Namespaces.INamespace poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);
            switch (ownedRelationshipCursor.Current)
            {
                case SysML2.NET.Core.POCO.Root.Namespaces.IOwningMembership owningMembership:
                    OwningMembershipTextualNotationBuilder.BuildNamespaceMember(owningMembership, cursorCache, stringBuilder);
                    ownedRelationshipCursor.Move();
                    break;
                case SysML2.NET.Core.POCO.Root.Namespaces.IMembership membership:
                    MembershipTextualNotationBuilder.BuildAliasMember(membership, cursorCache, stringBuilder);
                    ownedRelationshipCursor.Move();
                    break;
                case SysML2.NET.Core.POCO.Root.Namespaces.IImport import:
                    ImportTextualNotationBuilder.BuildImport(import, cursorCache, stringBuilder);
                    ownedRelationshipCursor.Move();
                    break;
                default:
                    ownedRelationshipCursor.Move();
                    break;
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule Namespace
        /// <para>Namespace=(ownedRelationship+=PrefixMetadataMember)*NamespaceDeclarationNamespaceBody</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Root.Namespaces.INamespace" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildNamespace(SysML2.NET.Core.POCO.Root.Namespaces.INamespace poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);

            while (ownedRelationshipCursor.Current != null && ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Root.Namespaces.IOwningMembership)
            {

                if (ownedRelationshipCursor.Current != null)
                {

                    if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Root.Namespaces.IOwningMembership elementAsOwningMembership)
                    {
                        OwningMembershipTextualNotationBuilder.BuildPrefixMetadataMember(elementAsOwningMembership, cursorCache, stringBuilder);
                    }
                }
                ownedRelationshipCursor.Move();

            }
            BuildNamespaceDeclaration(poco, cursorCache, stringBuilder);
            BuildNamespaceBody(poco, cursorCache, stringBuilder);

        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
