// -------------------------------------------------------------------------------------------------
// <copyright file="MultiplicityRangeExtensions.cs" company="Starion Group S.A.">
//
//    Copyright (C) 2022-2026 Starion Group S.A.
//
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
//
//        http://www.apache.org/licenses/LICENSE-2.0
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Core.POCO.Kernel.Multiplicities
{
    using System;
    using System.Collections.Generic;

    using SysML2.NET.Core.Core.Types;
    using SysML2.NET.Core.Root.Namespaces;
    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Kernel.Functions;
    using SysML2.NET.Core.POCO.Root.Annotations;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;

    /// <summary>
    /// The <see cref="MultiplicityRangeExtensions"/> class provides extensions methods for
    /// the <see cref="IMultiplicityRange"/> interface
    /// </summary>
    internal static class MultiplicityRangeExtensions
    {
        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// bound =
        ///                             if upperBound = null then Sequence{}
        ///                             else if lowerBound = null then Sequence{upperBound}
        ///                             else Sequence{lowerBound, upperBound}
        ///                             endif endif
        /// </code>
        /// </remarks>
        /// <param name="multiplicityRangeSubject">
        /// The subject <see cref="IMultiplicityRange"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static List<IExpression> ComputeBound(this IMultiplicityRange multiplicityRangeSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// lowerBound =
        ///                             let ownedExpressions : Sequence(Expression) =
        ///                             ownedMember-&gt;selectByKind(Expression) in
        ///                             if ownedExpressions-&gt;size() &lt; 2 then null
        ///                             else ownedExpressions-&gt;first()
        ///                             endif
        /// </code>
        /// </remarks>
        /// <param name="multiplicityRangeSubject">
        /// The subject <see cref="IMultiplicityRange"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static IExpression ComputeLowerBound(this IMultiplicityRange multiplicityRangeSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// upperBound =
        ///                             let ownedExpressions : Sequence(Expression) =
        ///                             ownedMember-&gt;selectByKind(Expression) in
        ///                             if ownedExpressions-&gt;isEmpty() then null
        ///                             else if ownedExpressions-&gt;size() = 1 then ownedExpressions-&gt;at(1)
        ///                             else ownedExpressions-&gt;at(2)
        ///                             endif endif
        /// </code>
        /// </remarks>
        /// <param name="multiplicityRangeSubject">
        /// The subject <see cref="IMultiplicityRange"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static IExpression ComputeUpperBound(this IMultiplicityRange multiplicityRangeSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Check whether this MultiplicityRange represents the range bounded by the given values lower and
        /// upper, presuming the lowerBound and upperBound Expressions are model-level evaluable.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// valueOf(upperBound) = upper and
        ///                                 let lowerValue: UnlimitedNatural = valueOf(lowerBound) in
        ///                                 (lowerValue = lower or
        ///                                 lowerValue = null and
        ///                                 (lower = upper or
        ///                                 lower = 0 and upper = *))
        /// </code>
        /// </remarks>
        /// <param name="multiplicityRangeSubject">
        /// The subject <see cref="IMultiplicityRange"/>
        /// </param>
        /// <param name="lower">
        /// No documentation provided
        /// </param>
        /// <param name="upper">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected <see cref="bool" />
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static bool ComputeHasBoundsOperation(this IMultiplicityRange multiplicityRangeSubject, int lower, string upper)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }

        /// <summary>
        /// Evaluate the given bound Expression (at model level) and return the result represented as a MOF
        /// UnlimitedNatural value.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// if bound = null or not bound.isModelLevelEvaluable then
        ///                                 null
        ///                                 else
        ///                                 let boundEval: Sequence(Element) = bound.evaluate(owningType) in
        ///                                 if boundEval-&gt;size() &lt;&gt; 1 then null else
        ///                                 let valueEval: Element = boundEval-&gt;at(1) in
        ///                                 if valueEval.oclIsKindOf(LiteralInfinity) then *
        ///                                 else if valueEval.oclIsKindOf(LiteralInteger) then
        ///                                 let value : Integer =
        ///                                 valueEval.oclAsKindOf(LiteralInteger).value in
        ///                                 if value &gt;= 0 then value else null endif
        ///                                 else null
        ///                                 endif endif
        ///                                 endif
        ///                                 endif
        /// </code>
        /// </remarks>
        /// <param name="multiplicityRangeSubject">
        /// The subject <see cref="IMultiplicityRange"/>
        /// </param>
        /// <param name="bound">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected <see cref="string" />
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static string ComputeValueOfOperation(this IMultiplicityRange multiplicityRangeSubject, IExpression bound)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }
    }
}
