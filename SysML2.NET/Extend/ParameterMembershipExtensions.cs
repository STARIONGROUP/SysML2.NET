// ------------------------------------------------------------------------------------------------
// <copyright file="ParameterMembershipExtensions.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Core.POCO.Kernel.Behaviors
{
    using System;
    using System.Collections.Generic;

    using SysML2.NET.Decorators;

    using SysML2.NET.Core.Core.Types;
    using SysML2.NET.Core.Root.Namespaces;
    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Root.Annotations;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Extensions;

    /// <summary>
    /// The <see cref="ParameterMembershipExtensions"/> class provides extensions methods for
    /// the <see cref="IParameterMembership"/> interface
    /// </summary>
    internal static class ParameterMembershipExtensions
    {
        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="parameterMembershipSubject">
        /// The subject <see cref="IParameterMembership"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [DerivedProperty(name: nameof(IParameterMembership.ownedMemberParameter))]
        internal static IFeature ComputeOwnedMemberParameter(this IParameterMembership parameterMembershipSubject)
        {
            if (parameterMembershipSubject == null)
            {
                throw new ArgumentNullException(nameof(parameterMembershipSubject));
            }

            return parameterMembershipSubject.OwnedRelatedElement.SingleStrict<IFeature>(nameof(parameterMembershipSubject));
        }

        /// <summary>
        /// Return the required value of the direction of the ownedMemberParameter. By default, this is in.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// FeatureDirectionKind::_'in'
        /// </code>
        /// </remarks>
        /// <param name="parameterMembershipSubject">
        /// The subject <see cref="IParameterMembership"/>
        /// </param>
        /// <returns>
        /// The expected <see cref="FeatureDirectionKind" />
        /// </returns>
        [Operation(name: nameof(IParameterMembership.ParameterDirection))]
        internal static FeatureDirectionKind ComputeParameterDirectionOperation(this IParameterMembership parameterMembershipSubject)
        {
            if (parameterMembershipSubject == null)
            {
                throw new ArgumentNullException(nameof(parameterMembershipSubject));
            }

            return FeatureDirectionKind.In;
        }
    }
}
