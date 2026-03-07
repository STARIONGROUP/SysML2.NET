// -------------------------------------------------------------------------------------------------
// <copyright file="CommonTextualNotationBuilder.cs" company="Starion Group S.A.">
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
    using System.Linq;

    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Systems.DefinitionAndUsage;

    /// <summary>
    /// Textual Notation Builder that provides common features, usable by others builder
    /// </summary>
    public static class CommonTextualNotationBuilder
    {
        /// <summary>
        /// Asserts that an <see cref="IElement"/> defines properties used by the Identification rule
        /// </summary>
        /// <param name="poco">The <see cref="IElement"/></param>
        /// <returns>True if the <see cref="IElement.DeclaredName"/> or <see cref="IElement.DeclaredShortName"/> is defined</returns>
        public static bool DoesDefinesIdentificationProperties(IElement poco)
        {
            return !string.IsNullOrWhiteSpace(poco.DeclaredName) || !string.IsNullOrWhiteSpace(poco.DeclaredShortName);
        }

        /// <summary>
        /// Asserts that an <see cref="IUsage"/> defines properties used by the UsageDeclaration rule
        /// </summary>
        /// <param name="poco">The <see cref="IUsage"/></param>
        /// <returns>True if respects the <see cref="DoesDefinesIdentificationProperties"/> or have <see cref="IElement.OwnedRelationship"/> not empty</returns>
        public static bool DoesDefinesUsageDeclaration(IElement poco) 
        {
            return DoesDefinesIdentificationProperties(poco) || poco.OwnedRelationship.OfType<IFeature>().Any();
        }
    }
}
