// -------------------------------------------------------------------------------------------------
// <copyright file="IExternalReferenceService.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Serializer.Xmi
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Resolves external references for XMI elements 
    /// </summary>
    public interface IExternalReferenceService
    {
        /// <summary>
        /// Adds a reference to an external file that have to be processed
        /// </summary>
        /// <param name="currentLocation"></param>
        /// <param name="externalReference">The reference to the external file</param>
        void AddExternalReferenceToProcess(Uri currentLocation, string externalReference);

        /// <summary>
        /// Gets <see cref="Uri"/> of external references that have to be processed
        /// </summary>
        /// <returns>A collection of <see cref="Uri"/> to process</returns>
        IReadOnlyCollection<Uri> GetExternalReferencesToProcess();
    }
}
