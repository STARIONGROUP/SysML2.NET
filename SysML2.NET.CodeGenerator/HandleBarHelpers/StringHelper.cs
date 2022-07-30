// -------------------------------------------------------------------------------------------------
// <copyright file="TypeNameHelper.cs" company="RHEA System S.A.">
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

namespace SysML2.NET.CodeGenerator.HandleBarHelpers
{
    using ECoreNetto;

    using HandlebarsDotNet;

    using SysML2.NET.CodeGenerator.Extensions;

    /// <summary>
    /// A block helper that prints the name of the type of a <see cref="EStructuralFeature"/>
    /// </summary>
    public static class StringHelper
    {
        /// <summary>
        /// Registers the <see cref="StructuralFeatureHelper"/>
        /// </summary>
        /// <param name="handlebars">
        /// The <see cref="IHandlebars"/> context with which the helper needs to be registered
        /// </param>
        public static void RegisterStringHelper(this IHandlebars handlebars)
        {
            handlebars.RegisterHelper("String.CapitalizeFirstLetter", (writer, context, parameters) =>
            {
                if (parameters.Length != 1)
                {
                    throw new HandlebarsException("{{#String.CapitalizeFirstLetter}} helper must have exactly one argument");
                }

                var value = parameters[0] as string;
                
                writer.WriteSafeString(value.CapitalizeFirstLetter());
            }); 

            handlebars.RegisterHelper("String.LowerCaseFirstLetter", (writer, context, parameters) =>
            {
                if (parameters.Length != 1)
                {
                    throw new HandlebarsException("{{#String.LowerCaseFirstLetter}} helper must have exactly one argument");
                }

                var value = parameters[0] as string;

                writer.WriteSafeString(value.LowerCaseFirstLetter());
            });
        }
    }
}
