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

    using SysML2.NET.Core.Core.Types;
    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Kernel.Expressions;
    using SysML2.NET.Core.POCO.Kernel.Functions;
    using SysML2.NET.Core.POCO.Kernel.Multiplicities;
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
        /// Creates a literal expression with its result parameter
        /// </summary>
        /// <typeparam name="TLiteral">
        /// The literal POCO type
        /// </typeparam>
        /// <param name="name">
        /// The name the elements are derived from
        /// </param>
        /// <param name="toDto">
        /// The generated <c>ToDto</c> extension of <typeparamref name="TLiteral"/>
        /// </param>
        /// <param name="setValue">
        /// Sets the value of the literal
        /// </param>
        /// <returns>
        /// The literal expression
        /// </returns>
        protected TLiteral Literal<TLiteral>(string name, Func<TLiteral, bool, DtoElement> toDto, Action<TLiteral> setValue) where TLiteral : IExpression, new()
        {
            var literal = this.Builder.Create(name, toDto);
            setValue(literal);
            this.Result(literal, name);

            return literal;
        }

        /// <summary>
        /// Gives a feature the multiplicity <c>[1]</c>, featured like the feature
        /// </summary>
        /// <param name="feature">
        /// The feature
        /// </param>
        /// <param name="featuringType">
        /// The type that features <paramref name="feature"/>
        /// </param>
        protected void MultiplicityOne(IFeature feature, IType featuringType)
        {
            this.MultiplicityRange(feature, featuringType, this.Literal<LiteralInteger>($"{feature.ElementId}/multiplicity/1", LiteralIntegerExtensions.ToDto, literal => literal.Value = 1));
        }

        /// <summary>
        /// Gives a feature a multiplicity range with the given bounds, featured like the feature as KerML requires
        /// (<c>validateFeatureMultiplicityDomain</c>)
        /// </summary>
        /// <param name="feature">
        /// The feature
        /// </param>
        /// <param name="featuringType">
        /// The type that features <paramref name="feature"/>
        /// </param>
        /// <param name="bounds">
        /// The bounds: the upper bound alone, or the lower and the upper bound
        /// </param>
        protected void MultiplicityRange(IFeature feature, IType featuringType, params IExpression[] bounds)
        {
            var range = this.Builder.Create<MultiplicityRange>($"{feature.ElementId}/multiplicity", MultiplicityRangeExtensions.ToDto);

            foreach (var bound in bounds)
            {
                this.OwnMember(range, bound);
            }

            this.OwnMember(feature, range);

            var featuring = this.Builder.Create<TypeFeaturing>($"{feature.ElementId}/multiplicity/featuring", TypeFeaturingExtensions.ToDto);
            featuring.FeatureOfType = range;
            featuring.FeaturingType = featuringType;
            range.AssignOwnership(featuring);
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

        /// <summary>
        /// Adds a parameter to a behavior or expression through a parameter membership
        /// </summary>
        /// <typeparam name="TMembership">
        /// The parameter membership POCO type
        /// </typeparam>
        /// <param name="owner">
        /// The behavior or expression
        /// </param>
        /// <param name="name">
        /// The name of the parameter
        /// </param>
        /// <param name="direction">
        /// The direction of the parameter
        /// </param>
        /// <param name="toDto">
        /// The generated <c>ToDto</c> extension of <typeparamref name="TMembership"/>
        /// </param>
        /// <returns>
        /// The parameter
        /// </returns>
        protected Feature Parameter<TMembership>(IType owner, string name, FeatureDirectionKind direction, Func<TMembership, bool, DtoElement> toDto) where TMembership : PocoRelationship, new()
        {
            var parameter = this.Builder.Create<Feature>(name, FeatureExtensions.ToDto);
            parameter.DeclaredName = name[(name.LastIndexOf('/') + 1)..];
            parameter.Direction = direction;

            var membership = this.Builder.Create($"{name}/membership", toDto);
            owner.AssignOwnership(membership, parameter);

            return parameter;
        }

        /// <summary>
        /// Adds the result parameter to an expression or function, through a <see cref="ReturnParameterMembership"/>
        /// </summary>
        /// <param name="owner">
        /// The expression or function
        /// </param>
        /// <param name="name">
        /// The name the elements are derived from
        /// </param>
        protected void Result(IType owner, string name)
        {
            this.Parameter<ReturnParameterMembership>(owner, $"{name}/result", FeatureDirectionKind.Out, ReturnParameterMembershipExtensions.ToDto);
        }

        /// <summary>
        /// Types a feature with a <see cref="FeatureTyping"/>
        /// </summary>
        /// <param name="feature">
        /// The feature
        /// </param>
        /// <param name="type">
        /// The type
        /// </param>
        protected void Typed(IFeature feature, IType type)
        {
            var typing = this.Builder.Create<FeatureTyping>($"{feature.ElementId}/typing", FeatureTypingExtensions.ToDto);
            typing.TypedFeature = feature;
            typing.Type = type;
            feature.AssignOwnership(typing);
        }
    }
}
