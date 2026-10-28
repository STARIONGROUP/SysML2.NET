// ------------------------------------------------------------------------------------------------
// <copyright file="OccurrenceUsageSuboccurrenceSpecializationGuard.cs" company="Starion Group S.A.">
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

    using SysML2.NET.Core.POCO.Kernel.Classes;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Systems.Occurrences;

    /// <summary>
    /// Guards checkOccurrenceUsageSuboccurrenceSpecialization: a composite OccurrenceUsage owned by a Class, OccurrenceUsage or Class-typed Feature specializes Occurrences::Occurrence::suboccurrences.
    /// </summary>
    /// <remarks>
    /// OCL: <c>isComposite and owningType &lt;&gt; null and (owningType.oclIsKindOf(Class) or owningType.oclIsKindOf(OccurrenceUsage) or owningType.oclIsKindOf(Feature) and owningType.oclAsType(Feature).type-&gt;exists(oclIsKind(Class)))</c>
    /// <para>Hand written because the owner disjunct navigates <c>oclAsType(Feature).type</c>, which is
    /// outside the generator's translatable shapes.</para>
    /// </remarks>
    public class OccurrenceUsageSuboccurrenceSpecializationGuard : IImpliedRuleGuard
    {
        /// <summary>
        /// Gets the name of the semantic constraint this guard decides.
        /// </summary>
        public string ConstraintName => "checkOccurrenceUsageSuboccurrenceSpecialization";

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
                : element is IOccurrenceUsage { IsComposite: true } occurrenceUsage
                  && (occurrenceUsage.owningType is IOccurrenceUsage
                      || OwningTypePredicates.IsOrIsTypedBy<IClass>(occurrenceUsage.owningType));
        }
    }
}
