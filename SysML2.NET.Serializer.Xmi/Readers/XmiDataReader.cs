// -------------------------------------------------------------------------------------------------
// <copyright file="XmiDataReader.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Serializer.Xmi.Readers
{
    using System;
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
        protected XmiDataReader(IXmiDataCache cache, IXmiDataReaderFacade xmiDataReaderFacade, IExternalReferenceService externalReferenceService, ILoggerFactory loggerFactory)
        {
            this.LoggerFactory = loggerFactory;
            this.XmiDataReaderFacade = xmiDataReaderFacade;
            this.Cache = cache;
            this.ExternalReferenceService = externalReferenceService;
        }

        /// <summary>
        /// Reads the <typeparamref name="TData" /> object from its XML representation
        /// </summary>
        /// <param name="xmiReader">An instance of <see cref="XmlReader" /></param>
        /// <param name="currentLocation">The <see cref="Uri" /> that keep tracks of the current location</param>
        /// <returns>The read <typeparamref name="TData" /></returns>
        public abstract TData Read(XmlReader xmiReader, Uri currentLocation);
    }
}
