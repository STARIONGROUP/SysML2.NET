// -------------------------------------------------------------------------------------------------
// <copyright file="IPimMappable.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.PSM.DTO
{
    /// <summary>
    /// Declares the correspondence between a REST/HTTP PSM model and its Platform Independent Model record
    /// </summary>
    /// <typeparam name="TRecord">
    /// The Platform Independent Model type that this model corresponds to, per Systems Modeling API and Services, Table 9.
    /// </typeparam>
    /// <remarks>
    /// <see cref="FromPim"/> is an instance mutator because static abstract interface members are unavailable on netstandard2.1.
    /// </remarks>
    public interface IPimMappable<TRecord>
    {
        /// <summary>
        /// Converts this model into its Platform Independent Model record
        /// </summary>
        /// <returns>The record carrying the members that the REST/HTTP PSM models.</returns>
        TRecord ToPim();

        /// <summary>
        /// Populates this model from its Platform Independent Model record
        /// </summary>
        /// <param name="record">The record to read from.</param>
        /// <exception cref="System.ArgumentNullException">Thrown when the record is <c>null</c>.</exception>
        void FromPim(TRecord record);
    }
}
