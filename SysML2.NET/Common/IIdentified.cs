// ------------------------------------------------------------------------------------------------
// <copyright file="IIdentified.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Common
{
    using System;

    /// <summary>
    /// Anything that is identified by a universally unique identifier
    /// </summary>
    public interface IIdentified
    {
        /// <summary>
        /// Gets or sets the unique identifier
        /// </summary>
        Guid Id { get; set; }
    }
}
