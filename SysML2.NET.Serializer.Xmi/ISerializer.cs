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
        /// <param name="includeDerivedProperties">
        /// Asserts that derived properties should also be part of the serialization
        /// </param>
        /// <param name="includesImplied">
        /// The project-level includesImplied flag as defined in KerML Clause 10, Note 5.
        /// When <c>true</c>, all implied relationships are serialized and every element's isImpliedIncluded
        /// attribute is written as "true". When <c>false</c>, implied relationships (where
        /// <see cref="SysML2.NET.Core.POCO.Root.Elements.IRelationship.IsImplied"/> is true) are excluded
        /// and no element's isImpliedIncluded attribute is written.
        /// </param>
        /// <param name="stream">
        /// The target <see cref="Stream"/>
        /// </param>
        void Serialize(INamespace @namespace, bool includeDerivedProperties, bool includesImplied, Stream stream);

        /// <summary>
        /// Serialize an <see cref="INamespace"/> as XMI to a target <see cref="Stream"/>,
        /// using an origin map for href reconstruction
        /// </summary>
        /// <param name="namespace">
        /// The <see cref="INamespace"/> that shall be serialized
        /// </param>
        /// <param name="includeDerivedProperties">
        /// Asserts that derived properties should also be part of the serialization
        /// </param>
        /// <param name="includesImplied">
        /// The project-level includesImplied flag. <c>true</c>: serialize all implied relationships with isImpliedIncluded=true.
        /// <c>false</c>: exclude all implied relationships with isImpliedIncluded=false. <c>null</c>: per-element control.
        /// </param>
        /// <param name="stream">
        /// The target <see cref="Stream"/>
        /// </param>
        /// <param name="elementOriginMap">
        /// The optional <see cref="IXmiElementOriginMap"/> for href reconstruction
        /// </param>
        /// <param name="currentFileUri">
        /// The optional <see cref="Uri"/> of the current output file for relative href computation
        /// </param>
        void Serialize(INamespace @namespace, bool includeDerivedProperties, bool includesImplied, Stream stream, IXmiElementOriginMap elementOriginMap, Uri currentFileUri);

        /// <summary>
        /// Asynchronously serialize an <see cref="INamespace"/> as XMI to a target <see cref="Stream"/>
        /// </summary>
        /// <param name="namespace">
        /// The <see cref="INamespace"/> that shall be serialized
        /// </param>
        /// <param name="stream">
        /// The target <see cref="Stream"/>
        /// </param>
        /// <param name="includeDerivedProperties">
        /// Asserts that derived properties should also be part of the serialization
        /// </param>
        /// <param name="includesImplied">
        /// The project-level includesImplied flag. <c>true</c>: serialize all implied relationships with isImpliedIncluded=true.
        /// <c>false</c>: exclude all implied relationships with isImpliedIncluded=false. <c>null</c>: per-element control.
        /// </param>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken"/> used to cancel the operation
        /// </param>
        Task SerializeAsync(INamespace @namespace, bool includeDerivedProperties, bool includesImplied, Stream stream, CancellationToken cancellationToken);

        /// <summary>
        /// Serialize an <see cref="INamespace"/> to multiple XMI files based on the element origin map
        /// </summary>
        /// <param name="rootNamespace">The root <see cref="INamespace"/> containing all elements</param>
        /// <param name="elementOriginMap">The <see cref="IXmiElementOriginMap"/> tracking element-to-file associations</param>
        /// <param name="outputDirectory">The target directory for output files</param>
        /// <param name="includeDerivedProperties">
        /// Asserts that derived properties should also be part of the serialization
        /// </param>
        /// <param name="includesImplied">
        /// The project-level includesImplied flag. <c>true</c>: serialize all implied relationships with isImpliedIncluded=true.
        /// <c>false</c>: exclude all implied relationships with isImpliedIncluded=false. <c>null</c>: per-element control.
        /// </param>
        void Serialize(INamespace rootNamespace, IXmiElementOriginMap elementOriginMap, DirectoryInfo outputDirectory, bool includeDerivedProperties, bool includesImplied);
    }
}
