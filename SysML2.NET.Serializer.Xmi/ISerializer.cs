// -------------------------------------------------------------------------------------------------
// <copyright file="ISerializer.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Serializer.Xmi
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    using SysML2.NET.Common;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Serializer.Xmi.Writers;

    /// <summary>
    /// The purpose of the <see cref="ISerializer"/> is to write an <see cref="INamespace"/>
    /// as XMI to a <see cref="Stream"/>
    /// </summary>
    public interface ISerializer
    {
        /// <summary>
        /// Serialize an <see cref="INamespace"/> as XMI to a target <see cref="Stream"/>
        /// </summary>
        /// <param name="namespace">
        /// The <see cref="INamespace"/> that shall be serialized
        /// </param>
        /// <param name="writerOptions">The <see cref="XmiWriterOptions"/> instance that provides writer output configuration</param>
        /// <param name="stream">
        /// The target <see cref="Stream"/>
        /// </param>
        void Serialize(INamespace @namespace, XmiWriterOptions writerOptions, Stream stream);

        /// <summary>
        /// Serialize an <see cref="INamespace"/> as XMI to a target <see cref="Stream"/>,
        /// using an origin map for href reconstruction
        /// </summary>
        /// <param name="namespace">
        /// The <see cref="INamespace"/> that shall be serialized
        /// </param>
        /// <param name="writerOptions">The <see cref="XmiWriterOptions"/> instance that provides writer output configuration</param>
        /// <param name="stream">
        /// The target <see cref="Stream"/>
        /// </param>
        /// <param name="elementOriginMap">
        /// The optional <see cref="IXmiElementOriginMap"/> for href reconstruction
        /// </param>
        /// <param name="currentFileUri">
        /// The optional <see cref="Uri"/> of the current output file for relative href computation
        /// </param>
        void Serialize(INamespace @namespace, XmiWriterOptions writerOptions, Stream stream, IXmiElementOriginMap elementOriginMap, Uri currentFileUri);

        /// <summary>
        /// Serialize an <see cref="INamespace"/> to multiple XMI files based on the element origin map
        /// </summary>
        /// <param name="rootNamespace">The root <see cref="INamespace"/> containing all elements</param>
        /// <param name="elementOriginMap">The <see cref="IXmiElementOriginMap"/> tracking element-to-file associations</param>
        /// <param name="outputDirectory">The target directory for output files</param>
        /// <param name="writerOptions">The <see cref="XmiWriterOptions"/> instance that provides writer output configuration</param>
        void Serialize(INamespace rootNamespace, IXmiElementOriginMap elementOriginMap, DirectoryInfo outputDirectory, XmiWriterOptions writerOptions);
        
        /// <summary>
        /// Asynchronously serialize an <see cref="INamespace"/> as XMI to a target <see cref="Stream"/>
        /// </summary>
        /// <param name="namespace">
        /// The <see cref="INamespace"/> that shall be serialized
        /// </param>
        /// <param name="stream">
        /// The target <see cref="Stream"/>
        /// </param>
        /// <param name="writerOptions">The <see cref="XmiWriterOptions"/> instance that provides writer output configuration</param>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken"/> used to cancel the operation
        /// </param>
        Task SerializeAsync(INamespace @namespace, XmiWriterOptions writerOptions, Stream stream, CancellationToken cancellationToken);

        /// <summary>
        /// Asynchronously serialize an <see cref="INamespace"/> as XMI to a target <see cref="Stream"/>,
        /// using an origin map for href reconstruction
        /// </summary>
        /// <param name="namespace">
        /// The <see cref="INamespace"/> that shall be serialized
        /// </param>
        /// <param name="writerOptions">The <see cref="XmiWriterOptions"/> instance that provides writer output configuration</param>
        /// <param name="stream">
        /// The target <see cref="Stream"/>
        /// </param>
        /// <param name="elementOriginMap">
        /// The optional <see cref="IXmiElementOriginMap"/> for href reconstruction
        /// </param>
        /// <param name="currentFileUri">
        /// The optional <see cref="Uri"/> of the current output file for relative href computation
        /// </param>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken"/> used to cancel the operation
        /// </param>
        Task SerializeAsync(INamespace @namespace, XmiWriterOptions writerOptions, Stream stream, IXmiElementOriginMap elementOriginMap, Uri currentFileUri, CancellationToken cancellationToken);

        /// <summary>
        /// Asynchronously serialize an <see cref="INamespace"/> to multiple XMI files based on the element origin map
        /// </summary>
        /// <param name="rootNamespace">The root <see cref="INamespace"/> containing all elements</param>
        /// <param name="elementOriginMap">The <see cref="IXmiElementOriginMap"/> tracking element-to-file associations</param>
        /// <param name="outputDirectory">The target directory for output files</param>
        /// <param name="writerOptions">The <see cref="XmiWriterOptions"/> instance that provides writer output configuration</param>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken"/> used to cancel the operation
        /// </param>
        Task SerializeAsync(INamespace rootNamespace, IXmiElementOriginMap elementOriginMap, DirectoryInfo outputDirectory, XmiWriterOptions writerOptions, CancellationToken cancellationToken);
    }
}
