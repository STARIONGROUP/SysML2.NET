// ------------------------------------------------------------------------------------------------
// <copyright file="TextualRepresentationExtensions.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Core.POCO.Root.Annotations
{
    using System;

    using SysML2.NET.Decorators;

    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Exceptions;

    /// <summary>
    /// The <see cref="TextualRepresentationExtensions"/> class provides extensions methods for
    /// the <see cref="ITextualRepresentation"/> interface
    /// </summary>
    internal static class TextualRepresentationExtensions
    {
        /// <summary>
        /// Computes the derived <c>representedElement</c> property of a <see cref="ITextualRepresentation"/>.
        /// </summary>
        /// <param name="textualRepresentationSubject">
        /// The subject <see cref="ITextualRepresentation"/>
        /// </param>
        /// <returns>
        /// The <see cref="IElement"/> that owns this <see cref="ITextualRepresentation"/>,
        /// which is the <c>representedElement</c>.
        /// </returns>
        /// <remarks>
        /// The representedElement must be the owner of the TextualRepresentation
        /// (OMG KerML spec: representedElement redefines owner, narrowing cardinality to 1..1).
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="textualRepresentationSubject"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="IncompleteModelException">
        /// Thrown when the <see cref="ITextualRepresentation"/> has no owner.
        /// </exception>
        [DerivedProperty(name: nameof(ITextualRepresentation.representedElement))]
        internal static IElement ComputeRepresentedElement(this ITextualRepresentation textualRepresentationSubject)
        {
            if (textualRepresentationSubject == null)
            {
                throw new ArgumentNullException(nameof(textualRepresentationSubject));
            }

            return textualRepresentationSubject.owner
                ?? throw new IncompleteModelException(
                    $"{nameof(textualRepresentationSubject)} must have an owner (its representedElement)");
        }
    }
}
