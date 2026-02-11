// -------------------------------------------------------------------------------------------------
// <copyright file="DeSerializerTestFixture.cs" company="Starion Group S.A.">
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
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    using SysML2.NET.Serializer.Xmi.Extensions;
    using SysML2.NET.Serializer.Xmi.Readers;

    [TestFixture]
    public class DeSerializerTestFixture
    {
        private DeSerializer deSerializer;
        private XmiDataCache xmiDataCache;

        [SetUp]
        public void Setup()
        {
            var serviceProvider = new ServiceCollection()
                .AddLogging(x => x.AddConsole())
                .BuildServiceProvider();

            this.xmiDataCache = new XmiDataCache(new PocoReferenceResolveExtensionsFacade(),serviceProvider.GetRequiredService<ILogger<XmiDataCache>>());

            this.deSerializer = new DeSerializer(new ExternalReferenceService(serviceProvider.GetRequiredService<ILogger<ExternalReferenceService>>()), new XmiDataReaderFacade(), this.xmiDataCache, serviceProvider.GetRequiredService<ILoggerFactory>());
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
