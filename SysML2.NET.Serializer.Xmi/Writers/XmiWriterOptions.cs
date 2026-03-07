// -------------------------------------------------------------------------------------------------
// <copyright file="XmiWriterOptions.cs" company="Starion Group S.A.">
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
    /// <summary>
    /// The <see cref="XmiWriterOptions"/> provides centralized option provided to <see cref="XmiDataWriter{TData}"/> to customize output format
    /// </summary>
    public class XmiWriterOptions
    {
        /// <summary>
        /// Asserts if derived properties have to be included or not.
        /// </summary>
        public bool IncludeDerivedProperties { get; set; }

        /// <summary>
        /// Asserts if implied properties have to be included or not.
        /// The project-level includesImplied flag as defined in KerML Clause 10, Note 5.
        /// When <c>true</c>, all implied relationships are serialized and every element's isImpliedIncluded
        /// attribute is written as "true". When <c>false</c>, implied relationships (where
        /// <see cref="SysML2.NET.Core.POCO.Root.Elements.IRelationship.IsImplied"/> is true) are excluded
        /// and no element's isImpliedIncluded attribute is written.
        /// </summary>
        public bool IncludeImplied { get; set; }

        /// <summary>
        /// Asserts if Id Reference values have to be written as attribute, when applicable.
        /// Default value is <c>true</c>
        /// </summary>
        public bool WriteIdRefAsAttribute { get; set; } = true;
    }
}
