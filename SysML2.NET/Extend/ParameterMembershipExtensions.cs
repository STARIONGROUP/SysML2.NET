// -------------------------------------------------------------------------------------------------
// <copyright file="ParameterMembershipExtensions.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Core.POCO.Kernel.Behaviors
{
    using System;
    using System.Collections.Generic;

    using SysML2.NET.Core.Core.Types;
    using SysML2.NET.Core.Root.Namespaces;
    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Root.Annotations;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Exceptions;

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
        internal static IFeature ComputeOwnedMemberParameter(this IParameterMembership parameterMembershipSubject)
        {
            if (parameterMembershipSubject == null)
            {
                throw new ArgumentNullException(nameof(parameterMembershipSubject));
            }

            return parameterMembershipSubject.OwnedRelatedElement.Count != 1
                ? throw new IncompleteModelException($"{nameof(parameterMembershipSubject)} must have exactly one related element")
                : parameterMembershipSubject.OwnedRelatedElement[0] as IFeature;
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
