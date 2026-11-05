// ------------------------------------------------------------------------------------------------
// <copyright file="SqlSchemaScriptsTestFixture.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Dal.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using NUnit.Framework;

    using SysML2.NET.Core;
    using SysML2.NET.Dal;

    [TestFixture]
    public class SqlSchemaScriptsTestFixture
    {
        [Test]
        public void VerifyGetAll()
        {
            var scripts = SqlSchemaScripts.GetAll();
            var names = scripts.Select(script => script.Name).ToList();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(names, Does.Contain("0001_initial.sql"));
                Assert.That(names, Has.All.Match(@"^\d{4}_[a-z0-9_.]+\.sql$"));
                Assert.That(names, Is.Ordered.Using((IComparer<string>)StringComparer.Ordinal));
                Assert.That(scripts.Select(script => script.Sql[0]), Has.None.EqualTo((char)0xFEFF));
                Assert.That(scripts.Select(script => script.Sql), Has.None.Contains("{{"));
            }

            var allSql = string.Concat(scripts.Select(script => script.Sql));

            var missingClassKinds = Enum.GetValues<ClassKind>()
                .Where(classKind => !allSql.Contains($"({(int)classKind}, '{classKind}',", StringComparison.Ordinal))
                .ToList();

            Assert.That(missingClassKinds, Is.Empty, "every compiled ClassKind must be seeded by the embedded schema scripts");
        }
    }
}
