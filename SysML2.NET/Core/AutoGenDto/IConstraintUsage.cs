// -------------------------------------------------------------------------------------------------
// <copyright file="IConstraintUsage.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Core.DTO
{
    using System;
    using System.Collections.Generic;

    using SysML2.NET.Core;
    using SysML2.NET.Decorators;

    /// <summary>
    /// A ConstraintUsage is an OccurrenceUsage that is also a BooleanExpression, and, so, is typed by a
    /// Predicate. Nominally, if the type is a ConstraintDefinition, a ConstraintUsage is a Usage of that
    /// ConstraintDefinition. However, other kinds of kernel Predicates are also allowed, to permit use of
    /// Predicates from the Kernel Model Libraries.owningFeatureMembership <> null
    /// andowningFeatureMembership.oclIsKindOf(RequirementConstraintMembership) implies    if
    /// owningFeatureMembership.oclAsType(RequirementConstraintMembership).kind =        
    /// RequirementConstraintKind::assumption then       
    /// specializesFromLibrary('Requirements::RequirementCheck::assumptions')    else       
    /// specializesFromLibrary('Requirements::RequirementCheck::constraints')   
    /// endifspecializesFromLibrary('Constraints::constraintChecks')owningType <> null
    /// and(owningType.oclIsKindOf(ItemDefinition) or owningType.oclIsKindOf(ItemUsage)) implies   
    /// specializesFromLibrary('Items::Item::checkedConstraints')
    /// </summary>
    public partial interface IConstraintUsage : IOccurrenceUsage, IBooleanExpression
    {
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
