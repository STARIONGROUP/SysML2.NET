// -------------------------------------------------------------------------------------------------
// <copyright file="DataFormatterResolver.cs" company="Starion Group S.A.">
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

    using SysML2.NET.Serializer.MessagePack.Core;

    /// <summary>
    /// The purpose of the <see cref="DataFormatterResolver"/> is to provide the model
    /// <see cref="IMessagePackFormatter"/> for all concrete classes
    /// </summary>
    public class DataFormatterResolver : IFormatterResolver
    {
        // Resolver should be singleton.
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
        /// A cache from which <see cref="IMessagePackFormatter"/> can be resolved
        /// </summary>
        /// <typeparam name="T"></typeparam>
        private static class FormatterCache<T>
        {
            public static readonly IMessagePackFormatter<T> Formatter;

            // generic's static constructor should be minimized for reduce type generation size!
            // use outer helper method.
            static FormatterCache()
            {
                var type = typeof(T);

                Formatter = (IMessagePackFormatter<T>)DataResolverGetFormatterHelper.GetFormatter(type);
            }
        }
    }
}
