// ------------------------------------------------------------------------------------------------
// <copyright file="HttpMethodHelper.cs" company="Starion Group S.A.">
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
    /// A handlebars helper that converts an HTTP method name to the name of the matching static <see cref="System.Net.Http.HttpMethod"/> member
    /// </summary>
    public static class HttpMethodHelper
    {
        /// <summary>
        /// Registers the <see cref="HttpMethodHelper"/>
        /// </summary>
        /// <param name="handlebars">
        /// The <see cref="IHandlebars"/> context with which the helper needs to be registered
        /// </param>
        public static void RegisterHttpMethodHelper(this IHandlebars handlebars)
        {
            ArgumentNullException.ThrowIfNull(handlebars);

            handlebars.RegisterHelper("OpenApi.HttpMethod", (_, arguments) =>
            {
                if (arguments.Length != 1)
                {
                    throw new HandlebarsException("{{OpenApi.HttpMethod}} helper must have exactly one argument");
                }

                var method = Convert.ToString(arguments[0], CultureInfo.InvariantCulture)?.ToUpperInvariant();

                return method switch
                {
                    "GET" => "Get",
                    "POST" => "Post",
                    "PUT" => "Put",
                    "DELETE" => "Delete",
                    "HEAD" => "Head",
                    "OPTIONS" => "Options",
                    "TRACE" => "Trace",
                    _ => throw new HandlebarsException($"{{{{OpenApi.HttpMethod}}}} helper does not support the HTTP method '{method}'")
                };
            });
        }
    }
}
