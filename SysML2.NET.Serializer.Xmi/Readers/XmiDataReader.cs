// ------------------------------------------------------------------------------------------------
// <copyright file="XmiDataReader.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Serializer.Xmi.Readers
{
    using System;
    using System.Threading.Tasks;
    using System.Xml;

    using Microsoft.Extensions.Logging;

    using SysML2.NET.Common;

    /// <summary>
    /// Abstract super class for each XMI reader
    /// </summary>
    /// <typeparam name="TData">The type of <see cref="IData" /> that needs to be read</typeparam>
    public abstract class XmiDataReader<TData> where TData : IData
    {
        /// <summary>
        /// The injected <see cref="IXmiDataCache" /> used cache the <typeparamref name="TData" />s
        /// </summary>
        protected readonly IXmiDataCache Cache;

        /// <summary>
        /// The injected <see cref="ILoggerFactory" /> used to set up logging
        /// </summary>
        protected readonly ILoggerFactory LoggerFactory;

        /// <summary>
        /// The injected <see cref="IXmiDataReaderFacade" /> that provides <see cref="XmiDataReader{TData}" /> resolve
        /// </summary>
        protected readonly IXmiDataReaderFacade XmiDataReaderFacade;

        /// <summary>
        /// The injected <see cref="IExternalReferenceService"/> used to register and process external references
        /// </summary>
        protected readonly IExternalReferenceService ExternalReferenceService;

        /// <summary>
        /// The optional <see cref="IXmiElementOriginMap"/> used to track element-to-file associations
        /// </summary>
        protected readonly IXmiElementOriginMap ElementOriginMap;

        /// <summary>
        /// the character used to split the values (of xml attributes) using a white space as separator
        /// </summary>
        protected static readonly char[] SplitMultiReference = [' '];

        /// <summary>
        /// Initializes a new instance of the <see cref="XmiDataReader{TData}" />
        /// </summary>
        /// <param name="cache">The injected <see cref="IXmiDataCache" /> used cache to store and resolve <see cref="IData"/></param>
        /// <param name="xmiDataReaderFacade">
        /// The injected <see cref="IXmiDataReaderFacade" /> that provides
        /// <see cref="XmiDataReader{TData}" /> resolve
        /// </param>
        /// <param name="externalReferenceService">The injected <see cref="IExternalReferenceService"/> used to register and process external references</param>
        /// <param name="loggerFactory">The injected <see cref="ILoggerFactory" /> used to set up logging</param>
        /// <param name="elementOriginMap">The optional <see cref="IXmiElementOriginMap"/> used to track element-to-file associations</param>
        protected XmiDataReader(IXmiDataCache cache, IXmiDataReaderFacade xmiDataReaderFacade, IExternalReferenceService externalReferenceService, ILoggerFactory loggerFactory, IXmiElementOriginMap elementOriginMap = null)
        {
            this.LoggerFactory = loggerFactory;
            this.XmiDataReaderFacade = xmiDataReaderFacade;
            this.Cache = cache;
            this.ExternalReferenceService = externalReferenceService;
            this.ElementOriginMap = elementOriginMap;
        }

        /// <summary>
        /// Reads the <typeparamref name="TData" /> object from its XML representation
        /// </summary>
        /// <param name="xmiReader">An instance of <see cref="XmlReader" /></param>
        /// <param name="currentLocation">The <see cref="Uri" /> that keep tracks of the current location</param>
        /// <returns>The read <typeparamref name="TData" /></returns>
        public abstract TData Read(XmlReader xmiReader, Uri currentLocation);
        
        /// <summary>
        /// Reads asynchronously the <typeparamref name="TData" /> object from its XML representation
        /// </summary>
        /// <param name="xmiReader">An instance of <see cref="XmlReader" /></param>
        /// <param name="currentLocation">The <see cref="Uri" /> that keep tracks of the current location</param>
        /// <returns>An awaitable <see cref="Task{TResult}"/> with the read <typeparamref name="TData" /></returns>
        public abstract Task<TData> ReadAsync(XmlReader xmiReader, Uri currentLocation);
    }
}
