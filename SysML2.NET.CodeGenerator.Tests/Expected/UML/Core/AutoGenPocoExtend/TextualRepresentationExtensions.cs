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
    using System.Collections.Generic;

    using SysML2.NET.Decorators;

    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;

    /// <summary>
    /// The <see cref="TextualRepresentationExtensions"/> class provides extensions methods for
    /// the <see cref="ITextualRepresentation"/> interface
    /// </summary>
    internal static class TextualRepresentationExtensions
    {
        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="textualRepresentationSubject">
        /// The subject <see cref="ITextualRepresentation"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        [DerivedProperty(name: nameof(ITextualRepresentation.representedElement))]
        internal static IElement ComputeRepresentedElement(this ITextualRepresentation textualRepresentationSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

    }
}
