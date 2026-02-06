// -------------------------------------------------------------------------------------------------
// <copyright file="IXmiDataReaderFacade.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Serializer.Xmi.Readers
{
    using System;
    using System.Xml;

    using Microsoft.Extensions.Logging;

    using SysML2.NET.Common;

    /// <summary>
    /// The <see cref="IXmiDataReaderFacade"/> provides <see cref="XmiDataReader{TData}"/> resolve feature based on XMI row type
    /// </summary>
    public interface IXmiDataReaderFacade
    {
        /// <summary>
        /// Queries an instance of an <see cref="IData" /> based on the position of the <see cref="XmlReader"/>.
        /// When the reader is at an XML Element and has an attribute of xsi:type, the value of that attribute is used to select the appropriate
        /// XmiElementReader from which the <see cref="IData"/> is returned.
        /// </summary>
        /// <param name="xmiReader">The current <see cref="XmlReader"/></param>
        /// <param name="xmiDataCache">The <see cref="IXmiDataCache"/> to register and query read <see cref="IData"/></param>
        /// <param name="currentLocation">The <see cref="Uri"/> that keep tracks of the current location</param>
        /// <param name="externalReferenceService">The <see cref="IExternalReferenceService"/> used to register and process external references</param>
        /// <param name="loggerFactory">The <see cref="ILoggerFactory"/> used to set up logging</param>
        /// <param name="explicitTypeName">The explicit type name to resolve, in case of un-specified xsi:type</param>
        /// <returns>An instance of the read <see cref="IData"/></returns>
        /// <exception cref="InvalidOperationException">If the xsi:type is not supported</exception>
        IData QueryXmiData(XmlReader xmiReader, IXmiDataCache xmiDataCache, Uri currentLocation, IExternalReferenceService externalReferenceService, ILoggerFactory loggerFactory, string explicitTypeName = "");
    }
}
