// -------------------------------------------------------------------------------------------------
// <copyright file="ReferenceUsageTextualNotationBuilder.cs" company="Starion Group S.A.">
// 
//    Copyright (C) 2022-2026 Starion Group S.A.
// 
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
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
    using System.Linq;

    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Kernel.FeatureValues;
    using SysML2.NET.Core.POCO.Systems.Actions;
    using SysML2.NET.Core.POCO.Systems.DefinitionAndUsage;

    /// <summary>
    /// Hand-coded part of the <see cref="ReferenceUsageTextualNotationBuilder" />
    /// </summary>
    public static partial class ReferenceUsageTextualNotationBuilder
    {
        /// <summary>
        /// Builds the Textual Notation string for the rule PayloadParameter
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IReferenceUsage" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="IndentedStringBuilder" /> that contains the entire textual notation</param>
        /// <remarks>
        /// PayloadParameter : ReferenceUsage =
        ///     PayloadFeature
        ///   | Identification PayloadFeatureSpecializationPart? TriggerValuePart
        ///
        /// Alt 2 applies when the reference usage carries a TriggerValuePart. `Identification` matches EMPTY
        /// (( '&lt;' NAME '&gt;' )? ( NAME )?) so it is structurally always present and cannot discriminate.
        /// Otherwise, delegate to PayloadFeature (Alt 1).
        /// </remarks>
        private static void BuildPayloadParameterHandCoded(IReferenceUsage poco, TextualNotationWriterContext writerContext, IndentedStringBuilder stringBuilder)
        {
            // TriggerFeatureValue and ValuePart's plain FeatureValue share the SAME target metaclass, so the
            // trigger must be identified by its nested TriggerInvocationExpression. Testing for any
            // IFeatureValue also matches a plain payload default (`accept x: Foo = default`) and routes it
            // into BuildTriggerValuePart, which emits nothing for a non-trigger expression — silent data loss.
            var hasTriggerValue = poco.OwnedRelationship
                .OfType<IFeatureValue>()
                .Any(featureValue => featureValue.OwnedRelatedElement.OfType<ITriggerInvocationExpression>().Any());
            
            if (hasTriggerValue)
            {
                // Alt 2: Identification PayloadFeatureSpecializationPart? TriggerValuePart
                ElementTextualNotationBuilder.BuildIdentification(poco, writerContext, stringBuilder);

                var ownedRelationshipCursor = writerContext.CursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);

                if (ownedRelationshipCursor.Current is ISpecialization)
                {
                    FeatureTextualNotationBuilder.BuildPayloadFeatureSpecializationPart(poco, writerContext, stringBuilder);
                }

                FeatureTextualNotationBuilder.BuildTriggerValuePart(poco, writerContext, stringBuilder);
            }
            else
            {
                // Alt 1: PayloadFeature
                FeatureTextualNotationBuilder.BuildPayloadFeature(poco, writerContext, stringBuilder);
            }
        }
    }
}
