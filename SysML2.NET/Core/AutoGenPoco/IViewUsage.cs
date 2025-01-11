// -------------------------------------------------------------------------------------------------
// <copyright file="IViewUsage.cs" company="Starion Group S.A.">
//
//   Copyright 2022-2025 Starion Group S.A.
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
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

namespace SysML2.NET.Core.POCO
{
    using System;
    using System.Collections.Generic;

    using SysML2.NET.Core;
    using SysML2.NET.Decorators;

    /// <summary>
    /// A ViewUsage is a usage of a ViewDefinition to specify the generation of a view of the members of a
    /// collection of exposedNamespaces. The ViewUsage can satisfy more viewpoints than its definition, and
    /// it can specialize the viewRendering specified by its definition.exposedElement =
    /// ownedImport->selectByKind(Expose).    importedMemberships(Set{}).memberElement->    select(elm |
    /// includeAsExposed(elm))->    asOrderedSet()satisfiedViewpoint = ownedRequirement->   
    /// selectByKind(ViewpointUsage)->    select(isComposite)viewCondition = ownedMembership->   
    /// selectByKind(ElementFilterMembership).    conditionviewRendering =    let renderings:
    /// OrderedSet(ViewRenderingMembership) =       
    /// featureMembership->selectByKind(ViewRenderingMembership) in    if renderings->isEmpty() then null   
    /// else renderings->first().referencedRendering    endiffeatureMembership->   
    /// selectByKind(ViewRenderingMembership)->    size() <=
    /// 1specializesFromLibrary('Views::views')owningType <> null and(owningType.oclIsKindOf(ViewDefinition)
    /// or owningType.oclIsKindOf(ViewUsage)) implies    specializesFromLibrary('Views::View::subviews')
    /// </summary>
    public partial interface IViewUsage : IPartUsage
    {
        /// <summary>
        /// Queries the derived property ExposedElement
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: true, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<IElement> QueryExposedElement();

        /// <summary>
        /// Queries the derived property SatisfiedViewpoint
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: true, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<ViewpointUsage> QuerySatisfiedViewpoint();

        /// <summary>
        /// Queries the derived property ViewCondition
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: true, isUnique: true, lowerBound: 0, upperBound: -1, isMany: false, isRequired: false, isContainment: false)]
        List<Expression> QueryViewCondition();

        /// <summary>
        /// Queries the derived property ViewDefinition
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: false, isUnique: true, lowerBound: 0, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        ViewDefinition QueryViewDefinition();

        /// <summary>
        /// Queries the derived property ViewRendering
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: false, isUnique: true, lowerBound: 0, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        RenderingUsage QueryViewRendering();

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
