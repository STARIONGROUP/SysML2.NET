// -------------------------------------------------------------------------------------------------
// <copyright file="ElementTextualNotationBuilder.cs" company="Starion Group S.A.">
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
    /// The <see cref="ElementTextualNotationBuilder" /> provides Textual Notation Builder for the <see cref="SysML2.NET.Core.POCO.Root.Elements.IElement" /> element
    /// </summary>
    public static partial class ElementTextualNotationBuilder
    {
        /// <summary>
        /// Builds the Textual Notation string for the rule Identification
        /// <para>Identification:Element=('&lt;'declaredShortName=NAME'&gt;')?(declaredName=NAME)?</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Root.Elements.IElement" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildIdentification(SysML2.NET.Core.POCO.Root.Elements.IElement poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {

            if (!string.IsNullOrWhiteSpace(poco.DeclaredShortName))
            {
                stringBuilder.Append("<");
                stringBuilder.Append(poco.DeclaredShortName);
                stringBuilder.Append(">");
                stringBuilder.Append(' ');
            }


            if (!string.IsNullOrWhiteSpace(poco.DeclaredName))
            {
                stringBuilder.Append(poco.DeclaredName);
            }


        }

        /// <summary>
        /// Builds the Textual Notation string for the rule DefinitionElement
        /// <para>DefinitionElement:Element=Package|LibraryPackage|AnnotatingElement|Dependency|AttributeDefinition|EnumerationDefinition|OccurrenceDefinition|IndividualDefinition|ItemDefinition|PartDefinition|ConnectionDefinition|FlowDefinition|InterfaceDefinition|PortDefinition|ActionDefinition|CalculationDefinition|StateDefinition|ConstraintDefinition|RequirementDefinition|ConcernDefinition|CaseDefinition|AnalysisCaseDefinition|VerificationCaseDefinition|UseCaseDefinition|ViewDefinition|ViewpointDefinition|RenderingDefinition|MetadataDefinition|ExtendedDefinition</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Root.Elements.IElement" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildDefinitionElement(SysML2.NET.Core.POCO.Root.Elements.IElement poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            BuildDefinitionElementHandCoded(poco, cursorCache, stringBuilder);
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule OwnedRelatedElement
        /// <para>OwnedRelatedElement:Element=NonFeatureElement|FeatureElement</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Root.Elements.IElement" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildOwnedRelatedElement(SysML2.NET.Core.POCO.Root.Elements.IElement poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            switch (poco)
            {
                case SysML2.NET.Core.POCO.Core.Features.IFeature pocoFeature:
                    FeatureTextualNotationBuilder.BuildFeatureElement(pocoFeature, cursorCache, stringBuilder);
                    break;
                default:
                    BuildNonFeatureElement(poco, cursorCache, stringBuilder);
                    break;
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule MemberElement
        /// <para>MemberElement:Element=AnnotatingElement|NonFeatureElement</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Root.Elements.IElement" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildMemberElement(SysML2.NET.Core.POCO.Root.Elements.IElement poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            switch (poco)
            {
                case SysML2.NET.Core.POCO.Root.Annotations.IAnnotatingElement pocoAnnotatingElement:
                    AnnotatingElementTextualNotationBuilder.BuildAnnotatingElement(pocoAnnotatingElement, cursorCache, stringBuilder);
                    break;
                default:
                    BuildNonFeatureElement(poco, cursorCache, stringBuilder);
                    break;
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule NonFeatureElement
        /// <para>NonFeatureElement:Element=Dependency|Namespace|Type|Classifier|DataType|Class|Structure|Metaclass|Association|AssociationStructure|Interaction|Behavior|Function|Predicate|Multiplicity|Package|LibraryPackage|Specialization|Conjugation|Subclassification|Disjoining|FeatureInverting|FeatureTyping|Subsetting|Redefinition|TypeFeaturing</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Root.Elements.IElement" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildNonFeatureElement(SysML2.NET.Core.POCO.Root.Elements.IElement poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            switch (poco)
            {
                case SysML2.NET.Core.POCO.Kernel.Associations.IAssociationStructure pocoAssociationStructure:
                    AssociationStructureTextualNotationBuilder.BuildAssociationStructure(pocoAssociationStructure, cursorCache, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Interactions.IInteraction pocoInteraction:
                    InteractionTextualNotationBuilder.BuildInteraction(pocoInteraction, cursorCache, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Functions.IPredicate pocoPredicate:
                    PredicateTextualNotationBuilder.BuildPredicate(pocoPredicate, cursorCache, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Metadata.IMetaclass pocoMetaclass:
                    MetaclassTextualNotationBuilder.BuildMetaclass(pocoMetaclass, cursorCache, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Functions.IFunction pocoFunction:
                    FunctionTextualNotationBuilder.BuildFunction(pocoFunction, cursorCache, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Structures.IStructure pocoStructure:
                    StructureTextualNotationBuilder.BuildStructure(pocoStructure, cursorCache, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Associations.IAssociation pocoAssociation:
                    AssociationTextualNotationBuilder.BuildAssociation(pocoAssociation, cursorCache, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Behaviors.IBehavior pocoBehavior:
                    BehaviorTextualNotationBuilder.BuildBehavior(pocoBehavior, cursorCache, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Kernel.DataTypes.IDataType pocoDataType:
                    DataTypeTextualNotationBuilder.BuildDataType(pocoDataType, cursorCache, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Classes.IClass pocoClass:
                    ClassTextualNotationBuilder.BuildClass(pocoClass, cursorCache, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Core.Types.IMultiplicity pocoMultiplicity:
                    MultiplicityTextualNotationBuilder.BuildMultiplicity(pocoMultiplicity, cursorCache, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Core.Features.IRedefinition pocoRedefinition:
                    RedefinitionTextualNotationBuilder.BuildRedefinition(pocoRedefinition, cursorCache, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Core.Classifiers.IClassifier pocoClassifier:
                    ClassifierTextualNotationBuilder.BuildClassifier(pocoClassifier, cursorCache, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Packages.ILibraryPackage pocoLibraryPackage:
                    LibraryPackageTextualNotationBuilder.BuildLibraryPackage(pocoLibraryPackage, cursorCache, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Core.Classifiers.ISubclassification pocoSubclassification:
                    SubclassificationTextualNotationBuilder.BuildSubclassification(pocoSubclassification, cursorCache, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Core.Features.IFeatureTyping pocoFeatureTyping:
                    FeatureTypingTextualNotationBuilder.BuildFeatureTyping(pocoFeatureTyping, cursorCache, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Core.Features.ISubsetting pocoSubsetting:
                    SubsettingTextualNotationBuilder.BuildSubsetting(pocoSubsetting, cursorCache, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Root.Dependencies.IDependency pocoDependency:
                    DependencyTextualNotationBuilder.BuildDependency(pocoDependency, cursorCache, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Core.Types.IType pocoType:
                    TypeTextualNotationBuilder.BuildType(pocoType, cursorCache, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Kernel.Packages.IPackage pocoPackage:
                    PackageTextualNotationBuilder.BuildPackage(pocoPackage, cursorCache, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Core.Types.ISpecialization pocoSpecialization:
                    SpecializationTextualNotationBuilder.BuildSpecialization(pocoSpecialization, cursorCache, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Core.Types.IConjugation pocoConjugation:
                    ConjugationTextualNotationBuilder.BuildConjugation(pocoConjugation, cursorCache, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Core.Types.IDisjoining pocoDisjoining:
                    DisjoiningTextualNotationBuilder.BuildDisjoining(pocoDisjoining, cursorCache, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Core.Features.IFeatureInverting pocoFeatureInverting:
                    FeatureInvertingTextualNotationBuilder.BuildFeatureInverting(pocoFeatureInverting, cursorCache, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Core.Features.ITypeFeaturing pocoTypeFeaturing:
                    TypeFeaturingTextualNotationBuilder.BuildTypeFeaturing(pocoTypeFeaturing, cursorCache, stringBuilder);
                    break;
                case SysML2.NET.Core.POCO.Root.Namespaces.INamespace pocoNamespace:
                    NamespaceTextualNotationBuilder.BuildNamespace(pocoNamespace, cursorCache, stringBuilder);
                    break;
            }

        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
