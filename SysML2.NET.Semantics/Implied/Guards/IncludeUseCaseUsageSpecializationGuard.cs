// -------------------------------------------------------------------------------------------------
// <copyright file="IncludeUseCaseUsageSpecializationGuard.cs" company="Starion Group S.A.">
//
//    Copyright (C) 2022-2026 Starion Group S.A.
//
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
//
//         http://www.apache.org/licenses/LICENSE-2.0
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Semantics.Implied.Guards
{
    using System;

    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Systems.UseCases;

    /// <summary>
    /// Guards checkIncludeUseCaseUsageSpecialization: an IncludeUseCaseUsage owned by a use case specializes UseCases::UseCase::includedUseCases.
    /// </summary>
    /// <remarks>
    /// OCL: <c>owningType &lt;&gt; null and (owningType.oclIsKindOf(UseCaseDefinition) or owningType.oclIsKindOf(UseCaseUsage) implies specializesFromLibrary('UseCases::UseCase::includedUseCases')</c>
    /// </remarks>
    public class IncludeUseCaseUsageSpecializationGuard : IImpliedRuleGuard
    {
        /// <summary>
        /// Gets the name of the semantic constraint this guard decides.
        /// </summary>
        public string ConstraintName => "checkIncludeUseCaseUsageSpecialization";

        /// <summary>
        /// Asserts whether the constraint applies to the supplied Element.
        /// </summary>
        /// <param name="element">The Element under evaluation.</param>
        /// <returns>True when the constraint applies.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="element" /> is null.</exception>
        public bool Applies(IElement element)
        {
            return element == null
                ? throw new ArgumentNullException(nameof(element))
                : element is IIncludeUseCaseUsage { owningType: IUseCaseDefinition or IUseCaseUsage };
        }
    }
}
