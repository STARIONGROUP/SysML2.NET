// -------------------------------------------------------------------------------------------------
// <copyright file="ModelEntry.cs" company="Starion Group S.A.">
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
