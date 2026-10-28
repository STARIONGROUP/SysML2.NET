// ------------------------------------------------------------------------------------------------
// <copyright file="NamedElementExtensions.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
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
