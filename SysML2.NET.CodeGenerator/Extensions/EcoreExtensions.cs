// -------------------------------------------------------------------------------------------------
// <copyright file="EcoreExtensions.cs" company="RHEA System S.A.">
// 
//   Copyright 2022-2024 RHEA System S.A.
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

namespace SysML2.NET.CodeGenerator.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using ECoreNetto;
    using ECoreNetto.Extensions;

    using HtmlAgilityPack;

    /// <summary>
    /// Extension methods for ECoreNetto classes
    /// </summary>
    public static class EcoreExtensions
    {
        /// <summary>
        /// Queries the type-name of the <see cref="EStructuralFeature"/>
        /// </summary>
        /// <param name="eStructuralFeature">
        /// The subject <see cref="EStructuralFeature"/>
        /// </param>
        /// <returns>
        /// the name of the type
        /// </returns>
        public static string QueryTypeName(this EStructuralFeature eStructuralFeature)
        {
            var typeName = "";

            if (eStructuralFeature is EAttribute eAttribute)
            {
                typeName = eAttribute.EType.Name;

            }
            else if (eStructuralFeature is EReference eReference)
            {
                typeName = eReference.EType.Name;
            }

            return typeName;
        }

        /// <summary>
        /// A mapping of the known SysML value types to C# types
        /// </summary>
        private static readonly Dictionary<string, string> CSharpTypeMapping = new Dictionary<string, string>
        {
            {"Boolean", "bool"},
            {"Integer", "int"},
            {"Real", "double"},
            {"UnlimitedNatural", "int"},
            {"String", "string"},
            {"EDouble", "double"}
        };

        /// <summary>
        /// Queries the C# type-name of the <see cref="EStructuralFeature"/>
        /// </summary>
        /// <param name="eStructuralFeature">
        /// The subject <see cref="EStructuralFeature"/>
        /// </param>
        /// <returns>
        /// the C# name of the type
        /// </returns>
        public static string QueryCSharpTypeName(this EStructuralFeature eStructuralFeature)
        {
            var typeName = "";

            if (eStructuralFeature is EAttribute eAttribute)
            {
                if (eAttribute.EType is EEnum)
                {
                    typeName = eAttribute.EType.Name;
                }
                else if (!CSharpTypeMapping.TryGetValue(eAttribute.EType.Name, out typeName))
                {
                    throw new KeyNotFoundException($"the {eAttribute.Name}.{eAttribute.EType.Name} is not a registered Type");
                }
            }
            else if (eStructuralFeature is EReference eReference)
            {
                typeName = eReference.EType.Name;
            }

            return typeName;
        }

        /// <summary>
        /// A mapping of the known SysML value types to GraphQL types
        /// </summary>
        private static readonly Dictionary<string, string> GraphQLTypeMapping = new Dictionary<string, string>
        {
            {"Boolean", "Boolean"},
            {"Integer", "Int"},
            {"Real", "Float"},
            {"UnlimitedNatural", "Int"},
            {"String", "String"},
            {"EDouble", "Float"}
        };

        /// <summary>
        /// Queries the GraphQL type-name of the <see cref="EStructuralFeature"/>
        /// </summary>
        /// <param name="eStructuralFeature">
        /// The subject <see cref="EStructuralFeature"/>
        /// </param>
        /// <returns>
        /// the C# name of the type
        /// </returns>
        public static string QueryGraphQLTypeName(this EStructuralFeature eStructuralFeature)
        {
            var typeName = "";

            if (eStructuralFeature is EAttribute eAttribute)
            {
                if (eAttribute.EType is EEnum)
                {
                    typeName = eAttribute.EType.Name;
                }
                else if (!GraphQLTypeMapping.TryGetValue(eAttribute.EType.Name, out typeName))
                {
                    throw new KeyNotFoundException($"the {eAttribute.Name}.{eAttribute.EType.Name} is not a registered Type");
                }
            }
            else if (eStructuralFeature is EReference eReference)
            {
                typeName = eReference.EType.Name;
            }

            return typeName;
        }

        /// <summary>
        /// Queries whether the <see cref="EStructuralFeature"/> Type is a boolean
        /// </summary>
        /// <param name="eStructuralFeature">
        /// the subject <see cref="EStructuralFeature"/>
        /// </param>
        /// <returns>
        /// true if it maps, false if not
        /// </returns>
        public static bool QueryIsBool(this EStructuralFeature eStructuralFeature)
        {
            if (eStructuralFeature is EAttribute eAttribute)
            {
                if (eAttribute.EType is EEnum)
                {
                    return false;
                }

                if (eAttribute.EType.Name == "Boolean")
                {
                    return true; 
                }
            }

            return false;
        }

        /// <summary>
        /// Queries whether the <see cref="EStructuralFeature"/> Type is numeric
        /// </summary>
        /// <param name="eStructuralFeature">
        /// the subject <see cref="EStructuralFeature"/>
        /// </param>
        /// <returns>
        /// true if it maps, false if not
        /// </returns>
        public static bool QueryIsNumeric(this EStructuralFeature eStructuralFeature)
        {
            if (eStructuralFeature is EAttribute eAttribute)
            {
                if (eAttribute.EType is EEnum)
                {
                    return false;
                }

                if (eAttribute.EType.Name == "Integer" 
                    || eAttribute.EType.Name == "Real"
                    || eAttribute.EType.Name == "UnlimitedNatural"
                    || eAttribute.EType.Name == "EDouble")
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Queries whether the <see cref="EStructuralFeature"/> Type is an integer
        /// </summary>
        /// <param name="eStructuralFeature">
        /// the subject <see cref="EStructuralFeature"/>
        /// </param>
        /// <returns>
        /// true if it maps, false if not
        /// </returns>
        public static bool QueryIsInt(this EStructuralFeature eStructuralFeature)
        {
            if (eStructuralFeature is EAttribute eAttribute)
            {
                if (eAttribute.EType is EEnum)
                {
                    return false;
                }

                if (eAttribute.EType.Name == "Integer")
                {
                    return true;
                }
            }

            return false;
        }
        
        /// <summary>
        /// Queries whether the <see cref="EStructuralFeature"/> Type is a double
        /// </summary>
        /// <param name="eStructuralFeature">
        /// the subject <see cref="EStructuralFeature"/>
        /// </param>
        /// <returns>
        /// true if it maps, false if not
        /// </returns>
        public static bool QueryIsDouble(this EStructuralFeature eStructuralFeature)
        {
            if (eStructuralFeature is EAttribute eAttribute)
            {
                if (eAttribute.EType is EEnum)
                {
                    return false;
                }

                if (eAttribute.EType.Name == "Real" || eAttribute.EType.Name == "EDouble")
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Queries whether the <see cref="EStructuralFeature"/> Type is string
        /// </summary>
        /// <param name="eStructuralFeature">
        /// the subject <see cref="EStructuralFeature"/>
        /// </param>
        /// <returns>
        /// true if it maps, false if not
        /// </returns>
        public static bool QueryIsString(this EStructuralFeature eStructuralFeature)
        {
            if (eStructuralFeature is EAttribute eAttribute)
            {
                if (eAttribute.EType is EEnum)
                {
                    return false;
                }

                if (eAttribute.EType.Name == "String")
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// returns the <see cref="ENamedElement"/> ordered by Name.
        /// </summary>
        /// <param name="namedElements">
        /// the source <see cref="IEnumerable{ENamedElement}"/>
        /// </param>
        /// <returns>
        /// the sorted <see cref="IEnumerable{ENamedElement}"/>
        /// </returns>
        public static IEnumerable<ENamedElement> QuerySortedByName(IEnumerable<ENamedElement> namedElements)
        {
            return namedElements.OrderBy(x => x.Name).ToList();
        }
        
        /// <summary>
        /// Queries whether the <see cref="EStructuralFeature"/> is nullable
        /// </summary>
        /// <param name="eStructuralFeature">
        /// The subject <see cref="EStructuralFeature"/>
        /// </param>
        /// <returns>
        /// true if <see cref="EStructuralFeature.LowerBound"/> = 0, false if not
        /// </returns>
        public static bool QueryIsNullable(this EStructuralFeature eStructuralFeature)
        {
            return eStructuralFeature.LowerBound == 0 && !eStructuralFeature.QueryIsEnumerable();
        }

        /// <summary>
        /// Queries whether the <see cref="EStructuralFeature"/> is a scalar
        /// </summary>
        /// <param name="eStructuralFeature">
        /// The subject <see cref="EStructuralFeature"/>
        /// </param>
        /// <returns>
        /// true if <see cref="ETypedElement.LowerBound"/> and <see cref="EStructuralFeature.UpperBound"/> = 1, false if not
        /// </returns>
        public static bool QueryIsScalar(this EStructuralFeature eStructuralFeature)
        {
            if (eStructuralFeature.QueryIsString() && eStructuralFeature.UpperBound == 1)
            {
                return true;
            }

            return eStructuralFeature.LowerBound == 1 && eStructuralFeature.UpperBound == 1;
        }
        
        /// <summary>
        /// Queries whether the <see cref="EStructuralFeature"/> is has a default value
        /// </summary>
        /// <param name="eStructuralFeature">
        /// The subject <see cref="EStructuralFeature"/>
        /// </param>
        /// <returns>
        /// true when the <see cref="EStructuralFeature.DefaultValueLiteral"/> contains a value
        /// </returns>
        public static string QueryDefaultValue(this EStructuralFeature eStructuralFeature)
        {
            if (eStructuralFeature is EAttribute eAttribute)
            {
                if (string.IsNullOrEmpty(eStructuralFeature.DefaultValueLiteral))
                {
                    throw new InvalidOperationException("the structural feature does not have a default value");
                }

                return eAttribute.EType is EEnum ? $"{eStructuralFeature.QueryCSharpTypeName()}.{eStructuralFeature.DefaultValueLiteral.CapitalizeFirstLetter()}" : eStructuralFeature.DefaultValueLiteral;
            }

            throw new NotSupportedException("");
        }

        /// <summary>
        /// Queries the documentation from the <see cref="EModelElement"/> and
        /// returns it as a string
        /// </summary>
        /// <param name="eModelElement"></param>
        /// <returns></returns>
        public static IEnumerable<string> QueryDocumentation(this EModelElement eModelElement)
        {
            var annotation = eModelElement.EAnnotations.FirstOrDefault(x => x.Details.ContainsKey("documentation"));
            if (annotation == null)
            {
                return Enumerable.Empty<string>();
            }

            if (annotation.Details.TryGetValue("documentation", out var documentation))
            {
                var unwantedTags = new List<string> { "p", "code", "em", "tt" };

                var result = documentation.RemoveUnwantedHtmlTags(unwantedTags).ReplaceLineEndings("");
                
                var splitLines = result.SplitToLines(100);

                return splitLines;
            }

            return Enumerable.Empty<string>();
        }

        /// <summary>
        /// Queries the documentation from the <see cref="EModelElement"/> and
        /// returns it as a string
        /// </summary>
        /// <param name="eModelElement"></param>
        /// <returns></returns>
        public static string QueryRawDocumentation(this EModelElement eModelElement)
        {
            var annotation = eModelElement.EAnnotations.FirstOrDefault(x => x.Details.ContainsKey("documentation"));
            if (annotation == null)
            {
                return string.Empty;
            }

            if (annotation.Details.TryGetValue("documentation", out var documentation))
            {
                var unwantedTags = new List<string> { "p", "code", "em", "tt" };

                var result = documentation.RemoveUnwantedHtmlTags(unwantedTags).ReplaceLineEndings("");

                return result;
            }

            return string.Empty;
        }

        /// <summary>
        /// removes the specified html tags from the <paramref name="html"/>
        /// </summary>
        /// <param name="html">
        /// the string from which the unwanted html tags are to be removed
        /// </param>
        /// <param name="unwantedTags">
        /// list of unwanted html tags
        /// </param>
        /// <returns>
        /// a cleaned up string
        /// </returns>
        public static string RemoveUnwantedHtmlTags(this string html, List<string> unwantedTags)
        {
            if (string.IsNullOrEmpty(html))
            {
                return html;
            }

            var document = new HtmlDocument();
            document.LoadHtml(html);

            HtmlNodeCollection tryGetNodes = document.DocumentNode.SelectNodes("./*|./text()");

            if (tryGetNodes == null || !tryGetNodes.Any())
            {
                return html;
            }

            var nodes = new Queue<HtmlNode>(tryGetNodes);

            while (nodes.Count > 0)
            {
                var node = nodes.Dequeue();
                var parentNode = node.ParentNode;

                var childNodes = node.SelectNodes("./*|./text()");

                if (childNodes != null)
                {
                    foreach (var child in childNodes)
                    {
                        nodes.Enqueue(child);
                    }
                }

                if (unwantedTags.Any(tag => tag == node.Name))
                {
                    if (childNodes != null)
                    {
                        foreach (var child in childNodes)
                        {
                            parentNode.InsertBefore(child, node);
                        }
                    }

                    parentNode.RemoveChild(node);

                }
            }

            return document.DocumentNode.InnerHtml;
        }
    }
}
