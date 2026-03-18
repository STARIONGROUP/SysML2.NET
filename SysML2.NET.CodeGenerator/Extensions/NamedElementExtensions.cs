// -------------------------------------------------------------------------------------------------
// <copyright file="NamedElementExtensions.cs" company="Starion Group S.A.">
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

    using uml4net.CommonStructure;
    using uml4net.SimpleClassifiers;

    /// <summary>
    /// Extension class for <see cref="INamedElement"/>
    /// </summary>
    public static class NamedElementExtensions
    {
        /// <summary>
        /// Query the CSharp namespace of a <see cref="INamedElement"/>
        /// </summary>
        /// <param name="namedElement">The <see cref="INamedElement"/></param>
        /// <returns>The CSharp compliant namespace</returns>
        public static string QueryNamespace(this INamedElement namedElement)
        {
            var qualifiedNameSpaces = namedElement.QualifiedName.Split("::");
            var namespaces = qualifiedNameSpaces.Skip(1).Take(qualifiedNameSpaces.Length - 2);
            return string.Join('.', namespaces);
        }

        /// <summary>
        /// Query the fully qualified type name (Namespace + Type name). 
        /// </summary>
        /// <param name="namedElement">The specific <see cref="INamedElement"/>that should have the fully qualified type name computed</param>
        /// <param name="namespacePart">A specific namespace part (POCO/DTO distinction)</param>
        /// <param name="targetInterface">Asserts if the type should be the interface name or not</param>
        /// <returns>The fully qualified type name</returns>
        public static string QueryFullyQualifiedTypeName(this INamedElement namedElement, string namespacePart = "POCO", bool targetInterface = true)
        {
            ArgumentNullException.ThrowIfNull(namedElement);
            ArgumentException.ThrowIfNullOrWhiteSpace(namespacePart);
            
            var typeName = "SysML2.NET.Core.";

            if (namedElement is not IEnumeration)
            {
                typeName += $"{namespacePart}.";
            }

            typeName += namedElement.QueryNamespace();
            typeName += ".";
                
            if (namedElement is not IEnumeration && targetInterface)
            {
                typeName += "I";
            }
                
            typeName += namedElement.Name;
            return typeName;
        }
    }
}
