// -------------------------------------------------------------------------------------------------
// <copyright file="HtmlReportGeneratorTestFixture.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.CodeGenerator.Tests.Generators
{
    using System.Collections.Generic;
    using System.IO;

    using Microsoft.Extensions.Logging;
    
    using NUnit.Framework;

    using Serilog;

    using uml4net.Reporting.Drawing;
    using uml4net.Reporting.Generators;
    using uml4net.xmi;
    using uml4net.xmi.Readers;

    public class HtmlReportGeneratorTestFixture
    {
        private FileInfo modelFileInfo;

        private FileInfo outputFileInfo;

        private DirectoryInfo rootDirectoryInfo;

        private HtmlReportGenerator htmlReportGenerator;

        private ILoggerFactory loggerFactory;

        private Dictionary<string, string> pathMaps;

        private string rootDirectory;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .CreateLogger();

            this.loggerFactory = LoggerFactory.Create(builder => { builder.AddSerilog(); });
        }

        [SetUp]
        public void SetUp()
        {
            this.rootDirectory = Path.Combine(TestContext.CurrentContext.TestDirectory, "datamodel");

            this.rootDirectoryInfo = new DirectoryInfo(this.rootDirectory);

            this.pathMaps = new Dictionary<string, string>
            {
                ["pathmap://UML_LIBRARIES/UMLPrimitiveTypes.library.uml"] =
                    Path.Combine(this.rootDirectory, "PrimitiveTypes.xmi")
            };

            this.modelFileInfo = new FileInfo(Path.Combine(TestContext.CurrentContext.TestDirectory, "datamodel",
                "SysML_xmi.uml"));

            this.outputFileInfo = new FileInfo(Path.Combine(TestContext.CurrentContext.TestDirectory, "UML", "_SysML2.NET.Core.AutoGenHtmlDocs", "index.html"));

            var inheritanceDiagramRenderer = new InheritanceDiagramRenderer(loggerFactory.CreateLogger<InheritanceDiagramRenderer>());
            this.htmlReportGenerator = new HtmlReportGenerator(inheritanceDiagramRenderer, this.loggerFactory);
        }

        [Test]
        public void verify_HTML_docs_Are_generated()
        {
            var customHtml = """
                             <div style="text-align: center;">
                             <H1>OMG SysML&#174; Version 2 <a href="https://github.com/Systems-Modeling/SysML-v2-Pilot-Implementation/blob/master/org.omg.sysml/model/SysML_xmi.uml" target="_blank" rel="noopener noreferrer">UML based Meta Model Documentation</a></H1>
                             <H3><a href="https://github.com/Systems-Modeling/SysML-v2-Pilot-Implementation/releases/tag/2025-07" target="_blank" rel="noopener noreferrer">Release 2025-07</a></H3>
                             <p class="small">Powered By <a href="https://www.stariongroup.eu" target="_blank" rel="noopener noreferrer">Starion Group</a>, 2022-2025</p>
                             <div>
                             """;

            Assert.That(() => this.htmlReportGenerator.GenerateReport(this.modelFileInfo, this.rootDirectoryInfo, false, this.pathMaps,
            this.outputFileInfo, customHtml),
            Throws.Nothing);
        }
    }
}