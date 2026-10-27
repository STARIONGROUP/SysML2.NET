// ------------------------------------------------------------------------------------------------
// <copyright file="EnumerationLiteralHelper.cs" company="Starion Group S.A.">
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

    using SysML2.NET.CodeGenerator.Extensions;

    using uml4net.Extensions;
    using uml4net.SimpleClassifiers;

    /// <summary>
    /// A block helper to support the generation of <see cref="Enumeration" /> and <see cref="EnumerationLiteral" />
    /// </summary>
    public static class EnumerationLiteralHelper
    {
        /// <summary>
        /// Registers the <see cref="EnumerationLiteralHelper" />
        /// </summary>
        /// <param name="handlebars">
        /// The <see cref="IHandlebars" /> context with which the helper needs to be registered
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

                var name = enumerationLiteral.Name.CapitalizeFirstLetter();

                if (ReservedCSharpNameMapper.QueryIsReserved(name))
                {
                    name = ReservedCSharpNameMapper.Map(name);
                }

                writer.WriteSafeString(name);
            });
        }
    }
}
