// ------------------------------------------------------------------------------------------------
// <copyright file="ModelEntry.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.ModelInterchange
{
    using System;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// A model file entry inside a <c>.kpar</c> archive.
    /// </summary>
    public sealed  class ModelEntry
    {
        /// <summary>
        /// Gets the archive-relative path of the entry (e.g., <c>structure/Body.json</c>).
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Gets an optional content type hint (e.g., <c>application/json</c>, <c>application/xml</c>, <c>text/plain</c>).
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        /// Gets a factory that opens a readable stream for the entry content.
        /// </summary>
        /// <remarks>
        /// Using a factory avoids lifetime issues when the package object outlives the underlying ZIP stream.
        /// </remarks>
        public Func<CancellationToken, ValueTask<Stream>> OpenReadAsync { get; set; }
    }
}
