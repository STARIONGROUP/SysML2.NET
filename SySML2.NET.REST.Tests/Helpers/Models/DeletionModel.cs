// ------------------------------------------------------------------------------------------------
// <copyright file="DeletionModel.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SySML2.NET.REST.Tests.Helpers.Models
{
    using System;

    using SysML2.NET.Core.POCO.Kernel.Packages;
    using SysML2.NET.Core.POCO.Root.Annotations;
    using SysML2.NET.Dal;

    /// <summary>
    /// The model of scenario step 12: content in the root package that the step deletes again, built for its three
    /// deletion cases so that no element of an earlier step loses a reference
    /// </summary>
    /// <remarks>
    /// <code>
    /// package &lt;del&gt; Deletions {
    ///     package &lt;sgl&gt; Single;                                     // D1: deleted with its owning membership
    ///     package &lt;emp&gt; Emptied { package First; package Second; }  // D2: its whole content deleted
    ///     package &lt;cas&gt; Cascade {                                   // D3: deleted alone, owning the rest
    ///         package Inner { comment Note /* Deleted with Cascade. */ }
    ///     }
    /// }
    /// </code>
    /// </remarks>
    public sealed class DeletionModel : StepModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeletionModel"/> class and builds the model
        /// </summary>
        /// <param name="root">
        /// The model of step 2, whose root package owns the content
        /// </param>
        public DeletionModel(RootModel root) : base((root ?? throw new ArgumentNullException(nameof(root))).Builder, "deletion")
        {
            this.Package = this.CreateNamed<Package>("Deletions", "del", PackageExtensions.ToDto);
            this.OwnMember(root.Root, this.Package);

            this.Single = this.CreateNamed<Package>("Single", "sgl", PackageExtensions.ToDto);
            this.OwnMember(this.Package, this.Single);

            this.Emptied = this.CreateNamed<Package>("Emptied", "emp", PackageExtensions.ToDto);
            this.OwnMember(this.Package, this.Emptied);
            this.First = this.CreateNamed<Package>("First", "fst", PackageExtensions.ToDto);
            this.OwnMember(this.Emptied, this.First);
            this.Second = this.CreateNamed<Package>("Second", "snd", PackageExtensions.ToDto);
            this.OwnMember(this.Emptied, this.Second);

            this.Cascade = this.CreateNamed<Package>("Cascade", "cas", PackageExtensions.ToDto);
            this.OwnMember(this.Package, this.Cascade);
            this.Inner = this.CreateNamed<Package>("Inner", "inr", PackageExtensions.ToDto);
            this.OwnMember(this.Cascade, this.Inner);
            this.Note = this.CreateNamed<Comment>("Note", "nte", CommentExtensions.ToDto);
            this.Note.Body = "Deleted with Cascade.";
            this.OwnMember(this.Inner, this.Note);
        }

        /// <summary>
        /// Gets the package that holds the content to delete
        /// </summary>
        public Package Package { get; }

        /// <summary>
        /// Gets the package that case D1 deletes with its owning membership
        /// </summary>
        public Package Single { get; }

        /// <summary>
        /// Gets the package whose whole content case D2 deletes
        /// </summary>
        public Package Emptied { get; }

        /// <summary>
        /// Gets the first member of <see cref="Emptied"/>
        /// </summary>
        public Package First { get; }

        /// <summary>
        /// Gets the second member of <see cref="Emptied"/>
        /// </summary>
        public Package Second { get; }

        /// <summary>
        /// Gets the package that case D3 deletes alone, so that the deletion must cascade to what it owns
        /// </summary>
        public Package Cascade { get; }

        /// <summary>
        /// Gets the package that <see cref="Cascade"/> owns
        /// </summary>
        public Package Inner { get; }

        /// <summary>
        /// Gets the comment that <see cref="Inner"/> owns
        /// </summary>
        public Comment Note { get; }
    }
}
