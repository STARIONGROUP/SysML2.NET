// -------------------------------------------------------------------------------------------------
// <copyright file="AnnotatingElementMessagePackFormatterTestFixture.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Serializer.MessagePack.Tests.MessagePackFormatter
{
    using System;

    using global::MessagePack;
    using global::MessagePack.Resolvers;

    using NUnit.Framework;
    
    using SysML2.NET.Core.DTO.Root.Annotations;

    [TestFixture]
    public class AnnotatingElementMessagePackFormatterTestFixture
    {
        private MessagePackSerializerOptions options;

        [SetUp]
        public void SetUp()
        {
            // Configure MessagePack to use the formatter under test for AnnotatingElement.
            // This avoids relying on attribute-based formatters/resolvers.
            var resolver = CompositeResolver.Create(
                [
                    new SysML2.NET.Serializer.MessagePack.Core.AnnotatingElementMessagePackFormatter()
                ],
                [
                    StandardResolver.Instance // Fallback for primitive types, strings, arrays, etc.
                ]);

            this.options = MessagePackSerializerOptions.Standard.WithResolver(resolver);
        }

        [Test]
        public void Verify_that_AnnotatingElement_can_be_serialized_and_deserialized()
        {
            var original = CreateAnnotatingElement();

            var bytes = MessagePackSerializer.Serialize(original, this.options);
            var roundTripped = MessagePackSerializer.Deserialize<AnnotatingElement>(bytes, this.options);

            AssertThatAnnotatingElementEquivalent(original, roundTripped);
        }

        [Test]
        public void Verify_that_OwningRelationship_null_round_trips_as_null()
        {
            var original = CreateAnnotatingElement();
            original.OwningRelationship = null;

            var bytes = MessagePackSerializer.Serialize(original, this.options);
            var roundTripped = MessagePackSerializer.Deserialize<AnnotatingElement>(bytes, this.options);

            Assert.That(roundTripped, Is.Not.Null);
            Assert.That(roundTripped.OwningRelationship, Is.Null);
            Assert.That(roundTripped.Id, Is.EqualTo(original.Id));
        }

        private static AnnotatingElement CreateAnnotatingElement()
        {
            return new AnnotatingElement
            {
                Id = Guid.Parse("11111111-2222-3333-4444-555555555555"),
                AliasIds = ["ALIAS-1", "ALIAS-2"],
                DeclaredName = "My Annotating Element",
                DeclaredShortName = "MyAE",
                ElementId = "ELEMENT-ID-123",
                IsImpliedIncluded = true,

                OwnedRelationship =
                [
                    Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                    Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                    Guid.Parse("00000000-0000-0000-0000-000000000010")
                ],

                OwningRelationship = Guid.Parse("99999999-8888-7777-6666-555555555555"),
            };
        }

        private static void AssertThatAnnotatingElementEquivalent(AnnotatingElement expected, AnnotatingElement actual)
        {
            Assert.That(actual, Is.Not.Null);

            Assert.That(actual.Id, Is.EqualTo(expected.Id));
            Assert.That(actual.DeclaredName, Is.EqualTo(expected.DeclaredName));
            Assert.That(actual.DeclaredShortName, Is.EqualTo(expected.DeclaredShortName));
            Assert.That(actual.ElementId, Is.EqualTo(expected.ElementId));
            Assert.That(actual.IsImpliedIncluded, Is.EqualTo(expected.IsImpliedIncluded));
            Assert.That(actual.OwningRelationship, Is.EqualTo(expected.OwningRelationship));

            Assert.That(actual.AliasIds, Is.EqualTo(expected.AliasIds));

            Assert.That(actual.OwnedRelationship, Is.EquivalentTo(expected.OwnedRelationship));
        }
    }
}
