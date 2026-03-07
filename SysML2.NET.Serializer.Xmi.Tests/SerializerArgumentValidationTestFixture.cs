// -------------------------------------------------------------------------------------------------
// <copyright file="SerializerArgumentValidationTestFixture.cs" company="Starion Group S.A.">
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
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using Core.POCO.Root.Namespaces;

    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    using SysML2.NET.Serializer.Xmi.Writers;

    [TestFixture]
    public class SerializerArgumentValidationTestFixture
    {
        private Serializer serializer;
        private DeSerializer deSerializer;

        private INamespace loadedNamespace;
        private XmiWriterOptions writerOptions = new();

        [SetUp]
        public void Setup()
        {
            var serviceProvider = new ServiceCollection()
                .AddLogging(x => x.AddConsole())
                .BuildServiceProvider();

            this.deSerializer = new DeSerializer(serviceProvider.GetRequiredService<ILoggerFactory>());

            var filePath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Resources", "Domain Libraries", "Quantities and Units", "Quantities.sysmlx");
            this.loadedNamespace = this.deSerializer.DeSerialize(new Uri(filePath));

            this.serializer = new Serializer(serviceProvider.GetRequiredService<ILoggerFactory>());
        }

        [Test]
        public void Verify_that_Serialize_throws_ArgumentNullException_when_namespace_is_null()
        {
            var stream = new MemoryStream();

            Assert.That(() => this.serializer.Serialize(null, this.writerOptions, stream),
                Throws.TypeOf<ArgumentNullException>());
        }

        [Test]
        public void Verify_that_Serialize_throws_ArgumentNullException_when_stream_is_null()
        {
            Assert.That(() => this.serializer.Serialize(this.loadedNamespace, this.writerOptions, null),
                Throws.TypeOf<ArgumentNullException>());
        }

        [Test]
        public void Verify_that_Serialize_with_origin_map_throws_ArgumentNullException_when_namespace_is_null()
        {
            var stream = new MemoryStream();
            var map = new XmiElementOriginMap();

            Assert.That(() => this.serializer.Serialize(null, this.writerOptions, stream, map, new Uri("file:///test.sysmlx")),
                Throws.TypeOf<ArgumentNullException>());
        }

        [Test]
        public void Verify_that_Serialize_with_origin_map_throws_ArgumentNullException_when_stream_is_null()
        {
            var map = new XmiElementOriginMap();

            Assert.That(() => this.serializer.Serialize(this.loadedNamespace, this.writerOptions, null, map, new Uri("file:///test.sysmlx")),
                Throws.TypeOf<ArgumentNullException>());
        }

        [Test]
        public void Verify_that_Serialize_with_origin_map_succeeds_when_elementOriginMap_is_null()
        {
            var stream = new MemoryStream();

            Assert.That(() => this.serializer.Serialize(this.loadedNamespace, this.writerOptions, stream, null, new Uri("file:///test.sysmlx")),
                Throws.Nothing);
        }

        [Test]
        public void Verify_that_Serialize_with_origin_map_succeeds_when_currentFileUri_is_null()
        {
            var stream = new MemoryStream();
            var map = new XmiElementOriginMap();

            Assert.That(() => this.serializer.Serialize(this.loadedNamespace, this.writerOptions, stream, map, null),
                Throws.Nothing);
        }

        [Test]
        public void Verify_that_multi_file_Serialize_throws_ArgumentNullException_when_rootNamespace_is_null()
        {
            var map = new XmiElementOriginMap();
            var outputDir = new DirectoryInfo(Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString()));

            Assert.That(() => this.serializer.Serialize(null, map, outputDir, this.writerOptions),
                Throws.TypeOf<ArgumentNullException>());
        }

        [Test]
        public void Verify_that_multi_file_Serialize_throws_ArgumentNullException_when_elementOriginMap_is_null()
        {
            var outputDir = new DirectoryInfo(Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString()));

            Assert.That(() => this.serializer.Serialize(this.loadedNamespace, null, outputDir, this.writerOptions),
                Throws.TypeOf<ArgumentNullException>());
        }

        [Test]
        public void Verify_that_multi_file_Serialize_throws_ArgumentNullException_when_outputDirectory_is_null()
        {
            var map = new XmiElementOriginMap();

            Assert.That(() => this.serializer.Serialize(this.loadedNamespace, map, null, this.writerOptions),
                Throws.TypeOf<ArgumentNullException>());
        }

        [Test]
        public void Verify_that_SerializeAsync_throws_ArgumentNullException_when_namespace_is_null()
        {
            var stream = new MemoryStream();

            Assert.That(async () => await this.serializer.SerializeAsync(null, this.writerOptions, stream, CancellationToken.None),
                Throws.TypeOf<ArgumentNullException>());
        }

        [Test]
        public void Verify_that_SerializeAsync_throws_ArgumentNullException_when_stream_is_null()
        {
            Assert.That(async () => await this.serializer.SerializeAsync(this.loadedNamespace, this.writerOptions, null, CancellationToken.None),
                Throws.TypeOf<ArgumentNullException>());
        }

        [Test]
        public void Verify_that_SerializeAsync_with_origin_map_throws_ArgumentNullException_when_namespace_is_null()
        {
            var stream = new MemoryStream();
            var map = new XmiElementOriginMap();

            Assert.That(async () => await this.serializer.SerializeAsync(null, this.writerOptions, stream, map, new Uri("file:///test.sysmlx"), CancellationToken.None),
                Throws.TypeOf<ArgumentNullException>());
        }

        [Test]
        public void Verify_that_SerializeAsync_with_origin_map_throws_ArgumentNullException_when_stream_is_null()
        {
            var map = new XmiElementOriginMap();

            Assert.That(async () => await this.serializer.SerializeAsync(this.loadedNamespace, this.writerOptions, null, map, new Uri("file:///test.sysmlx"), CancellationToken.None),
                Throws.TypeOf<ArgumentNullException>());
        }

        [Test]
        public void Verify_that_multi_file_SerializeAsync_throws_ArgumentNullException_when_rootNamespace_is_null()
        {
            var map = new XmiElementOriginMap();
            var outputDir = new DirectoryInfo(Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString()));

            Assert.That(async () => await this.serializer.SerializeAsync(null, map, outputDir, this.writerOptions, CancellationToken.None),
                Throws.TypeOf<ArgumentNullException>());
        }

        [Test]
        public void Verify_that_multi_file_SerializeAsync_throws_ArgumentNullException_when_elementOriginMap_is_null()
        {
            var outputDir = new DirectoryInfo(Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString()));

            Assert.That(async () => await this.serializer.SerializeAsync(this.loadedNamespace, null, outputDir, this.writerOptions, CancellationToken.None),
                Throws.TypeOf<ArgumentNullException>());
        }

        [Test]
        public void Verify_that_multi_file_SerializeAsync_throws_ArgumentNullException_when_outputDirectory_is_null()
        {
            var map = new XmiElementOriginMap();

            Assert.That(async () => await this.serializer.SerializeAsync(this.loadedNamespace, map, null, this.writerOptions, CancellationToken.None),
                Throws.TypeOf<ArgumentNullException>());
        }

        [Test]
        public void Verify_that_multi_file_Serialize_creates_output_directory_if_not_exists()
        {
            var outputDir = new DirectoryInfo(Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString()));

            try
            {
                Assert.That(outputDir.Exists, Is.False, "Output directory should not exist before serialization");

                var map = new XmiElementOriginMap();
                var sourceUri = new Uri("file:///test/Quantities.sysmlx");

                map.Register(this.loadedNamespace.Id, sourceUri);
                map.RegisterRootNamespace(sourceUri, this.loadedNamespace.Id);

                this.serializer.Serialize(this.loadedNamespace, map, outputDir, this.writerOptions);

                outputDir.Refresh();
                Assert.That(outputDir.Exists, Is.True, "Output directory should be created by serialization");
            }
            finally
            {
                if (outputDir.Exists)
                {
                    outputDir.Delete(true);
                }
            }
        }

        [Test]
        public void Verify_that_multi_file_Serialize_skips_files_with_empty_root_namespace_id()
        {
            var outputDir = new DirectoryInfo(Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString()));

            try
            {
                var map = new XmiElementOriginMap();
                var sourceUri = new Uri("file:///test/NoRoot.sysmlx");

                // Register an element so GetAllSourceFiles returns this URI,
                // but do NOT call RegisterRootNamespace so GetRootNamespaceId returns Guid.Empty
                map.Register(Guid.NewGuid(), sourceUri);

                Assert.That(() => this.serializer.Serialize(this.loadedNamespace, map, outputDir, this.writerOptions),
                    Throws.Nothing);

                outputDir.Refresh();

                if (outputDir.Exists)
                {
                    var files = outputDir.GetFiles();
                    Assert.That(files, Has.Length.EqualTo(0), "No files should be written when root namespace id is empty");
                }
            }
            finally
            {
                if (outputDir.Exists)
                {
                    outputDir.Delete(true);
                }
            }
        }

        [Test]
        public void Verify_that_multi_file_Serialize_skips_files_with_unknown_namespace_id()
        {
            var outputDir = new DirectoryInfo(Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString()));

            try
            {
                var map = new XmiElementOriginMap();
                var sourceUri = new Uri("file:///test/Unknown.sysmlx");
                var unknownNamespaceId = Guid.NewGuid();

                // Register a source file with a root namespace id that does not exist in the loaded namespace tree
                map.Register(Guid.NewGuid(), sourceUri);
                map.RegisterRootNamespace(sourceUri, unknownNamespaceId);

                Assert.That(() => this.serializer.Serialize(this.loadedNamespace, map, outputDir, this.writerOptions),
                    Throws.Nothing);

                outputDir.Refresh();

                if (outputDir.Exists)
                {
                    var files = outputDir.GetFiles();
                    Assert.That(files, Has.Length.EqualTo(0), "No files should be written when namespace id is not found in the namespace tree");
                }
            }
            finally
            {
                if (outputDir.Exists)
                {
                    outputDir.Delete(true);
                }
            }
        }

        [Test]
        public void Verify_that_Serialize_writes_declaredName_attribute_when_set()
        {
            this.loadedNamespace.DeclaredName = "TestNamespaceName";

            var stream = new MemoryStream();
            this.serializer.Serialize(this.loadedNamespace, this.writerOptions, stream);

            stream.Seek(0, SeekOrigin.Begin);
            var content = new StreamReader(stream).ReadToEnd();

            Assert.That(content, Does.Contain("declaredName=\"TestNamespaceName\""));
        }

        [Test]
        public void Verify_that_Serialize_handles_null_writerOptions_by_defaulting()
        {
            var stream = new MemoryStream();

            Assert.That(() => this.serializer.Serialize(this.loadedNamespace, null, stream), Throws.Nothing);
            Assert.That(stream.Length, Is.GreaterThan(0));
        }
    }
}
