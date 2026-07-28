// -------------------------------------------------------------------------------------------------
// <copyright file="ConjugatedPortDefinitionExtensionsTestFixture.cs" company="Starion Group S.A.">
// 
//   Copyright (C) 2022-2026 Starion Group S.A.
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

    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Core.POCO.Systems.Ports;
    using SysML2.NET.Exceptions;
    using SysML2.NET.Extensions;

    [TestFixture]
    public class ConjugatedPortDefinitionExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeOriginalPortDefinition()
        {
            // Null Subject:
            Assert.That(() => ((IConjugatedPortDefinition)null).ComputeOriginalPortDefinition(), Throws.TypeOf<ArgumentNullException>());

            // Missing Owning Membership:
            var emptyConjugatedPortDefinition = new ConjugatedPortDefinition();
            Assert.That(() => emptyConjugatedPortDefinition.ComputeOriginalPortDefinition(), Throws.TypeOf<IncompleteModelException>());

            // With Original Port Definition:
            var originalPortDefinition = new PortDefinition();
            var conjugatedPortDefinition = new ConjugatedPortDefinition();
            originalPortDefinition.AssignOwnership(new OwningMembership(), conjugatedPortDefinition);
            Assert.That(conjugatedPortDefinition.ComputeOriginalPortDefinition(), Is.SameAs(originalPortDefinition));

            // Wrong Owner Type:
            var nonPortDefinitionOwner = new Namespace();
            var conjugatedWithNonPortDefinitionOwner = new ConjugatedPortDefinition();
            nonPortDefinitionOwner.AssignOwnership(new OwningMembership(), conjugatedWithNonPortDefinitionOwner);
            Assert.That(() => conjugatedWithNonPortDefinitionOwner.ComputeOriginalPortDefinition(), Throws.TypeOf<IncompleteModelException>());
        }

        [Test]
        public void VerifyComputeOwnedPortConjugator()
        {
            // Null Subject:
            Assert.That(() => ((IConjugatedPortDefinition)null).ComputeOwnedPortConjugator(), Throws.TypeOf<ArgumentNullException>());

            // Missing Port Conjugation:
            var emptyConjugatedPortDefinition = new ConjugatedPortDefinition();
            Assert.That(() => emptyConjugatedPortDefinition.ComputeOwnedPortConjugator(), Throws.TypeOf<IncompleteModelException>());

            // With Owned Port Conjugator:
            var conjugatedPortDefinition = new ConjugatedPortDefinition();
            var portConjugation = new PortConjugation();
            ((IContainedElement)conjugatedPortDefinition).OwnedRelationship.Add(portConjugation);
            Assert.That(conjugatedPortDefinition.ComputeOwnedPortConjugator(), Is.SameAs(portConjugation));

            // Multiple Port Conjugations:
            var conjugatedWithMultiplePortConjugations = new ConjugatedPortDefinition();
            ((IContainedElement)conjugatedWithMultiplePortConjugations).OwnedRelationship.Add(new PortConjugation());
            ((IContainedElement)conjugatedWithMultiplePortConjugations).OwnedRelationship.Add(new PortConjugation());
            Assert.That(() => conjugatedWithMultiplePortConjugations.ComputeOwnedPortConjugator(), Throws.TypeOf<MultiplicityViolationException>());
        }
        
        [Test]
        public void VerifyComputeRedefinedEffectiveNameOperation()
        {
            // Null subject:
            Assert.That(() => ((IConjugatedPortDefinition)null).ComputeRedefinedEffectiveNameOperation(), Throws.TypeOf<ArgumentNullException>());

            // Missing original port definition:
            var emptyConjugatedPortDefinition = new ConjugatedPortDefinition();
            Assert.That(() => emptyConjugatedPortDefinition.ComputeRedefinedEffectiveNameOperation(), Throws.TypeOf<IncompleteModelException>());

            // Original name is null:
            var originalPortDefinitionWithoutName = new PortDefinition();
            var conjugatedPortDefinitionWithoutOriginalName = new ConjugatedPortDefinition();
            originalPortDefinitionWithoutName.AssignOwnership(new OwningMembership(), conjugatedPortDefinitionWithoutOriginalName);
            Assert.That(conjugatedPortDefinitionWithoutOriginalName.ComputeRedefinedEffectiveNameOperation(), Is.Null);

            // Original name exists:
            var originalPortDefinition = new PortDefinition { DeclaredName = "port" };
            var conjugatedPortDefinition = new ConjugatedPortDefinition();
            originalPortDefinition.AssignOwnership(new OwningMembership(), conjugatedPortDefinition);
            Assert.That(conjugatedPortDefinition.ComputeRedefinedEffectiveNameOperation(), Is.EqualTo("~port"));
        }
        
    }
}
