// ------------------------------------------------------------------------------------------------
// <copyright file="IContainedRelationship.cs" company="Starion Group S.A.">
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
    /// Provides internal locking principle to correctly implement the containment principle  for the <see cref="IRelationship" />
    /// </summary>
    internal interface IContainedRelationship: IContainedElement
    {
        /// <summary>
        /// Backing field for the <see cref="IRelationship.OwnedRelatedElement" />
        /// </summary>
        ContainerList<IRelationship, IElement> OwnedRelatedElement { get; set; }
        
        /// <summary>
        /// Backing field for the <see cref="IRelationship.OwningRelatedElement" />
        /// </summary>
        IElement OwningRelatedElement { get; set; }
    }
}
