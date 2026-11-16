// ------------------------------------------------------------------------------------------------
// <copyright file="OpenApiParameterTestFixture.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SySML2.NET.REST.Tests.HelpersTests
{
    using Json.Schema;

    using NUnit.Framework;

    using SySML2.NET.REST.Tests.Helpers;

    [TestFixture]
    public class OpenApiParameterTestFixture
    {
        [Test]
        public void VerifyCheck()
        {
            var pageSize = Parameter("integer", "{\"type\":\"integer\"}");
            var ratio = Parameter("number", "{\"type\":\"number\"}");
            var excludeUsed = Parameter("boolean", "{\"type\":\"boolean\"}");
            var sources = Parameter("array", "{\"type\":\"array\",\"items\":{\"type\":\"string\",\"format\":\"uuid\"}}");
            var direction = Parameter("string", "{\"type\":\"string\",\"enum\":[\"in\",\"out\",\"both\"]}");
            var untyped = Parameter(null, "{}");

            using (Assert.EnterMultipleScope())
            {
                Assert.That(pageSize.Check("10"), Is.Empty);
                Assert.That(pageSize.Check("-1"), Is.Empty);
                Assert.That(pageSize.Check("ten"), Has.Count.EqualTo(1));
                Assert.That(pageSize.Check("1.5"), Has.Count.EqualTo(1));
                Assert.That(ratio.Check("1.5"), Is.Empty);
                Assert.That(ratio.Check("half"), Has.Count.EqualTo(1));
                Assert.That(excludeUsed.Check("true"), Is.Empty);
                Assert.That(excludeUsed.Check("True"), Has.Count.EqualTo(1));
                Assert.That(sources.Check("3fa85f64-5717-4562-b3fc-2c963f66afa6,3fa85f64-5717-4562-b3fc-2c963f66afa7"), Is.Empty);
                Assert.That(sources.Check("3fa85f64-5717-4562-b3fc-2c963f66afa6,nope"), Has.Count.EqualTo(1));
                Assert.That(direction.Check("out"), Is.Empty);
                Assert.That(direction.Check("sideways"), Has.Count.EqualTo(1));
                Assert.That(untyped.Check("anything"), Is.Empty);
                Assert.That(pageSize.Check("ten")[0].Kind, Is.EqualTo(ContractViolationKind.InvalidParameter));
                Assert.That(pageSize.Check("ten")[0].Message, Does.Contain("page[size]=ten").And.Contain("integer"));
            }
        }

        [Test]
        public void VerifyOpenApiParameter()
        {
            var schema = JsonSchema.FromText("{}");

            using (Assert.EnterMultipleScope())
            {
                Assert.That(() => new OpenApiParameter(null, "query", false, null, schema), Throws.ArgumentNullException);
                Assert.That(() => new OpenApiParameter("name", null, false, null, schema), Throws.ArgumentNullException);
                Assert.That(() => new OpenApiParameter("name", "query", false, null, null), Throws.ArgumentNullException);
            }

            var parameter = new OpenApiParameter("projectId", "path", true, "string", schema);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(parameter.Name, Is.EqualTo("projectId"));
                Assert.That(parameter.Location, Is.EqualTo("path"));
                Assert.That(parameter.IsRequired, Is.True);
                Assert.That(parameter.Type, Is.EqualTo("string"));
                Assert.That(parameter.Schema, Is.SameAs(schema));
            }
        }

        private static OpenApiParameter Parameter(string type, string schema) => new("page[size]", "query", false, type, JsonSchema.FromText(schema));
    }
}
