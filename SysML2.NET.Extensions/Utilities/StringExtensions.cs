// -------------------------------------------------------------------------------------------------
// <copyright file="StringExtensions.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Extensions.Utilities
{
    using System;

    /// <summary>
    /// The <see cref="StringExtensions"/> class provides extensions methods for <see cref="String"/>
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Normalizes a ZIP entry path by converting directory separators to forward
        /// slashes (<c>'/'</c>) and removing any leading <c>"./"</c> segment.
        /// </summary>
        /// <param name="path">
        /// The ZIP entry path to normalize.
        /// </param>
        /// <returns>
        /// A normalized path using forward slashes as directory separators and
        /// without a leading <c>"./"</c> segment, if present.
        /// </returns>
        public static string NormalizeZipPath(this string path)
        {
            if (path == null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            path = path.Replace('\\', '/');

            while (path.StartsWith("./", StringComparison.Ordinal))
            {
                path = path.Substring(2);
            }

            return path;
        }
    }
}
