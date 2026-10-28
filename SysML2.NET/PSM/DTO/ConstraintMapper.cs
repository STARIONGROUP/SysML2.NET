// ------------------------------------------------------------------------------------------------
// <copyright file="ConstraintMapper.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.PSM.DTO
{
    using System;
    using System.Globalization;

    using SysML2.NET.PIM;
    using SysML2.NET.PIM.DTO;
    using SysML2.NET.PSM.Enumerations;

    /// <summary>
    /// Maps the query condition tree between the Platform Independent Model and the REST/HTTP PSM
    /// </summary>
    /// <remarks>
    /// The condition alternatives are reached through <see cref="IConstraint" /> and <see cref="IConstraintRequest" />,
    /// which carry no Platform Independent Model counterpart of their own, so the dispatch lives here.
    /// </remarks>
    public static class ConstraintMapper
    {
        /// <summary>
        /// Maps a REST/HTTP PSM query condition onto its Platform Independent Model counterpart
        /// </summary>
        /// <param name="constraint">The condition to map.</param>
        /// <returns>The Platform Independent Model condition, or <c>null</c> when the condition is <c>null</c>.</returns>
        /// <exception cref="NotSupportedException">Thrown when the condition is of an alternative the Platform Independent Model does not model.</exception>
        public static Constraint ToPim(IConstraint constraint)
        {
            return constraint switch
            {
                null => null,
                PrimitiveConstraint primitive => primitive.ToPim(),
                CompositeConstraint composite => composite.ToPim(),
                _ => throw new NotSupportedException($"A {constraint.GetType().Name} has no Platform Independent Model counterpart.")
            };
        }

        /// <summary>
        /// Maps a REST/HTTP PSM query condition request onto its Platform Independent Model counterpart
        /// </summary>
        /// <param name="request">The condition to map.</param>
        /// <returns>The Platform Independent Model condition, or <c>null</c> when the condition is <c>null</c>.</returns>
        /// <exception cref="NotSupportedException">Thrown when the condition is of an alternative the Platform Independent Model does not model.</exception>
        public static Constraint ToPim(IConstraintRequest request)
        {
            return request switch
            {
                null => null,
                PrimitiveConstraintRequest primitive => primitive.ToPim(),
                CompositeConstraintRequest composite => composite.ToPim(),
                _ => throw new NotSupportedException($"A {request.GetType().Name} has no Platform Independent Model counterpart.")
            };
        }

        /// <summary>
        /// Maps a REST/HTTP PSM comparison operator onto its Platform Independent Model literal
        /// </summary>
        /// <param name="comparisonOperator">The operator to map.</param>
        /// <returns>The Platform Independent Model literal denoting the same comparison.</returns>
        /// <exception cref="NotSupportedException">Thrown when the operator has no Platform Independent Model literal.</exception>
        internal static Operator ToPim(PrimitiveConstraintOperator comparisonOperator)
        {
            return comparisonOperator switch
            {
                PrimitiveConstraintOperator.instanceOf => Operator.instanceOf,
                PrimitiveConstraintOperator.equalto => Operator.equalto,
                PrimitiveConstraintOperator.lessthan => Operator.lessthan,
                PrimitiveConstraintOperator.lessthanorequalto => Operator.lessthanorequalto,
                PrimitiveConstraintOperator.greaterthan => Operator.greaterthan,
                PrimitiveConstraintOperator.greaterthanorequalto => Operator.greaterthanorequalto,
                PrimitiveConstraintOperator.@in => Operator.@in,
                _ => throw new NotSupportedException($"The {comparisonOperator} operator has no Platform Independent Model literal.")
            };
        }

        /// <summary>
        /// Maps a REST/HTTP PSM comparison operator request onto its Platform Independent Model literal
        /// </summary>
        /// <param name="comparisonOperator">The operator to map.</param>
        /// <returns>The Platform Independent Model literal denoting the same comparison.</returns>
        /// <exception cref="NotSupportedException">Thrown when the operator has no Platform Independent Model literal.</exception>
        internal static Operator ToPim(PrimitiveConstraintRequestOperator comparisonOperator)
        {
            return comparisonOperator switch
            {
                PrimitiveConstraintRequestOperator.instanceOf => Operator.instanceOf,
                PrimitiveConstraintRequestOperator.equalto => Operator.equalto,
                PrimitiveConstraintRequestOperator.lessthan => Operator.lessthan,
                PrimitiveConstraintRequestOperator.lessthanorequalto => Operator.lessthanorequalto,
                PrimitiveConstraintRequestOperator.greaterthan => Operator.greaterthan,
                PrimitiveConstraintRequestOperator.greaterthanorequalto => Operator.greaterthanorequalto,
                PrimitiveConstraintRequestOperator.@in => Operator.@in,
                _ => throw new NotSupportedException($"The {comparisonOperator} operator has no Platform Independent Model literal.")
            };
        }

        /// <summary>
        /// Maps a REST/HTTP PSM join operator onto its Platform Independent Model literal
        /// </summary>
        /// <param name="joinOperator">The operator to map.</param>
        /// <returns>The Platform Independent Model literal denoting the same join.</returns>
        /// <exception cref="NotSupportedException">Thrown when the operator has no Platform Independent Model literal.</exception>
        internal static JoinOperator ToPim(CompositeConstraintOperator joinOperator)
        {
            return joinOperator switch
            {
                CompositeConstraintOperator.and => JoinOperator.AND,
                CompositeConstraintOperator.or => JoinOperator.OR,
                _ => throw new NotSupportedException($"The {joinOperator} join operator has no Platform Independent Model literal.")
            };
        }

        /// <summary>
        /// Maps a REST/HTTP PSM join operator request onto its Platform Independent Model literal
        /// </summary>
        /// <param name="joinOperator">The operator to map.</param>
        /// <returns>The Platform Independent Model literal denoting the same join.</returns>
        /// <exception cref="NotSupportedException">Thrown when the operator has no Platform Independent Model literal.</exception>
        internal static JoinOperator ToPim(CompositeConstraintRequestOperator joinOperator)
        {
            return joinOperator switch
            {
                CompositeConstraintRequestOperator.and => JoinOperator.AND,
                CompositeConstraintRequestOperator.or => JoinOperator.OR,
                _ => throw new NotSupportedException($"The {joinOperator} join operator has no Platform Independent Model literal.")
            };
        }

        /// <summary>
        /// Maps a Platform Independent Model query condition onto its REST/HTTP PSM response alternative
        /// </summary>
        /// <param name="constraint">The condition to map.</param>
        /// <returns>The response condition, or <c>null</c> when the condition is <c>null</c>.</returns>
        /// <exception cref="NotSupportedException">Thrown when the condition is of a type the REST/HTTP PSM does not model.</exception>
        public static IConstraint ToResponse(Constraint constraint)
        {
            return constraint switch
            {
                null => null,
                PIM.DTO.PrimitiveConstraint primitive => PimMapper.Map<PrimitiveConstraint, PIM.DTO.PrimitiveConstraint>(primitive),
                PIM.DTO.CompositeConstraint composite => PimMapper.Map<CompositeConstraint, PIM.DTO.CompositeConstraint>(composite),
                _ => throw new NotSupportedException($"A {constraint.GetType().Name} has no REST/HTTP PSM counterpart.")
            };
        }

        /// <summary>
        /// Maps a Platform Independent Model comparison operator onto its REST/HTTP PSM response literal
        /// </summary>
        /// <param name="comparisonOperator">The operator to map.</param>
        /// <returns>The response literal denoting the same comparison.</returns>
        /// <exception cref="NotSupportedException">Thrown when the operator has no REST/HTTP PSM literal.</exception>
        internal static PrimitiveConstraintOperator ToResponse(Operator comparisonOperator)
        {
            return comparisonOperator switch
            {
                Operator.instanceOf => PrimitiveConstraintOperator.instanceOf,
                Operator.equalto => PrimitiveConstraintOperator.equalto,
                Operator.lessthan => PrimitiveConstraintOperator.lessthan,
                Operator.lessthanorequalto => PrimitiveConstraintOperator.lessthanorequalto,
                Operator.greaterthan => PrimitiveConstraintOperator.greaterthan,
                Operator.greaterthanorequalto => PrimitiveConstraintOperator.greaterthanorequalto,
                Operator.@in => PrimitiveConstraintOperator.@in,
                _ => throw new NotSupportedException($"The {comparisonOperator} operator has no REST/HTTP PSM literal.")
            };
        }

        /// <summary>
        /// Maps a Platform Independent Model join operator onto its REST/HTTP PSM response literal
        /// </summary>
        /// <param name="joinOperator">The operator to map.</param>
        /// <returns>The response literal denoting the same join.</returns>
        /// <exception cref="NotSupportedException">Thrown when the operator has no REST/HTTP PSM literal.</exception>
        internal static CompositeConstraintOperator ToResponse(JoinOperator joinOperator)
        {
            return joinOperator switch
            {
                JoinOperator.AND => CompositeConstraintOperator.and,
                JoinOperator.OR => CompositeConstraintOperator.or,
                _ => throw new NotSupportedException($"The {joinOperator} join operator has no REST/HTTP PSM literal.")
            };
        }

        /// <summary>
        /// Maps a Platform Independent Model query condition onto its REST/HTTP PSM request alternative
        /// </summary>
        /// <param name="constraint">The condition to map.</param>
        /// <returns>The request condition, or <c>null</c> when the condition is <c>null</c>.</returns>
        /// <exception cref="NotSupportedException">Thrown when the condition is of a type the REST/HTTP PSM does not model.</exception>
        public static IConstraintRequest ToRequest(Constraint constraint)
        {
            return constraint switch
            {
                null => null,
                PIM.DTO.PrimitiveConstraint primitive => PimMapper.Map<PrimitiveConstraintRequest, PIM.DTO.PrimitiveConstraint>(primitive),
                PIM.DTO.CompositeConstraint composite => PimMapper.Map<CompositeConstraintRequest, PIM.DTO.CompositeConstraint>(composite),
                _ => throw new NotSupportedException($"A {constraint.GetType().Name} has no REST/HTTP PSM counterpart.")
            };
        }

        /// <summary>
        /// Maps a Platform Independent Model comparison operator onto its REST/HTTP PSM request literal
        /// </summary>
        /// <param name="comparisonOperator">The operator to map.</param>
        /// <returns>The request literal denoting the same comparison.</returns>
        /// <exception cref="NotSupportedException">Thrown when the operator has no REST/HTTP PSM literal.</exception>
        internal static PrimitiveConstraintRequestOperator ToRequest(Operator comparisonOperator)
        {
            return comparisonOperator switch
            {
                Operator.instanceOf => PrimitiveConstraintRequestOperator.instanceOf,
                Operator.equalto => PrimitiveConstraintRequestOperator.equalto,
                Operator.lessthan => PrimitiveConstraintRequestOperator.lessthan,
                Operator.lessthanorequalto => PrimitiveConstraintRequestOperator.lessthanorequalto,
                Operator.greaterthan => PrimitiveConstraintRequestOperator.greaterthan,
                Operator.greaterthanorequalto => PrimitiveConstraintRequestOperator.greaterthanorequalto,
                Operator.@in => PrimitiveConstraintRequestOperator.@in,
                _ => throw new NotSupportedException($"The {comparisonOperator} operator has no REST/HTTP PSM literal.")
            };
        }

        /// <summary>
        /// Maps a Platform Independent Model join operator onto its REST/HTTP PSM request literal
        /// </summary>
        /// <param name="joinOperator">The operator to map.</param>
        /// <returns>The request literal denoting the same join.</returns>
        /// <exception cref="NotSupportedException">Thrown when the operator has no REST/HTTP PSM literal.</exception>
        internal static CompositeConstraintRequestOperator ToRequest(JoinOperator joinOperator)
        {
            return joinOperator switch
            {
                JoinOperator.AND => CompositeConstraintRequestOperator.and,
                JoinOperator.OR => CompositeConstraintRequestOperator.or,
                _ => throw new NotSupportedException($"The {joinOperator} join operator has no REST/HTTP PSM literal.")
            };
        }

        /// <summary>
        /// Renders a REST/HTTP PSM condition value as the string that the Platform Independent Model carries
        /// </summary>
        /// <param name="value">The value to render.</param>
        /// <returns>The rendered value, or <c>null</c> when the value is the null alternative.</returns>
        /// <remarks>
        /// Systems Modeling API and Services, Clause 7.1.4 assigns no explicit type to <c>PrimitiveConstraint::value</c>,
        /// whereas the Platform Independent Model types it <c>String</c>, so the alternative is not recoverable.
        /// </remarks>
        internal static string ToPimValue(ConstraintValue value)
        {
            if (value.TryGetString(out var stringValue))
            {
                return stringValue;
            }

            if (value.TryGetBoolean(out var booleanValue))
            {
                return booleanValue ? "true" : "false";
            }

            if (value.TryGetNumber(out var numberValue))
            {
                return numberValue.ToString(CultureInfo.InvariantCulture);
            }

            return value.TryGetGuid(out var guidValue) ? guidValue.ToString() : null;
        }

        /// <summary>
        /// Wraps the string that the Platform Independent Model carries as a REST/HTTP PSM condition value
        /// </summary>
        /// <param name="value">The value to wrap.</param>
        /// <returns>The wrapped value, which is the null alternative when the value is <c>null</c>.</returns>
        internal static ConstraintValue ToPsmValue(string value)
        {
            return ConstraintValue.From(value);
        }
    }
}
