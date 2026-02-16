// -------------------------------------------------------------------------------------------------
// <copyright file="IContainedElement.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Core.POCO.Root.Elements
{
    using SysML2.NET.Collections;

    /// <summary>
    /// Provides internal locking principle to correctly implement the containment principle  for the <see cref="IElement" />
    /// </summary>
    internal interface IContainedElement
    {
        /// <summary>
        /// Provided backing field for <see cref="IElement.OwnedRelationship" />
        /// </summary>
        ContainerList<IElement, IRelationship> OwnedRelationship { get; set; }
        
        /// <summary>
        /// Provided backing field for <see cref="IElement.OwningRelationship" />
        /// </summary>
        IRelationship OwningRelationship { get; set; }
    }
}
