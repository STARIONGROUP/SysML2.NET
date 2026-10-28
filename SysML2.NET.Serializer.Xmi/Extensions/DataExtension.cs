// ------------------------------------------------------------------------------------------------
// <copyright file="DataExtension.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Serializer.Xmi.Extensions
{
    using System;

    using SysML2.NET.Common;

    /// <summary>
    /// Extension class for the <see cref="IData"/> interface
    /// </summary>
    public static class DataExtension
    {
        /// <summary>
        /// Verify that the current <see cref="IData"/> can be target via an Id reference or not
        /// </summary>
        /// <param name="data">The <see cref="IData"/> that needs to be verified </param>
        /// <param name="elementOriginMap">The provided <see cref="IXmiElementOriginMap"/> that should be used to verify the registration of the <see cref="IData"/></param>
        /// <param name="currentFileUri">The <see cref="Uri"/> that locates an XMI file</param>
        /// <returns><c>true</c> if the <see cref="IData"/> can be referenced via identifier, <c>false</c> otherwise</returns>
        public static bool QueryIsValidIdRef(this IData data, IXmiElementOriginMap elementOriginMap, Uri currentFileUri)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            if (elementOriginMap == null || currentFileUri == null)
            {
                return true;
            }

            var sourceFile = elementOriginMap.GetSourceFile(data.Id);
            return sourceFile == null || sourceFile == currentFileUri;
        }
    }
}
