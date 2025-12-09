// -------------------------------------------------------------------------------------------------
// <copyright file="PropertyHelper.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.CodeGenerator.HandleBarHelpers
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;

    using HandlebarsDotNet;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using uml4net.SimpleClassifiers;
    using uml4net.Extensions;
    using uml4net.Classification;
    using uml4net.StructuredClassifiers;

    /// <summary>
    /// A handlebars block helper for the <see cref="IProperty"/> interface
    /// </summary>
     public static class PropertyHelper
    {
        /// <summary>
        /// Registers the <see cref="PropertyHelper"/>
        /// </summary>
        /// <param name="handlebars">
        /// The <see cref="IHandlebars"/> context with which the helper needs to be registered
        /// </param>
        public static void RegisterPropertyHelper(this IHandlebars handlebars)
        {
            handlebars.RegisterHelper("Property.WriteForDTOInterface", (writer, context, _) =>
            {
                if (!(context.Value is IProperty property))
                {
                    throw new ArgumentException("supposed to be IProperty");
                }

                var sb = new StringBuilder();

                if (property.Type is IDataType)
                {
                    if (property.QueryIsEnumerable())
                    {
                        sb.Append($"List<{property.QueryCSharpTypeName()}>");
                        sb.Append(" ");
                    }
                    else
                    {
                        sb.Append($"{property.QueryCSharpTypeName()}");
                        if (property.Lower == 0)
                        {
                            sb.Append("?");
                        }
                        sb.Append(" ");
                    }
                }
                else
                {
                    if (property.QueryIsEnumerable())
                    {
                        sb.Append("List<Guid>");
                        sb.Append(" ");
                    }
                    else
                    {
                        sb.Append("Guid");

                        if (property.Lower == 0)
                        {
                            sb.Append("?");
                        }

                        sb.Append(" ");
                    }
                }

                var propertyName = property.Name.CapitalizeFirstLetter();

                if (property.IsReadOnly || property.IsDerived || property.IsDerivedUnion)
                {
                    propertyName = $"{propertyName}";
                }

                sb.Append(propertyName);
                sb.Append(" ");

                if (property.IsReadOnly || property.IsDerived || property.IsDerivedUnion)
                {
                    sb.Append("{ get; }");
                }
                else
                {
                    sb.Append("{ get; set; }");
                }

                writer.WriteSafeString(sb + Environment.NewLine);
            });

            handlebars.RegisterHelper("Property.WriteForDTOClass", (writer, _, parameters) =>
            {
                if (parameters.Length != 2)
                {
                    throw new HandlebarsException("{{#Property.WriteForDTOClass}} helper must have exactly two arguments");
                }

                var property = parameters[0] as IProperty;
                var @class = parameters[1] as IClass;

                var sb = new StringBuilder();

                sb.Append(property.Visibility.ToString().ToLower(CultureInfo.InvariantCulture));
                sb.Append(" ");

                if (property.Type is IDataType)
                {
                    if (property.QueryIsEnumerable())
                    {
                        sb.Append($"List<{property.QueryCSharpTypeName()}>");
                        sb.Append(" ");
                    }
                    else
                    {
                        sb.Append($"{property.QueryCSharpTypeName()}");
                        if (property.Lower == 0)
                        {
                            sb.Append("?");
                        }
                        sb.Append(" ");
                    }
                }
                else
                {
                    if (property.QueryIsEnumerable())
                    {
                        sb.Append("List<Guid>");
                        sb.Append(" ");
                    }
                    else
                    {
                        sb.Append("Guid");

                        if (property.Lower == 0)
                        {
                            sb.Append("?");
                        }

                        sb.Append(" ");
                    }
                }

                var owner = property.Owner as IClass;

                var propertyName = property.Name.CapitalizeFirstLetter();

                if (property.IsReadOnly || property.IsDerived || property.IsDerivedUnion)
                {
                    propertyName = $"{propertyName}";
                }

                sb.Append(propertyName);
                sb.Append(" ");

                 if (property.IsReadOnly || property.IsDerived || property.IsDerivedUnion)
                {
                    sb.Append("{ get; internal set; }");
                }
                else
                {
                    sb.Append("{ get; set; }");

                    if (property.QueryIsEnumerable())
                    {
                        sb.Append(" = [];");
                    }
                }

                writer.WriteSafeString(sb + Environment.NewLine);
            });
        }
    }
}