// -------------------------------------------------------------------------------------------------
// <copyright file="UsageExtensionsTestFixture.cs" company="Starion Group S.A.">
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
    public class UsageExtensionsTestFixture
    {
        /// <summary>
        /// Builds a Usage owning one of every Usage subkind referenced by the
        /// nested-* derived selectors, and returns the (subject, kindMap)
        /// for use across the 23 ComputeNestedXxx tests. Each subkind is
        /// wired via a FeatureMembership in OwnedRelationship.
        /// </summary>
        private static Usage BuildUsageWithMixedNestedKinds(out NestedKindBag bag)
        {
            var subject = new Usage();

            bag = new NestedKindBag
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

        private sealed class NestedKindBag
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
        public void VerifyComputeDefinition()
        {
            Assert.That(() => ((IUsage)null).ComputeDefinition(), Throws.TypeOf<ArgumentNullException>());

            var subject = new Usage();

            Assert.That(subject.ComputeDefinition(), Has.Count.EqualTo(0));

            var definition = new Definition();
            var bareType = new SysML2.NET.Core.POCO.Core.Types.Type();
            subject.AssignOwnership(new FeatureTyping { Type = definition });
            subject.AssignOwnership(new FeatureTyping { Type = bareType });

            // Definition is an IClassifier; bare Type is not — only the Definition is returned.
            Assert.That(subject.ComputeDefinition(), Is.EquivalentTo(new[] { definition }));
        }

        [Test]
        public void VerifyComputeDirectedUsage()
        {
            Assert.That(() => ((IUsage)null).ComputeDirectedUsage(), Throws.TypeOf<ArgumentNullException>());

            var subject = new Usage();

            Assert.That(subject.ComputeDirectedUsage(), Has.Count.EqualTo(0));

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
        public void VerifyComputeIsReference()
        {
            Assert.That(() => ((IUsage)null).ComputeIsReference(), Throws.TypeOf<ArgumentNullException>());

            var subject = new Usage();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(subject.ComputeIsReference(), Is.True);

                subject.IsComposite = true;

                Assert.That(subject.ComputeIsReference(), Is.False);
            }
        }

        [Test]
        public void VerifyComputeMayTimeVary()
        {
            Assert.That(() => ((IUsage)null).ComputeMayTimeVary(), Throws.TypeOf<ArgumentNullException>());

            // Negative path: no owningType → false
            var orphan = new Usage();

            Assert.That(orphan.ComputeMayTimeVary(), Is.False);

            // Negative path: owningType present, but does NOT specialize Occurrences::Occurrence → false
            var owner = new Definition();
            var subject = new Usage();
            owner.AssignOwnership(new FeatureMembership(), subject);

            Assert.That(subject.ComputeMayTimeVary(), Is.False);

            // The "true" branch and the IsPortion / SelfLink / HappensLink / Action negative
            // branches all require a SysML library bootstrap (Occurrences::Occurrence,
            // Links::SelfLink, Occurrences::HappensLink, Actions::Action) which is out of scope
            // for this fixture. Negative path on owningType-not-Occurrence is the safe assertion
            // that exercises the OCL chain without needing a wired library.
        }

        [Test]
        public void VerifyComputeNestedAction()
        {
            Assert.That(() => ((IUsage)null).ComputeNestedAction(), Throws.TypeOf<ArgumentNullException>());

            var emptySubject = new Usage();

            Assert.That(emptySubject.ComputeNestedAction(), Has.Count.EqualTo(0));

            var subject = BuildUsageWithMixedNestedKinds(out var bag);

            Assert.That(subject.ComputeNestedAction(), Does.Contain(bag.Action));
            Assert.That(subject.ComputeNestedAction(), Does.Not.Contain(bag.BareUsage));
            Assert.That(subject.ComputeNestedAction(), Does.Not.Contain(bag.Attribute));
        }

        [Test]
        public void VerifyComputeNestedAllocation()
        {
            Assert.That(() => ((IUsage)null).ComputeNestedAllocation(), Throws.TypeOf<ArgumentNullException>());

            var emptySubject = new Usage();

            Assert.That(emptySubject.ComputeNestedAllocation(), Has.Count.EqualTo(0));

            var subject = BuildUsageWithMixedNestedKinds(out var bag);

            Assert.That(subject.ComputeNestedAllocation(), Does.Contain(bag.Allocation));
            Assert.That(subject.ComputeNestedAllocation(), Does.Not.Contain(bag.BareUsage));
        }

        [Test]
        public void VerifyComputeNestedAnalysisCase()
        {
            Assert.That(() => ((IUsage)null).ComputeNestedAnalysisCase(), Throws.TypeOf<ArgumentNullException>());

            var emptySubject = new Usage();

            Assert.That(emptySubject.ComputeNestedAnalysisCase(), Has.Count.EqualTo(0));

            var subject = BuildUsageWithMixedNestedKinds(out var bag);

            Assert.That(subject.ComputeNestedAnalysisCase(), Does.Contain(bag.AnalysisCase));
            Assert.That(subject.ComputeNestedAnalysisCase(), Does.Not.Contain(bag.BareUsage));
        }

        [Test]
        public void VerifyComputeNestedAttribute()
        {
            Assert.That(() => ((IUsage)null).ComputeNestedAttribute(), Throws.TypeOf<ArgumentNullException>());

            var emptySubject = new Usage();

            Assert.That(emptySubject.ComputeNestedAttribute(), Has.Count.EqualTo(0));

            var subject = BuildUsageWithMixedNestedKinds(out var bag);

            Assert.That(subject.ComputeNestedAttribute(), Does.Contain(bag.Attribute));
            Assert.That(subject.ComputeNestedAttribute(), Does.Not.Contain(bag.BareUsage));
        }

        [Test]
        public void VerifyComputeNestedCalculation()
        {
            Assert.That(() => ((IUsage)null).ComputeNestedCalculation(), Throws.TypeOf<ArgumentNullException>());

            var emptySubject = new Usage();

            Assert.That(emptySubject.ComputeNestedCalculation(), Has.Count.EqualTo(0));

            var subject = BuildUsageWithMixedNestedKinds(out var bag);

            Assert.That(subject.ComputeNestedCalculation(), Does.Contain(bag.Calculation));
            Assert.That(subject.ComputeNestedCalculation(), Does.Not.Contain(bag.BareUsage));
        }

        [Test]
        public void VerifyComputeNestedCase()
        {
            Assert.That(() => ((IUsage)null).ComputeNestedCase(), Throws.TypeOf<ArgumentNullException>());

            var emptySubject = new Usage();

            Assert.That(emptySubject.ComputeNestedCase(), Has.Count.EqualTo(0));

            var subject = BuildUsageWithMixedNestedKinds(out var bag);

            Assert.That(subject.ComputeNestedCase(), Does.Contain(bag.Case));
            Assert.That(subject.ComputeNestedCase(), Does.Not.Contain(bag.BareUsage));
        }

        [Test]
        public void VerifyComputeNestedConcern()
        {
            Assert.That(() => ((IUsage)null).ComputeNestedConcern(), Throws.TypeOf<ArgumentNullException>());

            var emptySubject = new Usage();

            Assert.That(emptySubject.ComputeNestedConcern(), Has.Count.EqualTo(0));

            var subject = BuildUsageWithMixedNestedKinds(out var bag);

            Assert.That(subject.ComputeNestedConcern(), Does.Contain(bag.Concern));
            Assert.That(subject.ComputeNestedConcern(), Does.Not.Contain(bag.BareUsage));
        }

        [Test]
        public void VerifyComputeNestedConnection()
        {
            Assert.That(() => ((IUsage)null).ComputeNestedConnection(), Throws.TypeOf<ArgumentNullException>());

            var emptySubject = new Usage();

            Assert.That(emptySubject.ComputeNestedConnection(), Has.Count.EqualTo(0));

            var subject = BuildUsageWithMixedNestedKinds(out var bag);

            // BindingConnectorAsUsage IS an IConnectorAsUsage. So is FlowUsage.
            Assert.That(subject.ComputeNestedConnection(), Does.Contain(bag.Connection));
            Assert.That(subject.ComputeNestedConnection(), Does.Contain(bag.Flow));
            Assert.That(subject.ComputeNestedConnection(), Does.Not.Contain(bag.BareUsage));
        }

        [Test]
        public void VerifyComputeNestedConstraint()
        {
            Assert.That(() => ((IUsage)null).ComputeNestedConstraint(), Throws.TypeOf<ArgumentNullException>());

            var emptySubject = new Usage();

            Assert.That(emptySubject.ComputeNestedConstraint(), Has.Count.EqualTo(0));

            var subject = BuildUsageWithMixedNestedKinds(out var bag);

            // Requirement IS a Constraint, so both should appear.
            Assert.That(subject.ComputeNestedConstraint(), Does.Contain(bag.Constraint));
            Assert.That(subject.ComputeNestedConstraint(), Does.Not.Contain(bag.BareUsage));
        }

        [Test]
        public void VerifyComputeNestedEnumeration()
        {
            Assert.That(() => ((IUsage)null).ComputeNestedEnumeration(), Throws.TypeOf<ArgumentNullException>());

            var emptySubject = new Usage();

            Assert.That(emptySubject.ComputeNestedEnumeration(), Has.Count.EqualTo(0));

            var subject = BuildUsageWithMixedNestedKinds(out var bag);

            Assert.That(subject.ComputeNestedEnumeration(), Does.Contain(bag.Enumeration));
            Assert.That(subject.ComputeNestedEnumeration(), Does.Not.Contain(bag.BareUsage));
        }

        [Test]
        public void VerifyComputeNestedFlow()
        {
            Assert.That(() => ((IUsage)null).ComputeNestedFlow(), Throws.TypeOf<ArgumentNullException>());

            var emptySubject = new Usage();

            Assert.That(emptySubject.ComputeNestedFlow(), Has.Count.EqualTo(0));

            var subject = BuildUsageWithMixedNestedKinds(out var bag);

            Assert.That(subject.ComputeNestedFlow(), Does.Contain(bag.Flow));
            Assert.That(subject.ComputeNestedFlow(), Does.Not.Contain(bag.BareUsage));
        }

        [Test]
        public void VerifyComputeNestedInterface()
        {
            Assert.That(() => ((IUsage)null).ComputeNestedInterface(), Throws.TypeOf<ArgumentNullException>());

            var emptySubject = new Usage();

            Assert.That(emptySubject.ComputeNestedInterface(), Has.Count.EqualTo(0));

            var subject = BuildUsageWithMixedNestedKinds(out var bag);

            // Asserts the C#-contract correction (XMI OCL erroneously says ReferenceUsage):
            // ComputeNestedInterface returns IInterfaceUsage, NOT ReferenceUsage.
            Assert.That(subject.ComputeNestedInterface(), Does.Contain(bag.Interface));
            Assert.That(subject.ComputeNestedInterface(), Does.Not.Contain(bag.Reference));
            Assert.That(subject.ComputeNestedInterface(), Does.Not.Contain(bag.BareUsage));
        }

        [Test]
        public void VerifyComputeNestedItem()
        {
            Assert.That(() => ((IUsage)null).ComputeNestedItem(), Throws.TypeOf<ArgumentNullException>());

            var emptySubject = new Usage();

            Assert.That(emptySubject.ComputeNestedItem(), Has.Count.EqualTo(0));

            var subject = BuildUsageWithMixedNestedKinds(out var bag);

            // PartUsage IS an ItemUsage, so it surfaces here too.
            Assert.That(subject.ComputeNestedItem(), Does.Contain(bag.Item));
            Assert.That(subject.ComputeNestedItem(), Does.Not.Contain(bag.BareUsage));
        }

        [Test]
        public void VerifyComputeNestedMetadata()
        {
            Assert.That(() => ((IUsage)null).ComputeNestedMetadata(), Throws.TypeOf<ArgumentNullException>());

            var emptySubject = new Usage();

            Assert.That(emptySubject.ComputeNestedMetadata(), Has.Count.EqualTo(0));

            var subject = BuildUsageWithMixedNestedKinds(out var bag);

            Assert.That(subject.ComputeNestedMetadata(), Does.Contain(bag.Metadata));
            Assert.That(subject.ComputeNestedMetadata(), Does.Not.Contain(bag.BareUsage));
        }

        [Test]
        public void VerifyComputeNestedOccurrence()
        {
            Assert.That(() => ((IUsage)null).ComputeNestedOccurrence(), Throws.TypeOf<ArgumentNullException>());

            var emptySubject = new Usage();

            Assert.That(emptySubject.ComputeNestedOccurrence(), Has.Count.EqualTo(0));

            var subject = BuildUsageWithMixedNestedKinds(out var bag);

            // Many subkinds (ActionUsage, ItemUsage, PartUsage, PortUsage, ...) ARE OccurrenceUsage.
            Assert.That(subject.ComputeNestedOccurrence(), Does.Contain(bag.Occurrence));
            Assert.That(subject.ComputeNestedOccurrence(), Does.Contain(bag.Action));
            Assert.That(subject.ComputeNestedOccurrence(), Does.Not.Contain(bag.BareUsage));
            Assert.That(subject.ComputeNestedOccurrence(), Does.Not.Contain(bag.Attribute));
        }

        [Test]
        public void VerifyComputeNestedPart()
        {
            Assert.That(() => ((IUsage)null).ComputeNestedPart(), Throws.TypeOf<ArgumentNullException>());

            var emptySubject = new Usage();

            Assert.That(emptySubject.ComputeNestedPart(), Has.Count.EqualTo(0));

            var subject = BuildUsageWithMixedNestedKinds(out var bag);

            Assert.That(subject.ComputeNestedPart(), Does.Contain(bag.Part));
            Assert.That(subject.ComputeNestedPart(), Does.Not.Contain(bag.BareUsage));
        }

        [Test]
        public void VerifyComputeNestedPort()
        {
            Assert.That(() => ((IUsage)null).ComputeNestedPort(), Throws.TypeOf<ArgumentNullException>());

            var emptySubject = new Usage();

            Assert.That(emptySubject.ComputeNestedPort(), Has.Count.EqualTo(0));

            var subject = BuildUsageWithMixedNestedKinds(out var bag);

            Assert.That(subject.ComputeNestedPort(), Does.Contain(bag.Port));
            Assert.That(subject.ComputeNestedPort(), Does.Not.Contain(bag.BareUsage));
        }

        [Test]
        public void VerifyComputeNestedReference()
        {
            Assert.That(() => ((IUsage)null).ComputeNestedReference(), Throws.TypeOf<ArgumentNullException>());

            var emptySubject = new Usage();

            Assert.That(emptySubject.ComputeNestedReference(), Has.Count.EqualTo(0));

            var subject = BuildUsageWithMixedNestedKinds(out var bag);

            Assert.That(subject.ComputeNestedReference(), Does.Contain(bag.Reference));
            Assert.That(subject.ComputeNestedReference(), Does.Not.Contain(bag.BareUsage));
            Assert.That(subject.ComputeNestedReference(), Does.Not.Contain(bag.Interface));
        }

        [Test]
        public void VerifyComputeNestedRendering()
        {
            Assert.That(() => ((IUsage)null).ComputeNestedRendering(), Throws.TypeOf<ArgumentNullException>());

            var emptySubject = new Usage();

            Assert.That(emptySubject.ComputeNestedRendering(), Has.Count.EqualTo(0));

            var subject = BuildUsageWithMixedNestedKinds(out var bag);

            Assert.That(subject.ComputeNestedRendering(), Does.Contain(bag.Rendering));
            Assert.That(subject.ComputeNestedRendering(), Does.Not.Contain(bag.BareUsage));
        }

        [Test]
        public void VerifyComputeNestedRequirement()
        {
            Assert.That(() => ((IUsage)null).ComputeNestedRequirement(), Throws.TypeOf<ArgumentNullException>());

            var emptySubject = new Usage();

            Assert.That(emptySubject.ComputeNestedRequirement(), Has.Count.EqualTo(0));

            var subject = BuildUsageWithMixedNestedKinds(out var bag);

            Assert.That(subject.ComputeNestedRequirement(), Does.Contain(bag.Requirement));
            Assert.That(subject.ComputeNestedRequirement(), Does.Not.Contain(bag.BareUsage));
        }

        [Test]
        public void VerifyComputeNestedState()
        {
            Assert.That(() => ((IUsage)null).ComputeNestedState(), Throws.TypeOf<ArgumentNullException>());

            var emptySubject = new Usage();

            Assert.That(emptySubject.ComputeNestedState(), Has.Count.EqualTo(0));

            var subject = BuildUsageWithMixedNestedKinds(out var bag);

            Assert.That(subject.ComputeNestedState(), Does.Contain(bag.State));
            Assert.That(subject.ComputeNestedState(), Does.Not.Contain(bag.BareUsage));
        }

        [Test]
        public void VerifyComputeNestedTransition()
        {
            Assert.That(() => ((IUsage)null).ComputeNestedTransition(), Throws.TypeOf<ArgumentNullException>());

            var emptySubject = new Usage();

            Assert.That(emptySubject.ComputeNestedTransition(), Has.Count.EqualTo(0));

            var subject = BuildUsageWithMixedNestedKinds(out var bag);

            Assert.That(subject.ComputeNestedTransition(), Does.Contain(bag.Transition));
            Assert.That(subject.ComputeNestedTransition(), Does.Not.Contain(bag.BareUsage));
        }

        [Test]
        public void VerifyComputeNestedUsage()
        {
            Assert.That(() => ((IUsage)null).ComputeNestedUsage(), Throws.TypeOf<ArgumentNullException>());

            var emptySubject = new Usage();

            Assert.That(emptySubject.ComputeNestedUsage(), Has.Count.EqualTo(0));

            var subject = new Usage();
            var ownedUsage = new PartUsage();
            var ownedNonUsage = new Feature();

            subject.AssignOwnership(new FeatureMembership(), ownedUsage);
            subject.AssignOwnership(new FeatureMembership(), ownedNonUsage);

            // Only the IUsage member is returned; bare IFeature is excluded.
            Assert.That(subject.ComputeNestedUsage(), Does.Contain(ownedUsage));
            Assert.That(subject.ComputeNestedUsage(), Does.Not.Contain(ownedNonUsage));
        }

        [Test]
        public void VerifyComputeNestedUseCase()
        {
            Assert.That(() => ((IUsage)null).ComputeNestedUseCase(), Throws.TypeOf<ArgumentNullException>());

            var emptySubject = new Usage();

            Assert.That(emptySubject.ComputeNestedUseCase(), Has.Count.EqualTo(0));

            var subject = BuildUsageWithMixedNestedKinds(out var bag);

            Assert.That(subject.ComputeNestedUseCase(), Does.Contain(bag.UseCase));
            Assert.That(subject.ComputeNestedUseCase(), Does.Not.Contain(bag.BareUsage));
        }

        [Test]
        public void VerifyComputeNestedVerificationCase()
        {
            Assert.That(() => ((IUsage)null).ComputeNestedVerificationCase(), Throws.TypeOf<ArgumentNullException>());

            var emptySubject = new Usage();

            Assert.That(emptySubject.ComputeNestedVerificationCase(), Has.Count.EqualTo(0));

            var subject = BuildUsageWithMixedNestedKinds(out var bag);

            Assert.That(subject.ComputeNestedVerificationCase(), Does.Contain(bag.VerificationCase));
            Assert.That(subject.ComputeNestedVerificationCase(), Does.Not.Contain(bag.BareUsage));
        }

        [Test]
        public void VerifyComputeNestedView()
        {
            Assert.That(() => ((IUsage)null).ComputeNestedView(), Throws.TypeOf<ArgumentNullException>());

            var emptySubject = new Usage();

            Assert.That(emptySubject.ComputeNestedView(), Has.Count.EqualTo(0));

            var subject = BuildUsageWithMixedNestedKinds(out var bag);

            Assert.That(subject.ComputeNestedView(), Does.Contain(bag.View));
            Assert.That(subject.ComputeNestedView(), Does.Not.Contain(bag.BareUsage));
        }

        [Test]
        public void VerifyComputeNestedViewpoint()
        {
            Assert.That(() => ((IUsage)null).ComputeNestedViewpoint(), Throws.TypeOf<ArgumentNullException>());

            var emptySubject = new Usage();

            Assert.That(emptySubject.ComputeNestedViewpoint(), Has.Count.EqualTo(0));

            var subject = BuildUsageWithMixedNestedKinds(out var bag);

            Assert.That(subject.ComputeNestedViewpoint(), Does.Contain(bag.Viewpoint));
            Assert.That(subject.ComputeNestedViewpoint(), Does.Not.Contain(bag.BareUsage));
        }

        [Test]
        public void VerifyComputeOwningDefinition()
        {
            Assert.That(() => ((IUsage)null).ComputeOwningDefinition(), Throws.TypeOf<ArgumentNullException>());

            var orphan = new Usage();

            Assert.That(orphan.ComputeOwningDefinition(), Is.Null);

            // Owner is a Definition → returned narrowed.
            var definition = new Definition();
            var nestedInDefinition = new Usage();
            definition.AssignOwnership(new FeatureMembership(), nestedInDefinition);

            Assert.That(nestedInDefinition.ComputeOwningDefinition(), Is.SameAs(definition));

            // Owner is a Usage (not Definition) → returns null.
            var owningUsage = new Usage();
            var nestedInUsage = new Usage();
            owningUsage.AssignOwnership(new FeatureMembership(), nestedInUsage);

            Assert.That(nestedInUsage.ComputeOwningDefinition(), Is.Null);
        }

        [Test]
        public void VerifyComputeOwningUsage()
        {
            Assert.That(() => ((IUsage)null).ComputeOwningUsage(), Throws.TypeOf<ArgumentNullException>());

            var orphan = new Usage();

            Assert.That(orphan.ComputeOwningUsage(), Is.Null);

            // Owner is a Usage → returned narrowed.
            var owningUsage = new Usage();
            var nestedInUsage = new Usage();
            owningUsage.AssignOwnership(new FeatureMembership(), nestedInUsage);

            Assert.That(nestedInUsage.ComputeOwningUsage(), Is.SameAs(owningUsage));

            // Owner is a Definition (not Usage) → returns null.
            var definition = new Definition();
            var nestedInDefinition = new Usage();
            definition.AssignOwnership(new FeatureMembership(), nestedInDefinition);

            Assert.That(nestedInDefinition.ComputeOwningUsage(), Is.Null);
        }

        [Test]
        public void VerifyComputeUsage()
        {
            Assert.That(() => ((IUsage)null).ComputeUsage(), Throws.TypeOf<ArgumentNullException>());

            var emptySubject = new Usage();

            Assert.That(emptySubject.ComputeUsage(), Has.Count.EqualTo(0));

            var subject = new Usage();
            var nestedUsage = new PartUsage();
            var nestedFeature = new Feature();

            subject.AssignOwnership(new FeatureMembership(), nestedUsage);
            subject.AssignOwnership(new FeatureMembership(), nestedFeature);

            // feature.OfType<IUsage>() — only the IUsage one surfaces.
            Assert.That(subject.ComputeUsage(), Does.Contain(nestedUsage));
            Assert.That(subject.ComputeUsage(), Does.Not.Contain(nestedFeature));
        }

        [Test]
        public void VerifyComputeVariant()
        {
            Assert.That(() => ((IUsage)null).ComputeVariant(), Throws.TypeOf<ArgumentNullException>());

            var emptySubject = new Usage();

            Assert.That(emptySubject.ComputeVariant(), Has.Count.EqualTo(0));

            // Populated case is stub-blocked: VariantMembershipExtensions.ComputeOwnedVariantUsage
            // is still a NotSupportedException stub. Asserting NotSupportedException here makes the
            // block visible; once the upstream stub is implemented, this assertion will fail loudly,
            // forcing a real assertion to be written.
            // For later: populated case depends on VariantMembershipExtensions.ComputeOwnedVariantUsage,
            // which is still a stub.
            var subject = new Usage();
            var variantUsage = new PartUsage();
            subject.AssignOwnership(new VariantMembership(), variantUsage);

            Assert.That(subject.ComputeVariant, Throws.TypeOf<NotSupportedException>());
        }

        [Test]
        public void VerifyComputeVariantMembership()
        {
            Assert.That(() => ((IUsage)null).ComputeVariantMembership(), Throws.TypeOf<ArgumentNullException>());

            var emptySubject = new Usage();

            Assert.That(emptySubject.ComputeVariantMembership(), Has.Count.EqualTo(0));

            var subject = new Usage();
            var variantUsage = new PartUsage();
            var variantMembership = new VariantMembership();
            subject.AssignOwnership(variantMembership, variantUsage);

            // A non-VariantMembership FeatureMembership in the same OwnedRelationship MUST NOT surface.
            var plainNested = new PartUsage();
            subject.AssignOwnership(new FeatureMembership(), plainNested);

            Assert.That(subject.ComputeVariantMembership(), Is.EquivalentTo(new[] { variantMembership }));
        }

        [Test]
        public void VerifyComputeRedefinedNamingFeatureOperation()
        {
            Assert.That(() => ((IUsage)null).ComputeRedefinedNamingFeatureOperation(), Throws.TypeOf<ArgumentNullException>());

            // Branch 1: owningMembership is NOT a VariantMembership AND no Redefinition → null
            var nonVariantOwner = new Definition();
            var subjectNonVariantNoRedef = new Usage();
            nonVariantOwner.AssignOwnership(new FeatureMembership(), subjectNonVariantNoRedef);

            Assert.That(subjectNonVariantNoRedef.ComputeRedefinedNamingFeatureOperation(), Is.Null);

            // Branch 1: owningMembership is NOT a VariantMembership AND has a Redefinition → returns RedefinedFeature
            var nonVariantOwner2 = new Definition();
            var subjectNonVariantWithRedef = new Usage();
            nonVariantOwner2.AssignOwnership(new FeatureMembership(), subjectNonVariantWithRedef);
            var redefinedTarget = new Feature();
            subjectNonVariantWithRedef.AssignOwnership(new Redefinition { RedefinedFeature = redefinedTarget });

            Assert.That(subjectNonVariantWithRedef.ComputeRedefinedNamingFeatureOperation(), Is.SameAs(redefinedTarget));

            // Branch 2: owningMembership IS a VariantMembership AND no ownedReferenceSubsetting → null
            var variantOwner = new Usage();
            var variantSubject = new Usage();
            variantOwner.AssignOwnership(new VariantMembership(), variantSubject);

            Assert.That(variantSubject.ComputeRedefinedNamingFeatureOperation(), Is.Null);

            // Branch 2: owningMembership IS a VariantMembership AND ownedReferenceSubsetting present → returns ReferencedFeature
            var variantOwner2 = new Usage();
            var variantSubjectWithRef = new Usage();
            variantOwner2.AssignOwnership(new VariantMembership(), variantSubjectWithRef);
            var refTarget = new Feature();
            variantSubjectWithRef.AssignOwnership(new ReferenceSubsetting { ReferencedFeature = refTarget });

            Assert.That(variantSubjectWithRef.ComputeRedefinedNamingFeatureOperation(), Is.SameAs(refTarget));

            // Edge case: owningMembership is null → falls into branch 1 → null (no redefinitions).
            var subjectNoOwner = new Usage();

            Assert.That(subjectNoOwner.ComputeRedefinedNamingFeatureOperation(), Is.Null);
        }

        [Test]
        public void VerifyComputeReferencedFeatureTargetOperation()
        {
            Assert.That(() => ((IUsage)null).ComputeReferencedFeatureTargetOperation(), Throws.TypeOf<ArgumentNullException>());

            // No ownedReferenceSubsetting → null
            var subjectNoSubsetting = new Usage();

            Assert.That(subjectNoSubsetting.ComputeReferencedFeatureTargetOperation(), Is.Null);

            // ownedReferenceSubsetting set, ReferencedFeature with no chainingFeatures → returns the referencedFeature itself
            // (because IFeature.featureTarget == self when chainingFeatures is empty).
            var subject = new Usage();
            var referencedFeature = new Feature();
            subject.AssignOwnership(new ReferenceSubsetting { ReferencedFeature = referencedFeature });

            Assert.That(subject.ComputeReferencedFeatureTargetOperation(), Is.SameAs(referencedFeature));
        }
    }
}
