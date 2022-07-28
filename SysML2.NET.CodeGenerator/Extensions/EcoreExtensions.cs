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
        /// Queries whether the <see cref="EStructuralFeature"/> is an <see cref="EAttribute"/> or not
        /// </summary>
        /// <param name="structuralFeature">
        /// The subject <see cref="EStructuralFeature"/>
        /// </param>
        /// <returns>
        /// true when the <paramref name="structuralFeature"/> is an instance of <see cref="EAttribute"/>, false if not.
        /// </returns>
        public static bool QueryIsAttribute(EStructuralFeature structuralFeature)
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
        public static bool QueryIsReference(EStructuralFeature structuralFeature)
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

            yield return line;
        }
    }
}
