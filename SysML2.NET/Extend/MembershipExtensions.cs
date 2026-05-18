// -------------------------------------------------------------------------------------------------
// <copyright file="MembershipExtensions.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Core.POCO.Root.Namespaces
{
    using System;
    using System.Collections.Generic;

    using SysML2.NET.Core.Root.Namespaces;
    using SysML2.NET.Core.POCO.Root.Annotations;
    using SysML2.NET.Core.POCO.Root.Elements;

    /// <summary>
    /// The <see cref="MembershipExtensions"/> class provides extensions methods for
    /// the <see cref="IMembership"/> interface
    /// </summary>
    internal static class MembershipExtensions
    {
        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="membershipSubject">
        /// The subject <see cref="IMembership"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static string ComputeMemberElementId(this IMembership membershipSubject)
        {
            return membershipSubject == null
                ? throw new ArgumentNullException(nameof(membershipSubject))
                : membershipSubject.MemberElement.ElementId;
        }

        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="membershipSubject">
        /// The subject <see cref="IMembership"/>
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        internal static INamespace ComputeMembershipOwningNamespace(this IMembership membershipSubject)
        {
            return membershipSubject == null ? throw new ArgumentNullException(nameof(membershipSubject)) : membershipSubject.OwningRelatedElement as INamespace;
        }

        /// <summary>
        /// Whether this Membership is distinguishable from a given other Membership. By default, this is true
        /// if this Membership has no memberShortName or memberName; or each of the memberShortName and
        /// memberName are different than both of those of the other Membership; or neither of the metaclasses
        /// of the memberElement of this Membership and the memberElement of the other Membership conform to the
        /// other. But this may be overridden in specializations of Membership.
        /// </summary>
        /// <param name="membershipSubject">
        /// The subject <see cref="IMembership"/>
        /// </param>
        /// <param name="other">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected <see cref="bool" />
        /// </returns>
        internal static bool ComputeIsDistinguishableFromOperation(this IMembership membershipSubject, IMembership other)
        {
            if (membershipSubject == null)
            {
                throw new ArgumentNullException(nameof(membershipSubject));
            }

            if (other == null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            // Clause C: metaclass incompatibility.
            // OCL: not (memberElement.oclKindOf(other.memberElement.oclType())
            //           or other.memberElement.oclKindOf(memberElement.oclType()))
            // De Morgan: !A && !B. A null memberElement on either side trips this
            // (no conformance is possible).
            var thisType = membershipSubject.MemberElement?.GetType();
            var otherType = other.MemberElement?.GetType();

            if (thisType == null || otherType == null
                || (!otherType.IsAssignableFrom(thisType)
                    && !thisType.IsAssignableFrom(otherType)))
            {
                return true;
            }

            // NamePart1 (OCL spells it shortMemberName — known XMI typo, real
            // attribute is MemberShortName):
            //   memberShortName = null
            //   OR (memberShortName != other.memberShortName
            //       AND memberShortName != other.memberName)
            var shortNamePart = string.IsNullOrWhiteSpace(membershipSubject.MemberShortName)
                || (membershipSubject.MemberShortName != other.MemberShortName
                    && membershipSubject.MemberShortName != other.MemberName);

            // NamePart2: same shape, MemberName variant.
            var namePart = string.IsNullOrWhiteSpace(membershipSubject.MemberName)
                || (membershipSubject.MemberName != other.MemberShortName
                    && membershipSubject.MemberName != other.MemberName);

            return shortNamePart && namePart;
        }
    }
}
