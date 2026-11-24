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
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
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

        [Test]
        public void SerializeAndDeSerialize_WithEveryPropertyPopulated_RoundTrips()
        {
            var dataItems = typeof(IElement).Assembly.GetTypes()
                .Where(type => type is { IsClass: true, IsAbstract: false, Namespace: not null } && type.Namespace.StartsWith("SysML2.NET.Core.DTO") && typeof(IElement).IsAssignableFrom(type))
                .Select((type, typeIndex) => CreatePopulatedDto(type, typeIndex))
                .ToList();

            Assert.That(dataItems, Has.Count.GreaterThan(150));

            foreach (var includeDerivedProperties in new[] { false, true })
            {
                var roundTripped = this.RoundTrip(dataItems, includeDerivedProperties, includeDerivedProperties).ToDictionary(x => x.Id);

                using (Assert.EnterMultipleScope())
                {
                    foreach (var original in dataItems)
                    {
                        Assert.That(this.WriteJson(roundTripped[original.Id], includeDerivedProperties), Is.EqualTo(this.WriteJson(original, includeDerivedProperties)),
                            $"Round-trip mismatch for {original.GetType().Name} with includeDerivedProperties={includeDerivedProperties}");
                    }
                }
            }
        }

        private static IData CreatePopulatedDto(Type type, int typeIndex)
        {
            var dto = (IData)Activator.CreateInstance(type);
            var seed = typeIndex * 10000;

            foreach (var property in type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).Where(x => x.SetMethod != null))
            {
                seed++;
                property.SetValue(dto, CreateFakeValue(property.PropertyType, seed));
            }

            return dto;
        }

        private static object CreateFakeValue(Type type, int seed)
        {
            var underlyingType = Nullable.GetUnderlyingType(type) ?? type;

            return underlyingType switch
            {
                _ when underlyingType == typeof(Guid) => CreateGuid(seed),
                _ when underlyingType == typeof(string) => $"value {seed}",
                _ when underlyingType == typeof(bool) => true,
                _ when underlyingType == typeof(int) => seed,
                _ when underlyingType == typeof(double) => seed + 0.5,
                _ when underlyingType.IsEnum => Enum.GetValues(underlyingType).GetValue(seed % Enum.GetValues(underlyingType).Length),
                _ when underlyingType == typeof(List<Guid>) => new List<Guid> { CreateGuid(seed), CreateGuid(seed + 1000) },
                _ when underlyingType == typeof(List<string>) => new List<string> { $"value {seed}", $"value {seed + 1000}" },
                _ => throw new NotSupportedException($"No fake value for {type.Name}")
            };
        }

        private static Guid CreateGuid(int seed)
        {
            return new Guid(seed, 0, 0, [1, 2, 3, 4, 5, 6, 7, 8]);
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
