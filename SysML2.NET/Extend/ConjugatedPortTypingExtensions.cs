// -------------------------------------------------------------------------------------------------
// <copyright file="ConjugatedPortTypingExtensions.cs" company="Starion Group S.A.">
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

    using SysML2.NET.Decorators;
    using SysML2.NET.Exceptions;

    /// <summary>
    /// The <see cref="ConjugatedPortTypingExtensions" /> class provides extensions methods for
    /// the <see cref="IConjugatedPortTyping" /> interface
    /// </summary>
    internal static class ConjugatedPortTypingExtensions
    {
        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <remarks>
        /// OCL2.0:
        /// <code>
        /// portDefinition = conjugatedPortDefinition.originalPortDefinition
        /// </code>
        /// </remarks>
        /// <param name="conjugatedPortTypingSubject">
        /// The subject <see cref="IConjugatedPortTyping" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        /// <exception cref="IncompleteModelException">
        /// Thrown when the conjugatedPortDefinition or its originalPortDefinition is null.
        /// </exception>
        [DerivedProperty(nameof(IConjugatedPortTyping.portDefinition))]
        internal static IPortDefinition ComputePortDefinition(this IConjugatedPortTyping conjugatedPortTypingSubject)
        {
            if (conjugatedPortTypingSubject == null)
            {
                throw new ArgumentNullException(nameof(conjugatedPortTypingSubject));
            }

            return conjugatedPortTypingSubject.ConjugatedPortDefinition?.originalPortDefinition
                   ?? throw new IncompleteModelException($"{nameof(conjugatedPortTypingSubject)} must have a conjugatedPortDefinition with an originalPortDefinition");
        }
    }
}
