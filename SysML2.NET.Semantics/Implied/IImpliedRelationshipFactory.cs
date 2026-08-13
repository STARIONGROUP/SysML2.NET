// -------------------------------------------------------------------------------------------------
// <copyright file="IImpliedRelationshipFactory.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Core.POCO.Core.Classifiers;
    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;

    /// <summary>
    /// Creates the Relationship instances that satisfy a semantic constraint.
    /// </summary>
    /// <remarks>
    /// Every product carries isImplied and is DETACHED: it is not added to any ownedRelationship, so the
    /// model stays a faithful match to what was read and isImpliedIncluded stays false.
    /// </remarks>
    public interface IImpliedRelationshipFactory
    {
        /// <summary>
        /// Creates an implied Subclassification between two Classifiers.
        /// </summary>
        /// <param name="specific">The specializing Classifier.</param>
        /// <param name="general">The Classifier being specialized.</param>
        /// <returns>A detached Subclassification with isImplied set.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when either argument is null.</exception>
        ISubclassification CreateImpliedSubclassification(IClassifier specific, IClassifier general);

        /// <summary>
        /// Creates an implied Subsetting between two Features.
        /// </summary>
        /// <param name="specific">The subsetting Feature.</param>
        /// <param name="general">The Feature being subsetted.</param>
        /// <returns>A detached Subsetting with isImplied set.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when either argument is null.</exception>
        ISubsetting CreateImpliedSubsetting(IFeature specific, IFeature general);

        /// <summary>
        /// Creates an implied Redefinition between two Features.
        /// </summary>
        /// <param name="specific">The redefining Feature.</param>
        /// <param name="general">The Feature being redefined.</param>
        /// <returns>A detached Redefinition with isImplied set.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when either argument is null.</exception>
        IRedefinition CreateImpliedRedefinition(IFeature specific, IFeature general);

        /// <summary>
        /// Creates an implied FeatureTyping between a Feature and the Type that types it.
        /// </summary>
        /// <param name="typedFeature">The Feature being typed.</param>
        /// <param name="type">The Type typing the Feature.</param>
        /// <returns>A detached FeatureTyping with isImplied set.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when either argument is null.</exception>
        IFeatureTyping CreateImpliedFeatureTyping(IFeature typedFeature, IType type);
    }
}
