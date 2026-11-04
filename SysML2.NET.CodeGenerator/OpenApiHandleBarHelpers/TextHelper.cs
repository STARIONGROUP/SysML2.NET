// ------------------------------------------------------------------------------------------------
// <copyright file="TextHelper.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.CodeGenerator.OpenApiHandleBarHelpers
{
    using System;
    using System.Globalization;

    using HandlebarsDotNet;

    /// <summary>
    /// A handlebars helper that converts a value to its invariant text so that templates can compare enumeration values
    /// </summary>
    public static class TextHelper
    {
        /// <summary>
        /// Registers the <see cref="TextHelper"/>
        /// </summary>
        /// <param name="handlebars">
        /// The <see cref="IHandlebars"/> context with which the helper needs to be registered
        /// </param>
        public static void RegisterTextHelper(this IHandlebars handlebars)
        {
            ArgumentNullException.ThrowIfNull(handlebars);

            handlebars.RegisterHelper("OpenApi.Text", (_, arguments) =>
            {
                if (arguments.Length != 1)
                {
                    throw new HandlebarsException("{{OpenApi.Text}} helper must have exactly one argument");
                }

                return Convert.ToString(arguments[0], CultureInfo.InvariantCulture) ?? string.Empty;
            });
        }
    }
}
