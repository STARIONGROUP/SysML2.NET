// -------------------------------------------------------------------------------------------------
// <copyright file="TransitionUsageExtensions.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Core.POCO.Systems.States
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using SysML2.NET.Core.Core.Types;
    using SysML2.NET.Core.Root.Namespaces;
    using SysML2.NET.Core.Systems.Occurrences;
    using SysML2.NET.Core.Systems.States;
    using SysML2.NET.Core.POCO.Core.Classifiers;
    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Kernel.Behaviors;
    using SysML2.NET.Core.POCO.Kernel.Classes;
    using SysML2.NET.Core.POCO.Kernel.Connectors;
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
    using SysML2.NET.Core.POCO.Systems.Constraints;
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
    using SysML2.NET.Core.POCO.Systems.UseCases;
    using SysML2.NET.Core.POCO.Systems.VerificationCases;
    using SysML2.NET.Core.POCO.Systems.Views;

    /// <summary>
    /// The <see cref="TransitionUsageExtensions"/> class provides extensions methods for
    /// the <see cref="ITransitionUsage"/> interface
    /// </summary>
    internal static class TransitionUsageExtensions
    {
        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// triggerAction = ownedFeatureMembership-&gt;
        ///                             selectByKind(TransitionFeatureMembership)-&gt;
        ///                             select(kind = TransitionFeatureKind::trigger).transitionFeatures-&gt;
        ///                             selectByKind(AcceptActionUsage)
        /// </code>
        /// </remarks>
        /// <param name="transitionUsageSubject">
        /// The subject <see cref="ITransitionUsage"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static List<IActionUsage> ComputeEffectAction(this ITransitionUsage transitionUsageSubject)
        {
            if (transitionUsageSubject == null)
            {
                throw new ArgumentNullException(nameof(transitionUsageSubject));
            }

            return [..transitionUsageSubject.ownedFeatureMembership
                .OfType<ITransitionFeatureMembership>()
                .Where(tfm => tfm.Kind == TransitionFeatureKind.Effect)
                .Select(tfm => tfm.transitionFeature)
                .OfType<IActionUsage>()];
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// guardExpression = ownedFeatureMembership-&gt;
        ///                             selectByKind(TransitionFeatureMembership)-&gt;
        ///                             select(kind = TransitionFeatureKind::trigger).transitionFeature-&gt;
        ///                             selectByKind(Expression)
        /// </code>
        /// </remarks>
        /// <param name="transitionUsageSubject">
        /// The subject <see cref="ITransitionUsage"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static List<IExpression> ComputeGuardExpression(this ITransitionUsage transitionUsageSubject)
        {
            if (transitionUsageSubject == null)
            {
                throw new ArgumentNullException(nameof(transitionUsageSubject));
            }

            return [..transitionUsageSubject.ownedFeatureMembership
                .OfType<ITransitionFeatureMembership>()
                .Where(tfm => tfm.Kind == TransitionFeatureKind.Guard)
                .Select(tfm => tfm.transitionFeature)
                .OfType<IExpression>()];
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// source =
        ///                             let sourceFeature : Feature = sourceFeature() in
        ///                             if sourceFeature = null then null
        ///                             else sourceFeature.featureTarget.oclAsType(ActionUsage)
        /// </code>
        /// </remarks>
        /// <param name="transitionUsageSubject">
        /// The subject <see cref="ITransitionUsage"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static IActionUsage ComputeSource(this ITransitionUsage transitionUsageSubject)
        {
            if (transitionUsageSubject == null)
            {
                throw new ArgumentNullException(nameof(transitionUsageSubject));
            }

            var sourceFeature = transitionUsageSubject.SourceFeature();

            return sourceFeature?.featureTarget as IActionUsage;
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// succession = ownedMember-&gt;selectByKind(Succession)-&gt;at(1)
        /// </code>
        /// </remarks>
        /// <param name="transitionUsageSubject">
        /// The subject <see cref="ITransitionUsage"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static ISuccession ComputeSuccession(this ITransitionUsage transitionUsageSubject)
        {
            if (transitionUsageSubject == null)
            {
                throw new ArgumentNullException(nameof(transitionUsageSubject));
            }

            return transitionUsageSubject.ownedMember.OfType<ISuccession>().FirstOrDefault();
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// target =
        ///                             if succession.targetFeature-&gt;isEmpty() then null
        ///                             else
        ///                             let targetFeature : Feature =
        ///                             succession.targetFeature-&gt;first().featureTarget in
        ///                             if not targetFeature.oclIsKindOf(ActionUsage) then null
        ///                             else targetFeature.oclAsType(ActionUsage)
        ///                             endif
        ///                             endif
        /// </code>
        /// </remarks>
        /// <param name="transitionUsageSubject">
        /// The subject <see cref="ITransitionUsage"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static IActionUsage ComputeTarget(this ITransitionUsage transitionUsageSubject)
        {
            if (transitionUsageSubject == null)
            {
                throw new ArgumentNullException(nameof(transitionUsageSubject));
            }

            var succession = transitionUsageSubject.succession;

            if (succession == null)
            {
                return null;
            }

            var firstTargetFeature = succession.targetFeature.Count == 0
                ? null
                : succession.targetFeature[0];

            return firstTargetFeature?.featureTarget as IActionUsage;
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// triggerAction = ownedFeatureMembership-&gt;
        ///                             selectByKind(TransitionFeatureMembership)-&gt;
        ///                             select(kind = TransitionFeatureKind::trigger).transitionFeature-&gt;
        ///                             selectByKind(AcceptActionUsage)
        /// </code>
        /// </remarks>
        /// <param name="transitionUsageSubject">
        /// The subject <see cref="ITransitionUsage"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static List<IAcceptActionUsage> ComputeTriggerAction(this ITransitionUsage transitionUsageSubject)
        {
            if (transitionUsageSubject == null)
            {
                throw new ArgumentNullException(nameof(transitionUsageSubject));
            }

            return [..transitionUsageSubject.ownedFeatureMembership
                .OfType<ITransitionFeatureMembership>()
                .Where(tfm => tfm.Kind == TransitionFeatureKind.Trigger)
                .Select(tfm => tfm.transitionFeature)
                .OfType<IAcceptActionUsage>()];
        }

        /// <summary>
        /// Return the payloadParameter of the triggerAction of this TransitionUsage, if it has one.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// if triggerAction-&gt;isEmpty() then null
        ///                                 else triggerAction-&gt;first().payloadParameter
        ///                                 endif
        /// </code>
        /// </remarks>
        /// <param name="transitionUsageSubject">
        /// The subject <see cref="ITransitionUsage"/>
        /// </param>
        /// <returns>
        /// The expected <see cref="IReferenceUsage" />
        /// </returns>
        internal static IReferenceUsage ComputeTriggerPayloadParameterOperation(this ITransitionUsage transitionUsageSubject)
        {
            if (transitionUsageSubject == null)
            {
                throw new ArgumentNullException(nameof(transitionUsageSubject));
            }

            var triggerActions = transitionUsageSubject.triggerAction;

            return triggerActions.Count == 0
                ? null
                : triggerActions[0].payloadParameter;
        }

        /// <summary>
        /// Return the Feature to be used as the source of the succession of this TransitionUsage, which is the
        /// first member of the TransitionUsage that is a Feature, that is owned by the TransitionUsage via a
        /// Membership that is not a FeatureMembership, and whose featureTarget is an ActionUsage.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// let features : Sequence(Feature) = ownedMembership-&gt;
        ///                                 reject(oclIsKindOf(FeatureMembership)).memberElement-&gt;
        ///                                 selectByKind(Feature)-&gt;
        ///                                 select(featureTarget.oclIsKindOf(ActionUsage)) in
        ///                                 if features-&gt;isEmpty() then null
        ///                                 else features-&gt;first()
        ///                                 endif
        /// </code>
        /// </remarks>
        /// <param name="transitionUsageSubject">
        /// The subject <see cref="ITransitionUsage"/>
        /// </param>
        /// <returns>
        /// The expected <see cref="IFeature" />
        /// </returns>
        internal static IFeature ComputeSourceFeatureOperation(this ITransitionUsage transitionUsageSubject)
        {
            if (transitionUsageSubject == null)
            {
                throw new ArgumentNullException(nameof(transitionUsageSubject));
            }

            return transitionUsageSubject.ownedMembership
                .Where(m => m is not IFeatureMembership)
                .Select(m => m.MemberElement)
                .OfType<IFeature>()
                .FirstOrDefault(f => f.featureTarget is IActionUsage);
        }
    }
}
