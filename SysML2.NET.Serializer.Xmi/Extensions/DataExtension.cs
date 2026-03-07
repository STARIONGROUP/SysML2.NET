// -------------------------------------------------------------------------------------------------
// <copyright file="DataExtension.cs" company="Starion Group S.A.">
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
