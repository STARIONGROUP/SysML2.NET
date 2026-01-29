// -------------------------------------------------------------------------------------------------
// <copyright file="ClassExtensions.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.CodeGenerator.Extensions
{
    using System;
    using System.Linq;

    using SysML2.NET.CodeGenerator.Enumeration;

    using uml4net.Extensions;
    using uml4net.StructuredClassifiers;

    /// <summary>
    /// Extension methods for the <see cref="IClass"/>
    /// </summary>
    public static class ClassExtensions
    {
        /// <summary>
        /// Asserts that the provided <see cref="IClass"/> implements from the Data interface.
        /// </summary>
        /// <param name="umlClass">The <see cref="IClass"/> to check</param>
        /// <returns>The result of the assertion</returns>
        public static bool QueryDoesImplementsDataInterface(this IClass umlClass)
        {
            return umlClass.QueryInterfaces().Any(x => x.Name == "Data");
        }

        /// <summary>
        /// Asserts that the provided <see cref="IClass" /> can be identified by an identifier property
        /// </summary>
        /// <param name="umlClass">The <see cref="IClass"/> to check</param>
        /// <param name="modelKind">The specific <see cref="ModelKind"/></param>
        /// <returns>The result of the assertion</returns>
        public static bool QueryIsIdentified(this IClass umlClass, ModelKind modelKind)
        {
            if (modelKind == ModelKind.Core)
            {
                return true;
            }

            if (umlClass.QueryDoesImplementsDataInterface())
            {
                return true;
            }

            var allProperties = umlClass.QueryAllProperties();
            return allProperties.Any(x => string.Equals(x.Name, "id", StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
