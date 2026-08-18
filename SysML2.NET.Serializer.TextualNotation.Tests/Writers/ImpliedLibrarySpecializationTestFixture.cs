// -------------------------------------------------------------------------------------------------
// <copyright file="ImpliedLibrarySpecializationTestFixture.cs" company="Starion Group S.A.">
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
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    using NUnit.Framework;

    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Semantics.Extensions;
    using SysML2.NET.Semantics.Implied;
    using SysML2.NET.Serializer.TextualNotation.Tests.Wrapper;
    using SysML2.NET.Serializer.Xmi;

    /// <summary>
    /// Exercises the table-driven library Specializations (KerML §8.4.2 "Set C").
    /// </summary>
    /// <remarks>
    /// The textual-notation corpus is byte-identical with <c>EnableLibrarySpecializations</c> on and off,
    /// because none of these Specializations happens to shorten or lengthen a qualified name in those
    /// models. That makes the corpus blind to this half of the layer: it would stay green if the whole
    /// table silently produced nothing. This fixture is the discriminating check.
    /// </remarks>
    [TestFixture]
    public class ImpliedLibrarySpecializationTestFixture
    {
        private IReadOnlyCollection<INamespace> libraryNamespaces;

        private IReadOnlyList<IType> modelTypes;

        [OneTimeSetUp]
        public async Task OneTimeSetUp()
        {
            var loggerFactory = LoggerFactory.Create(builder => builder.SetMinimumLevel(LogLevel.Error));

            var libraryRoot = Path.Combine(TestContext.CurrentContext.TestDirectory, "Resources");

            var redirectingService = new LibraryRedirectingExternalReferenceService(
                libraryRoot,
                loggerFactory.CreateLogger<LibraryRedirectingExternalReferenceService>());

            this.libraryNamespaces = await new ModelLibraryLoader(loggerFactory, redirectingService).LoadAsync(libraryRoot);

            var filePath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Validation", "01-Parts Tree", "1a-Parts Tree.sysmlx");
            var readResult = await new DeSerializer(loggerFactory, redirectingService).DeSerializeAsync(new Uri(filePath));

            var types = new List<IType>();
            CollectTypes(readResult.RootNamespace, types, new HashSet<IElement>());

            this.modelTypes = types;
        }

        [Test]
        public void VerifyLibrarySpecializationsAreComputed()
        {
            var withoutSetC = this.QueryImpliedGenerals(enableLibrarySpecializations: false);
            var withSetC = this.QueryImpliedGenerals(enableLibrarySpecializations: true);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(this.modelTypes, Is.Not.Empty, "the model must yield Types to evaluate");

                // Off, only the hand-coded rules contribute; on, the table adds the library Specializations.
                Assert.That(withSetC, Has.Count.GreaterThan(withoutSetC.Count),
                    "enabling library Specializations must add implied Specializations");

                // The canonical library Specializations every Parts model carries. If the table, the guards
                // or the library index regress, these disappear while the corpus stays green.
                Assert.That(withSetC, Does.Contain("Part"), "a PartDefinition subclassifies Parts::Part");
                Assert.That(withSetC, Does.Contain("parts"), "a PartUsage subsets Parts::parts");
                Assert.That(withSetC, Does.Contain("things"), "a Feature subsets Base::things");
            }
        }

        /// <summary>
        /// Returns the names of the general Types of every implied Specialization in the model.
        /// </summary>
        /// <param name="enableLibrarySpecializations">Whether the table-driven Specializations are enabled.</param>
        /// <returns>The general names, with duplicates.</returns>
        private List<string> QueryImpliedGenerals(bool enableLibrarySpecializations)
        {
            var services = new ServiceCollection();
            services.AddSysML2Semantics(options => options.EnableLibrarySpecializations = enableLibrarySpecializations);
            services.AddSingleton<ILibraryTypeIndex>(OwnershipTreeLibraryTypeIndex.Build(this.libraryNamespaces));

            using var serviceProvider = services.BuildServiceProvider();
            var provider = serviceProvider.GetRequiredService<IImpliedRelationshipProvider>();

            return [..this.modelTypes
                .SelectMany(provider.GetImpliedSpecializations)
                .Select(specialization => specialization.General?.name ?? specialization.General?.DeclaredName)
                .Where(generalName => generalName != null)];
        }

        /// <summary>
        /// Collects every Type reachable from an Element through owned relationships.
        /// </summary>
        /// <param name="element">The Element to walk from.</param>
        /// <param name="types">The accumulator.</param>
        /// <param name="visited">The Elements already walked, guarding against cycles.</param>
        private static void CollectTypes(IElement element, List<IType> types, HashSet<IElement> visited)
        {
            if (element == null || !visited.Add(element))
            {
                return;
            }

            if (element is IType type)
            {
                types.Add(type);
            }

            foreach (var owned in element.OwnedRelationship.SelectMany(relationship => relationship.OwnedRelatedElement))
            {
                CollectTypes(owned, types, visited);
            }
        }
    }
}
