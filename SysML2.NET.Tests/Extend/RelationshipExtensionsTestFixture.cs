// -------------------------------------------------------------------------------------------------
// <copyright file="RelationshipExtensionsTestFixture.cs" company="Starion Group S.A.">
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

    using Moq;

    using NUnit.Framework;
    
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Systems.DefinitionAndUsage;

    [TestFixture]
    public class RelationshipExtensionsTestFixture
    {
        [Test]
        public void VerfiyComputeRelatedElement()
        {
            Assert.That(() => ((IRelationship)null).ComputeRelatedElement(), Throws.TypeOf<ArgumentNullException>());
            
            var relationship = new Mock<IRelationship>();
            relationship.Setup(x => x.Source).Returns([]);
            relationship.Setup(x => x.Target).Returns([]);

            Assert.That(relationship.Object.ComputeRelatedElement(), Is.Empty);

            var sourceDefinition = new Definition();
            relationship.Setup(x => x.Source).Returns([sourceDefinition]);
            
            Assert.That(relationship.Object.ComputeRelatedElement(), Is.EquivalentTo([sourceDefinition]));

            var targetDefinition = new Definition();
            relationship.Setup(x => x.Target).Returns([targetDefinition]);
            
            Assert.That(relationship.Object.ComputeRelatedElement(), Is.EquivalentTo([sourceDefinition,  targetDefinition]));
            var secondSource = new Definition();
            
            relationship.Setup(x => x.Source).Returns([sourceDefinition, secondSource]);
            Assert.That(relationship.Object.ComputeRelatedElement(), Is.EquivalentTo([sourceDefinition, secondSource, targetDefinition]));
        }
    }
}
