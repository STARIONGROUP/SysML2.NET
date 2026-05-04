// -------------------------------------------------------------------------------------------------
// <copyright file="SharedTextualNotationBuilder.cs" company="Starion Group S.A.">
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
    /// The <see cref="SharedTextualNotationBuilder" /> hosts the generated textual-notation methods
    /// for grammar rules that declare no target type and do not share a name with any UML class
    /// (e.g. <c>FeaturePrefix</c>). These rules are lifted here so that every caller dispatches to a
    /// single implementation instead of duplicating the rule body at each call site.
    /// </summary>
    public static partial class SharedTextualNotationBuilder
    {
        /// <summary>
        /// Builds the Textual Notation string for the rule BasicDefinitionPrefix
        /// <para>BasicDefinitionPrefix=isAbstract?='abstract'|isVariation?='variation'</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IDefinition" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildBasicDefinitionPrefix(SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IDefinition poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            if (poco.IsAbstract)
            {
                stringBuilder.Append(" abstract ");
            }
            else if (poco.IsVariation)
            {
                stringBuilder.Append(" variation ");
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule DependencyDeclaration
        /// <para>DependencyDeclaration=(Identification'from')?client+=[QualifiedName](','client+=[QualifiedName])*'to'supplier+=[QualifiedName](','supplier+=[QualifiedName])*</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Root.Dependencies.IDependency" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildDependencyDeclaration(SysML2.NET.Core.POCO.Root.Dependencies.IDependency poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var clientCursor = cursorCache.GetOrCreateCursor(poco.Id, "client", poco.Client);
            var supplierCursor = cursorCache.GetOrCreateCursor(poco.Id, "supplier", poco.Supplier);

            if (!string.IsNullOrWhiteSpace(poco.DeclaredShortName) || !string.IsNullOrWhiteSpace(poco.DeclaredName))
            {
                ElementTextualNotationBuilder.BuildIdentification(poco, cursorCache, stringBuilder);
                stringBuilder.Append("from ");
                stringBuilder.Append(' ');
            }


            if (clientCursor.Current != null)
            {
                stringBuilder.Append(clientCursor.Current.qualifiedName);
                clientCursor.Move();
            }

            while (clientCursor.Current != null)
            {
                stringBuilder.Append(", ");

                if (clientCursor.Current != null)
                {
                    stringBuilder.Append(clientCursor.Current.qualifiedName);
                    clientCursor.Move();
                }
                clientCursor.Move();

            }
            stringBuilder.Append("to ");

            if (supplierCursor.Current != null)
            {
                stringBuilder.Append(supplierCursor.Current.qualifiedName);
                supplierCursor.Move();
            }

            while (supplierCursor.Current != null)
            {
                stringBuilder.Append(", ");

                if (supplierCursor.Current != null)
                {
                    stringBuilder.Append(supplierCursor.Current.qualifiedName);
                    supplierCursor.Move();
                }
                supplierCursor.Move();

            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule FeaturePrefix
        /// <para>FeaturePrefix=(EndFeaturePrefix(ownedRelationship+=OwnedCrossFeatureMember)?|BasicFeaturePrefix)(ownedRelationship+=PrefixMetadataMember)*</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Core.Features.IFeature" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildFeaturePrefix(SysML2.NET.Core.POCO.Core.Features.IFeature poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var ownedRelationshipCursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);
            BuildFeaturePrefixHandCoded(poco, cursorCache, stringBuilder);
            stringBuilder.Append(' ');

            while (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Root.Namespaces.IOwningMembership owningMembershipGuard && owningMembershipGuard.OwnedRelatedElement.OfType<SysML2.NET.Core.POCO.Systems.Metadata.IMetadataUsage>().Any())
            {

                if (ownedRelationshipCursor.Current != null)
                {

                    if (ownedRelationshipCursor.Current is SysML2.NET.Core.POCO.Root.Namespaces.IOwningMembership elementAsOwningMembership)
                    {
                        OwningMembershipTextualNotationBuilder.BuildPrefixMetadataMember(elementAsOwningMembership, cursorCache, stringBuilder);
                    }
                }
                ownedRelationshipCursor.Move();

            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule LiteralReal
        /// <para>LiteralReal=value=RealValue</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Kernel.FeatureValues.IFeatureValue" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildLiteralReal(SysML2.NET.Core.POCO.Kernel.FeatureValues.IFeatureValue poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {

            if (poco.value != null)
            {
                BuildRealValueHandCoded(poco.value, cursorCache, stringBuilder);
            }

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule NonBehaviorBodyItem
        /// <para>NonBehaviorBodyItem=ownedRelationship+=Import|ownedRelationship+=AliasMember|ownedRelationship+=DefinitionMember|ownedRelationship+=VariantUsageMember|ownedRelationship+=NonOccurrenceUsageMember|(ownedRelationship+=SourceSuccessionMember)?ownedRelationship+=StructureUsageMember</para>
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Root.Elements.IElement" /> from which the rule should be build</param>
        /// <param name="cursorCache">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildNonBehaviorBodyItem(SysML2.NET.Core.POCO.Root.Elements.IElement poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            BuildNonBehaviorBodyItemHandCoded(poco, cursorCache, stringBuilder);
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
