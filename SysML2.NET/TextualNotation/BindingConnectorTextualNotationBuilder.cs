// -------------------------------------------------------------------------------------------------
// <copyright file="BindingConnectorTextualNotationBuilder.cs" company="Starion Group S.A.">
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
    using System.Text;

    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Kernel.Connectors;

    /// <summary>
    /// Hand-coded part of the <see cref="BindingConnectorTextualNotationBuilder" />
    /// </summary>
    public static partial class BindingConnectorTextualNotationBuilder
    {
        /// <summary>
        /// Builds the Textual Notation string for the rule BindingConnectorDeclaration
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Kernel.Connectors.IBindingConnector" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        /// <remarks>
        /// BindingConnectorDeclaration : BindingConnector =
        ///     FeatureDeclaration ( 'of' ownedRelationship += ConnectorEndMember '=' ownedRelationship += ConnectorEndMember )?
        ///   | ( isSufficient ?= 'all' )? ( 'of'? ownedRelationship += ConnectorEndMember '=' ownedRelationship += ConnectorEndMember )?
        ///
        /// Auto-gen delegates entirely to this method.
        /// </remarks>
        private static void BuildBindingConnectorDeclarationHandCoded(IBindingConnector poco, TextualNotationWriterContext writerContext, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = writerContext.CursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);

            var hasDeclaration = !string.IsNullOrWhiteSpace(poco.DeclaredShortName)
                                 || !string.IsNullOrWhiteSpace(poco.DeclaredName)
                                 || ownedRelationshipCursor.Current is ISpecialization
                                 || ownedRelationshipCursor.Current is IConjugation;

            if (hasDeclaration)
            {
                // Alt 1: FeatureDeclaration ('of' ConnectorEndMember '=' ConnectorEndMember)?
                FeatureTextualNotationBuilder.BuildFeatureDeclaration(poco, writerContext, stringBuilder);

                if (ownedRelationshipCursor.Current is IEndFeatureMembership firstEnd)
                {
                    stringBuilder.Append("of ");
                    EndFeatureMembershipTextualNotationBuilder.BuildConnectorEndMember(firstEnd, writerContext, stringBuilder);
                    ownedRelationshipCursor.Move();

                    stringBuilder.Append("= ");

                    if (ownedRelationshipCursor.Current is IEndFeatureMembership secondEnd)
                    {
                        EndFeatureMembershipTextualNotationBuilder.BuildConnectorEndMember(secondEnd, writerContext, stringBuilder);
                    }

                    ownedRelationshipCursor.Move();
                }
            }
            else
            {
                // Alt 2: (isSufficient?='all')? ('of'? ConnectorEndMember '=' ConnectorEndMember)?
                if (poco.IsSufficient)
                {
                    stringBuilder.Append("all ");
                }

                if (ownedRelationshipCursor.Current is IEndFeatureMembership firstEnd)
                {
                    stringBuilder.Append("of ");
                    EndFeatureMembershipTextualNotationBuilder.BuildConnectorEndMember(firstEnd, writerContext, stringBuilder);
                    ownedRelationshipCursor.Move();

                    stringBuilder.Append("= ");

                    if (ownedRelationshipCursor.Current is IEndFeatureMembership secondEnd)
                    {
                        EndFeatureMembershipTextualNotationBuilder.BuildConnectorEndMember(secondEnd, writerContext, stringBuilder);
                    }

                    ownedRelationshipCursor.Move();
                }
            }
        }

    }
}
