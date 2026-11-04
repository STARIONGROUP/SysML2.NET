// ------------------------------------------------------------------------------------------------
// <copyright file="QueryStringBuilderTestFixture.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SySML2.NET.REST.Tests
{
    using System;

    using NUnit.Framework;

    using SysML2.NET.PSM;

    [TestFixture]
    public class QueryStringBuilderTestFixture
    {
        private static readonly Guid FirstCommitId = Guid.Parse("6d7ad9fd-6520-4ff2-885b-8c5c129e6c27");

        private static readonly Guid SecondCommitId = Guid.Parse("2f1a0e1c-6b2a-4d5e-9c3f-8a7b6d5e4c3b");

        private static readonly Guid[] NoCommitIds = [];

        private static readonly Guid[] TwoCommitIds = [FirstCommitId, SecondCommitId];

        [Test]
        public void VerifyConstructor()
        {
            Assert.That(new QueryStringBuilder().ToString(), Is.Empty);

            Assert.That(new QueryStringBuilder(new QueryParameters()).ToString(), Is.Empty);

            var page = new Page { Epoch = 1618861496632, Identifier = FirstCommitId };

            var builder = new QueryStringBuilder(new QueryParameters { PageSize = 2, PageAfter = page, PageBefore = page });

            Assert.That(builder.ToString(), Is.EqualTo($"?page[size]=2&page[after]={Uri.EscapeDataString(page.ToString())}&page[before]={Uri.EscapeDataString(page.ToString())}"));
        }

        [Test]
        public void VerifyAdd()
        {
            var builder = new QueryStringBuilder();

            builder.Add("commitId", null);
            builder.Add("sourceCommitId", NoCommitIds);

            Assert.That(builder.ToString(), Is.Empty);

            builder.Add("excludeUsed", false);
            builder.Add("sourceCommitId", TwoCommitIds);
            builder.Add("description", "the merge, & its description");
            builder.Add("pageSize", 1.5);

            Assert.That(builder.ToString(), Is.EqualTo($"?excludeUsed=false&sourceCommitId={FirstCommitId}%2C{SecondCommitId}&description=the%20merge%2C%20%26%20its%20description&pageSize=1.5"));
        }

        [Test]
        public void VerifyToString()
        {
            var builder = new QueryStringBuilder();

            Assert.That(builder.ToString(), Is.Empty);

            builder.Add("excludeUsed", true);

            Assert.That(builder.ToString(), Is.EqualTo("?excludeUsed=true"));
        }
    }
}
