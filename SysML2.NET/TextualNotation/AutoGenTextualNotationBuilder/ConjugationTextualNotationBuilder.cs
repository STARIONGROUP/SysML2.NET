// -------------------------------------------------------------------------------------------------
// <copyright file="ConjugationTextualNotationBuilder.cs" company="Starion Group S.A.">
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
    /// The <see cref="ConjugationTextualNotationBuilder" /> provides Textual Notation Builder for the <see cref="SysML2.NET.Core.POCO.Core.Types.IConjugation" /> element
    /// </summary>
    public static partial class ConjugationTextualNotationBuilder
    {
        /// <summary>
        /// Builds the Textual Notation string for the rule OwnedConjugation
        /// <para>OwnedConjugation:Conjugation=originalType=[QualifiedName]|originalType=FeatureChain{ownedRelatedElement+=originalType}</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IConjugation" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildOwnedConjugation(SysML2.NET.Core.POCO.Core.Types.IConjugation poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            if (poco.OwnedRelatedElement.Contains(poco.OriginalType) && poco.OriginalType is SysML2.NET.Core.POCO.Core.Features.IFeature chainedOriginalTypeAsFeature)
            {
                FeatureTextualNotationBuilder.BuildFeatureChain(chainedOriginalTypeAsFeature, cursorCache, stringBuilder);
            }
            else if (poco.OriginalType != null)
            {
                SharedTextualNotationBuilder.AppendQualifiedName(stringBuilder, poco.OriginalType);
                stringBuilder.Append(' ');
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule Conjugation
        /// <para>Conjugation=('conjugation'Identification)?'conjugate'(conjugatedType=[QualifiedName]|conjugatedType=FeatureChain{ownedRelatedElement+=conjugatedType})CONJUGATES(originalType=[QualifiedName]|originalType=FeatureChain{ownedRelatedElement+=originalType})RelationshipBody</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Types.IConjugation" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildConjugation(SysML2.NET.Core.POCO.Core.Types.IConjugation poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {

            if (!string.IsNullOrWhiteSpace(poco.DeclaredShortName) || !string.IsNullOrWhiteSpace(poco.DeclaredName))
            {
                stringBuilder.Append("conjugation ");
                ElementTextualNotationBuilder.BuildIdentification(poco, cursorCache, stringBuilder);
                stringBuilder.Append(' ');
            }

            stringBuilder.Append("conjugate ");
            if (poco.OwnedRelatedElement.Contains(poco.ConjugatedType) && poco.ConjugatedType is SysML2.NET.Core.POCO.Core.Features.IFeature chainedConjugatedTypeAsFeature)
            {
                FeatureTextualNotationBuilder.BuildFeatureChain(chainedConjugatedTypeAsFeature, cursorCache, stringBuilder);
            }
            else if (poco.ConjugatedType != null)
            {
                SharedTextualNotationBuilder.AppendQualifiedName(stringBuilder, poco.ConjugatedType);
                stringBuilder.Append(' ');
            }

            stringBuilder.Append(' ');
            stringBuilder.Append(" ~");
            if (poco.OwnedRelatedElement.Contains(poco.OriginalType) && poco.OriginalType is SysML2.NET.Core.POCO.Core.Features.IFeature chainedOriginalTypeAsFeature)
            {
                FeatureTextualNotationBuilder.BuildFeatureChain(chainedOriginalTypeAsFeature, cursorCache, stringBuilder);
            }
            else if (poco.OriginalType != null)
            {
                SharedTextualNotationBuilder.AppendQualifiedName(stringBuilder, poco.OriginalType);
                stringBuilder.Append(' ');
            }

            stringBuilder.Append(' ');
            RelationshipTextualNotationBuilder.BuildRelationshipBody(poco, cursorCache, stringBuilder);

        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
