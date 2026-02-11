// -------------------------------------------------------------------------------------------------
// <copyright file="ReaderTestFixture.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Kpar.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;
    
    using Microsoft.Extensions.Logging;
    
    using NUnit.Framework;

    using SysML2.NET.Kpar.Cryptography;
    using SysML2.NET.ModelInterchange;
    
    using Serilog;
    
    [TestFixture]
    public class ReaderTestFixture
    {
        private Reader reader;
        
        private ChecksumService checksumService;

        private ILoggerFactory loggerFactory;

        private static string GetKparPath()
        {
            return Path.Combine(TestContext.CurrentContext.TestDirectory, "TestData", "Kernel_Semantic_Library-1.0.0.kpar");
        }
        
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .CreateLogger();

            this.loggerFactory = LoggerFactory.Create(builder => { builder.AddSerilog(); });
        }
        
        [SetUp]
        public void SetUp()
        {
            this.checksumService = new ChecksumService();
            this.reader = new Reader(this.checksumService, this.loggerFactory.CreateLogger<Reader>());
        }

        private static IEnumerable<string> NullOrEmptyPaths()
        {
            yield return null;
            yield return string.Empty;
        }
        
        [Test]
        [TestCaseSource(nameof(NullOrEmptyPaths))]
        public void Verify_On_read_or_open_that_when_filepath_is_null_or_empty_argument_exception_is_thrown(string filePath)
        {
            Assert.That(() => this.reader.Read(filePath), Throws.ArgumentException);
            
            Assert.That(() => this.reader.Open(filePath), Throws.ArgumentException);
            
            Assert.That(async () => await this.reader.ReadAsync(filePath), Throws.ArgumentException);
            
            Assert.That(async () => await this.reader.OpenAsync(filePath), Throws.ArgumentException);
        }

        [Test]
        public void Verify_that_when_stream_is_null_ArgumentNullException_is_thrown()
        {
            Stream stream = null;
            
            Assert.That(() => this.reader.Read(stream), Throws.ArgumentNullException);
            
            Assert.That( async () => await this.reader.ReadAsync(stream), Throws.ArgumentNullException);
            
            Assert.That(() => this.reader.Open(stream), Throws.ArgumentNullException);
            
            Assert.That(async () => await this.reader.OpenAsync(stream), Throws.ArgumentNullException);
        }

        [Test]
        public void Verify_that_kpar_contents_can_be_read_from_path()
        {
            var kparPath = GetKparPath();
            
            var archive = this.reader.Read(kparPath);

            AssertArchive(archive, expectedPath: kparPath);
        }
        
        [Test]
        public void Verify_that_kpar_contents_can_be_read_from_stream()
        {
            var kparPath = GetKparPath();
            
            using var fileStream = File.OpenRead(kparPath);

            var archive = this.reader.Read(fileStream);

            AssertArchive(archive, expectedPath: null);
        }
        
        [Test]
        public async Task Verify_that_kpar_contents_can_be_read_async_from_path()
        {
            var kparPath = GetKparPath();

            var archive = await this.reader.ReadAsync(kparPath).ConfigureAwait(false);

            AssertArchive(archive, expectedPath: kparPath);
        }

        [Test]
        public async Task Verify_that_kpar_contents_can_be_read_async_from_stream()
        {
            var kparPath = GetKparPath();
            
            await using var fileStream = File.OpenRead(kparPath);

            var archive = await this.reader.ReadAsync(fileStream).ConfigureAwait(false);

            AssertArchive(archive, expectedPath: null);
        }
        
        [Test]
        public void Verify_that_kpar_can_be_opened_from_path_and_model_streams_can_be_opened()
        {
            var kparPath = GetKparPath();
            
            using var archiveSession = this.reader.Open(kparPath);

            AssertArchive(archiveSession.Archive, expectedPath: kparPath);

            using var modelStream = archiveSession.OpenModel("Base");
            Assert.That(modelStream, Is.Not.Null);
            Assert.That(modelStream.CanRead, Is.True);

            Assert.That(modelStream.Length, Is.GreaterThan(0));
            
            using var reader = new StreamReader(modelStream, Encoding.UTF8, detectEncodingFromByteOrderMarks: true);

            var content = reader.ReadToEnd();

            TestContext.WriteLine("---- Base.kerml content ----");
            TestContext.WriteLine(content);
            TestContext.WriteLine("---- End of content ----");
        }
        
        [Test]
        public async Task Verify_that_kpar_can_be_opened_async_from_path_and_model_streams_can_be_opened()
        {
            var kparPath = GetKparPath();

            await using var archiveSession = await this.reader.OpenAsync(kparPath).ConfigureAwait(false);

            AssertArchive(archiveSession.Archive, expectedPath: kparPath);

            await using var modelStream = archiveSession.OpenModel("Base");
            Assert.That(modelStream, Is.Not.Null);
            Assert.That(modelStream.CanRead, Is.True);
            Assert.That(modelStream.Length, Is.GreaterThan(0));
            
            using var reader = new StreamReader(modelStream, Encoding.UTF8, detectEncodingFromByteOrderMarks: true);

            var content = await reader.ReadToEndAsync();

            TestContext.WriteLine("---- Base.kerml content ----");
            TestContext.WriteLine(content);
            TestContext.WriteLine("---- End of content ----");
        }
        
        [Test]
        public void Verify_that_opened_entry_streams_become_invalid_after_session_dispose()
        {
            var kparPath = GetKparPath();

            var archiveSession = this.reader.Open(kparPath);

            var entryStream = archiveSession.OpenModel("Base");
            Assert.That(entryStream.CanRead, Is.True);

            archiveSession.Dispose();

            Assert.That(() => _ = entryStream.ReadByte(), Throws.Exception,
                "Reading from an entry stream after session disposal should fail.");
        }
        
        [Test]
        public async Task Verify_that_kpar_can_be_opened_async_from_stream_and_source_is_disposed_on_session_dispose()
        {
            var kparPath = GetKparPath();

            var source = File.OpenRead(kparPath);

            ArchiveSession archiveSession = null;

            try
            {
                archiveSession = await this.reader.OpenAsync(source).ConfigureAwait(false);

                AssertArchive(archiveSession.Archive, expectedPath: null);

                await using var modelStream = archiveSession.OpenModel("Base");
                Assert.That(modelStream.CanRead, Is.True);
                Assert.That(modelStream.Length, Is.GreaterThan(0));
            }
            finally
            {
                if (archiveSession is not null)
                {
                    await archiveSession.DisposeAsync().ConfigureAwait(false);
                }
            }

            Assert.That(source.CanRead, Is.False, "Source stream should be disposed .");
        }
        
        /// <summary>
        /// Centralized assertions for all read paths (file/stream, sync/async).
        /// </summary>
        private static void AssertArchive(Archive archive, string expectedPath)
        {
            Assert.That(archive, Is.Not.Null);

            if (expectedPath is null)
            {
                Assert.That(archive.Path, Is.Null);
            }
            else
            {
                Assert.That(archive.Path, Is.EqualTo(expectedPath));
            }

            // ---- META.JSON ASSERTIONS ----
            Assert.That(archive.Metadata, Is.Not.Null);

            Assert.That(archive.Metadata.Created, Is.EqualTo(DateTimeOffset.Parse("2025-03-13T00:00:00Z")));
            Assert.That(archive.Metadata.Metamodel, Is.EqualTo("https://www.omg.org/spec/KerML/20250201"));

            Assert.That(archive.Metadata.IncludesDerived, Is.False);
            Assert.That(archive.Metadata.IncludesImplied, Is.False);

            Assert.That(archive.Metadata.Index, Is.Not.Null);
            Assert.That(archive.Metadata.Index.Count, Is.EqualTo(16));

            Assert.Multiple(() =>
            {
                Assert.That(archive.Metadata.Index["Base"], Is.EqualTo("Base.kerml"));
                Assert.That(archive.Metadata.Index["Clocks"], Is.EqualTo("Clocks.kerml"));
                Assert.That(archive.Metadata.Index["ControlPerformances"], Is.EqualTo("ControlPerformances.kerml"));
                Assert.That(archive.Metadata.Index["FeatureReferencingPerformances"], Is.EqualTo("FeatureReferencingPerformances.kerml"));
                Assert.That(archive.Metadata.Index["KerML"], Is.EqualTo("KerML.kerml"));
                Assert.That(archive.Metadata.Index["Links"], Is.EqualTo("Links.kerml"));
                Assert.That(archive.Metadata.Index["Metaobjects"], Is.EqualTo("Metaobjects.kerml"));
                Assert.That(archive.Metadata.Index["Objects"], Is.EqualTo("Objects.kerml"));
                Assert.That(archive.Metadata.Index["Observation"], Is.EqualTo("Observation.kerml"));
                Assert.That(archive.Metadata.Index["Occurrences"], Is.EqualTo("Occurrences.kerml"));
                Assert.That(archive.Metadata.Index["Performances"], Is.EqualTo("Performances.kerml"));
                Assert.That(archive.Metadata.Index["SpatialFrames"], Is.EqualTo("SpatialFrames.kerml"));
                Assert.That(archive.Metadata.Index["StatePerformances"], Is.EqualTo("StatePerformances.kerml"));
                Assert.That(archive.Metadata.Index["Transfers"], Is.EqualTo("Transfers.kerml"));
                Assert.That(archive.Metadata.Index["TransitionPerformances"], Is.EqualTo("TransitionPerformances.kerml"));
                Assert.That(archive.Metadata.Index["Triggers"], Is.EqualTo("Triggers.kerml"));
            });

            Assert.That(archive.Metadata.Checksum, Is.Not.Null);
            Assert.That(archive.Metadata.Checksum.Count, Is.EqualTo(16));

            void AssertChecksum(string path, string expectedValue)
            {
                Assert.That(archive.Metadata.Checksum.ContainsKey(path), Is.True, $"Missing checksum for '{path}'");

                var entry = archive.Metadata.Checksum[path];

                Assert.That(entry, Is.Not.Null);
                Assert.That(entry.Algorithm, Is.EqualTo(ChecksumKind.SHA256));
                Assert.That(entry.Value, Is.EqualTo(expectedValue));
            }

            Assert.Multiple(() =>
            {
                AssertChecksum("Triggers.kerml", "124cad3625935e078d1363e6100ee12537ca9c51445a18108e056db8b4885609");
                AssertChecksum("ControlPerformances.kerml", "31385be7dca94bd0538f011d5c8f7925626d54f96970769f0fdb28b2186a9a03");
                AssertChecksum("Transfers.kerml", "fa40b483a7834d89f07aad0f6f57e79244adc2a58b4396c4734bddeb297d7c46");
                AssertChecksum("Objects.kerml", "9057e2781fe8793d5108973c0647318caa26310be6231c6380152a4cbc894c25");
                AssertChecksum("Metaobjects.kerml", "983dbd85a4b183d8859326ee512fc59d991fb98a115e009e72fad21d1f9d1685");
                AssertChecksum("Performances.kerml", "fd965e184b300737a192530de0c800cdbee236cb6220612f370400da21dfb327");
                AssertChecksum("StatePerformances.kerml", "f02fb7e8de58f4304c95c575ee1bcb7d271d621ce8e336ce36ea80a4e956c3da");
                AssertChecksum("Base.kerml", "56df84cda67f62c63d4e79e2786fc26046cfa361a958c4fcf0843d32a5707e09");
                AssertChecksum("Observation.kerml", "6bc57a73c43af6f61201b6eb659024a9f08f974643eb5a101e068e3637761ee4");
                AssertChecksum("TransitionPerformances.kerml", "1ce78437c817c8359a2cad43e8e72b23dd32b81d2a69dc1126c803fae72aae70");
                AssertChecksum("FeatureReferencingPerformances.kerml", "b6f9e5349c7c7f393591c0334c3bec86f1766b3e37209819179310c2f8fe1fb7");
                AssertChecksum("KerML.kerml", "8fdf4b7416e981c895cd74b75dc14b18091d13cbcaff7cdded6f9c23e2483d58");
                AssertChecksum("Occurrences.kerml", "b3a62ce0bc3a4f7e667102b4c2f68a4928ca8efeda425c6a4c8bdeadfbc9bbc1");
                AssertChecksum("Clocks.kerml", "960ac0884935e308beea55c78ed11b6946c37a386eb7958ef2c913aa275ae4c7");
                AssertChecksum("Links.kerml", "dcf3c002717cb91f8e16f1890fdf5526f4e178ada898a189621c7d0c24b5ddc0");
                AssertChecksum("SpatialFrames.kerml", "2a7790ebc2afacbd64eb781567906921e38eff385c917be03b090b8289353de7");
            });

            foreach (var (_, path) in archive.Metadata.Index)
            {
                Assert.That(
                    archive.Metadata.Checksum.ContainsKey(path),
                    Is.True,
                    $"Index path '{path}' should have a checksum entry");
            }

            // ---- PROJECT.JSON ASSERTIONS ----
            Assert.That(archive.Project, Is.Not.Null);

            Assert.That(archive.Project.Name, Is.EqualTo("Kernel Semantic Library"));
            Assert.That(archive.Project.Description, Is.EqualTo("Standard semantic library for the Kernel Modeling Language (KerML)"));
            Assert.That(archive.Project.Version, Is.EqualTo("1.0.0"));

            Assert.That(archive.Project.Usage, Is.Not.Null);
            Assert.That(archive.Project.Usage.Count, Is.EqualTo(2));

            Assert.Multiple(() =>
            {
                Assert.That(archive.Project.Usage[0].Resource, Is.EqualTo(new Uri("https://www.omg.org/spec/KerML/20250201/Data-Type-Library.kpar")));
                Assert.That(archive.Project.Usage[0].VersionConstraint, Is.EqualTo("1.0.0"));

                Assert.That(archive.Project.Usage[1].Resource, Is.EqualTo(new Uri("https://www.omg.org/spec/KerML/20250201/Function-Library.kpar")));
                Assert.That(archive.Project.Usage[1].VersionConstraint, Is.EqualTo("1.0.0"));
            });
        }
    }
}
