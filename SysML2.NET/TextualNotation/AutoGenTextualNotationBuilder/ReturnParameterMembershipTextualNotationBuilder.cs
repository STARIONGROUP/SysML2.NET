// -------------------------------------------------------------------------------------------------
// <copyright file="ReturnParameterMembershipTextualNotationBuilder.cs" company="Starion Group S.A.">
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
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using SysML2.NET.Core.POCO.Root.Elements;

    /// <summary>
    /// The <see cref="ReturnParameterMembershipTextualNotationBuilder" /> provides Textual Notation Builder for the <see cref="SysML2.NET.Core.POCO.Kernel.Functions.IReturnParameterMembership" /> element
    /// </summary>
    public static partial class ReturnParameterMembershipTextualNotationBuilder
    {
        /// <summary>
        /// Builds the Textual Notation string for the rule ReturnParameterMember
        /// <para>ReturnParameterMember:ReturnParameterMembership=MemberPrefix?'return'ownedRelatedElement+=UsageElement</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Kernel.Functions.IReturnParameterMembership" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildReturnParameterMember(SysML2.NET.Core.POCO.Kernel.Functions.IReturnParameterMembership poco, StringBuilder stringBuilder)
        {
            using var ownedRelatedElementOfUsageIterator = poco.OwnedRelatedElement.OfType<SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.Usage>().GetEnumerator();
            MembershipTextualNotationBuilder.BuildMemberPrefix(poco, stringBuilder);
            stringBuilder.Append("return ");
            ownedRelatedElementOfUsageIterator.MoveNext();

            if (ownedRelatedElementOfUsageIterator.Current != null)
            {
                UsageTextualNotationBuilder.BuildUsageElement(ownedRelatedElementOfUsageIterator.Current, stringBuilder);
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule ReturnFeatureMember
        /// <para>ReturnFeatureMember:ReturnParameterMembership=MemberPrefix'return'ownedRelatedElement+=FeatureElement</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Kernel.Functions.IReturnParameterMembership" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildReturnFeatureMember(SysML2.NET.Core.POCO.Kernel.Functions.IReturnParameterMembership poco, StringBuilder stringBuilder)
        {
            using var ownedRelatedElementOfFeatureIterator = poco.OwnedRelatedElement.OfType<SysML2.NET.Core.POCO.Core.Features.Feature>().GetEnumerator();
            MembershipTextualNotationBuilder.BuildMemberPrefix(poco, stringBuilder);
            stringBuilder.Append("return ");
            ownedRelatedElementOfFeatureIterator.MoveNext();

            if (ownedRelatedElementOfFeatureIterator.Current != null)
            {
                FeatureTextualNotationBuilder.BuildFeatureElement(ownedRelatedElementOfFeatureIterator.Current, stringBuilder);
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule EmptyResultMember
        /// <para>EmptyResultMember:ReturnParameterMembership=ownedRelatedElement+=EmptyFeature</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Kernel.Functions.IReturnParameterMembership" /> from which the rule should be build</param>
        /// <param name="elementIndex">The index of the <see cref="IElement" /> to process inside the <paramref name="elements" /> collection</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        /// <returns>The index of the next <see cref="IElement" /> to be processed inside the collection</returns>
        public static int BuildEmptyResultMember(SysML2.NET.Core.POCO.Kernel.Functions.IReturnParameterMembership poco, int elementIndex, StringBuilder stringBuilder)
        {
            if (elementIndex < poco.OwnedRelatedElement.Count)
            {
                var elementForOwnedRelatedElement = poco.OwnedRelatedElement[elementIndex];

                if (elementForOwnedRelatedElement is SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IReferenceUsage elementAsReferenceUsage)
                {
                    ReferenceUsageTextualNotationBuilder.BuildEmptyFeature(elementAsReferenceUsage, stringBuilder);
                }
            }

            return elementIndex;
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule ConstructorResultMember
        /// <para>ConstructorResultMember:ReturnParameterMembership=ownedRelatedElement+=ConstructorResult</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Kernel.Functions.IReturnParameterMembership" /> from which the rule should be build</param>
        /// <param name="elementIndex">The index of the <see cref="IElement" /> to process inside the <paramref name="elements" /> collection</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        /// <returns>The index of the next <see cref="IElement" /> to be processed inside the collection</returns>
        public static int BuildConstructorResultMember(SysML2.NET.Core.POCO.Kernel.Functions.IReturnParameterMembership poco, int elementIndex, StringBuilder stringBuilder)
        {
            if (elementIndex < poco.OwnedRelatedElement.Count)
            {
                var elementForOwnedRelatedElement = poco.OwnedRelatedElement[elementIndex];

                if (elementForOwnedRelatedElement is SysML2.NET.Core.POCO.Core.Features.IFeature elementAsFeature)
                {
                    FeatureTextualNotationBuilder.BuildConstructorResult(elementAsFeature, stringBuilder);
                }
            }

            return elementIndex;
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
