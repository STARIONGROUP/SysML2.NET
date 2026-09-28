// -------------------------------------------------------------------------------------------------
// <copyright file="HandleBarArguments.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
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

namespace SysML2.NET.CodeGenerator.OpenApiHandleBarHelpers
{
    using System;

    using HandlebarsDotNet;

    using Microsoft.OpenApi;

    using SysML2.NET.CodeGenerator.Extensions;

    /// <summary>
    /// Resolves the schema constructs that the property helpers are invoked with
    /// </summary>
    /// <remarks>
    /// The property helpers are invoked with the property name, the class name and the class schema, in that order.
    /// </remarks>
    internal static class HandleBarArguments
    {
        /// <param name="arguments">The helper arguments.</param>
        extension(Arguments arguments)
        {
            /// <summary>
            /// Queries a name from the helper arguments
            /// </summary>
            /// <param name="index">The index of the argument.</param>
            /// <returns>The name.</returns>
            internal string QuerySchemaName(int index)
            {
                if (arguments.Length < index + 1)
                {
                    throw new HandlebarsException("the helper is missing arguments");
                }

                return arguments[index] as string ?? throw new ArgumentException("supposed to be a name");
            }

            /// <summary>
            /// Queries the schema of the class that the helper arguments identify
            /// </summary>
            /// <returns>The <see cref="IOpenApiSchema"/> of the class.</returns>
            internal IOpenApiSchema QueryClassSchema()
            {
                if (arguments.Length != 3)
                {
                    throw new HandlebarsException("the helper must have exactly three arguments");
                }

                return arguments[2] as IOpenApiSchema ?? throw new ArgumentException("supposed to be IOpenApiSchema");
            }

            /// <summary>
            /// Queries the schema of the property that the helper arguments identify
            /// </summary>
            /// <returns>The <see cref="IOpenApiSchema"/> of the property.</returns>
            internal IOpenApiSchema QueryPropertySchema()
            {
                return arguments.QueryClassSchema().Properties[arguments.QuerySchemaName(0)];
            }

            /// <summary>
            /// Queries the C# type of the property that the helper arguments identify
            /// </summary>
            /// <returns>The C# type without its list wrapper or nullable annotation.</returns>
            internal string QueryCoreTypeName()
            {
                return arguments.QueryPropertySchema().QueryCoreTypeName(arguments.QuerySchemaName(1), arguments.QuerySchemaName(0));
            }

            /// <summary>
            /// Queries whether the class lists the property that the helper arguments identify as required
            /// </summary>
            /// <returns>True when the property is required.</returns>
            internal bool QueryIsRequired()
            {
                return arguments.QueryClassSchema().Required?.Contains(arguments.QuerySchemaName(0)) == true;
            }
        }

        /// <summary>
        /// Queries the name of the local variable that holds an instance of the class
        /// </summary>
        /// <param name="className">The name of the class.</param>
        /// <returns>The name of the local variable.</returns>
        internal static string QueryInstanceName(string className)
        {
            return char.ToLowerInvariant(className[0]) + className[1..];
        }

        /// <summary>
        /// Queries the name of the local variable that holds the value of a property
        /// </summary>
        /// <param name="propertyName">The name of the property as it appears in the schema.</param>
        /// <returns>The name of the local variable.</returns>
        internal static string QueryLocalName(string propertyName)
        {
            return QueryInstanceName(OpenApiSchemaExtensions.QueryPropertyName(propertyName));
        }
    }
}
