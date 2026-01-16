// -------------------------------------------------------------------------------------------------
// <copyright file="ApiSerializerTestFixture.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Serializer.Json.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Text.Json;
    using System.Threading;
    using System.Threading.Tasks;

    using NUnit.Framework;

    using SysML2.NET.Common;
    using SysML2.NET.PIM.DTO;
    using SysML2.NET.Serializer.Json;

    [TestFixture]
    public class ApiSerializerTestFixture
    {
        private ISerializer serializer;

        private Project project;

        private Branch branch;

        private Commit commit;

        [SetUp]
        public void SetUp()
        {
            this.serializer = new Serializer();

            this.CreateTestData();
        }

        private void CreateTestData()
        {
            this.project = new Project
            {
                Id = Guid.Parse("9b0e1914-3241-461e-b9ee-a3ff5120de4e"),
                Alias = new List<string> { "project alias 1", "project alias 2"},
                Created = new DateTime(1976, 8, 20),
                DefaultBranch = Guid.Parse("a910a705-7fbe-415f-9cbb-624bfadf6c20"),
                Description = "this is a description",
                Name = "test project",
                ResourceIdentifier = "http://www.stariongroup.eu/project",
            };

            this.commit = new Commit
            {
                Id = Guid.Parse("94e5b40e-741e-49ca-bd7f-f3138c071bf9"),
                Alias = new List<string> { "commit alias 1", "commit alias 2" },
                Created = new DateTime(1976, 8, 20),
                Description = "",
                OwningProject = Guid.Parse("9b0e1914-3241-461e-b9ee-a3ff5120de4e"),
                PreviousCommit = Guid.Empty,
                ResourceIdentifier = "http://www.stariongroup.eu/commit",
            };

            this.branch = new Branch
            {
                Id = Guid.Parse("a910a705-7fbe-415f-9cbb-624bfadf6c20"),
                Alias = null,
                Created = new DateTime(1976, 8, 20),
                Description = "branch description",
                Head = Guid.Parse("94e5b40e-741e-49ca-bd7f-f3138c071bf9"),
                Name = "branch name",
                OwningProject = Guid.Parse("9b0e1914-3241-461e-b9ee-a3ff5120de4e"),
                ResourceIdentifier = "http://http://www.stariongroup.eu/branch",
            };
        }

        [Test]
        [TestCase(false)]
        [TestCase(true)]
        public void Verify_that_PIM_IDatas_can_be_serialized(bool includeDerivedProperties)
        {
            var dataItems = new List<IData> { this.project, this.commit, this.branch };

            var stream = new MemoryStream();
            var jsonWriterOptions = new JsonWriterOptions { Indented = true };

            Assert.That(() => this.serializer.Serialize(dataItems, SerializationModeKind.JSON, includeDerivedProperties, stream, jsonWriterOptions), Throws.Nothing); ;

            var json = Encoding.UTF8.GetString(stream.ToArray());
            Console.WriteLine(json);
        }

        [Test]
        [TestCase(false)]
        [TestCase(true)]
        public void Verify_that_PIM_IData_can_be_serialized(bool includeDerivedProperties)
        {
            var jsonWriterOptions = new JsonWriterOptions { Indented = true };

            var stream = new MemoryStream();
            Assert.That(() => this.serializer.Serialize(this.project, SerializationModeKind.JSON, includeDerivedProperties, stream, jsonWriterOptions), Throws.Nothing);

            var json = Encoding.UTF8.GetString(stream.ToArray());
            Console.WriteLine(json);

            stream = new MemoryStream();
            Assert.That(() => this.serializer.Serialize(this.project, SerializationModeKind.JSON, includeDerivedProperties, stream, jsonWriterOptions), Throws.Nothing);

            json = Encoding.UTF8.GetString(stream.ToArray());
            Console.WriteLine(json);
        }

        [Test]
        [TestCase(false)]
        [TestCase(true)]
        public async Task Verify_that_PIM_IDatas_can_be_serialized_async(bool includeDerivedProperties)
        {
            var dataItems = new List<IData> { this.project, this.commit, this.branch };
            var stream = new MemoryStream();
            var jsonWriterOptions = new JsonWriterOptions { Indented = true };

            var cts = new CancellationTokenSource();

            await Assert.ThatAsync(() => this.serializer.SerializeAsync(dataItems, SerializationModeKind.JSON, includeDerivedProperties, stream, jsonWriterOptions, cts.Token), Throws.Nothing);

            var json = Encoding.UTF8.GetString(stream.ToArray());
            Console.WriteLine(json);
        }

        [Test]
        [TestCase(false)]
        [TestCase(true)]
        public async Task Verify_that_PIM_IData_can_be_serialized_async(bool includeDerivedProperties)
        {
            var stream = new MemoryStream();
            var jsonWriterOptions = new JsonWriterOptions { Indented = true };

            var cts = new CancellationTokenSource();

            await Assert.ThatAsync(() => this.serializer.SerializeAsync(this.project, SerializationModeKind.JSON, includeDerivedProperties, stream, jsonWriterOptions, cts.Token), Throws.Nothing);

            var json = Encoding.UTF8.GetString(stream.ToArray());
            Console.WriteLine(json);
        }
    }
}
