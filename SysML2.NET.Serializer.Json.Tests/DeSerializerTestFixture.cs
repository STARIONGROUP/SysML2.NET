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
            var fileName = Path.Combine(TestContext.CurrentContext.WorkDirectory, "Data", "projects.29845a29-b25b-4bab-b8cc-f46a021b7f5a.commits.ec39c63a-fdaa-4a47-98a5-8e8f56b3a986.elements.json");
            using (var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                var data = this.deSerializer.DeSerialize(stream, SerializationModeKind.JSON, SerializationTargetKind.CORE);

                Assert.That(data.Count(), Is.EqualTo(100));

                Assert.That(data.OfType<IFeature>().Count(), Is.EqualTo(40));

                var feature = data.OfType<IFeature>().Single(x => x.Id == Guid.Parse("0089a4a6-1abf-49c3-9806-1f0114662c13"));

                Assert.That(feature.AliasIds, Is.Empty);
                Assert.That(feature.ElementId, Is.EqualTo("0089a4a6-1abf-49c3-9806-1f0114662c13"));
                Assert.That(feature.IsAbstract, Is.False);
                Assert.That(feature.IsComposite, Is.False);
                Assert.That(feature.IsSufficient, Is.False);
                Assert.That(feature.IsEnd, Is.True);
                Assert.That(feature.DeclaredName, Is.Null);
                Assert.That(feature.OwnedRelationship, Is.Empty);
                Assert.That(feature.OwningRelationship, Is.EqualTo(Guid.Parse("4344ee51-05be-49a6-be59-4f6938614cd8")));
                Assert.That(feature.DeclaredShortName, Is.Null);
            }
        }

        [Test]
        public async Task Verify_that_idada_from_sysmlcore_json_can_be_deserialized_async()
        {
            var fileName = Path.Combine(TestContext.CurrentContext.WorkDirectory, "Data", "projects.29845a29-b25b-4bab-b8cc-f46a021b7f5a.commits.ec39c63a-fdaa-4a47-98a5-8e8f56b3a986.elements.json");
            using (var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                var cts = new CancellationTokenSource();

                var data = await this.deSerializer.DeSerializeAsync(stream, SerializationModeKind.JSON, SerializationTargetKind.CORE, cts.Token);

                Assert.That(data.Count(), Is.EqualTo(100));
                
                Assert.That(data.OfType<IFeature>().Count(), Is.EqualTo(40));

                var feature = data.OfType<IFeature>().Single(x => x.Id == Guid.Parse("0089a4a6-1abf-49c3-9806-1f0114662c13"));

                Assert.That(feature.AliasIds, Is.Empty);
                Assert.That(feature.ElementId, Is.EqualTo("0089a4a6-1abf-49c3-9806-1f0114662c13"));
                Assert.That(feature.IsAbstract, Is.False);
                Assert.That(feature.IsComposite, Is.False);
                Assert.That(feature.IsSufficient, Is.False);
                Assert.That(feature.IsEnd, Is.True);
                Assert.That(feature.DeclaredName, Is.Null);
                Assert.That(feature.OwnedRelationship, Is.Empty);
                Assert.That(feature.OwningRelationship, Is.EqualTo(Guid.Parse("4344ee51-05be-49a6-be59-4f6938614cd8")));
                Assert.That(feature.DeclaredShortName, Is.Null);
            }
        }

        [Test]
        public void Verify_that_projects_from_restapi_json_can_be_deserialized()
        {
            var fileName = Path.Combine(TestContext.CurrentContext.WorkDirectory, "Data", "projects.json");
            using (var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                var data = this.deSerializer.DeSerialize(stream, SerializationModeKind.JSON, SerializationTargetKind.PSM);

                Assert.That(data.Count(), Is.EqualTo(35));

                var project = data.OfType<Project>().Single(x => x.Id == Guid.Parse("110da323-e552-4a46-ad02-c89d37d5a5ce"));
                
                Assert.That(project.DefaultBranch, Is.EqualTo(Guid.Parse("cca1c0d6-0342-4fe7-98c1-a375d58af3a4")));
                Assert.That(project.Name, Is.EqualTo("12b-Allocation Thu Jul 21 12:56:03 EDT 2022"));
                Assert.That(project.Description, Is.Null);
            }
        }

        [Test]
        public void Verify_that_particular_project_from_restapi_json_can_be_deserialized()
        {
            var fileName = Path.Combine(TestContext.CurrentContext.WorkDirectory, "Data", "projects.82961e71-c75a-4d9d-adff-ef491fa8f5f3.json");
            using (var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                var data = this.deSerializer.DeSerialize(stream, SerializationModeKind.JSON, SerializationTargetKind.PSM);

                Assert.That(data.Count(), Is.EqualTo(1));

                var project = data.OfType<Project>().Single(x => x.Id == Guid.Parse("82961e71-c75a-4d9d-adff-ef491fa8f5f3"));

                Assert.That(project.DefaultBranch, Is.EqualTo(Guid.Parse("b541c0f3-0b12-4073-a769-f58ec94ea3c3")));
                Assert.That(project.Name, Is.EqualTo("Spacecraft project with branches and tags - 2022-07-21 13:51:18.826380"));
                Assert.That(project.Description, Is.EqualTo("Spacecraft project with multiple commits, branches, and tags"));
            }
        }

        [Test]
        public void Verify_that_particular_project_and_commits_from_restapi_json_can_be_deserialized()
        {
            var fileName = Path.Combine(TestContext.CurrentContext.WorkDirectory, "Data", "projects.82961e71-c75a-4d9d-adff-ef491fa8f5f3.commits.json");
            using (var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                var data = this.deSerializer.DeSerialize(stream, SerializationModeKind.JSON, SerializationTargetKind.PSM);

                Assert.That(data.Count(), Is.EqualTo(3));

                var firstCommit = data.OfType<Commit>().Single(x => x.Id == Guid.Parse("153b20ac-35a9-4e57-8415-117a7b489409"));
                Assert.That(firstCommit.OwningProject, Is.EqualTo(Guid.Parse("82961e71-c75a-4d9d-adff-ef491fa8f5f3")));
                Assert.That(firstCommit.PreviousCommit, Is.Null);
                Assert.That(firstCommit.Description, Is.Null);
                Assert.That(firstCommit.TimeStamp, Is.EqualTo(DateTime.Parse("2022-07-21T13:51:19.341045-04:00")));

                var lastCommit = data.OfType<Commit>().Single(x => x.Id == Guid.Parse("931978a2-e117-4408-96f4-02b861ddbedd"));
                Assert.That(lastCommit.OwningProject, Is.EqualTo(Guid.Parse("82961e71-c75a-4d9d-adff-ef491fa8f5f3")));
                Assert.That(lastCommit.PreviousCommit, Is.EqualTo(Guid.Parse("6bad3e3d-854d-4ed9-92ed-5ba7ec7f5493")));
                Assert.That(lastCommit.Description, Is.Null);
                Assert.That(lastCommit.TimeStamp, Is.EqualTo(DateTime.Parse("2022-07-21T13:51:21.167494-04:00")));
            }
        }

        [Test]
        public void Verify_that_particular_project_and_particular_commit_from_restapi_json_can_be_deserialized()
        {
            var fileName = Path.Combine(TestContext.CurrentContext.WorkDirectory, "Data", "projects.82961e71-c75a-4d9d-adff-ef491fa8f5f3.commits.931978a2-e117-4408-96f4-02b861ddbedd.json");
            using (var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                var data = this.deSerializer.DeSerialize(stream, SerializationModeKind.JSON, SerializationTargetKind.PSM);

                Assert.That(data.Count(), Is.EqualTo(1));

                var commit = data.OfType<Commit>().Single(x => x.Id == Guid.Parse("931978a2-e117-4408-96f4-02b861ddbedd"));
                Assert.That(commit.OwningProject, Is.EqualTo(Guid.Parse("82961e71-c75a-4d9d-adff-ef491fa8f5f3")));
                Assert.That(commit.PreviousCommit, Is.EqualTo(Guid.Parse("6bad3e3d-854d-4ed9-92ed-5ba7ec7f5493")));
                Assert.That(commit.Description, Is.Null);
                Assert.That(commit.TimeStamp, Is.EqualTo(DateTime.Parse("2022-07-21T13:51:21.167494-04:00")));
            }
        }

        [Test]
        public void Verify_that_particular_project_and_tags_from_restapi_json_can_be_deserialized()
        {
            var fileName = Path.Combine(TestContext.CurrentContext.WorkDirectory, "Data", "projects.82961e71-c75a-4d9d-adff-ef491fa8f5f3.tags.json");
            using (var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                var data = this.deSerializer.DeSerialize(stream, SerializationModeKind.JSON, SerializationTargetKind.PSM);

                Assert.That(data.Count(), Is.EqualTo(2));

                var tag = data.OfType<Tag>().Single(x => x.Id == Guid.Parse("4f502263-e15d-4ee9-a8c0-64fbb7f53070"));
                Assert.That(tag.OwningProject, Is.EqualTo(Guid.Parse("82961e71-c75a-4d9d-adff-ef491fa8f5f3")));
                Assert.That(tag.Name, Is.EqualTo("Spacecraft Internal Release 0.1"));
                Assert.That(tag.Description, Is.Null);
                Assert.That(tag.ReferencedCommit, Is.EqualTo(Guid.Parse("6bad3e3d-854d-4ed9-92ed-5ba7ec7f5493")));
                Assert.That(tag.TaggedCommit, Is.EqualTo(Guid.Parse("6bad3e3d-854d-4ed9-92ed-5ba7ec7f5493")));
                Assert.That(tag.TimeStamp, Is.EqualTo(DateTime.Parse("2022-07-21T13:51:20.942973-04:00")));
            }
        }

        [Test]
        public void Verify_that_particular_project_and_branches_from_restapi_json_can_be_deserialized()
        {
            var fileName = Path.Combine(TestContext.CurrentContext.WorkDirectory, "Data", "projects.82961e71-c75a-4d9d-adff-ef491fa8f5f3.branches.json");
            using (var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                var data = this.deSerializer.DeSerialize(stream, SerializationModeKind.JSON, SerializationTargetKind.PSM);

                Assert.That(data.Count(), Is.EqualTo(2));

                var branch = data.OfType<Branch>().Single(x => x.Id == Guid.Parse("b541c0f3-0b12-4073-a769-f58ec94ea3c3"));
                Assert.That(branch.OwningProject, Is.EqualTo(Guid.Parse("82961e71-c75a-4d9d-adff-ef491fa8f5f3")));
                Assert.That(branch.Name, Is.EqualTo("main"));
                Assert.That(branch.Description, Is.Null);
                Assert.That(branch.ReferencedCommit, Is.EqualTo(Guid.Parse("6bad3e3d-854d-4ed9-92ed-5ba7ec7f5493")));
                Assert.That(branch.Head, Is.EqualTo(Guid.Parse("6bad3e3d-854d-4ed9-92ed-5ba7ec7f5493")));
                Assert.That(branch.TimeStamp, Is.EqualTo(DateTime.Parse("2022-07-21T13:51:18.886889-04:00")));
            }
        }
    }
}
