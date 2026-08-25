// -------------------------------------------------------------------------------------------------
// <copyright file="CollectionCursorTestFixture.cs" company="Starion Group S.A.">
//
//    Copyright (C) 2022-2026 Starion Group S.A.
//
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
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

namespace SysML2.NET.Serializer.TextualNotation.Tests.Writers
{
    using System;

    using NUnit.Framework;

    using SysML2.NET.Serializer.TextualNotation.Writers;

    /// <summary>
    /// Test fixture for <see cref="CollectionCursor{T}"/>'s forward-progress surface —
    /// <see cref="CollectionCursor{T}.Position"/> and
    /// <see cref="CollectionCursor{T}.AssertAdvancedSince"/> — which every generated <c>*</c> loop relies
    /// on to turn a non-terminating iteration into an immediate failure.
    /// </summary>
    [TestFixture]
    public class CollectionCursorTestFixture
    {
        /// <summary>
        /// <see cref="CollectionCursor{T}.Position"/> reports the offset and tracks
        /// <see cref="CollectionCursor{T}.Move"/>, saturating at the end of the collection rather than
        /// running past it.
        /// </summary>
        [Test]
        public void VerifyPosition()
        {
            var cursor = new CollectionCursor<string>(["alpha", "beta"]);

            Assert.That(cursor.Position, Is.EqualTo(0));

            cursor.Move();

            Assert.That(cursor.Position, Is.EqualTo(1));

            cursor.Move();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(cursor.Position, Is.EqualTo(2));
                Assert.That(cursor.Current, Is.Null);
            }

            // Move past the end saturates, so the position stays a valid comparison anchor.
            cursor.Move();

            Assert.That(cursor.Position, Is.EqualTo(2));
        }

        /// <summary>
        /// <see cref="CollectionCursor{T}.AssertAdvancedSince"/> passes when the cursor consumed something
        /// and throws when it did not — the case that would otherwise spin the enclosing <c>*</c> loop
        /// forever.
        /// </summary>
        [Test]
        public void VerifyAssertAdvancedSince()
        {
            var cursor = new CollectionCursor<string>(["alpha", "beta"]);

            // An iteration that consumed one element is forward progress.
            var positionBeforeConsumingIteration = cursor.Position;
            cursor.Move();

            Assert.That(() => cursor.AssertAdvancedSince(positionBeforeConsumingIteration, "DefinitionBodyItem"), Throws.Nothing);

            // An iteration that consumed nothing cannot terminate the loop, so it must fail loudly.
            var positionBeforeStalledIteration = cursor.Position;

            var stalledIteration = Assert.Throws<InvalidOperationException>(
                () => cursor.AssertAdvancedSince(positionBeforeStalledIteration, "DefinitionBodyItem"));

            using (Assert.EnterMultipleScope())
            {
                // The rule name is the only handle a caller has on WHICH loop stalled, so it must be quoted.
                Assert.That(stalledIteration.Message, Does.Contain("DefinitionBodyItem"));

                // The element under the cursor is what the loop admitted and the builder declined — naming
                // it is what makes the mismatch diagnosable without a debugger.
                Assert.That(stalledIteration.Message, Does.Contain(nameof(String)));
            }

            // An exhausted cursor stalls too: the loop condition, not this assertion, is what ends iteration.
            cursor.Move();
            var positionAtEnd = cursor.Position;

            Assert.That(() => cursor.AssertAdvancedSince(positionAtEnd, "CaseBodyItem"), Throws.TypeOf<InvalidOperationException>());
        }
    }
}
