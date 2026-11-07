// ------------------------------------------------------------------------------------------------
// <copyright file="StepModel.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SySML2.NET.REST.Tests.Helpers.Models
{
    using System;

    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Dal;
    using SysML2.NET.Extensions;

    using DtoElement = SysML2.NET.Core.DTO.Root.Elements.IElement;
    using PocoElement = SysML2.NET.Core.POCO.Root.Elements.IElement;
    using PocoRelationship = SysML2.NET.Core.POCO.Root.Elements.IRelationship;

    /// <summary>
    /// The base of the model of one scenario step: the elements it adds to the one <see cref="ModelBuilder"/> that
    /// holds the whole model, built with the same naming and ownership conventions in every step
    /// </summary>
    public abstract class StepModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StepModel"/> class, starting the step on the builder
        /// </summary>
        /// <param name="builder">
        /// The builder holding the whole model
        /// </param>
        /// <param name="scope">
        /// The name of the step, which scopes the ids of its elements
        /// </param>
        protected StepModel(ModelBuilder builder, string scope)
        {
            this.Builder = builder ?? throw new ArgumentNullException(nameof(builder));
            this.Builder.BeginStep(scope);
        }

        /// <summary>
        /// Gets the <see cref="ModelBuilder"/> holding every element of the model
        /// </summary>
        public ModelBuilder Builder { get; }

        /// <summary>
        /// Creates an element with a declared name, short name and alias id
        /// </summary>
        /// <typeparam name="TPoco">
        /// The POCO type
        /// </typeparam>
        /// <param name="name">
        /// The declared name, also the name its id is derived from
        /// </param>
        /// <param name="shortName">
        /// The declared short name
        /// </param>
        /// <param name="toDto">
        /// The generated <c>ToDto</c> extension of <typeparamref name="TPoco"/>
        /// </param>
        /// <returns>
        /// The created POCO
        /// </returns>
        protected TPoco CreateNamed<TPoco>(string name, string shortName, Func<TPoco, bool, DtoElement> toDto) where TPoco : PocoElement, new()
        {
            var element = this.Builder.Create(name, toDto);
            element.DeclaredName = name;
            element.DeclaredShortName = shortName;
            element.AliasIds = [$"urn:coverage-model:{name}"];

            return element;
        }

        /// <summary>
        /// Makes a feature an owned feature of a type, through a <see cref="FeatureMembership"/> or, for an end
        /// feature, an <see cref="EndFeatureMembership"/>, and features it by that type with a <see cref="TypeFeaturing"/>
        /// </summary>
        /// <param name="owner">
        /// The owning type
        /// </param>
        /// <param name="feature">
        /// The owned feature
        /// </param>
        protected void OwnFeature(IType owner, IFeature feature)
        {
            PocoRelationship membership = feature.IsEnd
                ? this.Builder.Create<EndFeatureMembership>($"{feature.ElementId}/membership", EndFeatureMembershipExtensions.ToDto)
                : this.Builder.Create<FeatureMembership>($"{feature.ElementId}/membership", FeatureMembershipExtensions.ToDto);

            owner.AssignOwnership(membership, feature);

            var featuring = this.Builder.Create<TypeFeaturing>($"{feature.ElementId}/featuring", TypeFeaturingExtensions.ToDto);
            featuring.FeatureOfType = feature;
            featuring.FeaturingType = owner;
            feature.AssignOwnership(featuring);
        }

        /// <summary>
        /// Makes an element an owned member of a namespace through a new <see cref="OwningMembership"/>
        /// </summary>
        /// <param name="owner">
        /// The owning namespace
        /// </param>
        /// <param name="member">
        /// The owned member
        /// </param>
        protected void OwnMember(PocoElement owner, PocoElement member)
        {
            var membership = this.Builder.Create<OwningMembership>($"{member.ElementId}/membership", OwningMembershipExtensions.ToDto);
            owner.AssignOwnership(membership, member);
        }
    }
}
