// -------------------------------------------------------------------------------------------------
// <copyright file="DependencyTextualNotationBuilder.cs" company="Starion Group S.A.">
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
    /// The <see cref="DependencyTextualNotationBuilder" /> provides Textual Notation Builder for the <see cref="SysML2.NET.Core.POCO.Root.Dependencies.IDependency" /> element
    /// </summary>
    public static partial class DependencyTextualNotationBuilder
    {
        /// <summary>
        /// Builds the Textual Notation string for the rule Dependency
        /// <para>Dependency=(ownedRelationship+=PrefixMetadataAnnotation)*'dependency'DependencyDeclarationRelationshipBody</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Root.Dependencies.IDependency" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildDependency(SysML2.NET.Core.POCO.Root.Dependencies.IDependency poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);

            while (ownedRelationshipCursor.Current != null)
            {

                if (ownedRelationshipCursor.Current != null)
                {

                    if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Root.Annotations.IAnnotation elementAsAnnotation)
                    {
                        AnnotationTextualNotationBuilder.BuildPrefixMetadataAnnotation(elementAsAnnotation, cursorCache, stringBuilder);
                    }
                }
                ownedRelationshipCursor.Move();

            }
            stringBuilder.Append("dependency ");
            var clientCursor = cursorCache.GetOrCreateCursor(poco.Id, "client", poco.Client);
            var supplierCursor = cursorCache.GetOrCreateCursor(poco.Id, "supplier", poco.Supplier);

            if (!string.IsNullOrWhiteSpace(poco.DeclaredShortName) || !string.IsNullOrWhiteSpace(poco.DeclaredName))
            {
                ElementTextualNotationBuilder.BuildIdentification(poco, cursorCache, stringBuilder);
                stringBuilder.Append("from ");
                stringBuilder.Append(' ');
            }


            if (clientCursor.Current != null)
            {
                stringBuilder.Append(clientCursor.Current.qualifiedName);
                clientCursor.Move();
            }

            while (clientCursor.Current != null)
            {
                stringBuilder.Append(", ");

                if (clientCursor.Current != null)
                {
                    stringBuilder.Append(clientCursor.Current.qualifiedName);
                    clientCursor.Move();
                }
                clientCursor.Move();

            }
            stringBuilder.Append("to ");

            if (supplierCursor.Current != null)
            {
                stringBuilder.Append(supplierCursor.Current.qualifiedName);
                supplierCursor.Move();
            }

            while (supplierCursor.Current != null)
            {
                stringBuilder.Append(", ");

                if (supplierCursor.Current != null)
                {
                    stringBuilder.Append(supplierCursor.Current.qualifiedName);
                    supplierCursor.Move();
                }
                supplierCursor.Move();

            }

            RelationshipTextualNotationBuilder.BuildRelationshipBody(poco, cursorCache, stringBuilder);

        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
