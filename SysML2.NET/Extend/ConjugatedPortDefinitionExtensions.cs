// -------------------------------------------------------------------------------------------------
// <copyright file="ConjugatedPortDefinitionExtensions.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Core.POCO.Systems.Ports
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    using SysML2.NET.Exceptions;
    using SysML2.NET.Extensions;

    /// <summary>
    /// The <see cref="ConjugatedPortDefinitionExtensions" /> class provides extensions methods for
    /// the <see cref="IConjugatedPortDefinition" /> interface
    /// </summary>
    internal static class ConjugatedPortDefinitionExtensions
    {
        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="conjugatedPortDefinitionSubject">
        /// The subject <see cref="IConjugatedPortDefinition" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static IPortDefinition ComputeOriginalPortDefinition(this IConjugatedPortDefinition conjugatedPortDefinitionSubject)
        {
            if (conjugatedPortDefinitionSubject == null)
            {
                throw new ArgumentNullException(nameof(conjugatedPortDefinitionSubject));
            }

            return conjugatedPortDefinitionSubject.owningMembership?.membershipOwningNamespace as IPortDefinition
                   ?? throw new IncompleteModelException(
                       $"{nameof(conjugatedPortDefinitionSubject)} must have an owning namespace of type {nameof(IPortDefinition)}");
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="conjugatedPortDefinitionSubject">
        /// The subject <see cref="IConjugatedPortDefinition" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static IPortConjugation ComputeOwnedPortConjugator(this IConjugatedPortDefinition conjugatedPortDefinitionSubject)
        {
            if (conjugatedPortDefinitionSubject == null)
            {
                throw new ArgumentNullException(nameof(conjugatedPortDefinitionSubject));
            }

            return conjugatedPortDefinitionSubject.OwnedRelationship
                .SingleStrict<IPortConjugation>(nameof(conjugatedPortDefinitionSubject));
        }

        /// <summary>
        /// If the name of the originalPortDefinition is non-empty, then return that with the character ~
        /// prepended.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// let originalName : String = originalPortDefinition.name in
        ///                                 if originalName = null then null
        ///                                 else '~' + originalName
        ///                                 endif
        /// </code>
        /// </remarks>
        /// <param name="conjugatedPortDefinitionSubject">
        /// The subject <see cref="IConjugatedPortDefinition" />
        /// </param>
        /// <returns>
        /// The expected <see cref="string" />
        /// </returns>
        [ExcludeFromCodeCoverage]
        internal static string ComputeRedefinedEffectiveNameOperation(this IConjugatedPortDefinition conjugatedPortDefinitionSubject)
        {
            throw new NotSupportedException("Create a GitHub issue when this method is required");
        }
    }
}
