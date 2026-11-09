// ------------------------------------------------------------------------------------------------
// <copyright file="MoveModelTestFixture.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Core.DTO.Root.Namespaces;
    using SysML2.NET.Core.DTO.Systems.Ports;

    using SySML2.NET.REST.Tests.Helpers;
    using SySML2.NET.REST.Tests.Helpers.Models;

    using PocoLiteralRational = SysML2.NET.Core.POCO.Kernel.Expressions.LiteralRational;

    [TestFixture]
    public class MoveModelTestFixture
    {
        private static readonly string[] MovedProperties =
        [
            "Specialization.Specific", "Conjugation.ConjugatedType", "Disjoining.TypeDisjoined", "Subclassification.Subclassifier",
            "FeatureTyping.TypedFeature", "Subsetting.SubsettingFeature", "Redefinition.RedefiningFeature", "FeatureInverting.FeatureInverted",
            "TypeFeaturing.FeatureOfType", "TypeFeaturing.FeaturingType", "PortConjugation.OriginalPortDefinition", "Relationship.Source",
            "Relationship.OwningRelatedElement", "Relationship.OwnedRelatedElement", "Element.OwningRelationship"
        ];

        [Test]
        public void VerifyMoveModel()
        {
            Assert.That(() => new MoveModel(null), Throws.ArgumentNullException);

            var root = new RootModel();
            var atStep2 = root.Builder.ToDtos();
            var model = new MoveModel(root);
            var dtos = model.Builder.ToDtos();
            var changed = ModelBuilder.Changed(atStep2, dtos);
            var before = dtos.ToDictionary(dto => dto.Id);
            var listsBefore = dtos.ToDictionary(dto => dto.Id, dto => (Guid[])[.. dto.OwnedRelationship]);
            var moveScope = model.Builder.Elements.Where(entry => entry.Name.StartsWith("move/", StringComparison.Ordinal)).Select(entry => entry.Element.Id).ToHashSet();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(() => model.Moved(null), Throws.ArgumentNullException);
                Assert.That(dtos.Select(dto => dto.Id), Is.Unique);
                Assert.That(changed.Where(dto => atStep2.Any(earlier => earlier.Id == dto.Id)).Select(dto => dto.Id), Is.EqualTo((Guid[])[root.Root.Id]), "only the root package changes, to own the new package");
                Assert.That(model.Moves.Select(move => before[move.Relationship].GetType().Name), Is.EquivalentTo((string[])["Specialization", "Conjugation", "Disjoining", "Subclassification", "FeatureTyping", "Subsetting", "FeatureInverting", "TypeFeaturing", "Redefinition", "OwningMembership", "OwningMembership"]));
                Assert.That(model.Moves.Where(move => ((IRelationship)before[move.Relationship]).OwningRelatedElement != move.From), Is.Empty, "each relationship starts out owned by its first owner");
                Assert.That(ConjugatedPortDefinitionsOf(before, model.FirstPortDefinition.Id), Is.EqualTo((Guid[])[model.FirstConjugatedPortDefinition.Id]), "a port definition owns exactly one conjugated port definition");
                Assert.That(ConjugatedPortDefinitionsOf(before, model.SecondPortDefinition.Id), Is.EqualTo((Guid[])[model.SecondConjugatedPortDefinition.Id]));
            }

            var moved = model.Moved(dtos);
            var movedById = moved.ToDictionary(dto => dto.Id);
            var after = dtos.Select(dto => movedById.GetValueOrDefault(dto.Id, dto)).ToDictionary(dto => dto.Id);
            var unmoved = MovedProperties.Where(name =>
            {
                var property = DtoMetadata.DeclaredProperties.Single(candidate => candidate.Name == name);
                return !moved.Any(dto => property.Interface.IsInstanceOfType(dto) && !DtoMetadata.ValueEquals(property.ValueOf(dto), property.ValueOf(before[dto.Id])));
            });

            var ownedButNotOwning = after.Values.SelectMany(owner => owner.OwnedRelationship.Where(id => ((IRelationship)after[id]).OwningRelatedElement != owner.Id).Select(id => $"{owner.Id} owns {id}"));
            var owningButNotOwned = after.Values.OfType<IRelationship>().SelectMany(relationship => relationship.OwnedRelatedElement.Where(id => after[id].OwningRelationship != relationship.Id).Select(id => $"{relationship.Id} owns {id}"));

            using (Assert.EnterMultipleScope())
            {
                Assert.That(moved.Select(dto => dto.Id), Is.SubsetOf(moveScope), "only the content of the step moves");
                Assert.That(unmoved, Is.Empty, "U5: each listed property changes");
                Assert.That(model.Moves.Where(move => !after[move.To].OwnedRelationship.Contains(move.Relationship) || after[move.From].OwnedRelationship.Contains(move.Relationship)), Is.Empty, "each relationship leaves its first owner for its second");
                Assert.That(model.Moves.Where(move => ((IRelationship)after[move.Relationship]).OwningRelatedElement != move.To), Is.Empty);
                Assert.That(model.Moves.Where(move => !((IRelationship)after[move.Relationship]).Source.SequenceEqual([move.To])), Is.Empty, "the source end of each moved relationship is its new owner");
                Assert.That(ownedButNotOwning, Is.Empty, "every owned relationship has its owner as owning related element");
                Assert.That(owningButNotOwned, Is.Empty, "every owned related element has its owning relationship");
                Assert.That(after.Values.OfType<OwningMembership>().Where(membership => ((IMembership)membership).MemberElement != membership.OwnedRelatedElement.Single()), Is.Empty, "the member element of an owning membership is its owned member element");
                Assert.That(ConjugatedPortDefinitionsOf(after, model.FirstPortDefinition.Id), Is.EqualTo((Guid[])[model.SecondConjugatedPortDefinition.Id]), "after the moves, each port definition still owns exactly one conjugated port definition");
                Assert.That(ConjugatedPortDefinitionsOf(after, model.SecondPortDefinition.Id), Is.EqualTo((Guid[])[model.FirstConjugatedPortDefinition.Id]));
                Assert.That(after.Values.OfType<PortConjugation>().Where(conjugation => after[after[conjugation.ConjugatedType].OwningRelationship!.Value] is not IRelationship { OwningRelatedElement: { } owner } || owner != conjugation.OriginalPortDefinition), Is.Empty, "a conjugated port definition conjugates the port definition that owns it");
                Assert.That(after[model.First.Id].OwningRelationship, Is.EqualTo(before[model.Second.Id].OwningRelationship));
                Assert.That(after[model.Second.Id].OwningRelationship, Is.EqualTo(before[model.First.Id].OwningRelationship));
                Assert.That(dtos.Where(dto => !dto.OwnedRelationship.SequenceEqual(listsBefore[dto.Id])), Is.Empty, "the model passed in is not changed");
            }

            var fullRoot = new RootModel();
            var core = new CoreModel(fullRoot);
            var kernel = new KernelModel(fullRoot, core);
            _ = new KerMLUpdateModel(fullRoot, core, kernel);
            var atStep11 = fullRoot.Builder.ToDtos();
            var inScenario = new MoveModel(fullRoot);
            _ = new DeletionModel(fullRoot);
            var scenarioDtos = inScenario.Builder.ToDtos();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(scenarioDtos.Select(dto => dto.Id), Is.Unique, "the content of step 12 shares the builder with steps 2 to 5");
                Assert.That(ModelBuilder.Changed(atStep11, scenarioDtos).Where(dto => atStep11.Any(earlier => earlier.Id == dto.Id)).Select(dto => dto.Id), Is.EquivalentTo((Guid[])[fullRoot.Root.Id, fullRoot.Builder.Find<PocoLiteralRational>("kernel/rational").Id]), "only the root package, and the rational literal that its comparer reports as changed (#426)");
                Assert.That(inScenario.Moved(scenarioDtos).Select(dto => dto.Id), Is.EquivalentTo(moved.Select(dto => dto.Id)), "the moves are the same in the scenario");
            }
        }

        private static Guid[] ConjugatedPortDefinitionsOf(IReadOnlyDictionary<Guid, IElement> model, Guid portDefinitionId)
        {
            return [.. model[portDefinitionId].OwnedRelationship.Select(id => model[id]).OfType<OwningMembership>().SelectMany(membership => membership.OwnedRelatedElement).Where(id => model[id] is ConjugatedPortDefinition)];
        }
    }
}
