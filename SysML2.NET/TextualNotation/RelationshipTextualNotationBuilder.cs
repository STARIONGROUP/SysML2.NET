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

namespace SysML2.NET.TextualNotation
{
    using System.Text;

    using SysML2.NET.Core.POCO.Root.Annotations;
    using SysML2.NET.Core.POCO.Root.Elements;

    /// <summary>
    /// Hand-coded part of the <see cref="RelationshipTextualNotationBuilder" />
    /// </summary>
    public static partial class RelationshipTextualNotationBuilder
    {
        /// <summary>
        /// Builds the Textual Notation string for the rule RelationshipOwnedElement
        /// <remarks>RelationshipOwnedElement:Relationship=ownedRelatedElement+=OwnedRelatedElement|ownedRelationship+=OwnedAnnotation. Each alternative consumes exactly one element from its respective collection.</remarks>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Root.Elements.IRelationship" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        private static void BuildRelationshipOwnedElementHandCoded(IRelationship poco, TextualNotationWriterContext writerContext, StringBuilder stringBuilder)
        {
            // Alternative 1: process one OwnedRelatedElement if the ownedRelatedElement cursor has a current value
            var ownedRelatedElementCursor = writerContext.CursorCache.GetOrCreateCursor(poco.Id, "ownedRelatedElement", poco.OwnedRelatedElement);

            if (ownedRelatedElementCursor.Current != null)
            {
                ElementTextualNotationBuilder.BuildOwnedRelatedElement(ownedRelatedElementCursor.Current, writerContext, stringBuilder);
                ownedRelatedElementCursor.Move();
                return;
            }

            // Alternative 2: process one OwnedAnnotation from the ownedRelationship cursor
            var ownedRelationshipCursor = writerContext.CursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);

            if (ownedRelationshipCursor.Current is IAnnotation annotation)
            {
                AnnotationTextualNotationBuilder.BuildOwnedAnnotation(annotation, writerContext, stringBuilder);
                ownedRelationshipCursor.Move();
            }
        }
    }
}
