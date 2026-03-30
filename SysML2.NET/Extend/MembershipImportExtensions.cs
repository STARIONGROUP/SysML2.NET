// -------------------------------------------------------------------------------------------------
// <copyright file="MembershipImportExtensions.cs" company="Starion Group S.A.">
//
//    Copyright (C) 2022-2026 Starion Group S.A.
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

namespace SysML2.NET.Core.POCO.Root.Namespaces
{
    using System;
    using System.Collections.Generic;

    using SysML2.NET.Core.Root.Namespaces;
    using SysML2.NET.Core.POCO.Root.Annotations;
    using SysML2.NET.Core.POCO.Root.Elements;

    /// <summary>
    /// The <see cref="MembershipImportExtensions"/> class provides extensions methods for
    /// the <see cref="IMembershipImport"/> interface
    /// </summary>
    internal static class MembershipImportExtensions
    {
        /// <summary>
        /// Returns at least the importedMembership. If isRecursive = true and the memberElement of the
        /// importedMembership is a Namespace, then Memberships are also recursively imported from that
        /// Namespace.
        /// </summary>
        /// <param name="membershipImportSubject">
        /// The subject <see cref="IMembershipImport"/>
        /// </param>
        /// <param name="excluded">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected IMembership
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static IMembership ComputeRedefinedImportedMembershipsOperation(this IMembershipImport membershipImportSubject, INamespace excluded)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }
    }
}
