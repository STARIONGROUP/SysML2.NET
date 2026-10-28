// ------------------------------------------------------------------------------------------------
// <copyright file="OperatorExpressionExtensions.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Core.POCO.Kernel.Expressions
{
    using System;
    using System.Linq;

    using SysML2.NET.Decorators;

    using SysML2.NET.Core.POCO.Core.Types;

    /// <summary>
    /// The <see cref="OperatorExpressionExtensions" /> class provides extensions methods for
    /// the <see cref="IOperatorExpression" /> interface
    /// </summary>
    internal static class OperatorExpressionExtensions
    {
        /// <summary>
        /// The instantiatedType of an OperatorExpression is the resolution of it's operator from one of the
        /// packages BaseFunctions, DataFunctions, or ControlFunctions from the Kernel Function Library.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// let libFunctions : Sequence(Element) =
        ///                                 Sequence{'BaseFunctions', 'DataFunctions', 'ControlFunctions'}-&gt;
        ///                                 collect(ns | resolveGlobal(ns + "::'" + operator + "'").
        ///                                 memberElement) in
        ///                                 if libFunctions-&gt;isEmpty() then null
        ///                                 else libFunctions-&gt;first().oclAsType(Type)
        ///                                 endif
        /// </code>
        /// </remarks>
        /// <param name="operatorExpressionSubject">
        /// The subject <see cref="IOperatorExpression" />
        /// </param>
        /// <returns>
        /// The expected <see cref="IType" />
        /// </returns>
        [Operation(name: nameof(IOperatorExpression.InstantiatedType))]
        internal static IType ComputeRedefinedInstantiatedTypeOperation(this IOperatorExpression operatorExpressionSubject)
        {
            if (operatorExpressionSubject == null)
            {
                throw new ArgumentNullException(nameof(operatorExpressionSubject));
            }

            string[] namespaces = ["BaseFunctions", "DataFunctions", "ControlFunctions"];

            return namespaces
                .Select(ns => operatorExpressionSubject.ResolveGlobal($"{ns}::'{operatorExpressionSubject.Operator}'")?.MemberElement)
                .FirstOrDefault(memberElement => memberElement != null) as IType;
        }
    }
}
