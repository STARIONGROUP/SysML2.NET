// ------------------------------------------------------------------------------------------------
// <copyright file="DocumentationExtensionsTestFixture.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Systems.DefinitionAndUsage;
    using SysML2.NET.Exceptions;
    using SysML2.NET.Extensions;

    [TestFixture]
    public class DocumentationExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeDocumentedElement()
        {
            Assert.That(() => ((IDocumentation)null).ComputeDocumentedElement(),
                Throws.TypeOf<ArgumentNullException>());

            // Fresh Documentation has no owning relationship, so the documented element cannot be
            // resolved — the implementation must throw IncompleteModelException (Option A path).
            var emptyDoc = new Documentation();

            Assert.That(() => emptyDoc.ComputeDocumentedElement(),
                Throws.TypeOf<IncompleteModelException>());

            // Populated case: wire a Definition as the owner of the Documentation via an Annotation.
            // After AssignOwnership, documentation.OwningRelationship.OwningRelatedElement == target,
            // so ComputeDocumentedElement() must return target.
            var target = new Definition();
            var annotation = new Annotation();
            var doc = new Documentation();

            target.AssignOwnership(annotation, doc);

            Assert.That(doc.ComputeDocumentedElement(), Is.SameAs(target));
        }
    }
}
