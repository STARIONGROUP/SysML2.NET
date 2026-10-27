// ------------------------------------------------------------------------------------------------
// <copyright file="GeneratorSetupFixture.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.CodeGenerator.Tests.Generators.UmlHandleBarsGenerators
{
    using System.Collections.Generic;
    using System.IO;

    using Microsoft.Extensions.Logging;

    using NUnit.Framework;

    using Serilog;

    using uml4net.xmi;
    using uml4net.xmi.Readers;

    [SetUpFixture]
    public class GeneratorSetupFixture
    {
        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .CreateLogger();

            var loggerFactory = LoggerFactory.Create(builder => { builder.AddSerilog(); });

            var rootPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "datamodel");

            var pathMaps = new Dictionary<string, string>
            {
                ["pathmap://UML_LIBRARIES/UMLPrimitiveTypes.library.uml"] =
                    Path.Combine(rootPath, "PrimitiveTypes.xmi")
            };

            var reader = XmiReaderBuilder.Create()
                .UsingSettings(x =>
                {
                    x.PathMaps = pathMaps;
                    x.LocalReferenceBasePath = rootPath;
                })
                .WithLogger(loggerFactory)
                .Build();

            XmiReaderResult = reader.Read(Path.Combine(TestContext.CurrentContext.TestDirectory, "datamodel",
                "SysML_only_xmi.uml"));
        }

        public static XmiReaderResult XmiReaderResult;
    }
}
