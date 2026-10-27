// ------------------------------------------------------------------------------------------------
// <copyright file="NamespaceImportTextualNotationBuilder.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Serializer.TextualNotation.Writers
{
    using System.Linq;

    using SysML2.NET.Core.POCO.Kernel.Packages;
    using SysML2.NET.Core.POCO.Root.Namespaces;

    /// <summary>
    /// Hand-coded part of the <see cref="NamespaceImportTextualNotationBuilder"/>
    /// </summary>
    public static partial class NamespaceImportTextualNotationBuilder
    {
        /// <summary>
        /// Builds the Textual Notation string for the rule NamespaceImport
        /// <remarks>NamespaceImport=importedNamespace=[QualifiedName]'::''*'('::'isRecursive?='**')?|importedNamespace=FilterPackage{ownedRelatedElement+=importedNamespace}</remarks>
        /// </summary>
        /// <param name="poco">The <see cref="INamespaceImport" /> from which the rule should be build</param>
        /// <param name="writerContext">The <see cref="ICursorCache" /> used to get access to CursorCollection for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="IndentedStringBuilder" /> that contains the entire textual notation</param>
        private static void BuildNamespaceImportHandCoded(INamespaceImport poco, TextualNotationWriterContext writerContext, IndentedStringBuilder stringBuilder)
        {
            if (poco.OwnedRelatedElement.Contains(poco.ImportedNamespace) && poco.ImportedNamespace is IPackage filterPackage)
            {
                PackageTextualNotationBuilder.BuildFilterPackage(filterPackage, writerContext, stringBuilder);
            }
            else if (poco.ImportedNamespace != null)
            {
                SharedTextualNotationBuilder.AppendQualifiedName(stringBuilder, poco.ImportedNamespace, writerContext, poco);
                stringBuilder.Append("::*");

                if (poco.IsRecursive)
                {
                    stringBuilder.Append("::**");
                }

                stringBuilder.Append(' ');
            }
        }
    }
}
