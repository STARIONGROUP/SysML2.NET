// -------------------------------------------------------------------------------------------------
// <copyright file="EnumerationHelper.cs" company="Starion Group S.A.">
//
//    Copyright 2022-2026 Starion Group S.A.
//
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
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

namespace SysML2.NET.CodeGenerator.UmlHandleBarHelpers
{
    using System.Linq;

    using HandlebarsDotNet;

    using uml4net.SimpleClassifiers;

    /// <summary>
    /// A block helper to support the generation of <see cref="Enumeration" /> and <see cref="EnumerationLiteral" />
    /// </summary>
    public static class EnumerationHelper
    {
        /// <summary>
        /// Registers the <see cref="EnumerationHelper" />
        /// </summary>
        /// <param name="handlebars">
        /// The <see cref="IHandlebars" /> context with which the helper needs to be registered
        /// </param>
        public static void RegisterEnumerationHelper(this IHandlebars handlebars)
        {
            handlebars.RegisterHelper("Enumeration.WriteLengthLongestLiteral", (writer, context, arguments) =>
            {
                if (arguments.Length != 1)
                {
                    throw new HandlebarsException("{{#Enumeration.WriteLengthLongestLiteral}} helper must have exactly one argument");
                }

                var enumeration = arguments.Single() as Enumeration;

                int maxLenght = 0;
                foreach (var enumerationLiteral in enumeration.OwnedLiteral)
                {
                    if (!string.IsNullOrEmpty(enumerationLiteral.Name) && enumerationLiteral.Name.Length > maxLenght)
                    {
                        maxLenght = enumerationLiteral.Name.Length;
                    }
                }

                writer.WriteSafeString(maxLenght);
            });
        }
    }
}
