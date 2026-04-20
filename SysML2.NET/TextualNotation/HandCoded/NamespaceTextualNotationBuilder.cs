// -------------------------------------------------------------------------------------------------
// <copyright file="NamespaceTextualNotationBuilder.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.TextualNotation.HandCoded
{
    using System.Text;

    using SysML2.NET.Core.POCO.Kernel.Functions;
    using SysML2.NET.Core.POCO.Kernel.Metadata;
    using SysML2.NET.Core.POCO.Kernel.Packages;
    using SysML2.NET.Core.POCO.Root.Annotations;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Core.POCO.Systems.DefinitionAndUsage;

    public static class NamespaceTextualNotationBuilder
    {
        /// <summary>
        /// Builds the Textual Notation string for the rule RootNamespace
        /// <para>RootNamespace:Namespace=PackageBodyElement*</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Root.Namespaces.INamespace" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildRootNamespace(SysML2.NET.Core.POCO.Root.Namespaces.INamespace poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            // Should call the PackageBodyElement but since the rule focus on a Package type, have to grab the rule body
            // Getting cursor since assignment with += on the ownedRelationship (on next rule since we do have do it multiple times since the '*')
            var cursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);
            
            // While loop since calling with '*'

            while (cursor.Current != null)
            {
                // Order based on inheritance

                switch (cursor.Current)
                {
                    case SysML2.NET.Core.POCO.Kernel.Packages.IElementFilterMembership elementFilterMembership:
                        BuildElementFilterMember(elementFilterMembership, cursorCache, stringBuilder);
                        // Cursor.Move since we do have += 
                        cursor.Move();
                        break;
                    case SysML2.NET.Core.POCO.Root.Namespaces.IOwningMembership owningMembership:
                        BuildPackageMember(owningMembership, cursorCache, stringBuilder);
                        // Cursor.Move since we do have += 
                        cursor.Move();
                        break;
                    case SysML2.NET.Core.POCO.Root.Namespaces.IMembership membership:
                        BuildAliasMember(membership,cursorCache, stringBuilder);
                        // Cursor.Move since we do have += 
                        cursor.Move();
                        break;
                    case SysML2.NET.Core.POCO.Root.Namespaces.IImport import:
                        BuildImport(import, cursorCache, stringBuilder);
                        // Cursor.Move since we do have += 
                        cursor.Move();
                        break;                        
                }
            }
        }

        private static void BuildImport(IImport poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {

        }

        private static void BuildAliasMember(IMembership poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            MembershipTextualNotationBuilder.BuildMemberPrefix(poco, cursorCache, stringBuilder);
            stringBuilder.Append("alias");
            stringBuilder.Append(' ');

            if (!string.IsNullOrEmpty(poco.MemberShortName))
            {
                // no white space since it's '<'
                stringBuilder.Append("<");
                stringBuilder.Append(poco.MemberShortName);
                stringBuilder.Append(">");
                stringBuilder.Append(' ');
            }

            if (!string.IsNullOrEmpty(poco.MemberName))
            {
                stringBuilder.Append(poco.MemberName);
                stringBuilder.Append(' ');
            }
            
            stringBuilder.Append("for");
            stringBuilder.Append(' ');

            stringBuilder.Append(poco.MemberElement.qualifiedName);
            stringBuilder.Append(' ');
            
            BuildRelationshipBody(poco, cursorCache, stringBuilder);
        }

        private static void BuildRelationshipBody(IRelationship poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            // Having an alternate case : condition is defined because the second part have to loop through all ownedRelationship
            if (poco.OwnedRelationship.Count == 0)
            {
                stringBuilder.AppendLine(";");
            }
            else
            {
                stringBuilder.AppendLine("{");
                
                var cursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelationship", poco.OwnedRelationship);

                while (cursor.Current != null)
                {
                    if (cursor.Current is IAnnotation annotation)
                    {
                        BuildOwnedAnnotation(annotation, cursorCache,  stringBuilder);
                    }
                    
                    cursor.Move();
                }
                
                stringBuilder.AppendLine("}");
            }
        }

        private static void BuildOwnedAnnotation(IAnnotation annotation, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            var cursor = cursorCache.GetOrCreateCursor(annotation.Id, "ownedRelatedElement", annotation.OwnedRelatedElement);

            if (cursor.TryGetCurrent<IAnnotatingElement>(out var annotatingElement))
            {
                BuildAnnotatingElement(annotatingElement , cursorCache, stringBuilder);
            }

            cursor.Move();
        }

        private static void BuildAnnotatingElement(IAnnotatingElement poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            // Order based on inheritance

            switch (poco)
            {
                case IDocumentation documentation:
                    BuildDocumentation(documentation, cursorCache, stringBuilder);
                    break;
                case IComment comment:
                    BuildComment(comment, cursorCache, stringBuilder);
                    break;
                case ITextualRepresentation textualRepresentation:
                    BuildTextualRepresentation(textualRepresentation, cursorCache, stringBuilder);
                    break;
                case IMetadataFeature metadataFeature:
                    BuildMetadataFeature(metadataFeature, cursorCache, stringBuilder);
                    break;
            }
        }

        private static void BuildMetadataFeature(IMetadataFeature metadataFeature, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
        }

        private static void BuildTextualRepresentation(ITextualRepresentation textualRepresentation, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
        }

        private static void BuildComment(IComment comment, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
        }

        private static void BuildDocumentation(IDocumentation documentation, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
        }

        private static void BuildPackageMember(IOwningMembership poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            MembershipTextualNotationBuilder.BuildMemberPrefix(poco, cursorCache, stringBuilder);

            var cursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelatedElement", poco.OwnedRelatedElement);

            switch (cursor.Current)
            {
                case SysML2.NET.Core.POCO.Systems.DefinitionAndUsage.IUsage usage:
                    BuildUsageElement(usage, cursorCache, stringBuilder);
                    break;
                case { } element:
                    BuildDefinitionElement(element, cursorCache, stringBuilder);
                    cursor.Move();
                    break;            
            }
        }

        private static void BuildDefinitionElement(IElement poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
        }

        private static void BuildUsageElement(IUsage poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
        }

        private static void BuildElementFilterMember(IElementFilterMembership poco, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
            MembershipTextualNotationBuilder.BuildMemberPrefix(poco, cursorCache, stringBuilder);
            stringBuilder.Append("filter");
            stringBuilder.Append(' ');
            
            var cursor = cursorCache.GetOrCreateCursor(poco.Id, "ownedRelatedElement", poco.OwnedRelatedElement);

            if (cursor.Current is IExpression expression)
            {
                BuildOwnedExpression(expression, cursorCache, stringBuilder);
                cursor.Move();
            }

            // Append line since it's one of : ';', '{', '}' 
            stringBuilder.AppendLine(";");
        }

        private static void BuildOwnedExpression(IExpression expression, ICursorCache cursorCache, StringBuilder stringBuilder)
        {
        }
    }
}
