// -------------------------------------------------------------------------------------------------
// <copyright file="RestClientTestFixture.cs" company="Starion Group S.A.">
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

namespace SySML2.NET.REST.Tests
{
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
        
        [SetUp]
		public void Setup()
        {
            this.baseUri = "http://sysml2-sst.intercax.com:9000";

            this.cancellationTokenSource = new CancellationTokenSource();

            var httpClient = new HttpClient();
            var deserializer = new DeSerializer();
            var serializer = new Serializer();
            
            this.restClient = new RestClient(httpClient, deserializer, serializer);
        }

        [Test]
        public async Task Verify_that_RestClient_can_query_projects_branches_tags_commits_elemets_can_be_requested()
        {
            Assert.DoesNotThrowAsync(async () => await this.restClient.Open("john", "doe", this.baseUri, this.cancellationTokenSource.Token));

            var projects = await this.restClient.RequestProjects(null, null, this.cancellationTokenSource.Token);
            
            var projectId = projects.First().Id;

            var p = (await this.restClient.RequestProjects(projectId, null, this.cancellationTokenSource.Token)).Single();

            Assert.That(p.Id, Is.EqualTo(projectId));
            
            foreach (var project in projects)
            {
                Assert.That(project.Name, Is.Not.Null.Or.Empty);
                Assert.That(project.DefaultBranch, Is.Not.Null.Or.Empty);

                var branches = await this.restClient.RequestBranches(project.Id, null, null, this.cancellationTokenSource.Token);
                
                Assert.That(branches, Is.Not.Empty);

                foreach (var branch in branches)
                {
                    Assert.That(branch.Name, Is.Not.Null.Or.Empty);
                    Assert.That(branch.Head, Is.Not.Null.Or.Empty);
                    Assert.That(branch.OwningProject, Is.EqualTo(project.Id));
                }

                var tags = await this.restClient.RequestTags(project.Id, null, null, this.cancellationTokenSource.Token);
                
                foreach (var tag in tags)
                {
                    Assert.That(tag.Name, Is.Not.Null.Or.Empty);
                    Assert.That(tag.OwningProject, Is.EqualTo(project.Id));
                }

                var commits = await this.restClient.RequestCommits(project.Id, null, null, this.cancellationTokenSource.Token);

                foreach (var commit in commits)
                {
                    Assert.That(commit.OwningProject, Is.EqualTo(project.Id));

                    // var elements = await this.restClient.RequestElements(project.Id, commit.Id, null, null, this.cancellationTokenSource.Token);

                    //foreach (var element in elements)
                    //{
                    //    Assert.That(element.Id, Is.Not.Null.Or.Empty);
                    //}
                }
            }
	    }
    }
}
