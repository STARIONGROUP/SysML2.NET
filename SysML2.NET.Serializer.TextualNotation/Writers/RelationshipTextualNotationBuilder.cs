// ------------------------------------------------------------------------------------------------
// <copyright file="RelationshipTextualNotationBuilder.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Serializer.TextualNotation.Writers
{

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
        /// <param name="stringBuilder">The <see cref="IndentedStringBuilder" /> that contains the entire textual notation</param>
        private static void BuildRelationshipOwnedElementHandCoded(IRelationship poco, TextualNotationWriterContext writerContext, IndentedStringBuilder stringBuilder)
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
