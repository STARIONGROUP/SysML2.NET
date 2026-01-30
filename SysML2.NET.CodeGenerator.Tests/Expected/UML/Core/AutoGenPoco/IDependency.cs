// -------------------------------------------------------------------------------------------------
// <copyright file="IDependency.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Core.POCO.Root.Dependencies
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;

    using SysML2.NET.Core.POCO.Root.Annotations;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Decorators;

    /// <summary>
    /// A Dependency is a Relationship that indicates that one or more client Elements require one more
    /// supplier Elements for their complete specification. In general, this means that a change to one of
    /// the supplier Elements may necessitate a change to, or re-specification of, the client Elements. 
    /// Note that a Dependency is entirely a model-level Relationship, without instance-level semantics.
    /// </summary>
    [Class(xmiId: "Root-Dependencies-Dependency", isAbstract: false, isFinalSpecialization: false, isActive: false)]
    [GeneratedCode("SysML2.NET", "latest")]
    public partial interface IDependency : IRelationship
    {
        /// <summary>
        /// The Element or Elements dependent on the supplier Elements.
        /// </summary>
        [Property(xmiId: "Root-Dependencies-Dependency-client", aggregation: AggregationKind.None, lowerValue: 1, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "Root-Elements-Relationship-source")]
        List<IElement> Client { get; set; }

        /// <summary>
        /// The Element or Elements on which the client Elements depend in some respect.
        /// </summary>
        [Property(xmiId: "Root-Dependencies-Dependency-supplier", aggregation: AggregationKind.None, lowerValue: 1, upperValue: -1, isOrdered: true, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "Root-Elements-Relationship-target")]
        List<IElement> Supplier { get; set; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
