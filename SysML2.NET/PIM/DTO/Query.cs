// ------------------------------------------------------------------------------------------------
// <copyright file="Query.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.PIM.DTO
{
    using System;
    using System.Collections.Generic;

    using SysML2.NET.Decorators;

    /// <summary>
    /// <see cref="Query"/> is a subclass of <see cref="Record"/> that represents a precise and language-independent
    /// request for information  retrieval using the Systems Modeling API and Services.
    /// <see cref="Query"/> can be mapped to commonly used query languages, such as SQL, Gremlin, GraphQL, and SPARQL
    /// </summary>
    public class Query : Record
    {
        /// <summary>
        /// Gets or sets the name of the Query
        /// </summary>
        [Property(xmiId: "_19_0_4_58901f1_1671293480578_778123_7740", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "Record.Name")]
        public new string Name { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="DTO.Project"/> that owns the <see cref="Query"/>
        /// </summary>
        [Property(xmiId: "_19_0_4_58901f1_1597396233221_999408_902", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        public Guid Project { get; set; }

        /// <summary>
        /// list of properties of Element or its subtypes that will be included for each Element object in the
        /// query response. Element is the root-metaclass in KerML. If no properties are specified, then all the
        /// properties will be included for each Element in the query response
        /// </summary>
        [Property(xmiId: "_19_0_4_58901f1_1597377655027_119603_507", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        public List<string> Select { get; set; } = [];

        /// <summary>
        /// represents the conditions that Elements in the query response must satisfy
        /// </summary>
        [Property(xmiId: "_19_0_4_58901f1_1597377630809_759194_505", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        public Constraint Where { get; set; }

        /// <summary>
        /// list of Data objects that define the scope context for query execution. The default scope of a
        /// Query is the owning Project
        /// </summary>
        [Property(xmiId: "_19_0_4_58901f1_1597377703876_758020_511", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        public List<Guid> Scope { get; set; } = [];

        /// <summary>
        /// Gets or sets the list of properties of Data (or its realizations) used for sorting the query
        /// response. The order of the properties in the list governs the sorting order.
        /// </summary>
        [Property(xmiId: "_19_0_4_58901f1_1597387733264_611302_866", aggregation: AggregationKind.None, lowerValue: 0, upperValue: int.MaxValue, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        public List<string> OrderBy { get; set; } = [];
    }
}
