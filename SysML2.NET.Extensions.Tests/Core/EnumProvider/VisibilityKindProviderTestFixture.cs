// -------------------------------------------------------------------------------------------------
// <copyright file="VisibilityKindProviderTestFixture.cs" company="Starion Group S.A.">
// 
//   Copyright 2022-2025 Starion Group S.A.
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

namespace SysML2.NET.Extensions.Tests.Core.EnumProvider
{
    using System;

    using SysML2.NET.Core;
    using SysML2.NET.Extensions.Core;

    using NUnit.Framework;

    [TestFixture]
    public class VisibilityKindProviderTestFixture
    {
        [Test]
        [TestCase("Private", VisibilityKind.Private)]
        [TestCase("Protected", VisibilityKind.Protected)]
        [TestCase("Public", VisibilityKind.Public)]
        public void Verify_that_enumvalue_can_be_parsed_from_string(string input, VisibilityKind expected)
        {
            Assert.That(VisibilityKindProvider.Parse(input), 
                Is.EqualTo(expected));
        }

        [Test]
        public void Verify_that_enumvalue_throws_exception_from_unknown_string()
        {
            Assert.That(() => VisibilityKindProvider.Parse("Starion"), Throws.ArgumentException);
        }

        [Test]
        [TestCase("private", VisibilityKind.Private)]
        [TestCase("protected", VisibilityKind.Protected)]
        [TestCase("public", VisibilityKind.Public)]
        public void Verify_that_enumvalue_can_be_parsed_from_bytes(string input, VisibilityKind expected)
        {
            ReadOnlySpan<byte> value = System.Text.Encoding.UTF8.GetBytes(input);

            Assert.That(VisibilityKindProvider.Parse(value),
                Is.EqualTo(expected));
        }

        [Test]
        public void Verify_that_enumvalue_throws_exception_from_unknown_bytes()
        {
            Assert.That(() => VisibilityKindProvider.Parse("Starion"u8), Throws.ArgumentException);
        }
    }
}
