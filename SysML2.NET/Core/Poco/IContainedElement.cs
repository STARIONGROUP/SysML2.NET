// ------------------------------------------------------------------------------------------------
// <copyright file="IContainedElement.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Core.POCO.Root.Elements
{
    using SysML2.NET.Collections;

    /// <summary>
    /// Provides internal locking principle to correctly implement the containment principle  for the <see cref="IElement" />
    /// </summary>
    internal interface IContainedElement
    {
        /// <summary>
        /// Provided backing field for <see cref="IElement.OwnedRelationship" />
        /// </summary>
        ContainerList<IElement, IRelationship> OwnedRelationship { get; set; }
        
        /// <summary>
        /// Provided backing field for <see cref="IElement.OwningRelationship" />
        /// </summary>
        IRelationship OwningRelationship { get; set; }
    }
}
