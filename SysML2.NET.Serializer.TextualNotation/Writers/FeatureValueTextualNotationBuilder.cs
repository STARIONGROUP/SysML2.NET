// ------------------------------------------------------------------------------------------------
// <copyright file="FeatureValueTextualNotationBuilder.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Serializer.TextualNotation.Writers
{

    using SysML2.NET.Core.POCO.Kernel.FeatureValues;
    using SysML2.NET.Core.POCO.Kernel.Functions;

    /// <summary>
    /// Hand-coded part of the <see cref="FeatureValueTextualNotationBuilder"/>
    /// </summary>
    public static partial class FeatureValueTextualNotationBuilder
    {
        /// <summary>
        /// Builds the Textual Notation string for the rule FeatureValue
        /// <remarks>FeatureValue=('='|isInitial?=':='|isDefault?='default'('='|isInitial?=':=')?)ownedRelatedElement+=OwnedExpression</remarks>
        /// </summary>
        /// <param name="poco">The <see cref="IFeatureValue" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="IndentedStringBuilder" /> that contains the entire textual notation</param>
        private static void BuildFeatureValueHandCoded(IFeatureValue poco, TextualNotationWriterContext writerContext, IndentedStringBuilder stringBuilder)
        {
            if (poco.IsDefault)
            {
                stringBuilder.Append(" default ");

                if (poco.IsInitial)
                {
                    stringBuilder.Append(":= ");
                }
                else
                {
                    stringBuilder.Append("= ");
                }
            }
            else if (poco.IsInitial)
            {
                stringBuilder.Append(" := ");
            }
            else
            {
                stringBuilder.Append(" = ");
            }

            // OwnedExpression consumption is handled by the auto-gen caller (BuildFeatureValue),
            // which checks ownedRelatedElementCursor.Current and calls BuildOwnedExpression + Move().
        }
    }
}
