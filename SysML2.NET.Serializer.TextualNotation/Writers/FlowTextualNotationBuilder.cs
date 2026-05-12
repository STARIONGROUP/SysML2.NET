// -------------------------------------------------------------------------------------------------
// <copyright file="FlowTextualNotationBuilder.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Serializer.TextualNotation.Writers
{
    using System.Text;

    using SysML2.NET.Core.POCO.Kernel.Interactions;

    /// <summary>
    /// Hand-coded part of the <see cref="FlowTextualNotationBuilder" />
    /// </summary>
    public static partial class FlowTextualNotationBuilder
    {
        /// <summary>
        /// Builds the Textual Notation string for the rule FlowDeclaration
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Kernel.Interactions.IFlow" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        /// <remarks>
        /// FlowDeclaration : Flow =
        ///     FeatureDeclaration ValuePart?
        ///     ( 'of' ownedRelationship += PayloadFeatureMember )?
        ///     ( 'from' ownedRelationship += FlowEndMember 'to' ownedRelationship += FlowEndMember )?
        ///   | ( isSufficient ?= 'all' )?
        ///     ownedRelationship += FlowEndMember 'to' ownedRelationship += FlowEndMember
        ///
        /// Auto-gen emits FeaturePrefix + 'flow ' before and TypeBody after this method. Body delegates
        /// to the shared <see cref="SharedTextualNotationBuilder.BuildFlowDeclarationHandCoded"/> helper —
        /// the rule shares its body with <c>SuccessionFlow</c>'s <c>FlowDeclaration</c>.
        /// </remarks>
        private static void BuildFlowDeclarationHandCoded(IFlow poco, TextualNotationWriterContext writerContext, StringBuilder stringBuilder)
        {
            SharedTextualNotationBuilder.BuildFlowDeclarationHandCoded(poco, writerContext, stringBuilder);
        }
    }
}
