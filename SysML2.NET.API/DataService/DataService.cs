// -------------------------------------------------------------------------------------------------
// <copyright file="DataService.cs" company="RHEA System S.A.">
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

namespace SysML2.NET.API.DataService
{
    using System;
    using System.Collections.Generic;

    using SysML2.NET.Common;

    /// <summary>
    /// Abstract super class from which all <see cref="IData"/> services shall derive that provides before and after hooks
    /// on each operation: Create, Read, Update, Delete 
    /// </summary>
    public abstract class DataService
    {
        /// <summary>
        /// Executes additional before the create method is executed
        /// </summary>
        /// <param name="data">
        /// The <see cref="IData"/> that is to be created
        /// </param>
        /// <param name="abortCreate">
        /// A value indicating whether the Create method should aborted
        /// </param>
        public virtual void BeforeCreate(IData data, out bool abortCreate)
        {
            abortCreate = false;
        }

        /// <summary>
        /// Executes additional after the create method is executed
        /// </summary>
        /// <param name="data">
        /// The <see cref="IData"/> that is to be created
        /// </param>
        public virtual void AfterCreate(IData data)
        {
        }

        /// <summary>
        /// Executes additional before the read method is executed
        /// </summary>
        /// <param name="identifier">
        /// The <see cref="Guid"/> to read from the database
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
        /// <param name="dataItems">
        /// The <see cref="IEnumerable{dataItems}"/> that may be updated
        /// </param>
        /// <param name="identifier">
        /// The <see cref="Guid"/> to read from the database
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
        public virtual IEnumerable<IData> AfterRead(IEnumerable<IData> dataItems, Guid identifier, int page, int count)
        {
            return dataItems;
        }
    }
}
