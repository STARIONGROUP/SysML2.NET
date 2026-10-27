// ------------------------------------------------------------------------------------------------
// <copyright file="TextualRepresentationExtensionsTestFixture.cs" company="Starion Group S.A.">
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

    using SysML2.NET.Core.POCO.Root.Annotations;
    using SysML2.NET.Core.POCO.Systems.DefinitionAndUsage;
    using SysML2.NET.Exceptions;
    using SysML2.NET.Extensions;

    [TestFixture]
    public class TextualRepresentationExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeRepresentedElement()
        {
            Assert.That(() => ((ITextualRepresentation)null).ComputeRepresentedElement(),
                Throws.TypeOf<ArgumentNullException>());

            var emptyTextualRep = new TextualRepresentation();

            Assert.That(() => emptyTextualRep.ComputeRepresentedElement(),
                Throws.TypeOf<IncompleteModelException>());

            var target = new Definition();
            var textualRep = new TextualRepresentation();
            var annotation = new Annotation();

            target.AssignOwnership(annotation, textualRep);

            Assert.That(textualRep.ComputeRepresentedElement(), Is.SameAs(target));
        }
    }
}
