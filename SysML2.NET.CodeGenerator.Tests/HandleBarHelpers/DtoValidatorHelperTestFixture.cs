// ------------------------------------------------------------------------------------------------
// <copyright file="DtoValidatorHelperTestFixture.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.CodeGenerator.Tests.HandleBarHelpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    using HandlebarsDotNet;
    using HandlebarsDotNet.Helpers;

    using NUnit.Framework;

    using SysML2.NET.CodeGenerator.Extensions;
    using SysML2.NET.CodeGenerator.HandleBarHelpers;
    using SysML2.NET.CodeGenerator.Tests.Generators.UmlHandleBarsGenerators;

    using uml4net.Extensions;
    using uml4net.StructuredClassifiers;

    [TestFixture]
    public partial class DtoValidatorHelperTestFixture
    {
        private List<IClass> concreteClasses;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            this.concreteClasses = GeneratorSetupFixture.XmiReaderResult.QueryContainedAndImported("SysML")
                .SelectMany(x => x.PackagedElement.OfType<IClass>())
                .Where(x => !x.IsAbstract)
                .ToList();
        }

        [Test]
        public void VerifyQueryCollectionCountRule()
        {
            using (Assert.EnterMultipleScope())
            {
                Assert.That(DtoValidatorHelper.QueryCollectionCountRule(0, int.MaxValue, false), Is.Null);
                Assert.That(DtoValidatorHelper.QueryCollectionCountRule(0, 3, false), Is.EqualTo(".Must(value => value == null || value.Count <= 3)"));
                Assert.That(DtoValidatorHelper.QueryCollectionCountRule(1, int.MaxValue, false), Is.EqualTo(".NotEmpty()"));
                Assert.That(DtoValidatorHelper.QueryCollectionCountRule(2, int.MaxValue, false), Is.EqualTo(".Cascade(CascadeMode.Stop).NotEmpty().Must(value => value.Count >= 2)"));
                Assert.That(DtoValidatorHelper.QueryCollectionCountRule(1, 3, false), Is.EqualTo(".Cascade(CascadeMode.Stop).NotEmpty().Must(value => value.Count <= 3)"));
                Assert.That(DtoValidatorHelper.QueryCollectionCountRule(2, 5, false), Is.EqualTo(".Cascade(CascadeMode.Stop).NotEmpty().Must(value => value.Count >= 2).Must(value => value.Count <= 5)"));
                Assert.That(DtoValidatorHelper.QueryCollectionCountRule(0, int.MaxValue, true), Is.Null);
                Assert.That(DtoValidatorHelper.QueryCollectionCountRule(0, 3, true), Is.EqualTo(".Must(value => value == null || value.Count <= 3)"));
                Assert.That(DtoValidatorHelper.QueryCollectionCountRule(1, int.MaxValue, true), Is.EqualTo(".NotEmpty().WithSeverity(Severity.Warning)"));
                Assert.That(DtoValidatorHelper.QueryCollectionCountRule(2, int.MaxValue, true), Is.EqualTo(".Cascade(CascadeMode.Stop).NotEmpty().WithSeverity(Severity.Warning).Must(value => value.Count >= 2).WithSeverity(Severity.Warning)"));
                Assert.That(DtoValidatorHelper.QueryCollectionCountRule(1, 3, true), Is.EqualTo(".Cascade(CascadeMode.Stop).NotEmpty().WithSeverity(Severity.Warning).Must(value => value.Count <= 3)"));
                Assert.That(DtoValidatorHelper.QueryCollectionCountRule(2, 5, true), Is.EqualTo(".Cascade(CascadeMode.Stop).NotEmpty().WithSeverity(Severity.Warning).Must(value => value.Count >= 2).WithSeverity(Severity.Warning).Must(value => value.Count <= 5)"));
            }
        }

        [Test]
        public void VerifyQueryRules()
        {
            Assert.That(() => DtoValidatorHelper.QueryRules(null), Throws.TypeOf<ArgumentNullException>());

            var membership = this.QueryClass("Membership");
            var owningMembership = this.QueryClass("OwningMembership");
            var feature = this.QueryClass("Feature");

            List<string> membershipRules =
            [
                "this.RuleFor(dto => dto.ElementId).NotNull();",
                "this.RuleFor(dto => dto.MemberElement).NotEmpty();",
                "this.RuleFor(dto => dto.OwningRelatedElement).NotEmpty().WithSeverity(Severity.Warning);",
                "this.RuleFor(dto => dto.Visibility).IsInEnum();"
            ];

            using (Assert.EnterMultipleScope())
            {
                Assert.That(DtoValidatorHelper.QueryRules(membership), Is.EqualTo(membershipRules));

                Assert.That(QueryValidatedPropertyNames(owningMembership), Does.Not.Contain("MemberElement"));
                Assert.That(QueryValidatedPropertyNames(owningMembership), Does.Not.Contain("OwnedMemberElement"));
                Assert.That(DtoValidatorHelper.QueryRules(owningMembership), Does.Contain("this.RuleFor(dto => dto.OwnedRelatedElement).NotEmpty().WithSeverity(Severity.Warning);"));
                Assert.That(DtoValidatorHelper.QueryRules(this.QueryClass("FeatureMembership")), Does.Contain("this.RuleFor(dto => dto.OwnedRelatedElement).NotEmpty().WithSeverity(Severity.Warning);"));
                Assert.That(DtoValidatorHelper.QueryRules(this.QueryClass("ConjugatedPortDefinition")), Does.Contain("this.RuleFor(dto => dto.OwnedRelationship).NotEmpty().WithSeverity(Severity.Warning);"));
                Assert.That(DtoValidatorHelper.QueryRules(this.QueryClass("Dependency")), Does.Contain("this.RuleFor(dto => dto.Client).NotEmpty();"));

                Assert.That(QueryValidatedPropertyNames(this.QueryClass("PortDefinition")), Does.Not.Contain("OwnedRelationship"));
                Assert.That(QueryValidatedPropertyNames(feature), Does.Not.Contain("OwnedRelationship"));
                Assert.That(QueryValidatedPropertyNames(this.QueryClass("Dependency")), Does.Not.Contain("OwnedRelatedElement"));

                Assert.That(QueryValidatedPropertyNames(feature), Does.Not.Contain("IsComposite"));
                Assert.That(QueryValidatedPropertyNames(feature), Does.Not.Contain("DeclaredName"));
                Assert.That(QueryValidatedPropertyNames(this.QueryClass("LiteralInteger")), Does.Not.Contain("Value"));
            }

            var rulesByClass = this.concreteClasses
                .Select(@class => (@class.Name, Rules: DtoValidatorHelper.QueryRules(@class)))
                .ToList();

            var derivedViolations = this.concreteClasses
                .SelectMany(@class => QueryValidatedPropertyNames(@class)
                    .Intersect(QueryDerivedPropertyNames(@class))
                    .Select(propertyName => $"{@class.Name}.{propertyName}"))
                .ToList();

            var forbiddenRules = rulesByClass
                .SelectMany(entry => entry.Rules)
                .Where(rule => rule.Contains("RuleForEach") || rule.Contains("Distinct()") || rule.Contains("HasValue"))
                .ToList();

            var classesWithRepeatedProperty = rulesByClass
                .Where(entry => entry.Rules.Select(QuerySelectedPropertyName).Distinct().Count() != entry.Rules.Count)
                .Select(entry => entry.Name)
                .ToList();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(derivedViolations, Is.Empty);
                Assert.That(forbiddenRules, Is.Empty);
                Assert.That(classesWithRepeatedProperty, Is.Empty);
            }
        }

        [Test]
        public void VerifyRegisterDtoValidatorHelper()
        {
            Assert.That(() => ((IHandlebars)null).RegisterDtoValidatorHelper(), Throws.TypeOf<ArgumentNullException>());

            var handlebars = Handlebars.CreateSharedEnvironment();
            HandlebarsHelpers.Register(handlebars);
            handlebars.RegisterDtoValidatorHelper();

            var template = handlebars.Compile("{{DtoValidator.WriteRules this}}");

            using (Assert.EnterMultipleScope())
            {
                Assert.That(template(this.QueryClass("Membership")), Does.Contain("this.RuleFor(dto => dto.MemberElement).NotEmpty();"));
                Assert.That(() => template(new { }), Throws.TypeOf<ArgumentException>());
                Assert.That(() => handlebars.Compile("{{DtoValidator.WriteRules}}")(new { }), Throws.TypeOf<HandlebarsException>());
            }
        }

        private static HashSet<string> QueryDerivedPropertyNames(IClass @class)
        {
            var properties = @class.QueryAllProperties();

            var nonDerivedNames = properties
                .Where(property => !property.IsDerived && !property.IsDerivedUnion)
                .Select(property => property.Name.CapitalizeFirstLetter());

            return properties
                .Where(property => property.IsDerived || property.IsDerivedUnion)
                .Select(property => property.Name.CapitalizeFirstLetter())
                .Except(nonDerivedNames)
                .ToHashSet(StringComparer.Ordinal);
        }

        private static string QuerySelectedPropertyName(string rule)
        {
            return RuleSelectorRegex().Match(rule).Groups[1].Value;
        }

        private static HashSet<string> QueryValidatedPropertyNames(IClass @class)
        {
            return DtoValidatorHelper.QueryRules(@class)
                .Select(QuerySelectedPropertyName)
                .ToHashSet(StringComparer.Ordinal);
        }

        [GeneratedRegex(@"dto => dto\.(\w+)")]
        private static partial Regex RuleSelectorRegex();

        private IClass QueryClass(string name)
        {
            return this.concreteClasses.Single(x => x.Name == name);
        }
    }
}
