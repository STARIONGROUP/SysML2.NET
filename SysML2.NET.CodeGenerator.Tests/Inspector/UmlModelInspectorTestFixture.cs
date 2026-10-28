// ------------------------------------------------------------------------------------------------
// <copyright file="UmlModelInspectorTestFixture.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.CodeGenerator.Tests.Inspector
{
    using System.Collections.Generic;
    using System.IO;

    using Microsoft.Extensions.Logging;

    using NUnit.Framework;

    using Serilog;

    using uml4net.Reporting.Generators;

    [TestFixture]
    public class ModelInspectorTestFixture
    {
        private ILoggerFactory loggerFactory;

        private ModelInspector modelInspector;

        private string modelPath;

        private FileInfo modelFileInfo;

        private FileInfo reportFileInfo;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.Console()
                .CreateLogger();

            this.loggerFactory = LoggerFactory.Create(builder => { builder.AddSerilog(); });
        }

        [SetUp]
        public void SetUp()
        {
            var rootPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "datamodel");

            this.modelPath = Path.Combine(rootPath, "SysML_only_xmi.uml");
            this.modelFileInfo = new FileInfo(modelPath);

            var reportPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "uml-inspection-report.txt");
            this.reportFileInfo = new FileInfo(reportPath);
        }

        [Test]
        public void Verify_that_Inspection_report_can_be_generated()
        {
            this.modelInspector = new ModelInspector(this.loggerFactory);

            var rootPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "datamodel");

            var pathMaps = new Dictionary<string, string>
            {
                ["pathmap://UML_LIBRARIES/UMLPrimitiveTypes.library.uml"] =
                    Path.Combine(rootPath, "PrimitiveTypes.xmi")
            };

            Assert.That(() => this.modelInspector.GenerateReport(this.modelFileInfo, 
                this.modelFileInfo.Directory, "_h6bQED_xEfCL-qw9_9p9XQ", "SysML", 
                true, pathMaps, this.reportFileInfo), Throws.Nothing);
        }
    }
}