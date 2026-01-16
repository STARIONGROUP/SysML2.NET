// -------------------------------------------------------------------------------------------------
// <copyright file="ITextualRepresentation.cs" company="Starion Group S.A.">
//
//    Copyright (C) 2022-2025 Starion Group S.A.
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

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Core.POCO.Root.Annotations
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;

    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Decorators;

    /// <summary>
    /// A TextualRepresentation is an AnnotatingElement whose body represents the representedElement in a
    /// given language. The representedElement must be the owner of the TextualRepresentation. The named
    /// language can be a natural language, in which case the body is an informal representation, or an
    /// artificial language, in which case the body is expected to be a formal, machine-parsable
    /// representation.If the named language of a TextualRepresentation is machine-parsable, then the body
    /// text should be legal input text as defined for that language. The interpretation of the named
    /// language string shall be case insensitive. The following language names are defined to correspond to
    /// the given standard languages:<table border="1" cellpadding="1" cellspacing="1"
    /// width="498">	<thead>	</thead>	<tbody>		<tr>			<td style="text-align: center; width:
    /// 154px;">kerml</td>			<td style="width: 332px;">Kernel Modeling Language</td>		</tr>		<tr>			<td
    /// style="text-align: center; width: 154px;">ocl</td>			<td style="width: 332px;">Object Constraint
    /// Language</td>		</tr>		<tr>			<td style="text-align: center; width: 154px;">alf</td>			<td
    /// style="width: 332px;">Action Language for fUML</td>		</tr>	</tbody></table>Other specifications may
    /// define specific language strings, other than those shown above, to be used to indicate the use of
    /// languages from those specifications in KerML TextualRepresentation.If the language of a
    /// TextualRepresentation is &quot;kerml&quot;, then the body text shall be a legal representation of
    /// the representedElement in the KerML textual concrete syntax. A conforming tool can use such a
    /// TextualRepresentation Annotation to record the original KerML concrete syntax text from which an
    /// Element was parsed. In this case, it is a tool responsibility to ensure that the body of the
    /// TextualRepresentation remains correct (or the Annotation is removed) if the annotated Element
    /// changes other than by re-parsing the body text.An Element with a TextualRepresentation in a language
    /// other than KerML is essentially a semantically &quot;opaque&quot; Element specified in the other
    /// language. However, a conforming KerML tool may interpret such an element consistently with the
    /// specification of the named language.
    /// </summary>
    [Class(xmiId: "_19_0_2_12e503d9_1594152214531_455349_2448", isAbstract: false, isFinalSpecialization: false, isActive: false)]
    [GeneratedCode("SysML2.NET", "latest")]
    public partial interface ITextualRepresentation : IAnnotatingElement
    {
        /// <summary>
        /// The textual representation of the representedElement in the given language.
        /// </summary>
        [Property(xmiId: "_19_0_4_12e503d9_1647817353412_339800_421", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        string Body { get; set; }

        /// <summary>
        /// The natural or artifical language in which the body text is written.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1594152270061_927814_2479", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: false, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        string Language { get; set; }

        /// <summary>
        /// The Element that is represented by this TextualRepresentation.
        /// </summary>
        [Property(xmiId: "_19_0_2_12e503d9_1594154758494_414887_3389", aggregation: AggregationKind.None, lowerValue: 1, upperValue: 1, isOrdered: false, isReadOnly: false, isDerived: true, isDerivedUnion: false, isUnique: true, defaultValue: null)]
        [SubsettedProperty(propertyName: "_18_5_3_12e503d9_1543092869879_744477_17277")]
        [RedefinedProperty(propertyName: "_19_0_2_12e503d9_1594145755058_99428_86")]
        IElement representedElement { get; }

    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
