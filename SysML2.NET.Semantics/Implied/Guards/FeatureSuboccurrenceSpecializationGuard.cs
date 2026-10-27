// ------------------------------------------------------------------------------------------------
// <copyright file="FeatureSuboccurrenceSpecializationGuard.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Semantics.Implied.Guards
{
    using System;
    using System.Linq;

    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Kernel.Classes;
    using SysML2.NET.Core.POCO.Root.Elements;

    /// <summary>
    /// Guards checkFeatureSuboccurrenceSpecialization: a composite Class-typed Feature owned by a Class specializes Occurrence::Occurrence::suboccurrences.
    /// </summary>
    /// <remarks>
    /// OCL: <c>isComposite and ownedTyping.type-&gt;includes(oclIsKindOf(Class)) and owningType &lt;&gt; null and (owningType.oclIsKindOf(Class) or owningType.oclIsKindOf(Feature) and owningType.oclAsType(Feature).type-&gt;exists(oclIsKindOf(Class)))</c>
    /// <para>Hand written because the owner disjunct navigates <c>oclAsType(Feature).type</c>, which is
    /// outside the generator's translatable shapes.</para>
    /// </remarks>
    public class FeatureSuboccurrenceSpecializationGuard : IImpliedRuleGuard
    {
        /// <summary>
        /// Gets the name of the semantic constraint this guard decides.
        /// </summary>
        public string ConstraintName => "checkFeatureSuboccurrenceSpecialization";

        /// <summary>
        /// Asserts whether the constraint applies to the supplied Element.
        /// </summary>
        /// <param name="element">The Element under evaluation.</param>
        /// <returns>True when every conjunct of the constraint holds.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="element" /> is null.</exception>
        public bool Applies(IElement element)
        {
            return element == null
                ? throw new ArgumentNullException(nameof(element))
                : element is IFeature { IsComposite: true } feature
                  && feature.ownedTyping.Any(featureTyping => featureTyping.Type is IClass)
                  && OwningTypePredicates.IsOrIsTypedBy<IClass>(feature.owningType);
        }
    }
}
