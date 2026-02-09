// -------------------------------------------------------------------------------------------------
// <copyright file="DeSerializer.cs" company="Starion Group S.A.">
// 
//   Copyright 2022-2025 Starion Group S.A.
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
    using System.Diagnostics;
    using System.IO;
    using System.Xml;

    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Logging.Abstractions;

    using SysML2.NET.Common;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Serializer.Xmi.Readers;

    /// <summary>
    /// The purpose of the <see cref="DeSerializer"/> is to deserialize a XMI <see cref="Stream"/> to
    /// an <see cref="IData"/> and <see cref="IEnumerable{IData}"/>
    /// </summary>
    public class DeSerializer : IDeSerializer
    {
        /// <summary>
        /// The injected <see cref="ILogger{TCategoryName}"/> to produce logs statement
        /// </summary>
        private readonly ILogger<DeSerializer> logger;

        /// <summary>
        /// The injected <see cref="IExternalReferenceService"/> providing external reference file resolve
        /// </summary>
        private readonly IExternalReferenceService externalReferenceService;

        /// <summary>
        /// The injected <see cref="IXmiDataReaderFacade"/> providing <see cref="XmiDataReader{TData}"/> resolve feature based on XMI row type
        /// </summary>
        private readonly IXmiDataReaderFacade xmiDataReaderFacade;

        /// <summary>
        /// The injected <see cref="IXmiDataCache"/> storing read <see cref="IData"/>
        /// </summary>
        private readonly IXmiDataCache cache;

        /// <summary>
        /// The injected <see cref="ILoggerFactory "/> used to set up logging
        /// </summary>
        private readonly ILoggerFactory loggerFactory;

        /// <summary>Initializes a new instance of the <see cref="DeSerializer"></see> class.</summary>
        /// <param name="externalReferenceService">The injected <see cref="IExternalReferenceService"/> providing external reference file resolve</param>
        /// <param name="xmiDataReaderFacade">The injected <see cref="IXmiDataReaderFacade"/> providing <see cref="XmiDataReader{TData}"/> resolve feature based on XMI row type</param>
        /// <param name="cache">The injected <see cref="IXmiDataCache"/> storing read <see cref="IData"/></param>
        /// <param name="loggerFactory">The injected <see cref="ILoggerFactory "/> used to set up logging</param>
        public DeSerializer(IExternalReferenceService externalReferenceService, IXmiDataReaderFacade xmiDataReaderFacade, IXmiDataCache cache, ILoggerFactory loggerFactory)
        {
            this.loggerFactory =  loggerFactory ?? NullLoggerFactory.Instance;
            this.logger = this.loggerFactory.CreateLogger<DeSerializer>();
            this.cache = cache;
            this.externalReferenceService = externalReferenceService;
            this.xmiDataReaderFacade = xmiDataReaderFacade;
        }

        /// <summary>
        /// Deserializes the XMI file to a read <see cref="INamespace"/>
        /// </summary>
        /// <param name="fileLocation">
        /// the <see cref="Uri"/> that locates the XMI file
        /// </param>
        /// <exception cref="ArgumentNullException">If the <see cref="Uri"/> is null</exception>
        /// <exception cref="FileNotFoundException">If the <see cref="Uri"/> does not locate an existing file</exception>
        /// <returns>
        /// The read <see cref="INamespace"/>
        /// </returns>
        public INamespace DeSerialize(Uri fileLocation)
        {
            return this.DeSerialize(fileLocation, true);
        }

        /// <summary>
        /// Deserializes the XMI file to a read <see cref="INamespace"/>
        /// </summary>
        /// <param name="fileLocation">
        /// the <see cref="Uri"/> that locates the XMI file
        /// </param>
        /// <param name="isRoot">A value indicating whether the reading occurs on the root node</param>
        /// <exception cref="ArgumentNullException">If the <see cref="Uri"/> is null</exception>
        /// <exception cref="FileNotFoundException">If the <see cref="Uri"/> does not locate an existing file</exception>
        /// <returns>
        /// The read <see cref="INamespace"/>
        /// </returns>
        private INamespace DeSerialize(Uri fileLocation, bool isRoot)
        {
            if (fileLocation == null)
            {
                throw new ArgumentNullException(nameof(fileLocation));
            }

            var fileInfo = new FileInfo(fileLocation.LocalPath);

            if (!fileInfo.Exists)
            {
                throw new FileNotFoundException(fileLocation.LocalPath);
            }

            using var fileStream = File.OpenRead(fileLocation.LocalPath);

            this.logger.LogInformation("start deserializing from {Path}", fileInfo.Name);
            var stopWatch = Stopwatch.StartNew();

            var result = this.Read(fileStream, fileLocation, isRoot);

            this.logger.LogInformation("File {Path} deserialized in {ElapsedMilliseconds}[ms]", fileInfo.Name, stopWatch.ElapsedMilliseconds);

            return result;
        }

        /// <summary>
        /// Reads the XMI content of a <see cref="Stream"/> and extract read <see cref="INamespace"/>
        /// </summary>
        /// <param name="stream">The content<see cref="Stream"/></param>
        /// <param name="fileLocation">The <see cref="Uri"/> to locate the original <see cref="Stream"/></param>
        /// <param name="isRoot">A value indicating whether the reading occurs on the root node</param>
        /// <returns>The read <see cref="INamespace"/></returns>
        private INamespace Read(Stream stream, Uri fileLocation, bool isRoot)
        {
            stream.Seek(0, SeekOrigin.Begin);
            var stopWatch = Stopwatch.StartNew();
            var settings = new XmlReaderSettings();

            using var reader = new StreamReader(stream, detectEncodingFromByteOrderMarks: true);
            var fileInfo = new FileInfo(fileLocation.AbsolutePath);

            using var xmlReader = XmlReader.Create(reader, settings);

            this.logger.LogTrace("starting to read xml {DocumentName}", fileInfo.Name);

            xmlReader.MoveToContent();

            if (xmlReader.NodeType != XmlNodeType.Element || xmlReader.Name != "sysml:Namespace")
            {
                throw new InvalidOperationException($"The file {fileInfo.Name} does not represent a SysML:Namespace, can not process it.");
            }

            var namespaceId =  xmlReader.GetAttribute("xmi:id");

            if (Guid.TryParse(namespaceId, out var guid) && this.cache.TryGetData(guid, out var foundData) && foundData is INamespace existingNamespace)
            {
                this.logger.LogInformation("Circular dependency spot, Namespace with id {NamespaceId} already exists",  namespaceId);
                stopWatch.Stop();
                return existingNamespace;
            }

            var readNamespace = (INamespace)this.xmiDataReaderFacade.QueryXmiData(xmlReader, this.cache, fileLocation, this.externalReferenceService, this.loggerFactory, xmlReader.Name);
            stopWatch.Stop();
            this.logger.LogTrace("finished to read xml {DocumentName} in {ElapsedMilliseconds}[ms]", fileInfo.Name, stopWatch.ElapsedMilliseconds);

            this.ResolveExternalReference();

            if (isRoot)
            {
                this.cache.SynchronizeReferences();
            }

            return readNamespace;
        }
        
        /// <summary>
        /// Resolve and read all external references
        /// </summary>
        private void ResolveExternalReference()
        {
            var references = this.externalReferenceService.GetExternalReferencesToProcess();

            foreach (var reference in references)
            {
                this.DeSerialize(reference, false);
            }
        }
    }
}

