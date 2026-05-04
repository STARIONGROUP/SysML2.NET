// -------------------------------------------------------------------------------------------------
// <copyright file="RedefinitionTextualNotationBuilder.cs" company="Starion Group S.A.">
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
    /// The <see cref="RedefinitionTextualNotationBuilder" /> provides Textual Notation Builder for the <see cref="SysML2.NET.Core.POCO.Core.Features.IRedefinition" /> element
    /// </summary>
    public static partial class RedefinitionTextualNotationBuilder
    {
        /// <summary>
        /// Builds the Textual Notation string for the rule OwnedRedefinition
        /// <para>OwnedRedefinition:Redefinition=redefinedFeature=[QualifiedName]|redefinedFeature=OwnedFeatureChain{ownedRelatedElement+=redefinedFeature}</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IRedefinition" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildOwnedRedefinition(SysML2.NET.Core.POCO.Core.Features.IRedefinition poco, TextualNotationWriterContext writerContext, StringBuilder stringBuilder)
        {
            if (poco.OwnedRelatedElement.Contains(poco.RedefinedFeature) && poco.RedefinedFeature is SysML2.NET.Core.POCO.Core.Features.IFeature chainedRedefinedFeatureAsFeature)
            {
                FeatureTextualNotationBuilder.BuildOwnedFeatureChain(chainedRedefinedFeatureAsFeature, writerContext, stringBuilder);
            }
            else if (poco.RedefinedFeature != null)
            {
                SharedTextualNotationBuilder.AppendQualifiedName(stringBuilder, poco.RedefinedFeature, writerContext);
                stringBuilder.Append(' ');
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule FlowFeatureRedefinition
        /// <para>FlowFeatureRedefinition:Redefinition=redefinedFeature=[QualifiedName]</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IRedefinition" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildFlowFeatureRedefinition(SysML2.NET.Core.POCO.Core.Features.IRedefinition poco, TextualNotationWriterContext writerContext, StringBuilder stringBuilder)
        {

            if (poco.RedefinedFeature != null)
            {
                SharedTextualNotationBuilder.AppendQualifiedName(stringBuilder, poco.RedefinedFeature, writerContext);
                stringBuilder.Append(' ');
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule ParameterRedefinition
        /// <para>ParameterRedefinition:Redefinition=redefinedFeature=[QualifiedName]</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IRedefinition" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildParameterRedefinition(SysML2.NET.Core.POCO.Core.Features.IRedefinition poco, TextualNotationWriterContext writerContext, StringBuilder stringBuilder)
        {

            if (poco.RedefinedFeature != null)
            {
                SharedTextualNotationBuilder.AppendQualifiedName(stringBuilder, poco.RedefinedFeature, writerContext);
                stringBuilder.Append(' ');
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule Redefinition
        /// <para>Redefinition=('specialization'Identification)?'redefinition'SpecificTypeREDEFINESGeneralTypeRelationshipBody</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IRedefinition" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext" /> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildRedefinition(SysML2.NET.Core.POCO.Core.Features.IRedefinition poco, TextualNotationWriterContext writerContext, StringBuilder stringBuilder)
        {

            if (!string.IsNullOrWhiteSpace(poco.DeclaredShortName) || !string.IsNullOrWhiteSpace(poco.DeclaredName))
            {
                stringBuilder.Append("specialization ");
                ElementTextualNotationBuilder.BuildIdentification(poco, writerContext, stringBuilder);
                stringBuilder.Append(' ');
            }

            stringBuilder.Append("redefinition ");
            SpecializationTextualNotationBuilder.BuildSpecificType(poco, writerContext, stringBuilder);
            stringBuilder.Append(" :>> ");
            SpecializationTextualNotationBuilder.BuildGeneralType(poco, writerContext, stringBuilder);
            RelationshipTextualNotationBuilder.BuildRelationshipBody(poco, writerContext, stringBuilder);

        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
