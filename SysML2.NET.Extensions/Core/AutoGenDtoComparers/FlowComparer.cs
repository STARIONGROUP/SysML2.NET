// -------------------------------------------------------------------------------------------------
// <copyright file="FlowComparer.cs" company="Starion Group S.A.">
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

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Extensions.Core.DTO.Comparers
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;

    using SysML2.NET.Core.Core.Types;
    using SysML2.NET.Core.DTO.Kernel.Interactions;
    using SysML2.NET.Extensions.Comparers;

    /// <summary>
    /// Provides an equality comparison for <see cref="IFlow"/> instances
    /// based on their semantic content rather than object reference identity.
    /// </summary>
    /// <remarks>
    /// This comparer is intended for use in deserialization, testing, and model
    /// validation scenarios where two <see cref="IFlow"/> instances
    /// originating from different sources (e.g. JSON and MessagePack) must be
    /// considered equal if they represent the same SysML v2 model element.
    /// </remarks>
    [GeneratedCode("SysML2.NET", "latest")]
    public sealed class FlowComparer : IEqualityComparer<IFlow>
    {
        /// <summary>
        /// The <see cref="StringComparer"/> used for all string comparisons performed
        /// by this comparer.
        /// </summary>
        /// <remarks>
        /// <see cref="StringComparer.Ordinal"/> is used to ensure culture-independent,
        /// deterministic comparison semantics suitable for identifiers and
        /// model-level string values.
        /// </remarks>
        private static readonly StringComparer OrdinalStringComparer = StringComparer.Ordinal;

        /// <summary>
        /// Determines whether two <see cref="IFlow"/> instances are equal.
        /// </summary>
        /// <param name="x">
        /// The first <see cref="IFlow"/> to compare.
        /// </param>
        /// <param name="y">
        /// The second <see cref="IFlow"/> to compare.
        /// </param>
        /// <returns>
        /// <c>true</c> if both instances represent the same <see cref="IFlow"/>
        /// according to their semantic content; otherwise, <c>false</c>.
        /// </returns>
        /// <remarks>
        /// This method performs a deep, deterministic comparison of all relevant
        /// properties, ignoring object reference identity and any transient or
        /// derived state.
        /// </remarks>
        public bool Equals(IFlow x, IFlow y)
        {
            if (ReferenceEquals(x, y)) return true;

            if (x is null || y is null) return false;

            if (x.Id != y.Id) return false;

            if (!StringSequenceEquality.OrderedEqual(x.AliasIds, y.AliasIds)) return false;

            if (!OrdinalStringComparer.Equals(x.DeclaredName, y.DeclaredName)) return false;

            if (!OrdinalStringComparer.Equals(x.DeclaredShortName, y.DeclaredShortName)) return false;

            if (x.Direction != y.Direction) return false;

            if (!OrdinalStringComparer.Equals(x.ElementId, y.ElementId)) return false;

            if (x.IsAbstract != y.IsAbstract) return false;

            if (x.IsComposite != y.IsComposite) return false;

            if (x.IsConstant != y.IsConstant) return false;

            if (x.IsDerived != y.IsDerived) return false;

            if (x.IsEnd != y.IsEnd) return false;

            if (x.IsImplied != y.IsImplied) return false;

            if (x.IsImpliedIncluded != y.IsImpliedIncluded) return false;

            if (x.IsOrdered != y.IsOrdered) return false;

            if (x.IsPortion != y.IsPortion) return false;

            if (x.IsSufficient != y.IsSufficient) return false;

            if (x.IsUnique != y.IsUnique) return false;

            if (x.IsVariable != y.IsVariable) return false;

            if (!GuidSequenceEquality.OrderedEqual(x.OwnedRelatedElement, y.OwnedRelatedElement)) return false;

            if (!GuidSequenceEquality.OrderedEqual(x.OwnedRelationship, y.OwnedRelationship)) return false;

            if (x.OwningRelatedElement != y.OwningRelatedElement) return false;

            if (x.OwningRelationship != y.OwningRelationship) return false;

            return true;
        }

        /// <summary>
        /// Returns a hash code for the specified <see cref="IFlow"/>.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="IFlow"/> for which a hash code is to be returned.
        /// </param>
        /// <returns>
        /// A hash code based on the stable identity of the element.
        /// </returns>
        /// <remarks>
        /// The hash code is intentionally derived from the element identity only,
        /// ensuring stability and compatibility with collection-based equality
        /// checks
        /// </remarks>
        public int GetHashCode(IFlow obj)
        {
            return HashCode.Combine(typeof(IFlow), obj.Id);
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
