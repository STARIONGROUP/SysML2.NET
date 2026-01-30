// -------------------------------------------------------------------------------------------------
// <copyright file="GeneratorSetupFixture.cs" company="Starion Group S.A.">
// 
//   Copyright 2022-2025 Starion Group S.A.
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
                "SysML_xmi.uml"));
        }

        public static XmiReaderResult XmiReaderResult;
    }
}
