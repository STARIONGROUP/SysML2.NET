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

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.TextualNotation
{
    using System.Linq;
    using System.Text;

    using SysML2.NET.Core.POCO.Root.Elements;

    /// <summary>
    /// The <see cref="ClassifierTextualNotationBuilder" /> provides Textual Notation Builder for the <see cref="SysML2.NET.Core.POCO.Core.Classifiers.IClassifier" /> element
    /// </summary>
    public static partial class ClassifierTextualNotationBuilder
    {
        /// <summary>
        /// Builds the Textual Notation string for the rule SubclassificationPart
        /// <para>SubclassificationPart:Classifier=SPECIALIZESownedRelationship+=OwnedSubclassification(','ownedRelationship+=OwnedSubclassification)*</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Classifiers.IClassifier" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildSubclassificationPart(SysML2.NET.Core.POCO.Core.Classifiers.IClassifier poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);
            stringBuilder.Append(" :> ");

            if (ownedRelationshipCursor.Current != null)
            {

                if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Classifiers.ISubclassification elementAsSubclassification)
                {
                    SubclassificationTextualNotationBuilder.BuildOwnedSubclassification(elementAsSubclassification, cursorCache, stringBuilder);
                }
            }
            ownedRelationshipCursor.Move();


            while (ownedRelationshipCursor.Current != null)
            {
                stringBuilder.Append(", ");

                if (ownedRelationshipCursor.Current != null)
                {

                    if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Classifiers.ISubclassification elementAsSubclassification)
                    {
                        SubclassificationTextualNotationBuilder.BuildOwnedSubclassification(elementAsSubclassification, cursorCache, stringBuilder);
                    }
                }
                ownedRelationshipCursor.Move();

            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule ClassifierDeclaration
        /// <para>ClassifierDeclaration:Classifier=(isSufficient?='all')?Identification(ownedRelationship+=OwnedMultiplicity)?(SuperclassingPart|ConjugationPart)?TypeRelationshipPart*</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Classifiers.IClassifier" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildClassifierDeclaration(SysML2.NET.Core.POCO.Core.Classifiers.IClassifier poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);

            if (poco.IsSufficient)
            {
                stringBuilder.Append(" all ");
                stringBuilder.Append(' ');
            }

            ElementTextualNotationBuilder.BuildIdentification(poco, cursorCache, stringBuilder);

            if (ownedRelationshipCursor.Current != null)
            {

                if (ownedRelationshipCursor.Current != null)
                {

                    if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Root.Namespaces.IOwningMembership elementAsOwningMembership)
                    {
                        OwningMembershipTextualNotationBuilder.BuildOwnedMultiplicity(elementAsOwningMembership, cursorCache, stringBuilder);
                    }
                }
                stringBuilder.Append(' ');
            }

            switch (poco)
            {
                case SysML2.NET.Core.POCO.Core.Types.IType pocoType:
                    TypeTextualNotationBuilder.BuildConjugationPart(pocoType, cursorCache, stringBuilder);
                    break;
                default:
                    BuildSuperclassingPart(poco, cursorCache, stringBuilder);
                    break;
            }

            while (ownedRelationshipCursor.Current != null)
            {
                TypeTextualNotationBuilder.BuildTypeRelationshipPart(poco, cursorCache, stringBuilder);
                ownedRelationshipCursor.Move();
            }


        }

        /// <summary>
        /// Builds the Textual Notation string for the rule SuperclassingPart
        /// <para>SuperclassingPart:Classifier=SPECIALIZESownedRelationship+=OwnedSubclassification(','ownedRelationship+=OwnedSubclassification)*</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Classifiers.IClassifier" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildSuperclassingPart(SysML2.NET.Core.POCO.Core.Classifiers.IClassifier poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);
            stringBuilder.Append(" :> ");

            if (ownedRelationshipCursor.Current != null)
            {

                if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Classifiers.ISubclassification elementAsSubclassification)
                {
                    SubclassificationTextualNotationBuilder.BuildOwnedSubclassification(elementAsSubclassification, cursorCache, stringBuilder);
                }
            }
            ownedRelationshipCursor.Move();


            while (ownedRelationshipCursor.Current != null)
            {
                stringBuilder.Append(", ");

                if (ownedRelationshipCursor.Current != null)
                {

                    if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Core.Classifiers.ISubclassification elementAsSubclassification)
                    {
                        SubclassificationTextualNotationBuilder.BuildOwnedSubclassification(elementAsSubclassification, cursorCache, stringBuilder);
                    }
                }
                ownedRelationshipCursor.Move();

            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule Classifier
        /// <para>Classifier=TypePrefix'classifier'ClassifierDeclarationTypeBody</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Classifiers.IClassifier" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildClassifier(SysML2.NET.Core.POCO.Core.Classifiers.IClassifier poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            TypeTextualNotationBuilder.BuildTypePrefix(poco, cursorCache, stringBuilder);
            stringBuilder.Append("classifier ");
            BuildClassifierDeclaration(poco, cursorCache, stringBuilder);
            TypeTextualNotationBuilder.BuildTypeBody(poco, cursorCache, stringBuilder);

        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
