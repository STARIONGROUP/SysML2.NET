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
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildInvariant(SysML2.NET.Core.POCO.Kernel.Functions.IInvariant poco, StringBuilder stringBuilder)
        {
            // non Terminal : FeaturePrefix; Found rule FeaturePrefix=(EndFeaturePrefix(ownedRelationship+=OwnedCrossFeatureMember)?|BasicFeaturePrefix)(ownedRelationship+=PrefixMetadataMember)* 
            // Group Element
            throw new System.NotSupportedException("Multiple alternatives not implemented yet");
            // Group Element
            // Assignment Element : ownedRelationship += SysML2.NET.CodeGenerator.Grammar.Model.NonTerminalElement
            // If property ownedRelationship value is set, print SysML2.NET.CodeGenerator.Grammar.Model.NonTerminalElement


            stringBuilder.Append("inv ");
            // Group Element
            throw new System.NotSupportedException("Multiple alternatives not implemented yet");
            // non Terminal : FeatureDeclaration; Found rule FeatureDeclaration:Feature=(isSufficient?='all')?(FeatureIdentification(FeatureSpecializationPart|ConjugationPart)?|FeatureSpecializationPart|ConjugationPart)FeatureRelationshipPart* 
            FeatureTextualNotationBuilder.BuildFeatureDeclaration(poco, stringBuilder);
            // non Terminal : ValuePart; Found rule ValuePart:Feature=ownedRelationship+=FeatureValue 
            FeatureTextualNotationBuilder.BuildValuePart(poco, stringBuilder);
            // non Terminal : FunctionBody; Found rule FunctionBody:Type=';'|'{'FunctionBodyPart'}' 
            TypeTextualNotationBuilder.BuildFunctionBody(poco, stringBuilder);

        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
