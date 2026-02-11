// -------------------------------------------------------------------------------------------------
// <copyright file="ArchiveExtensionsTestFixture.cs" company="Starion Group S.A.">
//
//   Copyright 2022-2026 Starion Group S.A.
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//        http://www.apache.org/licenses/LICENSE-2.0
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.
//
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace SysML2.NET.ModelInterchange.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    using NUnit.Framework;

    using SysML2.NET.Extensions.ModelInterchange;

    [TestFixture]
    public class ArchiveExtensionsTestFixture
    {
        [Test]
        public void Verify_that_TryGetModelEntryByIndexKey_throws_when_archive_is_null()
        {
            Assert.That(() =>
            {
                ArchiveExtensions.TryGetModelEntryByIndexKey(null, "Base", out _);
            }, Throws.ArgumentNullException);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("   ")]
        public void Verify_that_TryGetModelEntryByIndexKey_throws_when_indexKey_is_invalid(string indexKey)
        {
            var archive = new Archive
            {
                Metadata = new InterchangeProjectMetadata { Index = new Dictionary<string, string>() },
                Models = Array.Empty<ModelEntry>()
            };

            Assert.That(() =>
            {
                archive.TryGetModelEntryByIndexKey(indexKey, out _);
            }, Throws.ArgumentException);
        }

        [Test]
        public void Verify_that_TryGetModelEntryByIndexKey_returns_false_when_metadata_index_is_missing()
        {
            var archive = new Archive
            {
                Metadata = new InterchangeProjectMetadata { Index = null },
                Models = Array.Empty<ModelEntry>()
            };

            var ok = archive.TryGetModelEntryByIndexKey("Base", out var entry);

            Assert.That(ok, Is.False);
            Assert.That(entry, Is.Null);
        }

        [Test]
        public void Verify_that_TryGetModelEntryByIndexKey_returns_false_when_key_is_missing()
        {
            var archive = new Archive
            {
                Metadata = new InterchangeProjectMetadata
                {
                    Index = new Dictionary<string, string>(StringComparer.Ordinal)
                    {
                        ["Other"] = "Other.kerml"
                    }
                },
                Models = new[]
                {
                    CreateModelEntry("Other.kerml")
                }
            };

            var ok = archive.TryGetModelEntryByIndexKey("Base", out var entry);

            Assert.That(ok, Is.False);
            Assert.That(entry, Is.Null);
        }

        [Test]
        public void Verify_that_TryGetModelEntryByIndexKey_returns_false_when_path_is_null_or_whitespace()
        {
            var archive = new Archive
            {
                Metadata = new InterchangeProjectMetadata
                {
                    Index = new Dictionary<string, string>(StringComparer.Ordinal)
                    {
                        ["Base"] = "   "
                    }
                },
                Models = new[]
                {
                    CreateModelEntry("Base.kerml")
                }
            };

            var ok = archive.TryGetModelEntryByIndexKey("Base", out var entry);

            Assert.That(ok, Is.False);
            Assert.That(entry, Is.Null);
        }

        [Test]
        public void Verify_that_TryGetModelEntryByIndexKey_returns_false_when_model_is_not_present()
        {
            var archive = new Archive
            {
                Metadata = new InterchangeProjectMetadata
                {
                    Index = new Dictionary<string, string>(StringComparer.Ordinal)
                    {
                        ["Base"] = "Base.kerml"
                    }
                },
                Models = new[]
                {
                    CreateModelEntry("Other.kerml")
                }
            };

            var ok = archive.TryGetModelEntryByIndexKey("Base", out var entry);

            Assert.That(ok, Is.False);
            Assert.That(entry, Is.Null);
        }

        [Test]
        public void Verify_that_TryGetModelEntryByIndexKey_returns_true_and_sets_entry_when_found()
        {
            var expected = CreateModelEntry("Base.kerml");

            var archive = new Archive
            {
                Metadata = new InterchangeProjectMetadata
                {
                    Index = new Dictionary<string, string>(StringComparer.Ordinal)
                    {
                        ["Base"] = "Base.kerml"
                    }
                },
                Models = new[]
                {
                    expected,
                    CreateModelEntry("Other.kerml")
                }
            };

            var ok = archive.TryGetModelEntryByIndexKey("Base", out var entry);

            Assert.That(ok, Is.True);
            Assert.That(entry, Is.SameAs(expected));
        }

        [Test]
        public void Verify_that_TryGetModelEntryByIndexKey_matches_using_normalized_paths()
        {
            // Index uses backslashes; model entry uses forward slashes.
            var expected = CreateModelEntry("folder/Base.kerml");

            var archive = new Archive
            {
                Metadata = new InterchangeProjectMetadata
                {
                    Index = new Dictionary<string, string>(StringComparer.Ordinal)
                    {
                        ["Base"] = @"folder\Base.kerml"
                    }
                },
                Models = new[]
                {
                    expected
                }
            };

            var ok = archive.TryGetModelEntryByIndexKey("Base", out var entry);

            Assert.That(ok, Is.True);
            Assert.That(entry, Is.SameAs(expected));
        }

        [Test]
        public void Verify_that_GetModelEntryByIndexKey_throws_when_archive_is_null()
        {
            Assert.That(() => ArchiveExtensions.GetModelEntryByIndexKey(null, "Base"), Throws.ArgumentNullException);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("   ")]
        public void Verify_that_GetModelEntryByIndexKey_throws_when_indexKey_is_invalid(string indexKey)
        {
            var archive = new Archive
            {
                Metadata = new InterchangeProjectMetadata { Index = new Dictionary<string, string>() },
                Models = Array.Empty<ModelEntry>()
            };

            Assert.That(() => archive.GetModelEntryByIndexKey(indexKey), Throws.ArgumentException);
        }

        [Test]
        public void Verify_that_GetModelEntryByIndexKey_throws_when_metadata_index_is_not_available()
        {
            var archive = new Archive
            {
                Metadata = null,
                Models = Array.Empty<ModelEntry>()
            };

            Assert.That(() => archive.GetModelEntryByIndexKey("Base"), Throws.InvalidOperationException);
        }

        [Test]
        public void Verify_that_GetModelEntryByIndexKey_throws_when_key_is_not_found()
        {
            var archive = new Archive
            {
                Metadata = new InterchangeProjectMetadata
                {
                    Index = new Dictionary<string, string>(StringComparer.Ordinal)
                    {
                        ["Other"] = "Other.kerml"
                    }
                },
                Models = new[] { CreateModelEntry("Other.kerml") }
            };

            Assert.That(() => archive.GetModelEntryByIndexKey("Base"), Throws.TypeOf<KeyNotFoundException>());
        }

        [Test]
        public void Verify_that_GetModelEntryByIndexKey_throws_when_index_entry_path_is_null_or_whitespace()
        {
            var archive = new Archive
            {
                Metadata = new InterchangeProjectMetadata
                {
                    Index = new Dictionary<string, string>(StringComparer.Ordinal)
                    {
                        ["Base"] = " "
                    }
                },
                Models = new[] { CreateModelEntry("Base.kerml") }
            };

            Assert.That(() => archive.GetModelEntryByIndexKey("Base"), Throws.TypeOf<KeyNotFoundException>());
        }

        [Test]
        public void Verify_that_GetModelEntryByIndexKey_throws_when_model_entry_is_missing()
        {
            var archive = new Archive
            {
                Metadata = new InterchangeProjectMetadata
                {
                    Index = new Dictionary<string, string>(StringComparer.Ordinal)
                    {
                        ["Base"] = "Base.kerml"
                    }
                },
                Models = new[] { CreateModelEntry("Other.kerml") }
            };

            Assert.That(() => archive.GetModelEntryByIndexKey("Base"), Throws.TypeOf<FileNotFoundException>());
        }

        [Test]
        public void Verify_that_GetModelEntryByIndexKey_returns_entry_when_found()
        {
            var expected = CreateModelEntry("Base.kerml");

            var archive = new Archive
            {
                Metadata = new InterchangeProjectMetadata
                {
                    Index = new Dictionary<string, string>(StringComparer.Ordinal)
                    {
                        ["Base"] = "Base.kerml"
                    }
                },
                Models = new[] { expected }
            };

            var entry = archive.GetModelEntryByIndexKey("Base");

            Assert.That(entry, Is.SameAs(expected));
        }

        [Test]
        public async Task Verify_that_OpenModelByIndexKeyAsync_opens_stream_from_entry()
        {
            var expectedBytes = Encoding.UTF8.GetBytes("hello kerml");

            var entry = new ModelEntry
            {
                Path = "Base.kerml",
                ContentType = "text/plain",
                OpenReadAsync = _ => new ValueTask<Stream>(new MemoryStream(expectedBytes, writable: false))
            };

            var archive = new Archive
            {
                Metadata = new InterchangeProjectMetadata
                {
                    Index = new Dictionary<string, string>(StringComparer.Ordinal)
                    {
                        ["Base"] = "Base.kerml"
                    }
                },
                Models = new[] { entry }
            };

            await using var stream = await archive.OpenModelByIndexKeyAsync("Base", CancellationToken.None);

            Assert.That(stream, Is.Not.Null);
            using var ms = new MemoryStream();
            await stream.CopyToAsync(ms);
            Assert.That(ms.ToArray(), Is.EqualTo(expectedBytes));
        }

        private static ModelEntry CreateModelEntry(string path)
        {
            return new ModelEntry
            {
                Path = path,
                ContentType = "text/plain",
                OpenReadAsync = _ => new ValueTask<Stream>(new MemoryStream(Array.Empty<byte>(), writable: false))
            };
        }
    }
}
