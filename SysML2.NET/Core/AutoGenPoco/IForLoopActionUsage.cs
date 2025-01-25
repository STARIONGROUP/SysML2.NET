// -------------------------------------------------------------------------------------------------
// <copyright file="IForLoopActionUsage.cs" company="Starion Group S.A.">
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
    /// A ForLoopActionUsage is a LoopActionUsage that specifies that its bodyAction ActionUsage should be
    /// performed once for each value, in order, from the sequence of values obtained as the result of the
    /// seqArgument Expression, with the loopVariable set to the value for each iteration.seqArgument =
    /// argument(1)isSubactionUsage() implies   
    /// specializesFromLibrary('Actions::Action::forLoops')loopVariable <> null
    /// andloopVariable.redefinesFromLibrary('Actions::ForLoopAction::var')specializesFromLibrary('Actions::forLoopActions')loopVariable
    /// =    if ownedFeature->isEmpty() or         not ownedFeature->first().oclIsKindOf(ReferenceUsage)
    /// then         null    else         ownedFeature->first().oclAsType(ReferenceUsage)   
    /// endifownedFeature->notEmpty()
    /// andownedFeature->at(1).oclIsKindOf(ReferenceUsage)inputParameters()->size() = 2
    /// </summary>
    public partial interface IForLoopActionUsage : ILoopActionUsage
    {
        /// <summary>
        /// Queries the derived property LoopVariable
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: false, isUnique: true, lowerBound: 1, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        ReferenceUsage QueryLoopVariable();

        /// <summary>
        /// Queries the derived property SeqArgument
        /// </summary>
        [EFeature(isChangeable: true, isVolatile: true, isTransient: true, isUnsettable: false, isDerived: true, isOrdered: false, isUnique: true, lowerBound: 1, upperBound: 1, isMany: false, isRequired: false, isContainment: false)]
        Expression QuerySeqArgument();

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
