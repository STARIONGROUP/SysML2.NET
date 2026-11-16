// ------------------------------------------------------------------------------------------------
// <copyright file="DeterministicGuid.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SySML2.NET.REST.Tests.Helpers
{
    using System;
    using System.Security.Cryptography;
    using System.Text;

    /// <summary>
    /// Creates name-based (version 5, SHA-1) UUIDs as specified by RFC 9562, so that the elements of the scenario
    /// have the same ids in every run
    /// </summary>
    public static class DeterministicGuid
    {
        /// <summary>
        /// The namespace UUID of the scenario's names
        /// </summary>
        private static readonly Guid ScenarioNamespace = new("4b0f3f6e-2a59-4b0a-9a51-407407407407");

        /// <summary>
        /// Creates the UUID of a name
        /// </summary>
        /// <param name="name">
        /// The name, e.g. <c>root/CoverageModel</c>
        /// </param>
        /// <returns>
        /// The version 5 UUID of <paramref name="name"/> in the scenario namespace
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown when <paramref name="name"/> is null or whitespace
        /// </exception>
        public static Guid Create(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("The name may not be null or whitespace", nameof(name));
            }

            var namespaceBytes = ScenarioNamespace.ToByteArray(true);
            var nameBytes = Encoding.UTF8.GetBytes(name);

            var input = new byte[namespaceBytes.Length + nameBytes.Length];
            namespaceBytes.CopyTo(input, 0);
            nameBytes.CopyTo(input, namespaceBytes.Length);

            var hash = SHA1.HashData(input);
            hash[6] = (byte)((hash[6] & 0x0F) | 0x50);
            hash[8] = (byte)((hash[8] & 0x3F) | 0x80);

            return new Guid(hash.AsSpan(0, 16), true);
        }
    }
}
