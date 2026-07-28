// -------------------------------------------------------------------------------------------------
// <copyright file="ConstraintUsageExtensions.cs" company="Starion Group S.A.">
//
//    Copyright (C) 2022-2026 Starion Group S.A.
//
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
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

namespace SysML2.NET.Core.POCO.Systems.Constraints
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using SysML2.NET.Core.Core.Types;
    using SysML2.NET.Core.Root.Namespaces;
    using SysML2.NET.Core.Systems.Occurrences;
    using SysML2.NET.Core.POCO.Core.Classifiers;
    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Kernel.Behaviors;
    using SysML2.NET.Core.POCO.Kernel.Classes;
    using SysML2.NET.Core.POCO.Kernel.Functions;
    using SysML2.NET.Core.POCO.Root.Annotations;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Core.POCO.Systems.Actions;
    using SysML2.NET.Core.POCO.Systems.Allocations;
    using SysML2.NET.Core.POCO.Systems.AnalysisCases;
    using SysML2.NET.Core.POCO.Systems.Attributes;
    using SysML2.NET.Core.POCO.Systems.Calculations;
    using SysML2.NET.Core.POCO.Systems.Cases;
    using SysML2.NET.Core.POCO.Systems.Connections;
    using SysML2.NET.Core.POCO.Systems.DefinitionAndUsage;
    using SysML2.NET.Core.POCO.Systems.Enumerations;
    using SysML2.NET.Core.POCO.Systems.Flows;
    using SysML2.NET.Core.POCO.Systems.Interfaces;
    using SysML2.NET.Core.POCO.Systems.Items;
    using SysML2.NET.Core.POCO.Systems.Metadata;
    using SysML2.NET.Core.POCO.Systems.Occurrences;
    using SysML2.NET.Core.POCO.Systems.Parts;
    using SysML2.NET.Core.POCO.Systems.Ports;
    using SysML2.NET.Core.POCO.Systems.Requirements;
    using SysML2.NET.Core.POCO.Systems.States;
    using SysML2.NET.Core.POCO.Systems.UseCases;
    using SysML2.NET.Core.POCO.Systems.VerificationCases;
    using SysML2.NET.Core.POCO.Systems.Views;
    using SysML2.NET.Exceptions;
    using SysML2.NET.Extensions;

    /// <summary>
    /// The <see cref="ConstraintUsageExtensions"/> class provides extensions methods for
    /// the <see cref="IConstraintUsage"/> interface
    /// </summary>
    internal static class ConstraintUsageExtensions
    {
        /// <summary>
        /// Computes the derived <c>constraintDefinition</c> property: the <see cref="IPredicate"/>
        /// targeted by the single <see cref="IFeatureTyping"/> owned by
        /// <paramref name="constraintUsageSubject"/>.
        /// </summary>
        /// <param name="constraintUsageSubject">
        /// The subject <see cref="IConstraintUsage"/>
        /// </param>
        /// <returns>
        /// The matching <see cref="IPredicate"/>, or <c>null</c> when no such typing exists.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="constraintUsageSubject"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="MultiplicityViolationException">
        /// Thrown when more than one <see cref="IFeatureTyping"/> targets an <see cref="IPredicate"/>
        /// (upper-bound violation against the derived <c>[0..1]</c> property).
        /// </exception>
        internal static IPredicate ComputeConstraintDefinition(this IConstraintUsage constraintUsageSubject)
        {
            return constraintUsageSubject == null
                ? throw new ArgumentNullException(nameof(constraintUsageSubject))
                : FeatureExtensions.ComputeType(constraintUsageSubject).SingleOrDefaultStrict<IPredicate>(nameof(constraintUsageSubject));
        }

        /// <summary>
        /// The naming Feature of a ConstraintUsage that is owned by a RequirementConstraintMembership and has
        /// an ownedReferenceSubsetting is the featureTarget of the referencedFeature of that
        /// ownedReferenceSubsetting.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// if owningFeatureMembership &lt;&gt; null and
        ///                                 owningFeatureMembership.oclIsKindOf(RequirementConstraintMembership) and
        ///                                 ownedReferenceSubsetting &lt;&gt; null then
        ///                                 ownedReferenceSubsetting.referencedFeature.featureTarget
        ///                                 else
        ///                                 self.oclAsType(OccurrenceUsage).namingFeature()
        ///                                 endif
        /// </code>
        /// </remarks>
        /// <param name="constraintUsageSubject">
        /// The subject <see cref="IConstraintUsage"/>
        /// </param>
        /// <returns>
        /// The expected <see cref="IFeature" />
        /// </returns>
        internal static IFeature ComputeRedefinedNamingFeatureOperation(this IConstraintUsage constraintUsageSubject)
        {
            if (constraintUsageSubject == null)
            {
                throw new ArgumentNullException(nameof(constraintUsageSubject));
            }

            if (constraintUsageSubject.owningFeatureMembership is IRequirementConstraintMembership
                && constraintUsageSubject.ownedReferenceSubsetting != null)
            {
                return constraintUsageSubject.ownedReferenceSubsetting.ReferencedFeature?.featureTarget;
            }

            // OCL: self.oclAsType(OccurrenceUsage).namingFeature() — explicit upcast bypass.
            // OccurrenceUsage does not override Usage::namingFeature, so dispatch resolves to Usage's body.
            return UsageExtensions.ComputeRedefinedNamingFeatureOperation(constraintUsageSubject);
        }

        /// <summary>
        /// A ConstraintUsage is not model-level evaluable.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// false
        /// </code>
        /// </remarks>
        /// <param name="constraintUsageSubject">
        /// The subject <see cref="IConstraintUsage"/>
        /// </param>
        /// <param name="visited">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected <see cref="bool" />
        /// </returns>
        internal static bool ComputeRedefinedModelLevelEvaluableOperation(this IConstraintUsage constraintUsageSubject, List<IFeature> visited)
        {
            if (constraintUsageSubject == null)
            {
                throw new ArgumentNullException(nameof(constraintUsageSubject));
            }

            return false;
        }
    }
}
