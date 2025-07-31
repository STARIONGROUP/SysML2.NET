// -------------------------------------------------------------------------------------------------
// <copyright file="EnumerationLiteralHelper.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.CodeGenerator.UmlHandleBarHelpers
{
    using HandlebarsDotNet;
    using SysML2.NET.CodeGenerator.Extensions;
    using System;
    using System.Linq;
    using uml4net;
    using uml4net.Extensions;
    using uml4net.SimpleClassifiers;

    /// <summary>
    /// A block helper to support the generation of <see cref="Enumeration"/> and <see cref="EnumerationLiteral"/>
    /// </summary>
    public static class EnumerationLiteralHelper
    {
        /// <summary>
        /// Registers the <see cref="EnumerationLiteralHelper"/>
        /// </summary>
        /// <param name="handlebars">
        /// The <see cref="IHandlebars"/> context with which the helper needs to be registered
        /// </param>
        public static void RegisterTypeNameHelper(this IHandlebars handlebars)
        {
            handlebars.RegisterHelper("EnumerationLiteral.Write", (writer, context, arguments) =>
            {
                if (arguments.Length != 1)
                {
                    throw new HandlebarsException("{{#EnumerationLiteral.Write}} helper must have exactly one argument");
                }

                var enumerationLiteral = arguments.Single() as EnumerationLiteral;

                var name = StringExtensions.CapitalizeFirstLetter(enumerationLiteral.Name);

                if (ReservedCSharpNameMapper.QueryIsReserved(name))
                {
                    name = ReservedCSharpNameMapper.Map(name);
                }

                writer.WriteSafeString(name);
            });
        }
    }
}
