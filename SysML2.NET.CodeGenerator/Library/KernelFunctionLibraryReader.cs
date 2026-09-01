// -------------------------------------------------------------------------------------------------
// <copyright file="KernelFunctionLibraryReader.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.CodeGenerator.Library
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Xml.Linq;

    /// <summary>
    /// Reads the Kernel Function Library from its <c>.kermlx</c> XMI files and reports the
    /// <c>Function</c> names that each library package declares.
    /// </summary>
    public static class KernelFunctionLibraryReader
    {
        /// <summary>
        /// The search pattern matching the KerML XMI files of a library folder.
        /// </summary>
        private const string KerMlXmiSearchPattern = "*.kermlx";

        /// <summary>
        /// The unqualified <c>xsi:type</c> discriminator of a library package element.
        /// </summary>
        private const string LibraryPackageType = "LibraryPackage";

        /// <summary>
        /// The unqualified <c>xsi:type</c> discriminator of a function element.
        /// </summary>
        private const string FunctionType = "Function";

        /// <summary>
        /// The name of the attribute carrying the declared name of an element.
        /// </summary>
        private static readonly XName DeclaredNameAttribute = "declaredName";

        /// <summary>
        /// The name of the XML Schema instance <c>type</c> attribute that discriminates each element.
        /// </summary>
        private static readonly XName TypeAttribute = XName.Get("type", "http://www.w3.org/2001/XMLSchema-instance");

        /// <summary>
        /// Reads every library package in the supplied Kernel Function Library folder.
        /// </summary>
        /// <param name="libraryDirectoryPath">The path of the folder holding the <c>.kermlx</c> files</param>
        /// <returns>The declared function names, keyed by the declaring library package name</returns>
        /// <exception cref="ArgumentException">Thrown when <paramref name="libraryDirectoryPath"/> is not supplied.</exception>
        /// <exception cref="DirectoryNotFoundException">Thrown when the folder does not exist.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the folder carries no readable library package.</exception>
        public static IReadOnlyDictionary<string, IReadOnlyList<string>> Read(string libraryDirectoryPath)
        {
            if (string.IsNullOrWhiteSpace(libraryDirectoryPath))
            {
                throw new ArgumentException("The path of the Kernel Function Library folder is required", nameof(libraryDirectoryPath));
            }

            if (!Directory.Exists(libraryDirectoryPath))
            {
                throw new DirectoryNotFoundException($"The Kernel Function Library folder '{libraryDirectoryPath}' was not found");
            }

            var packages = new Dictionary<string, IReadOnlyList<string>>(StringComparer.Ordinal);

            foreach (var filePath in Directory.EnumerateFiles(libraryDirectoryPath, KerMlXmiSearchPattern))
            {
                var elements = XDocument.Load(filePath).Descendants().ToList();

                var libraryPackage = elements.FirstOrDefault(element => IsOfType(element, LibraryPackageType));

                if (libraryPackage?.Attribute(DeclaredNameAttribute) == null)
                {
                    continue;
                }

                packages[libraryPackage.Attribute(DeclaredNameAttribute).Value] = elements
                    .Where(element => IsOfType(element, FunctionType))
                    .Select(element => element.Attribute(DeclaredNameAttribute)?.Value)
                    .Where(declaredName => !string.IsNullOrWhiteSpace(declaredName))
                    .ToList();
            }

            return packages.Count == 0
                ? throw new InvalidOperationException($"The Kernel Function Library folder '{libraryDirectoryPath}' carries no readable library package")
                : packages;
        }

        /// <summary>
        /// Asserts that an element carries the supplied <c>xsi:type</c>, ignoring the namespace prefix.
        /// </summary>
        /// <param name="element">The element to test</param>
        /// <param name="unqualifiedType">The unqualified type discriminator to match</param>
        /// <returns><c>true</c> when the element is of the supplied type, <c>false</c> otherwise</returns>
        private static bool IsOfType(XElement element, string unqualifiedType)
        {
            var type = element.Attribute(TypeAttribute)?.Value;

            return type != null && type[(type.IndexOf(':') + 1)..] == unqualifiedType;
        }
    }
}
