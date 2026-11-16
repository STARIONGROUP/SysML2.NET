// ------------------------------------------------------------------------------------------------
// <copyright file="ScenarioQueriesTestFixture.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SySML2.NET.REST.Tests.HelpersTests
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    using NUnit.Framework;

    using SySML2.NET.REST.Tests.Helpers;

    using SysML2.NET.Core.DTO.Kernel.Expressions;
    using SysML2.NET.Core.DTO.Kernel.Packages;
    using SysML2.NET.Core.DTO.Root.Dependencies;
    using SysML2.NET.Core.DTO.Root.Namespaces;
    using SysML2.NET.PSM.DTO;
    using SysML2.NET.PSM.Enumerations;

    [TestFixture]
    public class ScenarioQueriesTestFixture
    {
        [Test]
        public void VerifyAtLeast42()
        {
            var where = (CompositeConstraintRequest)ScenarioQueries.AtLeast42.Where;

            using (Assert.EnterMultipleScope())
            {
                Assert.That(where.Operator, Is.EqualTo(CompositeConstraintRequestOperator.and));
                Assert.That(Describe(where), Is.EqualTo((string[])["@type equalto False [LiteralInteger]", "value greaterthanorequalto False [42]"]));
                Assert.That(ScenarioQueries.AtLeast42.Select, Is.Empty, "the results are complete elements");
            }
        }

        [Test]
        public void VerifyConstraintText()
        {
            var stored = (CompositeConstraintRequest)ScenarioQueries.Stored.Where;
            var flipped = (PrimitiveConstraint)stored.Constraint[1];

            using (Assert.EnterMultipleScope())
            {
                Assert.That(ScenarioQueries.ConstraintText(stored), Is.EqualTo("or(and(@type equalto [\"LiteralInteger\"], value greaterthan [1]), @type in [\"Dependency\", \"LiteralRational\"])"));
                Assert.That(ScenarioQueries.ConstraintText(new CompositeConstraint { Operator = CompositeConstraintOperator.or, Constraint = stored.Constraint }), Is.EqualTo(ScenarioQueries.ConstraintText(stored)), "a stored constraint reads as the request it came from");
                Assert.That(ScenarioQueries.ConstraintText(ScenarioQueries.Updated.Where), Does.Contain("not value lessthan [2]"));
                Assert.That(ScenarioQueries.ConstraintText(ScenarioQueries.Namespaces.Where), Is.EqualTo("@type instanceOf [\"Namespace\"]"));
                Assert.That(ScenarioQueries.ConstraintText(ScenarioQueries.NamedWithoutInverse("x").Where), Is.EqualTo(ScenarioQueries.ConstraintText(ScenarioQueries.Named("x").Where)), "an unset inverse reads as false");
                Assert.That(ScenarioQueries.ConstraintText(new PrimitiveConstraint { Property = flipped.Property, Operator = flipped.Operator, Inverse = true, Value = flipped.Value }), Is.Not.EqualTo(ScenarioQueries.ConstraintText(flipped)));
                Assert.That(ScenarioQueries.ConstraintText(null), Is.EqualTo("none"));
                Assert.That(ScenarioQueries.ConstraintText("no constraint"), Is.EqualTo("none"));
            }
        }

        [Test]
        public void VerifyIsAtLeast42()
        {
            using (Assert.EnterMultipleScope())
            {
                Assert.That(ScenarioQueries.IsAtLeast42(new LiteralInteger { Value = 42 }), Is.True);
                Assert.That(ScenarioQueries.IsAtLeast42(new LiteralInteger { Value = 41 }), Is.False);
                Assert.That(ScenarioQueries.IsAtLeast42(new LiteralRational { Value = 50 }), Is.False);
            }
        }

        [Test]
        public void VerifyIsNamespace()
        {
            using (Assert.EnterMultipleScope())
            {
                Assert.That(ScenarioQueries.IsNamespace(new Namespace()), Is.True);
                Assert.That(ScenarioQueries.IsNamespace(new Package()), Is.True, "a package is a namespace");
                Assert.That(ScenarioQueries.IsNamespace(new Dependency()), Is.False);
            }
        }

        [Test]
        public void VerifyIsStoredResult()
        {
            using (Assert.EnterMultipleScope())
            {
                Assert.That(ScenarioQueries.IsStoredResult(new LiteralInteger { Value = 2 }), Is.True);
                Assert.That(ScenarioQueries.IsStoredResult(new LiteralInteger { Value = 1 }), Is.False);
                Assert.That(ScenarioQueries.IsStoredResult(new Dependency()), Is.True);
                Assert.That(ScenarioQueries.IsStoredResult(new LiteralRational { Value = 0 }), Is.True);
                Assert.That(ScenarioQueries.IsStoredResult(new Package()), Is.False);
            }
        }

        [Test]
        public void VerifyIsUpdatedResult()
        {
            using (Assert.EnterMultipleScope())
            {
                Assert.That(ScenarioQueries.IsUpdatedResult(new LiteralInteger { Value = 2 }), Is.True);
                Assert.That(ScenarioQueries.IsUpdatedResult(new LiteralInteger { Value = 42 }), Is.True);
                Assert.That(ScenarioQueries.IsUpdatedResult(new LiteralInteger { Value = 1 }), Is.False);
                Assert.That(ScenarioQueries.IsUpdatedResult(new LiteralInteger { Value = 43 }), Is.False);
                Assert.That(ScenarioQueries.IsUpdatedResult(new LiteralRational { Value = 3 }), Is.False);
            }
        }

        [Test]
        public void VerifyNamed()
        {
            var query = ScenarioQueries.Named("Systems");
            var where = (PrimitiveConstraintRequest)query.Where;

            using (Assert.EnterMultipleScope())
            {
                Assert.That(query.Select, Is.EqualTo((string[])["@id", "declaredName"]), "the select leaves out @type");
                Assert.That(where.Property, Is.EqualTo("declaredName"));
                Assert.That(where.Operator, Is.EqualTo(PrimitiveConstraintRequestOperator.equalto));
                Assert.That(where.Value, Is.EqualTo((ConstraintValue[])["Systems"]));
                Assert.That(where.Inverse, Is.False);
            }
        }

        [Test]
        public void VerifyNamedWithoutInverse()
        {
            var where = (PrimitiveConstraintRequest)ScenarioQueries.NamedWithoutInverse("Systems").Where;

            using (Assert.EnterMultipleScope())
            {
                Assert.That(where.Property, Is.EqualTo("declaredName"));
                Assert.That(where.Operator, Is.EqualTo(PrimitiveConstraintRequestOperator.equalto));
                Assert.That(where.Value, Is.EqualTo((ConstraintValue[])["Systems"]));
                Assert.That(where.Inverse, Is.Null);
            }
        }

        [Test]
        public void VerifyNamespaces()
        {
            var where = (PrimitiveConstraintRequest)ScenarioQueries.Namespaces.Where;

            using (Assert.EnterMultipleScope())
            {
                Assert.That(where.Property, Is.EqualTo("@type"));
                Assert.That(where.Operator, Is.EqualTo(PrimitiveConstraintRequestOperator.instanceOf));
                Assert.That(where.Value, Is.EqualTo((ConstraintValue[])["Namespace"]));
            }
        }

        [Test]
        public void VerifyStored()
        {
            var query = ScenarioQueries.Stored;
            var where = (CompositeConstraintRequest)query.Where;

            using (Assert.EnterMultipleScope())
            {
                Assert.That(query.Name, Is.Not.Empty);
                Assert.That(query.Description, Is.Not.Empty);
                Assert.That(query.Alias, Is.Not.Empty);
                Assert.That(where.Operator, Is.EqualTo(CompositeConstraintRequestOperator.or));
                Assert.That(where.Constraint.OfType<CompositeConstraint>().Select(nested => nested.Operator), Is.EqualTo((CompositeConstraintOperator[])[CompositeConstraintOperator.and]), "an and nested in the or");
                Assert.That(where.Constraint.OfType<CompositeConstraint>().Select(Describe), Is.EqualTo((string[][])[["@type equalto False [LiteralInteger]", "value greaterthan False [1]"]]), "the and holds the integer literals above 1");
                Assert.That(where.Constraint.OfType<PrimitiveConstraint>().Select(Describe), Is.EqualTo((string[])["@type in False [Dependency, LiteralRational]"]), "the or holds the dependencies and rational literals directly");
                Assert.That(ScenarioQueries.Stored, Is.Not.SameAs(query), "every read gives a new request");
            }
        }

        [Test]
        public void VerifyUpdated()
        {
            var where = (CompositeConstraintRequest)ScenarioQueries.Updated.Where;

            using (Assert.EnterMultipleScope())
            {
                Assert.That(where.Operator, Is.EqualTo(CompositeConstraintRequestOperator.and));
                Assert.That(Describe(where), Is.EqualTo((string[])["@type equalto False [LiteralInteger]", "value lessthan True [2]", "value lessthanorequalto False [42]"]), "the lower bound is an inverted less than");
            }
        }

        private static IEnumerable<string> Describe(CompositeConstraintRequest where) => where.Constraint.OfType<PrimitiveConstraint>().Select(Describe);

        private static IEnumerable<string> Describe(CompositeConstraint composite) => composite.Constraint.OfType<PrimitiveConstraint>().Select(Describe);

        private static string Describe(PrimitiveConstraint constraint) => $"{constraint.Property} {constraint.Operator} {constraint.Inverse} [{string.Join(", ", constraint.Value.Select(value => value.Match(() => "null", flag => flag.ToString(), number => number.ToString(CultureInfo.InvariantCulture), text => text, id => id.ToString())))}]";
    }
}
