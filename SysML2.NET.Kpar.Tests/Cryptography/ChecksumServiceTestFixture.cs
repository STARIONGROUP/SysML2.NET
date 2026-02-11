// -------------------------------------------------------------------------------------------------
// <copyright file="ChecksumServiceTestFixture.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Kpar.Tests.Cryptography
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.IO.Compression;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Logging;

    using NUnit.Framework;

    using Serilog;

    using SysML2.NET.Kpar.Cryptography;
    using SysML2.NET.ModelInterchange;

    [TestFixture]
    public class ChecksumServiceTestFixture
    {
        private ChecksumService checksumService;
        
        private Reader reader;

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
            this.checksumService = new ChecksumService();
        }

        [Test]
        public void Verify_that_checksums_can_be_validated_for_known_kpar()
        {
            var kparPath = GetKparPath();
            Assert.That(File.Exists(kparPath), Is.True, $"KPAR test file not found: {kparPath}");

            var archive = this.reader.Read(kparPath);
            Assert.That(archive?.Metadata, Is.Not.Null);

            using var zip = ZipFile.OpenRead(kparPath);

            var mismatches = this.checksumService.Validate(zip, archive.Metadata);

            Assert.That(mismatches, Is.Not.Null);
            Assert.That(mismatches.Count, Is.EqualTo(0));
        }

        [Test]
        public async Task Verify_that_checksums_can_be_validated_async_for_known_kpar()
        {
            var kparPath = GetKparPath();
            Assert.That(File.Exists(kparPath), Is.True, $"KPAR test file not found: {kparPath}");

            var archive = await this.reader.ReadAsync(kparPath).ConfigureAwait(false);
            Assert.That(archive?.Metadata, Is.Not.Null);

            using var zip = ZipFile.OpenRead(kparPath);

            var mismatches = await this.checksumService.ValidateAsync(zip, archive.Metadata, cancellationToken: CancellationToken.None)
                .ConfigureAwait(false);

            Assert.That(mismatches, Is.Not.Null);
            Assert.That(mismatches.Count, Is.EqualTo(0));
        }

        [Test]
        public void Verify_that_checksum_mismatch_throws_by_default()
        {
            var kparPath = GetKparPath();
            Assert.That(File.Exists(kparPath), Is.True, $"KPAR test file not found: {kparPath}");

            var archive = this.reader.Read(kparPath);
            var tampered = CloneMetadataWithSingleTamperedChecksumValue(archive.Metadata);

            using var zip = ZipFile.OpenRead(kparPath);

            var ex = Assert.Throws<ChecksumValidationException>(() => this.checksumService.Validate(zip, tampered));
            Assert.That(ex, Is.Not.Null);
            Assert.That(ex!.Mismatches, Is.Not.Null);
            Assert.That(ex.Mismatches.Count, Is.EqualTo(1));

            var m = ex.Mismatches[0];
            Assert.That(m.Path, Is.Not.Null.And.Not.Empty);
            Assert.That(m.Expected, Is.Not.Null.And.Not.Empty);
            Assert.That(m.Actual, Is.Not.Null.And.Not.Empty);
            Assert.That(string.Equals(m.Expected, m.Actual, StringComparison.OrdinalIgnoreCase), Is.False);
        }

        [Test]
        public async Task Verify_that_checksum_mismatch_can_be_returned_when_behavior_is_return_mismatches()
        {
            var kparPath = GetKparPath();
            Assert.That(File.Exists(kparPath), Is.True, $"KPAR test file not found: {kparPath}");

            var archive = await this.reader.ReadAsync(kparPath).ConfigureAwait(false);
            var tampered = CloneMetadataWithSingleTamperedChecksumValue(archive.Metadata);

            using var zip = ZipFile.OpenRead(kparPath);

            var mismatches = await this.checksumService.ValidateAsync(
                    zip,
                    tampered,
                    behavior: ChecksumFailureBehavior.Collect,
                    cancellationToken: CancellationToken.None)
                .ConfigureAwait(false);

            Assert.That(mismatches, Is.Not.Null);
            Assert.That(mismatches.Count, Is.EqualTo(1));

            var m = mismatches[0];
            Assert.That(m.Path, Is.Not.Null.And.Not.Empty);
            Assert.That(m.Expected, Is.Not.Null.And.Not.Empty);
            Assert.That(m.Actual, Is.Not.Null.And.Not.Empty);
            Assert.That(string.Equals(m.Expected, m.Actual, StringComparison.OrdinalIgnoreCase), Is.False);
        }

        [Test]
        public void Verify_that_missing_zip_entry_throws_filenotfound()
        {
            var kparPath = GetKparPath();
            Assert.That(File.Exists(kparPath), Is.True, $"KPAR test file not found: {kparPath}");

            var archive = this.reader.Read(kparPath);
            var tampered = CloneMetadataWithSingleMissingPath(archive.Metadata);

            using var zip = ZipFile.OpenRead(kparPath);

            Assert.Throws<FileNotFoundException>(() => this.checksumService.Validate(zip, tampered));
        }

        private static InterchangeProjectMetadata CloneMetadataWithSingleTamperedChecksumValue(InterchangeProjectMetadata original)
        {
            ArgumentNullException.ThrowIfNull(original);
            
            if (original.Checksum is null || original.Checksum.Count == 0)
            {
                throw new InvalidOperationException("Test requires non-empty metadata.Checksum.");
            }

            // Shallow clone is sufficient for the test; we only mutate one checksum value.
            var clone = new InterchangeProjectMetadata
            {
                Created = original.Created,
                Metamodel = original.Metamodel,
                IncludesDerived = original.IncludesDerived,
                IncludesImplied = original.IncludesImplied,
                Index = original.Index is null ? null : new Dictionary<string, string>(original.Index, StringComparer.Ordinal),
                Checksum = new Dictionary<string, InterchangeChecksum>(StringComparer.Ordinal)
            };

            foreach (var kvp in original.Checksum)
            {
                clone.Checksum[kvp.Key] = new InterchangeChecksum
                {
                    Algorithm = kvp.Value.Algorithm,
                    Value = kvp.Value.Value
                };
            }

            var firstKey = original.Checksum.Keys.First();
            var current = clone.Checksum[firstKey];

            // Flip a nibble deterministically.
            var value = current.Value?.Trim();
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new InvalidOperationException("Test requires non-empty checksum value.");
            }

            var chars = value.ToCharArray();
            chars[0] = chars[0] == '0' ? '1' : '0';
            current.Value = new string(chars);

            return clone;
        }

        private static InterchangeProjectMetadata CloneMetadataWithSingleMissingPath(InterchangeProjectMetadata original)
        {
            ArgumentNullException.ThrowIfNull(original);
            
            if (original.Checksum is null || original.Checksum.Count == 0)
            {
                throw new InvalidOperationException("Test requires non-empty metadata.Checksum.");
            }

            var clone = new InterchangeProjectMetadata
            {
                Created = original.Created,
                Metamodel = original.Metamodel,
                IncludesDerived = original.IncludesDerived,
                IncludesImplied = original.IncludesImplied,
                Index = original.Index is null ? null : new Dictionary<string, string>(original.Index, StringComparer.Ordinal),
                Checksum = new Dictionary<string, InterchangeChecksum>(StringComparer.Ordinal)
            };

            foreach (var kvp in original.Checksum)
            {
                clone.Checksum[kvp.Key] = new InterchangeChecksum
                {
                    Algorithm = kvp.Value.Algorithm,
                    Value = kvp.Value.Value
                };
            }

            var firstKey = original.Checksum.Keys.First();
            clone.Checksum.Remove(firstKey);
            clone.Checksum["DoesNotExist.kerml"] = new InterchangeChecksum
            {
                Algorithm = ChecksumKind.SHA256,
                Value = "00"
            };

            return clone;
        }
    }
}
