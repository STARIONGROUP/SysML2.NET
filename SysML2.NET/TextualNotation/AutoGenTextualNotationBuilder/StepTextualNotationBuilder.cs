// -------------------------------------------------------------------------------------------------
// <copyright file="StepTextualNotationBuilder.cs" company="Starion Group S.A.">
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
    /// The <see cref="StepTextualNotationBuilder" /> provides Textual Notation Builder for the <see cref="SysML2.NET.Core.POCO.Kernel.Behaviors.Step" /> element
    /// </summary>
    public class StepTextualNotationBuilder : TextualNotationBuilder<SysML2.NET.Core.POCO.Kernel.Behaviors.Step>
    {
        /// <summary>
        /// Initializes a new instance of a <see cref="StepTextualNotationBuilder"/>
        /// </summary>
        /// <param name="facade">The <see cref="ITextualNotationBuilderFacade"/> used to query textual notation of referenced <see cref="IElement"/></param>
        public StepTextualNotationBuilder(ITextualNotationBuilderFacade facade) : base(facade)
        {
        }

        /// <summary>
        /// Builds the Textual Notation string for the provided <see cref="SysML2.NET.Core.POCO.Kernel.Behaviors.Step"/>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Kernel.Behaviors.Step"/> from which the textual notation should be build</param>
        /// <returns>The built textual notation string</returns>
        public override string BuildTextualNotation(SysML2.NET.Core.POCO.Kernel.Behaviors.Step poco)
        {
            var stringBuilder = new StringBuilder();
            // Rule definition : Step=FeaturePrefix'step'FeatureDeclarationValuePart?TypeBody





            // non Terminal : FeaturePrefix; Found rule FeaturePrefix=(EndFeaturePrefix(ownedRelationship+=OwnedCrossFeatureMember)?|BasicFeaturePrefix)(ownedRelationship+=PrefixMetadataMember)*




            stringBuilder.Append("step ");
            // non Terminal : FeatureDeclaration; Found rule FeatureDeclaration:Feature=(isSufficient?='all')?(FeatureIdentification(FeatureSpecializationPart|ConjugationPart)?|FeatureSpecializationPart|ConjugationPart)FeatureRelationshipPart*


            // non Terminal : ValuePart; Found rule ValuePart:Feature=ownedRelationship+=FeatureValue


            // non Terminal : TypeBody; Found rule TypeBody:Type=';'|'{'TypeBodyElement*'}'



            return stringBuilder.ToString();
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
