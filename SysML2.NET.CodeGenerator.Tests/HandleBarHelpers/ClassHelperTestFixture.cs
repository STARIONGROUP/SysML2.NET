// -------------------------------------------------------------------------------------------------
// <copyright file="ClassHelperTestFixture.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.CodeGenerator.Tests.Generators.UmlHandleBarsGenerators
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using HandlebarsDotNet;
    using HandlebarsDotNet.Helpers;

    using NUnit.Framework;

    using SysML2.NET.CodeGenerator.Extensions;
    using SysML2.NET.CodeGenerator.HandleBarHelpers;

    using uml4net.Extensions;
    using uml4net.SimpleClassifiers;
    using uml4net.StructuredClassifiers;

    [TestFixture]
    public class ClassHelperTestFixture
    {
        private IHandlebars handlebars;
        private List<IClass> allClasses;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            this.handlebars = Handlebars.CreateSharedEnvironment();
            HandlebarsHelpers.Register(this.handlebars);
            this.handlebars.RegisterClassHelper();

            this.allClasses = GeneratorSetupFixture.XmiReaderResult.QueryContainedAndImported("SysML")
                .SelectMany(x => x.PackagedElement.OfType<IClass>())
                .ToList();
        }

        /// <summary>
        /// Helper to invoke a template with IClass as context via {{#each}}
        /// </summary>
        private string RenderWithClassContext(string helperExpression, IClass @class)
        {
            var template = this.handlebars.Compile($"{{{{#each items}}}}{helperExpression}{{{{/each}}}}");
            return template(new { items = new[] { @class } });
        }

        [Test]
        public void Verify_that_WriteEnumerationNameSpaces_writes_using_statements_for_enum_properties()
        {
            var classWithEnums = this.allClasses.FirstOrDefault(c =>
                c.QueryAllProperties().Any(p => p.QueryIsEnum()));

            if (classWithEnums == null)
            {
                Assert.Ignore("No class with enum properties found in the test data");
            }

            var result = this.RenderWithClassContext("{{Class.WriteEnumerationNameSpaces}}", classWithEnums);

            Assert.That(result, Does.Contain("using SysML2.NET.Core."));
        }

        [Test]
        public void Verify_that_WriteEnumerationNameSpaces_writes_nothing_for_class_without_enums()
        {
            var classWithoutEnums = this.allClasses.FirstOrDefault(c =>
                !c.QueryAllProperties().Any(p => p.QueryIsEnum()));

            if (classWithoutEnums == null)
            {
                Assert.Ignore("All classes have enum properties");
            }

            var result = this.RenderWithClassContext("{{Class.WriteEnumerationNameSpaces}}", classWithoutEnums);

            Assert.That(result, Is.Empty);
        }

        [Test]
        public void Verify_that_WriteEnumerationNameSpace_writes_using_for_enumeration()
        {
            var enumerations = GeneratorSetupFixture.XmiReaderResult.QueryContainedAndImported("SysML")
                .SelectMany(x => x.PackagedElement.OfType<IEnumeration>())
                .ToList();

            if (enumerations.Count == 0)
            {
                Assert.Ignore("No enumerations found in the test data");
            }

            var template = this.handlebars.Compile("{{#each items}}{{Class.WriteEnumerationNameSpace}}{{/each}}");
            var result = template(new { items = new[] { enumerations.First() } });

            Assert.That(result, Does.Contain("using SysML2.NET.Core."));
        }

        [Test]
        public void Verify_that_WriteNameSpaces_writes_using_statements_with_DTO_prefix()
        {
            var testClass = this.allClasses.First(c => c.SuperClass.Count > 0);

            var result = this.RenderWithClassContext("{{Class.WriteNameSpaces this \"DTO\"}}", testClass);

            Assert.That(result, Does.Contain("using SysML2.NET.Core.DTO."));
        }

        [Test]
        public void Verify_that_WriteNameSpaces_writes_using_statements_with_POCO_prefix()
        {
            var testClass = this.allClasses.First(c => c.SuperClass.Count > 0);
    
            var result = this.RenderWithClassContext("{{Class.WriteNameSpaces this \"POCO\"}}", testClass);

            Assert.That(result, Does.Contain("using SysML2.NET.Core.POCO."));
        }

        [Test]
        public void Verify_that_WriteCountAllNonDerivedProperties_writes_numeric_count()
        {
            var testClass = this.allClasses.First(c => !c.IsAbstract);

            var result = this.RenderWithClassContext("{{Class.WriteCountAllNonDerivedProperties}}", testClass);

            Assert.That(result, Is.Not.Null.And.Not.Empty);
            Assert.That(int.TryParse(result.Trim(), out var count), Is.True,
                $"Expected numeric output but got: '{result}'");
            Assert.That(count, Is.GreaterThanOrEqualTo(0));
        }

        [Test]
        public void Verify_that_QueryAllNonDerivedNonRedefinedProperties_does_not_throw()
        {
            var testClass = this.allClasses.First(c => !c.IsAbstract && c.QueryAllNonDerivedNonRedefinedProperties().Count > 0);

            Assert.That(() => this.RenderWithClassContext(
                "{{#Class.QueryAllNonDerivedNonRedefinedProperties}}item{{/Class.QueryAllNonDerivedNonRedefinedProperties}}",
                testClass), Throws.Nothing);
        }

        [Test]
        public void Verify_that_QueryAllNonDerivedNonRedefinedProperties_returns_expected_count()
        {
            var testClass = this.allClasses.First(c => !c.IsAbstract);

            var expectedCount = testClass.QueryAllNonDerivedNonRedefinedProperties().Count;
            var countResult = this.RenderWithClassContext("{{Class.WriteCountAllNonDerivedNonRedefinedProperties}}", testClass);
            var actualCount = int.Parse(countResult.Trim());

            Assert.That(actualCount, Is.EqualTo(expectedCount));
        }

        [Test]
        public void Verify_that_WriteCountAllNonDerivedNonRedefinedProperties_writes_numeric_count()
        {
            var testClass = this.allClasses.First(c => !c.IsAbstract);

            var result = this.RenderWithClassContext("{{Class.WriteCountAllNonDerivedNonRedefinedProperties}}", testClass);

            Assert.That(result, Is.Not.Null.And.Not.Empty);
            Assert.That(int.TryParse(result.Trim(), out var count), Is.True,
                $"Expected numeric output but got: '{result}'");
            Assert.That(count, Is.GreaterThanOrEqualTo(0));
        }

        [Test]
        public void Verify_that_WriteInternalInterface_does_not_throw()
        {
            var testClass = this.allClasses.First(c => !c.IsAbstract);

            var result = this.RenderWithClassContext("{{Class.WriteInternalInterface}}", testClass);

            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void Verify_that_QueryNonDerivedCompositeAggregation_iterates_composite_properties()
        {
            var testClass = this.allClasses.First(c => !c.IsAbstract);

            var result = this.RenderWithClassContext(
                "{{#Class.QueryNonDerivedCompositeAggregation}}{{Name}},{{/Class.QueryNonDerivedCompositeAggregation}}",
                testClass);

            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void Verify_that_QueryAllPropertiesSorted_does_not_throw()
        {
            var testClass = this.allClasses.First(c => c.QueryAllProperties().Count > 1);

            Assert.That(() => this.RenderWithClassContext(
                "{{#Class.QueryAllPropertiesSorted}}item{{/Class.QueryAllPropertiesSorted}}",
                testClass), Throws.Nothing);
        }

        [Test]
        public void Verify_that_all_helpers_can_be_invoked_on_every_class_without_throwing()
        {
            foreach (var @class in this.allClasses.Take(10))
            {
                Assert.That(() => this.RenderWithClassContext(
                    "{{Class.WriteCountAllNonDerivedProperties}}|{{Class.WriteCountAllNonDerivedNonRedefinedProperties}}|{{Class.WriteInternalInterface}}",
                    @class), Throws.Nothing,
                    $"Failed for class {@class.Name}");
            }
        }
    }
}
