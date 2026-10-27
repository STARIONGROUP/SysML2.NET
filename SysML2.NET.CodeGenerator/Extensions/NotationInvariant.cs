// ------------------------------------------------------------------------------------------------
// <copyright file="NotationInvariant.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.CodeGenerator.Extensions
{
    using System;

    /// <summary>
    /// A single notation invariant: a rule the writer must honour that neither the KEBNF nor the metamodel
    /// states machine-readably, anchored to the OMG name it depends on.
    /// </summary>
    public sealed class NotationInvariant
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NotationInvariant" /> class.
        /// </summary>
        /// <param name="name">The stable key the generator refers to the invariant by.</param>
        /// <param name="metamodelName">The OMG metaclass or property name the invariant depends on.</param>
        /// <param name="justification">Why the invariant holds, and what breaks without it.</param>
        /// <exception cref="ArgumentException">Thrown when any argument is null or whitespace.</exception>
        public NotationInvariant(string name, string metamodelName, string justification)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("The invariant name is required.", nameof(name));
            }

            if (string.IsNullOrWhiteSpace(metamodelName))
            {
                throw new ArgumentException("The metamodel name is required.", nameof(metamodelName));
            }

            if (string.IsNullOrWhiteSpace(justification))
            {
                throw new ArgumentException("A justification is required so the invariant can be audited.", nameof(justification));
            }

            this.Name = name;
            this.MetamodelName = metamodelName;
            this.Justification = justification;
        }

        /// <summary>
        /// Gets the stable key the generator refers to the invariant by.
        /// </summary>
        /// <remarks>
        /// Deliberately independent of <see cref="MetamodelName" />: the generator names the CONCEPT, so an
        /// OMG rename is a single edit here rather than a hunt through the emission code.
        /// </remarks>
        public string Name { get; }

        /// <summary>
        /// Gets the OMG metaclass or property name the invariant depends on.
        /// </summary>
        public string MetamodelName { get; }

        /// <summary>
        /// Gets the reason the invariant holds, and what breaks without it.
        /// </summary>
        public string Justification { get; }
    }
}
