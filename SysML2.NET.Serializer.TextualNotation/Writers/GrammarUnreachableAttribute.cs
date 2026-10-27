// ------------------------------------------------------------------------------------------------
// <copyright file="GrammarUnreachableAttribute.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Serializer.TextualNotation.Writers
{
    using System;

    /// <summary>
    /// Marks a generated <c>Build{Rule}</c> method whose grammar rule has no incoming reference,
    /// directly or transitively, from any other production in the effective SysML v2 textual
    /// grammar rooted at <c>RootNamespace</c>.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public sealed class GrammarUnreachableAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GrammarUnreachableAttribute"/> class.
        /// </summary>
        /// <param name="reason">Why the grammar rule this method implements cannot be reached.</param>
        public GrammarUnreachableAttribute(string reason)
        {
            this.Reason = reason;
        }

        /// <summary>
        /// Gets why the grammar rule this method implements cannot be reached.
        /// </summary>
        public string Reason { get; }
    }
}
