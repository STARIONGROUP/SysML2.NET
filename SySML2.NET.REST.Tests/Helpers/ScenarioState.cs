// ------------------------------------------------------------------------------------------------
// <copyright file="ScenarioState.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SySML2.NET.REST.Tests.Helpers
{
    using System;
    using System.Collections.Generic;

    using SysML2.NET.Common;

    using DtoElement = SysML2.NET.Core.DTO.Root.Elements.IElement;

    /// <summary>
    /// The state that the steps of the scenario hand on to each other: the ids the server assigned, the model as it
    /// should be at each commit, and everything the server returned
    /// </summary>
    public sealed class ScenarioState
    {
        /// <summary>
        /// Gets or sets the id of the project the model is built in, or <c>null</c> before it is created
        /// </summary>
        public Guid? ProjectId { get; set; }

        /// <summary>
        /// Gets or sets the id of the default branch of the project
        /// </summary>
        public Guid? DefaultBranchId { get; set; }

        /// <summary>
        /// Gets or sets the id of the latest commit on the default branch, or <c>null</c> before the first commit
        /// </summary>
        public Guid? HeadCommitId { get; set; }

        /// <summary>
        /// Gets the expected model at each commit, by commit id
        /// </summary>
        public Dictionary<Guid, IReadOnlyList<DtoElement>> ExpectedModels { get; } = [];

        /// <summary>
        /// Gets every data item the server returned, with the commit it was read at
        /// </summary>
        public List<(Guid CommitId, IData Data)> ReadBack { get; } = [];

        /// <summary>
        /// Gets the ids of the tags the scenario created, by name
        /// </summary>
        public Dictionary<string, Guid> TagIds { get; } = [];
    }
}
