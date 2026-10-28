// ------------------------------------------------------------------------------------------------
// <copyright file="IAssembler.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Dal
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;

    /// <summary>
    /// The purpose of the <see cref="IAssembler"/> is to assemble a SysML2.NET POCO object graph from a
    /// list of data-transfer-objects (DTO)
    /// </summary>
    public interface IAssembler
    {
        /// <summary>
        /// Gets the Cache that contains all the <see cref="Core.POCO.Root.Elements.IElement"/>s
        /// </summary>
        ConcurrentDictionary<Guid, Lazy<Core.POCO.Root.Elements.IElement>> Cache { get; }

        /// <summary>
        /// Synchronize the Cache based on the provided <paramref name="dtos"/>
        /// </summary>
        /// <param name="dtos">
        /// the DTOs used to update the cache with
        /// </param>
        void Synchronize(IEnumerable<Core.DTO.Root.Elements.IElement> dtos);
    }
}
