// -------------------------------------------------------------------------------------------------
// <copyright file="RestClientTestFixture.cs" company="RHEA System S.A.">
// 
//   Copyright 2022 RHEA System S.A.
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

namespace SySML2.NET.REST.Tests
{
    using System;
    using System.Linq;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    using SysML2.NET.Serializer.Json;

    using NUnit.Framework;

    /// <summary>
    /// Suite of tests for the <see cref="RestClient"/> class
    /// </summary>
    public class RestClientTestFixture
    {
        private string baseUri;
        
        private CancellationTokenSource cancellationTokenSource;

        private IRestClient restClient;

        private Guid projectId;

        private Guid branchId;

        private Guid commitId;

        private Guid tagId;

        private Guid elementId;

        [SetUp]
		public void Setup()
        {
            this.baseUri = "http://sysml2-sst.intercax.com:9000";

            this.cancellationTokenSource = new CancellationTokenSource();

            var httpClient = new HttpClient();
            var deserializer = new DeSerializer();
            var serializer = new Serializer();

            this.projectId = Guid.Parse("2e912d1b-a31c-4f93-b082-2c0aff296ea0");
            this.branchId = Guid.Parse("047c94c7-e8dc-420a-a0fb-0b19fc57c998");
            this.commitId = Guid.Parse("c5c5f0a2-5cce-4a2c-a083-97f39a576b53");
            this.tagId = Guid.Parse("79722f63-0592-40e8-941f-cbf60c14243e");
            this.elementId = Guid.Parse("00730052-0ac7-4344-8a90-a3d70da21ccb");

            this.restClient = new RestClient(httpClient, deserializer, serializer);
        }

		[Test]
		public async Task Verify_that_RestClient_can_be_opened_and_that_projects_are_returned()
        {
            var projects = await this.restClient.Open("john", "doe", this.baseUri, this.cancellationTokenSource.Token);

            Assert.That(projects, Is.Not.Empty);
        }

        [Test]
        public async Task Verify_that_projects_can_be_requested()
        {
            Assert.DoesNotThrowAsync(async () => await this.restClient.Open("john", "doe", this.baseUri, this.cancellationTokenSource.Token)); 

            var projects = await this.restClient.RequestProjects(null, null, this.cancellationTokenSource.Token);

            Assert.That(projects, Is.Not.Empty);

            projects = await this.restClient.RequestProjects(this.projectId, null, this.cancellationTokenSource.Token);

            var project = projects.Single();

            Assert.That(project.Name, Is.EqualTo("Spacecraft project with branches and tags - 2022-08-12 16:03:49.344073"));
        }

        [Test]
        public async Task Verify_that_branches_in_a_project_can_be_requested()
        {
            Assert.DoesNotThrowAsync(async () => await this.restClient.Open("john", "doe", this.baseUri, this.cancellationTokenSource.Token));

            var branches = await this.restClient.RequestBranches(this.projectId, null, null, this.cancellationTokenSource.Token);

            Assert.That(branches, Is.Not.Empty);

            branches = await this.restClient.RequestBranches(this.projectId, this.branchId, null, this.cancellationTokenSource.Token);

            var branch = branches.Single();

            Assert.That(branch.Name, Is.EqualTo("main"));
            Assert.That(branch.Head, Is.EqualTo(Guid.Parse("7aaae0dd-63f2-445f-b494-43e3305d3394")));
            Assert.That(branch.OwningProject, Is.EqualTo(this.projectId));
        }

        [Test]
        public async Task Verify_that_commits_in_a_project_can_be_requested()
        {
            Assert.DoesNotThrowAsync(async () => await this.restClient.Open("john", "doe", this.baseUri, this.cancellationTokenSource.Token));

            var commits = await this.restClient.RequestCommits(this.projectId, null, null, this.cancellationTokenSource.Token);

            Assert.That(commits, Is.Not.Empty);
            
            commits = await this.restClient.RequestCommits(this.projectId, this.commitId, null, this.cancellationTokenSource.Token);

            var commit = commits.Single();

            Assert.That(commit.PreviousCommit, Is.EqualTo(Guid.Parse("7aaae0dd-63f2-445f-b494-43e3305d3394")));
            Assert.That(commit.OwningProject, Is.EqualTo(this.projectId));
        }

        [Test]
        public async Task Verify_that_tags_in_a_project_can_be_requested()
        {
            Assert.DoesNotThrowAsync(async () => await this.restClient.Open("john", "doe", this.baseUri, this.cancellationTokenSource.Token));

            var tags = await this.restClient.RequestTags(this.projectId, null, null, this.cancellationTokenSource.Token);

            Assert.That(tags, Is.Not.Empty);
            
            tags = await this.restClient.RequestTags(this.projectId, this.tagId, null, this.cancellationTokenSource.Token);

            var tag = tags.Single();

            Assert.That(tag.Name, Is.EqualTo("Spacecraft Internal Release 0.2 build 1"));
            Assert.That(tag.OwningProject, Is.EqualTo(this.projectId));
        }

        [Test]
        public async Task Verify_that_elements_in_a_project_commit_can_be_requested()
        {
            Assert.DoesNotThrowAsync(async () => await this.restClient.Open("john", "doe", this.baseUri, this.cancellationTokenSource.Token));
            
            var elements = await this.restClient.RequestElements(this.projectId, this.commitId, null,null, this.cancellationTokenSource.Token);

            Assert.That(elements.ToList(), Is.Not.Empty);

            elements = await this.restClient.RequestElements(this.projectId, this.commitId, this.elementId, null, this.cancellationTokenSource.Token);

            var element = elements.Single();

            Assert.That(element.Name, Is.EqualTo("Propulsion System"));
        }
    }
}