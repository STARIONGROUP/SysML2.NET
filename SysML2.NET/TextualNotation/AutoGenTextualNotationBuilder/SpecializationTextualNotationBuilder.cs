// -------------------------------------------------------------------------------------------------
// <copyright file="SpecializationTextualNotationBuilder.cs" company="Starion Group S.A.">
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
    /// The <see cref="SpecializationTextualNotationBuilder" /> provides Textual Notation Builder for the <see cref="SysML2.NET.Core.POCO.Core.Types.ISpecialization" /> element
    /// </summary>
    public static partial class SpecializationTextualNotationBuilder
    {
        /// <summary>
        /// Builds the Textual Notation string for the rule OwnedSpecialization
        /// <para>OwnedSpecialization:Specialization=GeneralType</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.ISpecialization" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildOwnedSpecialization(SysML2.NET.Core.POCO.Core.Types.ISpecialization poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            BuildGeneralType(poco, cursorCache, stringBuilder);

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule SpecificType
        /// <para>SpecificType:Specialization=specific=[QualifiedName]|specific+=OwnedFeatureChain{ownedRelatedElement+=specific}</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.ISpecialization" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildSpecificType(SysML2.NET.Core.POCO.Core.Types.ISpecialization poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            if (poco.OwnedRelatedElement.Contains(poco.Specific) && poco.Specific is SysML2.NET.Core.POCO.Core.Features.IFeature chainedSpecificAsFeature)
            {
                FeatureTextualNotationBuilder.BuildOwnedFeatureChain(chainedSpecificAsFeature, cursorCache, stringBuilder);
            }
            else if (poco.Specific != null)
            {
                SharedTextualNotationBuilder.AppendQualifiedName(stringBuilder, poco.Specific);
                stringBuilder.Append(' ');
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule GeneralType
        /// <para>GeneralType:Specialization=general=[QualifiedName]|general+=OwnedFeatureChain{ownedRelatedElement+=general}</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.ISpecialization" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildGeneralType(SysML2.NET.Core.POCO.Core.Types.ISpecialization poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            if (poco.OwnedRelatedElement.Contains(poco.General) && poco.General is SysML2.NET.Core.POCO.Core.Features.IFeature chainedGeneralAsFeature)
            {
                FeatureTextualNotationBuilder.BuildOwnedFeatureChain(chainedGeneralAsFeature, cursorCache, stringBuilder);
            }
            else if (poco.General != null)
            {
                SharedTextualNotationBuilder.AppendQualifiedName(stringBuilder, poco.General);
                stringBuilder.Append(' ');
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule Specialization
        /// <para>Specialization=('specialization'Identification)?'subtype'SpecificTypeSPECIALIZESGeneralTypeRelationshipBody</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.ISpecialization" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildSpecialization(SysML2.NET.Core.POCO.Core.Types.ISpecialization poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {

            if (!string.IsNullOrWhiteSpace(poco.DeclaredShortName) || !string.IsNullOrWhiteSpace(poco.DeclaredName))
            {
                stringBuilder.Append("specialization ");
                ElementTextualNotationBuilder.BuildIdentification(poco, cursorCache, stringBuilder);
                stringBuilder.Append(' ');
            }

            stringBuilder.Append("subtype ");
            BuildSpecificType(poco, cursorCache, stringBuilder);
            stringBuilder.Append(" :> ");
            BuildGeneralType(poco, cursorCache, stringBuilder);
            RelationshipTextualNotationBuilder.BuildRelationshipBody(poco, cursorCache, stringBuilder);

        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
