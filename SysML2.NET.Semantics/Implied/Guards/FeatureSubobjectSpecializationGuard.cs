// -------------------------------------------------------------------------------------------------
// <copyright file="FeatureSubobjectSpecializationGuard.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Core.POCO.Kernel.Structures;
    using SysML2.NET.Core.POCO.Root.Elements;

    /// <summary>
    /// Guards checkFeatureSubobjectSpecialization: a composite Structure-typed Feature owned by a Structure specializes Occurrence::Occurrence::suboccurrences.
    /// </summary>
    /// <remarks>
    /// OCL: <c>isComposite and ownedTyping.type-&gt;includes(oclIsKindOf(Structure)) and owningType &lt;&gt; null and (owningType.oclIsKindOf(Structure) or owningType.type-&gt;includes(oclIsKindOf(Structure)))</c>
    /// <para>Hand written because the owner disjunct navigates <c>oclAsType(Feature).type</c>, which is
    /// outside the generator's translatable shapes.</para>
    /// </remarks>
    public class FeatureSubobjectSpecializationGuard : IImpliedRuleGuard
    {
        /// <summary>
        /// Gets the name of the semantic constraint this guard decides.
        /// </summary>
        public string ConstraintName => "checkFeatureSubobjectSpecialization";

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
                  && feature.ownedTyping.Any(featureTyping => featureTyping.Type is IStructure)
                  && OwningTypePredicates.IsOrIsTypedBy<IStructure>(feature.owningType);
        }
    }
}
