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
    /// Test fixture for <see cref="IndentedStringBuilder"/>: verifies indent emission,
    /// IncreaseIndent/DecreaseIndent nesting and underflow guard, leading-whitespace-at-line
    /// -start suppression, consecutive-space collapse, and tight-left / tight-both terminal
    /// stripping aligned with the SST tutorial conventions (no space before <c>;</c>,
    /// <c>,</c>, <c>)</c>, <c>]</c>; no space around <c>.</c>, <c>::</c>, <c>..</c>).
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

            // Underflow guard — DecreaseIndent at level 0 is a no-op, never goes negative.
            var underflowBuilder = new IndentedStringBuilder();
            underflowBuilder.DecreaseIndent();
            underflowBuilder.DecreaseIndent();

            Assert.That(underflowBuilder.IndentLevel, Is.EqualTo(0));

            // Empty-line AppendLine() never produces trailing whitespace, even with indent armed.
            var emptyLineBuilder = new IndentedStringBuilder();
            emptyLineBuilder.IncreaseIndent();
            emptyLineBuilder.AppendLine();
            emptyLineBuilder.Append("x");

            Assert.That(emptyLineBuilder.ToString(), Is.EqualTo($"{Environment.NewLine}    x"));

            // Leading whitespace at line start is suppressed — the indent has already been
            // (or will be) emitted, so the leading space of " standard" is dropped.
            var leadingBuilder = new IndentedStringBuilder();
            leadingBuilder.Append(" standard ").Append(" library ").Append("package");

            Assert.That(leadingBuilder.ToString(), Is.EqualTo("standard library package"));

            // Consecutive ASCII spaces are collapsed across calls.
            var collapseBuilder = new IndentedStringBuilder();
            collapseBuilder.Append("Array ").Append(' ').Append('{');

            Assert.That(collapseBuilder.ToString(), Is.EqualTo("Array {"));

            // Tight-left punctuation strips the preceding trailing space.
            // ";", ",", ")", "]" emit with no space before, single space (from payload) after.
            var tightLeftBuilder = new IndentedStringBuilder();
            tightLeftBuilder
                .Append("Collections::* ")
                .AppendLine(";")
                .Append("foo")
                .Append(' ')
                .Append(", ")
                .Append("bar")
                .Append(' ')
                .Append("]");

            Assert.That(tightLeftBuilder.ToString(), Is.EqualTo($"Collections::*;{Environment.NewLine}foo, bar]"));

            // `[` is tight-left: a preceding trailing space is stripped so multiplicity /
            // indexer brackets attach directly to the previous identifier
            // (`Number[1..*]`, not `Number [1..*]`).
            var multiplicitySuffixBuilder = new IndentedStringBuilder();
            multiplicitySuffixBuilder
                .Append("Number ")
                .Append("[")
                .Append("1")
                .Append("]");

            Assert.That(multiplicitySuffixBuilder.ToString(), Is.EqualTo("Number[1]"));

            // `#` is tight-left: index / select expressions attach directly to the
            // preceding identifier (`frontWheel#(1)`, not `frontWheel #(1)`).
            var indexExpressionBuilder = new IndentedStringBuilder();
            indexExpressionBuilder
                .Append("frontWheel ")
                .Append("#")
                .Append("(")
                .Append("1")
                .Append(")");

            Assert.That(indexExpressionBuilder.ToString(), Is.EqualTo("frontWheel#(1)"));

            // AppendLine strips trailing space from the buffer before emitting the line
            // terminator: a `doc ` keyword followed by an inner AppendLine() blank line
            // produces `doc<newline>`, not `doc<space><newline>`.
            var trailingSpaceBuilder = new IndentedStringBuilder();
            trailingSpaceBuilder.Append("doc ").AppendLine();

            Assert.That(trailingSpaceBuilder.ToString(), Is.EqualTo($"doc{Environment.NewLine}"));

            // Tight-both terminals also suppress a leading space in the IMMEDIATELY
            // following Append payload. Reproduces the multiplicity codegen pattern that
            // emits `Append(".. "); Append(' ');` for the optional `..` separator — the
            // wrapper must collapse the post-`..` space so `[1..*]` is not `[1.. *]`.
            var tightBothFollowedBySpaceBuilder = new IndentedStringBuilder();
            tightBothFollowedBySpaceBuilder
                .Append("[")
                .Append("1")
                .Append(".. ")
                .Append(' ')
                .Append("*")
                .Append("]");

            Assert.That(tightBothFollowedBySpaceBuilder.ToString(), Is.EqualTo("[1..*]"));

            // Tight-right opener characters ("[", "(", "~", "#") suppress any leading space
            // in the next payload — codegen often emits e.g. " kg" via the leading-space
            // helper after "[", which would otherwise produce "[ kg]".
            var tightRightBuilder = new IndentedStringBuilder();
            tightRightBuilder
                .Append("2000")
                .Append("[")
                .Append(" kg")
                .Append("]")
                .Append(' ')
                .Append("foo")
                .Append("(")
                .Append(" x")
                .Append(",")
                .Append(' ')
                .Append("y")
                .Append(")")
                .Append(' ')
                .Append("~")
                .Append(" negative")
                .Append(' ')
                .Append("#")
                .Append(" annotation");

            Assert.That(tightRightBuilder.ToString(), Is.EqualTo("2000[kg] foo(x, y) ~negative#annotation"));

            // Tight-both punctuation strips the preceding trailing space AND emits with no
            // trailing space: ".", "::", "..".
            // Codegen often emits these with a trailing space in the literal (e.g. ":: ");
            // the wrapper detects the core token and strips both sides.
            var tightBothBuilder = new IndentedStringBuilder();
            tightBothBuilder
                .Append("mRef ")
                .Append(".")
                .Append("dimensions")
                .Append(' ')
                .Append(":: ")
                .Append("Nested")
                .Append(' ')
                .Append(".. ")
                .Append("end");

            Assert.That(tightBothBuilder.ToString(), Is.EqualTo("mRef.dimensions::Nested..end"));

            // Mixed integration scenario — nested block + tight tokens + indentation,
            // exercising the full pipeline together.
            var integrationBuilder = new IndentedStringBuilder();
            integrationBuilder.Append("package ").Append("Foo").Append(' ').AppendLine("{");
            integrationBuilder.IncreaseIndent();
            integrationBuilder.Append(" private ").Append("import ").Append("Collections").Append(":: ").Append("*").AppendLine(";");
            integrationBuilder.Append("attribute ").Append("mass").Append(":").Append(' ').Append("Real ").AppendLine(";");
            integrationBuilder.DecreaseIndent();
            integrationBuilder.AppendLine("}");

            var integrationExpected =
                $"package Foo {{{Environment.NewLine}" +
                $"    private import Collections::*;{Environment.NewLine}" +
                $"    attribute mass: Real;{Environment.NewLine}" +
                $"}}{Environment.NewLine}";

            using (Assert.EnterMultipleScope())
            {
                Assert.That(integrationBuilder.IndentLevel, Is.EqualTo(0));
                Assert.That(integrationBuilder.ToString(), Is.EqualTo(integrationExpected));
                Assert.That(integrationBuilder.ToString(), Does.Not.Contain(" ;")); // no space before ;
                Assert.That(integrationBuilder.ToString(), Does.Not.Contain(" ::")); // no space before ::
                Assert.That(integrationBuilder.ToString(), Does.Not.Contain(":: ")); // no space after ::
            }

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
