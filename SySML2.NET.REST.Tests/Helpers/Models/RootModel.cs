// ------------------------------------------------------------------------------------------------
// <copyright file="RootModel.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SySML2.NET.REST.Tests.Helpers.Models
{
    using SysML2.NET.Core.Core.Types;
    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Kernel.Expressions;
    using SysML2.NET.Core.POCO.Kernel.Functions;
    using SysML2.NET.Core.POCO.Kernel.Packages;
    using SysML2.NET.Core.POCO.Root.Annotations;
    using SysML2.NET.Core.POCO.Root.Dependencies;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Core.Root.Namespaces;
    using SysML2.NET.Dal;
    using SysML2.NET.Extensions;

    /// <summary>
    /// The model of scenario step 2: the root package and the KerML Root and Packages metaclasses
    /// </summary>
    /// <remarks>
    /// <code>
    /// package &lt;cvm&gt; CoverageModel {
    ///     package &lt;nst&gt; Nested {
    ///         private import all Library::*::**;
    ///         public import Space::LibAlias;
    ///     }
    ///     namespace &lt;spc&gt; Space {
    ///         protected alias &lt;la&gt; LibAlias for Library;
    ///     }
    ///     standard library package &lt;lib&gt; Library {
    ///         filter true;
    ///     }
    ///     comment &lt;cmt&gt; Note about Nested locale "en-US" /* A comment about Nested. */
    ///     doc &lt;dcm&gt; /* The coverage model. */
    ///     rep &lt;rpr&gt; Representation language "natural" /* The coverage model, in words. */
    ///     dependency &lt;dep&gt; Uses from Nested to Library;
    /// }
    /// </code>
    /// plus an <see cref="AnnotatingElement"/> about <c>Library</c>, which has no textual notation of its own, and the
    /// result parameter of the filter condition, which KerML requires of every <c>Expression</c> (<c>result [1..1]</c>).
    /// </remarks>
    public sealed class RootModel : StepModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RootModel"/> class and builds the model
        /// </summary>
        public RootModel() : base(new ModelBuilder("root"), "root")
        {
            this.Root = this.CreateNamed<Package>("CoverageModel", "cvm", PackageExtensions.ToDto);

            this.Nested = this.CreateNamed<Package>("Nested", "nst", PackageExtensions.ToDto);
            this.OwnMember(this.Root, this.Nested);

            var space = this.CreateNamed<Namespace>("Space", "spc", NamespaceExtensions.ToDto);
            this.OwnMember(this.Root, space);

            var library = this.CreateNamed<LibraryPackage>("Library", "lib", LibraryPackageExtensions.ToDto);
            library.IsStandard = true;
            this.OwnMember(this.Root, library);

            var namespaceImport = this.Builder.Create<NamespaceImport>("Nested/import Library", NamespaceImportExtensions.ToDto);
            namespaceImport.ImportedNamespace = library;
            namespaceImport.IsImportAll = true;
            namespaceImport.IsRecursive = true;
            namespaceImport.Visibility = VisibilityKind.Private;
            this.Nested.AssignOwnership(namespaceImport);

            var alias = this.Builder.Create<Membership>("Space/LibAlias", MembershipExtensions.ToDto);
            alias.MemberElement = library;
            alias.MemberName = "LibAlias";
            alias.MemberShortName = "la";
            alias.Visibility = VisibilityKind.Protected;
            space.AssignOwnership(alias);

            var membershipImport = this.Builder.Create<MembershipImport>("Nested/import LibAlias", MembershipImportExtensions.ToDto);
            membershipImport.ImportedMembership = alias;
            membershipImport.Visibility = VisibilityKind.Public;
            this.Nested.AssignOwnership(membershipImport);

            var filter = this.Builder.Create<ElementFilterMembership>("Library/filter", ElementFilterMembershipExtensions.ToDto);
            var condition = this.Builder.Create<LiteralBoolean>("Library/filter/condition", LiteralBooleanExtensions.ToDto);
            condition.Value = true;
            library.AssignOwnership(filter, condition);

            var conditionResult = this.Builder.Create<Feature>("Library/filter/condition/result", FeatureExtensions.ToDto);
            conditionResult.Direction = FeatureDirectionKind.Out;
            var resultMembership = this.Builder.Create<ReturnParameterMembership>("Library/filter/condition/result/membership", ReturnParameterMembershipExtensions.ToDto);
            condition.AssignOwnership(resultMembership, conditionResult);

            var comment = this.CreateNamed<Comment>("Note", "cmt", CommentExtensions.ToDto);
            comment.Locale = "en-US";
            comment.Body = "A comment about Nested.";
            this.OwnMember(this.Root, comment);

            var commentAnnotation = this.Builder.Create<Annotation>("Note/about Nested", AnnotationExtensions.ToDto);
            commentAnnotation.AnnotatedElement = this.Nested;
            comment.AssignOwnership(commentAnnotation);

            var documentation = this.Builder.Create<Documentation>("doc", DocumentationExtensions.ToDto);
            documentation.DeclaredShortName = "dcm";
            documentation.Body = "The coverage model.";
            this.OwnMember(this.Root, documentation);

            var representation = this.CreateNamed<TextualRepresentation>("Representation", "rpr", TextualRepresentationExtensions.ToDto);
            representation.Language = "natural";
            representation.Body = "The coverage model, in words.";
            this.OwnMember(this.Root, representation);

            var annotator = this.CreateNamed<AnnotatingElement>("Annotator", "ann", AnnotatingElementExtensions.ToDto);
            this.OwnMember(this.Root, annotator);

            var annotatorAnnotation = this.Builder.Create<Annotation>("Annotator/about Library", AnnotationExtensions.ToDto);
            annotatorAnnotation.AnnotatedElement = library;
            annotator.AssignOwnership(annotatorAnnotation);

            var dependency = this.CreateNamed<Dependency>("Uses", "dep", DependencyExtensions.ToDto);
            dependency.Client.Add(this.Nested);
            dependency.Supplier.Add(library);
            this.OwnMember(this.Root, dependency);
        }

        /// <summary>
        /// Gets the root package
        /// </summary>
        public Package Root { get; }

        /// <summary>
        /// Gets the nested package, which owns the imports and is annotated by the comment
        /// </summary>
        public Package Nested { get; }
    }
}
