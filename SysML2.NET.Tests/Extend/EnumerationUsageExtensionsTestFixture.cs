// -------------------------------------------------------------------------------------------------
// <copyright file="EnumerationUsageExtensionsTestFixture.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Core.POCO.Systems.Enumerations;
    using SysML2.NET.Exceptions;
    using SysML2.NET.Extensions;

    [TestFixture]
    public class EnumerationUsageExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeEnumerationDefinition()
        {
            Assert.That(() => ((IEnumerationUsage)null).ComputeEnumerationDefinition(), Throws.TypeOf<ArgumentNullException>());

            // [1..1] lower-bound violation: no EnumerationDefinition typing → IncompleteModelException.
            var subjectNoTyping = new EnumerationUsage();

            Assert.That(subjectNoTyping.ComputeEnumerationDefinition, Throws.TypeOf<IncompleteModelException>());

            // Populated: exactly one FeatureTyping whose Type is an EnumerationDefinition → returned.
            var subjectOneTyping = new EnumerationUsage();
            var enumerationDefinition = new EnumerationDefinition();
            subjectOneTyping.AssignOwnership(new FeatureTyping { Type = enumerationDefinition });

            Assert.That(subjectOneTyping.ComputeEnumerationDefinition(), Is.SameAs(enumerationDefinition));

            // [1..1] upper-bound violation: two EnumerationDefinition typings → MultiplicityViolationException.
            var subjectTwoTypings = new EnumerationUsage();
            subjectTwoTypings.AssignOwnership(new FeatureTyping { Type = new EnumerationDefinition() });
            subjectTwoTypings.AssignOwnership(new FeatureTyping { Type = new EnumerationDefinition() });

            Assert.That(subjectTwoTypings.ComputeEnumerationDefinition, Throws.TypeOf<MultiplicityViolationException>());
        }
    }
}
