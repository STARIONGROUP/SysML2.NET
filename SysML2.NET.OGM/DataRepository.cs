// -------------------------------------------------------------------------------------------------
// <copyright file="DataRepository.cs" company="RHEA System S.A.">
// 
//   Copyright 2022-2023 RHEA System S.A.
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

namespace SysML2.NET.OGM
{
    using System;
    using System.Collections.Generic;
    
    using SysML2.NET.Common;

    /// <summary>
    /// Abstract super class from which all <see cref="IData"/> services shall derive that repositories before and after hooks
    /// on each operation: Create, Read, Update, Delete 
    /// </summary>
    public abstract class DataRepository
    {
        /// <summary>
        /// Executes additional before the create method is executed
        /// </summary>
        /// <param name="dataItem">
        /// The <see cref="IData"/> that is to be created
        /// </param>
        /// <param name="abortCreate">
        /// A value indicating whether the Read method should aborted
        /// </param>
        public virtual void BeforeCreate(IData dataItem, out bool abortCreate)
        {
            abortCreate = false;
        }

        /// <summary>
        /// Executes additional after the create method is executed
        /// </summary>
        /// <param name="dataItem">
        /// The <see cref="IData"/> that is to be created
        /// </param>
        public virtual void AfterCreate(IData dataItem)
        {
        }

        /// <summary>
        /// Executes additional before the read method is executed
        /// </summary>
        /// <param name="identifier">
        /// The <see cref="Guid"/> to read from the database (the list acts as a filter).
        /// </param>
        /// <param name="page">
        /// the page number used for pagination. The default value is -1, which means no pagination is used
        /// </param>
        /// <param name="count">
        /// The number of results per page
        /// </param>
        /// <param name="abortRead">
        /// A value indicating whether the Read method should aborted
        /// </param>
        public virtual void BeforeRead(Guid identifier, int page, int count, out bool abortRead)
        {
            abortRead = false;
        }

        /// <summary>
        /// Executes additional after the read method is executed
        /// </summary>
        /// <param name="things">
        /// The <see cref="IEnumerable{IData}"/> that may be updated
        /// </param>
        /// <param name="identifiers">
        /// The list of <see cref="Guid"/> to read from the database (the list acts as a filter).
        /// If the list is empty, then all <see cref="IData"/> objects are returned.
        /// </param>
        /// <param name="page">
        /// the page number used for pagination. The default value is -1, which means no pagination is used
        /// </param>
        /// <param name="count">
        /// The number of results per page
        /// </param>
        /// <returns>
        /// an updated list of <see cref="IEnumerable{Thing}"/>
        /// </returns>
        public virtual IEnumerable<IData> AfterRead(IEnumerable<IData> things, Guid identifiers, int page, int count)
        {
            return things;
        }
    }
}
