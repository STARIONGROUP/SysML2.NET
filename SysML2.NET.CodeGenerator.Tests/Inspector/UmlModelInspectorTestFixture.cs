// -------------------------------------------------------------------------------------------------
// <copyright file="UmlModelInspectorTestFixture.cs" company="Starion Group S.A.">
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
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.Console()
                .CreateLogger();
        }

        [Test]
        public void Verify_that_Inspection_report_can_be_generated()
        {
            var rootPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "datamodel");

            var modelPath = Path.Combine(rootPath, "SysML_xmi.uml");
            var modelFileInfo = new FileInfo(modelPath);

            var reportPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "uml-inspection-report.txt");
            var reportFileInfo = new FileInfo(reportPath);
            var modelInspector = new ModelInspector( LoggerFactory.Create(builder => { builder.AddSerilog(); }));

            var pathMaps = new Dictionary<string, string>
            {
                ["pathmap://UML_LIBRARIES/UMLPrimitiveTypes.library.uml"] =
                    Path.Combine(rootPath, "PrimitiveTypes.xmi")
            };

            Assert.That(() => modelInspector.GenerateReport(modelFileInfo, 
                modelFileInfo.Directory, "_h6bQED_xEfCL-qw9_9p9XQ", "SysML", 
                true, pathMaps, reportFileInfo), Throws.Nothing);
        }

        [Test]
        public void VerifyInspectionReportForPim()
        {
            var rootPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "datamodel");

            var modelPath = Path.Combine(rootPath, "SysML_PIM.xmi");
            var modelFileInfo = new FileInfo(modelPath);

            var reportPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "uml-pim-inspection-report.txt");
            var reportFileInfo = new FileInfo(reportPath);
            var modelInspector = new ModelInspector( LoggerFactory.Create(builder => { builder.AddSerilog(); }));

            var pathMaps = new Dictionary<string, string>
            {
                ["pathmap://UML_LIBRARIES/UMLPrimitiveTypes.library.uml"] =
                    Path.Combine(rootPath, "PrimitiveTypes.xmi")
            };

            Assert.That(() => modelInspector.GenerateReport(modelFileInfo, 
                modelFileInfo.Directory, "_19_0_4_3fa0198_1689000259946_865221_0", "Systems Modeling API and Services PIM", 
                true, pathMaps, reportFileInfo), Throws.Nothing);
        }
    }
}