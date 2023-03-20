// -------------------------------------------------------------------------------------------------
// <copyright file="ApiSerializerTestFixture.cs" company="RHEA System S.A.">
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

namespace SysML2.NET.Serializer.Json.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Text.Json;
    using System.Threading;

    using NUnit.Framework;

    using SysML2.NET.Common;
    using SysML2.NET.PIM.DTO;

    [TestFixture]
    public class ApiSerializerTestFixture
    {
        private ISerializer serializer;

        private Project project;

        private Branch branch;

        private Commit commit;

        private Tag tag;

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
                DefaultBranch = Guid.Parse("a910a705-7fbe-415f-9cbb-624bfadf6c20"),
                Description = "this is a description",
                Name = "test project",
            };

            this.commit = new Commit
            {
                Id = Guid.Parse("94e5b40e-741e-49ca-bd7f-f3138c071bf9"),
                Description = "",
                OwningProject = Guid.Parse("9b0e1914-3241-461e-b9ee-a3ff5120de4e"),
                PreviousCommit = null,
                TimeStamp = new DateTime(1976, 8, 20),
            };

            this.branch = new Branch
            {
                Id = Guid.Parse("a910a705-7fbe-415f-9cbb-624bfadf6c20"),
                Alias = new List<string> { "branch alias 1"},
                CreationTimestamp = new DateTime(1976, 8, 20),
                Description = "branch description",
                ReferencedCommit = Guid.Parse("94e5b40e-741e-49ca-bd7f-f3138c071bf9"),
                TimeStamp = new DateTime(1976, 8, 20)
            };
        }

        [Test]
        public void Verify_that_PIM_IDatas_can_be_serialized()
        {
            var dataItems = new List<IData> { this.project, this.commit, this.branch };

            var stream = new MemoryStream();
            var jsonWriterOptions = new JsonWriterOptions { Indented = true };

            Assert.That(() => this.serializer.Serialize(dataItems, SerializationModeKind.JSON, stream, jsonWriterOptions), Throws.Nothing); ;

            var json = Encoding.UTF8.GetString(stream.ToArray());
            Console.WriteLine(json);
        }

        [Test]
        public void Verify_that_PIM_IData_can_be_serialized()
        {
            var jsonWriterOptions = new JsonWriterOptions { Indented = true };

            var stream = new MemoryStream();
            Assert.That(() => this.serializer.Serialize(this.project, SerializationModeKind.JSON, stream, jsonWriterOptions), Throws.Nothing);

            var json = Encoding.UTF8.GetString(stream.ToArray());
            Console.WriteLine(json);

            stream = new MemoryStream();
            Assert.That(() => this.serializer.Serialize(this.project, SerializationModeKind.JSON, stream, jsonWriterOptions), Throws.Nothing);

            json = Encoding.UTF8.GetString(stream.ToArray());
            Console.WriteLine(json);
        }

        [Test]
        public void Verify_that_PIM_IDatas_can_be_serialized_async()
        {
            var dataItems = new List<IData> { this.project, this.commit, this.branch };
            var stream = new MemoryStream();
            var jsonWriterOptions = new JsonWriterOptions { Indented = true };

            var cts = new CancellationTokenSource();

            Assert.That(async () => await this.serializer.SerializeAsync(dataItems, SerializationModeKind.JSON, stream, jsonWriterOptions, cts.Token), Throws.Nothing); ;

            var json = Encoding.UTF8.GetString(stream.ToArray());
            Console.WriteLine(json);
        }

        [Test]
        public void Verify_that_PIM_IData_can_be_serialized_async()
        {
            var stream = new MemoryStream();
            var jsonWriterOptions = new JsonWriterOptions { Indented = true };

            var cts = new CancellationTokenSource();

            Assert.That(async () => await this.serializer.SerializeAsync(this.project, SerializationModeKind.JSON, stream, jsonWriterOptions, cts.Token), Throws.Nothing); ;

            var json = Encoding.UTF8.GetString(stream.ToArray());
            Console.WriteLine(json);
        }
    }
}
