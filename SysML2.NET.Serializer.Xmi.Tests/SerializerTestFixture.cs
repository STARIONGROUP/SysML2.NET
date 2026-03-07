// -------------------------------------------------------------------------------------------------
// <copyright file="SerializerTestFixture.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Serializer.Xmi.Tests
{
    using System;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    using Core.POCO.Root.Namespaces;

    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    using SysML2.NET.Serializer.Xmi.Extensions;
    using SysML2.NET.Serializer.Xmi.Readers;

    [TestFixture]
    public class SerializerTestFixture
    {
        private Serializer serializer;
        private DeSerializer deSerializer;
        private XmiDataCache xmiDataCache;

        private INamespace anonymouseNameSpace;

        [SetUp]
        public void Setup()
        {
            var serviceProvider = new ServiceCollection()
                .AddLogging(x => x.AddConsole())
                .BuildServiceProvider();

            this.xmiDataCache = new XmiDataCache(new PocoReferenceResolveExtensionsFacade(), serviceProvider.GetRequiredService<ILogger<XmiDataCache>>());

            this.deSerializer = new DeSerializer(new ExternalReferenceService(serviceProvider.GetRequiredService<ILogger<ExternalReferenceService>>()), new XmiDataReaderFacade(), this.xmiDataCache, serviceProvider.GetRequiredService<ILoggerFactory>());

            this.ReadAndAssemblePopulationFromXmiFile();

            this.serializer = new Serializer(serviceProvider.GetRequiredService<ILoggerFactory>());
        }

        [Test]
        public void Verify_that_the_name_space_can_be_serialized_to_xmi()
        {
            var targetStream = new MemoryStream();

            Assert.That(() => this.serializer.Serialize(this.anonymouseNameSpace, false, targetStream), Throws.Nothing);

            Assert.That(targetStream.Length, Is.GreaterThan(0));
        }

        [Test]
        public void Verify_that_serialized_xmi_contains_expected_structure()
        {
            var targetStream = new MemoryStream();

            this.serializer.Serialize(this.anonymouseNameSpace, false, targetStream);

            targetStream.Seek(0, SeekOrigin.Begin);
            var reader = new StreamReader(targetStream);
            var content = reader.ReadToEnd();

            Assert.That(content, Does.Contain("sysml:Namespace"));
            Assert.That(content, Does.Contain("xmi:id="));
            Assert.That(content, Does.Contain("xmlns:xmi"));
            Assert.That(content, Does.Contain("xmlns:xsi"));
        }

        [Test]
        public async Task Verify_that_the_name_space_can_be_serialized_to_xmi_async()
        {
            var targetStream = new MemoryStream();

            await this.serializer.SerializeAsync(this.anonymouseNameSpace, false, targetStream, CancellationToken.None);

            Assert.That(targetStream.Length, Is.GreaterThan(0));
        }

        private void ReadAndAssemblePopulationFromXmiFile()
        {
            var filePath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Resources", "Domain Libraries", "Quantities and Units", "Quantities.sysmlx");
            this.anonymouseNameSpace = this.deSerializer.DeSerialize(new Uri(filePath));
        }
    }
}
