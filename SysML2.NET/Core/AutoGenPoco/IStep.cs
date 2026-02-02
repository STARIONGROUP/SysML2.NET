// -------------------------------------------------------------------------------------------------
// <copyright file="IStep.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Core.POCO.Kernel.Behaviors
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;

    using SysML2.NET.Core.Core.Types;
    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Root.Annotations;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Decorators;

    /// <summary>
    /// A Step is a Feature that is typed by one or more Behaviors. Steps may be used by one Behavior to
    /// coordinate the performance of other Behaviors, supporting a steady refinement of behavioral
    /// descriptions. Steps can be ordered in time and can be connected using Flows to specify things
    /// flowing between their parameters.
    /// </summary>
    [Class(xmiId: "_18_5_3_b9102da_1536345916995_711141_17306", isAbstract: false, isFinalSpecialization: false, isActive: false)]
    [GeneratedCode("SysML2.NET", "latest")]
    public partial interface IStep : IFeature
    {
        /// <summary>
        /// The Behaviors that type this Step.
        /// </summary>
        [Property(xmiId: "_18_5_3_b9102da_1536346315176_954314_17388", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674969_376003_43216")]
        List<IBehavior> behavior { get; }

        /// <summary>
        /// The parameters of this Step, which are defined as its directedFeatures, whose values are passed into
        /// and/or out of a performance of the Step.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1595189174990_213826_657", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "_19_0_4_12e503d9_1623952188842_882068_37169")]
        List<IFeature> parameter { get; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
