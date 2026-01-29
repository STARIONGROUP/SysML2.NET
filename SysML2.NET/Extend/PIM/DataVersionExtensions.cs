// -------------------------------------------------------------------------------------------------
// <copyright file="DataVersionExtensions.cs" company="Starion Group S.A.">
//
//    Copyright (C) 2022-2025 Starion Group S.A.
//
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
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

namespace SysML2.NET.PIM.POCO.API_Model
{
    using System;
    using System.Collections.Generic;

    using SysML2.NET.Common;

    /// <summary>
    /// The <see cref="DataVersionExtensions"/> class provides extensions methods for
    /// the <see cref="IDataVersion"/> interface
    /// </summary>
    internal static class DataVersionExtensions
    {
        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="dataVersion">
        /// The subject <see cref="IDataVersion"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static IProject ComputeProject(this IDataVersion dataVersion)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

    }
}
