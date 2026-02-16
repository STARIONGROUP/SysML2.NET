// -------------------------------------------------------------------------------------------------
// <copyright file="PropertyExtension.cs" company="Starion Group S.A.">
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

    using uml4net.Classification;
    using uml4net.Extensions;
    using uml4net.SimpleClassifiers;

    /// <summary>
    /// Extension class for the <see cref="IProperty"/> 
    /// </summary>
    public static class PropertyExtension
    {
        /// <summary>
        /// Asserts that the <see cref="IProperty"/> is an enum type with a default value provided
        /// </summary>
        /// <param name="property">The <see cref="IProperty"/> to assert</param>
        /// <returns>True if the <see cref="IProperty"/> have a default value for an enum</returns>
        public static bool QueryIsEnumPropertyWithDefaultValue(this IProperty property)
        {
            ArgumentNullException.ThrowIfNull(property);

            if (!property.QueryIsEnum())
            {
                return false;
            }
            
            var defaultValue = property.QueryDefaultValueAsString();

            if (defaultValue == "null")
            {
                return false;
            }
            
            var valueSpecification = property.DefaultValue.FirstOrDefault();

            if (valueSpecification is IInstanceValue instanceValue)
            {
                return instanceValue.Instance is IEnumerationLiteral;
            }

            return false;
        }

        /// <summary>
        /// Gets the name of the property.
        /// </summary>
        /// <param name="property">The <see cref="IProperty"/></param>
        /// <returns>The <see cref="IProperty.Name"/> with the first letter lowered case in case of derived property, in upper case otherwise</returns>
        public static string QueryPropertyNameBasedOnUmlProperties(this IProperty property)
        {
            ArgumentNullException.ThrowIfNull(property);

            return property.IsDerived || property.IsDerivedUnion ? StringExtensions.LowerCaseFirstLetter(property.Name) : StringExtensions.CapitalizeFirstLetter(property.Name);
        }

        /// <summary>
        /// Asserts that the property is one of the part of a Composite Aggregation that is not derived
        /// </summary>
        /// <param name="property">The property to check</param>
        /// <exception cref="ArgumentNullException">If the provided <see cref="IProperty"/> is null</exception>
        /// <returns>True if the property is composite and not derived or if the opposite property is Composite and not derived</returns>
        public static bool QueryPropertyIsPartOfNonDerivedCompositeAggregation(this IProperty property)
        {
            ArgumentNullException.ThrowIfNull(property);

            if (property.IsComposite && !property.IsDerived)
            {
                return true;
            }

            return property.Opposite is { IsComposite: true, IsDerived: false };
        }
    }
}
