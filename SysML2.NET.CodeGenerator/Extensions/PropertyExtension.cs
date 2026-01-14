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
        /// Asserts that the current <see cref="IProperty" /> is C# nullable and not a string primitive 
        /// </summary>
        /// <param name="property">The <see cref="IProperty"/> to assert</param>
        /// <returns>True is the <see cref="IProperty"/> is C# nullable and not a string</returns>
        public static bool QueryIsNullableAndNotString(this IProperty property)
        {
            return property.QueryIsNullable() && property.QueryCSharpTypeName() != "string";
        }

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
    }
}
