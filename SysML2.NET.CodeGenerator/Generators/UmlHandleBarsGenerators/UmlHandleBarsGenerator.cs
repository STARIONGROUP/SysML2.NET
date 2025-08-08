﻿// -------------------------------------------------------------------------------------------------
// <copyright file="UmlHandleBarsGenerator.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.CodeGenerator.Generators.UmlHandleBarsGenerators
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using uml4net.Extensions;
    using uml4net.SimpleClassifiers;
    using uml4net.StructuredClassifiers;
    using uml4net.xmi.Readers;

    /// <summary>
    /// Abstract super class from which all uml based <see cref="HandlebarsDotNet"/> generators
    /// need to derive
    /// </summary>
    public abstract class UmlHandleBarsGenerator : HandleBarsGenerator
    {
        /// <summary>
        /// Generates code specific to the concrete implementation
        /// </summary>
        /// <param name="xmiReaderResult">
        /// the <see cref="XmiReaderResult"/> that contains the UML model to generate from
        /// </param>
        /// <param name="outputDirectory">
        /// The target <see cref="DirectoryInfo"/>
        /// </param>
        /// <returns>
        /// an awaitable <see cref="Task"/>
        /// </returns>
        public abstract Task GenerateAsync(XmiReaderResult xmiReaderResult, DirectoryInfo outputDirectory);

        /// <summary>
        /// Creates a <see cref="HandlebarsPayload"/> based on the provided root <see cref="XmiReaderResult"/>
        /// </summary>
        /// <param name="xmiReaderResult">
        /// the subject <see cref="XmiReaderResult"/>
        /// </param>
        /// <returns>
        /// an instance of <see cref="HandlebarsPayload"/>
        /// </returns>
        protected static HandlebarsPayload CreateHandlebarsPayload(XmiReaderResult xmiReaderResult)
        {
            if (xmiReaderResult == null)
            {
                throw new ArgumentNullException(nameof(xmiReaderResult));
            }

            var enumerations = new List<IEnumeration>();
            var primitiveTypes = new List<IPrimitiveType>();
            var dataTypes = new List<IDataType>();
            var classes = new List<IClass>();
            var interfaces = new List<IInterface>();

            foreach (var package in xmiReaderResult.Packages)
            {
                var containedPackages = package.QueryPackages();

                foreach (var containedPackage in containedPackages)
                {
                    enumerations.AddRange(containedPackage.PackagedElement.OfType<IEnumeration>());

                    primitiveTypes.AddRange(containedPackage.PackagedElement.OfType<IPrimitiveType>());

                    dataTypes.AddRange(containedPackage.PackagedElement
                        .OfType<IDataType>()
                        .Where(x => x is not IEnumeration && x is not IPrimitiveType));

                    classes.AddRange(containedPackage.PackagedElement.OfType<IClass>());

                    interfaces.AddRange(containedPackage.PackagedElement.OfType<IInterface>());
                }
            }

            var orderedEnumerations = enumerations.OrderBy(x => x.Name);
            var orderedPrimitiveTypes = primitiveTypes.OrderBy(x => x.Name);
            var orderedDataTypes = dataTypes.OrderBy(x => x.Name);
            var orderedClasses = classes.OrderBy(x => x.Name);
            var orderedInterfaces = interfaces.OrderBy(x => x.Name);

            var payload = new HandlebarsPayload(xmiReaderResult.Root, xmiReaderResult.Packages, orderedEnumerations, orderedPrimitiveTypes, orderedDataTypes, orderedClasses, orderedInterfaces);

            return payload;
        }
    }
}