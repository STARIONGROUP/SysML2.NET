// ------------------------------------------------------------------------------------------------
// <copyright file="SqlSchemaScripts.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Dal
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Provides the PostgreSQL schema scripts embedded in the SysML2.NET.Dal assembly.
    /// </summary>
    public static class SqlSchemaScripts
    {
        /// <summary>
        /// The manifest resource name prefix shared by every embedded schema script.
        /// </summary>
        private const string ResourcePrefix = "SysML2.NET.Dal.AutoGenSql.";

        /// <summary>
        /// Reads every embedded schema script, ordered by name so they can be applied in sequence.
        /// </summary>
        /// <returns>The scripts as (file name, SQL text) pairs, ordered ordinally by file name.</returns>
        public static IReadOnlyList<(string Name, string Sql)> GetAll()
        {
            var assembly = typeof(SqlSchemaScripts).Assembly;

            return assembly.GetManifestResourceNames()
                .Where(resourceName => resourceName.StartsWith(ResourcePrefix, StringComparison.Ordinal))
                .OrderBy(resourceName => resourceName, StringComparer.Ordinal)
                .Select(resourceName => (resourceName[ResourcePrefix.Length..], ReadResource(assembly, resourceName)))
                .ToList();
        }

        /// <summary>
        /// Reads the text of a manifest resource.
        /// </summary>
        /// <param name="assembly">The assembly that embeds the resource.</param>
        /// <param name="resourceName">The manifest resource name.</param>
        /// <returns>The resource text with LF line endings and without any byte-order mark.</returns>
        private static string ReadResource(Assembly assembly, string resourceName)
        {
            using var stream = assembly.GetManifestResourceStream(resourceName);
            using var reader = new StreamReader(stream);

            return reader.ReadToEnd().Replace("\r\n", "\n");
        }
    }
}
