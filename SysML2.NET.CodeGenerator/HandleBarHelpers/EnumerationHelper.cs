// ------------------------------------------------------------------------------------------------
// <copyright file="EnumerationHelper.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
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
