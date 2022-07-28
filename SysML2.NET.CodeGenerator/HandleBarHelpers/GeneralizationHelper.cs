// -------------------------------------------------------------------------------------------------
// <copyright file="GeneralizationHelper.cs" company="RHEA System S.A.">
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
    using System;
    using System.Linq;

    using ECoreNetto;

    using HandlebarsDotNet;

    /// <summary>
    /// A block helper that prints the generalization (super-type) information of an <see cref="EClass"/>
    /// </summary>
    public static class GeneralizationHelper
    {
        /// <summary>
        /// Registers the <see cref="GeneralizationHelper"/>
        /// </summary>
        /// <param name="handlebars">
        /// The <see cref="IHandlebars"/> context with which the helper needs to be registered
        /// </param>
        public static void RegisterGeneralizationHelper(this IHandlebars handlebars)
        {
            handlebars.RegisterHelper("Generalization.Interfaces", (writer, context, parameters) =>
            {
                if (!(context.Value is EClass eClass))
                    throw new ArgumentException("The context shall be an EClass");

                if (eClass.ESuperTypes.Any())
                {
                    var result = $": {string.Join(",", eClass.ESuperTypes.Select(x => $"I{x.Name}"))}";

                    writer.WriteSafeString(result);
                }
            });

            handlebars.RegisterHelper("Generalization.Classes", (writer, context, parameters) =>
            {
                if (!(context.Value is EClass eClass))
                    throw new ArgumentException("The context shall be an EClass");

                if (!eClass.ESuperTypes.Any())
                {
                    writer.WriteSafeString($": I{eClass.Name}");
                    return;
                }
                
                writer.WriteSafeString($": {eClass.ESuperTypes.First().Name}, I{eClass.Name}");
            });
        }
    }
}
