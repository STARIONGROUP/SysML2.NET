// ------------------------------------------------------------------------------------------------
// <copyright file="MembershipTextualNotationBuilder.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Serializer.TextualNotation.Writers
{
    using SysML2.NET.Core.POCO.Root.Namespaces;

    /// <summary>
    /// Hand-coded part of the <see cref="MembershipTextualNotationBuilder"/>
    /// </summary>
    public static partial class MembershipTextualNotationBuilder
    {
        /// <summary>
        /// Builds the Textual Notation string for the <c>InitialNodeMember</c> rule from a plain
        /// <see cref="IMembership"/>.
        /// <para><c>InitialNodeMember : FeatureMembership = MemberPrefix 'first'
        /// memberFeature = [QualifiedName] RelationshipBody</c></para>
        /// <para>The rule declares <c>FeatureMembership</c> as its target, but that metaclass cannot
        /// carry this construct: it specializes <c>OwningMembership</c> and therefore OWNS its member,
        /// while <c>[QualifiedName]</c> is a CROSS-REFERENCE to an element owned elsewhere —
        /// <c>first start;</c> names <c>Actions::Action::start</c> in the Systems Library. A plain
        /// <see cref="IMembership"/> holding <see cref="IMembership.MemberElement"/> is the only
        /// metamodel-valid encoding, and it is what the pilot exports. (<c>memberFeature</c> is also not
        /// a property of any metaclass in the abstract syntax; the rule names one that does not exist,
        /// still true in release 2026-05.)</para>
        /// <para>The generated <c>BuildInitialNodeMember</c> overload is typed to the rule's declared
        /// <c>IFeatureMembership</c> target and so cannot accept the exported element, which is why this
        /// wider entry point exists and why it re-emits the surrounding <c>MemberPrefix</c> /
        /// <c>'first'</c> / <c>RelationshipBody</c> tokens itself.</para>
        /// </summary>
        /// <param name="poco">The <see cref="IMembership"/> from which the rule should be built</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext"/> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="IndentedStringBuilder"/> that contains the entire textual notation</param>
        internal static void BuildInitialNodeMemberFromReference(IMembership poco, TextualNotationWriterContext writerContext, IndentedStringBuilder stringBuilder)
        {
            BuildMemberPrefix(poco, writerContext, stringBuilder);
            stringBuilder.Append("first ");
            AppendInitialNodeMemberFeature(poco, writerContext, stringBuilder);
            RelationshipTextualNotationBuilder.BuildRelationshipBody(poco, writerContext, stringBuilder);
        }

        /// <summary>
        /// Appends the <c>memberFeature = [QualifiedName]</c> part of the <c>InitialNodeMember</c> rule —
        /// the referenced element's shortest name that resolves from this reference site.
        /// </summary>
        /// <param name="poco">The <see cref="IMembership"/> whose <see cref="IMembership.MemberElement"/> is named</param>
        /// <param name="writerContext">The <see cref="TextualNotationWriterContext"/> providing the serialization context for the current <paramref name="poco"/></param>
        /// <param name="stringBuilder">The <see cref="IndentedStringBuilder"/> that contains the entire textual notation</param>
        internal static void AppendInitialNodeMemberFeature(IMembership poco, TextualNotationWriterContext writerContext, IndentedStringBuilder stringBuilder)
        {
            if (poco.MemberElement != null)
            {
                SharedTextualNotationBuilder.AppendQualifiedName(stringBuilder, poco.MemberElement, writerContext, poco);
            }
        }
    }
}
