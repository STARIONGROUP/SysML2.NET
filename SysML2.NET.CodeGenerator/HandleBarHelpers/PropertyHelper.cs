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
    using System.Linq;
    using System.Text;

    using HandlebarsDotNet;

    using SysML2.NET.CodeGenerator.Extensions;

    using uml4net.SimpleClassifiers;
    using uml4net.Extensions;
    using uml4net.Classification;
    using uml4net.CommonStructure;
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
                if (context.Value is not IProperty property)
                {
                    throw new ArgumentException("supposed to be IProperty");
                }

                var sb = new StringBuilder();
                
                if (property.RedefinedProperty.Any(x => x.Name == property.Name))
                {
                    sb.Append("new ");
                }
                
                if (property.Type is IDataType)
                {
                    if (property.QueryIsEnumerable())
                    {
                        sb.Append($"List<{property.QueryCSharpTypeName()}>");
                        sb.Append(' ');
                    }
                    else
                    {
                        sb.Append($"{property.QueryCSharpTypeName()}");
                        
                        if (property.QueryIsNullableAndNotString())
                        {
                            sb.Append('?');
                        }
                        
                        sb.Append(' ');
                    }
                }
                else
                {
                    if (property.QueryIsEnumerable())
                    {
                        sb.Append("List<Guid>");
                        sb.Append(' ');
                    }
                    else
                    {
                        sb.Append("Guid");

                        if (property.QueryIsNullableAndNotString())
                        {
                            sb.Append('?');
                        }

                        sb.Append(' ');
                    }
                } 
                
                var propertyName = StringExtensions.CapitalizeFirstLetter(property.Name);

                if (property.IsDerived || property.IsDerivedUnion)
                {
                    propertyName = StringExtensions.LowerCaseFirstLetter(propertyName);
                }

                sb.Append(propertyName);
                sb.Append(' ');

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

                var property = (parameters[0] as IProperty)!;
                var generatedClass = (parameters[1] as IClass)!;

                var sb = new StringBuilder();
                var propertyName = StringExtensions.CapitalizeFirstLetter(property.Name);
                var isRedefinedByProperty = property.TryQueryRedefinedByProperty(generatedClass, out var redefiningProperty);
                
                if (!isRedefinedByProperty)
                {
                    sb.Append(property.Visibility.ToString().ToLower(CultureInfo.InvariantCulture));
                    sb.Append(' ');    
                }

                if (property.Type is IDataType)
                {
                    if (property.QueryIsEnumerable())
                    {
                        sb.Append($"List<{property.QueryCSharpTypeName()}>");
                        sb.Append(' ');
                    }
                    else
                    {
                        sb.Append($"{property.QueryCSharpTypeName()}");
                        
                        if (property.QueryIsNullableAndNotString())
                        {
                            sb.Append('?');
                        }
                        
                        sb.Append(' ');
                    }
                }
                else
                {
                    if (property.QueryIsEnumerable())
                    {
                        sb.Append("List<Guid>");
                        sb.Append(' ');
                    }
                    else
                    {
                        sb.Append("Guid");

                        if (property.QueryIsNullableAndNotString())
                        {
                            sb.Append('?');
                        }

                        sb.Append(' ');
                    }
                }

                if (property.IsDerived || property.IsDerivedUnion)
                {
                    propertyName = StringExtensions.LowerCaseFirstLetter(propertyName);
                }

                if (isRedefinedByProperty)
                {
                    var owner = (INamedElement)property.Owner;
                    propertyName = $"I{owner.Name}.{propertyName}";

                    var ownerNamespace = owner.QueryNamespace();

                    if (ownerNamespace != generatedClass.QueryNamespace())
                    {
                        propertyName = $"{ownerNamespace}.{propertyName}";
                    }
                }

                sb.Append(propertyName);
                sb.Append(' ');

                 if (property.IsReadOnly || property.IsDerived || property.IsDerivedUnion)
                {
                    if (isRedefinedByProperty)
                    {
                        var owningClass = redefiningProperty.Owner as IClass;
                        sb.Append($"=> throw new InvalidOperationException(\"Redefined by property I{owningClass?.Name}.{StringExtensions.CapitalizeFirstLetter(redefiningProperty.Name)}\");");
                    }
                    else
                    {
                        sb.Append("{ get; internal set; }");
                    }
                }
                else
                {
                    if (isRedefinedByProperty)
                    {
                        var owningClass = redefiningProperty.Owner as IClass;

                        sb.AppendLine("{");
                        sb.AppendLine($"    get => throw new InvalidOperationException(\"Redefined by property I{owningClass?.Name}.{StringExtensions.CapitalizeFirstLetter(redefiningProperty.Name)}\");");
                        sb.AppendLine($"    set => throw new InvalidOperationException(\"Redefined by property I{owningClass?.Name}.{StringExtensions.CapitalizeFirstLetter(redefiningProperty.Name)}\");");
                        sb.Append('}');
                    }
                    else
                    {
                        sb.Append("{ get; set; }");

                        if (property.QueryIsEnumerable())
                        {
                            sb.Append(" = [];");
                        }
                        else if (property.QueryIsDefaultValueDifferentThanDefault())
                        {
                            if (property.QueryIsString())
                            {
                                sb.Append($" = \"{property.QueryDefaultValueAsString()}\";");
                            }
                            else
                            {
                                sb.Append($" = {property.QueryDefaultValueAsString()};");
                            }
                        }
                    }
                }

                writer.WriteSafeString(sb + Environment.NewLine);
            });
            
            handlebars.RegisterHelper("Property.WriteForPOCOInterface", (writer, context, _) =>
            {
                if (context.Value is not IProperty property)
                {
                    throw new ArgumentException("The #Property.WriteForPOCOInterface supposed to be IProperty");
                }

                var sb = new StringBuilder();
                var typeName = property.QueryCSharpTypeName();
                
                if (property.RedefinedProperty.Any(x => x.Name == property.Name))
                {
                    sb.Append("new ");
                }
                
                if (property.Type is not IDataType)
                {
                    typeName = $"I{typeName}";
                }

                if (property.QueryIsEnumerable())
                {
                    sb.Append($"List<{typeName}> ");
                }
                else
                {
                    if (property.QueryIsNullableAndNotString() && !property.QueryIsReferenceProperty())
                    {
                        sb.Append($"{typeName}? ");
                    }
                    else
                    { 
                        sb.Append($"{typeName} ");
                    }
                }
                
                var propertyName = StringExtensions.CapitalizeFirstLetter(property.Name);

                if (property.IsDerived || property.IsDerivedUnion)
                {
                    propertyName = StringExtensions.LowerCaseFirstLetter(propertyName);
                }

                sb.Append(propertyName);
                
                if (property.IsReadOnly || property.IsDerived || property.IsDerivedUnion)
                {
                    sb.Append(" { get; }");
                }
                else
                {
                    sb.Append(" { get; set; }");
                }

                writer.WriteSafeString(sb + Environment.NewLine);
            });
            
            handlebars.RegisterHelper("Property.WriteForPOCOClass", (writer, context, arguments) =>
            {
                if (context.Value is not IProperty property)
                {
                    throw new ArgumentException("The #Property.WriteForPOCOClass context supposed to be IProperty");
                }

                if (arguments.Length != 2)
                {
                    throw new ArgumentException("The #Property.WriteForPOCOClass supposed to be have 2 arguments IProperty");
                }

                if (arguments[1] is not IClass generatedClass)
                {
                    throw new ArgumentException("The #Property.WriteForPOCOClass supposed to be have an IClass as second argument");
                }

                var sb = new StringBuilder();
                var isRedefinedByProperty = property.TryQueryRedefinedByProperty(generatedClass, out var redefiningProperty);
                var typeName = property.QueryCSharpTypeName();

                var propertyName = StringExtensions.CapitalizeFirstLetter(property.Name);
                
                if (!isRedefinedByProperty)
                {
                    sb.Append(property.Visibility.ToString().ToLower(CultureInfo.InvariantCulture));
                    sb.Append(' ');    
                }
                
                if (property.Type is not IDataType)
                {
                    typeName = $"I{typeName}";
                }

                if (property.QueryIsEnumerable())
                {
                    sb.Append($"List<{typeName}> ");
                }
                else
                {
                    if (property.QueryIsNullableAndNotString()  && !property.QueryIsReferenceProperty())
                    {
                        sb.Append($"{typeName}? ");
                    }
                    else
                    { 
                        sb.Append($"{typeName} ");
                    }
                }
                
                if (property.IsDerived || property.IsDerivedUnion)
                {
                    propertyName = StringExtensions.LowerCaseFirstLetter(propertyName);
                }

                if (isRedefinedByProperty)
                {
                    var owner = (INamedElement)property.Owner;
                    propertyName = $"I{owner.Name}.{propertyName}";

                    var ownerNamespace = owner.QueryNamespace();

                    if (ownerNamespace != generatedClass.QueryNamespace())
                    {
                        propertyName = $"{ownerNamespace}.{propertyName}";
                    }
                }
                
                sb.Append(propertyName);

                if ( property.IsDerived || property.IsDerivedUnion)
                {
                    if (isRedefinedByProperty)
                    {
                        var owningClass = redefiningProperty.Owner as IClass;
                        sb.Append($"=> throw new InvalidOperationException(\"Redefined by property I{owningClass?.Name}.{StringExtensions.CapitalizeFirstLetter(redefiningProperty.Name)}\");");
                    }
                    else
                    {
                        sb.Append($"=> this.Compute{StringExtensions.CapitalizeFirstLetter(property.Name)}();");
                    }
                }
                else if (property.IsReadOnly)
                {
                    if (isRedefinedByProperty)
                    {
                        var owningClass = redefiningProperty.Owner as IClass;
                        sb.Append($"=> throw new InvalidOperationException(\"Redefined by property I{owningClass?.Name}.{StringExtensions.CapitalizeFirstLetter(redefiningProperty.Name)}\");");
                    }
                    else
                    {
                        sb.Append(" { get; }");
                    }
                }
                else
                {
                    if (isRedefinedByProperty)
                    {
                        var owningClass = redefiningProperty.Owner as IClass;

                        sb.AppendLine("{");
                        sb.AppendLine($"    get => throw new InvalidOperationException(\"Redefined by property I{owningClass?.Name}.{StringExtensions.CapitalizeFirstLetter(redefiningProperty.Name)}\");");
                        sb.AppendLine($"    set => throw new InvalidOperationException(\"Redefined by property I{owningClass?.Name}.{StringExtensions.CapitalizeFirstLetter(redefiningProperty.Name)}\");");
                        sb.Append('}');
                    }
                    else
                    {
                        sb.Append(" { get; set; }");

                        if (property.QueryIsEnumerable())
                        {
                            sb.Append(" = [];");
                        }
                    }
                }

                writer.WriteSafeString(sb + Environment.NewLine);
            });

            handlebars.RegisterHelper("Property.WriteTypeForExtendClass", (writer, context, _) =>
            {
                if (context.Value is not IProperty property)
                {
                    throw new ArgumentException("The #Property.WriteTypeForExtendClass context supposed to be IProperty");
                }

                if (!property.IsDerived && !property.IsDerivedUnion)
                {
                    throw new ArgumentException("The #Property.WriteTypeForExtendClass shall only be called on IsDerived or IsDerivedUnion properties");
                }

                var sb = new StringBuilder();
                var typeName = property.QueryCSharpTypeName();

                if (property.Type is not IDataType)
                {
                    typeName = $"I{typeName}";
                }

                if (property.QueryIsEnumerable())
                {
                    sb.Append($"List<{typeName}> ");
                }
                else
                {
                    sb.Append($"{typeName} ");
                }

                writer.WriteSafeString(sb);
            });

            handlebars.RegisterHelper("Property.IsPropertyRedefinedInClass", (context, arguments) =>
            {
                if (context.Value is not IProperty property)
                {
                    throw new ArgumentException("The #Property.IsPropertyRedefinedInClass context supposed to be IProperty");
                }

                if (arguments.Length != 2)
                {
                    throw new ArgumentException("The #Property.IsPropertyRedefinedInClass supposed to be have 2 arguments IProperty");
                }

                if (arguments[1] is not IClass generatedClass)
                {
                    throw new ArgumentException("The #Property.IsPropertyRedefinedInClass supposed to be have an IClass as second argument");
                }

                return property.TryQueryRedefinedByProperty(generatedClass, out _);
            });
            
            handlebars.RegisterHelper("Property.IsRedefinedOrRedifines", (context, arguments) =>
            {
                if (context.Value is not IProperty property)
                {
                    throw new ArgumentException("The #Property.IsRedefinedOrRedifines context supposed to be IProperty");
                }
                
                if (arguments.Length != 2)
                {
                    throw new ArgumentException("The #Property.IsRedefinedOrRedifines supposed to be have 2 arguments IProperty");
                }

                if (arguments[1] is not IClass generatedClass)
                {
                    throw new ArgumentException("The #Property.IsRedefinedOrRedifines supposed to be have an IClass as second argument");
                }

                var allProperties = generatedClass.QueryAllProperties();
                var propertiesWithSameName = allProperties.SkipWhile(x => x == property).Where(x => x.Name == property.Name);
                
                return property.RedefinedProperty.Any(x => propertiesWithSameName.Contains(x)) || propertiesWithSameName.Any(x => x.RedefinedProperty.Contains(property));
            });
        }
    }
}