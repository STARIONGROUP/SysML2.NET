// -------------------------------------------------------------------------------------------------
// <copyright file="DeSerializerTestFixture.cs" company="RHEA System S.A.">
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
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using NUnit.Framework;

    using SysML2.NET.PIM.DTO;
    using SysML2.NET.Core.DTO;
    using SysML2.NET.Serializer.Json;
    using SysML2.NET.Core;

    /// <summary>
    /// Suite of tests for the <see cref="DeSerializer"/>
    /// </summary>
    [TestFixture]
    public class DeSerializerTestFixture
    {
        private IDeSerializer deSerializer;

        [SetUp]
        public void SetUp()
        {
            this.deSerializer = new DeSerializer();
        }
        
        [Test]
        public void Verify_that_idada_from_sysmlcore_json_can_be_deserialized()
        {
            var fileName = Path.Combine(TestContext.CurrentContext.WorkDirectory, "Data", "projects.000e9890-6935-43e6-a5d7-5d7cac601f4c.commits.6d7ad9fd-6520-4ff2-885b-8c5c129e6c27.elements.json");
            using (var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                var data = this.deSerializer.DeSerialize(stream, SerializationModeKind.JSON, SerializationTargetKind.CORE);

                Assert.That(data.Count(), Is.EqualTo(100));

                Assert.That(data.OfType<IFeature>().Count(), Is.EqualTo(30));

                var feature = data.OfType<IFeature>().Single(x => x.Id == Guid.Parse("00a6ef10-d3dc-4741-9029-2c9978c2f083"));

                Assert.That(feature.AliasIds, Is.Empty);
                Assert.That(feature.ElementId, Is.EqualTo("00a6ef10-d3dc-4741-9029-2c9978c2f083"));
                Assert.That(feature.IsAbstract, Is.False);
                Assert.That(feature.IsComposite, Is.False);
                Assert.That(feature.IsSufficient, Is.False);
                Assert.That(feature.IsEnd, Is.False);
                Assert.That(feature.IsUnique, Is.True);
                Assert.That(feature.DeclaredName, Is.Null);
                Assert.That(feature.OwnedRelationship, Is.Empty);
                Assert.That(feature.OwningRelationship, Is.EqualTo(Guid.Parse("8a780d8b-61a6-472b-8b80-2564aa9f7c36")));
                Assert.That(feature.DeclaredShortName, Is.Null);
                Assert.That(feature.Direction, Is.EqualTo(FeatureDirectionKind.Out));
            }
        }

        [Test]
        public async Task Verify_that_idada_from_sysmlcore_json_can_be_deserialized_async()
        {
            var fileName = Path.Combine(TestContext.CurrentContext.WorkDirectory, "Data", "projects.000e9890-6935-43e6-a5d7-5d7cac601f4c.commits.6d7ad9fd-6520-4ff2-885b-8c5c129e6c27.elements.json");
            using (var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                var cts = new CancellationTokenSource();

                var data = await this.deSerializer.DeSerializeAsync(stream, SerializationModeKind.JSON, SerializationTargetKind.CORE, cts.Token);

                Assert.That(data.Count(), Is.EqualTo(100));

                Assert.That(data.OfType<IFeature>().Count(), Is.EqualTo(30));

                var feature = data.OfType<IFeature>().Single(x => x.Id == Guid.Parse("00a6ef10-d3dc-4741-9029-2c9978c2f083"));

                Assert.That(feature.AliasIds, Is.Empty);
                Assert.That(feature.ElementId, Is.EqualTo("00a6ef10-d3dc-4741-9029-2c9978c2f083"));
                Assert.That(feature.IsAbstract, Is.False);
                Assert.That(feature.IsComposite, Is.False);
                Assert.That(feature.IsSufficient, Is.False);
                Assert.That(feature.IsEnd, Is.False);
                Assert.That(feature.IsUnique, Is.True);
                Assert.That(feature.DeclaredName, Is.Null);
                Assert.That(feature.OwnedRelationship, Is.Empty);
                Assert.That(feature.OwningRelationship, Is.EqualTo(Guid.Parse("8a780d8b-61a6-472b-8b80-2564aa9f7c36")));
                Assert.That(feature.DeclaredShortName, Is.Null);
                Assert.That(feature.Direction, Is.EqualTo(FeatureDirectionKind.Out));
            }
        }

        [Test]
        public void Verify_that_projects_from_restapi_json_can_be_deserialized()
        {
            var fileName = Path.Combine(TestContext.CurrentContext.WorkDirectory, "Data", "projects.json");
            using (var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                var data = this.deSerializer.DeSerialize(stream, SerializationModeKind.JSON, SerializationTargetKind.PSM);

                Assert.That(data.Count(), Is.EqualTo(43));

                var project = data.OfType<Project>().Single(x => x.Id == Guid.Parse("000e9890-6935-43e6-a5d7-5d7cac601f4c"));

                Assert.That(project.DefaultBranch, Is.EqualTo(Guid.Parse("c294a463-6c9c-47a8-b592-01252c5ab2a7")));
                Assert.That(project.Name, Is.EqualTo("7b-Variant Configurations Mon Mar 13 17:54:29 EDT 2023"));
                Assert.That(project.Description, Is.Null);
            }
        }

        [Test]
        public void Verify_that_particular_project_from_restapi_json_can_be_deserialized()
        {
            var fileName = Path.Combine(TestContext.CurrentContext.WorkDirectory, "Data", "projects.000e9890-6935-43e6-a5d7-5d7cac601f4c.json");
            using (var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                var data = this.deSerializer.DeSerialize(stream, SerializationModeKind.JSON, SerializationTargetKind.PSM);

                Assert.That(data.Count(), Is.EqualTo(1));

                var project = data.OfType<Project>().Single(x => x.Id == Guid.Parse("000e9890-6935-43e6-a5d7-5d7cac601f4c"));

                Assert.That(project.DefaultBranch, Is.EqualTo(Guid.Parse("c294a463-6c9c-47a8-b592-01252c5ab2a7")));
                Assert.That(project.Name, Is.EqualTo("7b-Variant Configurations Mon Mar 13 17:54:29 EDT 2023"));
                Assert.That(project.Description, Is.Null);
            }
        }

        [Test]
        public void Verify_that_particular_project_and_commits_from_restapi_json_can_be_deserialized()
        {
            var fileName = Path.Combine(TestContext.CurrentContext.WorkDirectory, "Data", "projects.000e9890-6935-43e6-a5d7-5d7cac601f4c.commits.json");
            using (var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                var data = this.deSerializer.DeSerialize(stream, SerializationModeKind.JSON, SerializationTargetKind.PSM);

                Assert.That(data.Count(), Is.EqualTo(1));

                var firstCommit = data.OfType<Commit>().Single(x => x.Id == Guid.Parse("6d7ad9fd-6520-4ff2-885b-8c5c129e6c27"));
                Assert.That(firstCommit.OwningProject, Is.EqualTo(Guid.Parse("000e9890-6935-43e6-a5d7-5d7cac601f4c")));
                Assert.That(firstCommit.PreviousCommits, Is.Null);
                Assert.That(firstCommit.Description, Is.Null);
                Assert.That(firstCommit.Created, Is.EqualTo(DateTime.Parse("2023-03-13T17:53:59.111354-04:00")));
            }
        }

        [Test]
        public void Verify_that_particular_project_and_particular_commit_from_restapi_json_can_be_deserialized()
        {
            var fileName = Path.Combine(TestContext.CurrentContext.WorkDirectory, "Data", "projects.000e9890-6935-43e6-a5d7-5d7cac601f4c.commits.6d7ad9fd-6520-4ff2-885b-8c5c129e6c27.json");
            using (var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                var data = this.deSerializer.DeSerialize(stream, SerializationModeKind.JSON, SerializationTargetKind.PSM);

                Assert.That(data.Count(), Is.EqualTo(1));

                var firstCommit = data.OfType<Commit>().Single(x => x.Id == Guid.Parse("6d7ad9fd-6520-4ff2-885b-8c5c129e6c27"));
                Assert.That(firstCommit.OwningProject, Is.EqualTo(Guid.Parse("000e9890-6935-43e6-a5d7-5d7cac601f4c")));
                Assert.That(firstCommit.PreviousCommits, Is.Null);
                Assert.That(firstCommit.Description, Is.Null);
                Assert.That(firstCommit.Created, Is.EqualTo(DateTime.Parse("2023-03-13T17:53:59.111354-04:00")));
            }
        }

        [Test]
        public void Verify_that_particular_project_and_branches_from_restapi_json_can_be_deserialized()
        {
            var fileName = Path.Combine(TestContext.CurrentContext.WorkDirectory, "Data", "projects.000e9890-6935-43e6-a5d7-5d7cac601f4c.branches.json");
            using (var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                var data = this.deSerializer.DeSerialize(stream, SerializationModeKind.JSON, SerializationTargetKind.PSM);

                Assert.That(data.Count(), Is.EqualTo(1));

                var branch = data.OfType<Branch>().Single(x => x.Id == Guid.Parse("c294a463-6c9c-47a8-b592-01252c5ab2a7"));
                Assert.That(branch.OwningProject, Is.EqualTo(Guid.Parse("000e9890-6935-43e6-a5d7-5d7cac601f4c")));
                Assert.That(branch.Name, Is.EqualTo("main"));
                Assert.That(branch.Description, Is.Null);
                Assert.That(branch.Head, Is.EqualTo(Guid.Parse("6d7ad9fd-6520-4ff2-885b-8c5c129e6c27")));
                Assert.That(branch.Created, Is.EqualTo(DateTime.Parse("2023-03-13T17:53:50.188295-04:00")));
            }
        }
    }
}
