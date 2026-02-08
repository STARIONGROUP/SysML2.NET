// -------------------------------------------------------------------------------------------------
// <copyright file="SysML2PropertyHelper.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.CodeGenerator.HandleBarHelpers
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Text;

    using HandlebarsDotNet;

    using uml4net.SimpleClassifiers;
    using uml4net.Extensions;
    using uml4net.Classification;
    using uml4net.StructuredClassifiers;

    /// <summary>
    /// A handlebars block helper for the <see cref="IProperty"/> interface
    /// </summary>
    public static class SysML2PropertyHelper
    {
        /// <summary>
        /// Registers the <see cref="SysML2PropertyHelper"/>
        /// </summary>
        /// <param name="handlebars">
        /// The <see cref="IHandlebars"/> context with which the helper needs to be registered
        /// </param>
        public static void RegisterPropertyHelper(this IHandlebars handlebars)
        {
            handlebars.RegisterHelper("Property.WriteFoDtoInterface", (writer, context, _) =>
            {
                if (!(context.Value is IProperty property))
                {
                    throw new ArgumentException("supposed to be IProperty");
                }

                var sb = new StringBuilder();
                sb.Append(property.Visibility.ToString().ToLower());
                sb.Append(' ');

                if (property.RedefinedProperty.Count > 0)
                {
                    sb.Append("new ");
                }

                if (property.QueryIsEnumerable())
                {
                    if (property.QueryIsReferenceProperty())
                    {
                        sb.Append($"List<Guid>");
                        sb.Append(' ');
                    }
                    else
                    {
                        sb.Append($"List<{property.QueryCSharpTypeName()}>");
                        sb.Append(' ');
                    }
                }
                else
                {
                    if (property.QueryIsReferenceProperty())
                    {
                        sb.Append($"Guid");
                        sb.Append(' ');
                    }
                    else
                    {
                        sb.Append($"{property.QueryCSharpTypeName()}");
                        sb.Append(' ');
                    }
                }

                var propertyName = property.Name.Trim().CapitalizeFirstLetter();

                sb.Append(propertyName);
                sb.Append(' ');

                if (property.IsDerived || property.IsDerivedUnion)
                {
                    sb.Append("{ get; }");
                }
                else
                {
                    sb.Append("{ get; set; }");
                }

                writer.WriteSafeString(sb + Environment.NewLine);
            });

            handlebars.RegisterHelper("Property.WriteFoDtoClass", (writer, _, parameters) =>
            {
                if (parameters.Length != 2)
                {
                    throw new HandlebarsException("{{#Property.WriteFoDtoClass}} helper must have exactly two arguments");
                }

                var property = parameters[0] as IProperty;
                var @class = parameters[1] as IClass;

                var sb = new StringBuilder();

                var isRedefinedByProperty = property.TryQueryRedefinedByProperty(@class, out var redefiningProperty);

                if (!isRedefinedByProperty)
                {
                    sb.Append(property.Visibility.ToString().ToLower(CultureInfo.InvariantCulture));
                    sb.Append(' ');
                }

                if (property.RedefinedProperty.Count > 0)
                {
                    sb.Append("new ");
                }

                if (property.QueryIsEnumerable())
                {
                    if (property.QueryIsReferenceProperty())
                    {
                        sb.Append($"List<Guid>");
                        sb.Append(' ');
                    }
                    else
                    {
                        sb.Append($"List<{property.QueryCSharpTypeName()}>");
                        sb.Append(' ');
                    }
                }
                else
                {
                    if (property.QueryIsReferenceProperty())
                    {
                        sb.Append($"Guid");
                        sb.Append(' ');
                    }
                    else
                    {
                        sb.Append($"{property.QueryCSharpTypeName()}");
                        sb.Append(' ');
                    }
                }

                var owner = property.Owner as IClass;

                if (isRedefinedByProperty)
                {
                    sb.Append($"I{owner.Name}.");
                }

                var propertyName = property.Name.Trim().CapitalizeFirstLetter();

                sb.Append(propertyName);
                sb.Append(' ');

                if (!isRedefinedByProperty)
                {
                    if (property.IsDerived || property.IsDerivedUnion)
                    {
                        sb.Append("{ get; }");
                    }
                    else
                    {
                        sb.Append("{ get; set; }");
                    }

                    if (property.QueryIsEnumerable())
                    {
                        sb.Append(" = [];");
                    }
                }
                else
                {
                    var owningClass = redefiningProperty.Owner as IClass;

                    sb.AppendLine("{");
                    sb.AppendLine($"    get => throw new InvalidOperationException(\"Redefined by property I{owningClass.Name}.{redefiningProperty.Name.CapitalizeFirstLetter()}\");");
                    sb.AppendLine($"    set => throw new InvalidOperationException(\"Redefined by property I{owningClass.Name}.{redefiningProperty.Name.CapitalizeFirstLetter()}\");");
                    sb.Append('}');
                }

                writer.WriteSafeString(sb + Environment.NewLine);
            });
        }
    }
}
