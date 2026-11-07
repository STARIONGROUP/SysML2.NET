// ------------------------------------------------------------------------------------------------
// <copyright file="KerMLUpdateModelTestFixture.cs" company="Starion Group S.A.">
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

    using SysML2.NET.Core.DTO.Kernel.Packages;
    using SysML2.NET.Core.DTO.Root.Elements;

    using SySML2.NET.REST.Tests.Helpers;
    using SySML2.NET.REST.Tests.Helpers.Models;

    using PocoElement = SysML2.NET.Core.POCO.Root.Elements.IElement;
    using PocoFeature = SysML2.NET.Core.POCO.Core.Features.Feature;
    using PocoFeatureTyping = SysML2.NET.Core.POCO.Core.Features.FeatureTyping;

    [TestFixture]
    public class KerMLUpdateModelTestFixture
    {
        private static readonly string[] OwningMemberships = ["OwningMembership", "FeatureMembership", "EndFeatureMembership", "ParameterMembership", "ReturnParameterMembership", "ResultExpressionMembership", "FeatureValue", "ElementFilterMembership"];

        private static readonly (string Metaclass, string Element, string Property)[] Retargets =
        [
            ("Annotation", "root/Note/about Nested", "Annotation.AnnotatedElement"),
            ("Dependency", "root/Uses", "Dependency.Supplier"),
            ("NamespaceImport", "root/Nested/import Library", "NamespaceImport.ImportedNamespace"),
            ("MembershipImport", "root/Nested/import LibAlias", "MembershipImport.ImportedMembership"),
            ("Membership", "root/Space/LibAlias", "Membership.MemberElement"),
            ("FeatureTyping", "core/f/typing", "FeatureTyping.Type"),
            ("Subsetting", "core/f/subsetting", "Subsetting.SubsettedFeature"),
            ("Redefinition", "core/r/redefinition", "Redefinition.RedefinedFeature"),
            ("ReferenceSubsetting", "core/k/reference", "ReferenceSubsetting.ReferencedFeature"),
            ("CrossSubsetting", "core/e1/cross", "CrossSubsetting.CrossedFeature"),
            ("Specialization", "core/U/specialization", "Specialization.General"),
            ("Subclassification", "kernel/M/subclassification", "Subclassification.Superclassifier"),
            ("Conjugation", "core/T/conjugation", "Conjugation.OriginalType"),
            ("Disjoining", "core/C/disjoining", "Disjoining.DisjoiningType"),
            ("Unioning", "core/U/unioning 0", "Unioning.UnioningType"),
            ("Intersecting", "core/U/intersecting 0", "Intersecting.IntersectingType"),
            ("Differencing", "core/U/differencing 0", "Differencing.DifferencingType"),
            ("FeatureChaining", "core/cf/chaining 1", "FeatureChaining.ChainingFeature"),
            ("FeatureInverting", "core/k/inverting", "FeatureInverting.InvertingFeature"),
            ("Connector", "kernel/k/target/reference", "ReferenceSubsetting.ReferencedFeature"),
            ("BindingConnector", "kernel/bc/target/reference", "ReferenceSubsetting.ReferencedFeature"),
            ("Succession", "kernel/su/first/reference", "ReferenceSubsetting.ReferencedFeature"),
            ("Flow", "kernel/fl/source/reference", "ReferenceSubsetting.ReferencedFeature"),
            ("SuccessionFlow", "kernel/sf/source/reference", "ReferenceSubsetting.ReferencedFeature"),
            ("Association", "kernel/As/e2 typing", "FeatureTyping.Type"),
            ("AssociationStructure", "kernel/AsS/e2 typing", "FeatureTyping.Type"),
            ("Interaction", "kernel/I/e2 typing", "FeatureTyping.Type")
        ];

        [Test]
        public void VerifyKerMLUpdateModel()
        {
            var root = new RootModel();
            var core = new CoreModel(root);
            var kernel = new KernelModel(root, core);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(() => new KerMLUpdateModel(null, core, kernel), Throws.ArgumentNullException);
                Assert.That(() => new KerMLUpdateModel(root, null, kernel), Throws.ArgumentNullException);
                Assert.That(() => new KerMLUpdateModel(root, core, null), Throws.ArgumentNullException);
            }

            root = new RootModel();
            core = new CoreModel(root);
            kernel = new KernelModel(root, core);

            var atStep4 = kernel.Builder.ToDtos();
            var before = atStep4.ToDictionary(dto => dto.Id);
            var metaclassesAtStep4 = atStep4.Select(dto => dto.GetType()).Distinct().ToList();
            var model = new KerMLUpdateModel(root, core, kernel);

            var dtos = model.Builder.ToDtos();
            var byId = dtos.ToDictionary(dto => dto.Id);
            var changed = ModelBuilder.Changed(atStep4, dtos);
            var updated = dtos.Where(dto => before.ContainsKey(dto.Id)).ToList();

            var withoutU2ToU4 = metaclassesAtStep4
                .Where(metaclass => metaclass.Name != "TypeFeaturing" && !updated.Any(dto => dto.GetType() == metaclass && DiffersBeyondNames(before[dto.Id], dto)))
                .Select(metaclass => metaclass.Name);

            var relationshipMetaclasses = metaclassesAtStep4.Where(typeof(IRelationship).IsAssignableFrom).Select(metaclass => metaclass.Name);

            var notRetargeted = Retargets
                .Where(retarget => !Differs(before, byId, Element(model.Builder, retarget.Element).Id, retarget.Property))
                .Select(retarget => retarget.Metaclass);

            var corePackage = (Package)byId[core.Package.Id];
            var corePackageBefore = (Package)before[core.Package.Id];
            var chain = model.Builder.Find<PocoFeature>("kerml update/cf2");

            using (Assert.EnterMultipleScope())
            {
                Assert.That(dtos, Has.Count.GreaterThan(atStep4.Count));
                Assert.That(changed.Select(dto => dto.Id), Does.Contain(model.Added.Id).And.Contain(core.Package.Id));
                Assert.That(metaclassesAtStep4.Where(metaclass => !changed.Any(dto => dto.GetType() == metaclass && dto.DeclaredName?.EndsWith(" renamed") == true)), Is.Empty, "U1: every metaclass has a renamed instance");
                Assert.That(withoutU2ToU4, Is.Empty, "every metaclass but TypeFeaturing gets one of U2 to U4");
                Assert.That(relationshipMetaclasses, Is.EquivalentTo(Retargets.Select(retarget => retarget.Metaclass).Concat(OwningMemberships).Append("TypeFeaturing").Distinct()), "every relationship metaclass is retargeted or exempt");
                Assert.That(notRetargeted, Is.Empty, "U3: the end of each listed relationship changed");
                Assert.That(Value(byId, Element(model.Builder, "root/Nested/import Library").Id, "Import.IsRecursive"), Is.False);
                Assert.That(Value(byId, Element(model.Builder, "root/Nested/import Library").Id, "NamespaceImport.ImportedNamespace"), Is.EqualTo(Element(model.Builder, "root/Space").Id));
                Assert.That(Value(byId, Element(model.Builder, "kernel/a/value/42").Id, "LiteralInteger.Value"), Is.EqualTo(43));
                Assert.That(Value(byId, Element(model.Builder, "kernel/inv").Id, "Invariant.IsNegated"), Is.False);
                Assert.That(corePackage.OwnedRelationship, Is.EqualTo((Guid[])[.. corePackageBefore.OwnedRelationship, chain.OwningRelationship.Id, model.Added.OwningRelationship.Id]), "U4: Core is extended");
                Assert.That(model.Builder.WithoutDerivedValues, Is.EquivalentTo((string[])["kernel/sum", "kernel/index", "kernel/select", "kernel/collect", "kernel/chain"]));
            }
        }

        private static bool Differs(IReadOnlyDictionary<Guid, IElement> before, IReadOnlyDictionary<Guid, IElement> after, Guid id, string property)
        {
            return !DtoMetadata.ValueEquals(Value(before, id, property), Value(after, id, property));
        }

        private static bool DiffersBeyondNames(IElement before, IElement after)
        {
            return DtoMetadata.PropertiesOf(after.GetType())
                .Where(property => property.Name is not ("Element.DeclaredName" or "Element.DeclaredShortName"))
                .Any(property => !DtoMetadata.ValueEquals(property.ValueOf(before), property.ValueOf(after)));
        }

        private static PocoElement Element(ModelBuilder builder, string name)
        {
            if (!name.EndsWith(" typing", StringComparison.Ordinal))
            {
                return builder.Find<PocoElement>(name);
            }

            return builder.Find<PocoFeature>(name[..^" typing".Length]).OwnedRelationship.OfType<PocoFeatureTyping>().Single();
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
