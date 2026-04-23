// -------------------------------------------------------------------------------------------------
// <copyright file="FeatureTextualNotationBuilder.cs" company="Starion Group S.A.">
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
    using System.Linq;
    using System.Text;

    using SysML2.NET.Core.POCO.Core.Features;

    /// <summary>
    /// Hand-coded part of the <see cref="FeatureTextualNotationBuilder" />
    /// </summary>
    public static partial class FeatureTextualNotationBuilder
    {
        /// <summary>
        /// Builds the Textual Notation string for the rule BasicFeaturePrefix
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IFeature" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        /// <remarks>Trailing alternative of BasicFeaturePrefix: (isVariable?='var'|isConstant?='const'{isVariable=true})?. Note: 'const' implies isVariable=true, so check isConstant first.</remarks>
        private static void BuildBasicFeaturePrefixHandCoded(IFeature poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            if (poco.IsConstant)
            {
                stringBuilder.Append(" const ");
            }
            else if (poco.IsVariable)
            {
                stringBuilder.Append(" var ");
            }
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule FeatureDeclaration
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IFeature" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        private static void BuildFeatureDeclarationHandCoded(IFeature poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("BuildFeatureDeclarationHandCoded requires manual implementation");
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule Feature
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IFeature" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        private static void BuildFeatureHandCoded(IFeature poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("BuildFeatureHandCoded requires manual implementation");
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule FeatureIdentification
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IFeature" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        /// <remarks>FeatureIdentification:Feature='&lt;'declaredShortName=NAME'&gt;'(declaredName=NAME)?|declaredName=NAME</remarks>
        private static void BuildFeatureIdentificationHandCoded(IFeature poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            if (!string.IsNullOrWhiteSpace(poco.DeclaredShortName))
            {
                stringBuilder.Append('<');
                stringBuilder.Append(poco.DeclaredShortName);
                stringBuilder.Append('>');

                if (!string.IsNullOrWhiteSpace(poco.DeclaredName))
                {
                    stringBuilder.Append(' ');
                    stringBuilder.Append(poco.DeclaredName);
                }

                stringBuilder.Append(' ');
            }
            else if (!string.IsNullOrWhiteSpace(poco.DeclaredName))
            {
                stringBuilder.Append(poco.DeclaredName);
                stringBuilder.Append(' ');
            }
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule FeatureSpecializationPart
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IFeature" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        private static void BuildFeatureSpecializationPartHandCoded(IFeature poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("BuildFeatureSpecializationPartHandCoded requires manual implementation");
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule MultiplicityPart
        /// <remarks>MultiplicityPart:Feature=ownedRelationship+=OwnedMultiplicity|(ownedRelationship+=OwnedMultiplicity)?(isOrdered?='ordered'({isUnique=false}'nonunique')?|{isUnique=false}'nonunique'(isOrdered?='ordered')?)</remarks>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IFeature" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        private static void BuildMultiplicityPartHandCoded(IFeature poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            // Emit the OwnedMultiplicity if present (cursor advances on += processing)
            var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);

            if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Root.Namespaces.IOwningMembership multiplicityMember
                && multiplicityMember.OwnedRelatedElement.OfType<SysML2.NET.Core.POCO.Core.Types.IMultiplicity>().Any())
            {
                OwningMembershipTextualNotationBuilder.BuildOwnedMultiplicity(multiplicityMember, cursorCache, stringBuilder);
                ownedRelationshipCursor.Move();
            }

            // Emit the ordered/nonunique modifiers based on the Feature's flags.
            // IsUnique defaults to true; 'nonunique' is emitted when IsUnique == false.
            if (poco.IsOrdered)
            {
                stringBuilder.Append("ordered ");
            }

            if (!poco.IsUnique)
            {
                stringBuilder.Append("nonunique ");
            }
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule PayloadFeature
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IFeature" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        private static void BuildPayloadFeatureHandCoded(IFeature poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("BuildPayloadFeatureHandCoded requires manual implementation");
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule PayloadFeatureSpecializationPart
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IFeature" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        private static void BuildPayloadFeatureSpecializationPartHandCoded(IFeature poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("BuildPayloadFeatureSpecializationPartHandCoded requires manual implementation");
        }
    }
}
