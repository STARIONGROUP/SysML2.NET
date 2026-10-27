// ------------------------------------------------------------------------------------------------
// <copyright file="ExternalReferenceService.cs" company="Starion Group S.A.">
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

    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Resolves external references for XMI elements 
    /// </summary>
    public class ExternalReferenceService : IExternalReferenceService
    {
        /// <summary>
        /// Stores external references that have to be processed 
        /// </summary>
        private readonly HashSet<Uri> externalReferences = [];

        /// <summary>
        /// Stores external references that have been processed or that are currently in a processing state 
        /// </summary>
        private readonly HashSet<Uri> alreadyProcessedReferences = [];

        /// <summary>
        /// Gets the injected <see cref="ILogger{TCategoryName}"/> that allow producing logs
        /// </summary>
        private readonly ILogger<ExternalReferenceService> logger;

        /// <summary>Initializes a new instance of the <see cref="T:System.Object"></see> class.</summary>
        public ExternalReferenceService(ILogger<ExternalReferenceService> logger)
        {
            this.logger = logger;
        }

        /// <summary>
        /// Adds a reference to an external file that have to be processed
        /// </summary>
        /// <param name="currentLocation"></param>
        /// <param name="externalReference">The reference to the external file</param>
        public void AddExternalReferenceToProcess(Uri currentLocation, string externalReference)
        {
            var relativeUri = Uri.UnescapeDataString(externalReference);
            var uri = new Uri(currentLocation, relativeUri);

            if (!this.alreadyProcessedReferences.Contains(uri))
            {
                this.externalReferences.Add(uri);
            }
            else
            {
                var fileInfo = new FileInfo(uri.LocalPath);

                if (this.logger.IsEnabled(LogLevel.Information))
                {
                    this.logger.LogInformation("File {FileName} already processed", fileInfo.Name);
                }
            }
        }

        /// <summary>
        /// Gets <see cref="Uri"/> of external references that have to be processed
        /// </summary>
        /// <returns>A collection of <see cref="Uri"/> to process</returns>
        public IReadOnlyCollection<Uri> GetExternalReferencesToProcess()
        {
            var toBeProcessed = new List<Uri>(this.externalReferences);

            foreach (var uri in toBeProcessed)
            {
                this.alreadyProcessedReferences.Add(uri);
            }

            this.externalReferences.Clear();
            return toBeProcessed;
        }
    }
}
