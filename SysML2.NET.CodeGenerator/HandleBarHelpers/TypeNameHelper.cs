// -------------------------------------------------------------------------------------------------
// <copyright file="TypeNameHelper.cs" company="RHEA System S.A.">
// 
//   Copyright 2022 RHEA System S.A.
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

namespace SysML2.NET.CodeGenerator.HandleBarHelpers
{
    using System;
    using System.Collections.Generic;

    using ECoreNetto;

    using HandlebarsDotNet;
    
    /// <summary>
    /// A block helper that prints the name of the type of a <see cref="EStructuralFeature"/>
    /// </summary>
    public static class TypeNameHelper
    {
        /// <summary>
        /// A mapping of the known SysML value types to C# types
        /// </summary>
        public static Dictionary<string, string> TypeMapping = new Dictionary<string, string>
        {
            {"Boolean", "bool"},
            {"Integer", "int"},
            {"Real", "double"},
            {"UnlimitedNatural", "int"},
            {"String", "string"},
            {"EDouble", "double"}
        };

        /// <summary>
        /// Queries the type-name of the <see cref="EStructuralFeature"/>
        /// </summary>
        /// <param name="eStructuralFeature">
        /// The subject <see cref="EStructuralFeature"/>
        /// </param>
        /// <returns>
        /// the name of the type
        /// </returns>
        public static string QueryTypeName(EStructuralFeature eStructuralFeature)
        {
            var typeName = "";

            if (eStructuralFeature is EAttribute eAttribute)
            {
                if (eAttribute.EType is EEnum)
                {
                    typeName = eAttribute.EType.Name;
                }
                else if (!TypeMapping.TryGetValue(eAttribute.EType.Name, out typeName))
                {
                    throw new KeyNotFoundException($"the {eAttribute.Name}.{eAttribute.EType.Name} is not a registered Type");
                }
            }
            else if (eStructuralFeature is EReference eReference)
            {
                typeName = eReference.EType.Name;
            }

            return typeName;
        }

        /// <summary>
        /// Registers the <see cref="TypeNameHelper"/>
        /// </summary>
        /// <param name="handlebars">
        /// The <see cref="IHandlebars"/> context with which the helper needs to be registered
        /// </param>
        public static void RegisterTypeNameHelper(this IHandlebars handlebars)
        {
            handlebars.RegisterHelper("DTO.TypeName", (writer, context, parameters) =>
            {
                if (!(context.Value is EStructuralFeature eStructuralFeature))
                    throw new ArgumentException("supposed to be EStructuralFeature");

                var typeName = QueryTypeName(eStructuralFeature);

                var nullable = eStructuralFeature.LowerBound == 0;

                var enumerable = eStructuralFeature.UpperBound is -1 or > 1;

                if (eStructuralFeature is EAttribute)
                {
                    if (enumerable)
                    {
                        writer.WriteSafeString($"List<{typeName}>");
                    }
                    else if (typeName != "string" && nullable)
                    {
                        writer.WriteSafeString($"{typeName}?");
                    }
                    else
                    {
                        writer.WriteSafeString($"{typeName}");
                    }
                }
                else if (eStructuralFeature is EReference)
                {
                    if (enumerable)
                    {
                        writer.WriteSafeString($"List<Guid>");
                    }
                    else if (nullable)
                    {
                        writer.WriteSafeString($"Guid?");
                    }
                    else 
                    {
                        writer.WriteSafeString($"Guid");
                    }
                }
            });

            handlebars.RegisterHelper("POCO.TypeName", (writer, context, parameters) =>
            {
                if (!(context.Value is EStructuralFeature eStructuralFeature))
                    throw new ArgumentException("supposed to be EStructuralFeature");

                var typeName = QueryTypeName(eStructuralFeature);

                var nullable = eStructuralFeature.LowerBound == 0;

                var enumerable = eStructuralFeature.UpperBound is -1 or > 1;

                if (eStructuralFeature is EAttribute)
                {
                    if (enumerable)
                    {
                        writer.WriteSafeString($"List<{typeName}>");
                    }
                    else if (typeName != "string" && nullable)
                    {
                        writer.WriteSafeString($"{typeName}?");
                    }
                    else
                    {
                        writer.WriteSafeString($"{typeName}");
                    }
                }
                else if (eStructuralFeature is EReference)
                {
                    if (enumerable)
                    {
                        writer.WriteSafeString($"List<{typeName}>");
                    }
                    else
                    {
                        writer.WriteSafeString($"{typeName}");
                    }
                }
            });
        }
    }
}
