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

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.TextualNotation
{
    using System.Linq;
    using System.Text;

    using SysML2.NET.Core.POCO.Root.Elements;

    /// <summary>
    /// The <see cref="FeatureTextualNotationBuilder" /> provides Textual Notation Builder for the <see cref="SysML2.NET.Core.POCO.Core.Features.IFeature" /> element
    /// </summary>
    public static partial class FeatureTextualNotationBuilder
    {
        /// <summary>
        /// Builds the Textual Notation string for the rule ValuePart
        /// <para>ValuePart:Feature=ownedRelationship+=FeatureValue</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IFeature" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildValuePart(SysML2.NET.Core.POCO.Core.Features.IFeature poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);

            if (ownedRelationshipCursor.Current != null)
            {

                if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Kernel.FeatureValues.IFeatureValue elementAsFeatureValue)
                {
                    FeatureValueTextualNotationBuilder.BuildFeatureValue(elementAsFeatureValue, cursorCache, stringBuilder);
                }
            }
            ownedRelationshipCursor.Move();


        }

        /// <summary>
        /// Builds the Textual Notation string for the rule FeatureSpecializationPart
        /// <para>FeatureSpecializationPart:Feature=FeatureSpecialization+MultiplicityPart?FeatureSpecialization*|MultiplicityPartFeatureSpecialization*</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IFeature" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildFeatureSpecializationPart(SysML2.NET.Core.POCO.Core.Features.IFeature poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            BuildFeatureSpecializationPartHandCoded(poco, cursorCache, stringBuilder);
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule FeatureSpecialization
        /// <para>FeatureSpecialization:Feature=Typings|Subsettings|References|Crosses|Redefinitions</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IFeature" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildFeatureSpecialization(SysML2.NET.Core.POCO.Core.Features.IFeature poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            switch (poco)
            {
                case SysML2.NET.Core.POCO.Core.Features.IFeature pocoFeatureTypings when pocoFeatureTypings.IsValidForTypings():
                    BuildTypings(pocoFeatureTypings, cursorCache, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Core.Features.IFeature pocoFeatureSubsettings when pocoFeatureSubsettings.IsValidForSubsettings():
                    BuildSubsettings(pocoFeatureSubsettings, cursorCache, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Core.Features.IFeature pocoFeatureReferences when pocoFeatureReferences.IsValidForReferences():
                    BuildReferences(pocoFeatureReferences, cursorCache, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Core.Features.IFeature pocoFeatureCrosses when pocoFeatureCrosses.IsValidForCrosses():
                    BuildCrosses(pocoFeatureCrosses, cursorCache, stringBuilder);
                    break;
                default:
                    BuildRedefinitions(poco, cursorCache, stringBuilder);
                    break;
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule Typings
        /// <para>Typings:Feature=TypedBy(','ownedRelationship+=FeatureTyping)*</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IFeature" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildTypings(SysML2.NET.Core.POCO.Core.Features.IFeature poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);
            BuildTypedBy(poco, cursorCache, stringBuilder);

            while (ownedRelationshipCursor.Current != null && ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Features.IFeatureTyping)
            {
                stringBuilder.Append(", ");

                if (ownedRelationshipCursor.Current != null)
                {

                    if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Features.IFeatureTyping elementAsFeatureTyping)
                    {
                        FeatureTypingTextualNotationBuilder.BuildFeatureTyping(elementAsFeatureTyping, cursorCache, stringBuilder);
                    }
                }
                ownedRelationshipCursor.Move();

            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule TypedBy
        /// <para>TypedBy:Feature=DEFINED_BYownedRelationship+=FeatureTyping</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IFeature" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildTypedBy(SysML2.NET.Core.POCO.Core.Features.IFeature poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);
            stringBuilder.Append(": ");

            if (ownedRelationshipCursor.Current != null)
            {

                if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Features.IFeatureTyping elementAsFeatureTyping)
                {
                    FeatureTypingTextualNotationBuilder.BuildFeatureTyping(elementAsFeatureTyping, cursorCache, stringBuilder);
                }
            }
            ownedRelationshipCursor.Move();


        }

        /// <summary>
        /// Builds the Textual Notation string for the rule Subsettings
        /// <para>Subsettings:Feature=Subsets(','ownedRelationship+=OwnedSubsetting)*</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IFeature" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildSubsettings(SysML2.NET.Core.POCO.Core.Features.IFeature poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);
            BuildSubsets(poco, cursorCache, stringBuilder);

            while (ownedRelationshipCursor.Current != null && ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Features.ISubsetting)
            {
                stringBuilder.Append(", ");

                if (ownedRelationshipCursor.Current != null)
                {

                    if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Features.ISubsetting elementAsSubsetting)
                    {
                        SubsettingTextualNotationBuilder.BuildOwnedSubsetting(elementAsSubsetting, cursorCache, stringBuilder);
                    }
                }
                ownedRelationshipCursor.Move();

            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule Subsets
        /// <para>Subsets:Feature=SUBSETSownedRelationship+=OwnedSubsetting</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IFeature" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildSubsets(SysML2.NET.Core.POCO.Core.Features.IFeature poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);
            stringBuilder.Append(" :> ");

            if (ownedRelationshipCursor.Current != null)
            {

                if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Features.ISubsetting elementAsSubsetting)
                {
                    SubsettingTextualNotationBuilder.BuildOwnedSubsetting(elementAsSubsetting, cursorCache, stringBuilder);
                }
            }
            ownedRelationshipCursor.Move();


        }

        /// <summary>
        /// Builds the Textual Notation string for the rule References
        /// <para>References:Feature=REFERENCESownedRelationship+=OwnedReferenceSubsetting</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IFeature" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildReferences(SysML2.NET.Core.POCO.Core.Features.IFeature poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);
            stringBuilder.Append(" ::> ");

            if (ownedRelationshipCursor.Current != null)
            {

                if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Features.IReferenceSubsetting elementAsReferenceSubsetting)
                {
                    ReferenceSubsettingTextualNotationBuilder.BuildOwnedReferenceSubsetting(elementAsReferenceSubsetting, cursorCache, stringBuilder);
                }
            }
            ownedRelationshipCursor.Move();


        }

        /// <summary>
        /// Builds the Textual Notation string for the rule Crosses
        /// <para>Crosses:Feature=CROSSESownedRelationship+=OwnedCrossSubsetting</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IFeature" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildCrosses(SysML2.NET.Core.POCO.Core.Features.IFeature poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);
            stringBuilder.Append(" => ");

            if (ownedRelationshipCursor.Current != null)
            {

                if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Features.ICrossSubsetting elementAsCrossSubsetting)
                {
                    CrossSubsettingTextualNotationBuilder.BuildOwnedCrossSubsetting(elementAsCrossSubsetting, cursorCache, stringBuilder);
                }
            }
            ownedRelationshipCursor.Move();


        }

        /// <summary>
        /// Builds the Textual Notation string for the rule Redefinitions
        /// <para>Redefinitions:Feature=Redefines(','ownedRelationship+=OwnedRedefinition)*</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IFeature" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildRedefinitions(SysML2.NET.Core.POCO.Core.Features.IFeature poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);
            BuildRedefines(poco, cursorCache, stringBuilder);

            while (ownedRelationshipCursor.Current != null && ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Features.IRedefinition)
            {
                stringBuilder.Append(", ");

                if (ownedRelationshipCursor.Current != null)
                {

                    if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Features.IRedefinition elementAsRedefinition)
                    {
                        RedefinitionTextualNotationBuilder.BuildOwnedRedefinition(elementAsRedefinition, cursorCache, stringBuilder);
                    }
                }
                ownedRelationshipCursor.Move();

            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule Redefines
        /// <para>Redefines:Feature=REDEFINESownedRelationship+=OwnedRedefinition</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IFeature" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildRedefines(SysML2.NET.Core.POCO.Core.Features.IFeature poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);
            stringBuilder.Append(" :>> ");

            if (ownedRelationshipCursor.Current != null)
            {

                if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Features.IRedefinition elementAsRedefinition)
                {
                    RedefinitionTextualNotationBuilder.BuildOwnedRedefinition(elementAsRedefinition, cursorCache, stringBuilder);
                }
            }
            ownedRelationshipCursor.Move();


        }

        /// <summary>
        /// Builds the Textual Notation string for the rule OwnedFeatureChain
        /// <para>OwnedFeatureChain:Feature=ownedRelationship+=OwnedFeatureChaining('.'ownedRelationship+=OwnedFeatureChaining)+</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IFeature" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildOwnedFeatureChain(SysML2.NET.Core.POCO.Core.Features.IFeature poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);

            if (ownedRelationshipCursor.Current != null)
            {

                if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Features.IFeatureChaining elementAsFeatureChaining)
                {
                    FeatureChainingTextualNotationBuilder.BuildOwnedFeatureChaining(elementAsFeatureChaining, cursorCache, stringBuilder);
                }
            }
            ownedRelationshipCursor.Move();


            while (ownedRelationshipCursor.Current != null && ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Features.IFeatureChaining)
            {
                stringBuilder.Append(".");

                if (ownedRelationshipCursor.Current != null)
                {

                    if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Features.IFeatureChaining elementAsFeatureChaining)
                    {
                        FeatureChainingTextualNotationBuilder.BuildOwnedFeatureChaining(elementAsFeatureChaining, cursorCache, stringBuilder);
                    }
                }
                ownedRelationshipCursor.Move();

            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule MultiplicityPart
        /// <para>MultiplicityPart:Feature=ownedRelationship+=OwnedMultiplicity|(ownedRelationship+=OwnedMultiplicity)?(isOrdered?='ordered'({isUnique=false}'nonunique')?|{isUnique=false}'nonunique'(isOrdered?='ordered')?)</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IFeature" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildMultiplicityPart(SysML2.NET.Core.POCO.Core.Features.IFeature poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            BuildMultiplicityPartHandCoded(poco, cursorCache, stringBuilder);
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule OwnedCrossMultiplicity
        /// <para>OwnedCrossMultiplicity:Feature=ownedRelationship+=OwnedMultiplicity</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IFeature" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildOwnedCrossMultiplicity(SysML2.NET.Core.POCO.Core.Features.IFeature poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);

            if (ownedRelationshipCursor.Current != null)
            {

                if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Root.Namespaces.IOwningMembership elementAsOwningMembership)
                {
                    OwningMembershipTextualNotationBuilder.BuildOwnedMultiplicity(elementAsOwningMembership, cursorCache, stringBuilder);
                }
            }
            ownedRelationshipCursor.Move();


        }

        /// <summary>
        /// Builds the Textual Notation string for the rule PayloadFeature
        /// <para>PayloadFeature:Feature=Identification?PayloadFeatureSpecializationPartValuePart?|ownedRelationship+=OwnedFeatureTyping(ownedRelationship+=OwnedMultiplicity)?|ownedRelationship+=OwnedMultiplicityownedRelationship+=OwnedFeatureTyping</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IFeature" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildPayloadFeature(SysML2.NET.Core.POCO.Core.Features.IFeature poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            BuildPayloadFeatureHandCoded(poco, cursorCache, stringBuilder);
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule PayloadFeatureSpecializationPart
        /// <para>PayloadFeatureSpecializationPart:Feature=(FeatureSpecialization)+MultiplicityPart?FeatureSpecialization*|MultiplicityPartFeatureSpecialization+</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IFeature" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildPayloadFeatureSpecializationPart(SysML2.NET.Core.POCO.Core.Features.IFeature poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            BuildPayloadFeatureSpecializationPartHandCoded(poco, cursorCache, stringBuilder);
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule FeatureChainPrefix
        /// <para>FeatureChainPrefix:Feature=(ownedRelationship+=OwnedFeatureChaining'.')+ownedRelationship+=OwnedFeatureChaining'.'</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IFeature" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildFeatureChainPrefix(SysML2.NET.Core.POCO.Core.Features.IFeature poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);

            while (ownedRelationshipCursor.Current != null && ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Features.IFeatureChaining)
            {

                if (ownedRelationshipCursor.Current != null)
                {

                    if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Features.IFeatureChaining elementAsFeatureChaining)
                    {
                        FeatureChainingTextualNotationBuilder.BuildOwnedFeatureChaining(elementAsFeatureChaining, cursorCache, stringBuilder);
                    }
                }
                stringBuilder.Append(".");
                ownedRelationshipCursor.Move();

            }
            stringBuilder.Append(' ');

            if (ownedRelationshipCursor.Current != null)
            {

                if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Features.IFeatureChaining elementAsFeatureChaining)
                {
                    FeatureChainingTextualNotationBuilder.BuildOwnedFeatureChaining(elementAsFeatureChaining, cursorCache, stringBuilder);
                }
            }
            ownedRelationshipCursor.Move();

            stringBuilder.Append(".");

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule TriggerValuePart
        /// <para>TriggerValuePart:Feature=ownedRelationship+=TriggerFeatureValue</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IFeature" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildTriggerValuePart(SysML2.NET.Core.POCO.Core.Features.IFeature poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);

            if (ownedRelationshipCursor.Current != null)
            {

                if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Kernel.FeatureValues.IFeatureValue elementAsFeatureValue)
                {
                    FeatureValueTextualNotationBuilder.BuildTriggerFeatureValue(elementAsFeatureValue, cursorCache, stringBuilder);
                }
            }
            ownedRelationshipCursor.Move();


        }

        /// <summary>
        /// Builds the Textual Notation string for the rule Argument
        /// <para>Argument:Feature=ownedRelationship+=ArgumentValue</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IFeature" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildArgument(SysML2.NET.Core.POCO.Core.Features.IFeature poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);

            if (ownedRelationshipCursor.Current != null)
            {

                if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Kernel.FeatureValues.IFeatureValue elementAsFeatureValue)
                {
                    FeatureValueTextualNotationBuilder.BuildArgumentValue(elementAsFeatureValue, cursorCache, stringBuilder);
                }
            }
            ownedRelationshipCursor.Move();


        }

        /// <summary>
        /// Builds the Textual Notation string for the rule ArgumentExpression
        /// <para>ArgumentExpression:Feature=ownedRelationship+=ArgumentExpressionValue</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IFeature" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildArgumentExpression(SysML2.NET.Core.POCO.Core.Features.IFeature poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);

            if (ownedRelationshipCursor.Current != null)
            {

                if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Kernel.FeatureValues.IFeatureValue elementAsFeatureValue)
                {
                    FeatureValueTextualNotationBuilder.BuildArgumentExpressionValue(elementAsFeatureValue, cursorCache, stringBuilder);
                }
            }
            ownedRelationshipCursor.Move();


        }

        /// <summary>
        /// Builds the Textual Notation string for the rule FeatureElement
        /// <para>FeatureElement:Feature=Feature|Step|Expression|BooleanExpression|Invariant|Connector|BindingConnector|Succession|Flow|SuccessionFlow</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IFeature" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildFeatureElement(SysML2.NET.Core.POCO.Core.Features.IFeature poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            switch (poco)
            {
                case SysML2.NET.Core.POCO.Kernel.Interactions.ISuccessionFlow pocoSuccessionFlow:
                    SuccessionFlowTextualNotationBuilder.BuildSuccessionFlow(pocoSuccessionFlow, cursorCache, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Functions.IInvariant pocoInvariant:
                    InvariantTextualNotationBuilder.BuildInvariant(pocoInvariant, cursorCache, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Interactions.IFlow pocoFlow:
                    FlowTextualNotationBuilder.BuildFlow(pocoFlow, cursorCache, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Functions.IBooleanExpression pocoBooleanExpression:
                    BooleanExpressionTextualNotationBuilder.BuildBooleanExpression(pocoBooleanExpression, cursorCache, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Connectors.IBindingConnector pocoBindingConnector:
                    BindingConnectorTextualNotationBuilder.BuildBindingConnector(pocoBindingConnector, cursorCache, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Connectors.ISuccession pocoSuccession:
                    SuccessionTextualNotationBuilder.BuildSuccession(pocoSuccession, cursorCache, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Functions.IExpression pocoExpression:
                    ExpressionTextualNotationBuilder.BuildExpression(pocoExpression, cursorCache, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Connectors.IConnector pocoConnector:
                    ConnectorTextualNotationBuilder.BuildConnector(pocoConnector, cursorCache, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Behaviors.IStep pocoStep:
                    StepTextualNotationBuilder.BuildStep(pocoStep, cursorCache, stringBuilder);
                    break;
                default:
                    BuildFeature(poco, cursorCache, stringBuilder);
                    break;
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule EndFeaturePrefix
        /// <para>EndFeaturePrefix:Feature=(isConstant?='const'{isVariable=true})?isEnd?='end'</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IFeature" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildEndFeaturePrefix(SysML2.NET.Core.POCO.Core.Features.IFeature poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {

            if (poco.IsConstant)
            {
                stringBuilder.Append(" const ");
                // NonParsing Assignment Element : isVariable = true => Does not have to be process
                stringBuilder.Append(' ');
            }

            if (poco.IsEnd)
            {
                stringBuilder.Append(" end ");
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule BasicFeaturePrefix
        /// <para>BasicFeaturePrefix:Feature=(direction=FeatureDirection)?(isDerived?='derived')?(isAbstract?='abstract')?(isComposite?='composite'|isPortion?='portion')?(isVariable?='var'|isConstant?='const'{isVariable=true})?</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IFeature" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildBasicFeaturePrefix(SysML2.NET.Core.POCO.Core.Features.IFeature poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {

            if (poco.Direction.HasValue)
            {
                stringBuilder.Append(poco.Direction.ToString().ToLower());
                stringBuilder.Append(' ');
                stringBuilder.Append(' ');
            }


            if (poco.IsDerived)
            {
                stringBuilder.Append(" derived ");
                stringBuilder.Append(' ');
            }


            if (poco.IsAbstract)
            {
                stringBuilder.Append(" abstract ");
                stringBuilder.Append(' ');
            }

            if (poco.IsComposite)
            {
                stringBuilder.Append(" composite ");
            }
            else if (poco.IsPortion)
            {
                stringBuilder.Append(" portion ");
            }

            BuildBasicFeaturePrefixHandCoded(poco, cursorCache, stringBuilder);

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule FeatureDeclaration
        /// <para>FeatureDeclaration:Feature=(isSufficient?='all')?(FeatureIdentification(FeatureSpecializationPart|ConjugationPart)?|FeatureSpecializationPart|ConjugationPart)FeatureRelationshipPart*</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IFeature" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildFeatureDeclaration(SysML2.NET.Core.POCO.Core.Features.IFeature poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {

            if (poco.IsSufficient)
            {
                stringBuilder.Append(" all ");
                stringBuilder.Append(' ');
            }

            BuildFeatureDeclarationHandCoded(poco, cursorCache, stringBuilder);
            stringBuilder.Append(' ');



        }

        /// <summary>
        /// Builds the Textual Notation string for the rule FeatureIdentification
        /// <para>FeatureIdentification:Feature='&lt;'declaredShortName=NAME'&gt;'(declaredName=NAME)?|declaredName=NAME</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IFeature" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildFeatureIdentification(SysML2.NET.Core.POCO.Core.Features.IFeature poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            BuildFeatureIdentificationHandCoded(poco, cursorCache, stringBuilder);
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule FeatureRelationshipPart
        /// <para>FeatureRelationshipPart:Feature=TypeRelationshipPart|ChainingPart|InvertingPart|TypeFeaturingPart</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IFeature" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildFeatureRelationshipPart(SysML2.NET.Core.POCO.Core.Features.IFeature poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            switch (poco)
            {
                case SysML2.NET.Core.POCO.Core.Features.IFeature pocoFeatureChainingPart when pocoFeatureChainingPart.IsValidForChainingPart():
                    BuildChainingPart(pocoFeatureChainingPart, cursorCache, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Core.Features.IFeature pocoFeatureInvertingPart when pocoFeatureInvertingPart.IsValidForInvertingPart():
                    BuildInvertingPart(pocoFeatureInvertingPart, cursorCache, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Core.Types.IType pocoType:
                    TypeTextualNotationBuilder.BuildTypeRelationshipPart(pocoType, cursorCache, stringBuilder);
                    break;
                default:
                    BuildTypeFeaturingPart(poco, cursorCache, stringBuilder);
                    break;
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule ChainingPart
        /// <para>ChainingPart:Feature='chains'(ownedRelationship+=OwnedFeatureChaining|FeatureChain)</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IFeature" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildChainingPart(SysML2.NET.Core.POCO.Core.Features.IFeature poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);
            stringBuilder.Append("chains ");
            if (ownedRelationshipCursor.Current != null)
            {

                if (ownedRelationshipCursor.Current != null)
                {

                    if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Features.IFeatureChaining elementAsFeatureChaining)
                    {
                        FeatureChainingTextualNotationBuilder.BuildOwnedFeatureChaining(elementAsFeatureChaining, cursorCache, stringBuilder);
                    }
                }
            }
            else
            {
                BuildFeatureChain(poco, cursorCache, stringBuilder);
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule InvertingPart
        /// <para>InvertingPart:Feature='inverse''of'ownedRelationship+=OwnedFeatureInverting</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IFeature" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildInvertingPart(SysML2.NET.Core.POCO.Core.Features.IFeature poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);
            stringBuilder.Append("inverse ");
            stringBuilder.Append("of ");

            if (ownedRelationshipCursor.Current != null)
            {

                if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Features.IFeatureInverting elementAsFeatureInverting)
                {
                    FeatureInvertingTextualNotationBuilder.BuildOwnedFeatureInverting(elementAsFeatureInverting, cursorCache, stringBuilder);
                }
            }
            ownedRelationshipCursor.Move();


        }

        /// <summary>
        /// Builds the Textual Notation string for the rule TypeFeaturingPart
        /// <para>TypeFeaturingPart:Feature='featured''by'ownedRelationship+=OwnedTypeFeaturing(','ownedTypeFeaturing+=OwnedTypeFeaturing)*</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IFeature" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildTypeFeaturingPart(SysML2.NET.Core.POCO.Core.Features.IFeature poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);
            var ownedTypeFeaturingCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedTypeFeaturing", poco.ownedTypeFeaturing);
            stringBuilder.Append("featured ");
            stringBuilder.Append("by ");

            if (ownedRelationshipCursor.Current != null)
            {

                if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Features.ITypeFeaturing elementAsTypeFeaturing)
                {
                    TypeFeaturingTextualNotationBuilder.BuildOwnedTypeFeaturing(elementAsTypeFeaturing, cursorCache, stringBuilder);
                }
            }
            ownedRelationshipCursor.Move();


            while (ownedTypeFeaturingCursor.Current != null && ownedTypeFeaturingCursor.Current is SysML2.NET.Core.POCO.Core.Features.ITypeFeaturing)
            {
                stringBuilder.Append(", ");

                if (ownedTypeFeaturingCursor.Current != null)
                {

                    if (ownedTypeFeaturingCursor.Current is SysML2.NET.Core.POCO.Core.Features.ITypeFeaturing elementAsTypeFeaturing)
                    {
                        TypeFeaturingTextualNotationBuilder.BuildOwnedTypeFeaturing(elementAsTypeFeaturing, cursorCache, stringBuilder);
                    }
                }
                ownedTypeFeaturingCursor.Move();

            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule FeatureChain
        /// <para>FeatureChain:Feature=ownedRelationship+=OwnedFeatureChaining('.'ownedRelationship+=OwnedFeatureChaining)+</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IFeature" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildFeatureChain(SysML2.NET.Core.POCO.Core.Features.IFeature poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);

            if (ownedRelationshipCursor.Current != null)
            {

                if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Features.IFeatureChaining elementAsFeatureChaining)
                {
                    FeatureChainingTextualNotationBuilder.BuildOwnedFeatureChaining(elementAsFeatureChaining, cursorCache, stringBuilder);
                }
            }
            ownedRelationshipCursor.Move();


            while (ownedRelationshipCursor.Current != null && ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Features.IFeatureChaining)
            {
                stringBuilder.Append(".");

                if (ownedRelationshipCursor.Current != null)
                {

                    if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Features.IFeatureChaining elementAsFeatureChaining)
                    {
                        FeatureChainingTextualNotationBuilder.BuildOwnedFeatureChaining(elementAsFeatureChaining, cursorCache, stringBuilder);
                    }
                }
                ownedRelationshipCursor.Move();

            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule MetadataArgument
        /// <para>MetadataArgument:Feature=ownedRelationship+=MetadataValue</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IFeature" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildMetadataArgument(SysML2.NET.Core.POCO.Core.Features.IFeature poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);

            if (ownedRelationshipCursor.Current != null)
            {

                if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Kernel.FeatureValues.IFeatureValue elementAsFeatureValue)
                {
                    FeatureValueTextualNotationBuilder.BuildMetadataValue(elementAsFeatureValue, cursorCache, stringBuilder);
                }
            }
            ownedRelationshipCursor.Move();


        }

        /// <summary>
        /// Builds the Textual Notation string for the rule TypeReference
        /// <para>TypeReference:Feature=ownedRelationship+=ReferenceTyping</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IFeature" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildTypeReference(SysML2.NET.Core.POCO.Core.Features.IFeature poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);

            if (ownedRelationshipCursor.Current != null)
            {

                if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Features.IFeatureTyping elementAsFeatureTyping)
                {
                    FeatureTypingTextualNotationBuilder.BuildReferenceTyping(elementAsFeatureTyping, cursorCache, stringBuilder);
                }
            }
            ownedRelationshipCursor.Move();


        }

        /// <summary>
        /// Builds the Textual Notation string for the rule PrimaryArgument
        /// <para>PrimaryArgument:Feature=ownedRelationship+=PrimaryArgumentValue</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IFeature" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildPrimaryArgument(SysML2.NET.Core.POCO.Core.Features.IFeature poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);

            if (ownedRelationshipCursor.Current != null)
            {

                if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Kernel.FeatureValues.IFeatureValue elementAsFeatureValue)
                {
                    FeatureValueTextualNotationBuilder.BuildPrimaryArgumentValue(elementAsFeatureValue, cursorCache, stringBuilder);
                }
            }
            ownedRelationshipCursor.Move();


        }

        /// <summary>
        /// Builds the Textual Notation string for the rule NonFeatureChainPrimaryArgument
        /// <para>NonFeatureChainPrimaryArgument:Feature=ownedRelationship+=NonFeatureChainPrimaryArgumentValue</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IFeature" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildNonFeatureChainPrimaryArgument(SysML2.NET.Core.POCO.Core.Features.IFeature poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);

            if (ownedRelationshipCursor.Current != null)
            {

                if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Kernel.FeatureValues.IFeatureValue elementAsFeatureValue)
                {
                    FeatureValueTextualNotationBuilder.BuildNonFeatureChainPrimaryArgumentValue(elementAsFeatureValue, cursorCache, stringBuilder);
                }
            }
            ownedRelationshipCursor.Move();


        }

        /// <summary>
        /// Builds the Textual Notation string for the rule BodyArgument
        /// <para>BodyArgument:Feature=ownedRelationship+=BodyArgumentValue</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IFeature" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildBodyArgument(SysML2.NET.Core.POCO.Core.Features.IFeature poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);

            if (ownedRelationshipCursor.Current != null)
            {

                if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Kernel.FeatureValues.IFeatureValue elementAsFeatureValue)
                {
                    FeatureValueTextualNotationBuilder.BuildBodyArgumentValue(elementAsFeatureValue, cursorCache, stringBuilder);
                }
            }
            ownedRelationshipCursor.Move();


        }

        /// <summary>
        /// Builds the Textual Notation string for the rule FunctionReferenceArgument
        /// <para>FunctionReferenceArgument:Feature=ownedRelationship+=FunctionReferenceArgumentValue</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IFeature" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildFunctionReferenceArgument(SysML2.NET.Core.POCO.Core.Features.IFeature poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);

            if (ownedRelationshipCursor.Current != null)
            {

                if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Kernel.FeatureValues.IFeatureValue elementAsFeatureValue)
                {
                    FeatureValueTextualNotationBuilder.BuildFunctionReferenceArgumentValue(elementAsFeatureValue, cursorCache, stringBuilder);
                }
            }
            ownedRelationshipCursor.Move();


        }

        /// <summary>
        /// Builds the Textual Notation string for the rule FeatureReference
        /// <para>FeatureReference:Feature=[QualifiedName]</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IFeature" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildFeatureReference(SysML2.NET.Core.POCO.Core.Features.IFeature poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            stringBuilder.Append(poco.qualifiedName);
            stringBuilder.Append(' ');

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule ConstructorResult
        /// <para>ConstructorResult:Feature=ArgumentList</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IFeature" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildConstructorResult(SysML2.NET.Core.POCO.Core.Features.IFeature poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            BuildArgumentList(poco, cursorCache, stringBuilder);

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule ArgumentList
        /// <para>ArgumentList:Feature='('(PositionalArgumentList|NamedArgumentList)?')'</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IFeature" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildArgumentList(SysML2.NET.Core.POCO.Core.Features.IFeature poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            stringBuilder.Append("(");
            switch (poco)
            {
                case SysML2.NET.Core.POCO.Core.Features.IFeature pocoFeaturePositionalArgumentList when pocoFeaturePositionalArgumentList.IsValidForPositionalArgumentList():
                    BuildPositionalArgumentList(pocoFeaturePositionalArgumentList, cursorCache, stringBuilder);
                    break;
                default:
                    BuildNamedArgumentList(poco, cursorCache, stringBuilder);
                    break;
            }

            stringBuilder.Append(") ");

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule PositionalArgumentList
        /// <para>PositionalArgumentList:Feature=e.ownedRelationship+=ArgumentMember(','e.ownedRelationship+=ArgumentMember)*</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IFeature" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildPositionalArgumentList(SysML2.NET.Core.POCO.Core.Features.IFeature poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);

            if (ownedRelationshipCursor.Current != null)
            {

                if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Kernel.Behaviors.IParameterMembership elementAsParameterMembership)
                {
                    ParameterMembershipTextualNotationBuilder.BuildArgumentMember(elementAsParameterMembership, cursorCache, stringBuilder);
                }
            }
            ownedRelationshipCursor.Move();


            while (ownedRelationshipCursor.Current != null && ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Kernel.Behaviors.IParameterMembership)
            {
                stringBuilder.Append(", ");

                if (ownedRelationshipCursor.Current != null)
                {

                    if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Kernel.Behaviors.IParameterMembership elementAsParameterMembership)
                    {
                        ParameterMembershipTextualNotationBuilder.BuildArgumentMember(elementAsParameterMembership, cursorCache, stringBuilder);
                    }
                }
                ownedRelationshipCursor.Move();

            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule NamedArgumentList
        /// <para>NamedArgumentList:Feature=ownedRelationship+=NamedArgumentMember(','ownedRelationship+=NamedArgumentMember)*</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IFeature" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildNamedArgumentList(SysML2.NET.Core.POCO.Core.Features.IFeature poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);

            if (ownedRelationshipCursor.Current != null)
            {

                if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Types.IFeatureMembership elementAsFeatureMembership)
                {
                    FeatureMembershipTextualNotationBuilder.BuildNamedArgumentMember(elementAsFeatureMembership, cursorCache, stringBuilder);
                }
            }
            ownedRelationshipCursor.Move();


            while (ownedRelationshipCursor.Current != null && ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Types.IFeatureMembership)
            {
                stringBuilder.Append(", ");

                if (ownedRelationshipCursor.Current != null)
                {

                    if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Types.IFeatureMembership elementAsFeatureMembership)
                    {
                        FeatureMembershipTextualNotationBuilder.BuildNamedArgumentMember(elementAsFeatureMembership, cursorCache, stringBuilder);
                    }
                }
                ownedRelationshipCursor.Move();

            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule NamedArgument
        /// <para>NamedArgument:Feature=ownedRelationship+=ParameterRedefinition'='ownedRelationship+=ArgumentValue</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IFeature" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildNamedArgument(SysML2.NET.Core.POCO.Core.Features.IFeature poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);

            if (ownedRelationshipCursor.Current != null)
            {

                if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Features.IRedefinition elementAsRedefinition)
                {
                    RedefinitionTextualNotationBuilder.BuildParameterRedefinition(elementAsRedefinition, cursorCache, stringBuilder);
                }
            }
            ownedRelationshipCursor.Move();

            stringBuilder.Append("= ");

            if (ownedRelationshipCursor.Current != null)
            {

                if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Kernel.FeatureValues.IFeatureValue elementAsFeatureValue)
                {
                    FeatureValueTextualNotationBuilder.BuildArgumentValue(elementAsFeatureValue, cursorCache, stringBuilder);
                }
            }
            ownedRelationshipCursor.Move();


        }

        /// <summary>
        /// Builds the Textual Notation string for the rule MetadataBodyFeature
        /// <para>MetadataBodyFeature:Feature='feature'?(':&gt;&gt;'|'redefines')?ownedRelationship+=OwnedRedefinitionFeatureSpecializationPart?ValuePart?MetadataBody</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IFeature" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildMetadataBodyFeature(SysML2.NET.Core.POCO.Core.Features.IFeature poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);
            stringBuilder.Append("feature ");
            stringBuilder.Append(" :>> ");

            if (ownedRelationshipCursor.Current != null)
            {

                if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Features.IRedefinition elementAsRedefinition)
                {
                    RedefinitionTextualNotationBuilder.BuildOwnedRedefinition(elementAsRedefinition, cursorCache, stringBuilder);
                }
            }
            ownedRelationshipCursor.Move();


            if (poco.OwnedRelationship.Count != 0 || poco.type.Count != 0 || poco.chainingFeature.Count != 0 || poco.IsOrdered)
            {
                BuildFeatureSpecializationPart(poco, cursorCache, stringBuilder);
            }

            if (poco.OwnedRelationship.Count != 0 || poco.type.Count != 0 || poco.chainingFeature.Count != 0 || !string.IsNullOrWhiteSpace(poco.DeclaredShortName) || !string.IsNullOrWhiteSpace(poco.DeclaredName) || poco.Direction.HasValue || poco.IsDerived || poco.IsAbstract || poco.IsConstant || poco.IsOrdered || poco.IsEnd || poco.importedMembership.Count != 0 || poco.IsComposite || poco.IsPortion || poco.IsVariable || poco.IsSufficient || poco.unioningType.Count != 0 || poco.intersectingType.Count != 0 || poco.differencingType.Count != 0 || poco.featuringType.Count != 0 || poco.ownedTypeFeaturing.Count != 0)
            {
                BuildValuePart(poco, cursorCache, stringBuilder);
            }
            TypeTextualNotationBuilder.BuildMetadataBody(poco, cursorCache, stringBuilder);

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule Feature
        /// <para>Feature=(FeaturePrefix('feature'|ownedRelationship+=PrefixMetadataMember)FeatureDeclaration?|(EndFeaturePrefix|BasicFeaturePrefix)FeatureDeclaration)ValuePart?TypeBody</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IFeature" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildFeature(SysML2.NET.Core.POCO.Core.Features.IFeature poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);
            BuildFeatureHandCoded(poco, cursorCache, stringBuilder);
            stringBuilder.Append(' ');

            if (poco.OwnedRelationship.Count != 0 || poco.type.Count != 0 || poco.chainingFeature.Count != 0 || !string.IsNullOrWhiteSpace(poco.DeclaredShortName) || !string.IsNullOrWhiteSpace(poco.DeclaredName) || poco.Direction.HasValue || poco.IsDerived || poco.IsAbstract || poco.IsConstant || poco.IsOrdered || poco.IsEnd || poco.importedMembership.Count != 0 || poco.IsComposite || poco.IsPortion || poco.IsVariable || poco.IsSufficient || poco.unioningType.Count != 0 || poco.intersectingType.Count != 0 || poco.differencingType.Count != 0 || poco.featuringType.Count != 0 || poco.ownedTypeFeaturing.Count != 0)
            {
                BuildValuePart(poco, cursorCache, stringBuilder);
            }
            TypeTextualNotationBuilder.BuildTypeBody(poco, cursorCache, stringBuilder);

        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
