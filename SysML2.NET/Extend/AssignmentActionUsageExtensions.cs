// -------------------------------------------------------------------------------------------------
// <copyright file="AssignmentActionUsageExtensions.cs" company="Starion Group S.A.">
// 
//   Copyright (C) 2022-2026 Starion Group S.A.
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
    using System.Linq;

    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Kernel.Functions;
    using SysML2.NET.Core.POCO.Kernel.Metadata;

    /// <summary>
    /// The <see cref="AssignmentActionUsageExtensions" /> class provides extensions methods for
    /// the <see cref="IAssignmentActionUsage" /> interface
    /// </summary>
    internal static class AssignmentActionUsageExtensions
    {
        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// referent =
        ///     let unownedFeatures : Sequence(Feature) = ownedMembership-&gt;
        ///         reject(oclIsKindOf(FeatureMembership)).memberElement-&gt;
        ///         select(oclIsKindOf(Feature) and
        ///                not oclIsKindOf(MetadataFeature)) in
        ///     if unownedFeatures-&gt;isEmpty() then null
        ///     else unownedFeatures-&gt;first().oclAsType(Feature)
        ///     endif
        /// </code>
        /// </remarks>
        /// <param name="assignmentActionUsageSubject">
        /// The subject <see cref="IAssignmentActionUsage" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static IFeature ComputeReferent(this IAssignmentActionUsage assignmentActionUsageSubject)
        {
            if (assignmentActionUsageSubject == null)
            {
                throw new ArgumentNullException(nameof(assignmentActionUsageSubject));
            }

            return assignmentActionUsageSubject.ownedMembership
                .Where(membership => membership is not IFeatureMembership)
                .Select(membership => membership.MemberElement)
                .FirstOrDefault(memberElement => memberElement is IFeature and not IMetadataFeature) as IFeature;
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// targetArgument = argument(1)
        /// </code>
        /// </remarks>
        /// <param name="assignmentActionUsageSubject">
        /// The subject <see cref="IAssignmentActionUsage" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static IExpression ComputeTargetArgument(this IAssignmentActionUsage assignmentActionUsageSubject)
        {
            return assignmentActionUsageSubject == null
                ? throw new ArgumentNullException(nameof(assignmentActionUsageSubject))
                : assignmentActionUsageSubject.Argument(1);
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// valueExpression = argument(2)
        /// </code>
        /// </remarks>
        /// <param name="assignmentActionUsageSubject">
        /// The subject <see cref="IAssignmentActionUsage" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static IExpression ComputeValueExpression(this IAssignmentActionUsage assignmentActionUsageSubject)
        {
            return assignmentActionUsageSubject == null
                ? throw new ArgumentNullException(nameof(assignmentActionUsageSubject))
                : assignmentActionUsageSubject.Argument(2);
        }
    }
}
