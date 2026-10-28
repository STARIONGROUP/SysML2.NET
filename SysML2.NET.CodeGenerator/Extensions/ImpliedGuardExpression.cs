// ------------------------------------------------------------------------------------------------
// <copyright file="ImpliedGuardExpression.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.CodeGenerator.Extensions
{
    using System.Collections.Generic;

    /// <summary>
    /// A guard expression parsed into the operands a C# predicate needs.
    /// </summary>
    public class ImpliedGuardExpression
    {
        /// <summary>
        /// Gets the recognised shape, or <see cref="ImpliedGuardShape.RequiresHandCoding" />.
        /// </summary>
        public ImpliedGuardShape Shape { get; init; }

        /// <summary>
        /// Gets the OCL the expression was parsed from, retained for the generated doc comment.
        /// </summary>
        public string Ocl { get; init; }

        /// <summary>
        /// Gets the property or operation name the shape tests, e.g. <c>isComposite</c> or
        /// <c>isSubactionUsage</c>.
        /// </summary>
        public string MemberName { get; init; }

        /// <summary>
        /// Gets the metaclass names the shape tests against, e.g. <c>PartDefinition</c> and
        /// <c>PartUsage</c>.
        /// </summary>
        public IReadOnlyList<string> TypeNames { get; init; } = [];

        /// <summary>
        /// Gets a value indicating whether the expression is negated, as in <c>not isTriggerAction()</c>.
        /// </summary>
        public bool IsNegated { get; init; }

        /// <summary>
        /// Gets a value indicating whether the shape is additionally conjoined with <c>isComposite</c>.
        /// </summary>
        public bool RequiresComposite { get; init; }

        /// <summary>
        /// Gets the literal the shape compares against — an enumeration literal, a boolean argument, or a
        /// cardinality — or <c>null</c> when the shape has none.
        /// </summary>
        public string Literal { get; init; }
    }
}
