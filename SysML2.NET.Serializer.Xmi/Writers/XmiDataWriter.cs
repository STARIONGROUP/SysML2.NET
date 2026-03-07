// -------------------------------------------------------------------------------------------------
// <copyright file="XmiDataWriter.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Serializer.Xmi.Writers
{
    using System;
    using System.Threading.Tasks;
    using System.Xml;

    using Microsoft.Extensions.Logging;

    using SysML2.NET.Common;

    /// <summary>
    /// Abstract super class for each XMI writer
    /// </summary>
    /// <typeparam name="TData">The type of <see cref="IData" /> that needs to be written</typeparam>
    public abstract class XmiDataWriter<TData> where TData : IData
    {
        /// <summary>
        /// The injected <see cref="IXmiDataWriterFacade" /> that provides dispatch to per-type writers
        /// </summary>
        protected readonly IXmiDataWriterFacade XmiDataWriterFacade;

        /// <summary>
        /// The injected <see cref="ILoggerFactory" /> used to set up logging
        /// </summary>
        protected readonly ILoggerFactory LoggerFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="XmiDataWriter{TData}" />
        /// </summary>
        /// <param name="xmiDataWriterFacade">
        /// The injected <see cref="IXmiDataWriterFacade" /> that provides dispatch to per-type writers
        /// </param>
        /// <param name="loggerFactory">The injected <see cref="ILoggerFactory" /> used to set up logging</param>
        protected XmiDataWriter(IXmiDataWriterFacade xmiDataWriterFacade, ILoggerFactory loggerFactory)
        {
            this.XmiDataWriterFacade = xmiDataWriterFacade;
            this.LoggerFactory = loggerFactory;
        }

        /// <summary>
        /// Writes the <typeparamref name="TData" /> object to its XML representation
        /// </summary>
        /// <param name="xmlWriter">The target <see cref="XmlWriter" /></param>
        /// <param name="data">The <typeparamref name="TData" /> to write</param>
        /// <param name="elementName">The XML element name</param>
        /// <param name="includeDerivedProperties">Whether to include derived properties</param>
        /// <param name="includesImplied">
        /// The project-level includesImplied flag as defined in KerML Clause 10, Note 5.
        /// When <c>true</c>, all implied relationships are serialized and every element's isImpliedIncluded
        /// attribute is written as "true". When <c>false</c>, implied relationships (where
        /// <see cref="SysML2.NET.Core.POCO.Root.Elements.IRelationship.IsImplied"/> is true) are excluded
        /// and no element's isImpliedIncluded attribute is written.
        /// </param>
        /// <param name="elementOriginMap">The optional <see cref="IXmiElementOriginMap" /> for href reconstruction</param>
        /// <param name="currentFileUri">The optional <see cref="Uri" /> of the current output file</param>
        public abstract void Write(XmlWriter xmlWriter, TData data, string elementName,
            bool includeDerivedProperties, bool includesImplied, IXmiElementOriginMap elementOriginMap = null,
            Uri currentFileUri = null);

        /// <summary>
        /// Asynchronously writes the <typeparamref name="TData" /> object to its XML representation
        /// </summary>
        /// <param name="xmlWriter">The target <see cref="XmlWriter" /></param>
        /// <param name="data">The <typeparamref name="TData" /> to write</param>
        /// <param name="elementName">The XML element name</param>
        /// <param name="includeDerivedProperties">Whether to include derived properties</param>
        /// <param name="includesImplied">
        /// The project-level includesImplied flag as defined in KerML Clause 10, Note 5.
        /// When <c>true</c>, all implied relationships are serialized and every element's isImpliedIncluded
        /// attribute is written as "true". When <c>false</c>, implied relationships (where
        /// <see cref="SysML2.NET.Core.POCO.Root.Elements.IRelationship.IsImplied"/> is true) are excluded
        /// and no element's isImpliedIncluded attribute is written.
        /// </param>
        /// <param name="elementOriginMap">The optional <see cref="IXmiElementOriginMap" /> for href reconstruction</param>
        /// <param name="currentFileUri">The optional <see cref="Uri" /> of the current output file</param>
        /// <returns>An awaitable <see cref="Task" /></returns>
        public abstract Task WriteAsync(XmlWriter xmlWriter, TData data, string elementName,
            bool includeDerivedProperties, bool includesImplied, IXmiElementOriginMap elementOriginMap = null,
            Uri currentFileUri = null);
    }
}
