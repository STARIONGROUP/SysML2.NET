// -------------------------------------------------------------------------------------------------
// <copyright file="InvariantTextualNotationBuilder.cs" company="Starion Group S.A.">
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
    /// The <see cref="InvariantTextualNotationBuilder" /> provides Textual Notation Builder for the <see cref="SysML2.NET.Core.POCO.Kernel.Functions.IInvariant" /> element
    /// </summary>
    public static partial class InvariantTextualNotationBuilder
    {
        /// <summary>
        /// Builds the Textual Notation string for the rule Invariant
        /// <para>Invariant=FeaturePrefix'inv'('true'|isNegated?='false')?FeatureDeclarationValuePart?FunctionBody</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Kernel.Functions.IInvariant" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildInvariant(SysML2.NET.Core.POCO.Kernel.Functions.IInvariant poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);
            BuildFeaturePrefixHandCoded(poco, cursorCache, stringBuilder);
            stringBuilder.Append(' ');

            while (ownedRelationshipCursor.Current != null)
            {

                if (ownedRelationshipCursor.Current != null)
                {

                    if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Root.Namespaces.IOwningMembership elementAsOwningMembership)
                    {
                        OwningMembershipTextualNotationBuilder.BuildPrefixMetadataMember(elementAsOwningMembership, cursorCache, stringBuilder);
                    }
                }
                ownedRelationshipCursor.Move();

            }

            stringBuilder.Append("inv ");
            if (!poco.IsNegated)
            {
                stringBuilder.Append("true ");

            }
            else
            {
                stringBuilder.Append(" false ");
            }
            FeatureTextualNotationBuilder.BuildFeatureDeclaration(poco, cursorCache, stringBuilder);

            if (poco.OwnedRelationship.Count != 0 || poco.type.Count != 0 || poco.chainingFeature.Count != 0 || !string.IsNullOrWhiteSpace(poco.DeclaredShortName) || !string.IsNullOrWhiteSpace(poco.DeclaredName) || poco.Direction.HasValue || poco.IsDerived || poco.IsAbstract || poco.IsConstant || poco.IsOrdered || poco.IsEnd || poco.importedMembership.Count != 0 || poco.IsComposite || poco.IsPortion || poco.IsVariable || poco.IsSufficient || poco.unioningType.Count != 0 || poco.intersectingType.Count != 0 || poco.differencingType.Count != 0 || poco.featuringType.Count != 0 || poco.ownedTypeFeaturing.Count != 0)
            {
                FeatureTextualNotationBuilder.BuildValuePart(poco, cursorCache, stringBuilder);
            }
            TypeTextualNotationBuilder.BuildFunctionBody(poco, cursorCache, stringBuilder);

        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
