// -------------------------------------------------------------------------------------------------
// <copyright file="SegmentNaming.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Serializer.TextualNotation.NameResolution
{
    using System.Linq;

    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Extensions;

    /// <summary>
    /// The lexical form of one qualified-name segment: which of an Element's names a segment prefers, and
    /// how that name is escaped when it is not a basic NAME (KerML §8.2.2.3).
    /// </summary>
    internal static class SegmentNaming
    {
        /// <summary>
        /// Escapes <paramref name="rawName" /> per KEBNF: unchanged when it is a basic name, otherwise
        /// quoted as an unrestricted name.
        /// </summary>
        /// <param name="rawName">The raw name; must be non-blank.</param>
        /// <returns>The escaped form.</returns>
        internal static string Escape(string rawName)
        {
            return rawName.QueryIsValidBasicName() ? rawName : rawName.ToUnrestrictedName();
        }

        /// <summary>
        /// Returns the first non-blank candidate, or <see langword="null" /> when every one is blank.
        /// </summary>
        /// <param name="candidates">The candidate forms, most preferred first.</param>
        /// <returns>The first non-blank candidate, or <see langword="null" />.</returns>
        internal static string FirstNonBlank(params string[] candidates)
        {
            return candidates.FirstOrDefault(candidate => !string.IsNullOrWhiteSpace(candidate));
        }

        /// <summary>
        /// Returns the two lexical forms a membership binds: the membership's explicit name overrides when
        /// present, else the member element's own names.
        /// </summary>
        /// <param name="membership">The membership doing the binding.</param>
        /// <param name="target">The membership's member element.</param>
        /// <returns>The short and long lexical forms; either may be <see langword="null" />.</returns>
        internal static (string ShortName, string LongName) QueryMembershipNames(IMembership membership, IElement target)
        {
            return (!string.IsNullOrWhiteSpace(membership.MemberShortName) ? membership.MemberShortName : target.shortName,
                !string.IsNullOrWhiteSpace(membership.MemberName) ? membership.MemberName : target.name);
        }

        /// <summary>
        /// Returns the element's shortest escaped name segment (shortName preferred), or
        /// <see langword="null" /> when neither lexical form is available.
        /// </summary>
        /// <param name="element">The element to name; must be non-null.</param>
        /// <returns>The escaped segment, or <see langword="null" />.</returns>
        internal static string QueryPreferredEscapedSegment(IElement element)
        {
            var preferred = QueryPreferredRawName(element);

            return string.IsNullOrWhiteSpace(preferred) ? null : Escape(preferred);
        }

        /// <summary>
        /// Returns the element's preferred raw simple name: <c>shortName</c> when non-blank, otherwise
        /// <c>name</c>. May be <see langword="null" /> or blank.
        /// </summary>
        /// <param name="element">The element to name; must be non-null.</param>
        /// <returns>The preferred raw name.</returns>
        internal static string QueryPreferredRawName(IElement element)
        {
            return !string.IsNullOrWhiteSpace(element.shortName) ? element.shortName : element.name;
        }
    }
}
