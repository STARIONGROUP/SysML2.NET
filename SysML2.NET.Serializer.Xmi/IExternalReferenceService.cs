// ------------------------------------------------------------------------------------------------
// <copyright file="IExternalReferenceService.cs" company="Starion Group S.A.">
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
