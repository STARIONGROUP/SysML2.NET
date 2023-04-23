// -------------------------------------------------------------------------------------------------
// <copyright file="ReaderTestFixture.cs" company="RHEA System S.A.">
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

namespace SysML2.NET.Serializer.Dictionary.Tests
{
    using System;
    using System.Collections.Generic;
    
    using NUnit.Framework;
    
    using SysML2.NET.Core;
    using SysML2.NET.Core.DTO;
    using SysML2.NET.Serializer.Dictionary;

    /// <summary>
    /// Suite of tests for the <see cref="Reader"/> class
    /// </summary>
    [TestFixture]
    public class ReaderTestFixture
    {
        private IReader reader;

        private Dictionary<string, object> dictionary;

        [SetUp]
        public void SetUp()
        {
            this.reader = new Reader();
        }

        [Test]
        public void Verify_that_when_dictionary_is_read_instance_of_IAnnotatingElement_is_returned()
        {
            // READ COMPLEX - iteration 1

            this.dictionary = new Dictionary<string, object>
            {
                { "@type", "AnnotatingElement" },
                { "@id", Guid.Parse("0b192c18-afa2-44b6-8de2-86e5e6ffa09e") },
                { "aliasIds", new List<string> { "alias_1", "alias_2" } },
                { "annotation", new List<Guid> { Guid.Parse("7f8c06c6-93cb-4193-9a0c-c6ca4dc1eec1") } },
                { "declaredName", "the name" },
                { "declaredShortName", "the shortName" },
                { "elementId", "element id" },
                { "isImpliedIncluded", true },
                { "ownedRelationship", new List<Guid> { Guid.Parse("9006ff06-43fe-4a4e-a4bc-402e82f84dde") } },
                { "owningRelationship", Guid.Parse("fe6d7f0c-6e7b-4ce9-acbe-25d2537f08d9") }
            };

            var annotatingElement = reader.Read(this.dictionary, DictionaryKind.Complex) as IAnnotatingElement;

            Assert.That(annotatingElement.Id, Is.EqualTo(Guid.Parse("0b192c18-afa2-44b6-8de2-86e5e6ffa09e")));
            Assert.That(annotatingElement.AliasIds, Is.EqualTo(new List<string> { "alias_1", "alias_2" }));
            Assert.That(annotatingElement.DeclaredName, Is.EqualTo("the name"));
            Assert.That(annotatingElement.DeclaredShortName, Is.EqualTo("the shortName"));
            Assert.That(annotatingElement.ElementId, Is.EqualTo("element id"));
            Assert.That(annotatingElement.IsImpliedIncluded, Is.True);
            Assert.That(annotatingElement.OwnedRelationship, Is.EqualTo(new List<Guid> { Guid.Parse("9006ff06-43fe-4a4e-a4bc-402e82f84dde") }));
            Assert.That(annotatingElement.OwningRelationship, Is.EqualTo(Guid.Parse("fe6d7f0c-6e7b-4ce9-acbe-25d2537f08d9")));
            
            // READ COMPLEX - iteration 2

            this.dictionary = new Dictionary<string, object>
            {
                { "@type", "AnnotatingElement" },
                { "@id", Guid.Parse("0b192c18-afa2-44b6-8de2-86e5e6ffa09e") },
                { "aliasIds", new List<string>() },
                { "annotation", new List<Guid>() },
                { "declaredName", null },
                { "declaredShortName", null },
                { "elementId", "element id" },
                { "isImpliedIncluded", true },
                { "ownedRelationship", new List<Guid>() },
                { "owningRelationship", null }
            };

            annotatingElement = reader.Read(this.dictionary, DictionaryKind.Complex) as IAnnotatingElement;

            Assert.That(annotatingElement.Id, Is.EqualTo(Guid.Parse("0b192c18-afa2-44b6-8de2-86e5e6ffa09e")));
            Assert.That(annotatingElement.AliasIds, Is.EqualTo(new List<string>()));
            Assert.That(annotatingElement.DeclaredName, Is.Null);
            Assert.That(annotatingElement.DeclaredShortName, Is.Null);
            Assert.That(annotatingElement.ElementId, Is.EqualTo("element id"));
            Assert.That(annotatingElement.IsImpliedIncluded, Is.True);
            Assert.That(annotatingElement.OwnedRelationship, Is.EqualTo(new List<Guid>()));
            Assert.That(annotatingElement.OwningRelationship, Is.Null);
            
            // READ SIMPLIFIED - iteration 1

            this.dictionary = new Dictionary<string, object>
            {
                { "@type", "AnnotatingElement" },
                { "@id", "0b192c18-afa2-44b6-8de2-86e5e6ffa09e" },
                { "aliasIds", new List<string> { "alias_1", "alias_2" } },
                { "annotation", new List<string> { "7f8c06c6-93cb-4193-9a0c-c6ca4dc1eec1" } },
                { "declaredName", "the name" },
                { "declaredShortName", "the shortName" },
                { "elementId", "element id" },
                { "isImpliedIncluded", true },
                { "ownedRelationship", new List<string> { "9006ff06-43fe-4a4e-a4bc-402e82f84dde" } },
                { "owningRelationship", "fe6d7f0c-6e7b-4ce9-acbe-25d2537f08d9" }
            };

            annotatingElement = reader.Read(this.dictionary, DictionaryKind.Simplified) as IAnnotatingElement;

            Assert.That(annotatingElement.Id, Is.EqualTo(Guid.Parse("0b192c18-afa2-44b6-8de2-86e5e6ffa09e")));
            Assert.That(annotatingElement.AliasIds, Is.EqualTo(new List<string> { "alias_1", "alias_2" }));
            Assert.That(annotatingElement.ElementId, Is.EqualTo("element id"));
            Assert.That(annotatingElement.IsImpliedIncluded, Is.True);
            Assert.That(annotatingElement.DeclaredName, Is.EqualTo("the name"));
            Assert.That(annotatingElement.OwnedRelationship, Is.EqualTo(new List<Guid> { Guid.Parse("9006ff06-43fe-4a4e-a4bc-402e82f84dde") }));
            Assert.That(annotatingElement.OwningRelationship, Is.EqualTo(Guid.Parse("fe6d7f0c-6e7b-4ce9-acbe-25d2537f08d9")));
            Assert.That(annotatingElement.DeclaredShortName, Is.EqualTo("the shortName"));

            // READ SIMPLIFIED - iteration 2

            this.dictionary = new Dictionary<string, object>
            {
                { "@type", "AnnotatingElement" },
                { "@id", Guid.Parse("0b192c18-afa2-44b6-8de2-86e5e6ffa09e") },
                { "aliasIds", new List<string> () },
                { "annotation", new List<string>() },
                { "elementId", "element id" },
                { "isImpliedIncluded", true },
                { "declaredName", null },
                { "ownedRelationship", new List<string>() },
                { "owningRelationship", null },
                { "declaredShortName", null }
            };

            annotatingElement = reader.Read(this.dictionary, DictionaryKind.Simplified) as IAnnotatingElement;

            Assert.That(annotatingElement.Id, Is.EqualTo(Guid.Parse("0b192c18-afa2-44b6-8de2-86e5e6ffa09e")));
            Assert.That(annotatingElement.AliasIds, Is.EqualTo(new List<string>()));
            Assert.That(annotatingElement.ElementId, Is.EqualTo("element id"));
            Assert.That(annotatingElement.IsImpliedIncluded, Is.True);
            Assert.That(annotatingElement.DeclaredName, Is.Null);
            Assert.That(annotatingElement.OwnedRelationship, Is.EqualTo(new List<Guid>()));
            Assert.That(annotatingElement.OwningRelationship, Is.Null);
            Assert.That(annotatingElement.DeclaredShortName, Is.Null);
        }

        [Test]
        public void Verify_that_when_dictionary_is_read_instance_of_IAnnotation_is_returned()
        {
            // READ COMPLEX - iteration 1

            this.dictionary = new Dictionary<string, object>
            {
                { "@type", "Annotation" },
                { "@id", Guid.Parse("0b192c18-afa2-44b6-8de2-86e5e6ffa09e") },
                { "aliasIds", new List<string> { "alias_1", "alias_2" } },
                { "annotatedElement", Guid.Parse("bb66c812-3408-4166-99d9-402b798093e1")},
                { "annotatingElement", Guid.Parse("e6d3d799-fd33-49d1-a1c9-ceae0548de18")},
                { "elementId", "element id" },
                { "isImplied", true },
                { "isImpliedIncluded", true},
                { "declaredName", "the name" },
                { "ownedRelatedElement", new List<Guid> { Guid.Parse("c18450a9-fd96-4dd6-83c4-de5e6e1bd2f7") } },
                { "ownedRelationship", new List<Guid> { Guid.Parse("9006ff06-43fe-4a4e-a4bc-402e82f84dde") } },
                { "owningRelatedElement", Guid.Parse("df9670d5-36a7-4128-aa96-928432a80e42") },
                { "owningRelationship", Guid.Parse("fe6d7f0c-6e7b-4ce9-acbe-25d2537f08d9") },
                { "declaredShortName", "the shortName" },
                { "source", new List<Guid> { Guid.Parse("8ba10c06-4c00-4748-ae60-a724ef773e29") }},
                { "target", new List<Guid> { Guid.Parse("38c03271-5de8-4947-93db-20993d7a9dc2") }}
            };

            var annotatingElement = reader.Read(this.dictionary, DictionaryKind.Complex) as IAnnotation;

            Assert.That(annotatingElement.Id, Is.EqualTo(Guid.Parse("0b192c18-afa2-44b6-8de2-86e5e6ffa09e")));
            Assert.That(annotatingElement.AliasIds, Is.EqualTo(new List<string> { "alias_1", "alias_2" }));
            Assert.That(annotatingElement.ElementId, Is.EqualTo("element id"));
            Assert.That(annotatingElement.IsImpliedIncluded, Is.True);
            Assert.That(annotatingElement.DeclaredName, Is.EqualTo("the name"));
            Assert.That(annotatingElement.OwnedRelationship, Is.EqualTo(new List<Guid> { Guid.Parse("9006ff06-43fe-4a4e-a4bc-402e82f84dde") }));
            Assert.That(annotatingElement.OwningRelationship, Is.EqualTo(Guid.Parse("fe6d7f0c-6e7b-4ce9-acbe-25d2537f08d9")));
            Assert.That(annotatingElement.DeclaredShortName, Is.EqualTo("the shortName"));

            // READ COMPLEX - iteration 2

            this.dictionary = new Dictionary<string, object>
            {
                { "@type", "Annotation" },
                { "@id", Guid.Parse("0b192c18-afa2-44b6-8de2-86e5e6ffa09e") },
                { "aliasIds", new List<string> () },
                { "annotatedElement", Guid.Parse("bb66c812-3408-4166-99d9-402b798093e1")},
                { "annotatingElement", Guid.Parse("e6d3d799-fd33-49d1-a1c9-ceae0548de18")},
                { "elementId", "element id" },
                { "isImplied", true },
                { "isImpliedIncluded", true},
                { "declaredName", null },
                { "ownedRelatedElement", new List<Guid> () },
                { "ownedRelationship", new List<Guid> () },
                { "owningRelatedElement", null},
                { "owningRelationship", null },
                { "declaredShortName", null },
                { "source", new List<Guid> ()},
                { "target", new List<Guid> ()}
            };

            annotatingElement = reader.Read(this.dictionary, DictionaryKind.Complex) as IAnnotation;

            Assert.That(annotatingElement.Id, Is.EqualTo(Guid.Parse("0b192c18-afa2-44b6-8de2-86e5e6ffa09e")));
            Assert.That(annotatingElement.AliasIds, Is.EqualTo(new List<string>()));
            Assert.That(annotatingElement.ElementId, Is.EqualTo("element id"));
            Assert.That(annotatingElement.IsImpliedIncluded, Is.True);
            Assert.That(annotatingElement.DeclaredName, Is.Null);
            Assert.That(annotatingElement.OwnedRelationship, Is.EqualTo(new List<Guid>()));
            Assert.That(annotatingElement.OwningRelationship, Is.Null);
            Assert.That(annotatingElement.DeclaredShortName, Is.Null);

            // READ SIMPLIFIED - iteration 1

            this.dictionary = new Dictionary<string, object>
            {
                { "@type", "Annotation" },
                { "@id", "0b192c18-afa2-44b6-8de2-86e5e6ffa09e" },
                { "aliasIds", new List<string> { "alias_1", "alias_2" } },
                { "annotatedElement", "bb66c812-3408-4166-99d9-402b798093e1"},
                { "annotatingElement", "e6d3d799-fd33-49d1-a1c9-ceae0548de18"},
                { "elementId", "element id" },
                { "isImplied", true },
                { "isImpliedIncluded", true},
                { "declaredName", "the name" },
                { "ownedRelatedElement", new List<string> { "c18450a9-fd96-4dd6-83c4-de5e6e1bd2f7" } },
                { "ownedRelationship", new List<string> { "9006ff06-43fe-4a4e-a4bc-402e82f84dde" } },
                { "owningRelatedElement", "df9670d5-36a7-4128-aa96-928432a80e42" },
                { "owningRelationship", "fe6d7f0c-6e7b-4ce9-acbe-25d2537f08d9" },
                { "declaredShortName", "the shortName" },
                { "source", new List<string> { "8ba10c06-4c00-4748-ae60-a724ef773e29" }},
                { "target", new List<string> { "38c03271-5de8-4947-93db-20993d7a9dc2" }}
            };

            annotatingElement = reader.Read(this.dictionary, DictionaryKind.Simplified) as IAnnotation;

            Assert.That(annotatingElement.Id, Is.EqualTo(Guid.Parse("0b192c18-afa2-44b6-8de2-86e5e6ffa09e")));
            Assert.That(annotatingElement.AliasIds, Is.EqualTo(new List<string> { "alias_1", "alias_2" }));
            Assert.That(annotatingElement.ElementId, Is.EqualTo("element id"));
            Assert.That(annotatingElement.IsImpliedIncluded, Is.True);
            Assert.That(annotatingElement.DeclaredName, Is.EqualTo("the name"));
            Assert.That(annotatingElement.OwnedRelationship, Is.EqualTo(new List<Guid> { Guid.Parse("9006ff06-43fe-4a4e-a4bc-402e82f84dde") }));
            Assert.That(annotatingElement.OwningRelationship, Is.EqualTo(Guid.Parse("fe6d7f0c-6e7b-4ce9-acbe-25d2537f08d9")));
            Assert.That(annotatingElement.DeclaredShortName, Is.EqualTo("the shortName"));

            // READ SIMPLIFIED - iteration 2

            this.dictionary = new Dictionary<string, object>
            {
                { "@type", "Annotation" },
                { "@id", "0b192c18-afa2-44b6-8de2-86e5e6ffa09e" },
                { "aliasIds", new List<string> () },
                { "annotatedElement", "bb66c812-3408-4166-99d9-402b798093e1"},
                { "annotatingElement", "e6d3d799-fd33-49d1-a1c9-ceae0548de18"},
                { "elementId", "element id" },
                { "isImplied", true },
                { "isImpliedIncluded", true},
                { "declaredName", null },
                { "ownedRelatedElement", new List<string> () },
                { "ownedRelationship", new List<string> () },
                { "owningRelatedElement", null},
                { "owningRelationship", null },
                { "declaredShortName", null },
                { "source", new List<string> ()},
                { "target", new List<string> ()}
            };

            annotatingElement = reader.Read(this.dictionary, DictionaryKind.Simplified) as IAnnotation;

            Assert.That(annotatingElement.Id, Is.EqualTo(Guid.Parse("0b192c18-afa2-44b6-8de2-86e5e6ffa09e")));
            Assert.That(annotatingElement.AliasIds, Is.EqualTo(new List<string>()));
            Assert.That(annotatingElement.ElementId, Is.EqualTo("element id"));
            Assert.That(annotatingElement.IsImpliedIncluded, Is.True);
            Assert.That(annotatingElement.DeclaredName, Is.Null);
            Assert.That(annotatingElement.OwnedRelationship, Is.EqualTo(new List<Guid>()));
            Assert.That(annotatingElement.OwningRelationship, Is.Null);
            Assert.That(annotatingElement.DeclaredShortName, Is.Null);
        }

        [Test]
        public void Verify_that_When_dictionary_is_read_instance_of_IComment_is_returned()
        {
            // READ COMPLEX - iteration 1

            this.dictionary = new Dictionary<string, object>
            {
                { "@type", "Comment" },
                { "@id", Guid.Parse("0b192c18-afa2-44b6-8de2-86e5e6ffa09e") },
                { "aliasIds", new List<string> { "alias_1", "alias_2" } },
                { "annotation", new List<Guid> { Guid.Parse("c18450a9-fd96-4dd6-83c4-de5e6e1bd2f7") } },
                { "body", "the body" },
                { "elementId", "element id" },
                { "isImpliedIncluded", true },
                { "locale", "the locale" },
                { "declaredName", "the name" },
                { "ownedRelationship", new List<Guid> { Guid.Parse("9006ff06-43fe-4a4e-a4bc-402e82f84dde") } },
                { "owningRelationship", Guid.Parse("fe6d7f0c-6e7b-4ce9-acbe-25d2537f08d9") },
                { "declaredShortName", "the shortName" },
            };

            var comment = reader.Read(this.dictionary, DictionaryKind.Complex) as IComment;

            Assert.That(comment.Id, Is.EqualTo(Guid.Parse("0b192c18-afa2-44b6-8de2-86e5e6ffa09e")));
            Assert.That(comment.AliasIds, Is.EqualTo(new List<string> { "alias_1", "alias_2" }));
            Assert.That(comment.Annotation, Is.EqualTo(new List<Guid> { Guid.Parse("c18450a9-fd96-4dd6-83c4-de5e6e1bd2f7") }));
            Assert.That(comment.Body, Is.EqualTo("the body"));
            Assert.That(comment.ElementId, Is.EqualTo("element id"));
            Assert.That(comment.IsImpliedIncluded, Is.True);
            Assert.That(comment.Locale, Is.EqualTo("the locale"));
            Assert.That(comment.DeclaredName, Is.EqualTo("the name"));
            Assert.That(comment.OwnedRelationship, Is.EqualTo(new List<Guid> { Guid.Parse("9006ff06-43fe-4a4e-a4bc-402e82f84dde") }));
            Assert.That(comment.OwningRelationship, Is.EqualTo(Guid.Parse("fe6d7f0c-6e7b-4ce9-acbe-25d2537f08d9")));
            Assert.That(comment.DeclaredShortName, Is.EqualTo("the shortName"));

            // READ COMPLEX - iteration 2

            this.dictionary = new Dictionary<string, object>
            {
                { "@type", "Comment" },
                { "@id", Guid.Parse("0b192c18-afa2-44b6-8de2-86e5e6ffa09e") },
                { "aliasIds", new List<string> () },
                { "annotation", new List<Guid> () },
                { "body", "the body" },
                { "elementId", "element id" },
                { "isImpliedIncluded", true },
                { "locale", null },
                { "declaredName", null },
                { "ownedRelationship", new List<Guid> () },
                { "owningRelationship", null },
                { "declaredShortName", null },
            };

            comment = reader.Read(this.dictionary, DictionaryKind.Complex) as IComment;

            Assert.That(comment.Id, Is.EqualTo(Guid.Parse("0b192c18-afa2-44b6-8de2-86e5e6ffa09e")));
            Assert.That(comment.AliasIds, Is.EqualTo(new List<string>() ));
            Assert.That(comment.Annotation, Is.EqualTo(new List<Guid>()));
            Assert.That(comment.Body, Is.EqualTo("the body"));
            Assert.That(comment.ElementId, Is.EqualTo("element id"));
            Assert.That(comment.IsImpliedIncluded, Is.True);
            Assert.That(comment.Locale, Is.Null);
            Assert.That(comment.DeclaredName, Is.Null);
            Assert.That(comment.OwnedRelationship, Is.EqualTo(new List<Guid> ()));
            Assert.That(comment.OwningRelationship, Is.Null );
            Assert.That(comment.DeclaredShortName, Is.Null);

            // READ SIMPLIFIED - iteration 1

            this.dictionary = new Dictionary<string, object>
            {
                { "@type", "Comment" },
                { "@id", "0b192c18-afa2-44b6-8de2-86e5e6ffa09e" },
                { "aliasIds", new List<string> { "alias_1", "alias_2" } },
                { "annotation", new List<string> { "c18450a9-fd96-4dd6-83c4-de5e6e1bd2f7" } },
                { "body", "the body" },
                { "elementId", "element id" },
                { "isImpliedIncluded", true },
                { "locale", "the locale" },
                { "declaredName", "the name" },
                { "ownedRelationship", new List<string> { "9006ff06-43fe-4a4e-a4bc-402e82f84dde" } },
                { "owningRelationship", "fe6d7f0c-6e7b-4ce9-acbe-25d2537f08d9" },
                { "declaredShortName", "the shortName" },
            };

            comment = reader.Read(this.dictionary, DictionaryKind.Simplified) as IComment;

            Assert.That(comment.Id, Is.EqualTo(Guid.Parse("0b192c18-afa2-44b6-8de2-86e5e6ffa09e")));
            Assert.That(comment.AliasIds, Is.EqualTo(new List<string> { "alias_1", "alias_2" }));
            Assert.That(comment.Annotation, Is.EqualTo(new List<Guid> { Guid.Parse("c18450a9-fd96-4dd6-83c4-de5e6e1bd2f7") }));
            Assert.That(comment.Body, Is.EqualTo("the body"));
            Assert.That(comment.ElementId, Is.EqualTo("element id"));
            Assert.That(comment.IsImpliedIncluded, Is.True);
            Assert.That(comment.Locale, Is.EqualTo("the locale"));
            Assert.That(comment.DeclaredName, Is.EqualTo("the name"));
            Assert.That(comment.OwnedRelationship, Is.EqualTo(new List<Guid> { Guid.Parse("9006ff06-43fe-4a4e-a4bc-402e82f84dde") }));
            Assert.That(comment.OwningRelationship, Is.EqualTo(Guid.Parse("fe6d7f0c-6e7b-4ce9-acbe-25d2537f08d9")));
            Assert.That(comment.DeclaredShortName, Is.EqualTo("the shortName"));

            // READ SIMPLIFIED - iteration 2

            this.dictionary = new Dictionary<string, object>
            {
                { "@type", "Comment" },
                { "@id", "0b192c18-afa2-44b6-8de2-86e5e6ffa09e" },
                { "aliasIds", new List<string> () },
                { "annotation", new List<string> () },
                { "body", "the body" },
                { "elementId", "element id" },
                { "isImpliedIncluded", true },
                { "locale", null },
                { "declaredName", null },
                { "ownedRelationship", new List<string> () },
                { "owningRelationship", null },
                { "declaredShortName", null },
            };

            comment = reader.Read(this.dictionary, DictionaryKind.Simplified) as IComment;

            Assert.That(comment.Id, Is.EqualTo(Guid.Parse("0b192c18-afa2-44b6-8de2-86e5e6ffa09e")));
            Assert.That(comment.AliasIds, Is.EqualTo(new List<string>()));
            Assert.That(comment.Annotation, Is.EqualTo(new List<Guid>()));
            Assert.That(comment.Body, Is.EqualTo("the body"));
            Assert.That(comment.ElementId, Is.EqualTo("element id"));
            Assert.That(comment.IsImpliedIncluded, Is.True);
            Assert.That(comment.Locale, Is.Null);
            Assert.That(comment.DeclaredName, Is.Null);
            Assert.That(comment.OwnedRelationship, Is.EqualTo(new List<Guid>()));
            Assert.That(comment.OwningRelationship, Is.Null);
            Assert.That(comment.DeclaredShortName, Is.Null);
        }

        [Test]
        public void Verify_that_When_dictionary_is_read_instance_of_ILiteralInteger_is_returned()
        {
            // READ COMPLEX - iteration 1

            this.dictionary = new Dictionary<string, object>
            {
                { "@type", "LiteralInteger" },
                { "@id", Guid.Parse("0b192c18-afa2-44b6-8de2-86e5e6ffa09e") },
                { "aliasIds", new List<string> { "alias_1", "alias_2" } },
                { "direction", FeatureDirectionKind.In },
                { "elementId", "element id" },
                { "isAbstract", true },
                { "isComposite", true },
                { "isDerived", true },
                { "isEnd", true },
                { "isImpliedIncluded", true },
                { "isOrdered", true },
                { "isPortion", true },
                { "isReadOnly", true },
                { "isSufficient", true },
                { "isUnique", true },
                { "declaredName", "the name" },
                { "ownedRelationship", new List<Guid> { Guid.Parse("9006ff06-43fe-4a4e-a4bc-402e82f84dde") } },
                { "owningRelationship", Guid.Parse("fe6d7f0c-6e7b-4ce9-acbe-25d2537f08d9") },
                { "declaredShortName", "the shortName" },
                { "value", 123 },
            };

            var literalInteger = reader.Read(this.dictionary, DictionaryKind.Complex) as ILiteralInteger;

            Assert.That(literalInteger.Id, Is.EqualTo(Guid.Parse("0b192c18-afa2-44b6-8de2-86e5e6ffa09e")));
            Assert.That(literalInteger.AliasIds, Is.EqualTo(new List<string> { "alias_1", "alias_2" }));
            Assert.That(literalInteger.ElementId, Is.EqualTo("element id"));
            Assert.That(literalInteger.IsImpliedIncluded, Is.True);
            Assert.That(literalInteger.DeclaredName, Is.EqualTo("the name"));
            Assert.That(literalInteger.OwnedRelationship, Is.EqualTo(new List<Guid> { Guid.Parse("9006ff06-43fe-4a4e-a4bc-402e82f84dde") }));
            Assert.That(literalInteger.OwningRelationship, Is.EqualTo(Guid.Parse("fe6d7f0c-6e7b-4ce9-acbe-25d2537f08d9")));
            Assert.That(literalInteger.DeclaredShortName, Is.EqualTo("the shortName"));
            Assert.That(literalInteger.Value, Is.EqualTo(123));
        }

        [Test]
        public void Verify_that_When_dictionary_is_read_instance_of_ILiteralRational_is_returned()
        {
            // READ COMPLEX - iteration 1

            this.dictionary = new Dictionary<string, object>
            {
                { "@type", "LiteralRational" },
                { "@id", Guid.Parse("0b192c18-afa2-44b6-8de2-86e5e6ffa09e") },
                { "aliasIds", new List<string> { "alias_1", "alias_2" } },
                { "direction", FeatureDirectionKind.In },
                { "elementId", "element id" },
                { "isAbstract", true },
                { "isComposite", true },
                { "isDerived", true },
                { "isEnd", true },
                { "isImpliedIncluded", true },
                { "isOrdered", true },
                { "isPortion", true },
                { "isReadOnly", true },
                { "isSufficient", true },
                { "isUnique", true },
                { "declaredName", "the name" },
                { "ownedRelationship", new List<Guid> { Guid.Parse("9006ff06-43fe-4a4e-a4bc-402e82f84dde") } },
                { "owningRelationship", Guid.Parse("fe6d7f0c-6e7b-4ce9-acbe-25d2537f08d9") },
                { "declaredShortName", "the shortName" },
                { "value", 123d },
            };

            var literalRational = reader.Read(this.dictionary, DictionaryKind.Complex) as ILiteralRational;

            Assert.That(literalRational.Id, Is.EqualTo(Guid.Parse("0b192c18-afa2-44b6-8de2-86e5e6ffa09e")));
            Assert.That(literalRational.AliasIds, Is.EqualTo(new List<string> { "alias_1", "alias_2" }));
            Assert.That(literalRational.ElementId, Is.EqualTo("element id"));
            Assert.That(literalRational.IsImpliedIncluded, Is.True);
            Assert.That(literalRational.DeclaredName, Is.EqualTo("the name"));
            Assert.That(literalRational.OwnedRelationship, Is.EqualTo(new List<Guid> { Guid.Parse("9006ff06-43fe-4a4e-a4bc-402e82f84dde") }));
            Assert.That(literalRational.OwningRelationship, Is.EqualTo(Guid.Parse("fe6d7f0c-6e7b-4ce9-acbe-25d2537f08d9")));
            Assert.That(literalRational.DeclaredShortName, Is.EqualTo("the shortName"));
            Assert.That(literalRational.Value, Is.EqualTo(123d));
        }

        [Test]
        public void Verify_that_When_dictionary_is_read_instance_of_INamespaceImport_is_returned()
        {
            // READ COMPLEX - iteration 1

            this.dictionary = new Dictionary<string, object>
            {
                { "@type", "NamespaceImport" },
                { "@id", Guid.Parse("0b192c18-afa2-44b6-8de2-86e5e6ffa09e") },
                { "aliasIds", new List<string> { "alias_1", "alias_2" } },
                { "elementId", "element id" },
                { "importedNamespace", Guid.Parse("fe6d7f0c-6e7b-4ce9-acbe-25d2537f08d9") },
                { "isImplied", true },
                { "isImpliedIncluded", true },
                { "isImportAll", true },
                { "isRecursive", true },
                { "declaredName", "the name" },
                { "ownedRelatedElement", new List<Guid> { Guid.Parse("c18450a9-fd96-4dd6-83c4-de5e6e1bd2f7") } },
                { "ownedRelationship", new List<Guid> { Guid.Parse("9006ff06-43fe-4a4e-a4bc-402e82f84dde") } },
                { "owningRelatedElement",  Guid.Parse("9006ff06-43fe-4a4e-a4bc-402e82f84ddf") },
                { "owningRelationship", Guid.Parse("fe6d7f0c-6e7b-4ce9-acbe-25d2537f08d9") },
                { "declaredShortName", "the shortName" },
                { "source", new List<string> { "8ba10c06-4c00-4748-ae60-a724ef773e29" }},
                { "target", new List<string> { "38c03271-5de8-4947-93db-20993d7a9dc2" }},
                { "visibility", VisibilityKind.Public }
            };

            var namespaceImport = reader.Read(this.dictionary, DictionaryKind.Complex) as INamespaceImport;

            Assert.That(namespaceImport.Id, Is.EqualTo(Guid.Parse("0b192c18-afa2-44b6-8de2-86e5e6ffa09e")));
            Assert.That(namespaceImport.AliasIds, Is.EqualTo(new List<string> { "alias_1", "alias_2" }));
            Assert.That(namespaceImport.ElementId, Is.EqualTo("element id"));
            Assert.That(namespaceImport.IsImpliedIncluded, Is.True);
            Assert.That(namespaceImport.DeclaredName, Is.EqualTo("the name"));
            Assert.That(namespaceImport.OwnedRelationship, Is.EqualTo(new List<Guid> { Guid.Parse("9006ff06-43fe-4a4e-a4bc-402e82f84dde") }));
            Assert.That(namespaceImport.OwningRelationship, Is.EqualTo(Guid.Parse("fe6d7f0c-6e7b-4ce9-acbe-25d2537f08d9")));
            Assert.That(namespaceImport.DeclaredShortName, Is.EqualTo("the shortName"));
            Assert.That(namespaceImport.Visibility, Is.EqualTo(VisibilityKind.Public));

            // READ SIMPLIFIED - iteration 1

            this.dictionary = new Dictionary<string, object>
            {
                { "@type", "NamespaceImport" },
                { "@id", "0b192c18-afa2-44b6-8de2-86e5e6ffa09e" },
                { "aliasIds", new List<string> { "alias_1", "alias_2" } },
                { "elementId", "element id" },
                { "importedNamespace", "fe6d7f0c-6e7b-4ce9-acbe-25d2537f08d9" },
                { "isImplied", true },
                { "isImpliedIncluded", true },
                { "isImportAll", true },
                { "isRecursive", true },
                { "declaredName", "the name" },
                { "ownedRelatedElement", new List<string> { "c18450a9-fd96-4dd6-83c4-de5e6e1bd2f7" } },
                { "ownedRelationship", new List<string> { "9006ff06-43fe-4a4e-a4bc-402e82f84dde" } },
                { "owningRelatedElement",  "9006ff06-43fe-4a4e-a4bc-402e82f84ddf" },
                { "owningRelationship", "fe6d7f0c-6e7b-4ce9-acbe-25d2537f08d9" },
                { "declaredShortName", "the shortName" },
                { "source", new List<string> { "8ba10c06-4c00-4748-ae60-a724ef773e29" }},
                { "target", new List<string> { "38c03271-5de8-4947-93db-20993d7a9dc2" }},
                { "visibility", "Public" }
            };

            namespaceImport = reader.Read(this.dictionary, DictionaryKind.Simplified) as INamespaceImport;

            Assert.That(namespaceImport.Id, Is.EqualTo(Guid.Parse("0b192c18-afa2-44b6-8de2-86e5e6ffa09e")));
            Assert.That(namespaceImport.AliasIds, Is.EqualTo(new List<string> { "alias_1", "alias_2" }));
            Assert.That(namespaceImport.ElementId, Is.EqualTo("element id"));
            Assert.That(namespaceImport.IsImpliedIncluded, Is.True);
            Assert.That(namespaceImport.DeclaredName, Is.EqualTo("the name"));
            Assert.That(namespaceImport.OwnedRelationship, Is.EqualTo(new List<Guid> { Guid.Parse("9006ff06-43fe-4a4e-a4bc-402e82f84dde") }));
            Assert.That(namespaceImport.OwningRelationship, Is.EqualTo(Guid.Parse("fe6d7f0c-6e7b-4ce9-acbe-25d2537f08d9")));
            Assert.That(namespaceImport.DeclaredShortName, Is.EqualTo("the shortName"));
            Assert.That(namespaceImport.Visibility, Is.EqualTo(VisibilityKind.Public));
        }

        [Test]
        public void Verify_that_When_dictionary_is_read_instance_of_IAcceptActionUsage_is_returned()
        {
            // READ COMPLEX - iteration 1

            this.dictionary = new Dictionary<string, object>
            {
                { "@type", "AcceptActionUsage" },
                { "@id", Guid.Parse("0b192c18-afa2-44b6-8de2-86e5e6ffa09e") },
                { "aliasIds", new List<string> { "alias_1", "alias_2" } },
                { "direction", FeatureDirectionKind.In },
                { "elementId", "element id" },
                { "isAbstract", true },
                { "isComposite", true },
                { "isDerived", true },
                { "isEnd", true },
                { "isImpliedIncluded", true },
                { "isIndividual", true },
                { "isOrdered", true },
                { "isPortion", true },
                { "isReadOnly", true },
                { "isSufficient", true },
                { "isUnique", true },
                { "isVariation", true },
                { "declaredName", "the name" },
                { "ownedRelationship", new List<Guid> { Guid.Parse("9006ff06-43fe-4a4e-a4bc-402e82f84dde") } },
                { "owningRelationship", Guid.Parse("fe6d7f0c-6e7b-4ce9-acbe-25d2537f08d9") },
                { "portionKind", PortionKind.Snapshot },
                { "declaredShortName", "the shortName" }
            };

            var acceptActionUsage = reader.Read(this.dictionary, DictionaryKind.Complex) as IAcceptActionUsage;
            
            Assert.That(acceptActionUsage.Id, Is.EqualTo(Guid.Parse("0b192c18-afa2-44b6-8de2-86e5e6ffa09e")));
            Assert.That(acceptActionUsage.AliasIds, Is.EqualTo(new List<string> { "alias_1", "alias_2" }));
            Assert.That(acceptActionUsage.Direction, Is.EqualTo(FeatureDirectionKind.In));
            Assert.That(acceptActionUsage.ElementId, Is.EqualTo("element id"));
            Assert.That(acceptActionUsage.IsAbstract, Is.True);
            Assert.That(acceptActionUsage.IsComposite, Is.True);
            Assert.That(acceptActionUsage.IsDerived, Is.True);
            Assert.That(acceptActionUsage.IsEnd, Is.True);
            Assert.That(acceptActionUsage.IsImpliedIncluded, Is.True);
            Assert.That(acceptActionUsage.IsIndividual, Is.True);
            Assert.That(acceptActionUsage.IsOrdered, Is.True);
            Assert.That(acceptActionUsage.IsPortion, Is.True);
            Assert.That(acceptActionUsage.IsReadOnly, Is.True);
            Assert.That(acceptActionUsage.IsSufficient, Is.True);
            Assert.That(acceptActionUsage.IsUnique, Is.True);
            Assert.That(acceptActionUsage.IsVariation, Is.True);
            Assert.That(acceptActionUsage.DeclaredName, Is.EqualTo("the name"));
            Assert.That(acceptActionUsage.OwnedRelationship, Is.EqualTo(new List<Guid> { Guid.Parse("9006ff06-43fe-4a4e-a4bc-402e82f84dde") }));
            Assert.That(acceptActionUsage.OwningRelationship, Is.EqualTo(Guid.Parse("fe6d7f0c-6e7b-4ce9-acbe-25d2537f08d9")));
            Assert.That(acceptActionUsage.PortionKind, Is.EqualTo(PortionKind.Snapshot));
            Assert.That(acceptActionUsage.DeclaredShortName, Is.EqualTo("the shortName"));

            // READ COMPLEX - iteration 2

            this.dictionary = new Dictionary<string, object>
            {
                { "@type", "AcceptActionUsage" },
                { "@id", Guid.Parse("0b192c18-afa2-44b6-8de2-86e5e6ffa09e") },
                { "aliasIds", new List<string>() },
                { "direction", null },
                { "elementId", "element id" },
                { "isAbstract", true },
                { "isComposite", true },
                { "isDerived", true },
                { "isEnd", true },
                { "isImpliedIncluded", true },
                { "isIndividual", true },
                { "isOrdered", true },
                { "isPortion", true },
                { "isReadOnly", true },
                { "isSufficient", true },
                { "isUnique", true },
                { "isVariation", true },
                { "declaredName", null },
                { "ownedRelationship", new List<Guid>() },
                { "owningRelationship", null },
                { "portionKind", null },
                { "declaredShortName", null }
            };

            acceptActionUsage = reader.Read(this.dictionary, DictionaryKind.Complex) as IAcceptActionUsage;

            Assert.That(acceptActionUsage.Id, Is.EqualTo(Guid.Parse("0b192c18-afa2-44b6-8de2-86e5e6ffa09e")));
            Assert.That(acceptActionUsage.AliasIds, Is.EqualTo(new List<string>() ));
            Assert.That(acceptActionUsage.Direction, Is.Null);
            Assert.That(acceptActionUsage.ElementId, Is.EqualTo("element id"));
            Assert.That(acceptActionUsage.IsAbstract, Is.True);
            Assert.That(acceptActionUsage.IsComposite, Is.True);
            Assert.That(acceptActionUsage.IsDerived, Is.True);
            Assert.That(acceptActionUsage.IsEnd, Is.True);
            Assert.That(acceptActionUsage.IsImpliedIncluded, Is.True);
            Assert.That(acceptActionUsage.IsIndividual, Is.True);
            Assert.That(acceptActionUsage.IsOrdered, Is.True);
            Assert.That(acceptActionUsage.IsPortion, Is.True);
            Assert.That(acceptActionUsage.IsReadOnly, Is.True);
            Assert.That(acceptActionUsage.IsSufficient, Is.True);
            Assert.That(acceptActionUsage.IsUnique, Is.True);
            Assert.That(acceptActionUsage.IsVariation, Is.True);
            Assert.That(acceptActionUsage.DeclaredName, Is.Null);
            Assert.That(acceptActionUsage.OwnedRelationship, Is.EqualTo(new List<Guid>()));
            Assert.That(acceptActionUsage.OwningRelationship, Is.Null);
            Assert.That(acceptActionUsage.PortionKind, Is.Null);
            Assert.That(acceptActionUsage.DeclaredShortName, Is.Null);

            // READ SIMPLIFIED - iteration 1

            this.dictionary = new Dictionary<string, object>
            {
                { "@type", "AcceptActionUsage" },
                { "@id", "0b192c18-afa2-44b6-8de2-86e5e6ffa09e" },
                { "aliasIds", new List<string> { "alias_1", "alias_2" } },
                { "direction", "In"},
                { "elementId", "element id" },
                { "isAbstract", true },
                { "isComposite", true },
                { "isDerived", true },
                { "isEnd", true },
                { "isImpliedIncluded", true },
                { "isIndividual", true },
                { "isOrdered", true },
                { "isPortion", true },
                { "isReadOnly", true },
                { "isSufficient", true },
                { "isUnique", true },
                { "isVariation", true },
                { "declaredName", "the name" },
                { "ownedRelationship", new List<string> { "9006ff06-43fe-4a4e-a4bc-402e82f84dde" } },
                { "owningRelationship", "fe6d7f0c-6e7b-4ce9-acbe-25d2537f08d9" },
                { "portionKind", null },
                { "declaredShortName", "the shortName" },
            };

            acceptActionUsage = reader.Read(this.dictionary, DictionaryKind.Simplified) as IAcceptActionUsage;

            Assert.That(acceptActionUsage.Id, Is.EqualTo(Guid.Parse("0b192c18-afa2-44b6-8de2-86e5e6ffa09e")));
            Assert.That(acceptActionUsage.AliasIds, Is.EqualTo(new List<string> { "alias_1", "alias_2" }));
            Assert.That(acceptActionUsage.Direction, Is.EqualTo(FeatureDirectionKind.In));
            Assert.That(acceptActionUsage.ElementId, Is.EqualTo("element id"));
            Assert.That(acceptActionUsage.IsAbstract, Is.True);
            Assert.That(acceptActionUsage.IsComposite, Is.True);
            Assert.That(acceptActionUsage.IsDerived, Is.True);
            Assert.That(acceptActionUsage.IsEnd, Is.True);
            Assert.That(acceptActionUsage.IsImpliedIncluded, Is.True);
            Assert.That(acceptActionUsage.IsIndividual, Is.True);
            Assert.That(acceptActionUsage.IsOrdered, Is.True);
            Assert.That(acceptActionUsage.IsPortion, Is.True);
            Assert.That(acceptActionUsage.IsReadOnly, Is.True);
            Assert.That(acceptActionUsage.IsSufficient, Is.True);
            Assert.That(acceptActionUsage.IsUnique, Is.True);
            Assert.That(acceptActionUsage.IsVariation, Is.True);
            Assert.That(acceptActionUsage.DeclaredName, Is.EqualTo("the name"));
            Assert.That(acceptActionUsage.OwnedRelationship, Is.EqualTo(new List<Guid> { Guid.Parse("9006ff06-43fe-4a4e-a4bc-402e82f84dde") }));
            Assert.That(acceptActionUsage.OwningRelationship, Is.EqualTo(Guid.Parse("fe6d7f0c-6e7b-4ce9-acbe-25d2537f08d9")));
            Assert.That(acceptActionUsage.PortionKind, Is.Null);
            Assert.That(acceptActionUsage.DeclaredShortName, Is.EqualTo("the shortName"));

            // READ SIMPLIFIED - iteration 2

            this.dictionary = new Dictionary<string, object>
            {
                { "@type", "AcceptActionUsage" },
                { "@id", Guid.Parse("0b192c18-afa2-44b6-8de2-86e5e6ffa09e") },
                { "aliasIds", new List<string>() },
                { "direction", null },
                { "elementId", "element id" },
                { "isAbstract", true },
                { "isComposite", true },
                { "isDerived", true },
                { "isEnd", true },
                { "isImpliedIncluded", true },
                { "isIndividual", true },
                { "isOrdered", true },
                { "isPortion", true },
                { "isReadOnly", true },
                { "isSufficient", true },
                { "isUnique", true },
                { "isVariation", true },
                { "declaredName", null },
                { "ownedRelationship", new List<string>() },
                { "owningRelationship", null },
                { "portionKind", null },
                { "declaredShortName", null }
            };

            acceptActionUsage = reader.Read(this.dictionary, DictionaryKind.Simplified) as IAcceptActionUsage;

            Assert.That(acceptActionUsage.Id, Is.EqualTo(Guid.Parse("0b192c18-afa2-44b6-8de2-86e5e6ffa09e")));
            Assert.That(acceptActionUsage.AliasIds, Is.EqualTo(new List<string>() ));
            Assert.That(acceptActionUsage.Direction, Is.Null);
            Assert.That(acceptActionUsage.ElementId, Is.EqualTo("element id"));
            Assert.That(acceptActionUsage.IsAbstract, Is.True);
            Assert.That(acceptActionUsage.IsComposite, Is.True);
            Assert.That(acceptActionUsage.IsDerived, Is.True);
            Assert.That(acceptActionUsage.IsEnd, Is.True);
            Assert.That(acceptActionUsage.IsImpliedIncluded, Is.True);
            Assert.That(acceptActionUsage.IsIndividual, Is.True);
            Assert.That(acceptActionUsage.IsOrdered, Is.True);
            Assert.That(acceptActionUsage.IsPortion, Is.True);
            Assert.That(acceptActionUsage.IsReadOnly, Is.True);
            Assert.That(acceptActionUsage.IsSufficient, Is.True);
            Assert.That(acceptActionUsage.IsUnique, Is.True);
            Assert.That(acceptActionUsage.IsVariation, Is.True);
            Assert.That(acceptActionUsage.DeclaredName, Is.Null);
            Assert.That(acceptActionUsage.OwnedRelationship, Is.EqualTo(new List<Guid>()));
            Assert.That(acceptActionUsage.OwningRelationship, Is.Null);
            Assert.That(acceptActionUsage.PortionKind, Is.Null);
            Assert.That(acceptActionUsage.DeclaredShortName, Is.Null);
        }

        [Test]
        public void Verify_that_when_an_unsupported_type_is_provided_an_Exception_is_thrown()
        {
            var dictionary = new Dictionary<string, Object>
            {
                { "@type", "TestClass" },
                { "@id", Guid.NewGuid() }
            };
            
            Assert.That(() => this.reader.Read(dictionary, DictionaryKind.Simplified), Throws.Exception.TypeOf<NotSupportedException>());
            Assert.That(() => this.reader.Read(dictionary, DictionaryKind.Complex), Throws.Exception.TypeOf<NotSupportedException>());
        }
    }
}
