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
                var classContext = (parameters[1] as IClass)!;

                var sb = new StringBuilder();
                var propertyName = StringExtensions.CapitalizeFirstLetter(property.Name);
                var isRedefinedPropertyInContext = property.TryQueryRedefinedByProperty(classContext, out var redefiningProperty);

                if (!isRedefinedPropertyInContext)
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

                if (isRedefinedPropertyInContext)
                {
                    var owner = (INamedElement)property.Owner;
                    propertyName = $"I{owner.Name}.{propertyName}";

                    var ownerNamespace = owner.QueryNamespace();

                    if (ownerNamespace != classContext.QueryNamespace())
                    {
                        propertyName = $"{ownerNamespace}.{propertyName}";
                    }
                }

                sb.Append(propertyName);
                sb.Append(' ');

                if (property.IsReadOnly || property.IsDerived || property.IsDerivedUnion)
                {
                    if (isRedefinedPropertyInContext)
                    {
                        sb.Append($"=> {GetRedefinedPropertyGetterImplementationForDto(property, redefiningProperty, classContext)}");
                    }
                    else
                    {
                        sb.Append("{ get; internal set; }");

                        if (property.QueryIsEnumerable())
                        {
                            sb.Append(" = [];");
                        }
                    }
                }
                else
                {
                    if (isRedefinedPropertyInContext)
                    {
                        sb.AppendLine("{");
                        sb.AppendLine($"\tget => {GetRedefinedPropertyGetterImplementationForDto(property, redefiningProperty, classContext)}");
                        var setterImplementation = GetRedefinedPropertySetterImplementationForDto(property, redefiningProperty, classContext);

                        if (string.IsNullOrWhiteSpace(setterImplementation))
                        {
                            sb.AppendLine("\tset { }");
                        }
                        else
                        {
                            sb.AppendLine("\tset ");
                            sb.AppendLine("{");
                            sb.AppendLine($"\t{setterImplementation}");
                            sb.Append('}');
                        }

                        sb.Append('}');
                    }
                    else
                    {
                        sb.Append("{ get; set; }");

                        if (property.QueryIsEnumerable())
                        {
                            sb.Append(" = [];");
                        }
                    }
                }

                if (!isRedefinedPropertyInContext)
                {
                    if (property.QueryIsDefaultValueDifferentThanDefault())
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
                    else if (property.QueryIsEnumPropertyWithDefaultValue())
                    {
                        sb.Append($" = {StringExtensions.CapitalizeFirstLetter(property.Type.Name)}.{StringExtensions.CapitalizeFirstLetter(property.QueryDefaultValueAsString())};");
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

                if (arguments[1] is not IClass classContext)
                {
                    throw new ArgumentException("The #Property.WriteForPOCOClass supposed to be have an IClass as second argument");
                }

                var sb = new StringBuilder();
                var isRedefinedPropertyInContext = property.TryQueryRedefinedByProperty(classContext, out var redefiningProperty);
                var typeName = property.QueryCSharpTypeName();

                var propertyName = StringExtensions.CapitalizeFirstLetter(property.Name);

                if (!isRedefinedPropertyInContext)
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
                    if (property.QueryIsNullableAndNotString() && !property.QueryIsReferenceProperty())
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

                if (isRedefinedPropertyInContext)
                {
                    var owner = (INamedElement)property.Owner;
                    propertyName = $"I{owner.Name}.{propertyName}";

                    var ownerNamespace = owner.QueryNamespace();

                    if (ownerNamespace != classContext.QueryNamespace())
                    {
                        propertyName = $"{ownerNamespace}.{propertyName}";
                    }
                }

                sb.Append(propertyName);

                if (property.IsDerived || property.IsDerivedUnion)
                {
                    if (isRedefinedPropertyInContext)
                    {
                        sb.Append($"=> {GetRedefinedPropertyGetterImplementationForPoco(property, redefiningProperty, classContext)}");
                    }
                    else
                    {
                        sb.Append($"=> this.Compute{StringExtensions.CapitalizeFirstLetter(property.Name)}();");
                    }
                }
                else if (property.IsReadOnly)
                {
                    if (isRedefinedPropertyInContext)
                    {
                        sb.Append($"=> {GetRedefinedPropertyGetterImplementationForPoco(property, redefiningProperty, classContext)}");
                    }
                    else
                    {
                        sb.Append(" { get; }");
                    }
                }
                else
                {
                    if (isRedefinedPropertyInContext)
                    {
                        sb.AppendLine("{");
                        sb.AppendLine($"\tget => {GetRedefinedPropertyGetterImplementationForPoco(property, redefiningProperty, classContext)}");
                        
                        var setterImplementation = GetRedefinedPropertySetterImplementationForPoco(property, redefiningProperty, classContext);
                        
                        if (string.IsNullOrWhiteSpace(setterImplementation))
                        {
                            sb.AppendLine("\tset { }");
                        }
                        else
                        {
                            sb.AppendLine("\tset ");
                            sb.AppendLine("{");
                            sb.AppendLine($"\t{setterImplementation}");
                            sb.Append('}');
                        }
                        
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

                if ((!property.IsDerived || property.IsDerivedUnion) && !isRedefinedPropertyInContext)
                {
                    if (property.QueryIsDefaultValueDifferentThanDefault())
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
                    else if (property.QueryIsEnumPropertyWithDefaultValue())
                    {
                        sb.Append($" = {StringExtensions.CapitalizeFirstLetter(property.Type.Name)}.{StringExtensions.CapitalizeFirstLetter(property.QueryDefaultValueAsString())};");
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

            handlebars.RegisterHelper("Property.IsRedefinedByPropertyWithSameName", (context, arguments) =>
            {
                if (context.Value is not IProperty property)
                {
                    throw new ArgumentException("The #Property.IsRedefinedByPropertyWithSameName context supposed to be IProperty");
                }

                if (arguments.Length != 2)
                {
                    throw new ArgumentException("The #Property.IsRedefinedByPropertyWithSameName supposed to be have 2 arguments");
                }

                if (arguments[1] is not IClass generatedClass)
                {
                    throw new ArgumentException("The #Property.IsRedefinedByPropertyWithSameName supposed to be have an IClass as second argument");
                }

                if (!property.TryQueryRedefinedByProperty(generatedClass, out var redefinition))
                {
                    return false;
                }

                return redefinition.Name == property.Name;
            });

            handlebars.RegisterHelper("Property.WritePropertyName", (writer, context, _) =>
            {
                if (context.Value is not IProperty property)
                {
                    throw new ArgumentException("The #Property.WritePropertyName context supposed to be IProperty");
                }

                writer.WriteSafeString(property.QueryPropertyNameBasedOnUmlProperties());
            });

            handlebars.RegisterHelper("Property.WritePropertyWithExplicitInterfaceIfRequiredForDTO", (writer, context, arguments) =>
            {
                if (context.Value is not IProperty property)
                {
                    throw new ArgumentException("The #Property.WritePropertyWithExplicitInterfaceIfRequiredForDTO context supposed to be IProperty");
                }

                if (arguments.Length != 3)
                {
                    throw new ArgumentException("The #Property.WritePropertyWithExplicitInterfaceIfRequiredForDTO supposed to be have 3 arguments");
                }

                if (arguments[1] is not IClass generatedClass)
                {
                    throw new ArgumentException("The #Property.WritePropertyWithExplicitInterfaceIfRequiredForDTO supposed to be have an IClass as second argument");
                }

                var variableName = arguments[2].ToString();
                var propertyName = property.IsDerived || property.IsDerivedUnion ? StringExtensions.LowerCaseFirstLetter(property.Name) : StringExtensions.CapitalizeFirstLetter(property.Name);

                if (property.TryQueryRedefinedByProperty(generatedClass, out var redefinitionProperty) && redefinitionProperty.Name == property.Name)
                {
                    var owner = (INamedElement)property.Owner;
                    writer.WriteSafeString($"((SysML2.NET.Core.DTO.{owner.QueryNamespace()}.I{owner.Name}){variableName}).{propertyName}");
                }
                else
                {
                    writer.WriteSafeString($"{variableName}.{propertyName}");
                }
            });
        }

        /// <summary>
        /// Gets the getter implementation for an <see cref="IProperty"/> that has been redefined, for DTO generation
        /// </summary>
        /// <param name="redefinedProperty">The redefined property</param>
        /// <param name="redefinition">The property that redefines <paramref name="redefinedProperty"/></param>
        /// <param name="context">Gets the <see cref="IClass"/> context</param>
        /// <returns>The getter imlementation</returns>
        private static string GetRedefinedPropertyGetterImplementationForDto(IProperty redefinedProperty, IProperty redefinition, IClass context)
        {
            string redefinitionPropertyName;

            if (redefinition.TryQueryRedefinedByProperty(context, out _))
            {
                var owner = (INamedElement)redefinition.Owner;
                redefinitionPropertyName = $"((SysML2.NET.Core.DTO.{owner.QueryNamespace()}.I{owner.Name})this).{redefinition.QueryPropertyNameBasedOnUmlProperties()}";
            }
            else
            {
                redefinitionPropertyName = $"this.{redefinition.QueryPropertyNameBasedOnUmlProperties()}";
            }

            if (redefinedProperty.QueryIsEnumerable() && redefinition.QueryIsEnumerable())
            {
                return $"[..{redefinitionPropertyName}];";
            }

            if (redefinedProperty.QueryIsEnumerable() && !redefinition.QueryIsEnumerable())
            {
                return redefinition.QueryIsNullableAndNotString()
                    ? $"{redefinitionPropertyName}.HasValue ? [{redefinitionPropertyName}.Value] : [];"
                    : $"[{redefinitionPropertyName}];";
            }

            return redefinition.QueryIsNullableAndNotString()
                ? $"{redefinitionPropertyName}.HasValue ? {redefinitionPropertyName}.Value : {(redefinedProperty.QueryIsReferenceProperty() ? "Guid.Empty" : "default")};"
                : $"{redefinitionPropertyName};";
        }

        /// <summary>
        /// Gets the getter implementation for an <see cref="IProperty"/> that has been redefined, for POCO generation
        /// </summary>
        /// <param name="redefinedProperty">The redefined property</param>
        /// <param name="redefinition">The property that redefines <paramref name="redefinedProperty"/></param>
        /// <param name="context">Gets the <see cref="IClass"/> context</param>
        /// <returns>The getter imlementation</returns>
        private static string GetRedefinedPropertyGetterImplementationForPoco(IProperty redefinedProperty, IProperty redefinition, IClass context)
        {
            string redefinitionPropertyName;

            if (redefinition.TryQueryRedefinedByProperty(context, out _))
            {
                var owner = (INamedElement)redefinition.Owner;
                redefinitionPropertyName = $"((SysML2.NET.Core.POCO.{owner.QueryNamespace()}.I{owner.Name})this).{redefinition.QueryPropertyNameBasedOnUmlProperties()}";
            }
            else
            {
                redefinitionPropertyName = $"this.{redefinition.QueryPropertyNameBasedOnUmlProperties()}";
            }

            if (redefinedProperty.QueryIsEnumerable() && redefinition.QueryIsEnumerable())
            {
                return $"[..{redefinitionPropertyName}];";
            }

            if (redefinedProperty.QueryIsEnumerable() && !redefinition.QueryIsEnumerable())
            {
                if (redefinedProperty.QueryIsReferenceProperty())
                {
                    return $"{redefinitionPropertyName} != null ? [{redefinitionPropertyName}] : [];";
                }

                if (redefinedProperty.QueryIsString())
                {
                    return $"string.IsNullOrWhiteSpace({redefinitionPropertyName}) ? [{redefinitionPropertyName}] : [];";
                }

                return redefinition.QueryIsNullableAndNotString()
                    ? $"{redefinitionPropertyName}.HasValue ? [{redefinitionPropertyName}.Value] : [];"
                    : $"[{redefinitionPropertyName}];";
            }

            return redefinition.QueryIsNullableAndNotString() && !redefinedProperty.QueryIsReferenceProperty()
                ? $"{redefinitionPropertyName}.HasValue ? {redefinitionPropertyName}.Value : default;"
                : $"{redefinitionPropertyName};";
        }

        /// <summary>
        /// Gets the setter implementation for an <see cref="IProperty"/> that has been redefined, for DTO generation
        /// </summary>
        /// <param name="redefinedProperty">The redefined property</param>
        /// <param name="redefinition">The property that redefines <paramref name="redefinedProperty"/></param>
        /// <param name="context">Gets the <see cref="IClass"/> context</param>
        /// <returns>The setter imlementation</returns>
        private static string GetRedefinedPropertySetterImplementationForDto(IProperty redefinedProperty, IProperty redefinition, IClass context)
        {
            if (redefinition.IsDerived || redefinition.IsDerivedUnion || redefinition.IsReadOnly)
            {
                return string.Empty;
            }

            string redefinitionPropertyName;

            if (redefinition.TryQueryRedefinedByProperty(context, out _))
            {
                var owner = (INamedElement)redefinition.Owner;
                redefinitionPropertyName = $"((SysML2.NET.Core.DTO.{owner.QueryNamespace()}.I{owner.Name})this).{redefinition.QueryPropertyNameBasedOnUmlProperties()}";
            }
            else
            {
                redefinitionPropertyName = $"this.{redefinition.QueryPropertyNameBasedOnUmlProperties()}";
            }

            if (redefinedProperty.QueryIsEnumerable() == redefinition.QueryIsEnumerable())
            {
                return $"{redefinitionPropertyName} = value;";
            }

            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("if(value.Count != 0)");
            stringBuilder.AppendLine("{");
            stringBuilder.AppendLine($"\t{redefinitionPropertyName} = value[0];");
            stringBuilder.AppendLine("}");
            return stringBuilder.ToString();
        }

        /// <summary>
        /// Gets the setter implementation for an <see cref="IProperty"/> that has been redefined, for POCO generation
        /// </summary>
        /// <param name="redefinedProperty">The redefined property</param>
        /// <param name="redefinition">The property that redefines <paramref name="redefinedProperty"/></param>
        /// <param name="context">Gets the <see cref="IClass"/> context</param>
        /// <returns>The setter imlementation</returns>
        private static string GetRedefinedPropertySetterImplementationForPoco(IProperty redefinedProperty, IProperty redefinition, IClass context)
        {
            if (redefinition.IsDerived || redefinition.IsDerivedUnion || redefinition.IsReadOnly)
            {
                return string.Empty;
            }

            string redefinitionPropertyName;

            if (redefinition.TryQueryRedefinedByProperty(context, out _))
            {
                var owner = (INamedElement)redefinition.Owner;
                redefinitionPropertyName = $"((SysML2.NET.Core.POCO.{owner.QueryNamespace()}.I{owner.Name})this).{redefinition.QueryPropertyNameBasedOnUmlProperties()}";
            }
            else
            {
                redefinitionPropertyName = $"this.{redefinition.QueryPropertyNameBasedOnUmlProperties()}";
            }

            var redefinitionPropertyTypeName = redefinition.QueryIsReferenceProperty() ? $"I{redefinition.Type.Name}" : redefinition.QueryCSharpTypeName();
            
            if (redefinedProperty.QueryIsEnumerable() && redefinition.QueryIsEnumerable())
            {
                return redefinedProperty.Type == redefinition.Type ? $"{redefinitionPropertyName} = value;" : $"{redefinitionPropertyName} = [..value.OfType<{redefinitionPropertyTypeName}>()];";
            }

            var stringBuilder = new StringBuilder();

            if (redefinedProperty.QueryIsEnumerable())
            {
                if (redefinedProperty.Type == redefinition.Type)
                {
                    stringBuilder.AppendLine("if(value.Count != 0)");
                    stringBuilder.AppendLine("{");
                    stringBuilder.AppendLine($"\t{redefinitionPropertyName} = value[0];");
                }
                else
                {
                    stringBuilder.AppendLine($"if(value.OfType<{redefinitionPropertyTypeName}>().FirstOrDefault() is {{ }} firstValue)");
                    stringBuilder.AppendLine("{");
                    stringBuilder.AppendLine($"\t{redefinitionPropertyName} = firstValue;");
                }
                
                stringBuilder.Append('}');
            }
            else
            {
                if (redefinedProperty.Type == redefinition.Type)
                {
                    stringBuilder.Append($"{redefinitionPropertyName} = value;");
                }
                else
                {
                    stringBuilder.AppendLine($"if(value is {redefinitionPropertyTypeName} castedValue)");
                    stringBuilder.AppendLine("{");
                    stringBuilder.AppendLine($"\t{redefinitionPropertyName} = castedValue;");
                    stringBuilder.Append('}');
                }
            }

            return stringBuilder.ToString();
        }
    }
}
