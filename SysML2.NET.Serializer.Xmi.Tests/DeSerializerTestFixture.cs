// ------------------------------------------------------------------------------------------------
// <copyright file="DeSerializerTestFixture.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Serializer.Xmi.Tests
{
    using System;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    using SysML2.NET.Serializer.Xmi.Extensions;

    [TestFixture]
    public class DeSerializerTestFixture
    {
        private DeSerializer deSerializer;

        [SetUp]
        public void Setup()
        {
            var serviceProvider = new ServiceCollection()
                .AddLogging(x => x.AddConsole())
                .BuildServiceProvider();

            this.deSerializer = new DeSerializer(serviceProvider.GetRequiredService<ILoggerFactory>());
        }

        [Test]
        public void VerifyCanReadXmlLibrary()
        {
            var filePath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Resources", "Domain Libraries", "Quantities and Units", "Quantities.sysmlx");
            Assert.That(() => this.deSerializer.DeSerialize(new Uri(filePath)), Throws.Nothing);
        }
        
        [Test]
        public async Task VerifyCanReadXmlLibraryAsync()
        {
            var filePath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Resources", "Domain Libraries", "Quantities and Units", "Quantities.sysmlx");
            await Assert.ThatAsync(() => this.deSerializer.DeSerializeAsync(new Uri(filePath)), Throws.Nothing);
        }
        
        [Test]
        public async Task VerifyCanCancelReadXmlLibraryAsync()
        {
            using var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromMilliseconds(500));
            var filePath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Resources", "Domain Libraries", "Quantities and Units", "Quantities.sysmlx");
            await Assert.ThatAsync(() => this.deSerializer.DeSerializeAsync(new Uri(filePath), cancellationTokenSource.Token), Throws.InstanceOf<OperationCanceledException>());
        }
    }
}
