// -------------------------------------------------------------------------------------------------
// <copyright file="DefinitionExtensionsTestFixture.cs" company="Starion Group S.A.">
//
//   Copyright 2022-2026 Starion Group S.A.
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
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

namespace SysML2.NET.Tests.Extend
{
    using System;

    using NUnit.Framework;

    using SysML2.NET.Core.Core.Types;
    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Core.POCO.Systems.Actions;
    using SysML2.NET.Core.POCO.Systems.Allocations;
    using SysML2.NET.Core.POCO.Systems.AnalysisCases;
    using SysML2.NET.Core.POCO.Systems.Attributes;
    using SysML2.NET.Core.POCO.Systems.Calculations;
    using SysML2.NET.Core.POCO.Systems.Cases;
    using SysML2.NET.Core.POCO.Systems.Connections;
    using SysML2.NET.Core.POCO.Systems.Constraints;
    using SysML2.NET.Core.POCO.Systems.DefinitionAndUsage;
    using SysML2.NET.Core.POCO.Systems.Enumerations;
    using SysML2.NET.Core.POCO.Systems.Flows;
    using SysML2.NET.Core.POCO.Systems.Interfaces;
    using SysML2.NET.Core.POCO.Systems.Items;
    using SysML2.NET.Core.POCO.Systems.Metadata;
    using SysML2.NET.Core.POCO.Systems.Occurrences;
    using SysML2.NET.Core.POCO.Systems.Parts;
    using SysML2.NET.Core.POCO.Systems.Ports;
    using SysML2.NET.Core.POCO.Systems.Requirements;
    using SysML2.NET.Core.POCO.Systems.States;
    using SysML2.NET.Core.POCO.Systems.UseCases;
    using SysML2.NET.Core.POCO.Systems.VerificationCases;
    using SysML2.NET.Core.POCO.Systems.Views;
    using SysML2.NET.Extensions;

    [TestFixture]
    public class DefinitionExtensionsTestFixture
    {
        /// <summary>
        /// Builds a Definition owning one of every Usage subkind referenced by the
        /// owned-* derived selectors, and returns the (subject, kindMap) for use
        /// across the 23 ComputeOwnedXxx tests. Each subkind is wired via a
        /// FeatureMembership in OwnedRelationship.
        /// </summary>
        private static Definition BuildDefinitionWithMixedOwnedKinds(out OwnedKindBag bag)
        {
            var subject = new Definition();

            bag = new OwnedKindBag
            {
                Action = new ActionUsage(),
                Allocation = new AllocationUsage(),
                AnalysisCase = new AnalysisCaseUsage(),
                Attribute = new AttributeUsage(),
                Calculation = new CalculationUsage(),
                Case = new CaseUsage(),
                Concern = new ConcernUsage(),
                Connection = new BindingConnectorAsUsage(),
                Constraint = new ConstraintUsage(),
                Enumeration = new EnumerationUsage(),
                Flow = new FlowUsage(),
                Interface = new InterfaceUsage(),
                Item = new ItemUsage(),
                Metadata = new MetadataUsage(),
                Occurrence = new OccurrenceUsage(),
                Part = new PartUsage(),
                Port = new PortUsage(),
                Reference = new ReferenceUsage(),
                Rendering = new RenderingUsage(),
                Requirement = new RequirementUsage(),
                State = new StateUsage(),
                Transition = new TransitionUsage(),
                UseCase = new UseCaseUsage(),
                VerificationCase = new VerificationCaseUsage(),
                View = new ViewUsage(),
                Viewpoint = new ViewpointUsage(),
                BareUsage = new Usage(),
            };

            subject.AssignOwnership(new FeatureMembership(), bag.Action);
            subject.AssignOwnership(new FeatureMembership(), bag.Allocation);
            subject.AssignOwnership(new FeatureMembership(), bag.AnalysisCase);
            subject.AssignOwnership(new FeatureMembership(), bag.Attribute);
            subject.AssignOwnership(new FeatureMembership(), bag.Calculation);
            subject.AssignOwnership(new FeatureMembership(), bag.Case);
            subject.AssignOwnership(new FeatureMembership(), bag.Concern);
            subject.AssignOwnership(new FeatureMembership(), bag.Connection);
            subject.AssignOwnership(new FeatureMembership(), bag.Constraint);
            subject.AssignOwnership(new FeatureMembership(), bag.Enumeration);
            subject.AssignOwnership(new FeatureMembership(), bag.Flow);
            subject.AssignOwnership(new FeatureMembership(), bag.Interface);
            subject.AssignOwnership(new FeatureMembership(), bag.Item);
            subject.AssignOwnership(new FeatureMembership(), bag.Metadata);
            subject.AssignOwnership(new FeatureMembership(), bag.Occurrence);
            subject.AssignOwnership(new FeatureMembership(), bag.Part);
            subject.AssignOwnership(new FeatureMembership(), bag.Port);
            subject.AssignOwnership(new FeatureMembership(), bag.Reference);
            subject.AssignOwnership(new FeatureMembership(), bag.Rendering);
            subject.AssignOwnership(new FeatureMembership(), bag.Requirement);
            subject.AssignOwnership(new FeatureMembership(), bag.State);
            subject.AssignOwnership(new FeatureMembership(), bag.Transition);
            subject.AssignOwnership(new FeatureMembership(), bag.UseCase);
            subject.AssignOwnership(new FeatureMembership(), bag.VerificationCase);
            subject.AssignOwnership(new FeatureMembership(), bag.View);
            subject.AssignOwnership(new FeatureMembership(), bag.Viewpoint);
            subject.AssignOwnership(new FeatureMembership(), bag.BareUsage);

            return subject;
        }

        private sealed class OwnedKindBag
        {
            public ActionUsage Action;
            public AllocationUsage Allocation;
            public AnalysisCaseUsage AnalysisCase;
            public AttributeUsage Attribute;
            public CalculationUsage Calculation;
            public CaseUsage Case;
            public ConcernUsage Concern;
            public BindingConnectorAsUsage Connection;
            public ConstraintUsage Constraint;
            public EnumerationUsage Enumeration;
            public FlowUsage Flow;
            public InterfaceUsage Interface;
            public ItemUsage Item;
            public MetadataUsage Metadata;
            public OccurrenceUsage Occurrence;
            public PartUsage Part;
            public PortUsage Port;
            public ReferenceUsage Reference;
            public RenderingUsage Rendering;
            public RequirementUsage Requirement;
            public StateUsage State;
            public TransitionUsage Transition;
            public UseCaseUsage UseCase;
            public VerificationCaseUsage VerificationCase;
            public ViewUsage View;
            public ViewpointUsage Viewpoint;
            public Usage BareUsage;
        }

        [Test]
        public void VerifyComputeDirectedUsage()
        {
            Assert.That(() => ((IDefinition)null).ComputeDirectedUsage(), Throws.TypeOf<ArgumentNullException>());

            var emptySubject = new Definition();

            Assert.That(emptySubject.ComputeDirectedUsage(), Has.Count.EqualTo(0));

            var subject = new Definition();
            var directedUsage = new PartUsage { Direction = FeatureDirectionKind.In };
            var directedFeature = new Feature { Direction = FeatureDirectionKind.Out };
            var undirectedUsage = new PartUsage();

            subject.AssignOwnership(new FeatureMembership(), directedUsage);
            subject.AssignOwnership(new FeatureMembership(), directedFeature);
            subject.AssignOwnership(new FeatureMembership(), undirectedUsage);

            // Only the directed Usage is returned (directed non-Usage Feature and undirected Usage are excluded).
            Assert.That(subject.ComputeDirectedUsage(), Is.EquivalentTo(new[] { directedUsage }));
        }

        [Test]
        public void VerifyComputeOwnedAction()
        {
            Assert.That(() => ((IDefinition)null).ComputeOwnedAction(), Throws.TypeOf<ArgumentNullException>());

            var emptySubject = new Definition();

            Assert.That(emptySubject.ComputeOwnedAction(), Has.Count.EqualTo(0));

            var subject = BuildDefinitionWithMixedOwnedKinds(out var bag);

            Assert.That(subject.ComputeOwnedAction(), Does.Contain(bag.Action));
            Assert.That(subject.ComputeOwnedAction(), Does.Not.Contain(bag.BareUsage));
            Assert.That(subject.ComputeOwnedAction(), Does.Not.Contain(bag.Attribute));
        }

        [Test]
        public void VerifyComputeOwnedAllocation()
        {
            Assert.That(() => ((IDefinition)null).ComputeOwnedAllocation(), Throws.TypeOf<ArgumentNullException>());

            var emptySubject = new Definition();

            Assert.That(emptySubject.ComputeOwnedAllocation(), Has.Count.EqualTo(0));

            var subject = BuildDefinitionWithMixedOwnedKinds(out var bag);

            Assert.That(subject.ComputeOwnedAllocation(), Does.Contain(bag.Allocation));
            Assert.That(subject.ComputeOwnedAllocation(), Does.Not.Contain(bag.BareUsage));
        }

        [Test]
        public void VerifyComputeOwnedAnalysisCase()
        {
            Assert.That(() => ((IDefinition)null).ComputeOwnedAnalysisCase(), Throws.TypeOf<ArgumentNullException>());

            var emptySubject = new Definition();

            Assert.That(emptySubject.ComputeOwnedAnalysisCase(), Has.Count.EqualTo(0));

            var subject = BuildDefinitionWithMixedOwnedKinds(out var bag);

            Assert.That(subject.ComputeOwnedAnalysisCase(), Does.Contain(bag.AnalysisCase));
            Assert.That(subject.ComputeOwnedAnalysisCase(), Does.Not.Contain(bag.BareUsage));
        }

        [Test]
        public void VerifyComputeOwnedAttribute()
        {
            Assert.That(() => ((IDefinition)null).ComputeOwnedAttribute(), Throws.TypeOf<ArgumentNullException>());

            var emptySubject = new Definition();

            Assert.That(emptySubject.ComputeOwnedAttribute(), Has.Count.EqualTo(0));

            var subject = BuildDefinitionWithMixedOwnedKinds(out var bag);

            Assert.That(subject.ComputeOwnedAttribute(), Does.Contain(bag.Attribute));
            Assert.That(subject.ComputeOwnedAttribute(), Does.Not.Contain(bag.BareUsage));
        }

        [Test]
        public void VerifyComputeOwnedCalculation()
        {
            Assert.That(() => ((IDefinition)null).ComputeOwnedCalculation(), Throws.TypeOf<ArgumentNullException>());

            var emptySubject = new Definition();

            Assert.That(emptySubject.ComputeOwnedCalculation(), Has.Count.EqualTo(0));

            var subject = BuildDefinitionWithMixedOwnedKinds(out var bag);

            Assert.That(subject.ComputeOwnedCalculation(), Does.Contain(bag.Calculation));
            Assert.That(subject.ComputeOwnedCalculation(), Does.Not.Contain(bag.BareUsage));
        }

        [Test]
        public void VerifyComputeOwnedCase()
        {
            Assert.That(() => ((IDefinition)null).ComputeOwnedCase(), Throws.TypeOf<ArgumentNullException>());

            var emptySubject = new Definition();

            Assert.That(emptySubject.ComputeOwnedCase(), Has.Count.EqualTo(0));

            var subject = BuildDefinitionWithMixedOwnedKinds(out var bag);

            Assert.That(subject.ComputeOwnedCase(), Does.Contain(bag.Case));
            Assert.That(subject.ComputeOwnedCase(), Does.Not.Contain(bag.BareUsage));
        }

        [Test]
        public void VerifyComputeOwnedConcern()
        {
            Assert.That(() => ((IDefinition)null).ComputeOwnedConcern(), Throws.TypeOf<ArgumentNullException>());

            var emptySubject = new Definition();

            Assert.That(emptySubject.ComputeOwnedConcern(), Has.Count.EqualTo(0));

            var subject = BuildDefinitionWithMixedOwnedKinds(out var bag);

            Assert.That(subject.ComputeOwnedConcern(), Does.Contain(bag.Concern));
            Assert.That(subject.ComputeOwnedConcern(), Does.Not.Contain(bag.BareUsage));
        }

        [Test]
        public void VerifyComputeOwnedConnection()
        {
            Assert.That(() => ((IDefinition)null).ComputeOwnedConnection(), Throws.TypeOf<ArgumentNullException>());

            var emptySubject = new Definition();

            Assert.That(emptySubject.ComputeOwnedConnection(), Has.Count.EqualTo(0));

            var subject = BuildDefinitionWithMixedOwnedKinds(out var bag);

            // BindingConnectorAsUsage IS an IConnectorAsUsage. So is FlowUsage.
            Assert.That(subject.ComputeOwnedConnection(), Does.Contain(bag.Connection));
            Assert.That(subject.ComputeOwnedConnection(), Does.Contain(bag.Flow));
            Assert.That(subject.ComputeOwnedConnection(), Does.Not.Contain(bag.BareUsage));
        }

        [Test]
        public void VerifyComputeOwnedConstraint()
        {
            Assert.That(() => ((IDefinition)null).ComputeOwnedConstraint(), Throws.TypeOf<ArgumentNullException>());

            var emptySubject = new Definition();

            Assert.That(emptySubject.ComputeOwnedConstraint(), Has.Count.EqualTo(0));

            var subject = BuildDefinitionWithMixedOwnedKinds(out var bag);

            // Requirement IS a Constraint, so both should appear.
            Assert.That(subject.ComputeOwnedConstraint(), Does.Contain(bag.Constraint));
            Assert.That(subject.ComputeOwnedConstraint(), Does.Not.Contain(bag.BareUsage));
        }

        [Test]
        public void VerifyComputeOwnedEnumeration()
        {
            Assert.That(() => ((IDefinition)null).ComputeOwnedEnumeration(), Throws.TypeOf<ArgumentNullException>());

            var emptySubject = new Definition();

            Assert.That(emptySubject.ComputeOwnedEnumeration(), Has.Count.EqualTo(0));

            var subject = BuildDefinitionWithMixedOwnedKinds(out var bag);

            Assert.That(subject.ComputeOwnedEnumeration(), Does.Contain(bag.Enumeration));
            Assert.That(subject.ComputeOwnedEnumeration(), Does.Not.Contain(bag.BareUsage));
        }

        [Test]
        public void VerifyComputeOwnedFlow()
        {
            Assert.That(() => ((IDefinition)null).ComputeOwnedFlow(), Throws.TypeOf<ArgumentNullException>());

            var emptySubject = new Definition();

            Assert.That(emptySubject.ComputeOwnedFlow(), Has.Count.EqualTo(0));

            var subject = BuildDefinitionWithMixedOwnedKinds(out var bag);

            Assert.That(subject.ComputeOwnedFlow(), Does.Contain(bag.Flow));
            Assert.That(subject.ComputeOwnedFlow(), Does.Not.Contain(bag.BareUsage));
        }

        [Test]
        public void VerifyComputeOwnedInterface()
        {
            Assert.That(() => ((IDefinition)null).ComputeOwnedInterface(), Throws.TypeOf<ArgumentNullException>());

            var emptySubject = new Definition();

            Assert.That(emptySubject.ComputeOwnedInterface(), Has.Count.EqualTo(0));

            var subject = BuildDefinitionWithMixedOwnedKinds(out var bag);

            // Asserts the C#-contract correction (XMI OCL erroneously says ReferenceUsage):
            // ComputeOwnedInterface returns IInterfaceUsage, NOT ReferenceUsage.
            Assert.That(subject.ComputeOwnedInterface(), Does.Contain(bag.Interface));
            Assert.That(subject.ComputeOwnedInterface(), Does.Not.Contain(bag.Reference));
            Assert.That(subject.ComputeOwnedInterface(), Does.Not.Contain(bag.BareUsage));
        }

        [Test]
        public void VerifyComputeOwnedItem()
        {
            Assert.That(() => ((IDefinition)null).ComputeOwnedItem(), Throws.TypeOf<ArgumentNullException>());

            var emptySubject = new Definition();

            Assert.That(emptySubject.ComputeOwnedItem(), Has.Count.EqualTo(0));

            var subject = BuildDefinitionWithMixedOwnedKinds(out var bag);

            // PartUsage IS an ItemUsage, so it surfaces here too.
            Assert.That(subject.ComputeOwnedItem(), Does.Contain(bag.Item));
            Assert.That(subject.ComputeOwnedItem(), Does.Not.Contain(bag.BareUsage));
        }

        [Test]
        public void VerifyComputeOwnedMetadata()
        {
            Assert.That(() => ((IDefinition)null).ComputeOwnedMetadata(), Throws.TypeOf<ArgumentNullException>());

            var emptySubject = new Definition();

            Assert.That(emptySubject.ComputeOwnedMetadata(), Has.Count.EqualTo(0));

            var subject = BuildDefinitionWithMixedOwnedKinds(out var bag);

            Assert.That(subject.ComputeOwnedMetadata(), Does.Contain(bag.Metadata));
            Assert.That(subject.ComputeOwnedMetadata(), Does.Not.Contain(bag.BareUsage));
        }

        [Test]
        public void VerifyComputeOwnedOccurrence()
        {
            Assert.That(() => ((IDefinition)null).ComputeOwnedOccurrence(), Throws.TypeOf<ArgumentNullException>());

            var emptySubject = new Definition();

            Assert.That(emptySubject.ComputeOwnedOccurrence(), Has.Count.EqualTo(0));

            var subject = BuildDefinitionWithMixedOwnedKinds(out var bag);

            // Many subkinds (ActionUsage, ItemUsage, PartUsage, PortUsage, ...) ARE OccurrenceUsage.
            Assert.That(subject.ComputeOwnedOccurrence(), Does.Contain(bag.Occurrence));
            Assert.That(subject.ComputeOwnedOccurrence(), Does.Contain(bag.Action));
            Assert.That(subject.ComputeOwnedOccurrence(), Does.Not.Contain(bag.BareUsage));
            Assert.That(subject.ComputeOwnedOccurrence(), Does.Not.Contain(bag.Attribute));
        }

        [Test]
        public void VerifyComputeOwnedPart()
        {
            Assert.That(() => ((IDefinition)null).ComputeOwnedPart(), Throws.TypeOf<ArgumentNullException>());

            var emptySubject = new Definition();

            Assert.That(emptySubject.ComputeOwnedPart(), Has.Count.EqualTo(0));

            var subject = BuildDefinitionWithMixedOwnedKinds(out var bag);

            Assert.That(subject.ComputeOwnedPart(), Does.Contain(bag.Part));
            Assert.That(subject.ComputeOwnedPart(), Does.Not.Contain(bag.BareUsage));
        }

        [Test]
        public void VerifyComputeOwnedPort()
        {
            Assert.That(() => ((IDefinition)null).ComputeOwnedPort(), Throws.TypeOf<ArgumentNullException>());

            var emptySubject = new Definition();

            Assert.That(emptySubject.ComputeOwnedPort(), Has.Count.EqualTo(0));

            var subject = BuildDefinitionWithMixedOwnedKinds(out var bag);

            Assert.That(subject.ComputeOwnedPort(), Does.Contain(bag.Port));
            Assert.That(subject.ComputeOwnedPort(), Does.Not.Contain(bag.BareUsage));
        }

        [Test]
        public void VerifyComputeOwnedReference()
        {
            Assert.That(() => ((IDefinition)null).ComputeOwnedReference(), Throws.TypeOf<ArgumentNullException>());

            var emptySubject = new Definition();

            Assert.That(emptySubject.ComputeOwnedReference(), Has.Count.EqualTo(0));

            var subject = BuildDefinitionWithMixedOwnedKinds(out var bag);

            Assert.That(subject.ComputeOwnedReference(), Does.Contain(bag.Reference));
            Assert.That(subject.ComputeOwnedReference(), Does.Not.Contain(bag.BareUsage));
            Assert.That(subject.ComputeOwnedReference(), Does.Not.Contain(bag.Interface));
        }

        [Test]
        public void VerifyComputeOwnedRendering()
        {
            Assert.That(() => ((IDefinition)null).ComputeOwnedRendering(), Throws.TypeOf<ArgumentNullException>());

            var emptySubject = new Definition();

            Assert.That(emptySubject.ComputeOwnedRendering(), Has.Count.EqualTo(0));

            var subject = BuildDefinitionWithMixedOwnedKinds(out var bag);

            Assert.That(subject.ComputeOwnedRendering(), Does.Contain(bag.Rendering));
            Assert.That(subject.ComputeOwnedRendering(), Does.Not.Contain(bag.BareUsage));
        }

        [Test]
        public void VerifyComputeOwnedRequirement()
        {
            Assert.That(() => ((IDefinition)null).ComputeOwnedRequirement(), Throws.TypeOf<ArgumentNullException>());

            var emptySubject = new Definition();

            Assert.That(emptySubject.ComputeOwnedRequirement(), Has.Count.EqualTo(0));

            var subject = BuildDefinitionWithMixedOwnedKinds(out var bag);

            Assert.That(subject.ComputeOwnedRequirement(), Does.Contain(bag.Requirement));
            Assert.That(subject.ComputeOwnedRequirement(), Does.Not.Contain(bag.BareUsage));
        }

        [Test]
        public void VerifyComputeOwnedState()
        {
            Assert.That(() => ((IDefinition)null).ComputeOwnedState(), Throws.TypeOf<ArgumentNullException>());

            var emptySubject = new Definition();

            Assert.That(emptySubject.ComputeOwnedState(), Has.Count.EqualTo(0));

            var subject = BuildDefinitionWithMixedOwnedKinds(out var bag);

            Assert.That(subject.ComputeOwnedState(), Does.Contain(bag.State));
            Assert.That(subject.ComputeOwnedState(), Does.Not.Contain(bag.BareUsage));
        }

        [Test]
        public void VerifyComputeOwnedTransition()
        {
            Assert.That(() => ((IDefinition)null).ComputeOwnedTransition(), Throws.TypeOf<ArgumentNullException>());

            var emptySubject = new Definition();

            Assert.That(emptySubject.ComputeOwnedTransition(), Has.Count.EqualTo(0));

            var subject = BuildDefinitionWithMixedOwnedKinds(out var bag);

            Assert.That(subject.ComputeOwnedTransition(), Does.Contain(bag.Transition));
            Assert.That(subject.ComputeOwnedTransition(), Does.Not.Contain(bag.BareUsage));
        }

        [Test]
        public void VerifyComputeOwnedUsage()
        {
            Assert.That(() => ((IDefinition)null).ComputeOwnedUsage(), Throws.TypeOf<ArgumentNullException>());

            var emptySubject = new Definition();

            Assert.That(emptySubject.ComputeOwnedUsage(), Has.Count.EqualTo(0));

            var subject = new Definition();
            var ownedUsage = new PartUsage();
            var ownedNonUsage = new Feature();

            subject.AssignOwnership(new FeatureMembership(), ownedUsage);
            subject.AssignOwnership(new FeatureMembership(), ownedNonUsage);

            // Only the IUsage member is returned; bare IFeature is excluded.
            Assert.That(subject.ComputeOwnedUsage(), Does.Contain(ownedUsage));
            Assert.That(subject.ComputeOwnedUsage(), Does.Not.Contain(ownedNonUsage));
        }

        [Test]
        public void VerifyComputeOwnedUseCase()
        {
            Assert.That(() => ((IDefinition)null).ComputeOwnedUseCase(), Throws.TypeOf<ArgumentNullException>());

            var emptySubject = new Definition();

            Assert.That(emptySubject.ComputeOwnedUseCase(), Has.Count.EqualTo(0));

            var subject = BuildDefinitionWithMixedOwnedKinds(out var bag);

            Assert.That(subject.ComputeOwnedUseCase(), Does.Contain(bag.UseCase));
            Assert.That(subject.ComputeOwnedUseCase(), Does.Not.Contain(bag.BareUsage));
        }

        [Test]
        public void VerifyComputeOwnedVerificationCase()
        {
            Assert.That(() => ((IDefinition)null).ComputeOwnedVerificationCase(), Throws.TypeOf<ArgumentNullException>());

            var emptySubject = new Definition();

            Assert.That(emptySubject.ComputeOwnedVerificationCase(), Has.Count.EqualTo(0));

            var subject = BuildDefinitionWithMixedOwnedKinds(out var bag);

            Assert.That(subject.ComputeOwnedVerificationCase(), Does.Contain(bag.VerificationCase));
            Assert.That(subject.ComputeOwnedVerificationCase(), Does.Not.Contain(bag.BareUsage));
        }

        [Test]
        public void VerifyComputeOwnedView()
        {
            Assert.That(() => ((IDefinition)null).ComputeOwnedView(), Throws.TypeOf<ArgumentNullException>());

            var emptySubject = new Definition();

            Assert.That(emptySubject.ComputeOwnedView(), Has.Count.EqualTo(0));

            var subject = BuildDefinitionWithMixedOwnedKinds(out var bag);

            Assert.That(subject.ComputeOwnedView(), Does.Contain(bag.View));
            Assert.That(subject.ComputeOwnedView(), Does.Not.Contain(bag.BareUsage));
        }

        [Test]
        public void VerifyComputeOwnedViewpoint()
        {
            Assert.That(() => ((IDefinition)null).ComputeOwnedViewpoint(), Throws.TypeOf<ArgumentNullException>());

            var emptySubject = new Definition();

            Assert.That(emptySubject.ComputeOwnedViewpoint(), Has.Count.EqualTo(0));

            var subject = BuildDefinitionWithMixedOwnedKinds(out var bag);

            Assert.That(subject.ComputeOwnedViewpoint(), Does.Contain(bag.Viewpoint));
            Assert.That(subject.ComputeOwnedViewpoint(), Does.Not.Contain(bag.BareUsage));
        }

        [Test]
        public void VerifyComputeUsage()
        {
            Assert.That(() => ((IDefinition)null).ComputeUsage(), Throws.TypeOf<ArgumentNullException>());

            var emptySubject = new Definition();

            Assert.That(emptySubject.ComputeUsage(), Has.Count.EqualTo(0));

            var subject = new Definition();
            var ownedUsage = new PartUsage();
            var ownedFeature = new Feature();

            subject.AssignOwnership(new FeatureMembership(), ownedUsage);
            subject.AssignOwnership(new FeatureMembership(), ownedFeature);

            // feature.OfType<IUsage>() — only the IUsage one surfaces.
            Assert.That(subject.ComputeUsage(), Does.Contain(ownedUsage));
            Assert.That(subject.ComputeUsage(), Does.Not.Contain(ownedFeature));
        }

        [Test]
        public void VerifyComputeVariant()
        {
            Assert.That(() => ((IDefinition)null).ComputeVariant(), Throws.TypeOf<ArgumentNullException>());

            var emptySubject = new Definition();

            Assert.That(emptySubject.ComputeVariant(), Has.Count.EqualTo(0));

            // Populated case is stub-blocked: VariantMembershipExtensions.ComputeOwnedVariantUsage
            // is still a NotSupportedException stub. Asserting NotSupportedException here makes the
            // block visible; once the upstream stub is implemented, this assertion will fail loudly,
            // forcing a real assertion to be written.
            // For Later: populated case depends on VariantMembershipExtensions.ComputeOwnedVariantUsage, which is still a stub.
            var subject = new Definition();
            var variantUsage = new PartUsage();
            subject.AssignOwnership(new VariantMembership(), variantUsage);

            Assert.That(() => subject.ComputeVariant(), Throws.TypeOf<NotSupportedException>());
        }

        [Test]
        public void VerifyComputeVariantMembership()
        {
            Assert.That(() => ((IDefinition)null).ComputeVariantMembership(), Throws.TypeOf<ArgumentNullException>());

            var emptySubject = new Definition();

            Assert.That(emptySubject.ComputeVariantMembership(), Has.Count.EqualTo(0));

            var subject = new Definition();
            var variantUsage = new PartUsage();
            var variantMembership = new VariantMembership();
            subject.AssignOwnership(variantMembership, variantUsage);

            // A non-VariantMembership FeatureMembership in the same OwnedRelationship MUST NOT surface.
            var plainNested = new PartUsage();
            subject.AssignOwnership(new FeatureMembership(), plainNested);

            Assert.That(subject.ComputeVariantMembership(), Is.EquivalentTo(new[] { variantMembership }));
        }
    }
}
