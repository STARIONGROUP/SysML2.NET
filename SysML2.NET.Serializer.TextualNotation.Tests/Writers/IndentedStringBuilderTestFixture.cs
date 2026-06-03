// -------------------------------------------------------------------------------------------------
// <copyright file="IndentedStringBuilderTestFixture.cs" company="Starion Group S.A.">
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
    /// Test fixture for <see cref="IndentedStringBuilder"/>: verifies that the wrapper emits
    /// the configured 4-space indent prefix at the start of every new line, that
    /// <see cref="IndentedStringBuilder.IncreaseIndent"/> /
    /// <see cref="IndentedStringBuilder.DecreaseIndent"/> nest correctly, that the underflow
    /// guard prevents <see cref="IndentedStringBuilder.IndentLevel"/> going negative, and
    /// that empty-line <see cref="IndentedStringBuilder.AppendLine()"/> never produces
    /// trailing whitespace.
    /// </summary>
    [TestFixture]
    public class IndentedStringBuilderTestFixture
    {
        [Test]
        public void VerifyIndentedStringBuilder()
        {
            // Initial state — fresh builder is empty and at indent level 0.
            var builder = new IndentedStringBuilder();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(builder.IndentLevel, Is.EqualTo(0));
                Assert.That(builder.ToString(), Is.EqualTo(string.Empty));
            }

            // Append at zero indent — content emitted verbatim.
            builder.Append("package ").Append("Foo").Append(' ').Append('{');

            Assert.That(builder.ToString(), Is.EqualTo("package Foo {"));

            // AppendLine arms indent emission; IncreaseIndent affects the next Append.
            builder.AppendLine();
            builder.IncreaseIndent();
            builder.Append("part p;");

            using (Assert.EnterMultipleScope())
            {
                Assert.That(builder.IndentLevel, Is.EqualTo(1));
                Assert.That(builder.ToString(), Is.EqualTo($"package Foo {{{Environment.NewLine}    part p;"));
            }

            // Indent is emitted only once per logical line — subsequent Append on the same
            // line stays adjacent to the previous content.
            builder.Append(" /*comment*/");

            Assert.That(builder.ToString(), Is.EqualTo($"package Foo {{{Environment.NewLine}    part p; /*comment*/"));

            // Null/empty Append is a no-op AND does NOT disarm the indent prefix.
            builder.AppendLine();
            builder.Append((string)null).Append(string.Empty).Append("attribute mass;");

            Assert.That(builder.ToString(), Is.EqualTo(
                $"package Foo {{{Environment.NewLine}    part p; /*comment*/{Environment.NewLine}    attribute mass;"));

            // Nested block — IncreaseIndent stacks; DecreaseIndent before closing brace lets
            // `}` align with its parent declaration.
            builder.AppendLine();
            builder.AppendLine("part def Engine {");
            builder.IncreaseIndent();
            builder.AppendLine("attribute power;");
            builder.DecreaseIndent();
            builder.AppendLine("}");
            builder.DecreaseIndent();
            builder.AppendLine("}");

            var expected =
                $"package Foo {{{Environment.NewLine}" +
                $"    part p; /*comment*/{Environment.NewLine}" +
                $"    attribute mass;{Environment.NewLine}" +
                $"    part def Engine {{{Environment.NewLine}" +
                $"        attribute power;{Environment.NewLine}" +
                $"    }}{Environment.NewLine}" +
                $"}}{Environment.NewLine}";

            using (Assert.EnterMultipleScope())
            {
                Assert.That(builder.IndentLevel, Is.EqualTo(0));
                Assert.That(builder.ToString(), Is.EqualTo(expected));
            }

            // Underflow guard — DecreaseIndent at level 0 is a no-op, never goes negative.
            builder.DecreaseIndent();
            builder.DecreaseIndent();

            Assert.That(builder.IndentLevel, Is.EqualTo(0));

            // Empty-line AppendLine() never produces trailing whitespace, even with indent armed.
            var emptyLineBuilder = new IndentedStringBuilder();
            emptyLineBuilder.IncreaseIndent();
            emptyLineBuilder.AppendLine();
            emptyLineBuilder.Append("x");

            Assert.That(emptyLineBuilder.ToString(), Is.EqualTo($"{Environment.NewLine}    x"));

            // Fluent chaining — every mutator returns the same instance.
            var chainBuilder = new IndentedStringBuilder();
            var chainResult = chainBuilder.Append("a").Append(' ').Append("b").AppendLine().AppendLine("c");

            using (Assert.EnterMultipleScope())
            {
                Assert.That(chainResult, Is.SameAs(chainBuilder));
                Assert.That(chainBuilder.ToString(), Is.EqualTo($"a b{Environment.NewLine}c{Environment.NewLine}"));
            }
        }
    }
}
