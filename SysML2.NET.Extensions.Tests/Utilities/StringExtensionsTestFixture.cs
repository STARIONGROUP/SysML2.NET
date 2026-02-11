// -------------------------------------------------------------------------------------------------
// <copyright file="StringExtensionsTestFixture.cs" company="Starion Group S.A.">
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
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Extensions.Utilities.Tests
{
    using System;

    using NUnit.Framework;

    using SysML2.NET.Extensions.Utilities;

    [TestFixture]
    public sealed class StringExtensionsTestFixture
    {
        [Test]
        public void Verify_that_NormalizeZipPath_throws_when_path_is_null()
        {
            string path = null;

            Assert.That(() => path.NormalizeZipPath(), Throws.ArgumentNullException);
        }

        [Test]
        public void Verify_that_NormalizeZipPath_returns_empty_when_path_is_empty()
        {
            var result = string.Empty.NormalizeZipPath();

            Assert.That(result, Is.EqualTo(string.Empty));
        }

        [Test]
        public void Verify_that_NormalizeZipPath_replaces_backslashes_with_forward_slashes()
        {
            var input = @"a\b\c.txt";

            var result = input.NormalizeZipPath();

            Assert.That(result, Is.EqualTo("a/b/c.txt"));
        }

        [Test]
        public void Verify_that_NormalizeZipPath_removes_single_leading_dot_slash()
        {
            var input = "./Base.kerml";

            var result = input.NormalizeZipPath();

            Assert.That(result, Is.EqualTo("Base.kerml"));
        }

        [Test]
        public void Verify_that_NormalizeZipPath_removes_multiple_leading_dot_slash_segments()
        {
            var input = "./././folder/Base.kerml";

            var result = input.NormalizeZipPath();

            Assert.That(result, Is.EqualTo("folder/Base.kerml"));
        }

        [Test]
        public void Verify_that_NormalizeZipPath_removes_leading_dot_slash_after_backslash_replacement()
        {
            var input = @".\.\folder\Base.kerml";

            var result = input.NormalizeZipPath();

            Assert.That(result, Is.EqualTo("folder/Base.kerml"));
        }

        [Test]
        public void Verify_that_NormalizeZipPath_does_not_remove_dot_slash_in_middle_of_path()
        {
            var input = "folder/./Base.kerml";

            var result = input.NormalizeZipPath();

            Assert.That(result, Is.EqualTo("folder/./Base.kerml"));
        }

        [Test]
        public void Verify_that_NormalizeZipPath_does_not_trim_or_change_other_characters()
        {
            var input = "  ./a/b  ";

            var result = input.NormalizeZipPath();

            // Only "./" prefix is removed; whitespace is preserved by design.
            Assert.That(result, Is.EqualTo("  ./a/b  "));
        }

        [Test]
        public void Verify_that_NormalizeZipPath_is_idempotent()
        {
            var input = "./a\\b/c.kerml";

            var once = input.NormalizeZipPath();
            var twice = once.NormalizeZipPath();

            Assert.That(twice, Is.EqualTo(once));
        }
    }
}
