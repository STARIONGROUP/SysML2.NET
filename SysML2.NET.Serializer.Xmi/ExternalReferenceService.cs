// -------------------------------------------------------------------------------------------------
// <copyright file="ExternalReferenceService.cs" company="Starion Group S.A.">
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
    public class ExternalReferenceService: IExternalReferenceService
    {
        /// <summary>
        /// Stores external references that have to be processed and the process state. The key is the absolute path to 
        /// </summary>
        private readonly Dictionary<string, bool> externalReferences = []; 

        /// <summary>
        /// Adds a reference to an external file that have to be processed
        /// </summary>
        /// <param name="currentLocation"></param>
        /// <param name="externalReference">The reference to the external file</param>
        public void AddExternalReferenceToProcess(Uri currentLocation, string externalReference)
        {
            var uri = new Uri(currentLocation, externalReference);

            if (!this.externalReferences.ContainsKey(uri.AbsolutePath))
            {
                this.externalReferences.Add(uri.AbsolutePath, false);
            }
        }
    }
}
