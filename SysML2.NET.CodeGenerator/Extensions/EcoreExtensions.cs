// -------------------------------------------------------------------------------------------------
// <copyright file="EcoreExtensions.cs" company="RHEA System S.A.">
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

namespace SysML2.NET.CodeGenerator.Extensions
{
    using System.Collections.Generic;
    using System.Linq;

    using ECoreNetto;

    using HtmlAgilityPack;

    /// <summary>
    /// Extension methods for ECoreNetto classes
    /// </summary>
    public static class EcoreExtensions
    {
        /// <summary>
        /// A mapping of the known SysML value types to C# types
        /// </summary>
        private static readonly Dictionary<string, string> TypeMapping = new Dictionary<string, string>
        {
            {"Boolean", "bool"},
            {"Integer", "int"},
            {"Real", "double"},
            {"UnlimitedNatural", "int"},
            {"String", "string"},
            {"EDouble", "double"}
        };

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
                if (eAttribute.EType is EEnum)
                {
                    typeName = eAttribute.EType.Name;
                }
                else if (!TypeMapping.TryGetValue(eAttribute.EType.Name, out typeName))
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
        /// Queries whether the type of the <see cref="EStructuralFeature"/> is an <see cref="EEnum"/>
        /// </summary>
        /// <param name="eStructuralFeature">
        /// The subject <see cref="EStructuralFeature"/>
        /// </param>
        /// <returns>
        /// true of the type is a <see cref="EEnum"/>, false if not
        /// </returns>
        public static bool QueryIsEnum(this EStructuralFeature eStructuralFeature)
        {
            if (eStructuralFeature is EAttribute eAttribute)
            {
                return eAttribute.EType is EEnum;
            }
        
            return false;
        }

        /// <summary>
        /// Queries whether the <see cref="EStructuralFeature"/> Type maps to a C# bool
        /// </summary>
        /// <param name="eStructuralFeature">
        /// the subject <see cref="EStructuralFeature"/>
        /// </param>
        /// <returns>
        /// true if it maps, false if not
        /// </returns>
        public static bool QueryIsBool(this EStructuralFeature eStructuralFeature)
        {
            return eStructuralFeature.QueryTypeName() == "bool";
        }

        /// <summary>
        /// Queries whether the <see cref="EStructuralFeature"/> Type maps to a C# int or double
        /// </summary>
        /// <param name="eStructuralFeature">
        /// the subject <see cref="EStructuralFeature"/>
        /// </param>
        /// <returns>
        /// true if it maps, false if not
        /// </returns>
        public static bool QueryIsNumeric(this EStructuralFeature eStructuralFeature)
        {
            var typeName = eStructuralFeature.QueryTypeName();
            
            if (typeName == "int" || typeName == "double")
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Queries whether the <see cref="EStructuralFeature"/> Type maps to a C# string
        /// </summary>
        /// <param name="eStructuralFeature">
        /// the subject <see cref="EStructuralFeature"/>
        /// </param>
        /// <returns>
        /// true if it maps, false if not
        /// </returns>
        public static bool QueryIsString(this EStructuralFeature eStructuralFeature)
        {
            return eStructuralFeature.QueryTypeName() == "string";
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
        /// Queries whether the <see cref="EStructuralFeature"/> is Enumerable
        /// </summary>
        /// <param name="eStructuralFeature">
        /// The subject <see cref="EStructuralFeature"/>
        /// </param>
        /// <returns>
        /// true if <see cref="EStructuralFeature.UpperBound"/> = -1 or > 1, false if not
        /// </returns>
        public static bool QueryIsEnumerable(this EStructuralFeature eStructuralFeature)
        {
            return eStructuralFeature.UpperBound is -1 or > 1;
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
            if (eStructuralFeature.QueryTypeName() == "string")
            {
                return false;
            }

            return eStructuralFeature.LowerBound == 0 && !eStructuralFeature.QueryIsEnumerable();
        }

        public static bool QueryIsScalar(this EStructuralFeature eStructuralFeature)
        {
            if (eStructuralFeature.QueryTypeName() == "string" && eStructuralFeature.UpperBound == 1)
            {
                return true;
            }

            return eStructuralFeature.LowerBound == 1 && eStructuralFeature.UpperBound == 1;
        }

        /// <summary>
        /// Queries whether the <see cref="EStructuralFeature"/> is an <see cref="EAttribute"/> or not
        /// </summary>
        /// <param name="structuralFeature">
        /// The subject <see cref="EStructuralFeature"/>
        /// </param>
        /// <returns>
        /// true when the <paramref name="structuralFeature"/> is an instance of <see cref="EAttribute"/>, false if not.
        /// </returns>
        public static bool QueryIsAttribute(this EStructuralFeature structuralFeature)
        {
            if (structuralFeature is EAttribute)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Queries whether the <see cref="EStructuralFeature"/> is an <see cref="EReference"/> or not
        /// </summary>
        /// <param name="structuralFeature">
        /// The subject <see cref="EStructuralFeature"/>
        /// </param>
        /// <returns>
        /// true when the <paramref name="structuralFeature"/> is an instance of <see cref="EReference"/>, false if not.
        /// </returns>
        public static bool QueryIsReference(this EStructuralFeature structuralFeature)
        {
            if (structuralFeature is EReference)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Queries the documentation from the <see cref="EModelElement"/> and
        /// returns it as a string
        /// </summary>
        /// <param name="eModelElement"></param>
        /// <returns></returns>
        public static IEnumerable<string> QueryDocumentation(this EModelElement eModelElement)
        {
            var annotation = eModelElement.EAnnotations.FirstOrDefault();
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

        /// <summary>
        /// splits a string into multiple lines
        /// </summary>
        /// <param name="input">
        /// the subject input string
        /// </param>
        /// <param name="maximumLineLength">
        /// the maximum length of a line
        /// </param>
        /// <returns>
        /// an <see cref="IEnumerable{string}"/>
        /// </returns>
        public static IEnumerable<string> SplitToLines(this string input, int maximumLineLength)
        {
            var words = input.Split(' ');
            var line = words.First();
            foreach (var word in words.Skip(1))
            {
                var test = $"{line} {word}";
                if (test.Length > maximumLineLength)
                {
                    yield return line;
                    line = word;
                }
                else
                {
                    line = test;
                }
            }

            yield return line.Trim();
        }
    }
}
