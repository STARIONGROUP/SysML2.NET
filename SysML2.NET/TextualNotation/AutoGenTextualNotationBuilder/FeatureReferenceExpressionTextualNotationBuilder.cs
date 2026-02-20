// -------------------------------------------------------------------------------------------------
// <copyright file="FeatureReferenceExpressionTextualNotationBuilder.cs" company="Starion Group S.A.">
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
    using System.Text;

    using SysML2.NET.Core.POCO.Root.Elements;

    /// <summary>
    /// The <see cref="FeatureReferenceExpressionTextualNotationBuilder" /> provides Textual Notation Builder for the <see cref="SysML2.NET.Core.POCO.Kernel.Expressions.FeatureReferenceExpression" /> element
    /// </summary>
    public class FeatureReferenceExpressionTextualNotationBuilder : TextualNotationBuilder<SysML2.NET.Core.POCO.Kernel.Expressions.FeatureReferenceExpression>
    {
        /// <summary>
        /// Initializes a new instance of a <see cref="FeatureReferenceExpressionTextualNotationBuilder"/>
        /// </summary>
        /// <param name="facade">The <see cref="ITextualNotationBuilderFacade"/> used to query textual notation of referenced <see cref="IElement"/></param>
        public FeatureReferenceExpressionTextualNotationBuilder(ITextualNotationBuilderFacade facade) : base(facade)
        {
        }

        /// <summary>
        /// Builds the Textual Notation string for the provided <see cref="SysML2.NET.Core.POCO.Kernel.Expressions.FeatureReferenceExpression"/>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Kernel.Expressions.FeatureReferenceExpression"/> from which the textual notation should be build</param>
        /// <returns>The built textual notation string</returns>
        public override string BuildTextualNotation(SysML2.NET.Core.POCO.Kernel.Expressions.FeatureReferenceExpression poco)
        {
            var stringBuilder = new StringBuilder();
            // Rule definition : FeatureReferenceExpression:FeatureReferenceExpression=ownedRelationship+=FeatureReferenceMemberownedRelationship+=EmptyResultMember

            // Assignment Element : ownedRelationship += SysML2.NET.CodeGenerator.Grammar.Model.NonTerminalElement
            // Assignment Element : ownedRelationship += SysML2.NET.CodeGenerator.Grammar.Model.NonTerminalElement

            return stringBuilder.ToString();
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
