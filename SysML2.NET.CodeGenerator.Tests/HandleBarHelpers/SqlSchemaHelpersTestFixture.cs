// ------------------------------------------------------------------------------------------------
// <copyright file="SqlSchemaHelpersTestFixture.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.CodeGenerator.Tests.HandleBarHelpers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using NUnit.Framework;

    using SysML2.NET.CodeGenerator.Generators.UmlHandleBarsGenerators;
    using SysML2.NET.CodeGenerator.HandleBarHelpers;
    using SysML2.NET.CodeGenerator.Tests.Generators.UmlHandleBarsGenerators;

    using uml4net.xmi.Readers;

    [TestFixture]
    public class SqlSchemaHelpersTestFixture
    {
        private HandlebarsPayload payload;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            this.payload = PayloadGenerator.Create(GeneratorSetupFixture.XmiReaderResult);
        }

        [Test]
        public void VerifyAssertRegistryInSyncWithModel()
        {
            var modelVersions = ClassKindRegistry.ModelVersions;
            var classKinds = ClassKindRegistry.ClassKinds;

            Assert.That(() => SqlSchemaHelpers.AssertRegistryInSyncWithModel(null), Throws.TypeOf<ArgumentNullException>());
            Assert.That(() => SqlSchemaHelpers.AssertRegistryInSyncWithModel(this.payload, null, classKinds), Throws.TypeOf<ArgumentNullException>());
            Assert.That(() => SqlSchemaHelpers.AssertRegistryInSyncWithModel(this.payload, modelVersions, null), Throws.TypeOf<ArgumentNullException>());

            Assert.That(() => SqlSchemaHelpers.AssertRegistryInSyncWithModel(this.payload), Throws.Nothing);

            IReadOnlyList<ModelVersionRegistration> unreleasedVersions = [.. modelVersions, new(2, "next-release", "SysML:_unreleased")];

            Assert.That(() => SqlSchemaHelpers.AssertRegistryInSyncWithModel(this.payload, unreleasedVersions, classKinds),
                Throws.TypeOf<InvalidOperationException>().With.Message.Contains("fingerprint"));

            IReadOnlyList<ClassKindRegistration> metaclassInRecordBand = [.. classKinds, new(2001, "StrayMetaclass", false, 1)];

            Assert.That(() => SqlSchemaHelpers.AssertRegistryInSyncWithModel(this.payload, modelVersions, metaclassInRecordBand),
                Throws.TypeOf<InvalidOperationException>().With.Message.Contains("StrayMetaclass"));

            IReadOnlyList<ClassKindRegistration> recordBelowBand = [.. classKinds, new(999, "StrayRecord", false, 1, null, true)];

            Assert.That(() => SqlSchemaHelpers.AssertRegistryInSyncWithModel(this.payload, modelVersions, recordBelowBand),
                Throws.TypeOf<InvalidOperationException>().With.Message.Contains("reserved for API data records"));

            IReadOnlyList<ClassKindRegistration> withoutPartUsage = [.. classKinds.Where(registration => registration.Name != "PartUsage")];

            Assert.That(() => SqlSchemaHelpers.AssertRegistryInSyncWithModel(this.payload, modelVersions, withoutPartUsage),
                Throws.TypeOf<InvalidOperationException>().With.Message.Contains("new(176, \"PartUsage\", false, 1),"));

            IReadOnlyList<ClassKindRegistration> withRetiredMetaclass = [.. classKinds, new(176, "RetiredMetaclass", false, 1)];

            Assert.That(() => SqlSchemaHelpers.AssertRegistryInSyncWithModel(this.payload, modelVersions, withRetiredMetaclass),
                Throws.TypeOf<InvalidOperationException>().With.Message.Contains("RetiredMetaclass"));

            IReadOnlyList<ClassKindRegistration> withClosedMetaclass = [.. classKinds, new(176, "RetiredMetaclass", false, 1, 1)];

            Assert.That(() => SqlSchemaHelpers.AssertRegistryInSyncWithModel(this.payload, modelVersions, withClosedMetaclass), Throws.Nothing);

            IReadOnlyList<ClassKindRegistration> recordNamedLikeMetaclass = [.. classKinds, new(1004, "Package", false, 1, null, true)];

            Assert.That(() => SqlSchemaHelpers.AssertRegistryInSyncWithModel(this.payload, modelVersions, recordNamedLikeMetaclass),
                Throws.TypeOf<InvalidOperationException>().With.Message.Contains("also declares as metaclasses: Package"));

            IReadOnlyList<ClassKindRegistration> driftedAbstractness =
                [.. classKinds.Select(registration => registration.Name == "Element" ? registration with { IsAbstract = false } : registration)];

            Assert.That(() => SqlSchemaHelpers.AssertRegistryInSyncWithModel(this.payload, modelVersions, driftedAbstractness),
                Throws.TypeOf<InvalidOperationException>().With.Message.Contains("abstractness of Element"));
        }

        private sealed class PayloadGenerator : UmlHandleBarsGenerator
        {
            public static HandlebarsPayload Create(XmiReaderResult xmiReaderResult)
            {
                return CreateHandlebarsPayload(xmiReaderResult);
            }

            public override Task GenerateAsync(XmiReaderResult xmiReaderResult, DirectoryInfo outputDirectory)
            {
                throw new NotSupportedException();
            }

            protected override void RegisterHelpers()
            {
            }

            protected override void RegisterTemplates()
            {
            }
        }
    }
}
