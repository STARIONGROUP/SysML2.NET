// ------------------------------------------------------------------------------------------------
// <copyright file="ServiceCollectionExtensionsTestFixture.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Semantics.Tests.Extensions
{
    using System;
    using System.Linq;

    using Microsoft.Extensions.DependencyInjection;

    using NUnit.Framework;

    using SysML2.NET.Core.POCO.Kernel.Packages;
    using SysML2.NET.Semantics.Extensions;
    using SysML2.NET.Semantics.Implied;
    using SysML2.NET.Semantics.Implied.Rules;

    [TestFixture]
    public class ServiceCollectionExtensionsTestFixture
    {
        [Test]
        public void VerifyAddSysML2Semantics()
        {
            using (Assert.EnterMultipleScope())
            {
                Assert.That(() => ((IServiceCollection)null).AddSysML2Semantics(), Throws.TypeOf<ArgumentNullException>());
                Assert.That(() => ((IServiceCollection)null).AddSysML2Semantics(_ => { }), Throws.TypeOf<ArgumentNullException>());
                Assert.That(() => new ServiceCollection().AddSysML2Semantics(null), Throws.TypeOf<ArgumentNullException>());
            }

            var services = new ServiceCollection();

            Assert.That(services.AddSysML2Semantics(options => options.EnableLibrarySpecializations = true), Is.SameAs(services));

            using var serviceProvider = services.BuildServiceProvider();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(serviceProvider.GetRequiredService<ImpliedRelationshipOptions>().EnableLibrarySpecializations, Is.True);
                Assert.That(serviceProvider.GetServices<IImpliedRuleGuard>().ToList(), Is.Not.Empty);
                Assert.That(serviceProvider.GetService<ILibraryTypeIndex>(), Is.Null);
            }
        }

        [Test]
        public void VerifyAddImpliedRelationshipRule()
        {
            Assert.That(() => ((IServiceCollection)null).AddImpliedRelationshipRule<VariationUsageSpecializationRule>(), Throws.TypeOf<ArgumentNullException>());

            var services = new ServiceCollection();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(services.Any(descriptor => descriptor.ServiceType == typeof(IImpliedRelationshipRule)), Is.False);
                Assert.That(services.AddImpliedRelationshipRule<VariationUsageSpecializationRule>(), Is.SameAs(services));
            }

            var descriptors = services.Where(descriptor => descriptor.ServiceType == typeof(IImpliedRelationshipRule)).ToList();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(descriptors, Has.Count.EqualTo(1));
                Assert.That(descriptors[0].ImplementationType, Is.EqualTo(typeof(VariationUsageSpecializationRule)));
                Assert.That(descriptors[0].Lifetime, Is.EqualTo(ServiceLifetime.Scoped));
            }
        }

        [Test]
        public void VerifyAddLibraryTypeIndex()
        {
            using (Assert.EnterMultipleScope())
            {
                Assert.That(() => ((IServiceCollection)null).AddLibraryTypeIndex([]), Throws.TypeOf<ArgumentNullException>());
                Assert.That(() => new ServiceCollection().AddLibraryTypeIndex(null), Throws.TypeOf<ArgumentNullException>());
            }

            var services = new ServiceCollection();

            Assert.That(services.AddLibraryTypeIndex([]), Is.SameAs(services));

            using var emptyProvider = services.BuildServiceProvider();

            Assert.That(emptyProvider.GetRequiredService<ILibraryTypeIndex>(), Is.Not.Null);

            var libraryPackage = new Package { DeclaredName = "Base" };

            using var populatedProvider = new ServiceCollection().AddLibraryTypeIndex([libraryPackage]).BuildServiceProvider();

            Assert.That(populatedProvider.GetRequiredService<ILibraryTypeIndex>(), Is.Not.Null);
        }
    }
}
