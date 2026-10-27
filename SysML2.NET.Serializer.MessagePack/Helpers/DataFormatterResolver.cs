// ------------------------------------------------------------------------------------------------
// <copyright file="DataFormatterResolver.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Serializer.MessagePack.Helpers
{
    using global::MessagePack;
    using global::MessagePack.Formatters;

    using SysML2.NET.Serializer.MessagePack.Core;

    /// <summary>
    /// The purpose of the <see cref="DataFormatterResolver"/> is to provide the model
    /// <see cref="IMessagePackFormatter"/> for all concrete classes
    /// </summary>
    public class DataFormatterResolver : IFormatterResolver
    {
        /// <summary>
        /// Gets the singleton instance of the <see cref="DataFormatterResolver"/>.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This resolver is implemented as a singleton to ensure that formatter
        /// resolution is centralized, stateless, and allocation-free at runtime.
        /// </para>
        /// <para>
        /// MessagePack resolvers are expected to be reusable and thread-safe.
        /// Using a single shared instance avoids unnecessary allocations and
        /// ensures consistent formatter behavior across all serialization and
        /// deserialization operations.
        /// </para>
        /// </remarks>
        public static readonly IFormatterResolver Instance = new DataFormatterResolver();

        /// <summary>
        /// Initializes a new instance of the <see cref="DataFormatterResolver"/> class
        /// </summary>
        private DataFormatterResolver()
        {
        }

        /// <summary>
        /// Gets an <see cref="T:MessagePack.Formatters.IMessagePackFormatter`1" /> instance that can serialize or deserialize some type <typeparamref name="T" />.
        /// </summary>
        /// <typeparam name="T">The type of value to be serialized or deserialized.</typeparam>
        /// <returns>A formatter, if this resolver supplies one for type <typeparamref name="T" />; otherwise <see langword="null" />.</returns>
        public IMessagePackFormatter<T> GetFormatter<T>()
        {
            return FormatterCache<T>.Formatter;
        }

        /// <summary>
        /// Per-type cache for resolved MessagePack formatters.
        /// </summary>
        /// <typeparam name="T">The type for which a formatter is cached.</typeparam>
        private static class FormatterCache<T>
        {
            /// <summary>
            /// The cached formatter for <typeparamref name="T"/>, or <see langword="null"/> if none is available.
            /// </summary>
            public static readonly IMessagePackFormatter<T> Formatter =
                (IMessagePackFormatter<T>)DataResolverGetFormatterHelper.GetFormatter(typeof(T));
        }
    }
}
