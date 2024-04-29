// -------------------------------------------------------------------------------------------------
// <copyright file="DocumentationHelper.cs" company="Starion Group S.A.">
// 
//   Copyright 2022-2024 Starion Group S.A.
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

    using ECoreNetto;

    using HandlebarsDotNet;

    using SysML2.NET.CodeGenerator.Extensions;

    /// <summary>
    /// a block helper 
    /// </summary>
    public static class DocumentationHelper
    {
        /// <summary>
        /// Registers the <see cref="DocumentationHelper"/>
        /// </summary>
        /// <param name="handlebars">
        /// The <see cref="IHandlebars"/> context with which the helper needs to be registered
        /// </param>
        public static void RegisteredDocumentationHelper(this IHandlebars handlebars)
        {
            handlebars.RegisterHelper("Documentation", (writer, context, parameters) =>
            {
                if (!(context.Value is EModelElement eModelElement))
                    throw new ArgumentException("supposed to be EModelElement");

                writer.WriteSafeString($"/// <summary>{Environment.NewLine}");
                foreach (var line in eModelElement.QueryDocumentation())
                {
                    writer.WriteSafeString($"/// {line}{Environment.NewLine}");
                }
                writer.WriteSafeString($"/// </summary>{Environment.NewLine}");
            });

            handlebars.RegisterHelper("RawDocumentation", (writer, context, parameters) =>
            {
	            if (!(context.Value is EModelElement eModelElement))
		            throw new ArgumentException("supposed to be EModelElement");
                
	            writer.WriteSafeString(eModelElement.QueryRawDocumentation());
            });

            handlebars.RegisterHelper("DotDocumentation", (writer, context, parameters) =>
            {
	            if (!(context.Value is EModelElement eModelElement))
		            throw new ArgumentException("supposed to be EModelElement");

	            var documentation = eModelElement.QueryRawDocumentation();

	            documentation = documentation.Replace(":","_") 
							.Replace("\"", "\\\"");
                
				writer.WriteSafeString(documentation);
            });
		}
    }
}
