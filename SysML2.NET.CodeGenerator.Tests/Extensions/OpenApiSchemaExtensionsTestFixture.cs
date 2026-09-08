// -------------------------------------------------------------------------------------------------
// <copyright file="OpenApiSchemaExtensionsTestFixture.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
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

namespace SysML2.NET.CodeGenerator.Tests.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.OpenApi;

    using NUnit.Framework;

    using SysML2.NET.CodeGenerator.Extensions;

    [TestFixture]
    public class OpenApiSchemaExtensionsTestFixture
    {
        private IDictionary<string, IOpenApiSchema> schemas;

        [OneTimeSetUp]
        public async Task SetUp()
        {
            var openApiDocument = await OpenApiDocumentLoader.LoadAsync();

            this.schemas = openApiDocument.Components.Schemas;
        }

        [Test]
        public void VerifySchemaStructureQueries()
        {
            Assert.That(() => ((IOpenApiSchema)null).QueryIsUnion(), Throws.TypeOf<ArgumentNullException>());
            Assert.That(() => ((IOpenApiSchema)null).QueryUnionAlternativeNames(), Throws.TypeOf<ArgumentNullException>());
            Assert.That(() => ((IOpenApiSchema)null).QueryTypeDiscriminator(), Throws.TypeOf<ArgumentNullException>());
            Assert.That(() => ((IOpenApiSchema)null).QueryHasRequiredIdentifier(), Throws.TypeOf<ArgumentNullException>());
            Assert.That(() => ((IOpenApiSchema)null).QueryMappableProperties("Project"), Throws.TypeOf<ArgumentNullException>());
            Assert.That(() => this.schemas["Project"].QueryMappableProperties(null), Throws.TypeOf<ArgumentNullException>());

            using (Assert.EnterMultipleScope())
            {
                Assert.That(this.schemas["Data"].QueryIsUnion(), Is.True);
                Assert.That(this.schemas["ConstraintRequest"].QueryIsUnion(), Is.True);
                Assert.That(this.schemas["Project"].QueryIsUnion(), Is.False);
                Assert.That(this.schemas["Error"].QueryIsUnion(), Is.False);

                Assert.That(this.schemas["Data"].QueryUnionAlternativeNames(),
                    Is.EqualTo(["Element", "ExternalData", "ExternalRelationship", "ProjectUsage"]));
                Assert.That(this.schemas["Constraint"].QueryUnionAlternativeNames(),
                    Is.EqualTo(["CompositeConstraint", "PrimitiveConstraint"]));
                Assert.That(this.schemas["Project"].QueryUnionAlternativeNames(), Has.Count.EqualTo(0));

                Assert.That(this.schemas["Project"].QueryHasRequiredIdentifier(), Is.True);
                Assert.That(this.schemas["DataIdentityRequest"].QueryHasRequiredIdentifier(), Is.True);
                Assert.That(this.schemas["Error"].QueryHasRequiredIdentifier(), Is.False);
                Assert.That(this.schemas["PrimitiveConstraint"].QueryHasRequiredIdentifier(), Is.False);
                Assert.That(this.schemas["ProjectUsageRequest"].QueryHasRequiredIdentifier(), Is.False);

                Assert.That(this.schemas["Project"].QueryTypeDiscriminator(), Is.EqualTo("Project"));
                Assert.That(this.schemas["ProjectRequest"].QueryTypeDiscriminator(), Is.EqualTo("Project"));
                Assert.That(this.schemas["Data"].QueryTypeDiscriminator(), Is.Null);

                Assert.That(this.schemas["Project"].QueryMappableProperties("Project").Select(property => property.Key),
                    Is.EqualTo(["@id", "alias", "created", "defaultBranch", "description", "name"]));
                Assert.That(this.schemas["PrimitiveConstraint"].QueryMappableProperties("PrimitiveConstraint").Select(property => property.Key),
                    Is.EqualTo(["inverse", "operator", "property"]));
                Assert.That(this.schemas["Data"].QueryMappableProperties("Data"), Has.Count.EqualTo(0));

                Assert.That(this.schemas["PrimitiveConstraint"].QueryEnumerations("PrimitiveConstraint").Select(enumeration => enumeration.Key),
                    Is.EqualTo(["PrimitiveConstraintOperator"]));
                Assert.That(this.schemas["Project"].QueryEnumerations("Project"), Has.Count.EqualTo(0));

                Assert.That(this.schemas["PrimitiveConstraint"].Properties["operator"].QueryEnumerationValues(),
                    Is.EqualTo(["<", "<=", "=", ">", ">=", "in", "instanceOf"]));
                Assert.That(this.schemas["CompositeConstraint"].Properties["operator"].QueryEnumerationValues(), Is.EqualTo(["and", "or"]));
                Assert.That(this.schemas["Project"].Properties["name"].QueryEnumerationValues(), Has.Count.EqualTo(0));
            }
        }

        [Test]
        public void VerifyTypeResolution()
        {
            Assert.That(() => ((IOpenApiSchema)null).QueryCoreTypeName("Project", "name"), Throws.TypeOf<ArgumentNullException>());
            Assert.That(() => ((IOpenApiSchema)null).QueryCSharpTypeName("Project", "name", true), Throws.TypeOf<ArgumentNullException>());
            Assert.That(() => ((IOpenApiSchema)null).QueryIsMappable("Project", "name"), Throws.TypeOf<ArgumentNullException>());
            Assert.That(() => ((IOpenApiSchema)null).QueryReferencedSchemaName(), Throws.TypeOf<ArgumentNullException>());

            using (Assert.EnterMultipleScope())
            {
                Assert.That(this.schemas["Branch"].Properties["owningProject"].QueryCoreTypeName("Branch", "owningProject"), Is.EqualTo("Guid"));
                Assert.That(this.schemas["Commit"].Properties["previousCommit"].QueryCoreTypeName("Commit", "previousCommit"), Is.EqualTo("Guid"));
                Assert.That(this.schemas["DataVersion"].Properties["identity"].QueryCoreTypeName("DataVersion", "identity"), Is.EqualTo("DataIdentity"));
                Assert.That(this.schemas["PrimitiveConstraint"].Properties["value"].QueryCoreTypeName("PrimitiveConstraint", "value"), Is.Null);

                Assert.That(this.schemas["Branch"].Properties["owningProject"].QueryCSharpTypeName("Branch", "owningProject", true), Is.EqualTo("Guid"));
                Assert.That(this.schemas["Branch"].Properties["head"].QueryCSharpTypeName("Branch", "head", true), Is.EqualTo("Guid?"));
                Assert.That(this.schemas["Branch"].Properties["created"].QueryCSharpTypeName("Branch", "created", true), Is.EqualTo("DateTime"));
                Assert.That(this.schemas["Branch"].Properties["deleted"].QueryCSharpTypeName("Branch", "deleted", true), Is.EqualTo("DateTime?"));
                Assert.That(this.schemas["Project"].Properties["alias"].QueryCSharpTypeName("Project", "alias", true), Is.EqualTo("List<string>"));
                Assert.That(this.schemas["Commit"].Properties["previousCommit"].QueryCSharpTypeName("Commit", "previousCommit", true), Is.EqualTo("List<Guid>"));
                Assert.That(this.schemas["PrimitiveConstraint"].Properties["inverse"].QueryCSharpTypeName("PrimitiveConstraint", "inverse", true), Is.EqualTo("bool"));
                Assert.That(this.schemas["PrimitiveConstraintRequest"].Properties["inverse"].QueryCSharpTypeName("PrimitiveConstraintRequest", "inverse", false), Is.EqualTo("bool?"));
                Assert.That(this.schemas["PrimitiveConstraint"].Properties["operator"].QueryCSharpTypeName("PrimitiveConstraint", "operator", true), Is.EqualTo("PrimitiveConstraintOperator"));
                Assert.That(this.schemas["ExternalData"].Properties["resourceIdentifier"].QueryCSharpTypeName("ExternalData", "resourceIdentifier", true), Is.EqualTo("Uri"));
                Assert.That(this.schemas["Project"].Properties["description"].QueryCSharpTypeName("Project", "description", true), Is.EqualTo("string"));
                Assert.That(this.schemas["PrimitiveConstraint"].Properties["value"].QueryCSharpTypeName("PrimitiveConstraint", "value", true), Is.Null);

                Assert.That(this.schemas["DataVersion"].Properties["payload"].QueryCSharpTypeName("DataVersion", "payload", true), Is.EqualTo("IData"));
                Assert.That(this.schemas["Query"].Properties["where"].QueryCSharpTypeName("Query", "where", true), Is.EqualTo("IConstraint"));
                Assert.That(this.schemas["CompositeConstraint"].Properties["constraint"].QueryCSharpTypeName("CompositeConstraint", "constraint", true), Is.EqualTo("List<IConstraint>"));

                Assert.That(this.schemas["Project"].Properties["name"].QueryIsMappable("Project", "name"), Is.True);
                Assert.That(this.schemas["PrimitiveConstraint"].Properties["value"].QueryIsMappable("PrimitiveConstraint", "value"), Is.False);
                Assert.That(this.schemas["PrimitiveConstraintRequest"].Properties["value"].QueryIsMappable("PrimitiveConstraintRequest", "value"), Is.False);

                Assert.That(this.schemas["Branch"].Properties["owningProject"].QueryReferencedSchemaName(), Is.EqualTo("Project"));
                Assert.That(this.schemas["Branch"].Properties["head"].QueryReferencedSchemaName(), Is.EqualTo("Commit"));
                Assert.That(this.schemas["Commit"].Properties["previousCommit"].QueryReferencedSchemaName(), Is.EqualTo("Commit"));
                Assert.That(this.schemas["Project"].Properties["name"].QueryReferencedSchemaName(), Is.Null);
            }
        }

        [Test]
        public void VerifyMultiplicityAndNullability()
        {
            Assert.That(() => ((IOpenApiSchema)null).QueryIsCollection(), Throws.TypeOf<ArgumentNullException>());
            Assert.That(() => ((IOpenApiSchema)null).QueryIsNullable(true), Throws.TypeOf<ArgumentNullException>());
            Assert.That(() => ((IOpenApiSchema)null).QueryIsValueType(), Throws.TypeOf<ArgumentNullException>());

            using (Assert.EnterMultipleScope())
            {
                Assert.That(this.schemas["Project"].Properties["alias"].QueryIsCollection(), Is.True);
                Assert.That(this.schemas["Commit"].Properties["previousCommit"].QueryIsCollection(), Is.True);
                Assert.That(this.schemas["Project"].Properties["name"].QueryIsCollection(), Is.False);
                Assert.That(this.schemas["Branch"].Properties["owningProject"].QueryIsCollection(), Is.False);
                Assert.That(this.schemas["DataVersion"].Properties["payload"].QueryIsCollection(), Is.False);

                Assert.That(this.schemas["Branch"].Properties["created"].QueryIsNullable(true), Is.False);
                Assert.That(this.schemas["Branch"].Properties["owningProject"].QueryIsNullable(true), Is.False);
                Assert.That(this.schemas["Branch"].Properties["deleted"].QueryIsNullable(true), Is.True);
                Assert.That(this.schemas["Branch"].Properties["head"].QueryIsNullable(true), Is.True);
                Assert.That(this.schemas["ProjectRequest"].Properties["defaultBranch"].QueryIsNullable(false), Is.True);

                Assert.That(this.schemas["Branch"].Properties["created"].QueryIsValueType(), Is.True);
                Assert.That(this.schemas["Branch"].Properties["owningProject"].QueryIsValueType(), Is.True);
                Assert.That(this.schemas["PrimitiveConstraint"].Properties["inverse"].QueryIsValueType(), Is.True);
                Assert.That(this.schemas["PrimitiveConstraint"].Properties["operator"].QueryIsValueType(), Is.True);
                Assert.That(this.schemas["Project"].Properties["name"].QueryIsValueType(), Is.False);
                Assert.That(this.schemas["ExternalData"].Properties["resourceIdentifier"].QueryIsValueType(), Is.False);
                Assert.That(this.schemas["Project"].Properties["alias"].QueryIsValueType(), Is.False);
                Assert.That(this.schemas["DataVersion"].Properties["payload"].QueryIsValueType(), Is.False);
            }
        }

        [Test]
        public void VerifyNaming()
        {
            using (Assert.EnterMultipleScope())
            {
                Assert.That(() => OpenApiSchemaExtensions.QueryEnumerationTypeName(null, "operator"), Throws.TypeOf<ArgumentNullException>());
                Assert.That(() => OpenApiSchemaExtensions.QueryPropertyName(null), Throws.TypeOf<ArgumentNullException>());
                Assert.That(() => OpenApiSchemaExtensions.QueryInterfaceName(null), Throws.TypeOf<ArgumentNullException>());
                Assert.That(() => OpenApiSchemaExtensions.QueryEnumerationLiteralName(null), Throws.TypeOf<ArgumentNullException>());

                Assert.That(OpenApiSchemaExtensions.QueryEnumerationTypeName("PrimitiveConstraint", "operator"), Is.EqualTo("PrimitiveConstraintOperator"));
                Assert.That(OpenApiSchemaExtensions.QueryEnumerationTypeName("CompositeConstraintRequest", "operator"), Is.EqualTo("CompositeConstraintRequestOperator"));

                Assert.That(OpenApiSchemaExtensions.QueryPropertyName("@id"), Is.EqualTo("Id"));
                Assert.That(OpenApiSchemaExtensions.QueryPropertyName("@type"), Is.EqualTo("Type"));
                Assert.That(OpenApiSchemaExtensions.QueryPropertyName("resourceIdentifier"), Is.EqualTo("ResourceIdentifier"));
                Assert.That(OpenApiSchemaExtensions.QueryPropertyName("Name"), Is.EqualTo("Name"));

                Assert.That(OpenApiSchemaExtensions.QueryInterfaceName("Data"), Is.EqualTo("IData"));
                Assert.That(OpenApiSchemaExtensions.QueryInterfaceName("ConstraintRequest"), Is.EqualTo("IConstraintRequest"));

                Assert.That(OpenApiSchemaExtensions.QueryEnumerationLiteralName("<"), Is.EqualTo("lessthan"));
                Assert.That(OpenApiSchemaExtensions.QueryEnumerationLiteralName("<="), Is.EqualTo("lessthanorequalto"));
                Assert.That(OpenApiSchemaExtensions.QueryEnumerationLiteralName("="), Is.EqualTo("equalto"));
                Assert.That(OpenApiSchemaExtensions.QueryEnumerationLiteralName(">"), Is.EqualTo("greaterthan"));
                Assert.That(OpenApiSchemaExtensions.QueryEnumerationLiteralName(">="), Is.EqualTo("greaterthanorequalto"));
                Assert.That(OpenApiSchemaExtensions.QueryEnumerationLiteralName("in"), Is.EqualTo("@in"));
                Assert.That(OpenApiSchemaExtensions.QueryEnumerationLiteralName("instanceOf"), Is.EqualTo("instanceOf"));
                Assert.That(OpenApiSchemaExtensions.QueryEnumerationLiteralName("and"), Is.EqualTo("and"));
            }
        }
    }
}
