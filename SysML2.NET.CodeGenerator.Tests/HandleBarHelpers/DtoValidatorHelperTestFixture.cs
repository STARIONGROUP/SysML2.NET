// ------------------------------------------------------------------------------------------------
// <copyright file="DtoValidatorHelperTestFixture.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.CodeGenerator.Tests.Generators.UmlHandleBarsGenerators
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    using NUnit.Framework;

    using SysML2.NET.CodeGenerator.Extensions;
    using SysML2.NET.CodeGenerator.HandleBarHelpers;

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
        public void VerifyQueryRules()
        {
            Assert.That(() => DtoValidatorHelper.QueryRules(null), Throws.TypeOf<ArgumentNullException>());

            var membership = this.concreteClasses.Single(x => x.Name == "Membership");
            var owningMembership = this.concreteClasses.Single(x => x.Name == "OwningMembership");

            using (Assert.EnterMultipleScope())
            {
                Assert.That(QueryValidatedPropertyNames(membership), Does.Contain("MemberElement"));
                Assert.That(QueryValidatedPropertyNames(owningMembership), Does.Not.Contain("MemberElement"));
                Assert.That(QueryValidatedPropertyNames(owningMembership), Does.Not.Contain("OwnedMemberElement"));
                Assert.That(QueryValidatedPropertyNames(membership), Does.Not.Contain("OwnedRelatedElement"));
                Assert.That(QueryValidatedPropertyNames(membership), Does.Not.Contain("OwningRelatedElement"));
                Assert.That(DtoValidatorHelper.QueryRules(owningMembership), Does.Contain("this.RuleFor(dto => dto.OwnedRelatedElement).Cascade(CascadeMode.Stop).NotEmpty();"));
            }

            var violations = this.concreteClasses
                .SelectMany(@class => QueryValidatedPropertyNames(@class)
                    .Intersect(QueryDerivedPropertyNames(@class))
                    .Select(propertyName => $"{@class.Name}.{propertyName}"))
                .ToList();

            Assert.That(violations, Is.Empty);
        }

        private static HashSet<string> QueryValidatedPropertyNames(IClass @class)
        {
            return DtoValidatorHelper.QueryRules(@class)
                .Select(rule => RuleSelectorRegex().Match(rule).Groups[1].Value)
                .ToHashSet(StringComparer.Ordinal);
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

        [GeneratedRegex(@"dto => dto\.(\w+)")]
        private static partial Regex RuleSelectorRegex();
    }
}
