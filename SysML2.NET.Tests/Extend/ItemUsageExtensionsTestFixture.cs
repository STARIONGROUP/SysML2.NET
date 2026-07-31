// -------------------------------------------------------------------------------------------------
// <copyright file="ItemUsageExtensionsTestFixture.cs" company="Starion Group S.A.">
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

    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Kernel.Classes;
    using SysML2.NET.Core.POCO.Kernel.Structures;
    using SysML2.NET.Core.POCO.Systems.Items;
    using SysML2.NET.Extensions;

    [TestFixture]
    public class ItemUsageExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeItemDefinition()
        {
            Assert.That(() => ((IItemUsage)null).ComputeItemDefinition(), Throws.TypeOf<ArgumentNullException>());

            // Empty: no typings → empty list. itemDefinition = occurrenceDefinition->selectByKind(Structure),
            // occurrenceDefinition = type->selectByKind(Class).
            var emptySubject = new ItemUsage();

            Assert.That(emptySubject.ComputeItemDefinition(), Is.Empty);

            // Populated: a Structure (which is a Class → surfaces in occurrenceDefinition, and is a Structure → kept in
            // itemDefinition) plus a plain Class (surfaces in occurrenceDefinition but is NOT a Structure → excluded).
            var subject = new ItemUsage();
            var structure = new Structure();
            subject.AssignOwnership(new FeatureTyping { Type = structure });
            subject.AssignOwnership(new FeatureTyping { Type = new Class() });

            var result = subject.ComputeItemDefinition();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(result, Has.Count.EqualTo(1));
                Assert.That(result, Does.Contain(structure));
            }
        }
    }
}
