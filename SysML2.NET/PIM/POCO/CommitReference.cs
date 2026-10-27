// ------------------------------------------------------------------------------------------------
// <copyright file="CommitReference.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.PIM.POCO
{
    using System;

    using SysML2.NET.Decorators;

    /// <summary>
    /// An abstract subclass of <see cref="Record"/> that references a specific <see cref="Commit"/> (Commit Reference.referencedCommit). Project.commit is the set of all the Commit records for a given Project.
    /// <see cref="Project.CommitReferences"/> identifies specific <see cref="Commit"/> records in a <see cref="Project"/> that provide the context for navigating the
    /// <see cref="Common.IData"/> in a Project. Two special types of <see cref="CommitReference"/> are <see cref="Branch"/> and <see cref="Tag"/>
    /// </summary>
    public abstract class CommitReference : Record
    {
        /// <summary>
        /// Gets or sets the timestamp at which the <see cref="CommitReference"/> was created.
        /// </summary>
        [Property(xmiId: "_19_0_4_58901f1_1628778313797_665973_538", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        public DateTime Created { get; set; }

        /// <summary>
        /// Gets or sets the timestamp at which the <see cref="CommitReference"/> was deleted, or null
        /// when the <see cref="CommitReference"/> has not been deleted
        /// </summary>
        [Property(xmiId: "_2022x_2_f940371_1740179401841_624617_283", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        public DateTime? Deleted { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="Commit"/> that the <see cref="CommitReference"/> references.
        /// <see cref="Branch.Head"/> and <see cref="Tag.TaggedCommit"/> redefine this property.
        /// </summary>
        public Commit ReferencedCommit { get; set; }

        /// <summary>
        /// Gets or sets the name of the <see cref="CommitReference"/>
        /// </summary>
        [Property(xmiId: "_19_0_4_58901f1_1671293488580_668503_7742", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [RedefinedProperty(propertyName: "Record.Name")]
        public new string Name { get; set; }

        /// <summary>
        /// Gets or sets the Project that owns the given <see cref="CommitReference"/>
        /// </summary>
        [Property(xmiId: "_19_0_4_58901f1_1628779129336_65342_713", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        public Project OwningProject { get; set; }
    }
}
