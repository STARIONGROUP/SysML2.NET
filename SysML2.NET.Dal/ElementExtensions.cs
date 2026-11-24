// ------------------------------------------------------------------------------------------------
// <copyright file="ElementExtensions.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Dal
{
    using System;

    using SysML2.NET.Core.POCO.Root.Elements;

    /// <summary>
    /// Hand-coded extension members for the <see cref="IElement"/> interface
    /// </summary>
    public static partial class ElementExtensions
    {
        extension(IElement)
        {
            /// <summary>
            /// Runs the <paramref name="map"/> of one derived property, leaving its target at the default value when the derivation is not supported.
            /// </summary>
            /// <param name="map">The assignment of one derived property</param>
            internal static void TryMap(Action map)
            {
                try
                {
                    map();
                }
                catch (NotSupportedException)
                {
                    // An unimplemented derivation leaves the property unmapped.
                }
            }
        }
    }
}
