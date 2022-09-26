// -------------------------------------------------------------------------------------------------
// <copyright file="StructuralFeatureHelper.cs" company="RHEA System S.A.">
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
            handlebars.RegisterHelper("StructuralFeature.QueryIsEnumerable", (context, arguments) =>
            {
                if (arguments.Length != 1)
                {
                    throw new HandlebarsException("{{#StructuralFeature.QueryIsEnumerable}} helper must have exactly one argument");
                }

                var eStructuralFeature = arguments.Single() as EStructuralFeature;

                return eStructuralFeature.QueryIsEnumerable();
            });

            handlebars.RegisterHelper("StructuralFeature.IsEnumerable", (output, options, context, arguments) =>
            {
                if (arguments.Length != 1)
                {
                    throw new HandlebarsException("{{#StructuralFeature.IsEnumerable}} helper must have exactly one argument");
                }

                var eStructuralFeature = arguments.Single() as EStructuralFeature;

                var isEnumerable = eStructuralFeature.QueryIsEnumerable();

                if (isEnumerable)
                {
                    options.Template(output, context);
                }
            });

            handlebars.RegisterHelper("StructuralFeature.QueryIsNullable", (context, arguments) =>
            {
                if (arguments.Length != 1)
                {
                    throw new HandlebarsException("{{#StructuralFeature.QueryIsNullable}} helper must have exactly one argument");
                }

                var eStructuralFeature = arguments.Single() as EStructuralFeature;

                return eStructuralFeature.QueryIsNullable();
            });

            handlebars.RegisterHelper("StructuralFeature.IsNullable", (output, options, context, arguments) =>
            {
                if (arguments.Length != 1)
                {
                    throw new HandlebarsException("{{#StructuralFeature.IsNullable}} helper must have exactly one argument");
                }

                var eStructuralFeature = arguments.Single() as EStructuralFeature;

                var isNullable = eStructuralFeature.QueryIsNullable();

                if (isNullable)
                {
                    options.Template(output, context);
                }
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

            handlebars.RegisterHelper("StructuralFeature.IsScalar", (output, options, context, arguments) =>
            {
                if (arguments.Length != 1)
                {
                    throw new HandlebarsException("{{#StructuralFeature.IsScalar}} helper must have exactly one argument");
                }

                var eStructuralFeature = arguments.Single() as EStructuralFeature;

                var isScalar = eStructuralFeature.QueryIsScalar();

                if (isScalar)
                {
                    options.Template(output, context);
                }
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

            handlebars.RegisterHelper("StructuralFeature.IsBool", (output, options, context, arguments) =>
            {
                if (arguments.Length != 1)
                {
                    throw new HandlebarsException("{{#StructuralFeature.IsBool}} helper must have exactly one argument");
                }

                var eStructuralFeature = arguments.Single() as EStructuralFeature;

                var isBool = eStructuralFeature.QueryIsBool();

                if (isBool)
                {
                    options.Template(output, context);
                }
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

            handlebars.RegisterHelper("StructuralFeature.IsNumeric", (output, options, context, arguments) =>
            {
                if (arguments.Length != 1)
                {
                    throw new HandlebarsException("{{#StructuralFeature.IsNumeric}} helper must have exactly one argument");
                }

                var eStructuralFeature = arguments.Single() as EStructuralFeature;

                var isNumeric = eStructuralFeature.QueryIsNumeric();

                if (isNumeric)
                {
                    options.Template(output, context);
                }
            });

            handlebars.RegisterHelper("StructuralFeature.QueryIsInteger", (context, arguments) =>
            {
                if (arguments.Length != 1)
                {
                    throw new HandlebarsException("{{#StructuralFeature.QueryIsInteger}} helper must have exactly one argument");
                }

                var eStructuralFeature = arguments.Single() as EStructuralFeature;

                return eStructuralFeature.QueryTypeName() == "int";
            });

            handlebars.RegisterHelper("StructuralFeature.QueryIsDouble", (context, arguments) =>
            {
                if (arguments.Length != 1)
                {
                    throw new HandlebarsException("{{#StructuralFeature.QueryIsDouble}} helper must have exactly one argument");
                }

                var eStructuralFeature = arguments.Single() as EStructuralFeature;

                return eStructuralFeature.QueryTypeName() == "double";
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

            handlebars.RegisterHelper("StructuralFeature.IsString", (output, options, context, arguments) =>
            {
                if (arguments.Length != 1)
                {
                    throw new HandlebarsException("{{#StructuralFeature.IsString}} helper must have exactly one argument");
                }

                var eStructuralFeature = arguments.Single() as EStructuralFeature;

                var isString = eStructuralFeature.QueryIsString();

                if (isString)
                {
                    options.Template(output, context);
                }
            });

            handlebars.RegisterHelper("StructuralFeature.QueryIsAttribute", (context, arguments) =>
            {
                if (arguments.Length != 1)
                {
                    throw new HandlebarsException("{{#StructuralFeature.QueryIsAttribute}} helper must have exactly one argument");
                }

                var eStructuralFeature = arguments.Single() as EStructuralFeature;

                return eStructuralFeature.QueryIsAttribute();
            });

            handlebars.RegisterHelper("StructuralFeature.IsAttribute", (output, options, context, arguments) =>
            {
                if (arguments.Length != 1)
                {
                    throw new HandlebarsException("{{#StructuralFeature.IsAttribute}} helper must have exactly one argument");
                }

                var eStructuralFeature = arguments.Single() as EStructuralFeature;

                var isAttribute = eStructuralFeature.QueryIsAttribute();

                if (isAttribute)
                {
                    options.Template(output, context);
                }
            });

            handlebars.RegisterHelper("StructuralFeature.QueryIsReference", (context, arguments) =>
            {
                if (arguments.Length != 1)
                {
                    throw new HandlebarsException("{{#StructuralFeature.QueryIsReference}} helper must have exactly one argument");
                }

                var eStructuralFeature = arguments.Single() as EStructuralFeature;

                return eStructuralFeature.QueryIsReference();
            });

            handlebars.RegisterHelper("StructuralFeature.IsReference", (output, options, context, arguments) =>
            {
                if (arguments.Length != 1)
                {
                    throw new HandlebarsException("{{#StructuralFeature.IsReference}} helper must have exactly one argument");
                }

                var eStructuralFeature = arguments.Single() as EStructuralFeature;

                var isReference = eStructuralFeature.QueryIsReference();

                if (isReference)
                {
                    options.Template(output, context);
                }
            });

            handlebars.RegisterHelper("StructuralFeature.QueryStructuralFeatureNameEqualsEnclosingType", (context, arguments) =>
            {
                if (arguments.Length != 2)
                {
                    throw new HandlebarsException("{{#StructuralFeature.QueryStructuralFeatureNameEqualsEnclosingType}} helper must have exactly two arguments");
                }
                
                var eStructuralFeature = arguments[0] as EStructuralFeature;
                var eClass = arguments[1] as EClass;
                
                return eStructuralFeature.QueryStructuralFeatureNameEqualsEnclosingType(eClass);
            });

            handlebars.RegisterHelper("StructuralFeature.NameEqualsEnclosingType", (output, options, context, arguments) =>
            {
                if (arguments.Length != 1)
                {
                    throw new HandlebarsException("{{#StructuralFeature.NameEqualsEnclosingType}} helper must have exactly two arguments");
                }

                var eStructuralFeature = arguments.Single() as EStructuralFeature;
                var eClass = arguments.Last() as EClass;

                var nameEqualsEnclosingType = eStructuralFeature.QueryStructuralFeatureNameEqualsEnclosingType(eClass);

                if (nameEqualsEnclosingType)
                {
                    options.Template(output, context);
                }
            });
            
            handlebars.RegisterHelper("StructuralFeature.QueryIsEnum", (context, arguments) =>
            {
                if (arguments.Length != 1)
                {
                    throw new HandlebarsException("{{#StructuralFeature.QueryIsEnum}} helper must have exactly one argument");
                }

                var eStructuralFeature = arguments.Single() as EStructuralFeature;

                return eStructuralFeature.QueryIsEnum();
            });

            handlebars.RegisterHelper("StructuralFeature.IsEnum", (output, options, context, arguments) =>
            {
                if (arguments.Length != 1)
                {
                    throw new HandlebarsException("{{#StructuralFeature.IsEnum}} helper must have exactly one argument");
                }

                var eStructuralFeature = arguments.Single() as EStructuralFeature;

                var isEnum = eStructuralFeature.QueryIsEnum();

                if (isEnum)
                {
                    options.Template(output, context);
                }
            });

            handlebars.RegisterHelper("StructuralFeature.QueryHasDefaultValue", (context, arguments) =>
            {
                if (arguments.Length != 1)
                {
                    throw new HandlebarsException("{{#StructuralFeature.QueryHasDefaultValue}} helper must have exactly one argument");
                }

                var eStructuralFeature = arguments.Single() as EStructuralFeature;

                return eStructuralFeature.QueryHasDefaultValue();
            });

            handlebars.RegisterHelper("StructuralFeature.DefaultValue", (writer, context, parameters) =>
            {
                if (!(context.Value is EStructuralFeature eStructuralFeature))
                    throw new ArgumentException("supposed to be EStructuralFeature");

                var defaultValue = eStructuralFeature.QueryDefaultValue();

                writer.WriteSafeString($"{defaultValue}");
            });
        }
    }
}
