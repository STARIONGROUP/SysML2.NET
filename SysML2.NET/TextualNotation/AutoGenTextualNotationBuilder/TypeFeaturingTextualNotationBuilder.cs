// -------------------------------------------------------------------------------------------------
// <copyright file="TypeFeaturingTextualNotationBuilder.cs" company="Starion Group S.A.">
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
    /// The <see cref="TypeFeaturingTextualNotationBuilder" /> provides Textual Notation Builder for the <see cref="SysML2.NET.Core.POCO.Core.Features.ITypeFeaturing" /> element
    /// </summary>
    public static partial class TypeFeaturingTextualNotationBuilder
    {
        /// <summary>
        /// Builds the Textual Notation string for the rule OwnedTypeFeaturing
        /// <para>OwnedTypeFeaturing:TypeFeaturing=featuringType=[QualifiedName]</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.ITypeFeaturing" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildOwnedTypeFeaturing(SysML2.NET.Core.POCO.Core.Features.ITypeFeaturing poco, TextualNotationWriterContext writerContext, StringBuilder stringBuilder)
        {

            if (poco.FeaturingType != null)
            {
                SharedTextualNotationBuilder.AppendQualifiedName(stringBuilder, poco.FeaturingType, writerContext);
                stringBuilder.Append(' ');
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule TypeFeaturing
        /// <para>TypeFeaturing='featuring'(Identification'of')?featureOfType=[QualifiedName]'by'featuringType=[QualifiedName]RelationshipBody</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.ITypeFeaturing" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildTypeFeaturing(SysML2.NET.Core.POCO.Core.Features.ITypeFeaturing poco, TextualNotationWriterContext writerContext, StringBuilder stringBuilder)
        {
            stringBuilder.Append("featuring ");

            if (!string.IsNullOrWhiteSpace(poco.DeclaredShortName) || !string.IsNullOrWhiteSpace(poco.DeclaredName))
            {
                ElementTextualNotationBuilder.BuildIdentification(poco, writerContext, stringBuilder);
                stringBuilder.Append("of ");
                stringBuilder.Append(' ');
            }


            if (poco.FeatureOfType != null)
            {
                SharedTextualNotationBuilder.AppendQualifiedName(stringBuilder, poco.FeatureOfType, writerContext);
                stringBuilder.Append(' ');
            }
            stringBuilder.Append("by ");

            if (poco.FeaturingType != null)
            {
                SharedTextualNotationBuilder.AppendQualifiedName(stringBuilder, poco.FeaturingType, writerContext);
                stringBuilder.Append(' ');
            }
            RelationshipTextualNotationBuilder.BuildRelationshipBody(poco, writerContext, stringBuilder);

        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
