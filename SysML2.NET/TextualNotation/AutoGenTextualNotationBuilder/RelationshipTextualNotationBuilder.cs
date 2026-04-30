// -------------------------------------------------------------------------------------------------
// <copyright file="RelationshipTextualNotationBuilder.cs" company="Starion Group S.A.">
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
    /// The <see cref="RelationshipTextualNotationBuilder" /> provides Textual Notation Builder for the <see cref="SysML2.NET.Core.POCO.Root.Elements.IRelationship" /> element
    /// </summary>
    public static partial class RelationshipTextualNotationBuilder
    {
        /// <summary>
        /// Builds the Textual Notation string for the rule RelationshipBody
        /// <para>RelationshipBody:Relationship=';'|'{'(ownedRelationship+=OwnedAnnotation)*'}'</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Root.Elements.IRelationship" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildRelationshipBody(SysML2.NET.Core.POCO.Root.Elements.IRelationship poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            if (poco.OwnedRelationship.Count == 0)
            {
                stringBuilder.AppendLine(";");
            }
            else
            {
                var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);
                stringBuilder.Append(' ');
                stringBuilder.AppendLine("{");

                while (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Root.Annotations.IAnnotation annotationGuard && annotationGuard.OwnedRelatedElement.OfType<SysML2.NET.Core.POCO.Root.Annotations.IAnnotatingElement>().Any())
                {

                    if (ownedRelationshipCursor.Current != null)
                    {

                        if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Root.Annotations.IAnnotation elementAsAnnotation)
                        {
                            AnnotationTextualNotationBuilder.BuildOwnedAnnotation(elementAsAnnotation, cursorCache, stringBuilder);
                        }
                    }
                    ownedRelationshipCursor.Move();

                }
                stringBuilder.AppendLine("}");
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule RelationshipOwnedElement
        /// <para>RelationshipOwnedElement:Relationship=ownedRelatedElement+=OwnedRelatedElement|ownedRelationship+=OwnedAnnotation</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Root.Elements.IRelationship" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildRelationshipOwnedElement(SysML2.NET.Core.POCO.Root.Elements.IRelationship poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            BuildRelationshipOwnedElementHandCoded(poco, cursorCache, stringBuilder);
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
