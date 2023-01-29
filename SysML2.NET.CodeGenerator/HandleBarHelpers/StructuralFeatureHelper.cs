// -------------------------------------------------------------------------------------------------
// <copyright file="StructuralFeatureHelper.cs" company="RHEA System S.A.">
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
    using System.Linq;

    using ECoreNetto;

    using HandlebarsDotNet;

    using SysML2.NET.CodeGenerator.Extensions;

    /// <summary>
    /// A handlebars block helper
    /// </summary>
    public static class StructuralFeatureHelper
    {
        /// <summary>
        /// Registers the <see cref="StructuralFeatureHelper"/>
        /// </summary>
        /// <param name="handlebars">
        /// The <see cref="IHandlebars"/> context with which the helper needs to be registered
        /// </param>
        public static void RegisterStructuralFeatureHelper(this IHandlebars handlebars)
        {
            handlebars.RegisterHelper("StructuralFeature.QueryIsNullable", (context, arguments) =>
            {
                if (arguments.Length != 1)
                {
                    throw new HandlebarsException("{{#StructuralFeature.QueryIsNullable}} helper must have exactly one argument");
                }

                var eStructuralFeature = arguments.Single() as EStructuralFeature;

                return eStructuralFeature.QueryIsNullable();
            });

            handlebars.RegisterHelper("StructuralFeature.QueryIsScalar", (context, arguments) =>
            {
                if (arguments.Length != 1)
                {
                    throw new HandlebarsException("{{#StructuralFeature.QueryIsScalar}} helper must have exactly one argument");
                }

                var eStructuralFeature = arguments.Single() as EStructuralFeature;

                return eStructuralFeature.QueryIsScalar();
            });
            
            handlebars.RegisterHelper("StructuralFeature.QueryIsBool", (context, arguments) =>
            {
                if (arguments.Length != 1)
                {
                    throw new HandlebarsException("{{#StructuralFeature.QueryIsBool}} helper must have exactly one argument");
                }

                var eStructuralFeature = arguments.Single() as EStructuralFeature;

                return eStructuralFeature.QueryIsBool();
            });

            handlebars.RegisterHelper("StructuralFeature.QueryIsNumeric", (context, arguments) =>
            {
                if (arguments.Length != 1)
                {
                    throw new HandlebarsException("{{#StructuralFeature.QueryIsNumeric}} helper must have exactly one argument");
                }

                var eStructuralFeature = arguments.Single() as EStructuralFeature;

                return eStructuralFeature.QueryIsNumeric();
            });

            handlebars.RegisterHelper("StructuralFeature.QueryIsInteger", (context, arguments) =>
            {
                if (arguments.Length != 1)
                {
                    throw new HandlebarsException("{{#StructuralFeature.QueryIsInteger}} helper must have exactly one argument");
                }

                var eStructuralFeature = arguments.Single() as EStructuralFeature;

                return eStructuralFeature.QueryIsInt();
            });

            handlebars.RegisterHelper("StructuralFeature.QueryIsDouble", (context, arguments) =>
            {
                if (arguments.Length != 1)
                {
                    throw new HandlebarsException("{{#StructuralFeature.QueryIsDouble}} helper must have exactly one argument");
                }

                var eStructuralFeature = arguments.Single() as EStructuralFeature;

                return eStructuralFeature.QueryIsDouble();
            });

            handlebars.RegisterHelper("StructuralFeature.QueryIsString", (context, arguments) =>
            {
                if (arguments.Length != 1)
                {
                    throw new HandlebarsException("{{#StructuralFeature.QueryIsString}} helper must have exactly one argument");
                }

                var eStructuralFeature = arguments.Single() as EStructuralFeature;

                return eStructuralFeature.QueryIsString();
            });
            
            handlebars.RegisterHelper("StructuralFeature.DefaultValue", (writer, context, parameters) =>
            {
                if (!(context.Value is EStructuralFeature eStructuralFeature))
                    throw new ArgumentException("supposed to be EStructuralFeature");

                var defaultValue = eStructuralFeature.QueryDefaultValue();

                writer.WriteSafeString($"{defaultValue}");
            });

            handlebars.RegisterHelper("StructuralFeature.TypeName", (writer, context, parameters) =>
            {
	            if (!(context.Value is EStructuralFeature eStructuralFeature))
		            throw new ArgumentException("supposed to be EStructuralFeature");

	            var typeName = eStructuralFeature.QueryCSharpTypeName();

	            writer.WriteSafeString($"{typeName}");
            });
		}
    }
}
