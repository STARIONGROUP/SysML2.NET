// ------------------------------------------------------------------------------------------------
// <copyright file="FlexoTestHost.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SySML2.NET.REST.Tests.Helpers
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    using DotNet.Testcontainers.Builders;
    using DotNet.Testcontainers.Containers;
    using DotNet.Testcontainers.Networks;

    using NUnit.Framework;

    /// <summary>
    /// Runs a Flexo MMS SysML v2 server in Docker: a Fuseki quad store, the Flexo layer1 service and the
    /// Flexo SysML v2 service on one network, with the <c>sysmlv2</c> organization created on layer1
    /// </summary>
    /// <remarks>
    /// Start it from <c>[OneTimeSetUp]</c>: an unreachable Docker ignores the fixture, any later failure fails it.
    /// </remarks>
    public sealed class FlexoTestHost : IAsyncDisposable
    {
        /// <summary>
        /// The quad store image: <c>atomgraph/fuseki:4.6</c>, pinned by digest
        /// </summary>
        private const string QuadStoreImage = "atomgraph/fuseki@sha256:9eb7a16dbddd35d9e293eed32a4e5b27ab04b6bd8ba4ef827ed00119d6e45bea";

        /// <summary>
        /// The layer1 image: <c>openmbee/flexo-mms-layer1-service:v0.3.0</c>, pinned by digest
        /// </summary>
        private const string Layer1Image = "openmbee/flexo-mms-layer1-service@sha256:5e40dbf9318135f9e8097b4268f40f3cbcc1cf0d97c0eea04c4424749467325f";

        /// <summary>
        /// The SysML v2 service image: the <c>develop</c> build of commit 836d904d, pinned by digest
        /// </summary>
        private const string SysmlImage = "openmbee/flexo-sysmlv2@sha256:007bd8000a42cf07b76b0773cc351326f4ef5ee2660df2683d976258dbb34fd3";

        /// <summary>
        /// The network alias of the quad store, which the layer1 configuration refers to
        /// </summary>
        private const string QuadStoreAlias = "quad-server";

        /// <summary>
        /// The network alias of layer1, which <c>cluster.trig</c> and the SysML v2 configuration refer to
        /// </summary>
        private const string Layer1Alias = "layer1-service";

        /// <summary>
        /// The layer1 organization that the SysML v2 service stores its projects in
        /// </summary>
        private const string Organization = "sysmlv2";

        /// <summary>
        /// The port that Fuseki listens on
        /// </summary>
        private const ushort QuadStorePort = 3030;

        /// <summary>
        /// The port that layer1 and the SysML v2 service listen on
        /// </summary>
        private const ushort ServicePort = 8080;

        /// <summary>
        /// The time that each container may take to become ready, including the image pull of a first run
        /// </summary>
        private static readonly TimeSpan ReadinessTimeout = TimeSpan.FromMinutes(5);

        /// <summary>
        /// The Docker endpoint to use, or <c>null</c> to let Testcontainers probe for one
        /// </summary>
        private readonly string dockerEndpoint = ResolveDockerEndpoint();

        /// <summary>
        /// The network that the three containers share
        /// </summary>
        private INetwork network;

        /// <summary>
        /// The Fuseki quad store container
        /// </summary>
        private IContainer quadStore;

        /// <summary>
        /// The layer1 container
        /// </summary>
        private IContainer layer1;

        /// <summary>
        /// The SysML v2 service container
        /// </summary>
        private IContainer sysml;

        /// <summary>
        /// Gets the <see cref="FlexoJwt"/> whose secret layer1 is configured with
        /// </summary>
        public FlexoJwt Jwt { get; } = new();

        /// <summary>
        /// Gets the bearer token accepted by layer1, valid for the duration of a test run
        /// </summary>
        public string Token { get; private set; }

        /// <summary>
        /// Gets the base address of the SysML v2 API, available once <see cref="StartAsync"/> completed
        /// </summary>
        public Uri BaseAddress { get; private set; }

        /// <summary>
        /// Disposes the containers and the network
        /// </summary>
        /// <returns>
        /// An awaitable <see cref="ValueTask"/>
        /// </returns>
        public async ValueTask DisposeAsync()
        {
            IContainer[] containers = [this.sysml, this.layer1, this.quadStore];

            foreach (var container in containers.Where(container => container != null))
            {
                await container.DisposeAsync();
            }

            if (this.network != null)
            {
                await this.network.DisposeAsync();
            }
        }

        /// <summary>
        /// Starts the quad store, then layer1, creates the organization, and then starts the SysML v2 service
        /// </summary>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken"/> that cancels the start
        /// </param>
        /// <returns>
        /// An awaitable <see cref="Task"/>
        /// </returns>
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            try
            {
                var networkBuilder = new NetworkBuilder();

                if (this.dockerEndpoint != null)
                {
                    networkBuilder = networkBuilder.WithDockerEndpoint(this.dockerEndpoint);
                }

                this.network = networkBuilder.Build();
                await this.network.CreateAsync(cancellationToken);
            }
            catch (Exception exception)
            {
                Assert.Ignore($"Docker is not available for the Flexo integration tests: {exception.Message}");
            }

            this.Token = this.Jwt.CreateToken(TimeSpan.FromHours(4));

            Exception startFailure = null;

            try
            {
                await this.StartQuadStoreAsync(cancellationToken);
                await this.StartLayer1Async(cancellationToken);
                await this.CreateOrganizationAsync(cancellationToken);
                await this.StartSysmlAsync(cancellationToken);
            }
            catch (Exception exception)
            {
                startFailure = exception;
            }

            if (startFailure != null)
            {
                var logs = await this.CollectLogsAsync();
                Assert.Fail($"Flexo did not start: {startFailure.Message}{logs}");
            }
        }

        /// <summary>
        /// Resolves the Docker endpoint the way <c>PostgreSqlSchemaTestHost</c> does: <c>DOCKER_HOST</c> when set,
        /// otherwise Docker Desktop's Linux engine pipe on Windows, otherwise <c>null</c>
        /// </summary>
        /// <returns>
        /// The Docker endpoint, or <c>null</c>
        /// </returns>
        private static string ResolveDockerEndpoint()
        {
            var dockerHost = Environment.GetEnvironmentVariable("DOCKER_HOST");

            if (!string.IsNullOrWhiteSpace(dockerHost))
            {
                return dockerHost;
            }

            if (OperatingSystem.IsWindows() && File.Exists(@"\\.\pipe\dockerDesktopLinuxEngine"))
            {
                return "npipe://./pipe/dockerDesktopLinuxEngine";
            }

            return null;
        }

        /// <summary>
        /// Collects the tail of the logs of every started container, to explain a failed start
        /// </summary>
        /// <returns>
        /// The logs, one section per container
        /// </returns>
        private async Task<string> CollectLogsAsync()
        {
            var logs = new StringBuilder();

            (string Name, IContainer Container)[] containers = [("quad store", this.quadStore), ("layer1", this.layer1), ("sysmlv2", this.sysml)];

            foreach (var (name, container) in containers.Where(entry => entry.Container != null))
            {
                try
                {
                    var (stdout, stderr) = await container.GetLogsAsync(default, default, false, CancellationToken.None);
                    var output = $"{stdout}{stderr}";
                    logs.Append($"{Environment.NewLine}--- {name} ---{Environment.NewLine}{output[Math.Max(0, output.Length - 4000)..]}");
                }
                catch (Exception exception)
                {
                    logs.Append($"{Environment.NewLine}--- {name}: logs unavailable ({exception.Message}) ---");
                }
            }

            return logs.ToString();
        }

        /// <summary>
        /// Creates a <see cref="ContainerBuilder"/> on the shared network and Docker endpoint
        /// </summary>
        /// <param name="image">
        /// The image of the container
        /// </param>
        /// <param name="networkAlias">
        /// The alias by which the other containers reach it, or <c>null</c> when none is needed
        /// </param>
        /// <returns>
        /// The <see cref="ContainerBuilder"/>
        /// </returns>
        private ContainerBuilder CreateContainerBuilder(string image, string networkAlias)
        {
            var builder = new ContainerBuilder(image).WithNetwork(this.network);

            if (networkAlias != null)
            {
                builder = builder.WithNetworkAliases(networkAlias);
            }

            if (this.dockerEndpoint != null)
            {
                builder = builder.WithDockerEndpoint(this.dockerEndpoint);
            }

            return builder;
        }

        /// <summary>
        /// Creates the organization that the SysML v2 service stores its projects in
        /// </summary>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken"/> that cancels the request
        /// </param>
        /// <returns>
        /// An awaitable <see cref="Task"/>
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown when layer1 does not accept the organization
        /// </exception>
        private async Task CreateOrganizationAsync(CancellationToken cancellationToken)
        {
            using var httpClient = new HttpClient();
            var organizationUri = new Uri($"http://{this.layer1.Hostname}:{this.layer1.GetMappedPublicPort(ServicePort)}/orgs/{Organization}");

            using var request = new HttpRequestMessage(HttpMethod.Put, organizationUri);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", this.Token);
            request.Content = new StringContent($"<> <http://purl.org/dc/terms/title> \"{Organization}\"@en .", Encoding.UTF8, "text/turtle");

            using var response = await httpClient.SendAsync(request, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync(cancellationToken);
                throw new InvalidOperationException($"layer1 refused the {Organization} organization with {(int)response.StatusCode}: {body}");
            }
        }

        /// <summary>
        /// Starts the layer1 service, configured with the per-run JWT secret
        /// </summary>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken"/> that cancels the start
        /// </param>
        /// <returns>
        /// An awaitable <see cref="Task"/>
        /// </returns>
        private async Task StartLayer1Async(CancellationToken cancellationToken)
        {
            var quadStoreUrl = $"http://{QuadStoreAlias}:{QuadStorePort}/ds";

            this.layer1 = this.CreateContainerBuilder(Layer1Image, Layer1Alias)
                .WithEnvironment("JWT_DOMAIN", FlexoJwt.Issuer)
                .WithEnvironment("JWT_AUDIENCE", FlexoJwt.Audience)
                .WithEnvironment("JWT_REALM", FlexoJwt.Realm)
                .WithEnvironment("JWT_SECRET", this.Jwt.Secret)
                .WithEnvironment("FLEXO_MMS_ROOT_CONTEXT", $"http://{Layer1Alias}")
                .WithEnvironment("FLEXO_MMS_QUERY_URL", $"{quadStoreUrl}/sparql")
                .WithEnvironment("FLEXO_MMS_UPDATE_URL", $"{quadStoreUrl}/update")
                .WithEnvironment("FLEXO_MMS_GRAPH_STORE_PROTOCOL_URL", $"{quadStoreUrl}/data")
                .WithPortBinding(ServicePort, true)
                .WithWaitStrategy(Wait.ForUnixContainer().UntilHttpRequestIsSucceeded(
                    request => request.ForPort(ServicePort).ForPath("/").ForStatusCodeMatching(_ => true),
                    strategy => strategy.WithTimeout(ReadinessTimeout)))
                .Build();

            await this.layer1.StartAsync(cancellationToken);
        }

        /// <summary>
        /// Starts the Fuseki quad store, seeded with <c>cluster.trig</c>
        /// </summary>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken"/> that cancels the start
        /// </param>
        /// <returns>
        /// An awaitable <see cref="Task"/>
        /// </returns>
        private async Task StartQuadStoreAsync(CancellationToken cancellationToken)
        {
            var clusterPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Resources", "Flexo", "cluster.trig");
            var cluster = await File.ReadAllBytesAsync(clusterPath, cancellationToken);

            this.quadStore = this.CreateContainerBuilder(QuadStoreImage, QuadStoreAlias)
                .WithResourceMapping(cluster, "/tmp/mount/cluster.trig")
                .WithEnvironment("JAVA_OPTIONS", "-Xmx1g")
                .WithEnvironment("JAVA_TOOL_OPTIONS", "-XX:-UseContainerSupport")
                .WithCommand("--file=/tmp/mount/cluster.trig", "--update", "/ds")
                .WithPortBinding(QuadStorePort, true)
                .WithWaitStrategy(Wait.ForUnixContainer().UntilMessageIsLogged("Start Fuseki", strategy => strategy.WithTimeout(ReadinessTimeout)))
                .Build();

            await this.quadStore.StartAsync(cancellationToken);
        }

        /// <summary>
        /// Starts the SysML v2 service; it forwards the client's bearer token to layer1 and has no fallback token
        /// </summary>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken"/> that cancels the start
        /// </param>
        /// <returns>
        /// An awaitable <see cref="Task"/>
        /// </returns>
        private async Task StartSysmlAsync(CancellationToken cancellationToken)
        {
            this.sysml = this.CreateContainerBuilder(SysmlImage, null)
                .WithEnvironment("FLEXO_HOST", Layer1Alias)
                .WithEnvironment("FLEXO_PORT", ServicePort.ToString())
                .WithEnvironment("FLEXO_PROTOCOL", "http")
                .WithEnvironment("FLEXO_SYSMLV2_ORG", Organization)
                .WithPortBinding(ServicePort, true)
                .WithWaitStrategy(Wait.ForUnixContainer().UntilHttpRequestIsSucceeded(
                    request => request.ForPort(ServicePort).ForPath("/meta/datatypes").WithHeader("Authorization", $"Bearer {this.Token}"),
                    strategy => strategy.WithTimeout(ReadinessTimeout)))
                .Build();

            await this.sysml.StartAsync(cancellationToken);

            this.BaseAddress = new Uri($"http://{this.sysml.Hostname}:{this.sysml.GetMappedPublicPort(ServicePort)}/");
        }
    }
}
