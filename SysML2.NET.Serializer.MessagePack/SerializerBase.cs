// ------------------------------------------------------------------------------------------------
// <copyright file="SerializerBase.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Serializer.MessagePack
{
    using global::MessagePack;
    using global::MessagePack.Resolvers;

    using SysML2.NET.Serializer.MessagePack.Helpers;

    /// <summary>
    /// Base class from which the Serializer and Deserializer derive
    /// </summary>
    public abstract class SerializerBase
    {
        /// <summary>
        /// Creates an instance of the <see cref="MessagePackSerializerOptions"/> 
        /// </summary>
        /// <returns>
        /// An instance of <see cref="MessagePackSerializerOptions"/>
        /// </returns>
        protected static MessagePackSerializerOptions CreateSerializerOptions()
        {
            var formatterResolver = CompositeResolver.Create(
                PsmDataFormatterResolver.Instance,
                DataFormatterResolver.Instance,
                StandardResolver.Instance);

            var options = MessagePackSerializerOptions.Standard
                .WithResolver(formatterResolver)
                .WithOldSpec(false);

            return options;
        }

        /// <summary>
        /// Creates an instance of the <see cref="SysMLMessagePackSerializerOptions"/> that tells whether derived properties are exchanged
        /// </summary>
        /// <param name="includeDerivedProperties">Whether derived properties are written and read</param>
        /// <returns>
        /// An instance of <see cref="SysMLMessagePackSerializerOptions"/>
        /// </returns>
        protected static SysMLMessagePackSerializerOptions CreateSerializerOptions(bool includeDerivedProperties)
        {
            return new SysMLMessagePackSerializerOptions(CreateSerializerOptions(), includeDerivedProperties);
        }
    }
}
