// -------------------------------------------------------------------------------------------------
// <copyright file="ReturnParameterMembershipExtensions.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Core.POCO.Kernel.Functions
{
    using System;
    using System.Collections.Generic;

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
        /// <param name="returnParameterMembershipSubject">
        /// The subject <see cref="IReturnParameterMembership"/>
        /// </param>
        /// <returns>
        /// The expected FeatureDirectionKind
        /// </returns>
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        internal static FeatureDirectionKind ComputeRedefinedParameterDirectionOperation(this IReturnParameterMembership returnParameterMembershipSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }
    }
}
