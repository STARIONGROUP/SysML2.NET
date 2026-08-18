// -------------------------------------------------------------------------------------------------
// <copyright file="ModelLibraryTypeIndexTestFixture.cs" company="Starion Group S.A.">
//
//    Copyright (C) 2022-2026 Starion Group S.A.
//
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
//
//         http://www.apache.org/licenses/LICENSE-2.0
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Serializer.TextualNotation.Tests.Writers
{
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Logging;

    using NUnit.Framework;

    using SysML2.NET.Semantics.Implied;
    using SysML2.NET.Serializer.TextualNotation.Tests.Wrapper;
    using SysML2.NET.Serializer.Xmi;

    [TestFixture]
    public class ModelLibraryTypeIndexTestFixture
    {
        private static readonly string[] ConstraintTargets =
        [
            "Base::Anything",
            "Base::things",
            "Base::dataValues",
            "Occurrences::Occurrence",
            "Occurrences::occurrences",
            "Objects::objects",
            "Links::Link::participant",
            "Performances::performances"
        ];

        private OwnershipTreeLibraryTypeIndex index;

        [OneTimeSetUp]
        public async Task OneTimeSetUp()
        {
            var loggerFactory = LoggerFactory.Create(builder => builder.SetMinimumLevel(LogLevel.Error));

            var libraryRoot = Path.Combine(TestContext.CurrentContext.TestDirectory, "Resources");

            var redirectingService = new LibraryRedirectingExternalReferenceService(
                libraryRoot,
                loggerFactory.CreateLogger<LibraryRedirectingExternalReferenceService>());

            var loader = new ModelLibraryLoader(loggerFactory, redirectingService);

            this.index = OwnershipTreeLibraryTypeIndex.Build(await loader.LoadAsync(libraryRoot));
        }

        [Test]
        public void VerifyEveryTableTargetResolves()
        {
            // The whole table, not just the rows a corpus happens to exercise: a row whose library Type
            // does not resolve can never be satisfied, and surfaces only when a model reaches that row.
            // Eight such rows were found this way, all traced to typos in the XMI OCL and corrected by the
            // generator's errata map (SysML2.NET.CodeGenerator/Extensions/OclErrata.cs).
            var unresolved = ImpliedRelationshipTable.AllLibraryTargets
                .Where(libraryTarget => !this.index.TryGetType(libraryTarget, out _))
                .ToList();

            Assert.That(unresolved, Is.Empty, $"unresolved library targets: {string.Join(", ", unresolved)}");
        }

        [Test]
        public void VerifyConstraintTargetsResolve()
        {
            // A model-independent library load must resolve the qualified names the KerML 8.4.2 constraints
            // target. Deserializing a user model resolves only what that model imports, which is why the
            // index cannot be built from XmiReadResult.ReferencedNamespaces.
            using (Assert.EnterMultipleScope())
            {
                foreach (var qualifiedName in ConstraintTargets)
                {
                    Assert.That(this.index.TryGetType(qualifiedName, out var resolved), Is.True, $"'{qualifiedName}' must resolve from a full library load.");
                    Assert.That(resolved, Is.Not.Null);
                }

                Assert.That(this.index.TryGetType("Base::NoSuchTypeExists", out _), Is.False);
            }
        }
    }
}
