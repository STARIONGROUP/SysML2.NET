// -------------------------------------------------------------------------------------------------
// <copyright file="IXmiDataWriterFacade.cs" company="Starion Group S.A.">
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

    using SysML2.NET.Common;

    /// <summary>
    /// The purpose of the <see cref="IXmiDataWriterFacade"/> is to dispatch writing of <see cref="IData"/> instances
    /// to the appropriate per-type static writer class
    /// </summary>
    public interface IXmiDataWriterFacade
    {
        /// <summary>
        /// Writes the specified <see cref="IData"/> as an XMI element by dispatching to the appropriate per-type writer
        /// </summary>
        /// <param name="xmlWriter">The target <see cref="XmlWriter"/></param>
        /// <param name="data">The <see cref="IData"/> to write</param>
        /// <param name="elementName">The XML element name to use</param>
        /// <param name="writerOptions">The <see cref="XmiWriterOptions"/> instance that provides writer output configuration</param>
        /// <param name="elementOriginMap">The optional <see cref="IXmiElementOriginMap"/> for href reconstruction</param>
        /// <param name="currentFileUri">The <see cref="Uri"/> of the current output file for relative href computation</param>
        void Write(XmlWriter xmlWriter, IData data, string elementName, XmiWriterOptions writerOptions, IXmiElementOriginMap elementOriginMap = null, Uri currentFileUri = null);

        /// <summary>
        /// Writes a contained child element, checking origin map for cross-file href
        /// </summary>
        /// <param name="xmlWriter">The target <see cref="XmlWriter"/></param>
        /// <param name="childData">The child <see cref="IData"/> to write</param>
        /// <param name="elementName">The XML element name to use</param>
        /// <param name="writerOptions">The <see cref="XmiWriterOptions"/> instance that provides writer output configuration</param>
        /// <param name="elementOriginMap">The optional <see cref="IXmiElementOriginMap"/> for href reconstruction</param>
        /// <param name="currentFileUri">The <see cref="Uri"/> of the current output file for relative href computation</param>
        /// <param name="shouldSpecifyType">Asserts that the XSI type should be specified in case of HREF</param>
        void WriteContainedElement(XmlWriter xmlWriter, IData childData, string elementName, XmiWriterOptions writerOptions, IXmiElementOriginMap elementOriginMap, Uri currentFileUri, bool shouldSpecifyType = false);

        /// <summary>
        /// Writes a reference child element, checking origin map for cross-file href
        /// </summary>
        /// <param name="xmlWriter">The target <see cref="XmlWriter"/></param>
        /// <param name="childData">The child <see cref="IData"/> to write</param>
        /// <param name="elementName">The XML element name to use</param>
        /// <param name="elementOriginMap">The optional <see cref="IXmiElementOriginMap"/> for href reconstruction</param>
        /// <param name="currentFileUri">The <see cref="Uri"/> of the current output file for relative href computation</param>
        /// <param name="shouldSpecifyType">Asserts that the XSI type should be specified in case of HREF</param>
        void WriteReferenceElement(XmlWriter xmlWriter, IData childData, string elementName, IXmiElementOriginMap elementOriginMap, Uri currentFileUri, bool shouldSpecifyType = false);

        /// <summary>
        /// Asynchronously writes the specified <see cref="IData"/> as an XMI element by dispatching to the appropriate per-type writer
        /// </summary>
        /// <param name="xmlWriter">The target <see cref="XmlWriter"/></param>
        /// <param name="data">The <see cref="IData"/> to write</param>
        /// <param name="elementName">The XML element name to use</param>
        /// <param name="writerOptions">The <see cref="XmiWriterOptions"/> instance that provides writer output configuration</param>
        /// <param name="elementOriginMap">The optional <see cref="IXmiElementOriginMap"/> for href reconstruction</param>
        /// <param name="currentFileUri">The <see cref="Uri"/> of the current output file for relative href computation</param>
        Task WriteAsync(XmlWriter xmlWriter, IData data, string elementName, XmiWriterOptions writerOptions, IXmiElementOriginMap elementOriginMap = null, Uri currentFileUri = null);

        /// <summary>
        /// Asynchronously writes a contained child element, checking origin map for cross-file href
        /// </summary>
        /// <param name="xmlWriter">The target <see cref="XmlWriter"/></param>
        /// <param name="childData">The child <see cref="IData"/> to write</param>
        /// <param name="elementName">The XML element name to use</param>
        /// <param name="writerOptions">The <see cref="XmiWriterOptions"/> instance that provides writer output configuration</param>
        /// <param name="elementOriginMap">The optional <see cref="IXmiElementOriginMap"/> for href reconstruction</param>
        /// <param name="currentFileUri">The <see cref="Uri"/> of the current output file for relative href computation</param>
        /// <param name="shouldSpecifyType">Asserts that the XSI type should be specified in case of HREF</param>
        Task WriteContainedElementAsync(XmlWriter xmlWriter, IData childData, string elementName, XmiWriterOptions writerOptions, IXmiElementOriginMap elementOriginMap, Uri currentFileUri, bool shouldSpecifyType = false);

        /// <summary>
        /// Asynchronously writes a reference child element, checking origin map for cross-file href
        /// </summary>
        /// <param name="xmlWriter">The target <see cref="XmlWriter"/></param>
        /// <param name="childData">The child <see cref="IData"/> to write</param>
        /// <param name="elementName">The XML element name to use</param>
        /// <param name="elementOriginMap">The optional <see cref="IXmiElementOriginMap"/> for href reconstruction</param>
        /// <param name="currentFileUri">The <see cref="Uri"/> of the current output file for relative href computation</param>
        /// <param name="shouldSpecifyType">Asserts that the XSI type should be specified in case of HREF</param>
        Task WriteReferenceElementAsync(XmlWriter xmlWriter, IData childData, string elementName, IXmiElementOriginMap elementOriginMap, Uri currentFileUri, bool shouldSpecifyType = false);
    }
}
