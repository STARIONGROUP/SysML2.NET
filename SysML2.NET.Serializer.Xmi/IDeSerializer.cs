// -------------------------------------------------------------------------------------------------
// <copyright file="IDeSerializer.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Serializer.Xmi
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    using SysML2.NET.Common;
    using SysML2.NET.Core.POCO.Root.Namespaces;

    /// <summary>
    /// The purpose of the <see cref="IDeSerializer"/> is to deserialize a XMI <see cref="Stream"/> to
    /// an <see cref="IData"/> and <see cref="IEnumerable{IData}"/>
    /// </summary>
    public interface IDeSerializer
    {
        /// <summary>
        /// Deserializes the XMI file to a read <see cref="INamespace"/>
        /// </summary>
        /// <param name="fileLocation">
        /// the <see cref="Uri"/> that locates the XMI file
        /// </param>
        /// <exception cref="ArgumentNullException">If the <see cref="Uri"/> is null</exception>
        /// <exception cref="FileNotFoundException">If the <see cref="Uri"/> does not locate an existing file</exception>
        /// <returns>
        /// The read <see cref="INamespace"/>
        /// </returns>
        INamespace DeSerialize(Uri fileLocation);

        /// <summary>
        /// Deserializes asynchronously the XMI file to a read <see cref="INamespace"/>
        /// </summary>
        /// <param name="fileLocation">
        /// the <see cref="Uri"/> that locates the XMI file
        /// </param>
        /// <param name="cancellationToken">An optional <see cref="CancellationToken"/> to cancel the read process</param>
        /// <exception cref="ArgumentNullException">If the <see cref="Uri"/> is null</exception>
        /// <exception cref="FileNotFoundException">If the <see cref="Uri"/> does not locate an existing file</exception>
        /// <returns>
        /// An awaitable <see cref="Task{TResult}"/> with the read <see cref="INamespace"/>
        /// </returns>
        Task<INamespace> DeSerializeAsync(Uri fileLocation, CancellationToken cancellationToken = default);
    }
}
