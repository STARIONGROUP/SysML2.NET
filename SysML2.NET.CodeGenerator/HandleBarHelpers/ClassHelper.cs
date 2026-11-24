// ------------------------------------------------------------------------------------------------
// <copyright file="ClassHelper.cs" company="Starion Group S.A.">
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
    using System.Linq;

    using HandlebarsDotNet;

    using SysML2.NET.CodeGenerator.Extensions;

    using uml4net.CommonStructure;
    using uml4net.Extensions;
    using uml4net.SimpleClassifiers;
    using uml4net.StructuredClassifiers;

    /// <summary>
    /// A handlebars block helper for the <see cref="IClass" /> interface
    /// </summary>
    public static class ClassHelper
    {
        /// <summary>
        /// The namespace that carries the root <c>Element</c> types every generated serializer refers to
        /// </summary>
        private const string RootElementsNameSpace = "Root.Elements";

        /// <summary>
        /// Registers the <see cref="ClassHelper" />
        /// </summary>
        /// <param name="handlebars">
        /// The <see cref="IHandlebars" /> context with which the helper needs to be registered
        /// </param>
        public static void RegisterClassHelper(this IHandlebars handlebars)
        {
            handlebars.RegisterHelper("Class.WriteEnumerationNameSpaces", (writer, context, _) =>
            {
                if (context.Value is not IClass @class)
                {
                    throw new ArgumentException("supposed to be IClass");
                }

                var uniqueNamespaces = new HashSet<string>();

                var allProperties = @class.QueryAllProperties();

                foreach (var prop in allProperties.Where(x => x.QueryIsEnum()))
                {
                    uniqueNamespaces.Add(Extensions.NamedElementExtensions.QueryNamespace(prop.Type));
                }

                var orderedNamespaces = uniqueNamespaces.Order().ToList();

                foreach (var orderedNamespace in orderedNamespaces)
                {
                    writer.WriteSafeString($"using SysML2.NET.Core.{orderedNamespace} ;{Environment.NewLine}");
                }
            });
            
            handlebars.RegisterHelper("Class.WriteEnumerationNameSpacesWithOperation", (writer, context, _) =>
            {
                if (context.Value is not IClass @class)
                {
                    throw new ArgumentException("supposed to be IClass");
                }

                var uniqueNamespaces = new HashSet<string>();

                var allProperties = @class.QueryAllProperties();

                foreach (var prop in allProperties.Where(x => x.QueryIsEnum()))
                {
                    uniqueNamespaces.Add(Extensions.NamedElementExtensions.QueryNamespace(prop.Type));
                }

                var parameters = @class.QueryAllOperations().SelectMany(x => x.OwnedParameter);

                foreach (var enumeration in parameters.Where(x => x.Type is IEnumeration).Select(x => x.Type as IEnumeration))
                {
                    uniqueNamespaces.Add(Extensions.NamedElementExtensions.QueryNamespace(enumeration));
                }

                var orderedNamespaces = uniqueNamespaces.Order().ToList();

                foreach (var orderedNamespace in orderedNamespaces)
                {
                    writer.WriteSafeString($"using SysML2.NET.Core.{orderedNamespace} ;{Environment.NewLine}");
                }
            });

            handlebars.RegisterHelper("Class.WriteEnumerationNameSpace", (writer, context, _) =>
            {
                if (context.Value is not IEnumeration enumeration)
                {
                    throw new ArgumentException("#Class.WriteEnumerationNameSpace supposed to be an IEnumeration");
                }

                writer.WriteSafeString($"using SysML2.NET.Core.{Extensions.NamedElementExtensions.QueryNamespace(enumeration)};{Environment.NewLine}");
            });

            handlebars.RegisterHelper("Class.WriteNameSpaces", (writer, context, arguments) =>
            {
                if (context.Value is not IClass @class)
                {
                    throw new ArgumentException("supposed to be IClass");
                }

                if (arguments.Length != 2)
                {
                    throw new ArgumentException("#Class.WriteNameSpaces Expects to have 2 arguments");
                }

                var namespacePrefix = arguments[1].ToString();

                var uniqueNamespaces = QueryReferencedNameSpaces(@class, namespacePrefix);

                uniqueNamespaces.Remove(Extensions.NamedElementExtensions.QueryNamespace(@class));
                var orderedNamespaces = uniqueNamespaces.Order().ToList();

                foreach (var orderedNamespace in orderedNamespaces)
                {
                    writer.WriteSafeString($"using SysML2.NET.Core.{namespacePrefix}.{orderedNamespace} ;{Environment.NewLine}");
                }
            });

            handlebars.RegisterHelper("Class.WriteAllPocoNameSpaces", (writer, context, _) =>
            {
                if (context.Value is not IClass @class)
                {
                    throw new ArgumentException("#Class.WriteAllPocoNameSpaces supposed to be IClass");
                }

                var uniqueNamespaces = QueryReferencedNameSpaces(@class, "POCO");

                uniqueNamespaces.Add(Extensions.NamedElementExtensions.QueryNamespace(@class));
                uniqueNamespaces.Add(RootElementsNameSpace);

                foreach (var orderedNamespace in uniqueNamespaces.Order())
                {
                    writer.WriteSafeString($"using SysML2.NET.Core.POCO.{orderedNamespace} ;{Environment.NewLine}");
                }
            });

            // writes the count of non-derived properties of the IClass
            handlebars.RegisterHelper("Class.WriteCountAllNonDerivedProperties", (writer, context, _) => {

                if (context.Value is not IClass @class)
                {
                    throw new ArgumentException("supposed to be IClass");
                }

                writer.WriteSafeString(@class.CountAllNonDerivedProperties());
            });

            // writes the count of non-derived and non Redefined properties of the IClass
            handlebars.RegisterHelper("Class.WriteCountAllNonDerivedNonRedefinedProperties", (writer, context, _) => 
            {
                if (context.Value is not IClass @class)
                {
                    throw new ArgumentException("supposed to be IClass");
                }

                writer.WriteSafeString(@class.CountAllNonDerivedNonRedefinedProperties());
            });

            handlebars.RegisterHelper("Class.WriteCountDerivedSerializedProperties", (writer, context, _) =>
            {
                if (context.Value is not IClass @class)
                {
                    throw new ArgumentException("Class.WriteCountDerivedSerializedProperties context supposed to be IClass");
                }

                writer.WriteSafeString(@class.QueryDerivedSerializedProperties().Count);
            });

            handlebars.RegisterHelper("Class.QuerySerializedProperties", (_, arguments) =>
            {
                var (@class, kinds) = QuerySerializedPropertiesArguments("Class.QuerySerializedProperties", arguments);

                return @class.QuerySerializedProperties(kinds);
            });

            handlebars.RegisterHelper("Class.WriteCountSerializedProperties", (writer, _, arguments) =>
            {
                var (@class, kinds) = QuerySerializedPropertiesArguments("Class.WriteCountSerializedProperties", arguments);

                writer.WriteSafeString(@class.QuerySerializedProperties(kinds).Count);
            });

            handlebars.RegisterHelper("Class.WriteMessagePackDerivedSerialization", (writer, context, _) =>
            {
                if (context.Value is not IClass @class)
                {
                    throw new ArgumentException("Class.WriteMessagePackDerivedSerialization context supposed to be IClass");
                }

                writer.WriteSafeString(PropertyHelper.WriteMessagePackDerivedSerialization(@class));
            });

            handlebars.RegisterHelper("Class.WriteMessagePackDerivedDeserializationCases", (writer, context, _) =>
            {
                if (context.Value is not IClass @class)
                {
                    throw new ArgumentException("Class.WriteMessagePackDerivedDeserializationCases context supposed to be IClass");
                }

                writer.WriteSafeString(PropertyHelper.WriteMessagePackDerivedDeserializationCases(@class));
            });
            
            handlebars.RegisterHelper("Class.WriteInternalInterface", (writer, context, _) =>
            {
                if (context.Value is not IClass umlClass)
                {
                    throw new ArgumentException("Class.WriteInternalInterface context supposed to be IClass");
                }

                var interfaceName = umlClass.QueryInternalInterfaceName();

                if (!string.IsNullOrWhiteSpace(interfaceName))
                {
                    writer.WriteSafeString($", {interfaceName}");
                }
            });
            
            handlebars.RegisterHelper("Class.QueryNonDerivedCompositeAggregation", (context, _) =>
            {
                if (context.Value is not IClass umlClass)
                {
                    throw new ArgumentException("Class.QueryNonDerivedCompositeAggregation context supposed to be IClass");
                }

                var properties = umlClass.QueryAllProperties();
                return properties.Where(x => x.IsComposite && !x.IsDerived);
            });

            handlebars.RegisterHelper("Class.QueryAllPropertiesSorted", (context, _) =>
            {
                if (context.Value is not IClass umlClass)
                {
                    throw new ArgumentException("Class.QueryAllPropertiesSorted context supposed to be IClass");
                }

                return umlClass.QueryAllProperties().OrderBy(x => x.Name);
            });
        }

        /// <summary>
        /// Queries the distinct namespaces that the generated code for the specified <see cref="IClass" /> refers to
        /// </summary>
        /// <param name="class">
        /// The <see cref="IClass" /> for which the referenced namespaces are queried
        /// </param>
        /// <param name="namespacePrefix">
        /// The generated-code flavour, either <c>DTO</c> or <c>POCO</c>
        /// </param>
        /// <returns>
        /// The distinct namespaces, excluding the <c>SysML2.NET.Core.{namespacePrefix}</c> prefix
        /// </returns>
        private static HashSet<string> QueryReferencedNameSpaces(IClass @class, string namespacePrefix)
        {
            var superClasses = @class.SuperClass;

            var uniqueNamespaces = superClasses
                .Select(Extensions.NamedElementExtensions.QueryNamespace)
                .ToHashSet();

            if (namespacePrefix != "POCO")
            {
                return uniqueNamespaces;
            }

            uniqueNamespaces.UnionWith(@class.QueryAllProperties()
                .Where(x => x.QueryIsReferenceType())
                .Select(x => Extensions.NamedElementExtensions.QueryNamespace(x.Type)));

            uniqueNamespaces.UnionWith(superClasses
                .SelectMany(x => x.QueryAllProperties().Where(y => y.IsDerived || y.IsDerivedUnion))
                .Select(x => x.Possessor)
                .OfType<INamedElement>()
                .Select(Extensions.NamedElementExtensions.QueryNamespace));

            uniqueNamespaces.UnionWith(@class.QueryAllOperations()
                .SelectMany(x => x.OwnedParameter)
                .Select(x => x.Type)
                .OfType<IClass>()
                .Select(Extensions.NamedElementExtensions.QueryNamespace));

            return uniqueNamespaces;
        }

        /// <summary>
        /// Reads the arguments of a serialized-properties helper: the <see cref="IClass" /> followed by the names of the <see cref="SerializedPropertyKind" />s
        /// </summary>
        /// <param name="helperName">The name of the helper, used in the error message</param>
        /// <param name="arguments">The helper arguments</param>
        /// <returns>The <see cref="IClass" /> and the selected <see cref="SerializedPropertyKind" />s</returns>
        /// <exception cref="ArgumentException">When the first argument is not an <see cref="IClass" /> or a name is not a <see cref="SerializedPropertyKind" /></exception>
        private static (IClass Class, SerializedPropertyKind[] Kinds) QuerySerializedPropertiesArguments(string helperName, Arguments arguments)
        {
            if (arguments.Length < 2 || arguments[0] is not IClass @class)
            {
                throw new ArgumentException($"{helperName} expects an IClass followed by at least one SerializedPropertyKind name");
            }

            var kinds = arguments.Skip(1)
                .Select(argument => Enum.TryParse<SerializedPropertyKind>(argument as string, out var kind)
                    ? kind
                    : throw new ArgumentException($"{helperName}: '{argument}' is not a SerializedPropertyKind"))
                .ToArray();

            return (@class, kinds);
        }
    }
}
