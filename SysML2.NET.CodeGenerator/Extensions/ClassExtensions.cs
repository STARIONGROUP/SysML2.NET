// ------------------------------------------------------------------------------------------------
// <copyright file="ClassExtensions.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.CodeGenerator.Extensions
{
    using System.Linq;

    using uml4net.Classification;
    using uml4net.Extensions;
    using uml4net.StructuredClassifiers;

    /// <summary>
    /// Extension methods for <see cref="IClass" /> interface
    /// </summary>
    public static class ClassExtensions
    {
        /// <summary>
        /// Counts and returns to amount of non derived properties
        /// </summary>
        /// <param name="class">
        /// The subject <see cref="IClass" />
        /// </param>
        /// <returns>
        /// the amount of non derived properties
        /// </returns>
        public static int CountAllNonDerivedProperties(this IClass @class)
        {
            return @class.QueryAllNonDerivedProperties().Count;
        }

        /// <summary>
        /// Counts and returns to amount of non derived properties
        /// </summary>
        /// <param name="class">
        /// The subject <see cref="IClass" />
        /// </param>
        /// <returns>
        /// the amount of non derived properties
        /// </returns>
        public static int CountAllNonDerivedNonRedefinedProperties(this IClass @class)
        {
            return @class.QueryAllNonDerivedNonRedefinedProperties().Count;
        }

        /// <summary>
        /// Query the name of the internal interface to implement for an <see cref="IClass" />
        /// </summary>
        /// <param name="umlClass">The <see cref="IClass" /></param>
        /// <returns>The name of the internal interface to implement, if any</returns>
        public static string QueryInternalInterfaceName(this IClass umlClass)
        {
            var classifiers = umlClass.QueryAllGeneralClassifiers();

            if (classifiers.Any(x => x.Name == "Relationship"))
            {
                return "IContainedRelationship";
            }

            return classifiers.Any(x => x.Name == "Element") ? "IContainedElement" : string.Empty;
        }

        /// <summary>
        /// Asserts that an <see cref="IClass" /> is a subclass of another
        /// </summary>
        /// <param name="umlClass">The <see cref="IClass" /> to check</param>
        /// <param name="potentialSuperClass">
        /// The <see cref="IClass" /> that is potentially a super class of
        /// <paramref name="umlClass" />
        /// </param>
        /// <returns>
        /// True if the <paramref name="potentialSuperClass" /> is a super class of the <paramref name="umlClass" />
        /// </returns>
        public static bool QueryIsSubclassOf(this IClass umlClass, IClass potentialSuperClass)
        {
            return umlClass.QueryAllGeneralClassifiers().Contains(potentialSuperClass);
        }
    }
}
