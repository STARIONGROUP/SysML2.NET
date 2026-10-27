// ------------------------------------------------------------------------------------------------
// <copyright file="ReturnParameterMembershipExtensions.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Core.POCO.Kernel.Functions
{
    using System;
    using System.Collections.Generic;

    using SysML2.NET.Decorators;

    using SysML2.NET.Core.Core.Types;
    using SysML2.NET.Core.Root.Namespaces;
    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Kernel.Behaviors;
    using SysML2.NET.Core.POCO.Root.Annotations;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;

    /// <summary>
    /// The <see cref="ReturnParameterMembershipExtensions"/> class provides extensions methods for
    /// the <see cref="IReturnParameterMembership"/> interface
    /// </summary>
    internal static class ReturnParameterMembershipExtensions
    {
        /// <summary>
        /// The ownedMemberParameter of a ReturnParameterMembership must have direction out. (This is a leaf
        /// operation that cannot be further redefined.)
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// FeatureDirectionKind::out
        /// </code>
        /// </remarks>
        /// <param name="returnParameterMembershipSubject">
        /// The subject <see cref="IReturnParameterMembership"/>
        /// </param>
        /// <returns>
        /// The expected <see cref="FeatureDirectionKind" />
        /// </returns>
        [Operation(name: nameof(IReturnParameterMembership.ParameterDirection))]
        internal static FeatureDirectionKind ComputeRedefinedParameterDirectionOperation(this IReturnParameterMembership returnParameterMembershipSubject)
        {
            return returnParameterMembershipSubject == null
                ? throw new ArgumentNullException(nameof(returnParameterMembershipSubject))
                : FeatureDirectionKind.Out;
        }
    }
}
