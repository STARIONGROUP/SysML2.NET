// -------------------------------------------------------------------------------------------------
// <copyright file="OccurrenceUsageTextualNotationBuilder.cs" company="Starion Group S.A.">
//
//   Copyright 2022-2026 Starion Group S.A.
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

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.TextualNotation
{
    using System.Linq;
    using System.Text;

    using SysML2.NET.Core.POCO.Root.Elements;

    /// <summary>
    /// The <see cref="OccurrenceUsageTextualNotationBuilder" /> provides Textual Notation Builder for the <see cref="SysML2.NET.Core.POCO.Systems.Occurrences.IOccurrenceUsage" /> element
    /// </summary>
    public static partial class OccurrenceUsageTextualNotationBuilder
    {
        /// <summary>
        /// Builds the Textual Notation string for the rule OccurrenceUsagePrefix
        /// <para>OccurrenceUsagePrefix:OccurrenceUsage=BasicUsagePrefix(isIndividual?='individual')?(portionKind=PortionKind{isPortion=true})?UsageExtensionKeyword*</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Occurrences.IOccurrenceUsage" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildOccurrenceUsagePrefix(SysML2.NET.Core.POCO.Systems.Occurrences.IOccurrenceUsage poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            UsageTextualNotationBuilder.BuildBasicUsagePrefix(poco, cursorCache, stringBuilder);

            if (poco.IsIndividual)
            {
                stringBuilder.Append(" individual ");
                stringBuilder.Append(' ');
            }


            if (poco.PortionKind.HasValue)
            {
                stringBuilder.Append(poco.PortionKind.ToString().ToLower());
                // NonParsing Assignment Element : isPortion = true => Does not have to be process
                stringBuilder.Append(' ');
            }

            var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);
            while (ownedRelationshipCursor.Current != null)
            {
                UsageTextualNotationBuilder.BuildUsageExtensionKeyword(poco, cursorCache, stringBuilder);
            }


        }

        /// <summary>
        /// Builds the Textual Notation string for the rule IndividualUsage
        /// <para>IndividualUsage:OccurrenceUsage=BasicUsagePrefixisIndividual?='individual'UsageExtensionKeyword*Usage</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Occurrences.IOccurrenceUsage" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildIndividualUsage(SysML2.NET.Core.POCO.Systems.Occurrences.IOccurrenceUsage poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            UsageTextualNotationBuilder.BuildBasicUsagePrefix(poco, cursorCache, stringBuilder);
            if (poco.IsIndividual)
            {
                stringBuilder.Append(" individual ");
            }
            var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);
            while (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Root.Namespaces.IOwningMembership)
            {
                UsageTextualNotationBuilder.BuildUsageExtensionKeyword(poco, cursorCache, stringBuilder);
            }

            UsageTextualNotationBuilder.BuildUsage(poco, cursorCache, stringBuilder);

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule PortionUsage
        /// <para>PortionUsage:OccurrenceUsage=BasicUsagePrefix(isIndividual?='individual')?portionKind=PortionKindUsageExtensionKeyword*Usage{isPortion=true}</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Occurrences.IOccurrenceUsage" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildPortionUsage(SysML2.NET.Core.POCO.Systems.Occurrences.IOccurrenceUsage poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            UsageTextualNotationBuilder.BuildBasicUsagePrefix(poco, cursorCache, stringBuilder);

            if (poco.IsIndividual)
            {
                stringBuilder.Append(" individual ");
                stringBuilder.Append(' ');
            }

            stringBuilder.Append(poco.PortionKind.ToString().ToLower());
            var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);
            while (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Root.Namespaces.IOwningMembership)
            {
                UsageTextualNotationBuilder.BuildUsageExtensionKeyword(poco, cursorCache, stringBuilder);
            }

            UsageTextualNotationBuilder.BuildUsage(poco, cursorCache, stringBuilder);
            // NonParsing Assignment Element : isPortion = true => Does not have to be process

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule ControlNodePrefix
        /// <para>ControlNodePrefix:OccurrenceUsage=RefPrefix(isIndividual?='individual')?(portionKind=PortionKind{isPortion=true})?UsageExtensionKeyword*</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Occurrences.IOccurrenceUsage" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildControlNodePrefix(SysML2.NET.Core.POCO.Systems.Occurrences.IOccurrenceUsage poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            UsageTextualNotationBuilder.BuildRefPrefix(poco, cursorCache, stringBuilder);

            if (poco.IsIndividual)
            {
                stringBuilder.Append(" individual ");
                stringBuilder.Append(' ');
            }


            if (poco.PortionKind.HasValue)
            {
                stringBuilder.Append(poco.PortionKind.ToString().ToLower());
                // NonParsing Assignment Element : isPortion = true => Does not have to be process
                stringBuilder.Append(' ');
            }

            var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);
            while (ownedRelationshipCursor.Current != null)
            {
                UsageTextualNotationBuilder.BuildUsageExtensionKeyword(poco, cursorCache, stringBuilder);
            }


        }

        /// <summary>
        /// Builds the Textual Notation string for the rule OccurrenceUsage
        /// <para>OccurrenceUsage=OccurrenceUsagePrefix'occurrence'Usage</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.Occurrences.IOccurrenceUsage" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildOccurrenceUsage(SysML2.NET.Core.POCO.Systems.Occurrences.IOccurrenceUsage poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            BuildOccurrenceUsagePrefix(poco, cursorCache, stringBuilder);
            stringBuilder.Append("occurrence ");
            UsageTextualNotationBuilder.BuildUsage(poco, cursorCache, stringBuilder);

        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
