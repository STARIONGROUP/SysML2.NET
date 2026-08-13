// -------------------------------------------------------------------------------------------------
// <copyright file="FeaturePortionSpecializationGuard.cs" company="Starion Group S.A.">
//
//    Copyright (C) 2022-2026 Starion Group S.A.
//
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
//
//         http://www.apache.org/licenses/LICENSE-2.0
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Semantics.Implied.Guards
{
    using System;
    using System.Linq;

    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Kernel.Classes;
    using SysML2.NET.Core.POCO.Root.Elements;

    /// <summary>
    /// Guards checkFeaturePortionSpecialization: a portion Feature typed by a Class, owned by a Class or by
    /// a Class-typed Feature, specializes Occurrences::Occurrence::portions.
    /// </summary>
    /// <remarks>
    /// OCL: <c>isPortion and ownedTyping.type-&gt;includes(oclIsKindOf(Class)) and owningType &lt;&gt; null
    /// and (owningType.oclIsKindOf(Class) or owningType.oclIsKindOf(Feature) and
    /// owningType.oclAsType(Feature).type-&gt;exists(oclIsKindOf(Class)))</c>. The nested
    /// <c>oclAsType(Feature).type</c> navigation in the final disjunct is outside the generator's
    /// translatable shapes, so the guard is written by hand.
    /// </remarks>
    public class FeaturePortionSpecializationGuard : IImpliedRuleGuard
    {
        /// <summary>
        /// Gets the name of the semantic constraint this guard decides.
        /// </summary>
        public string ConstraintName => "checkFeaturePortionSpecialization";

        /// <summary>
        /// Asserts whether the Feature is a Class-typed portion owned by a Class or a Class-typed Feature.
        /// </summary>
        /// <param name="element">The Element under evaluation.</param>
        /// <returns>True when every conjunct of the constraint holds.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="element" /> is null.</exception>
        public bool Applies(IElement element)
        {
            if (element == null)
            {
                throw new ArgumentNullException(nameof(element));
            }

            return element is IFeature { IsPortion: true } feature
                   && feature.ownedTyping.Any(featureTyping => featureTyping.Type is IClass)
                   && IsOwnedByAClassOrAClassTypedFeature(feature.owningType);
        }

        /// <summary>
        /// Asserts whether an owning Type is a Class, or a Feature that is itself typed by a Class.
        /// </summary>
        /// <param name="owningType">The owning Type, which may be null.</param>
        /// <returns>True when the owning Type satisfies the constraint's final disjunct.</returns>
        private static bool IsOwnedByAClassOrAClassTypedFeature(IType owningType)
        {
            return owningType switch
            {
                IClass => true,
                IFeature owningFeature => owningFeature.type.Any(type => type is IClass),
                _ => false
            };
        }
    }
}
