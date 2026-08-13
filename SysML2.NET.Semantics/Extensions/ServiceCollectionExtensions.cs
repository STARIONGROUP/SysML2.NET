// -------------------------------------------------------------------------------------------------
// <copyright file="ServiceCollectionExtensions.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Semantics.Extensions
{
    using System;
    using System.Collections.Generic;

    using Microsoft.Extensions.DependencyInjection;

    using SysML2.NET.Semantics.Implied;
    using SysML2.NET.Semantics.Implied.Guards;
    using SysML2.NET.Semantics.Implied.Rules;

    /// <summary>
    /// Registers the semantics layer with a dependency-injection container.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Registers the implied-relationship services with their default configuration.
        /// </summary>
        /// <param name="services">The service collection to register with.</param>
        /// <returns>The same service collection, to allow chaining.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="services" /> is null.</exception>
        /// <remarks>
        /// An <see cref="ILibraryTypeIndex" /> is NOT registered here: only the caller knows where the model
        /// libraries were loaded from, so it must register one built from its own loaded Namespaces.
        /// </remarks>
        public static IServiceCollection AddSysML2Semantics(this IServiceCollection services) => services.AddSysML2Semantics(_ => { });

        /// <summary>
        /// Registers the implied-relationship services with a caller-supplied configuration.
        /// </summary>
        /// <param name="services">The service collection to register with.</param>
        /// <param name="configure">The delegate configuring the options.</param>
        /// <returns>The same service collection, to allow chaining.</returns>
        /// <exception cref="ArgumentNullException">Thrown when either argument is null.</exception>
        public static IServiceCollection AddSysML2Semantics(this IServiceCollection services, Action<ImpliedRelationshipOptions> configure)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (configure == null)
            {
                throw new ArgumentNullException(nameof(configure));
            }

            var options = new ImpliedRelationshipOptions();
            configure(options);

            services.AddSingleton(options);
            services.AddScoped<IImpliedRelationshipFactory, ImpliedRelationshipFactory>();
            services.AddScoped<IImpliedSpecializationReducer, ImpliedSpecializationReducer>();
            services.AddScoped<IImpliedRuleGuardRegistry>(serviceProvider => new ImpliedRuleGuardRegistry(serviceProvider.GetServices<IImpliedRuleGuard>()));
            services.AddScoped<IImpliedRelationshipProvider, ImpliedRelationshipProvider>();

            services.AddImpliedRelationshipRule<VariationUsageSpecializationRule>();
            services.AddImpliedRelationshipRule<VariationDefinitionSpecializationRule>();

            // The mechanically translatable guards come from the generator; only the shapes its parser
            // deliberately declines are hand-written.
            foreach (var generatedGuard in GeneratedImpliedRuleGuards.All)
            {
                services.AddSingleton(generatedGuard);
            }

            services.AddImpliedRuleGuard<AcceptActionUsageSubactionSpecializationGuard>();
            services.AddImpliedRuleGuard<AssociationBinarySpecializationGuard>();
            services.AddImpliedRuleGuard<AssociationStructureBinarySpecializationGuard>();
            services.AddImpliedRuleGuard<ConnectorBinaryObjectSpecializationGuard>();
            services.AddImpliedRuleGuard<ConnectorBinarySpecializationGuard>();
            services.AddImpliedRuleGuard<ConnectorObjectSpecializationGuard>();
            services.AddImpliedRuleGuard<FeatureEndSpecializationGuard>();
            services.AddImpliedRuleGuard<FeaturePortionSpecializationGuard>();
            services.AddImpliedRuleGuard<FeatureSubobjectSpecializationGuard>();
            services.AddImpliedRuleGuard<FeatureSuboccurrenceSpecializationGuard>();
            services.AddImpliedRuleGuard<FlowDefinitionBinarySpecializationGuard>();
            services.AddImpliedRuleGuard<IncludeUseCaseUsageSpecializationGuard>();
            services.AddImpliedRuleGuard<OccurrenceUsageSuboccurrenceSpecializationGuard>();
            services.AddImpliedRuleGuard<StepOwnedPerformanceSpecializationGuard>();
            services.AddImpliedRuleGuard<StepSubperformanceSpecializationGuard>();
            services.AddImpliedRuleGuard<TransitionUsageActionSpecializationGuard>();
            services.AddImpliedRuleGuard<TransitionUsageStateSpecializationGuard>();

            return services;
        }

        /// <summary>
        /// Registers a hand-coded rule for a semantic constraint the generated table cannot express.
        /// </summary>
        /// <typeparam name="TRule">The rule to register.</typeparam>
        /// <param name="services">The service collection to register with.</param>
        /// <returns>The same service collection, to allow chaining.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="services" /> is null.</exception>
        public static IServiceCollection AddImpliedRelationshipRule<TRule>(this IServiceCollection services)
            where TRule : class, IImpliedRelationshipRule
        {
            return services == null
                ? throw new ArgumentNullException(nameof(services))
                : services.AddScoped<IImpliedRelationshipRule, TRule>();
        }

        /// <summary>
        /// Registers a guard for a conditional semantic constraint.
        /// </summary>
        /// <typeparam name="TGuard">The guard to register.</typeparam>
        /// <param name="services">The service collection to register with.</param>
        /// <returns>The same service collection, to allow chaining.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="services" /> is null.</exception>
        /// <remarks>
        /// Guards are registered explicitly rather than discovered by assembly scanning, so the registered
        /// set stays visible in source and the assembly stays trimmable.
        /// </remarks>
        public static IServiceCollection AddImpliedRuleGuard<TGuard>(this IServiceCollection services)
            where TGuard : class, IImpliedRuleGuard
        {
            return services == null
                ? throw new ArgumentNullException(nameof(services))
                : services.AddScoped<IImpliedRuleGuard, TGuard>();
        }

        /// <summary>
        /// Registers an <see cref="ILibraryTypeIndex" /> built from the supplied library root Namespaces.
        /// </summary>
        /// <param name="services">The service collection to register with.</param>
        /// <param name="libraryNamespaces">The library root Namespaces to index.</param>
        /// <returns>The same service collection, to allow chaining.</returns>
        /// <exception cref="ArgumentNullException">Thrown when either argument is null.</exception>
        public static IServiceCollection AddLibraryTypeIndex(this IServiceCollection services, IEnumerable<Core.POCO.Root.Namespaces.INamespace> libraryNamespaces)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (libraryNamespaces == null)
            {
                throw new ArgumentNullException(nameof(libraryNamespaces));
            }

            var index = OwnershipTreeLibraryTypeIndex.Build(libraryNamespaces);

            return services.AddSingleton<ILibraryTypeIndex>(index);
        }
    }
}
