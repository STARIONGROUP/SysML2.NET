// ------------------------------------------------------------------------------------------------
// <copyright file="KernelModelTestFixture.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SySML2.NET.REST.Tests.HelpersTests.Models
{
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Text.Json;

    using NUnit.Framework;

    using SysML2.NET.Serializer.Json;

    using SySML2.NET.REST.Tests.Helpers;
    using SySML2.NET.REST.Tests.Helpers.Models;

    [TestFixture]
    public class KernelModelTestFixture
    {
        private static readonly string[] KernelMetaclasses =
        [
            "Association", "AssociationStructure", "Behavior", "ParameterMembership", "Step", "Class", "BindingConnector", "Connector",
            "Succession", "DataType", "CollectExpression", "ConstructorExpression", "FeatureChainExpression", "FeatureReferenceExpression",
            "IndexExpression", "InvocationExpression", "LiteralBoolean", "LiteralExpression", "LiteralInfinity", "LiteralInteger",
            "LiteralRational", "LiteralString", "MetadataAccessExpression", "NullExpression", "OperatorExpression", "SelectExpression",
            "FeatureValue", "BooleanExpression", "Expression", "Function", "Invariant", "Predicate", "ResultExpressionMembership",
            "ReturnParameterMembership", "Flow", "FlowEnd", "Interaction", "PayloadFeature", "SuccessionFlow", "Metaclass",
            "MetadataFeature", "MultiplicityRange", "Structure"
        ];

        [Test]
        public void VerifyKernelModel()
        {
            var root = new RootModel();
            var core = new CoreModel(root);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(() => new KernelModel(null, core), Throws.ArgumentNullException);
                Assert.That(() => new KernelModel(root, null), Throws.ArgumentNullException);
            }

            root = new RootModel();
            core = new CoreModel(root);
            var atStep3 = core.Builder.ToDtos();
            var model = new KernelModel(root, core);

            var dtos = model.Builder.ToDtos();
            var changed = ModelBuilder.Changed(atStep3, dtos);
            var changedIds = changed.Select(dto => dto.Id).ToList();

            using var stream = new MemoryStream();
            new Serializer().SerializeRequest(ModelBuilder.ToCommitRequest(changed, "kernel model"), SerializationModeKind.JSON, false, stream, new JsonWriterOptions());
            var json = Encoding.UTF8.GetString(stream.ToArray());

            using (Assert.EnterMultipleScope())
            {
                Assert.That(dtos.Select(dto => dto.Id), Is.Unique);
                Assert.That(dtos.Select(dto => dto.GetType().Name).Distinct(), Is.SupersetOf(KernelMetaclasses));
                Assert.That(changedIds, Does.Contain(root.Root.Id).And.Contain(core.FirstEnd.Id).And.Contain(core.SecondEnd.Id));
                Assert.That(changedIds, Does.Not.Contain(core.A.Id));
                Assert.That(changed, Has.Count.EqualTo(dtos.Count - atStep3.Count + 3));
                Assert.That(json, Does.Not.Contain("00000000-0000-0000-0000-000000000000"));
                Assert.That(model.Builder.WithoutDerivedValues, Is.EquivalentTo((string[])["kernel/sum", "kernel/index", "kernel/select", "kernel/collect", "kernel/chain"]), "only the operator expressions, whose instantiated type needs the model library");
            }

            var again = new RootModel();
            Assert.That(new KernelModel(again, new CoreModel(again)).Builder.ToDtos().Select(dto => dto.Id), Is.EqualTo(dtos.Select(dto => dto.Id)));
        }
    }
}
