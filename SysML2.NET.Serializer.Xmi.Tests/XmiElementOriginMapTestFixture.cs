// -------------------------------------------------------------------------------------------------
// <copyright file="XmiElementOriginMapTestFixture.cs" company="Starion Group S.A.">
//
//   Copyright 2022-2026 Starion Group S.A.
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

namespace SysML2.NET.Serializer.Xmi.Tests
{
    using System;
    using System.Linq;

    [TestFixture]
    public class XmiElementOriginMapTestFixture
    {
        private XmiElementOriginMap originMap;

        [SetUp]
        public void Setup()
        {
            this.originMap = new XmiElementOriginMap();
        }

        [Test]
        public void Verify_that_element_can_be_registered_and_retrieved()
        {
            var elementId = Guid.NewGuid();
            var sourceFile = new Uri("file:///C:/test/model.sysmlx");

            this.originMap.Register(elementId, sourceFile);

            Assert.That(this.originMap.GetSourceFile(elementId), Is.EqualTo(sourceFile));
        }

        [Test]
        public void Verify_that_GetSourceFile_returns_null_for_unregistered_element()
        {
            var elementId = Guid.NewGuid();

            Assert.That(this.originMap.GetSourceFile(elementId), Is.Null);
        }

        [Test]
        public void Verify_that_GetElementsInFile_returns_correct_elements()
        {
            var sourceFile = new Uri("file:///C:/test/model.sysmlx");
            var element1 = Guid.NewGuid();
            var element2 = Guid.NewGuid();
            var element3 = Guid.NewGuid();
            var otherFile = new Uri("file:///C:/test/other.sysmlx");

            this.originMap.Register(element1, sourceFile);
            this.originMap.Register(element2, sourceFile);
            this.originMap.Register(element3, otherFile);

            var elementsInFile = this.originMap.GetElementsInFile(sourceFile).ToList();

            Assert.That(elementsInFile, Has.Count.EqualTo(2));
            Assert.That(elementsInFile, Does.Contain(element1));
            Assert.That(elementsInFile, Does.Contain(element2));
            Assert.That(elementsInFile, Does.Not.Contain(element3));
        }

        [Test]
        public void Verify_that_GetAllSourceFiles_returns_distinct_files()
        {
            var file1 = new Uri("file:///C:/test/model1.sysmlx");
            var file2 = new Uri("file:///C:/test/model2.sysmlx");

            this.originMap.Register(Guid.NewGuid(), file1);
            this.originMap.Register(Guid.NewGuid(), file1);
            this.originMap.Register(Guid.NewGuid(), file2);

            var allFiles = this.originMap.GetAllSourceFiles().ToList();

            Assert.That(allFiles, Has.Count.EqualTo(2));
            Assert.That(allFiles, Does.Contain(file1));
            Assert.That(allFiles, Does.Contain(file2));
        }

        [Test]
        public void Verify_that_root_namespace_can_be_registered_and_retrieved()
        {
            var sourceFile = new Uri("file:///C:/test/model.sysmlx");
            var rootNamespaceId = Guid.NewGuid();

            this.originMap.RegisterRootNamespace(sourceFile, rootNamespaceId);

            Assert.That(this.originMap.GetRootNamespaceId(sourceFile), Is.EqualTo(rootNamespaceId));
        }

        [Test]
        public void Verify_that_GetRootNamespaceId_returns_empty_guid_for_unregistered_file()
        {
            var sourceFile = new Uri("file:///C:/test/model.sysmlx");

            Assert.That(this.originMap.GetRootNamespaceId(sourceFile), Is.EqualTo(Guid.Empty));
        }

        [Test]
        public void Verify_that_registering_same_element_overwrites_source_file()
        {
            var elementId = Guid.NewGuid();
            var file1 = new Uri("file:///C:/test/model1.sysmlx");
            var file2 = new Uri("file:///C:/test/model2.sysmlx");

            this.originMap.Register(elementId, file1);
            this.originMap.Register(elementId, file2);

            Assert.That(this.originMap.GetSourceFile(elementId), Is.EqualTo(file2));
        }
    }
}
