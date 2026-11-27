// ------------------------------------------------------------------------------------------------
// <copyright file="MoveModel.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SySML2.NET.REST.Tests.Helpers.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using SysML2.NET.Core.POCO.Core.Classifiers;
    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Kernel.Packages;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Core.POCO.Systems.Ports;
    using SysML2.NET.Dal;
    using SysML2.NET.Extensions;

    using DtoElement = SysML2.NET.Core.DTO.Root.Elements.IElement;
    using PocoElement = SysML2.NET.Core.POCO.Root.Elements.IElement;
    using PocoRelationship = SysML2.NET.Core.POCO.Root.Elements.IRelationship;
    using Type = SysML2.NET.Core.POCO.Core.Types.Type;

    /// <summary>
    /// The content of a package <c>Moves</c> for the update U5 of scenario step 12, after plan §6.1: each relationship
    /// whose source end is its owner moves to a new owner, which it then has as its source end, two port definitions
    /// exchange their conjugated port definitions, and two members exchange their owning memberships
    /// </summary>
    /// <remarks>
    /// <code>
    /// package Moves {
    ///     type G; type S1 specializes G; type S2; type C1 ~ G; type C2; type D1 disjoint from G; type D2;
    ///     classifier KG; classifier K1 specializes KG; classifier K2;
    ///     feature FG; feature FT1 : G; feature FT2; feature FS1 subsets FG; feature FS2;
    ///     feature FI1 inverse of FG; feature FI2; feature TF1 featured by G; feature TF2;
    ///     type RB { feature rg; }   type RS { feature r1 redefines RB::rg; feature r2; }
    ///     port def PA;   port def PB;   // each owns its conjugated port definition
    ///     package X;   package Y;
    /// }
    /// </code>
    /// The public SDK cannot take a relationship out of an ownership list (#427), so <see cref="Moved"/> builds the
    /// moved content in a model builder of its own, with the same names and so the same ids; the model builder of the
    /// scenario no longer describes the server's model once the moves are committed.
    /// </remarks>
    public sealed class MoveModel : StepModel
    {
        /// <summary>
        /// The scope of the content of this step
        /// </summary>
        private const string Scope = "move";

        /// <summary>
        /// Per moved relationship: the relationship, and its owner before and after the move
        /// </summary>
        private readonly List<(Guid Relationship, Guid From, Guid To)> moves = [];

        /// <summary>
        /// Initializes a new instance of the <see cref="MoveModel"/> class and builds the content to move
        /// </summary>
        /// <param name="root">
        /// The model of step 2
        /// </param>
        public MoveModel(RootModel root) : this(root, false)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MoveModel"/> class and builds the content before or after the moves
        /// </summary>
        /// <param name="root">
        /// The model of step 2
        /// </param>
        /// <param name="moved">
        /// Whether to build the content as it is after the moves
        /// </param>
        private MoveModel(RootModel root, bool moved) : base((root ?? throw new ArgumentNullException(nameof(root))).Builder, Scope)
        {
            this.Package = this.CreateNamed<Package>("Moves", "mov", PackageExtensions.ToDto);
            this.OwnMember(root.Root, this.Package);

            var general = this.Member<Type>("G", TypeExtensions.ToDto);
            var classifierGeneral = this.Member<Classifier>("KG", ClassifierExtensions.ToDto);
            var featureGeneral = this.Member<Feature>("FG", FeatureExtensions.ToDto);

            this.Relate<Specialization>(this.Member<Type>("S1", TypeExtensions.ToDto), this.Member<Type>("S2", TypeExtensions.ToDto), moved, "specialization", SpecializationExtensions.ToDto, (relationship, owner) =>
            {
                relationship.Specific = (Type)owner;
                relationship.General = general;
            });

            this.Relate<Conjugation>(this.Member<Type>("C1", TypeExtensions.ToDto), this.Member<Type>("C2", TypeExtensions.ToDto), moved, "conjugation", ConjugationExtensions.ToDto, (relationship, owner) =>
            {
                relationship.ConjugatedType = (Type)owner;
                relationship.OriginalType = general;
            });

            this.Relate<Disjoining>(this.Member<Type>("D1", TypeExtensions.ToDto), this.Member<Type>("D2", TypeExtensions.ToDto), moved, "disjoining", DisjoiningExtensions.ToDto, (relationship, owner) =>
            {
                relationship.TypeDisjoined = (Type)owner;
                relationship.DisjoiningType = general;
            });

            this.Relate<Subclassification>(this.Member<Classifier>("K1", ClassifierExtensions.ToDto), this.Member<Classifier>("K2", ClassifierExtensions.ToDto), moved, "subclassification", SubclassificationExtensions.ToDto, (relationship, owner) =>
            {
                relationship.Subclassifier = (Classifier)owner;
                relationship.Superclassifier = classifierGeneral;
            });

            this.Relate<FeatureTyping>(this.Member<Feature>("FT1", FeatureExtensions.ToDto), this.Member<Feature>("FT2", FeatureExtensions.ToDto), moved, "typing", FeatureTypingExtensions.ToDto, (relationship, owner) =>
            {
                relationship.TypedFeature = (Feature)owner;
                relationship.Type = general;
            });

            this.Relate<Subsetting>(this.Member<Feature>("FS1", FeatureExtensions.ToDto), this.Member<Feature>("FS2", FeatureExtensions.ToDto), moved, "subsetting", SubsettingExtensions.ToDto, (relationship, owner) =>
            {
                relationship.SubsettingFeature = (Feature)owner;
                relationship.SubsettedFeature = featureGeneral;
            });

            this.Relate<FeatureInverting>(this.Member<Feature>("FI1", FeatureExtensions.ToDto), this.Member<Feature>("FI2", FeatureExtensions.ToDto), moved, "inverting", FeatureInvertingExtensions.ToDto, (relationship, owner) =>
            {
                relationship.FeatureInverted = (Feature)owner;
                relationship.InvertingFeature = featureGeneral;
            });

            this.Relate<TypeFeaturing>(this.Member<Feature>("TF1", FeatureExtensions.ToDto), this.Member<Feature>("TF2", FeatureExtensions.ToDto), moved, "featuring", TypeFeaturingExtensions.ToDto, (relationship, owner) =>
            {
                relationship.FeatureOfType = (Feature)owner;
                relationship.FeaturingType = moved ? classifierGeneral : general;
            });

            var redefinedOwner = this.Member<Type>("RB", TypeExtensions.ToDto);
            var redefined = this.CreateNamed<Feature>("rg", "rg", FeatureExtensions.ToDto);
            this.OwnFeature(redefinedOwner, redefined);
            var redefiningOwner = this.Member<Type>("RS", TypeExtensions.ToDto);
            var firstRedefining = this.CreateNamed<Feature>("r1", "r1", FeatureExtensions.ToDto);
            var secondRedefining = this.CreateNamed<Feature>("r2", "r2", FeatureExtensions.ToDto);
            this.OwnFeature(redefiningOwner, firstRedefining);
            this.OwnFeature(redefiningOwner, secondRedefining);
            this.Relate<Redefinition>(firstRedefining, secondRedefining, moved, "redefinition", RedefinitionExtensions.ToDto, (relationship, owner) =>
            {
                relationship.RedefiningFeature = (Feature)owner;
                relationship.RedefinedFeature = redefined;
            });

            this.FirstPortDefinition = this.Member<PortDefinition>("PA", PortDefinitionExtensions.ToDto);
            this.SecondPortDefinition = this.Member<PortDefinition>("PB", PortDefinitionExtensions.ToDto);
            this.FirstConjugatedPortDefinition = this.Conjugated("PA", this.FirstPortDefinition, this.SecondPortDefinition, moved);
            this.SecondConjugatedPortDefinition = this.Conjugated("PB", this.SecondPortDefinition, this.FirstPortDefinition, moved);

            this.First = this.CreateNamed<Package>("X", "x", PackageExtensions.ToDto);
            this.Second = this.CreateNamed<Package>("Y", "y", PackageExtensions.ToDto);
            this.Package.AssignOwnership(this.Builder.Create<OwningMembership>("X/membership", OwningMembershipExtensions.ToDto), moved ? this.Second : this.First);
            this.Package.AssignOwnership(this.Builder.Create<OwningMembership>("Y/membership", OwningMembershipExtensions.ToDto), moved ? this.First : this.Second);
        }

        /// <summary>
        /// Gets the package that holds the content to move
        /// </summary>
        public Package Package { get; }

        /// <summary>
        /// Gets the port definition <c>PA</c>
        /// </summary>
        public PortDefinition FirstPortDefinition { get; }

        /// <summary>
        /// Gets the port definition <c>PB</c>
        /// </summary>
        public PortDefinition SecondPortDefinition { get; }

        /// <summary>
        /// Gets the conjugated port definition that <see cref="FirstPortDefinition"/> owns before the moves and
        /// <see cref="SecondPortDefinition"/> after them
        /// </summary>
        public ConjugatedPortDefinition FirstConjugatedPortDefinition { get; }

        /// <summary>
        /// Gets the conjugated port definition that <see cref="SecondPortDefinition"/> owns before the moves and
        /// <see cref="FirstPortDefinition"/> after them
        /// </summary>
        public ConjugatedPortDefinition SecondConjugatedPortDefinition { get; }

        /// <summary>
        /// Gets the package <c>X</c>, which exchanges its owning membership with <see cref="Second"/>
        /// </summary>
        public Package First { get; }

        /// <summary>
        /// Gets the package <c>Y</c>, which exchanges its owning membership with <see cref="First"/>
        /// </summary>
        public Package Second { get; }

        /// <summary>
        /// Gets the moved relationships, each with its owner before and after the move
        /// </summary>
        public IReadOnlyList<(Guid Relationship, Guid From, Guid To)> Moves => this.moves;

        /// <summary>
        /// Gets the elements of this step that the moves change, as they are after the moves
        /// </summary>
        /// <param name="model">
        /// The model that holds the content of this step before the moves, which is not changed
        /// </param>
        /// <returns>
        /// The DTO of every element of this step that differs from its version in <paramref name="model"/>
        /// </returns>
        public static IReadOnlyList<DtoElement> Moved(IReadOnlyList<DtoElement> model)
        {
            ArgumentNullException.ThrowIfNull(model);

            var after = new MoveModel(new RootModel(), true);
            var ids = after.Builder.Elements.Where(entry => entry.Name.StartsWith($"{Scope}/", StringComparison.Ordinal)).Select(entry => entry.Element.Id).ToHashSet();

            return ModelBuilder.Changed(model, [.. after.Builder.ToDtos().Where(dto => ids.Contains(dto.Id))]);
        }

        /// <summary>
        /// Creates the conjugated port definition of a port definition, with its port conjugation, owned by that port
        /// definition before the moves and by the other one after them
        /// </summary>
        /// <param name="name">
        /// The name of the port definition, which scopes the names of what is created
        /// </param>
        /// <param name="from">
        /// The port definition that owns the conjugated port definition before the moves
        /// </param>
        /// <param name="to">
        /// The port definition that owns it after the moves
        /// </param>
        /// <param name="moved">
        /// Whether to build the content as it is after the moves
        /// </param>
        /// <returns>
        /// The conjugated port definition
        /// </returns>
        private ConjugatedPortDefinition Conjugated(string name, PortDefinition from, PortDefinition to, bool moved)
        {
            var owner = moved ? to : from;
            var conjugated = this.Builder.Create<ConjugatedPortDefinition>($"{name}/conjugated", ConjugatedPortDefinitionExtensions.ToDto);
            var membership = this.Builder.Create<OwningMembership>($"{name}/conjugated/membership", OwningMembershipExtensions.ToDto);
            owner.AssignOwnership(membership, conjugated);

            var conjugation = this.Builder.Create<PortConjugation>($"{name}/conjugated/conjugation", PortConjugationExtensions.ToDto);
            conjugation.OriginalPortDefinition = owner;
            conjugation.ConjugatedType = conjugated;
            conjugated.AssignOwnership(conjugation);

            this.moves.Add((membership.Id, from.Id, to.Id));

            return conjugated;
        }

        /// <summary>
        /// Creates a member of <see cref="Package"/>
        /// </summary>
        /// <typeparam name="TPoco">
        /// The metaclass of the member
        /// </typeparam>
        /// <param name="name">
        /// The name of the member, whose lower case is its short name
        /// </param>
        /// <param name="toDto">
        /// The generated <c>ToDto</c> extension of <typeparamref name="TPoco"/>
        /// </param>
        /// <returns>
        /// The member
        /// </returns>
        private TPoco Member<TPoco>(string name, Func<TPoco, bool, DtoElement> toDto) where TPoco : PocoElement, new()
        {
            var member = this.CreateNamed(name, name.ToLowerInvariant(), toDto);
            this.OwnMember(this.Package, member);

            return member;
        }

        /// <summary>
        /// Creates a relationship whose source end is its owner, owned by one element before the moves and by another
        /// after them
        /// </summary>
        /// <typeparam name="TRelationship">
        /// The metaclass of the relationship
        /// </typeparam>
        /// <param name="from">
        /// The owner before the moves
        /// </param>
        /// <param name="to">
        /// The owner after the moves
        /// </param>
        /// <param name="moved">
        /// Whether to build the content as it is after the moves
        /// </param>
        /// <param name="kind">
        /// The name of the relationship within the name of its first owner
        /// </param>
        /// <param name="toDto">
        /// The generated <c>ToDto</c> extension of <typeparamref name="TRelationship"/>
        /// </param>
        /// <param name="setEnds">
        /// Sets the ends of the relationship, its source end to the given owner
        /// </param>
        private void Relate<TRelationship>(PocoElement from, PocoElement to, bool moved, string kind, Func<TRelationship, bool, DtoElement> toDto, Action<TRelationship, PocoElement> setEnds) where TRelationship : PocoRelationship, new()
        {
            var owner = moved ? to : from;
            var relationship = this.Builder.Create($"{from.DeclaredName}/{kind}", toDto);
            setEnds(relationship, owner);
            owner.AssignOwnership(relationship);

            this.moves.Add((relationship.Id, from.Id, to.Id));
        }
    }
}
