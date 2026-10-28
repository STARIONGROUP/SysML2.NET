// ------------------------------------------------------------------------------------------------
// <copyright file="ConstraintValueTestFixture.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Tests.PSM
{
    using System;
    using System.Globalization;

    using NUnit.Framework;

    using SysML2.NET.PSM.DTO;

    [TestFixture]
    public class ConstraintValueTestFixture
    {
        private static readonly Guid Identifier = Guid.Parse("2f1a0e1c-6b2a-4d5e-9c3f-8a7b6d5e4c3b");

        [Test]
        public void VerifyFrom()
        {
            using (Assert.EnterMultipleScope())
            {
                Assert.That(default(ConstraintValue).Kind, Is.EqualTo(ConstraintValueKind.Null));
                Assert.That(default(ConstraintValue).IsNull, Is.True);
                Assert.That(ConstraintValue.Null.Kind, Is.EqualTo(ConstraintValueKind.Null));

                Assert.That(ConstraintValue.From(true).Kind, Is.EqualTo(ConstraintValueKind.Boolean));
                Assert.That(ConstraintValue.From(1.5).Kind, Is.EqualTo(ConstraintValueKind.Number));
                Assert.That(ConstraintValue.From("text").Kind, Is.EqualTo(ConstraintValueKind.String));
                Assert.That(ConstraintValue.From(Identifier).Kind, Is.EqualTo(ConstraintValueKind.Guid));

                Assert.That(ConstraintValue.From((string)null).Kind, Is.EqualTo(ConstraintValueKind.Null));
                Assert.That(ConstraintValue.From((string)null).IsNull, Is.True);
                Assert.That(ConstraintValue.From(true).IsNull, Is.False);
            }

            ConstraintValue fromBoolean = true;
            ConstraintValue fromNumber = 1.5;
            ConstraintValue fromInteger = 3;
            ConstraintValue fromString = "text";
            ConstraintValue fromGuid = Identifier;

            using (Assert.EnterMultipleScope())
            {
                Assert.That(fromBoolean.Kind, Is.EqualTo(ConstraintValueKind.Boolean));
                Assert.That(fromNumber.Kind, Is.EqualTo(ConstraintValueKind.Number));
                Assert.That(fromInteger.Kind, Is.EqualTo(ConstraintValueKind.Number));
                Assert.That(fromString.Kind, Is.EqualTo(ConstraintValueKind.String));
                Assert.That(fromGuid.Kind, Is.EqualTo(ConstraintValueKind.Guid));
            }
        }

        [Test]
        public void VerifyTryGet()
        {
            var boolean = ConstraintValue.From(true);
            var number = ConstraintValue.From(1.5);
            var text = ConstraintValue.From("text");
            var identifier = ConstraintValue.From(Identifier);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(boolean.TryGetBoolean(out var booleanValue), Is.True);
                Assert.That(booleanValue, Is.True);
                Assert.That(number.TryGetNumber(out var numberValue), Is.True);
                Assert.That(numberValue, Is.EqualTo(1.5));
                Assert.That(text.TryGetString(out var stringValue), Is.True);
                Assert.That(stringValue, Is.EqualTo("text"));
                Assert.That(identifier.TryGetGuid(out var guidValue), Is.True);
                Assert.That(guidValue, Is.EqualTo(Identifier));
            }

            using (Assert.EnterMultipleScope())
            {
                Assert.That(number.TryGetBoolean(out _), Is.False);
                Assert.That(text.TryGetNumber(out _), Is.False);
                Assert.That(identifier.TryGetString(out _), Is.False);
                Assert.That(boolean.TryGetGuid(out _), Is.False);

                Assert.That(ConstraintValue.Null.TryGetBoolean(out _), Is.False);
                Assert.That(ConstraintValue.Null.TryGetNumber(out _), Is.False);
                Assert.That(ConstraintValue.Null.TryGetString(out var nullString), Is.False);
                Assert.That(nullString, Is.Null);
                Assert.That(ConstraintValue.Null.TryGetGuid(out _), Is.False);
            }
        }

        [Test]
        public void VerifyMatch()
        {
            Assert.That(() => ConstraintValue.Null.Match(null, _ => "b", _ => "n", _ => "s", _ => "g"), Throws.TypeOf<ArgumentNullException>());
            Assert.That(() => ConstraintValue.Null.Match(() => "x", null, _ => "n", _ => "s", _ => "g"), Throws.TypeOf<ArgumentNullException>());
            Assert.That(() => ConstraintValue.Null.Match(() => "x", _ => "b", null, _ => "s", _ => "g"), Throws.TypeOf<ArgumentNullException>());
            Assert.That(() => ConstraintValue.Null.Match(() => "x", _ => "b", _ => "n", null, _ => "g"), Throws.TypeOf<ArgumentNullException>());
            Assert.That(() => ConstraintValue.Null.Match(() => "x", _ => "b", _ => "n", _ => "s", null), Throws.TypeOf<ArgumentNullException>());

            static string Describe(ConstraintValue value) =>
                value.Match(
                    () => "null",
                    boolean => $"bool:{boolean}",
                    number => "number:" + number.ToString(CultureInfo.InvariantCulture),
                    text => $"string:{text}",
                    identifier => $"guid:{identifier:D}");

            using (Assert.EnterMultipleScope())
            {
                Assert.That(Describe(ConstraintValue.Null), Is.EqualTo("null"));
                Assert.That(Describe(ConstraintValue.From(true)), Is.EqualTo("bool:True"));
                Assert.That(Describe(ConstraintValue.From(1.5)), Is.EqualTo("number:1.5"));
                Assert.That(Describe(ConstraintValue.From("text")), Is.EqualTo("string:text"));
                Assert.That(Describe(ConstraintValue.From(Identifier)), Is.EqualTo($"guid:{Identifier:D}"));
            }
        }

        [Test]
        public void VerifyEquals()
        {
            using (Assert.EnterMultipleScope())
            {
                Assert.That(ConstraintValue.From("text"), Is.EqualTo(ConstraintValue.From("text")));
                Assert.That(ConstraintValue.From(1.5), Is.EqualTo(ConstraintValue.From(1.5)));
                Assert.That(ConstraintValue.Null, Is.EqualTo(default(ConstraintValue)));
                Assert.That(ConstraintValue.From(Identifier), Is.EqualTo(ConstraintValue.From(Identifier)));

                Assert.That(ConstraintValue.From("text"), Is.Not.EqualTo(ConstraintValue.From("other")));
                Assert.That(ConstraintValue.From(true), Is.Not.EqualTo(ConstraintValue.From(1.5)));
                Assert.That(ConstraintValue.From("text"), Is.Not.EqualTo(ConstraintValue.Null));
                Assert.That(ConstraintValue.From(Identifier).Equals("not a constraint value"), Is.False);
            }

            using (Assert.EnterMultipleScope())
            {
                Assert.That(ConstraintValue.From("text") == ConstraintValue.From("text"), Is.True);
                Assert.That(ConstraintValue.From("text") != ConstraintValue.From("other"), Is.True);
                Assert.That(ConstraintValue.From("text").GetHashCode(), Is.EqualTo(ConstraintValue.From("text").GetHashCode()));
                Assert.That(ConstraintValue.Null.GetHashCode(), Is.EqualTo(default(ConstraintValue).GetHashCode()));
            }
        }
    }
}
