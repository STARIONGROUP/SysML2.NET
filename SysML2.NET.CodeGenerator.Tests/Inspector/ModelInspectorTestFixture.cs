// -------------------------------------------------------------------------------------------------
// <copyright file="ModelInspectorTestFixture.cs" company="RHEA System S.A.">
// 
//   Copyright 2022-2023 RHEA System S.A.
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
    using System.Linq;

    using ECoreNetto;

    using NUnit.Framework;

    using SysML2.NET.CodeGenerator;
    using SysML2.NET.CodeGenerator.Inspector;

    /// <summary>
    /// Suite of tests for the <see cref="ModelInspector"/> class.
    /// </summary>
    [TestFixture]
    public class ModelInspectorTestFixture
    {
        private ModelInspector modelInspector;

        [SetUp]
        public void SetUp()
        {
            this.modelInspector = new ModelInspector();
        }

        [Test]
        public void Verify_that_inspects_executes_as_expected()
        {
            Assert.That(() => this.modelInspector.Inspect(), Throws.Nothing);
        }

        [Test, TestCaseSource(typeof(Expected.ExpectedConcreteClasses))]
        public void Verify_that_inspect_class_executes_as_expected(string className)
        {
            Assert.That(() => this.modelInspector.Inspect(className), Throws.Nothing);
        }
    }
}
