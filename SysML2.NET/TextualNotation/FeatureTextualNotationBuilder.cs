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
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Core.POCO.Systems.Metadata;

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
                stringBuilder.Append("const ");
            }
            else if (poco.IsVariable)
            {
                stringBuilder.Append("var ");
            }
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule FeatureDeclaration
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IFeature" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        /// <remarks>
        /// FeatureDeclaration : Feature =
        ///     ( isSufficient ?= 'all' )?    — handled by auto-gen, NOT here
        ///     ( FeatureIdentification ( FeatureSpecializationPart | ConjugationPart )?
        ///     | FeatureSpecializationPart
        ///     | ConjugationPart
        ///     )
        ///     FeatureRelationshipPart*
        ///
        /// </remarks>
        private static void BuildFeatureDeclarationHandCoded(IFeature poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);

            var hasIdentification = !string.IsNullOrWhiteSpace(poco.DeclaredShortName) || !string.IsNullOrWhiteSpace(poco.DeclaredName);

            if (hasIdentification)
            {
                BuildFeatureIdentification(poco, cursorCache, stringBuilder);

                if (ownedRelationshipCursor.Current is ISpecialization)
                {
                    BuildFeatureSpecializationPart(poco, cursorCache, stringBuilder);
                }
                else if (ownedRelationshipCursor.Current is IConjugation)
                {
                    TypeTextualNotationBuilder.BuildConjugationPart(poco, cursorCache, stringBuilder);
                }
            }
            else if (ownedRelationshipCursor.Current is ISpecialization)
            {
                BuildFeatureSpecializationPart(poco, cursorCache, stringBuilder);
            }
            else if (ownedRelationshipCursor.Current is IConjugation)
            {
                TypeTextualNotationBuilder.BuildConjugationPart(poco, cursorCache, stringBuilder);
            }

            // FeatureRelationshipPart* — zero or more, dispatched by cursor type.
            // FeatureRelationshipPart = TypeRelationshipPart | ChainingPart | InvertingPart | TypeFeaturingPart
            while (ownedRelationshipCursor.Current is IDisjoining
                   || ownedRelationshipCursor.Current is IUnioning
                   || ownedRelationshipCursor.Current is IIntersecting
                   || ownedRelationshipCursor.Current is IDifferencing
                   || ownedRelationshipCursor.Current is IFeatureChaining
                   || ownedRelationshipCursor.Current is IFeatureInverting
                   || ownedRelationshipCursor.Current is ITypeFeaturing)
            {
                switch (ownedRelationshipCursor.Current)
                {
                    case IFeatureChaining:
                        BuildChainingPart(poco, cursorCache, stringBuilder);
                        break;
                    case IFeatureInverting:
                        BuildInvertingPart(poco, cursorCache, stringBuilder);
                        break;
                    case ITypeFeaturing:
                        BuildTypeFeaturingPart(poco, cursorCache, stringBuilder);
                        break;
                    default:
                        TypeTextualNotationBuilder.BuildTypeRelationshipPart(poco, cursorCache, stringBuilder);
                        break;
                }
            }
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule Feature
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IFeature" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        /// <remarks>
        /// Feature =
        ///     ( FeaturePrefix ( 'feature' | ownedRelationship += PrefixMetadataMember ) FeatureDeclaration?
        ///     | ( EndFeaturePrefix | BasicFeaturePrefix ) FeatureDeclaration )
        ///     ValuePart? TypeBody
        ///
        /// Auto-gen handles ValuePart and TypeBody after this method.
        /// This method emits: prefix + optional 'feature' keyword + declaration.
        ///
        /// SharedTextualNotationBuilder.BuildFeaturePrefix dispatches to EndFeaturePrefix
        /// or BasicFeaturePrefix and consumes PrefixMetadataMember* from the cursor.
        /// BuildFeatureDeclaration is then called to emit the identification, specialization,
        /// conjugation, and relationship parts.
        /// </remarks>
        private static void BuildFeatureHandCoded(IFeature poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            // Alt 1 uses FeaturePrefix (which handles abstract/variation/readonly/derived/end
            // and PrefixMetadataMember*). It applies when the feature carries modifiers that
            // BasicFeaturePrefix cannot express, or has PrefixMetadataMember annotations.
            // EndFeaturePrefix features take Alt 2.
            var hasPrefixMetadata = poco.OwnedRelationship
                .OfType<IOwningMembership>()
                .Any(owningMembership => owningMembership.OwnedRelatedElement.OfType<IMetadataUsage>().Any());

            if (!poco.IsEnd && (poco.IsAbstract || poco.IsDerived || hasPrefixMetadata))
            {
                // Alt 1: FeaturePrefix ('feature' | ownedRelationship += PrefixMetadataMember) FeatureDeclaration?
                SharedTextualNotationBuilder.BuildFeaturePrefix(poco, cursorCache, stringBuilder);

                var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);

                if (ownedRelationshipCursor.Current is IOwningMembership prefixMetadata
                    && prefixMetadata.OwnedRelatedElement.OfType<IMetadataUsage>().Any())
                {
                    // PrefixMetadataMember alternative — consume a single metadata annotation
                    OwningMembershipTextualNotationBuilder.BuildPrefixMetadataMember(prefixMetadata, cursorCache, stringBuilder);
                    ownedRelationshipCursor.Move();
                }
                else
                {
                    // 'feature' keyword alternative
                    stringBuilder.Append("feature ");
                }

                // FeatureDeclaration is optional in Alt 1
                BuildFeatureDeclaration(poco, cursorCache, stringBuilder);
            }
            else
            {
                // Alt 2: (EndFeaturePrefix | BasicFeaturePrefix) FeatureDeclaration
                SharedTextualNotationBuilder.BuildFeaturePrefix(poco, cursorCache, stringBuilder);
                BuildFeatureDeclaration(poco, cursorCache, stringBuilder);
            }
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
        /// Builds the Textual Notation string for the rule FeatureSpecializationPart.
        /// <para><c>FeatureSpecializationPart : Feature =
        /// FeatureSpecialization+ MultiplicityPart? FeatureSpecialization*
        /// | MultiplicityPart FeatureSpecialization*</c></para>
        /// <para>Branches on whether the cursor starts on a specialization or not, mirroring the grammar's
        /// two alternatives. <c>while</c>-loops on <c>cursor.Current is ISpecialization</c> drive the
        /// <c>+</c>/<c>*</c> quantifiers; <see cref="BuildFeatureSpecialization"/> is the existing dispatcher
        /// that consumes a contiguous run of one specialization category per call.</para>
        /// <para><see cref="BuildMultiplicityPart"/> matches when any of {an <see cref="IOwningMembership"/>
        /// wrapping an <see cref="IMultiplicity"/> at the cursor, <c>poco.IsOrdered</c>, <c>!poco.IsUnique</c>}
        /// is present — see the rule's own grammar in <c>BuildMultiplicityPartHandCoded</c>'s remarks.</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IFeature" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        private static void BuildFeatureSpecializationPartHandCoded(IFeature poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);

            if (ownedRelationshipCursor.Current is ISpecialization)
            {
                // Alt 1: FeatureSpecialization+ MultiplicityPart? FeatureSpecialization*
                while (ownedRelationshipCursor.Current is ISpecialization)
                {
                    BuildFeatureSpecialization(poco, cursorCache, stringBuilder);
                }

                var multiplicityElementPresent = ownedRelationshipCursor.Current is IOwningMembership owningMembership
                    && owningMembership.OwnedRelatedElement.OfType<IMultiplicity>().Any();

                if (multiplicityElementPresent || poco.IsOrdered || !poco.IsUnique)
                {
                    BuildMultiplicityPart(poco, cursorCache, stringBuilder);
                }

                // Trailing FeatureSpecialization* — runs regardless of whether MultiplicityPart fired,
                // per the grammar's three-segment shape `FeatureSpecialization+ MultiplicityPart? FeatureSpecialization*`.
                while (ownedRelationshipCursor.Current is ISpecialization)
                {
                    BuildFeatureSpecialization(poco, cursorCache, stringBuilder);
                }
            }
            else
            {
                // Alt 2: MultiplicityPart FeatureSpecialization*
                BuildMultiplicityPart(poco, cursorCache, stringBuilder);

                while (ownedRelationshipCursor.Current is ISpecialization)
                {
                    BuildFeatureSpecialization(poco, cursorCache, stringBuilder);
                }
            }
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
        /// <remarks>
        /// PayloadFeature : Feature =
        ///     Identification? PayloadFeatureSpecializationPart ValuePart?
        ///   | ownedRelationship += OwnedFeatureTyping ( ownedRelationship += OwnedMultiplicity )?
        ///   | ownedRelationship += OwnedMultiplicity ownedRelationship += OwnedFeatureTyping
        /// </remarks>
        private static void BuildPayloadFeatureHandCoded(IFeature poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);

            var hasIdentification = !string.IsNullOrWhiteSpace(poco.DeclaredShortName) || !string.IsNullOrWhiteSpace(poco.DeclaredName);

            if (hasIdentification || ownedRelationshipCursor.Current is ISpecialization)
            {
                // Alt 1: Identification? PayloadFeatureSpecializationPart ValuePart?
                if (hasIdentification)
                {
                    ElementTextualNotationBuilder.BuildIdentification(poco, cursorCache, stringBuilder);
                }

                if (ownedRelationshipCursor.Current is ISpecialization
                    || ownedRelationshipCursor.Current is IOwningMembership multiplicityCheck
                       && multiplicityCheck.OwnedRelatedElement.OfType<IMultiplicity>().Any())
                {
                    BuildPayloadFeatureSpecializationPart(poco, cursorCache, stringBuilder);
                }

                BuildValuePart(poco, cursorCache, stringBuilder);
            }
            else if (ownedRelationshipCursor.Current is IFeatureTyping featureTyping)
            {
                // Alt 2: OwnedFeatureTyping (OwnedMultiplicity)?
                FeatureTypingTextualNotationBuilder.BuildOwnedFeatureTyping(featureTyping, cursorCache, stringBuilder);
                ownedRelationshipCursor.Move();

                if (ownedRelationshipCursor.Current is IOwningMembership owningMembership
                    && owningMembership.OwnedRelatedElement.OfType<IMultiplicity>().Any())
                {
                    OwningMembershipTextualNotationBuilder.BuildOwnedMultiplicity(owningMembership, cursorCache, stringBuilder);
                    ownedRelationshipCursor.Move();
                }
            }
            else if (ownedRelationshipCursor.Current is IOwningMembership multiplicityMember
                     && multiplicityMember.OwnedRelatedElement.OfType<IMultiplicity>().Any())
            {
                // Alt 3: OwnedMultiplicity OwnedFeatureTyping
                OwningMembershipTextualNotationBuilder.BuildOwnedMultiplicity(multiplicityMember, cursorCache, stringBuilder);
                ownedRelationshipCursor.Move();

                if (ownedRelationshipCursor.Current is IFeatureTyping featureTypingAfterMult)
                {
                    FeatureTypingTextualNotationBuilder.BuildOwnedFeatureTyping(featureTypingAfterMult, cursorCache, stringBuilder);
                    ownedRelationshipCursor.Move();
                }
            }
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule PayloadFeatureSpecializationPart
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IFeature" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        /// <remarks>
        /// PayloadFeatureSpecializationPart : Feature =
        ///     ( FeatureSpecialization )+ MultiplicityPart? FeatureSpecialization*
        ///   | MultiplicityPart FeatureSpecialization+
        ///
        /// Structurally identical to FeatureSpecializationPart; the only grammar difference
        /// (Alt 2 uses '+' instead of '*') is a parse-time validation concern, not a
        /// serialization difference.
        /// </remarks>
        private static void BuildPayloadFeatureSpecializationPartHandCoded(IFeature poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);

            if (ownedRelationshipCursor.Current is ISpecialization)
            {
                // Alt 1: FeatureSpecialization+ MultiplicityPart? FeatureSpecialization*
                while (ownedRelationshipCursor.Current is ISpecialization)
                {
                    BuildFeatureSpecialization(poco, cursorCache, stringBuilder);
                }

                var multiplicityElementPresent = ownedRelationshipCursor.Current is IOwningMembership owningMembership
                    && owningMembership.OwnedRelatedElement.OfType<IMultiplicity>().Any();

                if (multiplicityElementPresent || poco.IsOrdered || !poco.IsUnique)
                {
                    BuildMultiplicityPart(poco, cursorCache, stringBuilder);
                }

                while (ownedRelationshipCursor.Current is ISpecialization)
                {
                    BuildFeatureSpecialization(poco, cursorCache, stringBuilder);
                }
            }
            else
            {
                // Alt 2: MultiplicityPart FeatureSpecialization+
                BuildMultiplicityPart(poco, cursorCache, stringBuilder);

                while (ownedRelationshipCursor.Current is ISpecialization)
                {
                    BuildFeatureSpecialization(poco, cursorCache, stringBuilder);
                }
            }
        }
    }
}
