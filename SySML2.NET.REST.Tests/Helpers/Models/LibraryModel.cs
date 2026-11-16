// ------------------------------------------------------------------------------------------------
// <copyright file="LibraryModel.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SySML2.NET.REST.Tests.Helpers.Models
{
    using SysML2.NET.Core.POCO.Kernel.Packages;
    using SysML2.NET.Dal;

    /// <summary>
    /// The model of the second project of scenario step 10, which the main model uses through a project usage
    /// </summary>
    /// <remarks>
    /// <code>
    /// library package &lt;lib&gt; UsedLibrary {
    ///     package &lt;usd&gt; Used;
    /// }
    /// </code>
    /// </remarks>
    public sealed class LibraryModel : StepModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LibraryModel"/> class and builds the model
        /// </summary>
        public LibraryModel() : base(new ModelBuilder("library"), "library")
        {
            this.Root = this.CreateNamed<LibraryPackage>("UsedLibrary", "lib", LibraryPackageExtensions.ToDto);
            this.Used = this.CreateNamed<Package>("Used", "usd", PackageExtensions.ToDto);
            this.OwnMember(this.Root, this.Used);
        }

        /// <summary>
        /// Gets the root library package
        /// </summary>
        public LibraryPackage Root { get; }

        /// <summary>
        /// Gets the package that the library package owns, the element whose project usage step 10 reads
        /// </summary>
        public Package Used { get; }
    }
}
