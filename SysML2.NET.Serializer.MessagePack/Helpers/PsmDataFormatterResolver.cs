// -------------------------------------------------------------------------------------------------
// <copyright file="PsmDataFormatterResolver.cs" company="Starion Group S.A.">
//
//   Copyright 2022-2026 Starion Group S.A.
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

namespace SysML2.NET.Serializer.MessagePack.Helpers
{
    using global::MessagePack;
    using global::MessagePack.Formatters;

    using SysML2.NET.Serializer.MessagePack.PSM;

    /// <summary>
    /// The purpose of the <see cref="PsmDataFormatterResolver"/> is to provide the
    /// <see cref="IMessagePackFormatter"/> for the Systems Modeling API and Services types
    /// </summary>
    /// <remarks>
    /// The resolver returns null for any other type so that the composite resolver falls through to the
    /// <see cref="DataFormatterResolver"/>, which is what lets a Data payload carry a SysML Core element.
    /// </remarks>
    public class PsmDataFormatterResolver : IFormatterResolver
    {
        /// <summary>
        /// Gets the singleton instance of the <see cref="PsmDataFormatterResolver"/>.
        /// </summary>
        public static readonly IFormatterResolver Instance = new PsmDataFormatterResolver();

        /// <summary>
        /// Initializes a new instance of the <see cref="PsmDataFormatterResolver"/> class
        /// </summary>
        private PsmDataFormatterResolver()
        {
        }

        /// <summary>
        /// Gets an <see cref="IMessagePackFormatter{T}"/> instance that can serialize some type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type of value to be serialized.</typeparam>
        /// <returns>A formatter, if this resolver supplies one for type <typeparamref name="T"/>; otherwise <see langword="null"/>.</returns>
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
                (IMessagePackFormatter<T>)PsmDataResolverGetFormatterHelper.GetFormatter(typeof(T));
        }
    }
}
