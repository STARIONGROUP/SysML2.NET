// ------------------------------------------------------------------------------------------------
// <copyright file="IElementFactory.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Dal
{
    using System; 

    /// <summary>
    /// The purpose of the <see cref="IElementFactory"/> is to create a <see cref="Core.POCO.Root.Elements.IElement"/> POCO
    /// based on a <see cref="Core.POCO.Root.Elements.IElement"/> DTO
    /// </summary>
    public interface IElementFactory
    {
        /// <summary>
        /// Creates a <see cref="Core.POCO.Root.Elements.IElement"/> POCO based on a <see cref="Core.POCO.Root.Elements.IElement"/>
        /// </summary>
        /// <param name="dto">
        /// the source <see cref="Core.DTO.Root.Elements.IElement"/> DTO
        /// </param>
        /// <returns>
        /// a <see cref="Core.POCO.Root.Elements.IElement"/> POCO
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// thrown when <paramref name="dto"/> is null
        /// </exception>
        Core.POCO.Root.Elements.IElement Create(Core.DTO.Root.Elements.IElement dto);
    }
}
