// ------------------------------------------------------------------------------------------------
// <copyright file="SystemsUpdateModelTestFixture.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SySML2.NET.REST.Tests.HelpersTests.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using NUnit.Framework;

    using SysML2.NET.Core.DTO.Root.Elements;

    using SySML2.NET.REST.Tests.Helpers;
    using SySML2.NET.REST.Tests.Helpers.Models;

    using PocoConjugatedPortTyping = SysML2.NET.Core.POCO.Systems.Ports.ConjugatedPortTyping;
    using PocoElement = SysML2.NET.Core.POCO.Root.Elements.IElement;
    using PocoFeatureValue = SysML2.NET.Core.POCO.Kernel.FeatureValues.FeatureValue;

    [TestFixture]
    public class SystemsUpdateModelTestFixture
    {
        private static readonly (string Element, string Property)[] Changes =
        [
            ("systems structure/Ind", "OccurrenceDefinition.IsIndividual"),
            ("systems structure/solo", "OccurrenceUsage.IsIndividual"),
            ("systems structure/Plain", "Definition.IsVariation"),
            ("systems structure/bare", "Usage.IsVariation"),
            ("systems structure/ts", "OccurrenceUsage.PortionKind"),
            ("systems behaviour/limit", "Feature.IsConstant"),
            ("systems behaviour/limit", "Feature.IsVariable"),
            ("systems behaviour/R/assumed/membership", "RequirementConstraintMembership.Kind"),
            ("systems behaviour/R/required/membership", "RequirementConstraintMembership.Kind"),
            ("systems behaviour/Par", "StateDefinition.IsParallel"),
            ("systems behaviour/pu", "StateUsage.IsParallel"),
            ("systems behaviour/St/entry/membership", "StateSubactionMembership.Kind"),
            ("systems behaviour/St/exit/membership", "StateSubactionMembership.Kind"),
            ("systems behaviour/t/Trigger/membership", "TransitionFeatureMembership.Kind"),
            ("systems behaviour/accept at/trigger", "TriggerInvocationExpression.Kind")
        ];

        [Test]
        public void VerifySystemsUpdateModel()
        {
            var (structure, behavior) = BuildToStep7();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(() => new SystemsUpdateModel(null, behavior), Throws.ArgumentNullException);
                Assert.That(() => new SystemsUpdateModel(structure, null), Throws.ArgumentNullException);
            }

            (structure, behavior) = BuildToStep7();

            var atStep7 = behavior.Builder.ToDtos();
            var before = atStep7.ToDictionary(dto => dto.Id);
            List<string> withoutDerivedValues = [.. behavior.Builder.WithoutDerivedValues];
            var systemsMetaclasses = behavior.Builder.Elements
                .Where(entry => entry.Name.StartsWith("systems ", StringComparison.Ordinal))
                .Select(entry => entry.Element.GetType().Name)
                .Distinct()
                .ToList();

            var model = new SystemsUpdateModel(structure, behavior);
            var dtos = model.Builder.ToDtos();
            var after = dtos.ToDictionary(dto => dto.Id);
            var changed = ModelBuilder.Changed(atStep7, dtos);

            var limit = model.Builder.Find<PocoElement>("systems behaviour/limit");
            var initialValue = limit.OwnedRelationship.OfType<PocoFeatureValue>().Single();
            var spareTyping = structure.SparePort.OwnedRelationship.OfType<PocoConjugatedPortTyping>().Single();
            var namesById = model.Builder.Elements.ToDictionary(entry => entry.Element.Id, entry => entry.Name);
            var rational = model.Builder.Find<PocoElement>("kernel/rational").Id;
            var renamed = changed.Where(dto => dto.Id != rational && dto.DeclaredName?.EndsWith(" renamed") == true).ToList();
            var unchanged = Changes.Where(change => !Differs(before, after, model.Builder.Find<PocoElement>(change.Element).Id, change.Property)).Select(change => $"{change.Element} {change.Property}");

            using (Assert.EnterMultipleScope())
            {
                Assert.That(dtos, Has.Count.EqualTo(atStep7.Count), "the update creates nothing");
                Assert.That(systemsMetaclasses.Where(metaclass => !changed.Any(dto => dto.GetType().Name == metaclass && dto.DeclaredName?.EndsWith(" renamed") == true)), Is.Empty, "U1: every metaclass of steps 6 and 7 has a renamed instance");
                Assert.That(renamed.Where(dto => dto.DeclaredShortName == before[dto.Id].DeclaredShortName || dto.DeclaredShortName?.EndsWith('2') != true), Is.Empty, "U1 also changes the short name");
                Assert.That(changed.Where(dto => !namesById[dto.Id].StartsWith("systems structure/", StringComparison.Ordinal) && !namesById[dto.Id].StartsWith("systems behaviour/", StringComparison.Ordinal)).Select(dto => dto.Id), Is.EqualTo((Guid[])[rational]), "only the content of steps 6 and 7, and the rational literal that its comparer reports as changed (#426)");
                Assert.That(unchanged, Is.Empty, "U2: each listed attribute changed");
                Assert.That(Differs(before, after, initialValue.Id, "FeatureValue.IsInitial"), Is.True);
                Assert.That(Value(after, spareTyping.Id, "ConjugatedPortTyping.ConjugatedPortDefinition"), Is.EqualTo(structure.OtherConjugatedPortDefinition.Id));
                Assert.That(Value(after, structure.PlainDefinition.Id, "Type.IsAbstract"), Is.True, "a variation is abstract");
                Assert.That(Value(after, structure.PlainUsage.Id, "Type.IsAbstract"), Is.True, "a variation is abstract");
                Assert.That(Value(after, limit.Id, "Feature.IsVariable"), Is.False);
                Assert.That(Value(after, initialValue.Id, "FeatureValue.IsInitial"), Is.False, "an initial value needs a variable feature");
                Assert.That(model.Builder.WithoutDerivedValues, Is.EquivalentTo(withoutDerivedValues), "no changed element loses its derived values");
            }
        }

        private static (SystemsStructureModel Structure, SystemsBehaviorModel Behavior) BuildToStep7()
        {
            var root = new RootModel();
            var core = new CoreModel(root);
            var kernel = new KernelModel(root, core);
            _ = new KerMLUpdateModel(root, core, kernel);
            var structure = new SystemsStructureModel(root);

            return (structure, new SystemsBehaviorModel(root, structure));
        }

        private static bool Differs(IReadOnlyDictionary<Guid, IElement> before, IReadOnlyDictionary<Guid, IElement> after, Guid id, string property)
        {
            return !DtoMetadata.ValueEquals(Value(before, id, property), Value(after, id, property));
        }

        private static object Value(IReadOnlyDictionary<Guid, IElement> dtos, Guid id, string property)
        {
            var dto = dtos[id];

            var metamodelProperty = DtoMetadata.PropertiesOf(dto.GetType()).SingleOrDefault(candidate => candidate.Name == property)
                                    ?? throw new ArgumentException($"{dto.GetType().Name} has no non-derived property {property}", nameof(property));

            return metamodelProperty.ValueOf(dto);
        }
    }
}
