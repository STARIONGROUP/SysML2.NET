// ------------------------------------------------------------------------------------------------
// <copyright file="OpenApiContentTestFixture.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SySML2.NET.REST.Tests.HelpersTests
{
    using System.Collections.Generic;
    using System.Linq;

    using Json.Schema;

    using NUnit.Framework;

    using SySML2.NET.REST.Tests.Helpers;

    [TestFixture]
    public class OpenApiContentTestFixture
    {
        private const string MetaclassSchema = """
            {
              "anyOf": [
                { "type": "object", "properties": { "@type": { "const": "A" }, "a": { "type": "string" } }, "required": ["@type", "a"] },
                { "type": "object", "properties": { "@type": { "const": "B" }, "b": { "type": "string" }, "id": { "type": "string", "format": "uuid" }, "note": { "oneOf": [{ "type": "string" }, { "type": "null" }] } }, "required": ["@type", "b"] }
              ]
            }
            """;

        [Test]
        public void VerifyCheck()
        {
            Assert.That(() => new OpenApiContent(null), Throws.ArgumentNullException);

            var none = new OpenApiContent(new Dictionary<string, JsonSchema>());
            var anything = new OpenApiContent(new Dictionary<string, JsonSchema> { ["*/*"] = JsonSchema.FromText("{\"type\":\"null\"}") });
            var anyApplication = new OpenApiContent(new Dictionary<string, JsonSchema> { ["application/*"] = JsonSchema.FromText("true") });
            var metaclasses = new OpenApiContent(new Dictionary<string, JsonSchema> { ["application/json"] = JsonSchema.FromText(MetaclassSchema) });

            using (Assert.EnterMultipleScope())
            {
                Assert.That(none.Check(null, string.Empty), Is.Empty);
                Assert.That(Kinds(none.Check("application/json", "{}")), Is.EqualTo((ContractViolationKind[])[ContractViolationKind.UnexpectedBody]));
                Assert.That(anything.Check(null, string.Empty), Is.Empty);
                Assert.That(anything.Check("text/plain", "ignored"), Is.Empty);
                Assert.That(anyApplication.Check("application/xml", "<a/>"), Is.Empty);
                Assert.That(Kinds(anyApplication.Check("text/plain", "a")), Is.EqualTo((ContractViolationKind[])[ContractViolationKind.UndeclaredMediaType]));
                Assert.That(Kinds(metaclasses.Check(null, string.Empty)), Is.EqualTo((ContractViolationKind[])[ContractViolationKind.UnexpectedBody]));
                Assert.That(Kinds(metaclasses.Check("application/ld+json", "{}")), Is.EqualTo((ContractViolationKind[])[ContractViolationKind.UndeclaredMediaType]));
                Assert.That(Kinds(metaclasses.Check("application/json", "{")), Is.EqualTo((ContractViolationKind[])[ContractViolationKind.InvalidJson]));
                Assert.That(metaclasses.Check("APPLICATION/JSON", "{\"@type\":\"A\",\"a\":\"x\"}"), Is.Empty);
                Assert.That(metaclasses.Check("application/json", "{\"@type\":\"B\",\"b\":\"x\",\"note\":null,\"id\":\"3fa85f64-5717-4562-b3fc-2c963f66afa6\"}"), Is.Empty);
            }

            var missingInB = metaclasses.Check("application/json", "{\"@type\":\"B\"}");
            var unknownType = metaclasses.Check("application/json", "{\"@type\":\"C\"}");
            var badNote = metaclasses.Check("application/json", "{\"@type\":\"B\",\"b\":\"x\",\"note\":5}");
            var badId = metaclasses.Check("application/json", "{\"@type\":\"B\",\"b\":\"x\",\"id\":\"nope\"}");

            using (Assert.EnterMultipleScope())
            {
                Assert.That(Kinds(missingInB), Is.EqualTo((ContractViolationKind[])[ContractViolationKind.SchemaMismatch]));
                Assert.That(missingInB[0].Message, Does.Contain("\"b\"").And.Not.Contain("\"a\""));
                Assert.That(unknownType[0].Message, Does.Contain("@type const"));
                Assert.That(badNote[0].Message, Does.Contain("/note"));
                Assert.That(badId[0].Message, Does.Contain("/id format"));
                Assert.That(missingInB[0].ToString(), Does.StartWith("SchemaMismatch: "));
            }
        }

        private static IReadOnlyList<ContractViolationKind> Kinds(IReadOnlyList<ContractViolation> violations) => violations.Select(violation => violation.Kind).ToList();
    }
}
