// ------------------------------------------------------------------------------------------------
// <copyright file="IDeSerializer.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
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
        /// The <see cref="XmiReadResult"/> holding the read <see cref="INamespace"/> and the roots of every
        /// resource read to resolve its external references
        /// </returns>
        XmiReadResult DeSerialize(Uri fileLocation);

        /// <summary>
        /// Deserializes the XMI file to a read <see cref="INamespace"/>, tracking element origins
        /// </summary>
        /// <param name="fileLocation">
        /// the <see cref="Uri"/> that locates the XMI file
        /// </param>
        /// <param name="elementOriginMap">
        /// The <see cref="IXmiElementOriginMap"/> to populate with element-to-file associations
        /// </param>
        /// <exception cref="ArgumentNullException">If the <see cref="Uri"/> or <see cref="IXmiElementOriginMap"/> is null</exception>
        /// <exception cref="FileNotFoundException">If the <see cref="Uri"/> does not locate an existing file</exception>
        /// <returns>
        /// The <see cref="XmiReadResult"/> holding the read <see cref="INamespace"/> and the roots of every
        /// resource read to resolve its external references
        /// </returns>
        XmiReadResult DeSerialize(Uri fileLocation, IXmiElementOriginMap elementOriginMap);

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
        /// An awaitable <see cref="Task{TResult}"/> with the <see cref="XmiReadResult"/> holding the read
        /// <see cref="INamespace"/> and the roots of every resource read to resolve its external references
        /// </returns>
        Task<XmiReadResult> DeSerializeAsync(Uri fileLocation, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deserializes asynchronously the XMI file to a read <see cref="INamespace"/>, tracking element origins
        /// </summary>
        /// <param name="fileLocation">
        /// the <see cref="Uri"/> that locates the XMI file
        /// </param>
        /// <param name="elementOriginMap">
        /// The <see cref="IXmiElementOriginMap"/> to populate with element-to-file associations
        /// </param>
        /// <param name="cancellationToken">An optional <see cref="CancellationToken"/> to cancel the read process</param>
        /// <exception cref="ArgumentNullException">If the <see cref="Uri"/> or <see cref="IXmiElementOriginMap"/> is null</exception>
        /// <exception cref="FileNotFoundException">If the <see cref="Uri"/> does not locate an existing file</exception>
        /// <returns>
        /// An awaitable <see cref="Task{TResult}"/> with the <see cref="XmiReadResult"/> holding the read
        /// <see cref="INamespace"/> and the roots of every resource read to resolve its external references
        /// </returns>
        Task<XmiReadResult> DeSerializeAsync(Uri fileLocation, IXmiElementOriginMap elementOriginMap, CancellationToken cancellationToken = default);
    }
}
