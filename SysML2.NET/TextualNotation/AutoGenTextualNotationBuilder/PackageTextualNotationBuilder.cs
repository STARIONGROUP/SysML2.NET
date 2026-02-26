// -------------------------------------------------------------------------------------------------
// <copyright file="PackageTextualNotationBuilder.cs" company="Starion Group S.A.">
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
    /// The <see cref="PackageTextualNotationBuilder" /> provides Textual Notation Builder for the <see cref="SysML2.NET.Core.POCO.Kernel.Packages.IPackage" /> element
    /// </summary>
    public static partial class PackageTextualNotationBuilder
    {
        /// <summary>
        /// Builds the Textual Notation string for the rule PackageDeclaration
        /// <para>PackageDeclaration:Package='package'Identification</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Kernel.Packages.IPackage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildPackageDeclaration(SysML2.NET.Core.POCO.Kernel.Packages.IPackage poco, StringBuilder stringBuilder)
        {
            stringBuilder.Append("package ");
            ElementTextualNotationBuilder.BuildIdentification(poco, stringBuilder);

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule PackageBody
        /// <para>PackageBody:Package=';'|'{'PackageBodyElement*'}'</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Kernel.Packages.IPackage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildPackageBody(SysML2.NET.Core.POCO.Kernel.Packages.IPackage poco, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("Multiple alternatives not implemented yet");
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule PackageBodyElement
        /// <para>PackageBodyElement:Package=ownedRelationship+=PackageMember|ownedRelationship+=ElementFilterMember|ownedRelationship+=AliasMember|ownedRelationship+=Import</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Kernel.Packages.IPackage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildPackageBodyElement(SysML2.NET.Core.POCO.Kernel.Packages.IPackage poco, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("Multiple alternatives not implemented yet");
        }

        /// <summary>
        /// Builds the Textual Notation string for the rule FilterPackage
        /// <para>FilterPackage:Package=ownedRelationship+=FilterPackageImport(ownedRelationship+=FilterPackageMember)+</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Kernel.Packages.IPackage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildFilterPackage(SysML2.NET.Core.POCO.Kernel.Packages.IPackage poco, StringBuilder stringBuilder)
        {
            throw new System.NotSupportedException("Assigment of enumerable not supported yet");
            throw new System.NotSupportedException("Assigment of enumerable not supported yet");

            stringBuilder.Append(' ');

        }

        /// <summary>
        /// Builds the Textual Notation string for the rule Package
        /// <para>Package=(ownedRelationship+=PrefixMetadataMember)*PackageDeclarationPackageBody</para>    
        /// </summary>
        /// <param name="poco">The <see cref="SysML2.NET.Core.POCO.Kernel.Packages.IPackage" /> from which the rule should be build</param>
        /// <param name="stringBuilder">The <see cref="StringBuilder" /> that contains the entire textual notation</param>
        public static void BuildPackage(SysML2.NET.Core.POCO.Kernel.Packages.IPackage poco, StringBuilder stringBuilder)
        {
            if (poco.OwnedRelationship.Count != 0)
            {
                throw new System.NotSupportedException("Assigment of enumerable not supported yet");
                stringBuilder.Append(' ');
            }

            BuildPackageDeclaration(poco, stringBuilder);
            BuildPackageBody(poco, stringBuilder);

        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
