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

            // ComputeItemDefinition faithfully implements the OCL itemDefinition = occurrenceDefinition->selectByKind(Structure),
            // reading the subsetted occurrenceDefinition property directly. occurrenceDefinition resolves to
            // OccurrenceUsageExtensions.ComputeOccurrenceDefinition, which is still a stub, so any non-null subject throws
            // NotSupportedException (stub-blocker pattern).
            // For later: once OccurrenceUsageExtensions.ComputeOccurrenceDefinition is implemented, replace the assertion below with a
            // real one: a subject whose occurrenceDefinition includes a Structure plus a non-Structure Class → ComputeItemDefinition
            // returns only the Structure(s).
            var subject = new ItemUsage();
            subject.AssignOwnership(new FeatureTyping { Type = new Structure() });

            Assert.That(subject.ComputeItemDefinition, Throws.TypeOf<NotSupportedException>());
        }
    }
}
