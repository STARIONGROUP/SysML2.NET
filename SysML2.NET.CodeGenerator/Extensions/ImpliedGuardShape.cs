// -------------------------------------------------------------------------------------------------
// <copyright file="ImpliedGuardShape.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.CodeGenerator.Extensions
{
    /// <summary>
    /// The mechanically translatable shapes a semantic-constraint guard expression can take.
    /// </summary>
    /// <remarks>
    /// A guard is the antecedent of an <c>&lt;guard&gt; implies specializesFromLibrary('…')</c> constraint.
    /// These shapes cover the majority of them; anything else is reported as
    /// <see cref="RequiresHandCoding" /> so it is never silently mistranslated.
    /// </remarks>
    public enum ImpliedGuardShape
    {
        /// <summary>
        /// The guard is not one of the recognised shapes and must be written by hand.
        /// </summary>
        RequiresHandCoding,

        /// <summary>
        /// A bare boolean property, e.g. <c>isIndividual</c>.
        /// </summary>
        BooleanProperty,

        /// <summary>
        /// A boolean operation call, optionally negated, e.g. <c>isSubactionUsage()</c>,
        /// <c>not isTriggerAction()</c>, <c>isSubstateUsage(true)</c>.
        /// </summary>
        OperationCall,

        /// <summary>
        /// An owning-Type kind test over two alternatives, optionally conjoined with <c>isComposite</c>,
        /// e.g. <c>owningType &lt;&gt; null and (owningType.oclIsKindOf(PartDefinition) or
        /// owningType.oclIsKindOf(PartUsage))</c>.
        /// </summary>
        OwningTypeKind,

        /// <summary>
        /// An owned-end-Feature cardinality test, e.g. <c>ownedEndFeature-&gt;size() = 2</c> or
        /// <c>ownedEndFeatures-&gt;notEmpty()</c>.
        /// </summary>
        OwnedEndFeatureCount,

        /// <summary>
        /// An owned-typing kind test, e.g. <c>ownedTyping.type-&gt;exists(selectByKind(DataType))</c>.
        /// </summary>
        OwnedTypingKind,

        /// <summary>
        /// An owning-FeatureMembership kind test, e.g. <c>owningFeatureMembership &lt;&gt; null and
        /// owningFeatureMembership.oclIsKindOf(StakeholderMembership)</c>.
        /// </summary>
        OwningFeatureMembershipKind,

        /// <summary>
        /// An enumeration-literal comparison, e.g. <c>portionKind = PortionKind::timeslice</c>.
        /// </summary>
        EnumerationComparison
    }
}
