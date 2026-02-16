// -------------------------------------------------------------------------------------------------
// <copyright file="ElementExtensions.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Extensions
{
    using System;
    using System.Linq;

    using SysML2.NET.Core.POCO.Root.Elements;

    /// <summary>
    /// Extension method for the <see cref="IElement"/> interface
    /// </summary>
    public static class ElementExtensions
    {
        /// <summary>
        /// Assigns the containment ownership of a target <see cref="IElement"/> to a source <see cref="IElement"/> via the bridge <see cref="IRelationship"/>
        /// </summary>
        /// <param name="source">The container<see cref="IElement"/></param>
        /// <param name="bridgeRelationship">The bridge <see cref="IRelationship"/></param>
        /// <param name="target">The contained <see cref="IElement"/></param>
        /// <exception cref="ArgumentNullException">If one of the parameter is null</exception>
        /// <exception cref="InvalidOperationException">If the <paramref name="source"/> equals the <paramref name="target"/>
        ///  or if the <paramref name="bridgeRelationship"/> equals the <paramref name="target"/></exception>
        /// <remarks>Note: The source is the container element and the target is the containee</remarks>
        public static void AssignOwnership(this IElement source, IRelationship bridgeRelationship, IElement target)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (bridgeRelationship == null)
            {
                throw new ArgumentNullException(nameof(bridgeRelationship));
            }

            if (target == null)
            {
                throw new ArgumentNullException(nameof(target));
            }

            if (source == target)
            {
                throw new InvalidOperationException("The parent cannot own itself.");
            }

            if (bridgeRelationship == target)
            {
                throw new InvalidOperationException("The relationship can not own itself.");
            }

            // Missing logic: Child can not contain Parent at any containment level

            if (bridgeRelationship.OwningRelatedElement != null && bridgeRelationship.OwningRelatedElement != source)
            {
                ((IContainedElement)bridgeRelationship.OwningRelatedElement).OwnedRelationship.Remove(bridgeRelationship);
            }

            ((IContainedRelationship)bridgeRelationship).OwningRelatedElement = source;
            
            if (!source.OwnedRelationship.Contains(bridgeRelationship))
            {
                ((IContainedElement)source).OwnedRelationship.Add(bridgeRelationship);
            }

            if (target.OwningRelationship != null && target.OwningRelationship != bridgeRelationship)
            {
                ((IContainedRelationship)target.OwningRelationship).OwnedRelatedElement.Remove(target);
            }
            
            ((IContainedRelationship)bridgeRelationship).OwnedRelatedElement.Add(target);
        }
    }
}
