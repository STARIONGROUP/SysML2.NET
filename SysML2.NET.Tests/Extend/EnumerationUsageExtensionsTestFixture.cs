// ------------------------------------------------------------------------------------------------
// <copyright file="EnumerationUsageExtensionsTestFixture.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
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
