// -------------------------------------------------------------------------------------------------
// <copyright file="PropertyHelperTestFixture.cs" company="Starion Group S.A.">
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
    using System.Collections.Generic;
    using System.Linq;

    using HandlebarsDotNet;
    using HandlebarsDotNet.Helpers;

    using NUnit.Framework;

    using SysML2.NET.CodeGenerator.Extensions;
    using SysML2.NET.CodeGenerator.HandleBarHelpers;

    using uml4net.Classification;
    using uml4net.Extensions;
    using uml4net.StructuredClassifiers;

    [TestFixture]
    public class PropertyHelperTestFixture
    {
        private IHandlebars handlebars;
        private List<IClass> allClasses;
        private IClass testClass;
        private IProperty testProperty;
        private IProperty derivedProperty;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            this.handlebars = Handlebars.CreateSharedEnvironment();
            HandlebarsHelpers.Register(this.handlebars);
            PropertyHelper.RegisterPropertyHelper(this.handlebars);

            this.allClasses = GeneratorSetupFixture.XmiReaderResult.QueryContainedAndImported("SysML")
                .SelectMany(x => x.PackagedElement.OfType<IClass>())
                .ToList();

            this.testClass = this.allClasses.First(x => !x.IsAbstract);
            this.testProperty = this.testClass.QueryAllProperties().First();
            this.derivedProperty = this.allClasses
                .SelectMany(c => c.QueryAllProperties())
                .First(p => p.IsDerived || p.IsDerivedUnion);
        }

        /// <summary>
        /// Renders a template with IProperty as context using {{#each}} to set context.Value properly
        /// </summary>
        private string RenderWithPropertyContext(string helperExpression, IProperty property)
        {
            var template = this.handlebars.Compile($"{{{{#each items}}}}{helperExpression}{{{{/each}}}}");
            return template(new { items = new[] { property } });
        }

        /// <summary>
        /// Renders a template with IProperty as context and an IClass in parent scope
        /// </summary>
        private string RenderWithPropertyAndClassContext(string helperExpression, IProperty property, IClass @class)
        {
            var template = this.handlebars.Compile($"{{{{#each items}}}}{helperExpression}{{{{/each}}}}");
            return template(new { items = new[] { property }, classObj = @class });
        }

        // -------------------------------------------------------------------
        // Property.WriteForDTOInterface (context.Value = IProperty)
        // -------------------------------------------------------------------

        [Test]
        public void Verify_that_WriteForDTOInterface_generates_property_declaration()
        {
            var result = this.RenderWithPropertyContext("{{Property.WriteForDTOInterface}}", this.testProperty);

            Assert.That(result.Trim(), Is.Not.Empty);
            Assert.That(result, Does.Contain("get"));
        }

        [Test]
        public void Verify_that_WriteForDTOInterface_generates_for_multiple_property_types()
        {
            var properties = this.allClasses
                .SelectMany(c => c.QueryAllProperties())
                .Take(20);

            foreach (var property in properties)
            {
                var result = this.RenderWithPropertyContext("{{Property.WriteForDTOInterface}}", property);

                Assert.That(result.Trim(), Is.Not.Empty,
                    $"WriteForDTOInterface produced empty output for property {property.Name}");
            }
        }

        // -------------------------------------------------------------------
        // Property.WriteForPOCOInterface (context.Value = IProperty)
        // -------------------------------------------------------------------

        [Test]
        public void Verify_that_WriteForPOCOInterface_generates_property_declaration()
        {
            var result = this.RenderWithPropertyContext("{{Property.WriteForPOCOInterface}}", this.testProperty);

            Assert.That(result.Trim(), Is.Not.Empty);
        }

        [Test]
        public void Verify_that_WriteForPOCOInterface_generates_for_multiple_property_types()
        {
            var properties = this.allClasses
                .SelectMany(c => c.QueryAllProperties())
                .Take(20);

            foreach (var property in properties)
            {
                var result = this.RenderWithPropertyContext("{{Property.WriteForPOCOInterface}}", property);

                Assert.That(result.Trim(), Is.Not.Empty,
                    $"WriteForPOCOInterface produced empty output for property {property.Name}");
            }
        }

        // -------------------------------------------------------------------
        // Property.WriteTypeForExtendClass (context.Value = IProperty, must be derived)
        // -------------------------------------------------------------------

        [Test]
        public void Verify_that_WriteTypeForExtendClass_generates_type_for_derived_property()
        {
            var result = this.RenderWithPropertyContext("{{Property.WriteTypeForExtendClass}}", this.derivedProperty);

            Assert.That(result.Trim(), Is.Not.Empty,
                $"Expected type output for derived property {this.derivedProperty.Name}");
        }

        // -------------------------------------------------------------------
        // Property.WriteForDTOClass (parameters[0] = IProperty, parameters[1] = IClass)
        // -------------------------------------------------------------------

        [Test]
        public void Verify_that_WriteForDTOClass_generates_property_for_each_class()
        {
            foreach (var @class in this.allClasses.Where(c => !c.IsAbstract).Take(5))
            {
                var properties = @class.QueryAllNonDerivedNonRedefinedProperties();

                foreach (var property in properties.Take(3))
                {
                    var result = this.RenderWithPropertyAndClassContext(
                        "{{Property.WriteForDTOClass this ../classObj}}", property, @class);

                    Assert.That(result, Is.Not.Null,
                        $"WriteForDTOClass failed for property {property.Name} on class {@class.Name}");
                }
            }
        }

        // -------------------------------------------------------------------
        // Property.WriteForPOCOClass (context.Value = IProperty, arguments[1] = IClass)
        // -------------------------------------------------------------------

        [Test]
        public void Verify_that_WriteForPOCOClass_generates_property()
        {
            var properties = this.testClass.QueryAllProperties().Take(3).ToArray();

            foreach (var property in properties)
            {
                var result = this.RenderWithPropertyAndClassContext(
                    "{{Property.WriteForPOCOClass this ../classObj}}", property, this.testClass);

                Assert.That(result, Is.Not.Null,
                    $"WriteForPOCOClass failed for {property.Name}");
            }
        }

        // -------------------------------------------------------------------
        // Property.WriteForDTOMessagePackFormatterSerialize
        // -------------------------------------------------------------------

        [Test]
        public void Verify_that_WriteForDTOMessagePackFormatterSerialize_generates_code_for_non_derived_property()
        {
            var nonAbstractClass = this.allClasses.First(x => !x.IsAbstract);

            var nonDerivedProperty = nonAbstractClass.QueryAllProperties()
                .FirstOrDefault(p => !p.IsDerived && !p.IsDerivedUnion && !p.IsReadOnly);

            if (nonDerivedProperty == null)
            {
                Assert.Ignore("No non-derived, non-readonly property found");
            }

            var result = this.RenderWithPropertyAndClassContext(
                "{{Property.WriteForDTOMessagePackFormatterSerialize this ../classObj}}", nonDerivedProperty, nonAbstractClass);

            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void Verify_that_WriteForDTOMessagePackFormatterSerialize_produces_empty_for_derived_property()
        {
            var result = this.RenderWithPropertyAndClassContext(
                "{{Property.WriteForDTOMessagePackFormatterSerialize this ../classObj}}", this.derivedProperty, this.testClass);

            Assert.That(result.Trim(), Is.Empty);
        }

        // -------------------------------------------------------------------
        // Property.WriteForDTOMessagePackFormatterDeSerialize
        // -------------------------------------------------------------------

        [Test]
        public void Verify_that_WriteForDTOMessagePackFormatterDeSerialize_generates_code()
        {
            var nonAbstractClass = this.allClasses.First(x => !x.IsAbstract);

            var nonDerivedProperty = nonAbstractClass.QueryAllProperties()
                .FirstOrDefault(p => !p.IsDerived && !p.IsDerivedUnion && !p.IsReadOnly);

            if (nonDerivedProperty == null)
            {
                Assert.Ignore("No non-derived, non-readonly property found");
            }

            var result = this.RenderWithPropertyAndClassContext(
                "{{Property.WriteForDTOMessagePackFormatterDeSerialize this ../classObj}}", nonDerivedProperty, nonAbstractClass);

            Assert.That(result, Is.Not.Null);
        }

        // -------------------------------------------------------------------
        // Property.WriteForDTOComparer
        // -------------------------------------------------------------------

        [Test]
        public void Verify_that_WriteForDTOComparer_generates_comparison_code()
        {
            var nonAbstractClass = this.allClasses.First(x => !x.IsAbstract);

            var nonDerivedNonRedefinedProperty = nonAbstractClass.QueryAllProperties()
                .FirstOrDefault(p => !p.IsDerived && !p.IsDerivedUnion
                    && !p.TryQueryRedefinedByProperty(nonAbstractClass, out _));

            if (nonDerivedNonRedefinedProperty == null)
            {
                Assert.Ignore("No suitable property found");
            }

            var result = this.RenderWithPropertyAndClassContext(
                "{{Property.WriteForDTOComparer this ../classObj}}", nonDerivedNonRedefinedProperty, nonAbstractClass);

            Assert.That(result.Trim(), Is.Not.Empty);
            Assert.That(result, Does.Contain("return false"));
        }

        // -------------------------------------------------------------------
        // Boolean helpers (subexpression syntax with {{#each}} context)
        // -------------------------------------------------------------------

        [Test]
        public void Verify_that_IsPropertyRedefinedInClass_can_be_evaluated()
        {
            var result = this.RenderWithPropertyAndClassContext(
                "{{#if (Property.IsPropertyRedefinedInClass this ../classObj)}}YES{{else}}NO{{/if}}",
                this.testProperty, this.testClass);

            Assert.That(result, Is.EqualTo("YES").Or.EqualTo("NO"));
        }

        [Test]
        public void Verify_that_IsRedefinedOrRedifines_can_be_evaluated()
        {
            var result = this.RenderWithPropertyAndClassContext(
                "{{#if (Property.IsRedefinedOrRedifines this ../classObj)}}YES{{else}}NO{{/if}}",
                this.testProperty, this.testClass);

            Assert.That(result, Is.EqualTo("YES").Or.EqualTo("NO"));
        }

        [Test]
        public void Verify_that_IsRedefinedByPropertyWithSameName_can_be_evaluated()
        {
            var result = this.RenderWithPropertyAndClassContext(
                "{{#if (Property.IsRedefinedByPropertyWithSameName this ../classObj)}}YES{{else}}NO{{/if}}",
                this.testProperty, this.testClass);

            Assert.That(result, Is.EqualTo("YES").Or.EqualTo("NO"));
        }

        [Test]
        public void Verify_that_IsOfTypeBaseElement_can_be_evaluated()
        {
            var result = this.RenderWithPropertyContext(
                "{{#if (Property.IsOfTypeBaseElement this)}}YES{{else}}NO{{/if}}",
                this.testProperty);

            Assert.That(result, Is.EqualTo("YES").Or.EqualTo("NO"));
        }

        [Test]
        public void Verify_that_QueryIsImpliedIncluded_returns_true_for_isImpliedIncluded_property()
        {
            var impliedIncludedProp = this.allClasses
                .SelectMany(c => c.QueryAllProperties())
                .FirstOrDefault(p => p.Name == "isImpliedIncluded");

            if (impliedIncludedProp == null)
            {
                Assert.Ignore("No isImpliedIncluded property found");
            }

            var result = this.RenderWithPropertyContext(
                "{{#if (Property.QueryIsImpliedIncluded this)}}YES{{else}}NO{{/if}}",
                impliedIncludedProp);

            Assert.That(result, Is.EqualTo("YES"));
        }

        [Test]
        public void Verify_that_QueryIsImpliedIncluded_returns_false_for_other_property()
        {
            var otherProp = this.allClasses
                .SelectMany(c => c.QueryAllProperties())
                .First(p => p.Name != "isImpliedIncluded");

            var result = this.RenderWithPropertyContext(
                "{{#if (Property.QueryIsImpliedIncluded this)}}YES{{else}}NO{{/if}}",
                otherProp);

            Assert.That(result, Is.EqualTo("NO"));
        }

        [Test]
        public void Verify_that_QueryHasDefaultValueWithDifferentValueThanDefault_can_be_evaluated()
        {
            var result = this.RenderWithPropertyContext(
                "{{#if (Property.QueryHasDefaultValueWithDifferentValueThanDefault this)}}YES{{else}}NO{{/if}}",
                this.testProperty);

            Assert.That(result, Is.EqualTo("YES").Or.EqualTo("NO"));
        }

        [Test]
        public void Verify_that_QueryIsEnumerableAndReferenceProperty_can_be_evaluated()
        {
            var result = this.RenderWithPropertyContext(
                "{{#if (Property.QueryIsEnumerableAndReferenceProperty this)}}YES{{else}}NO{{/if}}",
                this.testProperty);

            Assert.That(result, Is.EqualTo("YES").Or.EqualTo("NO"));
        }

        [Test]
        public void Verify_that_IsTypeAbstract_can_be_evaluated()
        {
            // Find a property whose Type supports IsAbstract without throwing
            var safeProperty = this.allClasses
                .SelectMany(c => c.QueryAllProperties())
                .FirstOrDefault(p =>
                {
                    try
                    {
                        var _ = p.Type is uml4net.Classification.IClassifier { IsAbstract: true };
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                });

            if (safeProperty == null)
            {
                Assert.Ignore("No property with safe IsAbstract access found");
            }

            var result = this.RenderWithPropertyContext(
                "{{#if (Property.IsTypeAbstract this)}}YES{{else}}NO{{/if}}",
                safeProperty);

            Assert.That(result, Is.EqualTo("YES").Or.EqualTo("NO"));
        }

        // -------------------------------------------------------------------
        // Property.WritePropertyName
        // -------------------------------------------------------------------

        [Test]
        public void Verify_that_WritePropertyName_writes_property_name()
        {
            var result = this.RenderWithPropertyContext("{{Property.WritePropertyName this}}", this.testProperty);

            Assert.That(result.Trim(), Is.Not.Empty);
        }

        // -------------------------------------------------------------------
        // Property.WritePropertyWithExplicitInterfaceIfRequiredForDTO
        // -------------------------------------------------------------------

        [Test]
        public void Verify_that_WritePropertyWithExplicitInterfaceIfRequiredForDTO_writes_name()
        {
            var result = this.RenderWithPropertyAndClassContext(
                "{{Property.WritePropertyWithExplicitInterfaceIfRequiredForDTO this ../classObj \"dto\"}}",
                this.testProperty, this.testClass);

            Assert.That(result, Is.Not.Null.And.Not.Empty);
            Assert.That(result, Does.Contain("dto."));
        }

        // -------------------------------------------------------------------
        // Property.WriteDefaultValue
        // -------------------------------------------------------------------

        [Test]
        public void Verify_that_WriteDefaultValue_writes_value_for_property_with_default()
        {
            var propertyWithDefault = this.allClasses
                .SelectMany(c => c.QueryAllProperties())
                .FirstOrDefault(p => p.QueryHasDefaultValue() && p.QueryIsDefaultValueDifferentThanDefault());

            if (propertyWithDefault == null)
            {
                Assert.Ignore("No property with a non-default default value found");
            }

            var result = this.RenderWithPropertyContext("{{Property.WriteDefaultValue this}}", propertyWithDefault);

            Assert.That(result, Is.Not.Empty);
        }

        // -------------------------------------------------------------------
        // Comprehensive tests
        // -------------------------------------------------------------------

        [Test]
        public void Verify_that_WriteForDTOInterface_and_WriteForPOCOInterface_produce_output_for_all_classes()
        {
            foreach (var @class in this.allClasses.Take(5))
            {
                foreach (var property in @class.QueryAllProperties().Take(5))
                {
                    var dtoResult = this.RenderWithPropertyContext("{{Property.WriteForDTOInterface}}", property);
                    Assert.That(dtoResult.Trim(), Is.Not.Empty,
                        $"WriteForDTOInterface empty for {@class.Name}.{property.Name}");

                    var pocoResult = this.RenderWithPropertyContext("{{Property.WriteForPOCOInterface}}", property);
                    Assert.That(pocoResult.Trim(), Is.Not.Empty,
                        $"WriteForPOCOInterface empty for {@class.Name}.{property.Name}");
                }
            }
        }
    }
}
