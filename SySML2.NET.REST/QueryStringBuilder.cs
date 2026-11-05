// ------------------------------------------------------------------------------------------------
// <copyright file="QueryStringBuilder.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SySML2.NET.REST
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    using SysML2.NET.PSM;

    /// <summary>
    /// Builds the query string of a request to the Systems Modeling API and Services, skipping parameters without a value
    /// </summary>
    internal sealed class QueryStringBuilder
    {
        /// <summary>
        /// The encoded name/value pairs of the query string, in the order they were added
        /// </summary>
        private readonly List<string> parameters = [];

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryStringBuilder"/> class
        /// </summary>
        /// <param name="queryParameters">
        /// The <see cref="QueryParameters"/> that select the page of the result, or <c>null</c> when the request is not paged
        /// </param>
        public QueryStringBuilder(QueryParameters queryParameters = null)
        {
            if (queryParameters is null)
            {
                return;
            }

            if (queryParameters.PageSize > 0)
            {
                this.Add("page[size]", queryParameters.PageSize);
            }

            this.Add("page[after]", queryParameters.PageAfter);
            this.Add("page[before]", queryParameters.PageBefore);
        }

        /// <summary>
        /// Adds a query parameter, writing a sequence as a comma separated list in the OpenAPI <c>form</c> style without explode
        /// </summary>
        /// <param name="name">
        /// The name of the query parameter as declared in the OpenAPI document
        /// </param>
        /// <param name="value">
        /// The value of the query parameter, or <c>null</c> or an empty sequence to omit it
        /// </param>
        public void Add(string name, object value)
        {
            var text = value switch
            {
                null => null,
                string stringValue => Uri.EscapeDataString(stringValue),
                bool booleanValue => booleanValue ? "true" : "false",
                IEnumerable sequence => FormatSequence(sequence),
                _ => FormatValue(value)
            };

            if (text is not null)
            {
                this.parameters.Add($"{name}={text}");
            }
        }

        /// <summary>
        /// Returns the query string, including its leading question mark
        /// </summary>
        /// <returns>
        /// The query string, or an empty string when no parameter has a value
        /// </returns>
        public override string ToString()
        {
            return this.parameters.Count == 0 ? string.Empty : $"?{string.Join("&", this.parameters)}";
        }

        /// <summary>
        /// Formats a sequence as its escaped values joined by a literal comma
        /// </summary>
        /// <param name="sequence">
        /// The sequence to format
        /// </param>
        /// <returns>
        /// The formatted list, or <c>null</c> when the sequence is empty
        /// </returns>
        private static string FormatSequence(IEnumerable sequence)
        {
            var values = sequence.Cast<object>().Select(FormatValue).ToList();

            return values.Count == 0 ? null : string.Join(',', values);
        }

        /// <summary>
        /// Formats a scalar value using the invariant culture and escapes it
        /// </summary>
        /// <param name="value">
        /// The value to format
        /// </param>
        /// <returns>
        /// The escaped value, or <c>null</c> when the value formats as <c>null</c>
        /// </returns>
        private static string FormatValue(object value)
        {
            var text = Convert.ToString(value, CultureInfo.InvariantCulture);

            return text is null ? null : Uri.EscapeDataString(text);
        }
    }
}
