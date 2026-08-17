// -------------------------------------------------------------------------------------------------
// <copyright file="ImpliedRelationshipFactory.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Semantics.Implied
{
    using System;

    using SysML2.NET.Core.POCO.Core.Classifiers;
    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Extensions;

    /// <summary>
    /// Creates detached Relationship instances that carry isImplied.
    /// </summary>
    /// <remarks>
    /// Each product is given a fresh Id so that instances remain distinguishable in the dictionaries and sets
    /// the computation uses. Nothing created here is attached to an ownedRelationship, so the model read from
    /// disk is left untouched and isImpliedIncluded stays false.
    /// </remarks>
    public class ImpliedRelationshipFactory : IImpliedRelationshipFactory
    {
        /// <summary>
        /// Creates an implied Subclassification between two Classifiers.
        /// </summary>
        /// <param name="specific">The specializing Classifier.</param>
        /// <param name="general">The Classifier being specialized.</param>
        /// <returns>A detached Subclassification with isImplied set.</returns>
        /// <exception cref="ArgumentNullException">Thrown when either argument is null.</exception>
        public ISubclassification CreateImpliedSubclassification(IClassifier specific, IClassifier general)
        {
            if (specific == null)
            {
                throw new ArgumentNullException(nameof(specific));
            }

            if (general == null)
            {
                throw new ArgumentNullException(nameof(general));
            }

            return new Subclassification
            {
                Id = Guid.NewGuid(),
                IsImplied = true,
                Subclassifier = specific,
                Superclassifier = general
            };
        }

        /// <summary>
        /// Creates an implied Subsetting between two Features.
        /// </summary>
        /// <param name="specific">The subsetting Feature.</param>
        /// <param name="general">The Feature being subsetted.</param>
        /// <returns>A detached Subsetting with isImplied set.</returns>
        /// <exception cref="ArgumentNullException">Thrown when either argument is null.</exception>
        public ISubsetting CreateImpliedSubsetting(IFeature specific, IFeature general)
        {
            if (specific == null)
            {
                throw new ArgumentNullException(nameof(specific));
            }

            if (general == null)
            {
                throw new ArgumentNullException(nameof(general));
            }

            return new Subsetting
            {
                Id = Guid.NewGuid(),
                IsImplied = true,
                SubsettingFeature = specific,
                SubsettedFeature = general
            };
        }

        /// <summary>
        /// Creates a detached Feature whose chainingFeatures are the two supplied Features, in order.
        /// </summary>
        /// <param name="first">The first Feature of the chain.</param>
        /// <param name="second">The second Feature of the chain.</param>
        /// <returns>A detached Feature standing for the chain <c>first.second</c>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when either Feature is null.</exception>
        public IFeature CreateImpliedFeatureChain(IFeature first, IFeature second)
        {
            if (first == null)
            {
                throw new ArgumentNullException(nameof(first));
            }

            if (second == null)
            {
                throw new ArgumentNullException(nameof(second));
            }

            var chain = new Feature { Id = Guid.NewGuid() };

            // The chain is expressed through owned FeatureChainings, which is what chainingFeature derives
            // from — setting the derived list directly would not survive a re-read of the property.
            chain.AssignOwnership(new FeatureChaining { Id = Guid.NewGuid(), IsImplied = true, ChainingFeature = first });
            chain.AssignOwnership(new FeatureChaining { Id = Guid.NewGuid(), IsImplied = true, ChainingFeature = second });

            return chain;
        }

        /// <summary>
        /// Creates an implied Redefinition between two Features.
        /// </summary>
        /// <param name="specific">The redefining Feature.</param>
        /// <param name="general">The Feature being redefined.</param>
        /// <returns>A detached Redefinition with isImplied set.</returns>
        /// <exception cref="ArgumentNullException">Thrown when either argument is null.</exception>
        public IRedefinition CreateImpliedRedefinition(IFeature specific, IFeature general)
        {
            if (specific == null)
            {
                throw new ArgumentNullException(nameof(specific));
            }

            if (general == null)
            {
                throw new ArgumentNullException(nameof(general));
            }

            return new Redefinition
            {
                Id = Guid.NewGuid(),
                IsImplied = true,
                RedefiningFeature = specific,
                RedefinedFeature = general
            };
        }

        /// <summary>
        /// Creates an implied FeatureTyping between a Feature and the Type that types it.
        /// </summary>
        /// <param name="typedFeature">The Feature being typed.</param>
        /// <param name="type">The Type typing the Feature.</param>
        /// <returns>A detached FeatureTyping with isImplied set.</returns>
        /// <exception cref="ArgumentNullException">Thrown when either argument is null.</exception>
        public IFeatureTyping CreateImpliedFeatureTyping(IFeature typedFeature, IType type)
        {
            if (typedFeature == null)
            {
                throw new ArgumentNullException(nameof(typedFeature));
            }

            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return new FeatureTyping
            {
                Id = Guid.NewGuid(),
                IsImplied = true,
                TypedFeature = typedFeature,
                Type = type
            };
        }
    }
}
