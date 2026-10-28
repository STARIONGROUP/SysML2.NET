// ------------------------------------------------------------------------------------------------
// <copyright file="SubjectMembershipExtensions.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Core.POCO.Systems.Requirements
{
    using System;
    using System.Collections.Generic;

    using SysML2.NET.Decorators;

    using SysML2.NET.Core.POCO.Systems.DefinitionAndUsage;
    using SysML2.NET.Extensions;

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
        [DerivedProperty(name: nameof(ISubjectMembership.ownedSubjectParameter))]
        internal static IUsage ComputeOwnedSubjectParameter(this ISubjectMembership subjectMembershipSubject)
        {
            if (subjectMembershipSubject == null)
            {
                throw new ArgumentNullException(nameof(subjectMembershipSubject));
            }

            return subjectMembershipSubject.OwnedRelatedElement.SingleStrict<IUsage>(nameof(subjectMembershipSubject));
        }

    }
}
