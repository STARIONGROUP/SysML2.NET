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

namespace SysML2.NET.PIM.POCO
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
        /// Gets or sets the <see cref="Project"/> <see cref="Commit"/> at which the wrapped data (payload) was created, modified, or deleted.
        /// </summary>
        [Property(lowerValue: 1, upperValue: 1)]
        public Commit Commit { get; set; }

        /// <summary>
        /// Gets or sets a reference to corresponding the <see cref="DataIdentity"/>
        /// </summary>
        [Property(lowerValue: 1, upperValue: 1)]
        public DataIdentity Identity { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="IData"/> payload
        /// </summary>
        /// <remarks>
        /// <c>null</c> when no data with the given identity is present at the <see cref="Commit"/>.
        /// </remarks>
        [Property(lowerValue: 0, upperValue: 1)]
        public IData Payload { get; set; }

        /// <summary>
        /// Queries the derived property <see cref="Project"/>
        /// </summary>
        /// <returns>The <see cref="Project"/> that owns the <see cref="DataVersion"/>.</returns>
        /// <exception cref="NotImplementedException">Thrown until the derivation is supported.</exception>
        [Property(lowerValue: 1, upperValue: 1, isDerived: true)]
        [DerivedProperty(name: "Project")]
        public Project QueryProject()
        {
            throw new NotImplementedException("Derived property Project not yet supported");
        }
    }
}
