// ------------------------------------------------------------------------------------------------
// <copyright file="PropertyHelper.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.CodeGenerator.HandleBarHelpers
{
    using System;
    using System.Collections.Generic;
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
    using uml4net.Values;

    /// <summary>
    /// A handlebars block helper for the <see cref="IProperty"/> interface
    /// </summary>
    public static class PropertyHelper
    {
        /// <summary>
        /// The placeholder for the local holding a deserialized redefined value in a <see cref="RedefinedDeserializationShape"/>
        /// </summary>
        private const string ValuePlaceholder = "{value}";

        /// <summary>
        /// The placeholder for the left operand of a <see cref="RedefinedDeserializationShape"/> comparison
        /// </summary>
        private const string LeftPlaceholder = "{left}";

        /// <summary>
        /// The placeholder for the right operand of a <see cref="RedefinedDeserializationShape"/> comparison
        /// </summary>
        private const string RightPlaceholder = "{right}";

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

                        if (uml4net.Extensions.PropertyExtensions.QueryIsNullableAndNotString(property))
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

                        if (uml4net.Extensions.PropertyExtensions.QueryIsNullableAndNotString(property))
                        {
                            sb.Append('?');
                        }

                        sb.Append(' ');
                    }
                }

                var propertyName = property.Name.CapitalizeFirstLetter();

                if (property.IsDerived || property.IsDerivedUnion)
                {
                    propertyName = propertyName.LowerCaseFirstLetter();
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

                var property = (IProperty)parameters[0];
                var classContext = (IClass)parameters[1];

                var sb = new StringBuilder();
                var propertyName = property.Name.CapitalizeFirstLetter();
                var isRedefinedPropertyInContext = property.TryQueryRedefinedByProperty(classContext, out var redefiningProperty);

                if (!isRedefinedPropertyInContext)
                {
                    sb.Append(property.Visibility?.ToString().ToLower(CultureInfo.InvariantCulture));
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

                        if (uml4net.Extensions.PropertyExtensions.QueryIsNullableAndNotString(property))
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

                        if (uml4net.Extensions.PropertyExtensions.QueryIsNullableAndNotString(property))
                        {
                            sb.Append('?');
                        }

                        sb.Append(' ');
                    }
                }

                if (property.IsDerived || property.IsDerivedUnion)
                {
                    propertyName = propertyName.LowerCaseFirstLetter();
                }

                if (isRedefinedPropertyInContext)
                {
                    var owner = (INamedElement)property.Owner;
                    propertyName = $"I{owner.Name}.{propertyName}";

                    var ownerNamespace = Extensions.NamedElementExtensions.QueryNamespace(owner);

                    if (ownerNamespace != Extensions.NamedElementExtensions.QueryNamespace(classContext))
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
                    if (property.QueryIsEnumPropertyWithDefaultValue())
                    {
                        sb.Append($" = {property.Type.Name.CapitalizeFirstLetter()}.{property.QueryDefaultValueAsString().CapitalizeFirstLetter()};");
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
                    if (property.QueryPropertyIsPartOfNonDerivedCompositeAggregation())
                    {
                        sb.Append($"IReadOnlyList<{typeName}> ");
                    }
                    else
                    {
                        sb.Append($"List<{typeName}> ");
                    }
                }
                else
                {
                    if (uml4net.Extensions.PropertyExtensions.QueryIsNullableAndNotString(property) && !property.QueryIsReferenceType())
                    {
                        sb.Append($"{typeName}? ");
                    }
                    else
                    {
                        sb.Append($"{typeName} ");
                    }
                }

                var propertyName = property.Name.CapitalizeFirstLetter();

                if (property.IsDerived || property.IsDerivedUnion)
                {
                    propertyName = propertyName.LowerCaseFirstLetter();
                }

                sb.Append(propertyName);

                if (property.IsReadOnly || property.IsDerived || property.IsDerivedUnion || property.IsComposite || property.QueryPropertyIsPartOfNonDerivedCompositeAggregation())
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

                var propertyName = property.Name.CapitalizeFirstLetter();

                if (!isRedefinedPropertyInContext)
                {
                    sb.Append(property.Visibility?.ToString().ToLower(CultureInfo.InvariantCulture));
                    sb.Append(' ');
                }

                if (property.Type is not IDataType)
                {
                    typeName = $"I{typeName}";
                }

                if (property.QueryIsEnumerable())
                {
                    if (property.QueryPropertyIsPartOfNonDerivedCompositeAggregation())
                    {
                        sb.Append($"IReadOnlyList<{typeName}> ");
                    }
                    else
                    {
                        sb.Append($"List<{typeName}> ");
                    }
                }
                else
                {
                    if (uml4net.Extensions.PropertyExtensions.QueryIsNullableAndNotString(property) && !property.QueryIsReferenceType())
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
                    propertyName = propertyName.LowerCaseFirstLetter();
                }

                if (isRedefinedPropertyInContext)
                {
                    var owner = (INamedElement)property.Owner;
                    propertyName = $"I{owner.Name}.{propertyName}";

                    var ownerNamespace = Extensions.NamedElementExtensions.QueryNamespace(owner);

                    if (ownerNamespace != Extensions.NamedElementExtensions.QueryNamespace(classContext))
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
                        sb.Append($"=> this.Compute{property.Name.CapitalizeFirstLetter()}();");
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
                        if (property.QueryPropertyIsPartOfNonDerivedCompositeAggregation())
                        {
                            var propertyOwner = property.Owner as IClass;
                            var internalInterfaceName = $"IContained{propertyOwner?.Name}";
                            sb.Append($" => (({internalInterfaceName})this).{propertyName};");
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
                }

                if ((!property.IsDerived || property.IsDerivedUnion) && !isRedefinedPropertyInContext)
                {
                    if (property.QueryIsEnumPropertyWithDefaultValue())
                    {
                        sb.Append($" = {property.Type.Name.CapitalizeFirstLetter()}.{property.QueryDefaultValueAsString().CapitalizeFirstLetter()};");
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

                if (property.QueryPropertyIsPartOfNonDerivedCompositeAggregation())
                {
                    var propertyOwner = property.Owner as IClass;
                    var internalInterfaceName = $"IContained{propertyOwner?.Name}";
                    sb.AppendLine(Environment.NewLine);
                    sb.AppendLine($"/// <summary>Backing field for I{propertyOwner?.Name}.{propertyName}</summary>");

                    if (property.QueryIsEnumerable())
                    {
                        var oppositeTypeName = property.Opposite.Type.Name;

                        sb.Append($"ContainerList<I{oppositeTypeName}, {typeName}> ");
                    }
                    else
                    {
                        sb.Append($"{typeName} ");
                    }

                    sb.Append($"{internalInterfaceName}.{propertyName}");
                    sb.Append("{ get; set; }");
                }

                writer.WriteSafeString(sb + Environment.NewLine);
            });

            handlebars.RegisterHelper("Property.WriteForDTOMessagePackFormatterSerialize", (writer, context, arguments) =>
            {
                if (context.Value is not IProperty property)
                {
                    throw new ArgumentException("The #Property.WriteForDTOMessagePackFormatterSerialize context supposed to be IProperty");
                }

                if (arguments.Length != 2)
                {
                    throw new ArgumentException("The #Property.WriteForDTOMessagePackFormatterSerialize supposed to be have 2 arguments IProperty");
                }

                if (arguments[1] is not IClass generatedClass)
                {
                    throw new ArgumentException("The #Property.WriteForDTOMessagePackFormatterSerialize supposed to be have an IClass as second argument");
                }

                // derived, derived-union and readonly properties are ignored
                if (property.IsDerived || property.IsDerivedUnion || property.IsReadOnly)
                {
                    return;
                }

                // properties that have been redefined in the context of the
                // generated class are ignored
                if (property.TryQueryRedefinedByProperty(generatedClass, out _))
                {
                    return;
                }

                writer.WriteSafeString(WriteMessagePackSerialization(property, $"dto.{property.Name.CapitalizeFirstLetter()}", property.Name.LowerCaseFirstLetter()));
            });

            handlebars.RegisterHelper("Property.WriteForDTOMessagePackFormatterDeSerialize", (writer, context, arguments) =>
            {
                if (context.Value is not IProperty property)
                {
                    throw new ArgumentException("The #Property.WriteForDTOMessagePackFormatterSerialize context supposed to be IProperty");
                }

                if (arguments.Length != 2)
                {
                    throw new ArgumentException("The #Property.WriteForDTOMessagePackFormatterSerialize supposed to be have 2 arguments IProperty");
                }

                if (arguments[1] is not IClass generatedClass)
                {
                    throw new ArgumentException("The #Property.WriteForDTOMessagePackFormatterSerialize supposed to be have an IClass as second argument");
                }

                // derived, derived-union and readonly properties are ignored
                if (property.IsDerived || property.IsDerivedUnion || property.IsReadOnly)
                {
                    return;
                }

                // properties that have been redefined in the context of the
                // generated class are ignored
                if (property.TryQueryRedefinedByProperty(generatedClass, out _))
                {
                    return;
                }

                writer.WriteSafeString(WriteMessagePackDeserialization(property, $"dto.{property.Name.CapitalizeFirstLetter()}", property.Name.LowerCaseFirstLetter(), false));
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

            handlebars.RegisterHelper("Property.WriteForDTOComparer", (writer, _, parameters) => WriteForDtoComparer(writer, parameters));

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

            handlebars.RegisterHelper("Property.IsSerializedRedefinition", (_, arguments) =>
            {
                if (arguments.Length != 2 || arguments[0] is not IProperty property || arguments[1] is not IClass generatedClass)
                {
                    throw new ArgumentException("The #Property.IsSerializedRedefinition helper expects an IProperty and an IClass");
                }

                return property.QueryIsSerializedRedefinition(generatedClass);
            });

            handlebars.RegisterHelper("Property.WriteRedefinedByPropertyAttribute", (writer, _, arguments) =>
            {
                if (arguments.Length != 2 || arguments[0] is not IProperty property || arguments[1] is not IClass generatedClass)
                {
                    throw new HandlebarsException("{{#Property.WriteRedefinedByPropertyAttribute}} expects an IProperty and an IClass");
                }

                if (!property.TryQueryRedefinedByProperty(generatedClass, out var redefinition))
                {
                    return;
                }

                var owner = (INamedElement)redefinition.Owner;
                var isDerivedByRedefinition = !property.IsDerived && !property.IsDerivedUnion && property.QueryHasDerivedValueIn(generatedClass);
                var derivedFlag = isDerivedByRedefinition ? "true" : "false";

                writer.WriteSafeString($"[RedefinedByProperty(\"I{owner.Name}.{redefinition.Name.CapitalizeFirstLetter()}\", isDerivedByRedefinition: {derivedFlag})]");
            });

            handlebars.RegisterHelper("Property.HasDerivedValueIn", (_, arguments) =>
            {
                if (arguments.Length != 2 || arguments[0] is not IProperty property || arguments[1] is not IClass generatedClass)
                {
                    throw new ArgumentException("The #Property.HasDerivedValueIn helper expects an IProperty and an IClass");
                }

                return property.QueryHasDerivedValueIn(generatedClass);
            });

            handlebars.RegisterHelper("Property.WriteRedefinedToDtoAssignment", (writer, _, arguments) =>
            {
                if (arguments.Length != 1 || arguments[0] is not IProperty property)
                {
                    throw new HandlebarsException("{{#Property.WriteRedefinedToDtoAssignment}} expects an IProperty");
                }

                writer.WriteSafeString(WriteRedefinedToDtoAssignment(property));
            });

            handlebars.RegisterHelper("Property.WriteToDtoAssignment", (writer, _, arguments) =>
            {
                if (arguments.Length != 1 || arguments[0] is not IProperty property)
                {
                    throw new HandlebarsException("{{#Property.WriteToDtoAssignment}} expects an IProperty");
                }

                var propertyName = property.QueryPropertyNameBasedOnUmlProperties();

                writer.WriteSafeString(QueryToDtoAssignment(property, $"dto.{propertyName}", $"poco.{propertyName}"));
            });

            handlebars.RegisterHelper("Property.WriteRedefinedDeserialization", (writer, _, arguments) =>
            {
                if (arguments.Length != 3 || arguments[0] is not IClass generatedClass || arguments[1] is not string section || arguments[2] is not bool includeDerivedProperties)
                {
                    throw new HandlebarsException("{{#Property.WriteRedefinedDeserialization}} expects an IClass, a section name and an include-derived flag");
                }

                writer.WriteSafeString(WriteRedefinedDeserialization(generatedClass, section, includeDerivedProperties));
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

            handlebars.RegisterHelper("Property.WritePropertyName", (writer, _, arguments) =>
            {
                if (arguments.Single() is not IProperty property)
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
                var propertyName = property.IsDerived || property.IsDerivedUnion ? property.Name.LowerCaseFirstLetter() : property.Name.CapitalizeFirstLetter();

                if (property.TryQueryRedefinedByProperty(generatedClass, out var redefinitionProperty) && redefinitionProperty.Name == property.Name)
                {
                    var owner = (INamedElement)property.Owner;
                    writer.WriteSafeString($"((SysML2.NET.Core.DTO.{Extensions.NamedElementExtensions.QueryNamespace(owner)}.I{owner.Name}){variableName}).{propertyName}");
                }
                else
                {
                    writer.WriteSafeString($"{variableName}.{propertyName}");
                }
            });

            handlebars.RegisterHelper("Property.IsOfTypeBaseElement", (_, arguments) =>
            {
                if (arguments.Single() is not IProperty property)
                {
                    throw new ArgumentException("The #Property.IsOfTypeBaseElement argument supposed to be IProperty");
                }

                return property.Type.Name == "Element";
            });

            handlebars.RegisterHelper("Property.QueryIsImpliedIncluded", (_, arguments) =>
            {
                if (arguments.Single() is not IProperty property)
                {
                    throw new ArgumentException("The #Property.QueryIsImpliedIncluded argument supposed to be IProperty");
                }

                return property.Name == "isImpliedIncluded";
            });

            handlebars.RegisterHelper("Property.WriteForNonDerivedCompositeAggregation", (writer, context, _) =>
            {
                if (context.Value is not IProperty property)
                {
                    throw new ArgumentException("The #Property.WriteForNonDerivedCompositeAggregation context supposed to be IProperty");
                }

                var stringBuilder = new StringBuilder();
                var propertyOwner = (IClass)property.Owner;

                var interfaceName = propertyOwner.QueryInternalInterfaceName();
                var propertyName = property.Name.CapitalizeFirstLetter();
                var oppositePropertyName = property.Opposite.Name.CapitalizeFirstLetter();
                var childVariableName = property.Type.Name.LowerCaseFirstLetter();
                var parentVariableName = property.Opposite.Type.Name.LowerCaseFirstLetter();

                stringBuilder.AppendLine($"(({interfaceName})this).{propertyName} = new ContainerList<I{property.Opposite.Type.Name}, I{property.Type.Name}>(this, ");
                stringBuilder.AppendLine($"({childVariableName}, {parentVariableName}) => ((IContained{property.Type.Name}){childVariableName}).{oppositePropertyName} = {parentVariableName},");
                stringBuilder.AppendLine($"{childVariableName} => {childVariableName}.{oppositePropertyName});");
                writer.WriteSafeString(stringBuilder + Environment.NewLine);
            });

            handlebars.RegisterHelper("Property.QueryHasDefaultValueWithDifferentValueThanDefault", (_, arguments) =>
            {
                if (arguments.Length != 1)
                {
                    throw new ArgumentException("The #Property.QueryHasDefaultValueWithDifferentValueThanDefault supposed to have one argument");
                }

                if (arguments[0] is not IProperty property)
                {
                    throw new ArgumentException("The #Property.QueryHasDefaultValueWithDifferentValueThanDefault argument supposed to be an IProperty");
                }

                return property.QueryIsEnum()
                    ? property.QueryIsEnumPropertyWithDefaultValue()
                    : (property.QueryHasDefaultValue() && property.QueryIsDefaultValueDifferentThanDefault());
            });

            handlebars.RegisterHelper("Property.WriteDefaultValue", (writer, _, arguments) =>
            {
                if (arguments.Length != 1)
                {
                    throw new ArgumentException("The #Property.WriteDefaultValue supposed to have one argument");
                }

                if (arguments[0] is not IProperty property)
                {
                    throw new ArgumentException("The #Property.WriteDefaultValue argument supposed to be an IProperty");
                }

                if (property.QueryIsEnum())
                {
                    writer.WriteSafeString($"{property.Type.Name}.{property.QueryDefaultValueAsString().CapitalizeFirstLetter()}");
                }
                else if (property.QueryIsString())
                {
                    writer.WriteSafeString($"\"{property.QueryDefaultValueAsString()}\"");
                }
                else
                {
                    writer.WriteSafeString($"{property.QueryDefaultValueAsString()}");
                }
            });

            handlebars.RegisterHelper("Property.QueryIsEnumerableAndReferenceProperty", (_, arguments) =>
            {
                if (arguments.Length != 1)
                {
                    throw new ArgumentException("The #Property.QueryIsEnumerableAndReferenceProperty supposed to have one argument");
                }

                if (arguments[0] is not IProperty property)
                {
                    throw new ArgumentException("The #Property.QueryIsEnumerableAndReferenceProperty argument supposed to be an IProperty");
                }

                return property.QueryIsEnumerable() && property.QueryIsReferenceType();
            });

            handlebars.RegisterHelper("Property.IsTypeAbstract", (_, arguments) =>
            {
                if (arguments.Length != 1)
                {
                    throw new ArgumentException("The #Property.IsTypeAbstract supposed to have one argument");
                }

                if (arguments[0] is not IProperty property)
                {
                    throw new ArgumentException("The #Property.IsTypeAbstract argument supposed to be an IProperty");
                }

                return property.Type is IClassifier { IsAbstract: true };
            });

            handlebars.RegisterHelper("Property.QueryHasOwnedConstraints", (_, arguments) =>
            {
                if (arguments.Length != 1 || arguments[0] is not IProperty property)
                {
                    throw new HandlebarsException("{{Property.QueryHasOwnedConstraints}} expects a single IProperty argument");
                }

                return HasRenderableConstraint(property.QueryOwnedConstraints());
            });

            handlebars.RegisterHelper("Operation.QueryHasOwnedConstraints", (_, arguments) =>
            {
                if (arguments.Length != 1 || arguments[0] is not IOperation operation)
                {
                    throw new HandlebarsException("{{Operation.QueryHasOwnedConstraints}} expects a single IOperation argument");
                }

                return HasRenderableConstraint(operation.QueryOwnedConstraints());
            });

            handlebars.RegisterHelper("Property.WriteOwnedRulesAsRemarksBlock", (writer, _, arguments) =>
            {
                if (arguments.Length != 1 || arguments[0] is not IProperty property)
                {
                    throw new HandlebarsException("{{Property.WriteOwnedRulesAsRemarksBlock}} expects a single IProperty argument");
                }

                writer.WriteSafeString(BuildOwnedRulesRemarksBlock(property.QueryOwnedConstraints()));
            });

            handlebars.RegisterHelper("Operation.WriteOwnedRulesAsRemarksBlock", (writer, _, arguments) =>
            {
                if (arguments.Length != 1 || arguments[0] is not IOperation operation)
                {
                    throw new HandlebarsException("{{Operation.WriteOwnedRulesAsRemarksBlock}} expects a single IOperation argument");
                }

                writer.WriteSafeString(BuildOwnedRulesRemarksBlock(operation.QueryOwnedConstraints()));
            });
        }

        /// <summary>
        /// Returns <see langword="true"/> when at least one constraint in <paramref name="constraints"/>
        /// carries an <see cref="IOpaqueExpression"/> with at least one non-blank body line — i.e. when
        /// <see cref="BuildOwnedRulesRemarksBlock"/> would emit a non-empty <c>&lt;remarks&gt;</c> block.
        /// </summary>
        /// <param name="constraints">The constraints to evaluate.</param>
        /// <returns>Whether the helpers would produce any output for these constraints.</returns>
        private static bool HasRenderableConstraint(IEnumerable<IConstraint> constraints)
        {
            foreach (var constraint in constraints)
            {
                var opaqueExpression = constraint.Specification?.OfType<IOpaqueExpression>().FirstOrDefault();

                if (opaqueExpression?.Body != null && opaqueExpression.Body.Any(body => !string.IsNullOrWhiteSpace(body)))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Builds a complete XML <c>&lt;remarks&gt;</c> block listing every constraint body carried by
        /// the supplied <see cref="IConstraint"/> sequence, labelled by language. Returns the empty
        /// string when no constraint carries an <see cref="IOpaqueExpression"/> body.
        /// </summary>
        /// <param name="constraints">The constraints to render.</param>
        /// <returns>
        /// A multi-line string starting with <c>/// &lt;remarks&gt;</c> and ending with <c>/// &lt;/remarks&gt;</c>,
        /// each line prefixed for direct emission inside the Extend template; empty when nothing to emit.
        /// </returns>
        private static string BuildOwnedRulesRemarksBlock(IEnumerable<IConstraint> constraints)
        {
            var entries = new List<(string Language, string Body)>();

            foreach (var constraint in constraints)
            {
                var opaqueExpression = constraint.Specification?.OfType<IOpaqueExpression>().FirstOrDefault();

                if (opaqueExpression?.Body == null)
                {
                    continue;
                }

                var bodies = opaqueExpression.Body;

                for (var index = 0; index < bodies.Count; index++)
                {
                    if (string.IsNullOrWhiteSpace(bodies[index]))
                    {
                        continue;
                    }

                    entries.Add((GetConstraintLanguage(opaqueExpression.Language, index), bodies[index].Trim()));
                }
            }

            if (entries.Count == 0)
            {
                return string.Empty;
            }

            var sb = new StringBuilder();
            sb.AppendLine("/// <remarks>");

            for (var index = 0; index < entries.Count; index++)
            {
                var (language, body) = entries[index];

                sb.AppendLine($"/// {EscapeXml(language)}:");
                sb.AppendLine("/// <code>");

                foreach (var line in body.Replace("\r\n", "\n").Replace('\r', '\n').Split('\n'))
                {
                    sb.AppendLine($"/// {EscapeXml(line.TrimEnd())}");
                }

                sb.AppendLine("/// </code>");
            }

            sb.Append("/// </remarks>");

            return sb.ToString();
        }

        /// <summary>
        /// Gets the language label of the constraint body at <paramref name="index"/>, or <c>Constraint</c> when none is given
        /// </summary>
        /// <param name="languages">The languages of an <see cref="IOpaqueExpression"/>, positionally matching its bodies</param>
        /// <param name="index">The 0-based index of the body</param>
        /// <returns>The trimmed language label</returns>
        private static string GetConstraintLanguage(List<string> languages, int index)
        {
            if (languages == null || index >= languages.Count || string.IsNullOrWhiteSpace(languages[index]))
            {
                return "Constraint";
            }

            return languages[index].Trim();
        }

        /// <summary>
        /// Replaces XML-significant characters with their entity equivalents so the result is safe to
        /// embed inside an XML doc comment.
        /// </summary>
        /// <param name="value">The string to escape.</param>
        /// <returns>The escaped string.</returns>
        private static string EscapeXml(string value)
        {
            return value
                .Replace("&", "&amp;")
                .Replace("<", "&lt;")
                .Replace(">", "&gt;");
        }

        /// <summary>
        /// Writes the equality check of a DTO comparer for the <see cref="IProperty"/> and <see cref="IClass"/> passed as arguments
        /// </summary>
        /// <param name="writer">The <see cref="EncodedTextWriter"/> to write to</param>
        /// <param name="parameters">The helper arguments: the <see cref="IProperty"/> and its <see cref="IClass"/> context</param>
        private static void WriteForDtoComparer(EncodedTextWriter writer, Arguments parameters)
        {
            if (parameters.Length != 2)
            {
                throw new HandlebarsException("{{#Property.WriteForDTOComparer}} helper must have exactly two arguments");
            }

            var property = (IProperty)parameters[0];
            var classContext = (IClass)parameters[1];

            if (property.TryQueryRedefinedByProperty(classContext, out _) && !property.QueryIsSerializedRedefinition(classContext))
            {
                throw new InvalidOperationException("Property.WriteForDTOComparer is invalid for redefined properties that are not serialized under their own name");
            }

            if (property.IsDerived || property.IsDerivedUnion)
            {
                throw new InvalidOperationException("Property.WriteForDTOComparer is invalid for derived or derivedunion properties");
            }

            var sb = new StringBuilder();
            var propertyName = property.Name.CapitalizeFirstLetter();

            if (property.Type is IDataType)
            {
                AppendDataTypeComparison(sb, property, propertyName);
            }
            else
            {
                AppendSequenceOrScalarComparison(sb, property, propertyName, "GuidSequenceEquality");
            }

            writer.WriteSafeString(sb + Environment.NewLine);
        }

        /// <summary>
        /// Appends the equality check for an <see cref="IProperty"/> typed by an <see cref="IDataType"/>
        /// </summary>
        /// <param name="sb">The <see cref="StringBuilder"/> to append to</param>
        /// <param name="property">The <see cref="IProperty"/> to compare</param>
        /// <param name="propertyName">The C# name of the <paramref name="property"/></param>
        private static void AppendDataTypeComparison(StringBuilder sb, IProperty property, string propertyName)
        {
            if (property.QueryIsString())
            {
                if (property.QueryIsEnumerable())
                {
                    AppendSequenceOrScalarComparison(sb, property, propertyName, "StringSequenceEquality");
                }
                else
                {
                    sb.AppendLine($"if (!OrdinalStringComparer.Equals(x.{propertyName}, y.{propertyName})) return false;");
                }
            }
            else if (property.QueryIsDouble())
            {
                AppendFloatingPointComparison(sb, property, propertyName, "DefaultDoubleEpsilon");
            }
            else if (property.QueryIsFloat())
            {
                AppendFloatingPointComparison(sb, property, propertyName, "DefaultFloatEpsilon");
            }
            else
            {
                sb.AppendLine($"if (x.{propertyName} != y.{propertyName}) return false;");
            }
        }

        /// <summary>
        /// Appends the equality check for an <see cref="IProperty"/> compared as an ordered or unordered sequence, or as a single value
        /// </summary>
        /// <param name="sb">The <see cref="StringBuilder"/> to append to</param>
        /// <param name="property">The <see cref="IProperty"/> to compare</param>
        /// <param name="propertyName">The C# name of the <paramref name="property"/></param>
        /// <param name="sequenceEquality">The name of the sequence-equality class used when the property is enumerable</param>
        private static void AppendSequenceOrScalarComparison(StringBuilder sb, IProperty property, string propertyName, string sequenceEquality)
        {
            if (!property.QueryIsEnumerable())
            {
                sb.AppendLine($"if (x.{propertyName} != y.{propertyName}) return false;");
                return;
            }

            var method = property.IsOrdered ? "OrderedEqual" : "UnorderedEqual";

            sb.AppendLine($"if (!{sequenceEquality}.{method}(x.{propertyName}, y.{propertyName})) return false;");
        }

        /// <summary>
        /// Appends the tolerance-based equality check for a floating-point <see cref="IProperty"/>
        /// </summary>
        /// <param name="sb">The <see cref="StringBuilder"/> to append to</param>
        /// <param name="property">The floating-point <see cref="IProperty"/> to compare</param>
        /// <param name="propertyName">The C# name of the <paramref name="property"/></param>
        /// <param name="epsilon">The name of the <c>FloatingPointComparer</c> epsilon constant to compare with</param>
        private static void AppendFloatingPointComparison(StringBuilder sb, IProperty property, string propertyName, string epsilon)
        {
            if (property.QueryIsNullable())
            {
                sb.AppendLine($"if (x.{propertyName}.HasValue != y.{propertyName}.HasValue) return false;");

                sb.AppendLine($"if (x.{propertyName}.HasValue && !FloatingPointComparer.NearlyEqual(x.{propertyName}.Value, y.{propertyName}.Value, FloatingPointComparer.{epsilon})) return false;");
            }
            else
            {
                sb.AppendLine($"if (!FloatingPointComparer.NearlyEqual(x.{propertyName}, y.{propertyName}, FloatingPointComparer.{epsilon})) return false;");
            }
        }

        /// <summary>
        /// Gets the getter implementation for an <see cref="IProperty"/> that has been redefined, for DTO generation
        /// </summary>
        /// <param name="redefinedProperty">The redefined property</param>
        /// <param name="redefinition">The property that redefines <paramref name="redefinedProperty"/></param>
        /// <param name="context">Gets the <see cref="IClass"/> context</param>
        /// <returns>The getter implementation</returns>
        private static string GetRedefinedPropertyGetterImplementationForDto(IProperty redefinedProperty, IProperty redefinition, IClass context)
        {
            string redefinitionPropertyName;

            if (redefinition.TryQueryRedefinedByProperty(context, out _))
            {
                var owner = (INamedElement)redefinition.Owner;
                redefinitionPropertyName = $"((SysML2.NET.Core.DTO.{Extensions.NamedElementExtensions.QueryNamespace(owner)}.I{owner.Name})this).{redefinition.QueryPropertyNameBasedOnUmlProperties()}";
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
                return uml4net.Extensions.PropertyExtensions.QueryIsNullableAndNotString(redefinition)
                    ? $"{redefinitionPropertyName}.HasValue ? [{redefinitionPropertyName}.Value] : [];"
                    : $"[{redefinitionPropertyName}];";
            }

            var defaultValue = redefinedProperty.QueryIsReferenceType() ? "Guid.Empty" : "default";

            return uml4net.Extensions.PropertyExtensions.QueryIsNullableAndNotString(redefinition)
                ? $"{redefinitionPropertyName}.HasValue ? {redefinitionPropertyName}.Value : {defaultValue};"
                : $"{redefinitionPropertyName};";
        }

        /// <summary>
        /// Gets the getter implementation for an <see cref="IProperty"/> that has been redefined, for POCO generation
        /// </summary>
        /// <param name="redefinedProperty">The redefined property</param>
        /// <param name="redefinition">The property that redefines <paramref name="redefinedProperty"/></param>
        /// <param name="context">Gets the <see cref="IClass"/> context</param>
        /// <returns>The getter implementation</returns>
        private static string GetRedefinedPropertyGetterImplementationForPoco(IProperty redefinedProperty, IProperty redefinition, IClass context)
        {
            string redefinitionPropertyName;

            if (redefinition.TryQueryRedefinedByProperty(context, out _))
            {
                var owner = (INamedElement)redefinition.Owner;
                redefinitionPropertyName = $"((SysML2.NET.Core.POCO.{Extensions.NamedElementExtensions.QueryNamespace(owner)}.I{owner.Name})this).{redefinition.QueryPropertyNameBasedOnUmlProperties()}";
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
                if (redefinedProperty.QueryIsReferenceType())
                {
                    return $"{redefinitionPropertyName} != null ? [{redefinitionPropertyName}] : [];";
                }

                if (redefinedProperty.QueryIsString())
                {
                    return $"string.IsNullOrWhiteSpace({redefinitionPropertyName}) ? [{redefinitionPropertyName}] : [];";
                }

                return uml4net.Extensions.PropertyExtensions.QueryIsNullableAndNotString(redefinition)
                    ? $"{redefinitionPropertyName}.HasValue ? [{redefinitionPropertyName}.Value] : [];"
                    : $"[{redefinitionPropertyName}];";
            }

            return uml4net.Extensions.PropertyExtensions.QueryIsNullableAndNotString(redefinition) && !redefinedProperty.QueryIsReferenceType()
                ? $"{redefinitionPropertyName}.HasValue ? {redefinitionPropertyName}.Value : default;"
                : $"{redefinitionPropertyName};";
        }

        /// <summary>
        /// Gets the setter implementation for an <see cref="IProperty"/> that has been redefined, for DTO generation
        /// </summary>
        /// <param name="redefinedProperty">The redefined property</param>
        /// <param name="redefinition">The property that redefines <paramref name="redefinedProperty"/></param>
        /// <param name="context">Gets the <see cref="IClass"/> context</param>
        /// <returns>The setter implementation</returns>
        private static string GetRedefinedPropertySetterImplementationForDto(IProperty redefinedProperty, IProperty redefinition, IClass context)
        {
            if (redefinition.IsDerived || redefinition.IsDerivedUnion)
            {
                return GetSetterIntoDerivedStorageForDto(redefinedProperty, redefinition, context);
            }

            if (redefinition.IsReadOnly)
            {
                return string.Empty;
            }

            string redefinitionPropertyName;

            if (redefinition.TryQueryRedefinedByProperty(context, out _))
            {
                var owner = (INamedElement)redefinition.Owner;
                redefinitionPropertyName = $"((SysML2.NET.Core.DTO.{Extensions.NamedElementExtensions.QueryNamespace(owner)}.I{owner.Name})this).{redefinition.QueryPropertyNameBasedOnUmlProperties()}";
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
        /// Writes one section of the JSON deserialization of the properties that the <paramref name="generatedClass"/> redefines but that are
        /// still serialized under their own name.
        /// </summary>
        /// <param name="generatedClass">The <see cref="IClass"/> whose deserializer is generated</param>
        /// <param name="section">Either <c>declarations</c>, <c>reads</c> or <c>assignments</c></param>
        /// <param name="includeDerivedProperties">Whether the generated method also reads derived properties</param>
        /// <returns>The generated code</returns>
        /// <exception cref="HandlebarsException">If the <paramref name="section"/> is unknown or a property has an unsupported type</exception>
        private static string WriteRedefinedDeserialization(IClass generatedClass, string section, bool includeDerivedProperties)
        {
            var sb = new StringBuilder();

            var redefinedProperties = generatedClass.QueryAllProperties()
                .Where(property => property.QueryIsSerializedRedefinition(generatedClass) && (includeDerivedProperties || !property.QueryHasDerivedValueIn(generatedClass)));

            foreach (var property in redefinedProperties)
            {
                var shape = QueryRedefinedDeserializationShape(property);

                switch (section)
                {
                    case "declarations":
                        sb.AppendLine($"var {property.Name}RedefinitionSeen = false;");
                        sb.AppendLine($"{shape.LocalType} {property.Name}RedefinitionValue = {shape.LocalDefault};");
                        break;
                    case "reads":
                        sb.AppendLine($"if (reader.ValueTextEquals(\"{property.Name}\"u8))");
                        sb.AppendLine("{");
                        sb.AppendLine($"{property.Name}RedefinitionSeen = true;");
                        sb.AppendLine("reader.Read();");
                        sb.AppendLine(shape.Read.Replace(ValuePlaceholder, $"{property.Name}RedefinitionValue"));
                        sb.AppendLine("continue;");
                        sb.AppendLine("}");
                        break;
                    case "assignments":
                        AppendRedefinedAssignment(sb, property, generatedClass, shape, includeDerivedProperties);
                        break;
                    default:
                        throw new HandlebarsException($"Unknown redefined-deserialization section '{section}'");
                }

                sb.AppendLine();
            }

            return sb.ToString();
        }

        /// <summary>
        /// Writes the MessagePack serialization of the <paramref name="property"/> read through the <paramref name="accessor"/>.
        /// </summary>
        /// <param name="property">The serialized <see cref="IProperty"/></param>
        /// <param name="accessor">The C# expression that reads the property from the DTO</param>
        /// <param name="localName">The base name of the locals the generated code declares</param>
        /// <returns>The generated code</returns>
        internal static string WriteMessagePackSerialization(IProperty property, string accessor, string localName)
        {
            var sb = new StringBuilder();

            if (property.Type is IDataType)
            {
                if (property.QueryIsEnum())
                {
                    if (property.QueryIsNullable())
                    {
                        sb.AppendLine($"if ({accessor}.HasValue)");
                        sb.AppendLine("{");
                        sb.AppendLine($"    var {localName}Bytes = {property.QueryTypeName()}Provider.ToUtf8LowerBytes({accessor}.Value);");
                        sb.AppendLine($"    writer.WriteString({localName}Bytes);");
                        sb.AppendLine("}");
                        sb.AppendLine("else");
                        sb.AppendLine("{");
                        sb.AppendLine("    writer.WriteNil();");
                        sb.AppendLine("}");
                    }
                    else
                    {
                        sb.AppendLine($"    var {localName}Bytes = {property.QueryTypeName()}Provider.ToUtf8LowerBytes({accessor});");
                        sb.AppendLine($"    writer.WriteString({localName}Bytes);");
                    }
                }
                else if (property.QueryIsEnumerable())
                {
                    sb.AppendLine($"writer.WriteArrayHeader({accessor}.Count);");
                    sb.AppendLine($"for (var i = 0; i < {accessor}.Count; i++)");
                    sb.AppendLine("{");
                    sb.AppendLine($"writer.Write({accessor}[i]);");
                    sb.AppendLine("}");
                }
                else
                {
                    sb.AppendLine($"writer.Write({accessor});");
                }
            }
            else if (property.QueryIsReferenceType())
            {
                if (property.QueryIsEnumerable())
                {
                    sb.AppendLine($"var {localName} = {accessor};");

                    // when properties are not ordered, they are serialized ordered by GUID
                    if (!property.IsOrdered)
                    {
                        sb.AppendLine($"if ({localName}.Count > 1)");
                        sb.AppendLine("{");
                        sb.AppendLine($"{localName}.Sort(GuidComparer);");
                        sb.AppendLine("}");
                        sb.AppendLine();
                    }

                    sb.AppendLine($"writer.WriteArrayHeader({localName}.Count);");
                    sb.AppendLine($"for (var i = 0; i < {localName}.Count; i++)");
                    sb.AppendLine("{");
                    sb.AppendLine($"WriteGuidBin16(ref writer, {localName}[i]);");
                    sb.AppendLine("}");
                }
                else if (property.QueryIsNullable())
                {
                    sb.AppendLine($"if ({accessor}.HasValue)");
                    sb.AppendLine("{");
                    sb.AppendLine($"WriteGuidBin16(ref writer, {accessor}.Value);");
                    sb.AppendLine("}");
                    sb.AppendLine("else");
                    sb.AppendLine("{");
                    sb.AppendLine("writer.WriteNil();");
                    sb.AppendLine("}");
                }
                else
                {
                    sb.AppendLine($"WriteGuidBin16(ref writer, {accessor});");
                }
            }

            sb.AppendLine();

            return sb.ToString();
        }

        /// <summary>
        /// Writes the MessagePack deserialization of the <paramref name="property"/> into the <paramref name="accessor"/>.
        /// </summary>
        /// <param name="property">The deserialized <see cref="IProperty"/></param>
        /// <param name="accessor">The C# expression that stores the property on the DTO</param>
        /// <param name="localName">The base name of the locals the generated code declares</param>
        /// <param name="assignCollection">Whether a collection is assigned to the <paramref name="accessor"/> as a whole instead of filled in place</param>
        /// <returns>The generated code</returns>
        internal static string WriteMessagePackDeserialization(IProperty property, string accessor, string localName, bool assignCollection)
        {
            var sb = new StringBuilder();
            var collection = assignCollection ? $"{localName}Values" : accessor;

            if (property.Type is IDataType)
            {
                if (property.QueryIsEnum())
                {
                    sb.AppendLine($"var {localName}Sequence = reader.ReadStringSequence();");

                    if (property.QueryIsNullable())
                    {
                        sb.AppendLine($"if (!{localName}Sequence.HasValue)");
                        sb.AppendLine("{");
                        sb.AppendLine($"{accessor} = null;");
                        sb.AppendLine("}");
                        sb.AppendLine("else");
                        sb.AppendLine("{");
                        sb.AppendLine($"{accessor} = {property.QueryTypeName()}Provider.Parse({localName}Sequence.Value);");
                        sb.AppendLine("}");
                    }
                    else
                    {
                        sb.AppendLine($"{accessor} = {property.QueryTypeName()}Provider.Parse({localName}Sequence.Value);");
                    }
                }
                else if (property.QueryIsEnumerable())
                {
                    sb.AppendLine("valueLength = reader.ReadArrayHeader();");
                    AppendCollectionStart(sb, accessor, localName, assignCollection, QueryMessagePackElementTypeName(property));
                    sb.AppendLine("for (valueCounter = 0; valueCounter < valueLength; valueCounter++)");
                    sb.AppendLine("{");

                    if (property.QueryIsBool())
                    {
                        sb.AppendLine($"{collection}.Add(reader.ReadBoolean());");
                    }
                    else if (property.QueryCSharpTypeName() == "DateTime")
                    {
                        sb.AppendLine($"{collection}.Add(reader.ReadDateTime());");
                    }
                    else if (property.QueryIsString())
                    {
                        sb.AppendLine($"{collection}.Add(reader.ReadString());");
                    }
                    else if (property.QueryIsNumeric())
                    {
                        sb.AppendLine($"{collection}.Add(reader.{QueryMessagePackNumericReadMethod(property)}());");
                    }

                    sb.Append('}');
                    AppendCollectionEnd(sb, accessor, localName, assignCollection);
                }
                else
                {
                    if (property.QueryIsBool())
                    {
                        sb.AppendLine($"{accessor} = reader.ReadBoolean();");
                    }
                    else if (property.QueryCSharpTypeName() == "DateTime")
                    {
                        sb.AppendLine($"{accessor} = reader.ReadDateTime();");
                    }
                    else if (property.QueryIsString())
                    {
                        sb.AppendLine($"{accessor} = reader.ReadString();");
                    }
                    else if (property.QueryIsNumeric())
                    {
                        sb.AppendLine($"{accessor} = reader.{QueryMessagePackNumericReadMethod(property)}();");
                    }
                }
            }
            else if (property.QueryIsReferenceType())
            {
                if (property.QueryIsEnumerable())
                {
                    sb.AppendLine("valueLength = reader.ReadArrayHeader();");
                    AppendCollectionStart(sb, accessor, localName, assignCollection, QueryMessagePackElementTypeName(property));
                    sb.AppendLine("for (valueCounter = 0; valueCounter < valueLength; valueCounter++)");
                    sb.AppendLine("{");
                    sb.AppendLine($"    {collection}.Add(ReadGuidBin16(ref reader));");
                    sb.AppendLine("}");
                    AppendCollectionEnd(sb, accessor, localName, assignCollection);
                }
                else if (property.QueryIsNullable())
                {
                    sb.AppendLine("if (reader.TryReadNil())");
                    sb.AppendLine("{");
                    sb.AppendLine($"    {accessor} = null;");
                    sb.AppendLine("}");
                    sb.AppendLine("else");
                    sb.AppendLine("{");
                    sb.AppendLine($"    {accessor} = ReadGuidBin16(ref reader);");
                    sb.AppendLine("}");
                }
                else
                {
                    sb.AppendLine($"{accessor} = ReadGuidBin16(ref reader);");
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// Writes the MessagePack serialization of the derived properties of the <paramref name="generatedClass"/>, one block per property.
        /// </summary>
        /// <param name="generatedClass">The <see cref="IClass"/> whose formatter is generated</param>
        /// <returns>The generated code</returns>
        internal static string WriteMessagePackDerivedSerialization(IClass generatedClass)
        {
            var sb = new StringBuilder();

            foreach (var property in generatedClass.QueryDerivedSerializedProperties())
            {
                sb.Append(WriteMessagePackSerialization(property, QueryDerivedDtoAccessor(property), $"{property.Name.LowerCaseFirstLetter()}Derived"));
            }

            return sb.ToString();
        }

        /// <summary>
        /// Writes the MessagePack deserialization cases of the derived properties of the <paramref name="generatedClass"/>, numbered after its stored properties.
        /// </summary>
        /// <param name="generatedClass">The <see cref="IClass"/> whose formatter is generated</param>
        /// <returns>The generated code</returns>
        internal static string WriteMessagePackDerivedDeserializationCases(IClass generatedClass)
        {
            var sb = new StringBuilder();
            var caseIndex = generatedClass.CountAllNonDerivedNonRedefinedProperties();

            foreach (var property in generatedClass.QueryDerivedSerializedProperties())
            {
                caseIndex++;
                sb.AppendLine($"case {caseIndex}:");
                sb.AppendLine(WriteMessagePackDeserialization(property, QueryDerivedDtoAccessor(property), $"{property.Name.LowerCaseFirstLetter()}Derived", true));
                sb.AppendLine("break;");
            }

            return sb.ToString();
        }

        /// <summary>
        /// Queries the C# expression that reads or stores a derived property on the generated DTO.
        /// </summary>
        /// <param name="property">The derived <see cref="IProperty"/>, or a property made derived by redefinition</param>
        /// <returns>The member of the DTO, cast to the owner interface when the property is implemented explicitly</returns>
        private static string QueryDerivedDtoAccessor(IProperty property)
        {
            if (property.IsDerived || property.IsDerivedUnion)
            {
                return $"dto.{property.QueryPropertyNameBasedOnUmlProperties()}";
            }

            var owner = (INamedElement)property.Owner;

            return $"((SysML2.NET.Core.DTO.{Extensions.NamedElementExtensions.QueryNamespace(owner)}.I{owner.Name})dto).{property.QueryPropertyNameBasedOnUmlProperties()}";
        }

        /// <summary>
        /// Queries the C# type name of one element of a multi-valued property as MessagePack reads it.
        /// </summary>
        /// <param name="property">The multi-valued <see cref="IProperty"/></param>
        /// <returns>The element type name</returns>
        private static string QueryMessagePackElementTypeName(IProperty property)
        {
            if (property.QueryIsReferenceType())
            {
                return "Guid";
            }

            if (property.QueryIsBool())
            {
                return "bool";
            }

            if (property.QueryIsString())
            {
                return "string";
            }

            return property.QueryCSharpTypeName() switch
            {
                "DateTime" => "DateTime",
                "double" => "double",
                _ => "int"
            };
        }

        /// <summary>
        /// Queries the <c>MessagePackReader</c> method that reads one value of a numeric property.
        /// </summary>
        /// <param name="property">The numeric <see cref="IProperty"/></param>
        /// <returns>The name of the read method</returns>
        private static string QueryMessagePackNumericReadMethod(IProperty property)
        {
            return property.QueryCSharpTypeName() == "double" ? "ReadDouble" : "ReadInt32";
        }

        /// <summary>
        /// Appends the start of a collection read: either a local list or the capacity of the DTO list filled in place.
        /// </summary>
        /// <param name="sb">The <see cref="StringBuilder"/> to append to</param>
        /// <param name="accessor">The C# expression of the DTO collection</param>
        /// <param name="localName">The base name of the local list</param>
        /// <param name="assignCollection">Whether the collection is read into a local list</param>
        /// <param name="elementTypeName">The C# type name of one element</param>
        private static void AppendCollectionStart(StringBuilder sb, string accessor, string localName, bool assignCollection, string elementTypeName)
        {
            sb.AppendLine(assignCollection
                ? $"var {localName}Values = new List<{elementTypeName}>(valueLength);"
                : $"{accessor}.Capacity = valueLength;");
        }

        /// <summary>
        /// Appends the assignment of a local list to the DTO collection when the collection was read into a local list.
        /// </summary>
        /// <param name="sb">The <see cref="StringBuilder"/> to append to</param>
        /// <param name="accessor">The C# expression of the DTO collection</param>
        /// <param name="localName">The base name of the local list</param>
        /// <param name="assignCollection">Whether the collection was read into a local list</param>
        private static void AppendCollectionEnd(StringBuilder sb, string accessor, string localName, bool assignCollection)
        {
            if (assignCollection)
            {
                sb.AppendLine().Append($"{accessor} = {localName}Values;");
            }
        }

        /// <summary>
        /// Writes the assignment, without its terminating semicolon, that copies a redefined property still serialized under its own name from a POCO to its DTO.
        /// </summary>
        /// <param name="property">The redefined <see cref="IProperty"/></param>
        /// <returns>The generated assignment</returns>
        private static string WriteRedefinedToDtoAssignment(IProperty property)
        {
            var owner = (INamedElement)property.Owner;
            var ownerNamespace = Extensions.NamedElementExtensions.QueryNamespace(owner);
            var propertyName = property.QueryPropertyNameBasedOnUmlProperties();
            var target = $"((Core.DTO.{ownerNamespace}.I{owner.Name})dto).{propertyName}";
            var source = $"((Core.POCO.{ownerNamespace}.I{owner.Name})poco).{propertyName}";

            return QueryToDtoAssignment(property, target, source);
        }

        /// <summary>
        /// Writes the assignment, without its terminating semicolon, that copies the <paramref name="property"/> from a POCO to its DTO.
        /// </summary>
        /// <param name="property">The <see cref="IProperty"/> to copy</param>
        /// <param name="target">The DTO member assigned to</param>
        /// <param name="source">The POCO member read from</param>
        /// <returns>The generated assignment, which maps an absent single reference to <see cref="Guid.Empty"/> when the DTO cannot hold <c>null</c></returns>
        private static string QueryToDtoAssignment(IProperty property, string target, string source)
        {
            if (!property.QueryIsReferenceType())
            {
                return property.QueryIsEnumerable() ? $"{target} = [.. {source}]" : $"{target} = {source}";
            }

            if (property.QueryIsEnumerable())
            {
                return $"{target} = {source}.Select(x => x.Id).ToList()";
            }

            return uml4net.Extensions.PropertyExtensions.QueryIsNullableAndNotString(property)
                ? $"{target} = {source}?.Id"
                : $"{target} = {source}?.Id ?? Guid.Empty";
        }

        /// <summary>
        /// Appends the code that stores a deserialized redefined property, warning when the value disagrees with the one of its redefinition.
        /// </summary>
        /// <param name="sb">The <see cref="StringBuilder"/> to append to</param>
        /// <param name="property">The redefined <see cref="IProperty"/></param>
        /// <param name="generatedClass">The <see cref="IClass"/> whose deserializer is generated</param>
        /// <param name="shape">The deserialization shape of the <paramref name="property"/></param>
        /// <param name="includeDerivedProperties">Whether the generated method also reads derived properties</param>
        private static void AppendRedefinedAssignment(StringBuilder sb, IProperty property, IClass generatedClass, RedefinedDeserializationShape shape, bool includeDerivedProperties)
        {
            var owner = (INamedElement)property.Owner;
            var accessor = $"((SysML2.NET.Core.DTO.{Extensions.NamedElementExtensions.QueryNamespace(owner)}.I{owner.Name})dtoInstance).{property.QueryPropertyNameBasedOnUmlProperties()}";
            var localValue = $"{property.Name}RedefinitionValue";
            var value = shape.Convert.Replace(ValuePlaceholder, localValue);
            var storage = property.QueryRedefinitionChainEnd(generatedClass);
            var isStorageRead = includeDerivedProperties || !(storage.IsDerived || storage.IsDerivedUnion);

            sb.AppendLine($"if ({property.Name}RedefinitionSeen{shape.HasValueCondition.Replace(ValuePlaceholder, localValue)})");
            sb.AppendLine("{");

            if (isStorageRead)
            {
                sb.AppendLine($"if (!{storage.Name}Seen)");
                sb.AppendLine("{");
                sb.AppendLine($"{accessor} = {value};");
                sb.AppendLine("}");
                sb.AppendLine($"else if ({shape.Differs.Replace(LeftPlaceholder, accessor).Replace(RightPlaceholder, value)} && logger.IsEnabled(LogLevel.Warning))");
                sb.AppendLine("{");
                sb.AppendLine($"logger.LogWarning(\"The {property.Name} Json property of the {generatedClass.Name} {{Id}} disagrees with {storage.Name}; the {storage.Name} value is kept\", dtoInstance.Id);");
                sb.AppendLine("}");
            }
            else
            {
                sb.AppendLine($"{accessor} = {value};");
            }

            sb.AppendLine("}");
        }

        /// <summary>
        /// Gets the code fragments that deserialize a redefined property according to its type and multiplicity.
        /// </summary>
        /// <param name="property">The redefined <see cref="IProperty"/></param>
        /// <returns>The <see cref="RedefinedDeserializationShape"/></returns>
        /// <exception cref="HandlebarsException">If the type of the <paramref name="property"/> is not supported</exception>
        private static RedefinedDeserializationShape QueryRedefinedDeserializationShape(IProperty property)
        {
            const string sequenceDiffers = "!System.Linq.Enumerable.SequenceEqual({left}, {right})";
            const string scalarDiffers = "{left} != {right}";

            if (property.QueryIsReferenceType())
            {
                if (property.QueryIsEnumerable())
                {
                    return new RedefinedDeserializationShape("System.Collections.Generic.List<Guid>", "[]",
                        "Utf8JsonReaderHelper.ExpectArrayStart(ref reader);\nwhile (reader.Read() && reader.TokenType != JsonTokenType.EndArray)\n{\nif (Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var redefinitionItem))\n{\n{value}.Add(redefinitionItem);\n}\n}",
                        ValuePlaceholder, string.Empty, sequenceDiffers);
                }

                var convert = uml4net.Extensions.PropertyExtensions.QueryIsNullableAndNotString(property) ? ValuePlaceholder : "({value} ?? Guid.Empty)";

                return new RedefinedDeserializationShape("Guid?", "null",
                    "if (reader.TokenType != JsonTokenType.Null && Utf8JsonReaderHelper.TryReadReferenceIdentifier(ref reader, out var redefinitionItem))\n{\n{value} = redefinitionItem;\n}",
                    convert, string.Empty, scalarDiffers);
            }

            if (property.QueryIsString() && !property.QueryIsEnumerable())
            {
                return new RedefinedDeserializationShape("string", "null", "{value} = Utf8JsonReaderHelper.ReadStringOrNull(ref reader);",
                    ValuePlaceholder, string.Empty, "!string.Equals({left}, {right}, StringComparison.Ordinal)");
            }

            if (property.QueryIsBool() && !property.QueryIsEnumerable())
            {
                return new RedefinedDeserializationShape("bool?", "null", "{value} = Utf8JsonReaderHelper.ReadBoolOrNull(ref reader);",
                    "{value}.Value", " && {value}.HasValue", scalarDiffers);
            }

            throw new HandlebarsException($"The redefined property {property.Name} of type {property.Type.Name} cannot be deserialized under its own name");
        }

        /// <summary>
        /// Gets the DTO setter body that stores the value of a non-derived property, redefined by a derived one, in the member that holds the
        /// end of the redefinition chain in the class context.
        /// </summary>
        /// <param name="redefinedProperty">The non-derived redefined property</param>
        /// <param name="redefinition">The derived property that redefines <paramref name="redefinedProperty"/></param>
        /// <param name="context">The <see cref="IClass"/> context</param>
        /// <returns>The setter implementation</returns>
        private static string GetSetterIntoDerivedStorageForDto(IProperty redefinedProperty, IProperty redefinition, IClass context)
        {
            var storage = redefinition.QueryRedefinitionChainEnd(context);
            var storageName = $"this.{storage.QueryPropertyNameBasedOnUmlProperties()}";

            if (redefinedProperty.QueryIsEnumerable() && storage.QueryIsEnumerable())
            {
                return $"{storageName} = [.. value];";
            }

            if (!redefinedProperty.QueryIsEnumerable() && storage.QueryIsEnumerable())
            {
                return $"{storageName} = [value];";
            }

            if (!redefinedProperty.QueryIsEnumerable())
            {
                return $"{storageName} = value;";
            }

            string emptyValue;

            if (uml4net.Extensions.PropertyExtensions.QueryIsNullableAndNotString(storage) || storage.QueryIsString())
            {
                emptyValue = "null";
            }
            else
            {
                emptyValue = storage.QueryIsReferenceType() ? "Guid.Empty" : "default";
            }

            return $"{storageName} = value.Count != 0 ? value[0] : {emptyValue};";
        }

        /// <summary>
        /// Gets the setter implementation for an <see cref="IProperty"/> that has been redefined, for POCO generation
        /// </summary>
        /// <param name="redefinedProperty">The redefined property</param>
        /// <param name="redefinition">The property that redefines <paramref name="redefinedProperty"/></param>
        /// <param name="context">Gets the <see cref="IClass"/> context</param>
        /// <returns>The setter implementation</returns>
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
                redefinitionPropertyName = $"((SysML2.NET.Core.POCO.{Extensions.NamedElementExtensions.QueryNamespace(owner)}.I{owner.Name})this).{redefinition.QueryPropertyNameBasedOnUmlProperties()}";
            }
            else
            {
                redefinitionPropertyName = $"this.{redefinition.QueryPropertyNameBasedOnUmlProperties()}";
            }

            var redefinitionPropertyTypeName = redefinition.QueryIsReferenceType() ? $"I{redefinition.Type.Name}" : redefinition.QueryCSharpTypeName();

            if (redefinedProperty.QueryIsEnumerable() && redefinition.QueryIsEnumerable())
            {
                return redefinedProperty.Type == redefinition.Type ? $"{redefinitionPropertyName} = value;" : $"{redefinitionPropertyName} = [..value.OfType<{redefinitionPropertyTypeName}>()];";
            }

            var isSameType = redefinedProperty.Type == redefinition.Type;

            return redefinedProperty.QueryIsEnumerable()
                ? GetSetterFromEnumerableForPoco(redefinitionPropertyName, redefinitionPropertyTypeName, isSameType)
                : GetSetterFromSingleValueForPoco(redefinitionPropertyName, redefinitionPropertyTypeName, isSameType);
        }

        /// <summary>
        /// Gets the POCO setter body that assigns the first value of an enumerable redefined property to a single-valued redefinition
        /// </summary>
        /// <param name="redefinitionPropertyName">The accessor of the redefining property</param>
        /// <param name="redefinitionPropertyTypeName">The C# type name of the redefining property</param>
        /// <param name="isSameType">Whether the redefined and redefining properties have the same type</param>
        /// <returns>The setter implementation</returns>
        private static string GetSetterFromEnumerableForPoco(string redefinitionPropertyName, string redefinitionPropertyTypeName, bool isSameType)
        {
            var stringBuilder = new StringBuilder();

            if (isSameType)
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

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Gets the POCO setter body that assigns a single-valued redefined property to its redefinition
        /// </summary>
        /// <param name="redefinitionPropertyName">The accessor of the redefining property</param>
        /// <param name="redefinitionPropertyTypeName">The C# type name of the redefining property</param>
        /// <param name="isSameType">Whether the redefined and redefining properties have the same type</param>
        /// <returns>The setter implementation</returns>
        private static string GetSetterFromSingleValueForPoco(string redefinitionPropertyName, string redefinitionPropertyTypeName, bool isSameType)
        {
            if (isSameType)
            {
                return $"{redefinitionPropertyName} = value;";
            }

            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"if(value is {redefinitionPropertyTypeName} castedValue)");
            stringBuilder.AppendLine("{");
            stringBuilder.AppendLine($"\t{redefinitionPropertyName} = castedValue;");
            stringBuilder.Append('}');

            return stringBuilder.ToString();
        }

        /// <summary>
        /// The code fragments that deserialize a redefined property; <c>{value}</c>, <c>{left}</c> and <c>{right}</c> are placeholders.
        /// </summary>
        /// <param name="LocalType">The C# type of the local holding the read value</param>
        /// <param name="LocalDefault">The initial value of that local</param>
        /// <param name="Read">The statements that read the Json value into the local</param>
        /// <param name="Convert">The expression that converts the local to the type of the property</param>
        /// <param name="HasValueCondition">The extra condition under which the local holds a value to store</param>
        /// <param name="Differs">The expression that tells whether two values of the property differ</param>
        private sealed record RedefinedDeserializationShape(string LocalType, string LocalDefault, string Read, string Convert, string HasValueCondition, string Differs);
    }
}
