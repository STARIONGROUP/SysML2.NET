// -------------------------------------------------------------------------------------------------
// <copyright file="ActionUsageExtensions.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Core.POCO.Systems.Actions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Kernel.Behaviors;
    using SysML2.NET.Core.POCO.Kernel.FeatureValues;
    using SysML2.NET.Core.POCO.Kernel.Functions;
    using SysML2.NET.Core.POCO.Systems.States;
    using SysML2.NET.Core.Systems.States;

    /// <summary>
    /// The <see cref="ActionUsageExtensions" /> class provides extensions methods for
    /// the <see cref="IActionUsage" /> interface
    /// </summary>
    internal static class ActionUsageExtensions
    {
        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="actionUsageSubject">
        /// The subject <see cref="IActionUsage" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static List<IBehavior> ComputeActionDefinition(this IActionUsage actionUsageSubject)
        {
            return actionUsageSubject == null
                ? throw new ArgumentNullException(nameof(actionUsageSubject))
                : [.. actionUsageSubject.OwnedRelationship.OfType<IFeatureTyping>().Select(featureTyping => featureTyping.Type).OfType<IBehavior>()];
        }

        /// <summary>
        /// Return the owned input parameters of this ActionUsage.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// input-&gt;select(f | f.owner = self)
        /// </code>
        /// </remarks>
        /// <param name="actionUsageSubject">
        /// The subject <see cref="IActionUsage" />
        /// </param>
        /// <returns>
        /// The expected collection of <see cref="IFeature" />
        /// </returns>
        internal static List<IFeature> ComputeInputParametersOperation(this IActionUsage actionUsageSubject)
        {
            return actionUsageSubject == null
                ? throw new ArgumentNullException(nameof(actionUsageSubject))
                : [.. actionUsageSubject.input.Where(inputFeature => inputFeature.owner == actionUsageSubject)];
        }

        /// <summary>
        /// Return the i-th owned input parameter of the ActionUsage. Return null if the ActionUsage has less
        /// than i owned input parameters.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// if inputParameters()-&gt;size() &lt; i then null
        ///                                 else inputParameters()-&gt;at(i)
        ///                                 endif
        /// </code>
        /// </remarks>
        /// <param name="actionUsageSubject">
        /// The subject <see cref="IActionUsage" />
        /// </param>
        /// <param name="i">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected <see cref="IFeature" />
        /// </returns>
        internal static IFeature ComputeInputParameterOperation(this IActionUsage actionUsageSubject, int i)
        {
            if (actionUsageSubject == null)
            {
                throw new ArgumentNullException(nameof(actionUsageSubject));
            }

            var inputParameters = actionUsageSubject.InputParameters();

            return i <= 0 || inputParameters.Count < i
                ? null
                : inputParameters[i - 1];
        }

        /// <summary>
        /// Return the i-th argument Expression of an ActionUsage, defined as the value Expression of the
        /// FeatureValue of the i-th owned input parameter of the ActionUsage. Return null if the ActionUsage
        /// has less than i owned input parameters or the i-th owned input parameter has no FeatureValue.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// if inputParameter(i) = null then null
        ///                                 else
        ///                                 let featureValue : Sequence(FeatureValue) = inputParameter(i).
        ///                                 ownedMembership-&gt;select(oclIsKindOf(FeatureValue)) in
        ///                                 if featureValue-&gt;isEmpty() then null
        ///                                 else featureValue-&gt;at(1).value
        ///                                 endif
        ///                                 endif
        /// </code>
        /// </remarks>
        /// <param name="actionUsageSubject">
        /// The subject <see cref="IActionUsage" />
        /// </param>
        /// <param name="i">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected <see cref="IExpression" />
        /// </returns>
        internal static IExpression ComputeArgumentOperation(this IActionUsage actionUsageSubject, int i)
        {
            return actionUsageSubject == null
                ? throw new ArgumentNullException(nameof(actionUsageSubject))
                : actionUsageSubject.InputParameter(i)?.ownedMembership.OfType<IFeatureValue>().FirstOrDefault()?.value;
        }

        /// <summary>
        /// Check if this ActionUsage is composite and has an owningType that is an ActionDefinition or
        /// ActionUsage but is not the entryAction or exitAction of a StateDefinition or StateUsage. If so, then
        /// it represents an Action that is a subaction of another Action.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// isComposite and owningType &lt;&gt; null and
        ///                                 (owningType.oclIsKindOf(ActionDefinition) or
        ///                                 owningType.oclIsKindOf(ActionUsage)) and
        ///                                 (owningFeatureMembership.oclIsKindOf(StateSubactionMembership) implies
        ///                                 owningFeatureMembership.oclAsType(StateSubactionMembership).kind =
        ///                                 StateSubactionKind::do)
        /// </code>
        /// </remarks>
        /// <param name="actionUsageSubject">
        /// The subject <see cref="IActionUsage" />
        /// </param>
        /// <returns>
        /// The expected <see cref="bool" />
        /// </returns>
        internal static bool ComputeIsSubactionUsageOperation(this IActionUsage actionUsageSubject)
        {
            if (actionUsageSubject == null)
            {
                throw new ArgumentNullException(nameof(actionUsageSubject));
            }

            return actionUsageSubject.IsComposite
                   && actionUsageSubject.owningType is IActionDefinition or IActionUsage
                   && (actionUsageSubject.owningFeatureMembership is not IStateSubactionMembership ssm
                       || ssm.Kind == StateSubactionKind.Do);
        }
    }
}
