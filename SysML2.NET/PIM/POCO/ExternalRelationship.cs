// -------------------------------------------------------------------------------------------------
// <copyright file="ExternalRelationship.cs" company="RHEA System S.A.">
// 
//   Copyright 2022-2024 RHEA System S.A.
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

namespace SysML2.NET.PIM.POCO
{
    using System;

    using SysML2.NET.Common;
    using SysML2.NET.Core.POCO;

    /// <summary>
    /// ExternalRelationship is a realization of Data, and represents the relationship between a
    /// KerML Element[KerML] in a provider tool or repository to ExternalData in another tool or repository.The
    /// ExternalData may be a KerML Element or a non-KerML Element. A hyperlink between a KerML Element to a web
    /// resource is the most primitive example of an ExternalRelationship
    /// </summary>
    public class ExternalRelationship : IData
    {
        /// <summary>
        /// Gets or sets the unique identifier
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the formal representation of the semantics of the ExternalRelationship. The specification
        /// can be a collection of mathematical expressions.For example, an ExternalRelationship can be defined to
        /// map the attributes of a KerML Element to the attributes of an ExternalData.In this case, the specification
        /// would contain mathematical expressions, such as equations, representing the mapping.This is an optional
        /// attribute.
        /// </summary>
        public string Specification { get; set; }

        /// <summary>
        /// Gets or sets the name of the expression language used for the specification. This is an optional attribute.
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="IElement"/>
        /// </summary>
        public IElement ElementEnd { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="ExternalData"/>
        /// </summary>
        public ExternalData ExternalDataEnd { get; set; }
    }
}
