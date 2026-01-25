// -------------------------------------------------------------------------------------------------
// <copyright file="PayloadMessagePackFormatter.cs" company="Starion Group S.A.">
//
//    Copyright 2022-2026 Starion Group S.A.
//
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
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

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Serializer.MessagePack.Core
{
    using System;

    using global::MessagePack;
    using global::MessagePack.Formatters;

    using SysML2.NET.Core.DTO.Root.Annotations;

    /// <summary>
    /// The purpose of the <see cref="PayloadMessagePackFormatter"/> is to provide
    /// the contract for serialization of the <see cref="Payload"/> type
    /// </summary>
    internal class PayloadMessagePackFormatter : IMessagePackFormatter<Payload>
    {
        /// <summary>
        /// Serializes an <see cref="Payload"/>.
        /// </summary>
        /// <param name="writer">
        /// The writer to use when serializing the <see cref="Payload"/>.
        /// </param>
        /// <param name="payload">
        /// The <see cref="Payload"/> that is to be serialized.
        /// </param>
        /// <param name="options">
        /// The serialization settings to use.
        /// </param>
        public void Serialize(ref MessagePackWriter writer, Payload payload, MessagePackSerializerOptions options)
        {
            if (payload == null)
            {
                throw new ArgumentNullException(nameof(payload), "The Payload may not be null");
            }

            var formatterResolver = options.Resolver;

            writer.WriteArrayHeader(147);

            writer.Write(payload.Created);

            writer.WriteArrayHeader(payload.AnnotatingElement.Count);
            foreach (var annotatingElement in payload.AnnotatingElement)
            {
                formatterResolver.GetFormatterWithVerify<AnnotatingElement>().Serialize(ref writer, annotatingElement, options);
            }

            writer.Flush();
        }

        /// <summary>
        /// Deserializes an <see cref="Payload"/>.
        /// </summary>
        /// <param name="reader">
        /// The reader to deserialize from.
        /// </param>
        /// <param name="options">
        /// The serialization settings to use.
        /// </param>
        /// <returns>
        /// The deserialized value.
        /// </returns>
        public Payload Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
        {
            if (reader.TryReadNil())
            {
                return null;
            }

            var formatterResolver = options.Resolver;
            options.Security.DepthStep(ref reader);

            var payload = new Payload();

            var propertyCounter = reader.ReadArrayHeader();

            for (var i = 0; i < propertyCounter; i++)
            {
                int valueLength;
                int valueCounter;

                switch (i)
                {
                    case 0:
                        payload.Created = reader.ReadDateTime();
                        break;
                    case 1:
                        valueLength = reader.ReadArrayHeader();
                        for (valueCounter = 0; valueCounter < valueLength; valueCounter++)
                        {
                            var actualFiniteState = formatterResolver.GetFormatterWithVerify<AnnotatingElement>().Deserialize(ref reader, options);
                            payload.AnnotatingElement.Add(actualFiniteState);
                        }

                        break;
                }
            }

            return payload;
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
