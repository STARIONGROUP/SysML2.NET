// ------------------------------------------------------------------------------------------------
// <copyright file="SerializerTestFixture.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Serializer.MessagePack.Tests
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Text.Json;

    using NUnit.Framework;

    using SysML2.NET.Common;
    using SysML2.NET.Core.DTO.Root.Elements;
    using SysML2.NET.Serializer.Json;
    using SysML2.NET.Extensions.Core.DTO.Comparers;

    using SerializationTargetKind = MessagePack.SerializationTargetKind;

    [TestFixture]
    public class SerializerTestFixture
    {
        private Json.DeSerializer jsonDeSerializer;

        private Json.Serializer jsonSerializer;

        private MessagePack.DeSerializer messagePackDeSerializer;

        private MessagePack.Serializer messagePackSerializer;

        [SetUp]
        public void SetUp()
        {
            this.jsonDeSerializer = new Json.DeSerializer();
            this.jsonSerializer = new Json.Serializer();
            this.messagePackDeSerializer = new MessagePack.DeSerializer();
            this.messagePackSerializer = new MessagePack.Serializer();
        }

        [Test]
        public void VerifySerializeAndDeSerialize()
        {
            var jsonData = this.ReadJson(false);

            Assert.That(jsonData, Has.Count.EqualTo(100));

            var messagePackData = this.RoundTrip(jsonData, false, false);

            Assert.That(messagePackData, Has.Count.EqualTo(jsonData.Count));

            var jsonById = jsonData.ToDictionary(x => x.Id);
            var messagePackById = messagePackData.ToDictionary(x => x.Id);

            Assert.That(messagePackById.Keys, Is.EquivalentTo(jsonById.Keys));

            using (Assert.EnterMultipleScope())
            {
                foreach (var id in jsonById.Keys)
                {
                    var left = jsonById[id];
                    var right = messagePackById[id];

                    Assert.That(right.GetType(), Is.EqualTo(left.GetType()), $"Type mismatch for id {id}");
                    Assert.That(ComparerProvider.Resolve(left).Equals(left, right), Is.True, $"Round-trip semantic mismatch for id {id} ({left.GetType().Name})");
                    Assert.That(this.WriteJson(right, false), Is.EqualTo(this.WriteJson(left, false)), $"Round-trip mismatch without derived properties for id {id}");
                }
            }

            var jsonDataWithDerived = this.ReadJson(true);
            var jsonWithDerivedById = jsonDataWithDerived.ToDictionary(x => x.Id);
            var messagePackWithDerivedById = this.RoundTrip(jsonDataWithDerived, true, true).ToDictionary(x => x.Id);

            using (Assert.EnterMultipleScope())
            {
                foreach (var id in jsonWithDerivedById.Keys)
                {
                    Assert.That(this.WriteJson(messagePackWithDerivedById[id], true), Is.EqualTo(this.WriteJson(jsonWithDerivedById[id], true)), $"Round-trip mismatch with derived properties for id {id}");
                }
            }

            var derivedSkipped = this.RoundTrip(jsonDataWithDerived, true, false);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(jsonDataWithDerived.OfType<IElement>().Count(x => x.qualifiedName != null), Is.GreaterThan(0));
                Assert.That(derivedSkipped.OfType<IElement>().Where(x => x.qualifiedName != null), Is.Empty);

                foreach (var skipped in derivedSkipped)
                {
                    Assert.That(this.WriteJson(skipped, false), Is.EqualTo(this.WriteJson(jsonWithDerivedById[skipped.Id], false)), $"Stored properties lost while skipping derived properties for id {skipped.Id}");
                }
            }
        }

        private List<IData> ReadJson(bool deserializeDerivedProperties)
        {
            var fileName = Path.Combine(TestContext.CurrentContext.WorkDirectory, "Data", "projects.000e9890-6935-43e6-a5d7-5d7cac601f4c.commits.6d7ad9fd-6520-4ff2-885b-8c5c129e6c27.elements.json");
            using var jsonStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);

            return this.jsonDeSerializer.DeSerialize(jsonStream, SerializationModeKind.JSON, Json.SerializationTargetKind.CORE, deserializeDerivedProperties).Cast<IData>().ToList();
        }

        private List<IData> RoundTrip(List<IData> dataItems, bool includeDerivedProperties, bool deserializeDerivedProperties)
        {
            using var messagePackStream = new MemoryStream();

            this.messagePackSerializer.Serialize(dataItems, includeDerivedProperties, messagePackStream);
            messagePackStream.Position = 0;

            return this.messagePackDeSerializer.DeSerialize(messagePackStream, SerializationTargetKind.CORE, deserializeDerivedProperties).ToList();
        }

        private string WriteJson(IData dataItem, bool includeDerivedProperties)
        {
            using var jsonStream = new MemoryStream();

            this.jsonSerializer.Serialize((IIdentified)dataItem, SerializationModeKind.JSON, includeDerivedProperties, jsonStream, new JsonWriterOptions());

            return Encoding.UTF8.GetString(jsonStream.ToArray());
        }
    }
}
