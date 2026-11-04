// ------------------------------------------------------------------------------------------------
// <copyright file="HttpMethodHelperTestFixture.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.CodeGenerator.Tests.OpenApiHandleBarHelpers
{
    using System;

    using HandlebarsDotNet;
    using HandlebarsDotNet.Helpers;

    using NUnit.Framework;

    using SysML2.NET.CodeGenerator.OpenApiHandleBarHelpers;

    [TestFixture]
    public class HttpMethodHelperTestFixture
    {
        [Test]
        public void VerifyRegisterHttpMethodHelper()
        {
            Assert.That(() => ((IHandlebars)null).RegisterHttpMethodHelper(), Throws.TypeOf<ArgumentNullException>());

            var handlebars = Handlebars.CreateSharedEnvironment();
            HandlebarsHelpers.Register(handlebars);
            handlebars.RegisterHttpMethodHelper();

            var template = handlebars.Compile("{{OpenApi.HttpMethod Method}}");

            using (Assert.EnterMultipleScope())
            {
                Assert.That(template(new { Method = "GET" }), Is.EqualTo("Get"));
                Assert.That(template(new { Method = "POST" }), Is.EqualTo("Post"));
                Assert.That(template(new { Method = "PUT" }), Is.EqualTo("Put"));
                Assert.That(template(new { Method = "DELETE" }), Is.EqualTo("Delete"));
                Assert.That(template(new { Method = "HEAD" }), Is.EqualTo("Head"));
                Assert.That(template(new { Method = "OPTIONS" }), Is.EqualTo("Options"));
                Assert.That(template(new { Method = "TRACE" }), Is.EqualTo("Trace"));
                Assert.That(template(new { Method = "get" }), Is.EqualTo("Get"));
                Assert.That(() => template(new { Method = "PATCH" }), Throws.TypeOf<HandlebarsException>());
                Assert.That(() => template(new { Method = (string)null }), Throws.TypeOf<HandlebarsException>());
                Assert.That(() => handlebars.Compile("{{OpenApi.HttpMethod}}")(new { }), Throws.TypeOf<HandlebarsException>());
            }
        }
    }
}
