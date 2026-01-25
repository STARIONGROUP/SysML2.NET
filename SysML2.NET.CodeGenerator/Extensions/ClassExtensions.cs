// -------------------------------------------------------------------------------------------------
// <copyright file="ClassExtensions.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.CodeGenerator.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    using uml4net.Classification;
    using uml4net.Extensions;
    using uml4net.Packages;
    using uml4net.StructuredClassifiers;

    /// <summary>
    /// Extension methods for <see cref="IClass"/> interface
    /// </summary>
    public static class ClassExtensions
    {
        /// <summary>
        /// Queries all the properties that are implemented by the class directly or through superclasses
        /// or interface implementations EXCLUDING IsDerived
        /// </summary>
        /// <param name="class">
        /// The <see cref="IClass"/> from which to query the properties
        /// </param>
        /// <returns>
        /// A <see cref="ReadOnlyCollection{T}"/> of <see cref="IProperty"/>
        /// </returns>
        public static ReadOnlyCollection<IProperty> QueryAllNonDerivedProperties(this IClass @class)
        {
            var properties = @class.QueryAllProperties()
                .Where(x => !x.IsDerived);

            return properties.ToList().AsReadOnly();
        }

        /// <summary>
        /// Queries all the properties that are implemented by the class directly or through superclasses
        /// or interface implementations EXCLUDING IsDerived and EXCLUDING properties that have been
        /// redefined in the context of the current class
        /// </summary>
        /// <param name="class">
        /// The <see cref="IClass"/> from which to query the properties
        /// </param>
        /// <returns>
        /// A <see cref="ReadOnlyCollection{T}"/> of <see cref="IProperty"/>
        /// </returns>
        public static ReadOnlyCollection<IProperty> QueryAllNonDerivedNonRedefinedProperties(this IClass @class)
        {
            var properties = @class.QueryAllProperties()
                .Where(x => !x.IsDerived)
                .Where(x => !x.TryQueryRedefinedByProperty(@class, out _))
                .OrderBy(x => x.Name);

            return properties.ToList().AsReadOnly();
        }

        /// <summary>
        /// Counts and returns to amount of non derived properties
        /// </summary>
        /// <param name="class">
        /// The subject <see cref="IClass"/>
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
        /// The subject <see cref="IClass"/>
        /// </param>
        /// <returns>
        /// the amount of non derived properties
        /// </returns>
        public static int CountAllNonDerivedNonRedefinedProperties(this IClass @class)
        {
            return @class.QueryAllNonDerivedNonRedefinedProperties().Count;
        }
    }
}
