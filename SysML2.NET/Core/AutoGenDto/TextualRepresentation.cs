// -------------------------------------------------------------------------------------------------
// <copyright file="TextualRepresentation.cs" company="RHEA System S.A.">
//
//   Copyright 2022-2023 RHEA System S.A.
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

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Core.DTO
{
    using System;
    using System.Collections.Generic;

    using SysML2.NET.Core;

    /// <summary>
    /// A TextualRepresentation is an AnnotatingElement whose body represents the representedElement in a
    /// given language. The representedElement must be the owner of the TextualRepresentation. The named
    /// language can be a natural language, in which case the body is an informal representation, or an
    /// artifical language, in which case the body is expected to be a formal, machine-parsable
    /// representation.If the named language of a TextualRepresentation is machine-parsable, then the body
    /// text should be legal input text as defined for that language. The interpretation of the named
    /// language string shall be case insensitive. The following language names are defined to correspond to
    /// the given standard languages:<table border="1" cellpadding="1" cellspacing="1"
    /// width="498">	<thead>	</thead>	<tbody>		<tr>			<td style="text-align: center; width:
    /// 154px;">kerml</td>			<td style="width: 332px;">Kernel Modeling Language</td>		</tr>		<tr>			<td
    /// style="text-align: center; width: 154px;">ocl</td>			<td style="width: 332px;">Object Constraint
    /// Language</td>		</tr>		<tr>			<td style="text-align: center; width: 154px;">alf</td>			<td
    /// style="width: 332px;">Action Language for fUML</td>		</tr>	</tbody></table>Other specifications may
    /// define specific language strings, other than those shown in <mms-view-link
    /// mms-doc-id="_19_0_4_12e503d9_1655498859928_646482_53332"
    /// mms-element-id="MMS_1656305537944_6a3ca48e-424a-4a4d-8ce2-56df128ebabe"
    /// mms-pe-id="_hidden_MMS_1656305558930_8d3925ff-003f-4024-a594-14317550f480_pei">[cf:Standard Language
    /// Names.vlink]</mms-view-link>, to be used to indicate the use of languages from those specifications
    /// in KerML TextualRepresentations.If the language of a TextualRepresentation is &quot;kerml&quot;,
    /// then the body text shall be a legal representation of the representedElement in the KerML textual
    /// concrete syntax. A conforming tool can use such a TextualRepresentation Annotation to record the
    /// original KerML concrete syntax text from which an Element was parsed. In this case, it is a tool
    /// responsibility to ensure that the body of the TextualRepresentation remains correct (or the
    /// Annotation is removed) if the annotated Element changes other than by re-parsing the body text.An
    /// Element with a TextualRepresentation in a language other than KerML is essentially a semantically
    /// &quot;opaque&quot; Element specified in the other language. However, a conforming KerML tool may
    /// interpret such an element consistently with the specification of the named language.
    /// </summary>
    public partial class TextualRepresentation : ITextualRepresentation
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TextualRepresentation"/> class.
        /// </summary>
        public TextualRepresentation()
        {
            this.AliasIds = new List<string>();
            this.Annotation = new List<Guid>();
            this.IsImpliedIncluded = false;
            this.OwnedRelationship = new List<Guid>();
        }

        /// <summary>
        /// Gets or sets the unique identifier
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Various alternative identifiers for this Element. Generally, these will be set by tools.
        /// </summary>
        public List<string> AliasIds { get; set; }

        /// <summary>
        /// The Annotations that relate this AnnotatingElement to its annotatedElements.
        /// </summary>
        public List<Guid> Annotation { get; set; }

        /// <summary>
        /// The textual representation of the representedElement in the given language.
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// The globally unique identifier for this Element. This is intended to be set by tooling, and it must
        /// not change during the lifetime of the Element.
        /// </summary>
        public string ElementId { get; set; }

        /// <summary>
        /// Whether all necessary implied Relationships have been included in the ownedRelationships of this
        /// Element. This property may be true, even if there are not actually any ownedRelationships with
        /// isImplied = true, meaning that no such Relationships are actually implied for this Element. However,
        /// if it is false, then ownedRelationships may not contain any implied Relationships. That is, either
        /// all required implied Relationships must be included, or none of them.
        /// </summary>
        public bool IsImpliedIncluded { get; set; }

        /// <summary>
        /// The natural or artifical language in which the body text is written.
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// The primary name of this Element.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The Relationships for which this Element is the owningRelatedElement.
        /// </summary>
        public List<Guid> OwnedRelationship { get; set; }

        /// <summary>
        /// The Relationship for which this Element is an ownedRelatedElement, if any.
        /// </summary>
        public Guid? OwningRelationship { get; set; }

        /// <summary>
        /// An optional alternative name for the Element that is intended to be shorter or in some way more
        /// succinct than its primary name. It may act as a modeler-specified identifier for the Element, though
        /// it is then the responsibility of the modeler to maintain the uniqueness of this identifier within a
        /// model or relative to some other context.
        /// </summary>
        public string ShortName { get; set; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
