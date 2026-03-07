// -------------------------------------------------------------------------------------------------
// <copyright file="IXmiWriter.cs" company="Starion Group S.A.">
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
    using System.Xml;

    using SysML2.NET.Common;

    /// <summary>
    /// The purpose of the <see cref="IXmiWriter"/> is to write <see cref="IData"/> instances
    /// to an <see cref="XmlWriter"/> in XMI format
    /// </summary>
    public interface IXmiWriter
    {
        /// <summary>
        /// Writes the specified <see cref="IData"/> as an XMI element
        /// </summary>
        /// <param name="xmlWriter">The target <see cref="XmlWriter"/></param>
        /// <param name="data">The <see cref="IData"/> to write</param>
        /// <param name="elementName">The XML element name to use</param>
        /// <param name="includeDerivedProperties">Whether to include derived properties</param>
        /// <param name="elementOriginMap">The optional <see cref="IXmiElementOriginMap"/> for href reconstruction</param>
        /// <param name="currentFileUri">The <see cref="Uri"/> of the current output file for relative href computation</param>
        void Write(XmlWriter xmlWriter, IData data, string elementName, bool includeDerivedProperties, IXmiElementOriginMap elementOriginMap = null, Uri currentFileUri = null);
    }
}
