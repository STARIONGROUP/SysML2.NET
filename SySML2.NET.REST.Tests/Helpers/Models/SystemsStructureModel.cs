// ------------------------------------------------------------------------------------------------
// <copyright file="SystemsStructureModel.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Core.POCO.Kernel.Interactions;
    using SysML2.NET.Core.POCO.Kernel.Packages;
    using SysML2.NET.Core.POCO.Systems.Allocations;
    using SysML2.NET.Core.POCO.Systems.Attributes;
    using SysML2.NET.Core.POCO.Systems.Connections;
    using SysML2.NET.Core.POCO.Systems.DefinitionAndUsage;
    using SysML2.NET.Core.POCO.Systems.Enumerations;
    using SysML2.NET.Core.POCO.Systems.Flows;
    using SysML2.NET.Core.POCO.Systems.Interfaces;
    using SysML2.NET.Core.POCO.Systems.Items;
    using SysML2.NET.Core.POCO.Systems.Occurrences;
    using SysML2.NET.Core.POCO.Systems.Parts;
    using SysML2.NET.Core.POCO.Systems.Ports;
    using SysML2.NET.Core.Systems.Occurrences;
    using SysML2.NET.Dal;
    using SysML2.NET.Extensions;

    using DtoElement = SysML2.NET.Core.DTO.Root.Elements.IElement;

    /// <summary>
    /// The model of scenario step 6: the SysML structure metaclasses, in a package <c>Systems</c> of the root package,
    /// committed on the branch <c>systems</c>
    /// </summary>
    /// <remarks>
    /// <code>
    /// package Systems {
    ///     part def Q;  attribute def A;  occurrence def O;  item def I { attribute content : A; }
    ///     abstract enum def E { enum e1; enum e2; }
    ///     abstract variation part def PV { variant part pv1 : PV; }
    ///     abstract variation part vu { variant part vu1 :> vu; }
    ///     port def PD { attribute pa : A; }   // owns ~PD through a port conjugation
    ///     connection def CD { end ref cdA : P [1]; end ref cdB : P [1]; }
    ///     interface def IF { end port ifA : PD [1]; end port ifB : ~PD [1]; }
    ///     allocation def AD { end ref adA : P [1]; end ref adB : I [1]; }
    ///     flow def FD { end ref fdA : I [1]; end ref fdB : I [1]; }
    ///     part def P {
    ///         attribute x : A; attribute y : A; enum ev : E;
    ///         occurrence o1 : O; occurrence o2 : O; timeslice ts : O; snapshot sn : O; event occurrence ev1 = o1;
    ///         item it1 : I; item it2 : I; part sub1 : Q; part sub2 : Q; port p1 : PD; port p2 : ~PD; ref r;
    ///         connection c : CD connect sub1 to sub2;   interface i : IF connect p1 to p2;
    ///         allocation al : AD allocate sub1 to it1;   binding b of x = y;   succession s first o1 then o2;
    ///         flow f : FD from it1.content to it2.content;   succession flow sf from it1.content to it2.content;
    ///     }
    ///     definition D { usage u; }
    /// }
    /// </code>
    /// Every end has the multiplicity <c>[1]</c>, every directed or end usage is referential, and the rules that need
    /// the model library (the <c>check…Specialization</c> rules) cannot hold without one.
    /// </remarks>
    public sealed class SystemsStructureModel : StepModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SystemsStructureModel"/> class and builds the model on that of
        /// steps 2 to 5
        /// </summary>
        /// <param name="root">
        /// The model of step 2
        /// </param>
        public SystemsStructureModel(RootModel root) : base((root ?? throw new ArgumentNullException(nameof(root))).Builder, "systems structure")
        {
            this.Package = this.CreateNamed<Package>("Systems", "sys", PackageExtensions.ToDto);
            this.OwnMember(root.Root, this.Package);

            var q = this.Member<PartDefinition>("Q", PartDefinitionExtensions.ToDto);
            this.AttributeDefinition = this.Member<AttributeDefinition>("A", AttributeDefinitionExtensions.ToDto);
            this.OccurrenceDefinition = this.Member<OccurrenceDefinition>("O", OccurrenceDefinitionExtensions.ToDto);

            this.ItemDefinition = this.Member<ItemDefinition>("I", ItemDefinitionExtensions.ToDto);
            this.ItemContent = this.Owned<AttributeUsage>(this.ItemDefinition, "content", AttributeUsageExtensions.ToDto, this.AttributeDefinition);

            var enumeration = this.Member<EnumerationDefinition>("E", EnumerationDefinitionExtensions.ToDto);
            enumeration.IsVariation = true;
            enumeration.IsAbstract = true;

            foreach (var name in new[] { "e1", "e2" })
            {
                var value = this.CreateNamed<EnumerationUsage>(name, name, EnumerationUsageExtensions.ToDto);
                enumeration.AssignOwnership(this.Builder.Create<VariantMembership>($"E/{name}/membership", VariantMembershipExtensions.ToDto), value);
                this.Typed(value, enumeration);
            }

            var variation = this.Member<PartDefinition>("PV", PartDefinitionExtensions.ToDto);
            variation.IsVariation = true;
            variation.IsAbstract = true;
            var variant = this.CreateNamed<PartUsage>("pv1", "pv1", PartUsageExtensions.ToDto);
            variation.AssignOwnership(this.Builder.Create<VariantMembership>("PV/pv1/membership", VariantMembershipExtensions.ToDto), variant);
            this.Typed(variant, variation);

            var variationUsage = this.Member<PartUsage>("vu", PartUsageExtensions.ToDto);
            variationUsage.IsVariation = true;
            variationUsage.IsAbstract = true;
            var variantUsage = this.CreateNamed<PartUsage>("vu1", "vu1", PartUsageExtensions.ToDto);
            variationUsage.AssignOwnership(this.Builder.Create<VariantMembership>("vu/vu1/membership", VariantMembershipExtensions.ToDto), variantUsage);
            this.Subsets(variantUsage, variationUsage);

            this.PortDefinition = this.Member<PortDefinition>("PD", PortDefinitionExtensions.ToDto);
            this.Owned<AttributeUsage>(this.PortDefinition, "pa", AttributeUsageExtensions.ToDto, this.AttributeDefinition);

            this.ConjugatedPortDefinition = this.Builder.Create<ConjugatedPortDefinition>("PD/conjugated", ConjugatedPortDefinitionExtensions.ToDto);
            this.OwnMember(this.PortDefinition, this.ConjugatedPortDefinition);

            var conjugation = this.Builder.Create<PortConjugation>("PD/conjugated/conjugation", PortConjugationExtensions.ToDto);
            conjugation.OriginalPortDefinition = this.PortDefinition;
            conjugation.ConjugatedType = this.ConjugatedPortDefinition;
            this.ConjugatedPortDefinition.AssignOwnership(conjugation);

            this.Part = this.Member<PartDefinition>("P", PartDefinitionExtensions.ToDto);

            var connection = this.Member<ConnectionDefinition>("CD", ConnectionDefinitionExtensions.ToDto);
            connection.IsSufficient = true;
            this.End<ReferenceUsage>(connection, "cdA", ReferenceUsageExtensions.ToDto, this.Part);
            this.End<ReferenceUsage>(connection, "cdB", ReferenceUsageExtensions.ToDto, this.Part);

            var interfaceDefinition = this.Member<InterfaceDefinition>("IF", InterfaceDefinitionExtensions.ToDto);
            interfaceDefinition.IsSufficient = true;
            this.End<PortUsage>(interfaceDefinition, "ifA", PortUsageExtensions.ToDto, this.PortDefinition);
            var conjugatedEnd = this.End<PortUsage>(interfaceDefinition, "ifB", PortUsageExtensions.ToDto, null);
            this.ConjugatedTyping(conjugatedEnd);

            var allocation = this.Member<AllocationDefinition>("AD", AllocationDefinitionExtensions.ToDto);
            allocation.IsSufficient = true;
            this.End<ReferenceUsage>(allocation, "adA", ReferenceUsageExtensions.ToDto, this.Part);
            this.End<ReferenceUsage>(allocation, "adB", ReferenceUsageExtensions.ToDto, this.ItemDefinition);

            var flowDefinition = this.Member<FlowDefinition>("FD", FlowDefinitionExtensions.ToDto);
            this.End<ReferenceUsage>(flowDefinition, "fdA", ReferenceUsageExtensions.ToDto, this.ItemDefinition);
            this.End<ReferenceUsage>(flowDefinition, "fdB", ReferenceUsageExtensions.ToDto, this.ItemDefinition);

            var x = this.Owned<AttributeUsage>(this.Part, "x", AttributeUsageExtensions.ToDto, this.AttributeDefinition);
            var y = this.Owned<AttributeUsage>(this.Part, "y", AttributeUsageExtensions.ToDto, this.AttributeDefinition);
            this.Owned<EnumerationUsage>(this.Part, "ev", EnumerationUsageExtensions.ToDto, enumeration);

            var o1 = this.Owned<OccurrenceUsage>(this.Part, "o1", OccurrenceUsageExtensions.ToDto, this.OccurrenceDefinition);
            var o2 = this.Owned<OccurrenceUsage>(this.Part, "o2", OccurrenceUsageExtensions.ToDto, this.OccurrenceDefinition);

            foreach (var (name, kind) in new[] { ("ts", PortionKind.Timeslice), ("sn", PortionKind.Snapshot) })
            {
                var portion = this.Owned<OccurrenceUsage>(this.Part, name, OccurrenceUsageExtensions.ToDto, this.OccurrenceDefinition);
                portion.IsPortion = true;
                portion.PortionKind = kind;
            }

            var eventOccurrence = this.Owned<EventOccurrenceUsage>(this.Part, "ev1", EventOccurrenceUsageExtensions.ToDto, null);
            this.Reference(eventOccurrence, "ev1/reference", o1);

            this.FirstItem = this.Owned<ItemUsage>(this.Part, "it1", ItemUsageExtensions.ToDto, this.ItemDefinition);
            var secondItem = this.Owned<ItemUsage>(this.Part, "it2", ItemUsageExtensions.ToDto, this.ItemDefinition);
            var sub1 = this.Owned<PartUsage>(this.Part, "sub1", PartUsageExtensions.ToDto, q);
            var sub2 = this.Owned<PartUsage>(this.Part, "sub2", PartUsageExtensions.ToDto, q);
            var p1 = this.Owned<PortUsage>(this.Part, "p1", PortUsageExtensions.ToDto, this.PortDefinition);
            var p2 = this.Owned<PortUsage>(this.Part, "p2", PortUsageExtensions.ToDto, null);
            this.ConjugatedTyping(p2);
            this.Owned<ReferenceUsage>(this.Part, "r", ReferenceUsageExtensions.ToDto, null);

            this.Connect<ConnectionUsage, ReferenceUsage>("c", ConnectionUsageExtensions.ToDto, ReferenceUsageExtensions.ToDto, connection, sub1, sub2);
            this.Connect<InterfaceUsage, PortUsage>("i", InterfaceUsageExtensions.ToDto, PortUsageExtensions.ToDto, interfaceDefinition, p1, p2);
            this.Connect<AllocationUsage, ReferenceUsage>("al", AllocationUsageExtensions.ToDto, ReferenceUsageExtensions.ToDto, allocation, sub1, this.FirstItem);
            this.Connect<BindingConnectorAsUsage, ReferenceUsage>("b", BindingConnectorAsUsageExtensions.ToDto, ReferenceUsageExtensions.ToDto, null, x, y);
            this.Connect<SuccessionAsUsage, ReferenceUsage>("s", SuccessionAsUsageExtensions.ToDto, ReferenceUsageExtensions.ToDto, null, o1, o2);

            this.Flow<FlowUsage>("f", FlowUsageExtensions.ToDto, flowDefinition, this.FirstItem, secondItem);
            this.Flow<SuccessionFlowUsage>("sf", SuccessionFlowUsageExtensions.ToDto, null, this.FirstItem, secondItem);

            var definition = this.Member<Definition>("D", DefinitionExtensions.ToDto);
            var usage = this.Owned<Usage>(definition, "u", UsageExtensions.ToDto, null);
            usage.IsComposite = true;
        }

        /// <summary>
        /// Gets the package that holds the step
        /// </summary>
        public Package Package { get; }

        /// <summary>
        /// Gets the part definition <c>P</c>, which owns a usage of every kind
        /// </summary>
        public PartDefinition Part { get; }

        /// <summary>
        /// Gets the attribute definition <c>A</c>
        /// </summary>
        public AttributeDefinition AttributeDefinition { get; }

        /// <summary>
        /// Gets the occurrence definition <c>O</c>
        /// </summary>
        public OccurrenceDefinition OccurrenceDefinition { get; }

        /// <summary>
        /// Gets the item definition <c>I</c>
        /// </summary>
        public ItemDefinition ItemDefinition { get; }

        /// <summary>
        /// Gets the attribute <c>content</c> of <see cref="ItemDefinition"/>, which the flow features redefine
        /// </summary>
        public AttributeUsage ItemContent { get; }

        /// <summary>
        /// Gets the item usage <c>it1</c> of <see cref="Part"/>
        /// </summary>
        public ItemUsage FirstItem { get; }

        /// <summary>
        /// Gets the port definition <c>PD</c>
        /// </summary>
        public PortDefinition PortDefinition { get; }

        /// <summary>
        /// Gets the conjugated port definition <c>~PD</c>, owned by <see cref="PortDefinition"/>
        /// </summary>
        public ConjugatedPortDefinition ConjugatedPortDefinition { get; }

        /// <summary>
        /// Types a port usage by <see cref="ConjugatedPortDefinition"/> through a <see cref="ConjugatedPortTyping"/>
        /// </summary>
        /// <param name="port">
        /// The port usage
        /// </param>
        private void ConjugatedTyping(PortUsage port)
        {
            var typing = this.Builder.Create<ConjugatedPortTyping>($"{port.ElementId}/conjugated typing", ConjugatedPortTypingExtensions.ToDto);
            typing.TypedFeature = port;
            typing.ConjugatedPortDefinition = this.ConjugatedPortDefinition;
            port.AssignOwnership(typing);
        }

        /// <summary>
        /// Creates a connector usage of <see cref="Part"/> between two of its features: two referential end usages with
        /// the multiplicity <c>[1]</c>, each referencing a related feature
        /// </summary>
        /// <typeparam name="TConnector">
        /// The connector usage POCO type
        /// </typeparam>
        /// <typeparam name="TEnd">
        /// The end usage POCO type
        /// </typeparam>
        /// <param name="name">
        /// The name of the connector usage
        /// </param>
        /// <param name="toDto">
        /// The generated <c>ToDto</c> extension of <typeparamref name="TConnector"/>
        /// </param>
        /// <param name="endToDto">
        /// The generated <c>ToDto</c> extension of <typeparamref name="TEnd"/>
        /// </param>
        /// <param name="definition">
        /// The definition that types the connector usage, or <c>null</c>
        /// </param>
        /// <param name="source">
        /// The first related feature
        /// </param>
        /// <param name="target">
        /// The second related feature
        /// </param>
        private void Connect<TConnector, TEnd>(string name, Func<TConnector, bool, DtoElement> toDto, Func<TEnd, bool, DtoElement> endToDto, IType definition, IFeature source, IFeature target)
            where TConnector : IFeature, new()
            where TEnd : IFeature, new()
        {
            var connector = this.Owned(this.Part, name, toDto, definition);

            foreach (var (endName, related) in new[] { ($"{name}/source", source), ($"{name}/target", target) })
            {
                var end = this.Builder.Create(endName, endToDto);
                end.IsEnd = true;
                this.OwnFeature(connector, end);
                this.MultiplicityOne(end, connector);
                this.Reference(end, $"{endName}/reference", related);
            }
        }

        /// <summary>
        /// Creates an end usage of a connection-like definition, typed by one type, with the multiplicity <c>[1]</c>
        /// </summary>
        /// <typeparam name="TEnd">
        /// The end usage POCO type
        /// </typeparam>
        /// <param name="owner">
        /// The definition
        /// </param>
        /// <param name="name">
        /// The name of the end
        /// </param>
        /// <param name="toDto">
        /// The generated <c>ToDto</c> extension of <typeparamref name="TEnd"/>
        /// </param>
        /// <param name="type">
        /// The type of the end, or <c>null</c> when the caller types it
        /// </param>
        /// <returns>
        /// The end usage
        /// </returns>
        private TEnd End<TEnd>(IType owner, string name, Func<TEnd, bool, DtoElement> toDto, IType type) where TEnd : IFeature, new()
        {
            var end = this.CreateNamed(name, name, toDto);
            end.IsEnd = true;
            this.OwnFeature(owner, end);

            if (type != null)
            {
                this.Typed(end, type);
            }

            this.MultiplicityOne(end, owner);

            return end;
        }

        /// <summary>
        /// Creates a flow-like usage of <see cref="Part"/> from the <c>content</c> of one item usage to that of another
        /// </summary>
        /// <typeparam name="TFlow">
        /// The flow usage POCO type
        /// </typeparam>
        /// <param name="name">
        /// The name of the flow usage
        /// </param>
        /// <param name="toDto">
        /// The generated <c>ToDto</c> extension of <typeparamref name="TFlow"/>
        /// </param>
        /// <param name="definition">
        /// The flow definition that types the flow usage, or <c>null</c>
        /// </param>
        /// <param name="source">
        /// The source item usage
        /// </param>
        /// <param name="target">
        /// The target item usage
        /// </param>
        private void Flow<TFlow>(string name, Func<TFlow, bool, DtoElement> toDto, IType definition, IFeature source, IFeature target) where TFlow : IFeature, new()
        {
            var flow = this.Owned(this.Part, name, toDto, definition);

            foreach (var (endName, related) in new[] { ($"{name}/source", source), ($"{name}/target", target) })
            {
                var end = this.Builder.Create<FlowEnd>(endName, FlowEndExtensions.ToDto);
                end.IsEnd = true;
                this.OwnFeature(flow, end);
                this.MultiplicityOne(end, flow);
                this.Reference(end, $"{endName}/reference", related);

                var flowFeature = this.Builder.Create<ReferenceUsage>($"{endName}/feature", ReferenceUsageExtensions.ToDto);
                this.OwnFeature(end, flowFeature);

                var redefinition = this.Builder.Create<Redefinition>($"{endName}/feature/redefinition", RedefinitionExtensions.ToDto);
                redefinition.RedefiningFeature = flowFeature;
                redefinition.RedefinedFeature = this.ItemContent;
                flowFeature.AssignOwnership(redefinition);
            }
        }

        /// <summary>
        /// Creates a named member of <see cref="Package"/>
        /// </summary>
        /// <typeparam name="TPoco">
        /// The POCO type
        /// </typeparam>
        /// <param name="name">
        /// The declared name
        /// </param>
        /// <param name="toDto">
        /// The generated <c>ToDto</c> extension of <typeparamref name="TPoco"/>
        /// </param>
        /// <returns>
        /// The member
        /// </returns>
        private TPoco Member<TPoco>(string name, Func<TPoco, bool, DtoElement> toDto) where TPoco : SysML2.NET.Core.POCO.Root.Elements.IElement, new()
        {
            var member = this.CreateNamed(name, name.ToLowerInvariant(), toDto);
            this.OwnMember(this.Package, member);

            return member;
        }

        /// <summary>
        /// Creates a named usage owned by a type, typed by another type when one is given
        /// </summary>
        /// <typeparam name="TUsage">
        /// The usage POCO type
        /// </typeparam>
        /// <param name="owner">
        /// The owning type
        /// </param>
        /// <param name="name">
        /// The declared name
        /// </param>
        /// <param name="toDto">
        /// The generated <c>ToDto</c> extension of <typeparamref name="TUsage"/>
        /// </param>
        /// <param name="type">
        /// The type of the usage, or <c>null</c> to leave it untyped
        /// </param>
        /// <returns>
        /// The usage
        /// </returns>
        private TUsage Owned<TUsage>(IType owner, string name, Func<TUsage, bool, DtoElement> toDto, IType type) where TUsage : IFeature, new()
        {
            var usage = this.CreateNamed(name, name, toDto);
            this.OwnFeature(owner, usage);

            if (type != null)
            {
                this.Typed(usage, type);
            }

            return usage;
        }

        /// <summary>
        /// Makes a feature reference another through a <see cref="ReferenceSubsetting"/>
        /// </summary>
        /// <param name="feature">
        /// The referencing feature
        /// </param>
        /// <param name="name">
        /// The name the reference subsetting is derived from
        /// </param>
        /// <param name="referenced">
        /// The referenced feature
        /// </param>
        private void Reference(IFeature feature, string name, IFeature referenced)
        {
            var reference = this.Builder.Create<ReferenceSubsetting>(name, ReferenceSubsettingExtensions.ToDto);
            reference.ReferencedFeature = referenced;
            feature.AssignOwnership(reference);
        }
    }
}
