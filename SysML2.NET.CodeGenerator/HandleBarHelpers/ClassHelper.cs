// -------------------------------------------------------------------------------------------------
// <copyright file="ClassHelper.cs" company="Starion Group S.A.">
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
    using System.Collections.Generic;
    using System.Linq;

    using HandlebarsDotNet;

    using Microsoft.CodeAnalysis.CSharp.Syntax;

    using SysML2.NET.CodeGenerator.Enumeration;
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
        /// Registers the <see cref="ClassHelper" />
        /// </summary>
        /// <param name="handlebars">
        /// The <see cref="IHandlebars" /> context with which the helper needs to be registered
        /// </param>
        public static void RegisterClassHelper(this IHandlebars handlebars)
        {
            handlebars.RegisterHelper("Class.WriteEnumerationNameSpaces", (writer, context, arguments) =>
            {
                if (context.Value is not IClass @class)
                {
                    throw new ArgumentException("supposed to be IClass");
                }

                var modelKind = arguments[1].ToString();

                var uniqueNamespaces = new HashSet<string>();

                var allProperties = @class.QueryAllProperties();

                foreach (var prop in allProperties.Where(x => x.QueryIsEnum()))
                {
                    uniqueNamespaces.Add(Extensions.NamedElementExtensions.QueryNamespace(prop.Type));
                }

                var orderedNamespaces = uniqueNamespaces.Order().ToList();

                foreach (var orderedNamespace in orderedNamespaces)
                {
                    writer.WriteSafeString($"using SysML2.NET.{modelKind}.{orderedNamespace} ;{Environment.NewLine}");
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

                if (arguments.Length != 3)
                {
                    throw new ArgumentException("#Class.WriteNameSpaces Expects to have 3 arguments");
                }

                var namespacePrefix = arguments[1].ToString();
                var modelKind = Enum.Parse<ModelKind>(arguments[2].ToString()!);

                var superClasses = @class.SuperClass;

                var uniqueNamespaces = new HashSet<string>();

                foreach (var superClass in superClasses)
                {
                    uniqueNamespaces.Add($"{modelKind}.{namespacePrefix}.{Extensions.NamedElementExtensions.QueryNamespace(superClass)}");
                }

                if (@class.QueryDoesImplementsDataInterface())
                {
                    uniqueNamespaces.Add("Common");
                }

                if (namespacePrefix == "POCO")
                {
                    var allProperties = @class.QueryAllProperties();

                    foreach (var prop in allProperties)
                    {
                        if (prop.Type != null && prop.QueryIsReferenceProperty())
                        {
                            if (prop.Type is Interface)
                            {
                                uniqueNamespaces.Add("Common");
                            }
                            else
                            {
                                var modelKindTarget = modelKind;
                                
                                if (prop.Type.DocumentName != prop.DocumentName)
                                {
                                    modelKindTarget = (ModelKind)((int)modelKind ^ 1);
                                }

                                uniqueNamespaces.Add($"{modelKindTarget}.{namespacePrefix}.{Extensions.NamedElementExtensions.QueryNamespace(prop.Type)}");
                            }
                        }
                    }

                    var interfaceDerivedProperties =
                        superClasses.SelectMany(x => x.QueryAllProperties()
                            .Where(y => y.IsDerived || y.IsDerivedUnion))
                            .ToList();

                    foreach (var interfaceDerivedProperty in interfaceDerivedProperties)
                    {
                        if (interfaceDerivedProperty.Possessor is INamedElement owner)
                        {
                            var @namespace = Extensions.NamedElementExtensions.QueryNamespace(owner);
                            uniqueNamespaces.Add($"{modelKind}.{namespacePrefix}.{@namespace}");
                        }
                    }
                }

                uniqueNamespaces.Remove($"{modelKind}.{namespacePrefix}.{Extensions.NamedElementExtensions.QueryNamespace(@class)}");
                
                var orderedNamespaces = uniqueNamespaces.Order().ToList();

                foreach (var orderedNamespace in orderedNamespaces)
                {
                    writer.WriteSafeString($"using SysML2.NET.{orderedNamespace};{Environment.NewLine}");
                }
            });
            
            handlebars.RegisterHelper("Class.DoesImplementsDataInterface", (context, arguments) =>
            {
                if (context.Value is not IClass @class)
                {
                    throw new ArgumentException("#Class.DoesImplementsDataInterface Context supposed to be IClass");
                }

                if (arguments.Length != 2)
                {
                    throw new ArgumentException("#Class.DoesImplementsDataInterface expects to have 2 arguments");
                }

                var modelKind = Enum.Parse<ModelKind>(arguments[1].ToString()!);

                return modelKind == ModelKind.Core || @class.QueryDoesImplementsDataInterface();
            });
        }
    }
}
