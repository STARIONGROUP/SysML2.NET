// -------------------------------------------------------------------------------------------------
// <copyright file="ClassifierTextualNotationBuilder.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.TextualNotation
{
    using System.Text;

    using SysML2.NET.Core.POCO.Core.Classifiers;
    using SysML2.NET.Core.POCO.Core.Types;

    /// <summary>
    /// Hand-coded part of the <see cref="ClassifierTextualNotationBuilder" />
    /// </summary>
    public static partial class ClassifierTextualNotationBuilder
    {
        /// <summary>
        /// Builds the Textual Notation string for the rule ClassifierDeclaration.
        /// <para>Handles the <c>(SuperclassingPart | ConjugationPart)+ TypeRelationshipPart*</c>
        /// portion that the auto-gen cannot produce correctly (it generates a degenerate switch
        /// on <c>poco</c> type where <c>case IType</c> always matches since <c>IClassifier : IType</c>,
        /// making the <c>default:</c> arm with <c>BuildSuperclassingPart</c> unreachable).</para>
        /// <para>The discriminator must be cursor-element-based:
        /// <c>ISubclassification</c> → SuperclassingPart,
        /// <c>IConjugation</c> → ConjugationPart.</para>
        /// </summary>
        /// <param name="poco">The <see cref="IClassifier" /> from which the rule should be built</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        /// <remarks>
        /// ClassifierDeclaration : Classifier =
        ///     (isSufficient ?= 'all')? Identification (OwnedMultiplicity)?
        ///     (SuperclassingPart | ConjugationPart)+ TypeRelationshipPart*
        ///
        /// The auto-gen handles isSufficient, Identification, and OwnedMultiplicity.
        /// This HandCoded method handles: (SuperclassingPart | ConjugationPart)+ TypeRelationshipPart*
        /// </remarks>
        private static void BuildClassifierDeclarationHandCoded(IClassifier poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);

            // (SuperclassingPart | ConjugationPart)+ — dispatch on cursor element type
            while (ownedRelationshipCursor.Current is ISubclassification or IConjugation)
            {
                if (ownedRelationshipCursor.Current is ISubclassification)
                {
                    BuildSuperclassingPart(poco, cursorCache, stringBuilder);
                }
                else
                {
                    TypeTextualNotationBuilder.BuildConjugationPart(poco, cursorCache, stringBuilder);
                }
            }

            // TypeRelationshipPart* — zero or more
            while (ownedRelationshipCursor.Current is IDisjoining or IUnioning or IIntersecting or IDifferencing)
            {
                TypeTextualNotationBuilder.BuildTypeRelationshipPart(poco, cursorCache, stringBuilder);
            }
        }
    }
}
