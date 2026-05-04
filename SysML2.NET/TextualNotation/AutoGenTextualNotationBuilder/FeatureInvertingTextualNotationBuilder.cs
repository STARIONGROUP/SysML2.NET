// -------------------------------------------------------------------------------------------------
// <copyright file="FeatureInvertingTextualNotationBuilder.cs" company="Starion Group S.A.">
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
    /// The <see cref="FeatureInvertingTextualNotationBuilder" /> provides Textual Notation Builder for the <see cref="SysML2.NET.Core.POCO.Core.Features.IFeatureInverting" /> element
    /// </summary>
    public static partial class FeatureInvertingTextualNotationBuilder
    {
        /// <summary>
        /// Builds the Textual Notation string for the rule OwnedFeatureInverting
        /// <para>OwnedFeatureInverting:FeatureInverting=invertingFeature=[QualifiedName]|invertingFeature=OwnedFeatureChain{ownedRelatedElement+=invertingFeature}</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IFeatureInverting" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildOwnedFeatureInverting(SysML2.NET.Core.POCO.Core.Features.IFeatureInverting poco, TextualNotationWriterContext writerContext, StringBuilder stringBuilder)
        {
            if (poco.OwnedRelatedElement.Contains(poco.InvertingFeature) && poco.InvertingFeature is SysML2.NET.Core.POCO.Core.Features.IFeature chainedInvertingFeatureAsFeature)
            {
                FeatureTextualNotationBuilder.BuildOwnedFeatureChain(chainedInvertingFeatureAsFeature, writerContext, stringBuilder);
            }
            else if (poco.InvertingFeature != null)
            {
                SharedTextualNotationBuilder.AppendQualifiedName(stringBuilder, poco.InvertingFeature, writerContext);
                stringBuilder.Append(' ');
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule FeatureInverting
        /// <para>FeatureInverting=('inverting'Identification?)?'inverse'(featureInverted=[QualifiedName]|featureInverted=OwnedFeatureChain{ownedRelatedElement+=featureInverted})'of'(invertingFeature=[QualifiedName]|ownedRelatedElement+=OwnedFeatureChain{ownedRelatedElement+=invertingFeature})RelationshipBody</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IFeatureInverting" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildFeatureInverting(SysML2.NET.Core.POCO.Core.Features.IFeatureInverting poco, TextualNotationWriterContext writerContext, StringBuilder stringBuilder)
        {
            var ownedRelatedElementCursor = writerContext.CursorCache.GetOrCreateCursor(poco.Id, "ownedRelatedElement", poco.OwnedRelatedElement);

            if (!string.IsNullOrWhiteSpace(poco.DeclaredShortName) || !string.IsNullOrWhiteSpace(poco.DeclaredName))
            {
                stringBuilder.Append("inverting ");

                if (!string.IsNullOrWhiteSpace(poco.DeclaredShortName) || !string.IsNullOrWhiteSpace(poco.DeclaredName))
                {
                    ElementTextualNotationBuilder.BuildIdentification(poco, writerContext, stringBuilder);
                }
                stringBuilder.Append(' ');
            }

            stringBuilder.Append("inverse ");
            if (poco.OwnedRelatedElement.Contains(poco.FeatureInverted) && poco.FeatureInverted is SysML2.NET.Core.POCO.Core.Features.IFeature chainedFeatureInvertedAsFeature)
            {
                FeatureTextualNotationBuilder.BuildOwnedFeatureChain(chainedFeatureInvertedAsFeature, writerContext, stringBuilder);
            }
            else if (poco.FeatureInverted != null)
            {
                SharedTextualNotationBuilder.AppendQualifiedName(stringBuilder, poco.FeatureInverted, writerContext);
                stringBuilder.Append(' ');
            }

            stringBuilder.Append(' ');
            stringBuilder.Append("of ");
            if (poco.OwnedRelatedElement.Contains(poco.InvertingFeature) && poco.InvertingFeature is SysML2.NET.Core.POCO.Core.Features.IFeature chainedInvertingFeatureAsFeature)
            {
                FeatureTextualNotationBuilder.BuildOwnedFeatureChain(chainedInvertingFeatureAsFeature, writerContext, stringBuilder);
            }
            else if (poco.InvertingFeature != null)
            {
                SharedTextualNotationBuilder.AppendQualifiedName(stringBuilder, poco.InvertingFeature, writerContext);
                stringBuilder.Append(' ');
            }

            stringBuilder.Append(' ');
            RelationshipTextualNotationBuilder.BuildRelationshipBody(poco, writerContext, stringBuilder);

        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
