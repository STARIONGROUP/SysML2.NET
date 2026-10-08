// -------------------------------------------------------------------------------------------------
// <copyright file="MembershipExtensions.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Core.POCO.Root.Namespaces
{
    using System;
    using System.Linq;

    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Decorators;
    using SysML2.NET.Exceptions;

    /// <summary>
    /// The <see cref="MembershipExtensions" /> class provides extensions methods for
    /// the <see cref="IMembership" /> interface
    /// </summary>
    internal static class MembershipExtensions
    {
        /// <summary>
        /// Computes the derived property.
        /// </summary>
        /// <param name="membershipSubject">
        /// The subject <see cref="IMembership" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        [DerivedProperty(nameof(IMembership.memberElementId))]
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
        /// The subject <see cref="IMembership" />
        /// </param>
        /// <returns>
        /// the computed result
        /// </returns>
        /// <exception cref="IncompleteModelException">
        /// Thrown when the owning related element is null or is not an <see cref="INamespace" />.
        /// </exception>
        [DerivedProperty(nameof(IMembership.membershipOwningNamespace))]
        internal static INamespace ComputeMembershipOwningNamespace(this IMembership membershipSubject)
        {
            if (membershipSubject == null)
            {
                throw new ArgumentNullException(nameof(membershipSubject));
            }

            return membershipSubject.OwningRelatedElement as INamespace
                   ?? throw new IncompleteModelException(
                       $"{nameof(membershipSubject)} must have an owning related element of type {nameof(INamespace)}");
        }

        /// <summary>
        /// Whether this Membership is distinguishable from a given other Membership. By default, this is true
        /// if this Membership has no memberShortName or memberName; or each of the memberShortName and
        /// memberName are different than both of those of the other Membership; or neither of the metaclasses
        /// of the memberElement of this Membership and the memberElement of the other Membership conform to the
        /// other. But this may be overridden in specializations of Membership.
        /// </summary>
        /// <param name="membershipSubject">
        /// The subject <see cref="IMembership" />
        /// </param>
        /// <param name="other">
        /// No documentation provided
        /// </param>
        /// <returns>
        /// The expected <see cref="bool" />
        /// </returns>
        [Operation(nameof(IMembership.IsDistinguishableFrom))]
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
            var thisElement = membershipSubject.MemberElement;
            var otherElement = other.MemberElement;

            if (thisElement == null || otherElement == null
                                    || (!ConformsToMetaclassOf(thisElement, otherElement)
                                        && !ConformsToMetaclassOf(otherElement, thisElement)))
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

        /// <summary>
        /// Asserts whether the metaclass of <paramref name="element" /> conforms to the metaclass of
        /// <paramref name="other" />, the C# equivalent of OCL <c>oclKindOf(other.oclType())</c>.
        /// </summary>
        /// <param name="element">The <see cref="IElement" /> whose metaclass is tested.</param>
        /// <param name="other">The <see cref="IElement" /> supplying the metaclass to conform to.</param>
        /// <returns>True when every metaclass interface of <paramref name="other" /> is implemented by <paramref name="element" />.</returns>
        /// <remarks>
        /// A generated POCO class implements its metaclass interface rather than inheriting from the POCO
        /// class of its supertype, so metaclass conformance is carried by the interface set and not by
        /// <see cref="Type.IsAssignableFrom" /> over the concrete classes.
        /// </remarks>
        private static bool ConformsToMetaclassOf(IElement element, IElement other)
        {
            var elementType = element.GetType();

            return other.GetType().GetInterfaces().All(otherInterface => otherInterface.IsAssignableFrom(elementType));
        }
    }
}
