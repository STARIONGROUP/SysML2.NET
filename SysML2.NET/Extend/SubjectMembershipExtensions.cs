// -------------------------------------------------------------------------------------------------
// <copyright file="SubjectMembershipExtensions.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Core.POCO.Systems.Requirements
{
    using System;

    using SysML2.NET.Core.POCO.Systems.DefinitionAndUsage;
    using SysML2.NET.Exceptions;

    /// <summary>
    /// The <see cref="SubjectMembershipExtensions"/> class provides extensions methods for
    /// the <see cref="ISubjectMembership"/> interface
    /// </summary>
    internal static class SubjectMembershipExtensions
    {
        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="subjectMembershipSubject">
        /// The subject <see cref="ISubjectMembership"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static IUsage ComputeOwnedSubjectParameter(this ISubjectMembership subjectMembershipSubject)
        {
            if (subjectMembershipSubject == null)
            {
                throw new ArgumentNullException(nameof(subjectMembershipSubject));
            }

            return subjectMembershipSubject.OwnedRelatedElement.Count != 1
                ? throw new IncompleteModelException($"{nameof(subjectMembershipSubject)} must have exactly one related element")
                : subjectMembershipSubject.OwnedRelatedElement[0] as IUsage;
        }

    }
}
