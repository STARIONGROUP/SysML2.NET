// ------------------------------------------------------------------------------------------------
// <copyright file="ExternalData.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.PIM.POCO
{
    using System;

    using SysML2.NET.Common;
    using SysML2.NET.Decorators;

    /// <summary>
    /// ExternalData is a realization of Data, and represents a resource external to a given tool or
    /// repository. ExternalData is defined only for the purpose of defining an ExternalRelationship
    /// </summary>
    public class ExternalData : IData
    {
        /// <summary>
        /// Gets or sets the unique identifier
        /// </summary>
        [Property(xmiId: "sysml2.net", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the string representation of the IRI of the resource represented by the ExternalData
        /// </summary>
        [Property(xmiId: "_19_0_2_8bf0285_1596606723805_618012_132", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        public string ResourceIdentifier { get; set; }
    }
}
