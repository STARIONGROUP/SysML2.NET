// -------------------------------------------------------------------------------------------------
// <copyright file="FeatureValueExtensionsTestFixture.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Tests.Extend
{
    using System;

    using NUnit.Framework;

    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Kernel.Expressions;
    using SysML2.NET.Core.POCO.Kernel.FeatureValues;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Extensions;

    [TestFixture]
    public class FeatureValueExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeFeatureWithValue()
        {
            Assert.That(() => ((IFeatureValue)null).ComputeFeatureWithValue(), Throws.TypeOf<ArgumentNullException>());

            var feature = new Feature();
            var featureValue = new FeatureValue();
            var literalBoolean = new LiteralBoolean();

            feature.AssignOwnership(featureValue, literalBoolean);

            Assert.That(featureValue.ComputeFeatureWithValue(), Is.SameAs(feature));

            // Non-Feature owning namespace: direct field bypass — the cast must return null.
            var nonFeatureFeatureValue = new FeatureValue();
            var nonFeatureNamespace = new Namespace();

            ((IContainedRelationship)nonFeatureFeatureValue).OwningRelatedElement = nonFeatureNamespace;

            Assert.That(nonFeatureFeatureValue.ComputeFeatureWithValue(), Is.Null);
        }

        [Test]
        public void VerifyComputeValue()
        {
            Assert.That(() => ((IFeatureValue)null).ComputeValue(), Throws.TypeOf<ArgumentNullException>());

            var feature = new Feature();
            var featureValue = new FeatureValue();
            var literalBoolean = new LiteralBoolean();

            feature.AssignOwnership(featureValue, literalBoolean);

            Assert.That(featureValue.ComputeValue(), Is.SameAs(literalBoolean));

            // Non-Expression owned member: direct field bypass — the cast must return null.
            var nonExprFeatureValue = new FeatureValue();
            var nonExprElement = new Namespace();

            ((IContainedRelationship)nonExprFeatureValue).OwnedRelatedElement.Add(nonExprElement);

            Assert.That(nonExprFeatureValue.ComputeValue(), Is.Null);
        }
    }
}
