// ------------------------------------------------------------------------------------------------
// <copyright file="DefaultSeedModel.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SySML2.NET.REST.Tests.Integration
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    using SysML2.NET.Core.DTO.Kernel.Packages;
    using SysML2.NET.Core.DTO.Root.Elements;
    using SysML2.NET.Core.DTO.Root.Namespaces;
    using SysML2.NET.Core.DTO.Systems.Parts;
    using SysML2.NET.Core.Root.Namespaces;
    using SysML2.NET.PSM.DTO;

    /// <summary>
    /// Builds the deterministic default model committed to a live server: a root package owning one part definition
    /// </summary>
    public static class DefaultSeedModel
    {
        /// <summary>
        /// The identifier of the <see cref="OwningMembership"/> between the package and the part definition
        /// </summary>
        private static readonly Guid MembershipId = Guid.Parse("8c1f4b2a-3d5e-4f60-9a7b-1c2d3e4f5a61");

        /// <summary>
        /// Gets the identifier of the root <see cref="Package"/>
        /// </summary>
        public static Guid PackageId { get; } = Guid.Parse("8c1f4b2a-3d5e-4f60-9a7b-1c2d3e4f5a60");

        /// <summary>
        /// Gets the identifier of the <see cref="PartDefinition"/>
        /// </summary>
        public static Guid PartDefinitionId { get; } = Guid.Parse("8c1f4b2a-3d5e-4f60-9a7b-1c2d3e4f5a62");

        /// <summary>
        /// Gets the declared name of the <see cref="PartDefinition"/> in the seed commit
        /// </summary>
        public static string PartDefinitionName { get; } = "Vehicle";

        /// <summary>
        /// Creates the <see cref="CommitRequest"/> that adds the whole default model
        /// </summary>
        /// <returns>
        /// The <see cref="CommitRequest"/> carrying one change per element
        /// </returns>
        public static CommitRequest CreateCommitRequest()
        {
            var package = new Package
            {
                Id = PackageId,
                ElementId = PackageId.ToString(),
                DeclaredName = "SysML2.NET seed",
                OwnedRelationship = [MembershipId]
            };

            var membership = new OwningMembership
            {
                Id = MembershipId,
                ElementId = MembershipId.ToString(),
                Visibility = VisibilityKind.Public,
                OwningRelatedElement = PackageId,
                OwnedRelatedElement = [PartDefinitionId]
            };

            return CreateCommitRequest("Seed the default model", [package, membership, CreatePartDefinition(PartDefinitionName)]);
        }

        /// <summary>
        /// Creates the <see cref="CommitRequest"/> that renames the <see cref="PartDefinition"/>
        /// </summary>
        /// <param name="name">
        /// The new declared name
        /// </param>
        /// <returns>
        /// The <see cref="CommitRequest"/> carrying the one changed element
        /// </returns>
        public static CommitRequest CreateRenameRequest(string name)
        {
            return CreateCommitRequest(string.Create(CultureInfo.InvariantCulture, $"Rename the part definition to {name}"), [CreatePartDefinition(name)]);
        }

        /// <summary>
        /// Creates the <see cref="PartDefinition"/> owned by the seed membership
        /// </summary>
        /// <param name="name">
        /// The declared name
        /// </param>
        /// <returns>
        /// The <see cref="PartDefinition"/>
        /// </returns>
        private static PartDefinition CreatePartDefinition(string name)
        {
            return new PartDefinition
            {
                Id = PartDefinitionId,
                ElementId = PartDefinitionId.ToString(),
                DeclaredName = name,
                OwningRelationship = MembershipId
            };
        }

        /// <summary>
        /// Wraps each element in a <see cref="DataVersionRequest"/> of one <see cref="CommitRequest"/>
        /// </summary>
        /// <param name="description">
        /// The description of the commit
        /// </param>
        /// <param name="elements">
        /// The elements to commit
        /// </param>
        /// <returns>
        /// The <see cref="CommitRequest"/>
        /// </returns>
        private static CommitRequest CreateCommitRequest(string description, IEnumerable<IElement> elements)
        {
            return new CommitRequest
            {
                Description = description,
                Change = [.. elements.Select(element => new DataVersionRequest { Identity = new DataIdentityRequest { Id = element.Id }, Payload = element })]
            };
        }
    }
}
