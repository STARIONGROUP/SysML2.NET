// -------------------------------------------------------------------------------------------------
// <copyright file="EcoreModelInspectorTestFixture.cs" company="Starion Group S.A.">
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
    using System;
    
    using ECoreNetto;
    using ECoreNetto.Extensions;
    using ECoreNetto.Reporting.Generators;
    using Expected.Ecore.Core;
    using NUnit.Framework;

    using SysML2.NET.CodeGenerator;
    
    /// <summary>
    /// Suite of tests for the <see cref="ModelInspector"/> class.
    /// </summary>
    [TestFixture]
    public class EcoreModelInspectorTestFixture
    {
        private ModelInspector modelInspector;

        private EPackage rootPackage;

        [SetUp]
        public void SetUp()
        {
            this.rootPackage = DataModelLoader.Load();

            this.modelInspector = new ModelInspector();
        }

        [Test]
        public void Verify_that_inspects_executes_as_expected()
        {
            var report = this.modelInspector.Inspect(this.rootPackage, true);

            Assert.That(report, Is.Not.Empty);

            Console.WriteLine(report);
        }

        [Test, TestCaseSource(typeof(ExpectedConcreteClasses)), Category("Expected")]
        public void Verify_that_inspect_class_executes_as_expected(string className)
        {
            var report = this.modelInspector.Inspect(this.rootPackage, className);

            Assert.That(report, Is.Not.Empty);

            Console.WriteLine(report);
        }

        [Test]
        public void Verify_that_analyze_docs_executes_as_expected()
        {
            var report = this.modelInspector.AnalyzeDocumentation(this.rootPackage, true);

            Assert.That(report, Is.Not.Empty);

            Console.WriteLine(report);
        }
    }
}
