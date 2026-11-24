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

    using Microsoft.Extensions.Logging;

    using SysML2.NET.Core.POCO.Root.Elements;

    /// <summary>
    /// Hand-coded extension members for the <see cref="IElement"/> interface
    /// </summary>
    public static partial class ElementExtensions
    {
        /// <param name="element">The <see cref="IElement"/> whose derived property is mapped</param>
        extension(IElement element)
        {
            /// <summary>
            /// Runs the <paramref name="map"/> of one derived property, leaving its target at the default value and logging a warning when the derivation is not supported.
            /// </summary>
            /// <param name="propertyName">The name of the derived property</param>
            /// <param name="map">The assignment of the derived property</param>
            /// <param name="logger">The <see cref="ILogger"/> that reports an unsupported derivation; nothing is logged when it is null</param>
            internal void TryMap(string propertyName, Action map, ILogger logger)
            {
                try
                {
                    map();
                }
                catch (NotSupportedException exception)
                {
                    if (logger != null && logger.IsEnabled(LogLevel.Warning))
                    {
                        logger.LogWarning(exception, "The derived property {PropertyName} of the {ElementType} {ElementId} is not supported and is left unmapped", propertyName, element.GetType().Name, element.Id);
                    }
                }
            }
        }
    }
}
