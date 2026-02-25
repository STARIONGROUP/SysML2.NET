// -------------------------------------------------------------------------------------------------
// <copyright file="NamedElementHelper.cs" company="Starion Group S.A.">
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
    using System.Linq;

    using HandlebarsDotNet;

    using SysML2.NET.CodeGenerator.Extensions;

    using uml4net.SimpleClassifiers;
    using uml4net.CommonStructure;

    /// <summary>
    /// A handlebars block helper for the <see cref="INamedElement"/> interface
    /// </summary>
    public static class NamedElementHelper
    {
        /// <summary>
        /// Registers the <see cref="NamedElementHelper"/>
        /// </summary>
        /// <param name="handlebars">
        /// The <see cref="IHandlebars"/> context with which the helper needs to be registered
        /// </param>
        public static void RegisterNamedElementHelper(this IHandlebars handlebars)
        {
            handlebars.RegisterHelper("NamedElement.WriteFullyQualifiedNameSpace", (writer, _, arguments) =>
            {
                if (arguments[0] is not INamedElement namedElement)
                {
                    throw new ArgumentException("supposed to be INamedElement");
                }

                writer.WriteSafeString(namedElement.QueryNamespace());
            });

            handlebars.RegisterHelper("NamedElement.WriteFullyQualifiedTypeName", (writer, _, arguments) =>
            {
                if (arguments[0] is not INamedElement namedElement)
                {
                    throw new ArgumentException("supposed to be INamedElement");
                }

                writer.WriteSafeString(namedElement.QueryFullyQualifiedTypeName());
            });
        }
    }
}
