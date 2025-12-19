// -------------------------------------------------------------------------------------------------
// <copyright file="IConnector.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Core.POCO.Kernel.Connectors
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;

    using SysML2.NET.Core.Core.Types;
    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Kernel.Associations;
    using SysML2.NET.Core.POCO.Root.Annotations;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Decorators;

    /// <summary>
    /// A Connector is a usage of Associations, with links restricted according to instances of the Type in
    /// which they are used (domain of the Connector). The associations of the Connector restrict what kinds
    /// of things might be linked. The Connector further restricts these links to be between values of
    /// Features on instances of its domain.
    /// </summary>
    [Class(xmiId: "_18_5_3_12e503d9_1533160651698_598377_42185", isAbstract: false, isFinalSpecialization: false, isActive: false)]
    [GeneratedCode("SysML2.NET", "latest")]
    public partial interface IConnector : IFeature, IRelationship
    {
        /// <summary>
        /// The Associations that type the Connector.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674983_471497_43284", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "_18_5_3_12e503d9_1533160674969_376003_43216")]
        List<IAssociation> QueryAssociation();

        /// <summary>
        /// The endFeatures of a Connector, which redefine the endFeatures of the associations of the Connector.
        /// The connectorEnds determine via ReferenceSubsetting Relationships which Features are related by the
        /// Connector.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1556735067666_827798_21922", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "_18_5_3_12e503d9_1562476168385_824569_22106")]
        List<IFeature> QueryConnectorEnd();

        /// <summary>
        /// The innermost Type that is a common direct or indirect featuringType of the relatedFeatures, such
        /// that, if it exists and was the featuringType of this Connector, the Connector would satisfy the
        /// checkConnectorTypeFeaturing constraint.
        /// </summary>
        [Property(xmiId: "_2022x_2_12e503d9_1737751598145_444042_71", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        IType QueryDefaultFeaturingType();

        /// <summary>
        /// The Features that are related by this Connector considered as a Relationship and that restrict the
        /// links it identifies, given by the referenced Features of the connectorEnds of the Connector.
        /// </summary>
        [Property(xmiId: "_18_5_3_12e503d9_1533160674968_916334_43210", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: false, defaultValue: null)]
        [RedefinedProperty(propertyName: "_18_5_3_12e503d9_1533160674961_132339_43177")]
        List<IFeature> QueryRelatedFeature();

        /// <summary>
        /// The source relatedFeature for this Connector. It is the first relatedFeature.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1594953058873_558253_3897", aggregation: AggregationKind.None, lowerValue: 0, upperValue: 1, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674968_916334_43210")]
        [RedefinedProperty(propertyName: "_18_5_3_12e503d9_1533160674971_696758_43228")]
        IFeature QuerySourceFeature();

        /// <summary>
        /// The target relatedFeatures for this Connector. This includes all the relatedFeatures other than the
        /// sourceFeature.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1594953128207_991867_3946", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: true, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1533160674968_916334_43210")]
        [RedefinedProperty(propertyName: "_18_5_3_12e503d9_1533160674961_138197_43179")]
        List<IFeature> QueryTargetFeature();

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
