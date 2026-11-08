// ------------------------------------------------------------------------------------------------
// <copyright file="ScenarioQueries.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SySML2.NET.REST.Tests.Helpers
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    using SysML2.NET.Core.DTO.Kernel.Expressions;
    using SysML2.NET.Core.DTO.Root.Dependencies;
    using SysML2.NET.Core.DTO.Root.Elements;
    using SysML2.NET.Core.DTO.Root.Namespaces;
    using SysML2.NET.PSM.DTO;
    using SysML2.NET.PSM.Enumerations;

    /// <summary>
    /// The queries of the scenario, each with the elements of a model it must answer, so that the results are checked
    /// against the expected model
    /// </summary>
    /// <remarks>
    /// Together they use every <c>PrimitiveConstraint</c> operator, both <c>CompositeConstraint</c> operators, a nested
    /// composite and <c>inverse</c>. Every <c>@type</c> they name is a metaclass that no other metaclass specializes, so
    /// whether <c>=</c> on <c>@type</c> also matches specializations does not change the results.
    /// </remarks>
    public static class ScenarioQueries
    {
        /// <summary>
        /// Gets the query that step 9 creates: the integer literals above 1, the dependencies and the rational literals
        /// </summary>
        public static QueryRequest Stored => new()
        {
            Name = "Literals and dependencies",
            Description = "The integer literals above 1, the dependencies and the rational literals",
            Alias = ["literals"],
            Where = new CompositeConstraintRequest
            {
                Operator = CompositeConstraintRequestOperator.or,
                Constraint =
                [
                    new CompositeConstraint
                    {
                        Operator = CompositeConstraintOperator.and,
                        Constraint =
                        [
                            Constraint("@type", PrimitiveConstraintOperator.equalto, false, "LiteralInteger"),
                            Constraint("value", PrimitiveConstraintOperator.greaterthan, false, 1d)
                        ]
                    },
                    Constraint("@type", PrimitiveConstraintOperator.@in, false, "Dependency", "LiteralRational")
                ]
            }
        };

        /// <summary>
        /// Gets the query that step 9 replaces <see cref="Stored"/> with: the integer literals from 2 to 42
        /// </summary>
        public static QueryRequest Updated => new()
        {
            Name = "Integer literals from 2 to 42",
            Description = "The integer literals that are not below 2 and at most 42",
            Where = new CompositeConstraintRequest
            {
                Operator = CompositeConstraintRequestOperator.and,
                Constraint =
                [
                    Constraint("@type", PrimitiveConstraintOperator.equalto, false, "LiteralInteger"),
                    Constraint("value", PrimitiveConstraintOperator.lessthan, true, 2d),
                    Constraint("value", PrimitiveConstraintOperator.lessthanorequalto, false, 42d)
                ]
            }
        };

        /// <summary>
        /// Gets the query definition of the integer literals of at least 42
        /// </summary>
        public static QueryRequest AtLeast42 => new()
        {
            Name = "Integer literals of at least 42",
            Where = new CompositeConstraintRequest
            {
                Operator = CompositeConstraintRequestOperator.and,
                Constraint =
                [
                    Constraint("@type", PrimitiveConstraintOperator.equalto, false, "LiteralInteger"),
                    Constraint("value", PrimitiveConstraintOperator.greaterthanorequalto, false, 42d)
                ]
            }
        };

        /// <summary>
        /// Gets the query definition of the namespaces, by the <c>instanceOf</c> operator
        /// </summary>
        public static QueryRequest Namespaces => new()
        {
            Name = "Namespaces",
            Where = new PrimitiveConstraintRequest { Property = "@type", Operator = PrimitiveConstraintRequestOperator.instanceOf, Value = ["Namespace"], Inverse = false }
        };

        /// <summary>
        /// Renders a constraint, of a request or of a stored query, as text, so that two constraints can be compared; an
        /// unset <c>inverse</c> reads as <c>false</c>
        /// </summary>
        /// <param name="constraint">
        /// The constraint
        /// </param>
        /// <returns>
        /// The text, or <c>none</c> for anything that is no constraint
        /// </returns>
        public static string ConstraintText(object constraint)
        {
            static string Primitive(string property, string constraintOperator, bool inverse, IEnumerable<ConstraintValue> values) =>
                $"{(inverse ? "not " : string.Empty)}{property} {constraintOperator} [{string.Join(", ", (values ?? []).Select(value => value.Match(() => "null", flag => flag.ToString(), number => number.ToString(CultureInfo.InvariantCulture), text => $"\"{text}\"", id => id.ToString())))}]";

            return constraint switch
            {
                CompositeConstraintRequest composite => $"{composite.Operator}({string.Join(", ", composite.Constraint.Select(ConstraintText))})",
                CompositeConstraint composite => $"{composite.Operator}({string.Join(", ", composite.Constraint.Select(ConstraintText))})",
                PrimitiveConstraintRequest primitive => Primitive(primitive.Property, primitive.Operator.ToString(), primitive.Inverse ?? false, primitive.Value),
                PrimitiveConstraint primitive => Primitive(primitive.Property, primitive.Operator.ToString(), primitive.Inverse, primitive.Value),
                _ => "none"
            };
        }

        /// <summary>
        /// Gets whether an element is a result of <see cref="AtLeast42"/>
        /// </summary>
        /// <param name="element">
        /// The element
        /// </param>
        /// <returns>
        /// Whether the query answers it
        /// </returns>
        public static bool IsAtLeast42(IElement element) => element is LiteralInteger { Value: >= 42 };

        /// <summary>
        /// Gets whether an element is a result of <see cref="Namespaces"/>
        /// </summary>
        /// <param name="element">
        /// The element
        /// </param>
        /// <returns>
        /// Whether the query answers it
        /// </returns>
        public static bool IsNamespace(IElement element) => element is INamespace;

        /// <summary>
        /// Gets whether an element is a result of <see cref="Stored"/>
        /// </summary>
        /// <param name="element">
        /// The element
        /// </param>
        /// <returns>
        /// Whether the query answers it
        /// </returns>
        public static bool IsStoredResult(IElement element) => element is LiteralInteger { Value: > 1 } || element.GetType() == typeof(Dependency) || element.GetType() == typeof(LiteralRational);

        /// <summary>
        /// Gets whether an element is a result of <see cref="Updated"/>
        /// </summary>
        /// <param name="element">
        /// The element
        /// </param>
        /// <returns>
        /// Whether the query answers it
        /// </returns>
        public static bool IsUpdatedResult(IElement element) => element is LiteralInteger { Value: >= 2 and <= 42 };

        /// <summary>
        /// Gets the query definition of the elements of a declared name that selects only their id and name
        /// </summary>
        /// <param name="declaredName">
        /// The declared name
        /// </param>
        /// <returns>
        /// The query definition
        /// </returns>
        public static QueryRequest Named(string declaredName) => new()
        {
            Name = $"Named {declaredName}",
            Select = ["@id", "declaredName"],
            Where = new PrimitiveConstraintRequest { Property = "declaredName", Operator = PrimitiveConstraintRequestOperator.equalto, Value = [declaredName], Inverse = false }
        };

        /// <summary>
        /// Gets the query definition of the elements of a declared name, whose constraint leaves <c>inverse</c> unset
        /// </summary>
        /// <param name="declaredName">
        /// The declared name
        /// </param>
        /// <returns>
        /// The query definition
        /// </returns>
        public static QueryRequest NamedWithoutInverse(string declaredName) => new()
        {
            Name = $"Named {declaredName}",
            Where = new PrimitiveConstraintRequest { Property = "declaredName", Operator = PrimitiveConstraintRequestOperator.equalto, Value = [declaredName] }
        };

        /// <summary>
        /// Creates a primitive constraint, as nested in a composite constraint
        /// </summary>
        /// <param name="property">
        /// The property the constraint is about
        /// </param>
        /// <param name="constraintOperator">
        /// The operator
        /// </param>
        /// <param name="inverse">
        /// Whether the constraint is inverted
        /// </param>
        /// <param name="values">
        /// The values the property is compared with
        /// </param>
        /// <returns>
        /// The constraint
        /// </returns>
        private static PrimitiveConstraint Constraint(string property, PrimitiveConstraintOperator constraintOperator, bool inverse, params ConstraintValue[] values) => new()
        {
            Property = property,
            Operator = constraintOperator,
            Inverse = inverse,
            Value = [.. values]
        };
    }
}
