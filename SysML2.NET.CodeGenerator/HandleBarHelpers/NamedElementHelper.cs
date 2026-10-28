// ------------------------------------------------------------------------------------------------
// <copyright file="NamedElementHelper.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
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
