// -------------------------------------------------------------------------------------------------
// <copyright file="DataVersion.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.PIM.DTO
{
    using System;

    using SysML2.NET.Common;
    using SysML2.NET.Decorators;

    /// <summary>
    /// A subclass of <see cref="Record"/> that represents <see cref="IData"/> at a specific version in its lifecycle.
    /// A <see cref="DataVersion"/> record is associated with only one (1) <see cref="DataIdentity"/> record. <see cref="DataVersion"/>
    /// serves as a wrapper for <see cref="IData"/> (payload) in the context of a <see cref="Commit"/> in a <see cref="Project"/>.
    /// </summary>
    public class DataVersion : Record
    {
        /// <summary>
        /// Gets or sets the <see cref="DTO.Project"/> <see cref="Commit"/> at which the wrapped data (payload) was created, modified, or deleted.
        /// </summary>
        [Property(lowerValue: 1, upperValue: 1)]
        public Guid Commit { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="DTO.Project"/> that owns the <see cref="DataVersion"/>
        /// </summary>
        /// <remarks>
        /// Derived by Systems Modeling API and Services, Clause 7.1.2 from the owning project of <see cref="Commit"/>.
        /// </remarks>
        [Property(lowerValue: 1, upperValue: 1, isDerived: true)]
        public Guid Project { get; set; }

        /// <summary>
        /// Gets or sets the contained <see cref="DataIdentity"/>
        /// </summary>
        [Property(lowerValue: 1, upperValue: 1)]
        public DataIdentity Identity { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="IData"/> that is the payload
        /// </summary>
        /// <remarks>
        /// <c>null</c> when no data with the given identity is present at the <see cref="Commit"/>.
        /// </remarks>
        [Property(lowerValue: 0, upperValue: 1)]
        public IData Payload { get; set; }
    }
}
