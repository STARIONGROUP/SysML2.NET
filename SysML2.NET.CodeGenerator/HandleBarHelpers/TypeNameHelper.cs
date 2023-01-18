// -------------------------------------------------------------------------------------------------
// <copyright file="TypeNameHelper.cs" company="RHEA System S.A.">
// 
//   Copyright 2022-2023 RHEA System S.A.
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

    using ECoreNetto;
    using ECoreNetto.Extensions;

    using HandlebarsDotNet;

    using SysML2.NET.CodeGenerator.Extensions;

    /// <summary>
    /// A block helper that prints the name of the type of a <see cref="EStructuralFeature"/>
    /// </summary>
    public static class TypeNameHelper
    {
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

                var typeName = eStructuralFeature.QueryTypeName();

                var nullable = eStructuralFeature.QueryIsNullable();

                var enumerable = eStructuralFeature.QueryIsEnumerable();

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

                var typeName = eStructuralFeature.QueryTypeName();

                var nullable = eStructuralFeature.QueryIsNullable();

                var enumerable = eStructuralFeature.QueryIsEnumerable();

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
                    var @class = eStructuralFeature.QueryClass();
                    
                    if (@class.Abstract)
                    {
                        typeName = $"I{typeName}";
                    }

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
