// -------------------------------------------------------------------------------------------------
// <copyright file="IFeatureTyping.cs" company="Starion Group S.A.">
//
//    Copyright (C) 2022-2025 Starion Group S.A.
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

namespace SysML2.NET.Core.POCO.Core.Features
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;

    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Root.Annotations;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Decorators;

    /// <summary>
    /// FeatureTyping is Specialization in which the specific Type is a Feature. This means the set of
    /// instances of the (specific) typedFeature is a subset of the set of instances of the (general) type.
    /// In the simplest case, the type is a Classifier, whereupon the typedFeature has values that are
    /// instances of the Classifier.
    /// </summary>
    [Class(xmiId: "_18_5_3_12e503d9_1543180339807_437641_20928", isAbstract: false, isFinalSpecialization: false, isActive: false)]
    [GeneratedCode("SysML2.NET", "latest")]
    public partial interface IFeatureTyping : ISpecialization
    {
        /// <summary>
        /// A typedFeature that is also the owningRelatedElement of this FeatureTyping.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1596597427753_801746_43", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1543180501615_13273_21101")]
        [RedefinedProperty(propertyName: "_18_5_3_12e503d9_1533160674971_573157_43226")]
        IFeature QueryOwningFeature();

        /// <summary>
        /// The Type that is being applied by this FeatureTyping.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1543180520185_480887_21131", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "_18_5_3_12e503d9_1533160674980_563969_43273")]
        IType Type { get; set; }

        /// <summary>
        /// The Feature that has a type determined by this FeatureTyping.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1543180501615_13273_21101", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "_18_5_3_12e503d9_1533160674982_253967_43281")]
        IFeature TypedFeature { get; set; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
