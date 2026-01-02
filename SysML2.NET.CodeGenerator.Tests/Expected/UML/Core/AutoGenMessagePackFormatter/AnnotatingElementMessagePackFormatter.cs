// -------------------------------------------------------------------------------------------------
// <copyright file="AnnotatingElementMessagePackFormatter.cs" company="Starion Group S.A.">
// 
//   Copyright 2022-2025 Starion Group S.A.
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

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Serializer.MessagePack.Core
{
    using System;
    using System.Linq;

    using global::MessagePack;
    using global::MessagePack.Formatters;
    
    using SysML2.NET.Core.DTO.Root.Annotations;
    using SysML2.NET.Extensions.Comparers;
    using SysML2.NET.Serializer.MessagePack.Helpers;
    
    /// <summary>
    /// The purpose of the <see cref="AnnotatingElementMessagePackFormatter"/> is to provide
    /// the contract for MessagePack (de)serialization of the <see cref="AnnotatingElement"/> type
    /// </summary>
    public class AnnotatingElementMessagePackFormatter : IMessagePackFormatter<AnnotatingElement>
    {
        /// <summary>
        /// The <see cref="GuidComparer"/> used to compare 2 <see cref="Guid"/>s
        /// </summary>
        private static readonly GuidComparer GuidComparer = new GuidComparer();

        /// <summary>
        /// Serializes an <see cref="AnnotatingElement"/> DTO.
        /// </summary>
        /// <param name="writer">
        /// The <see cref="MessagePackWriter"/> to use when serializing the value.
        /// </param>
        /// <param name="annotatingElement">
        /// The <see cref="AnnotatingElement"/> that is to be serialized.
        /// </param>
        /// <param name="options">
        /// The serialization settings to use.
        /// </param>
        public void Serialize(ref MessagePackWriter writer, AnnotatingElement annotatingElement, MessagePackSerializerOptions options)
        {
            if (annotatingElement == null)
            {
                throw new ArgumentNullException(nameof(annotatingElement), "The AnnotatingElement may not be null");
            }

            writer.WriteArrayHeader(26);

            writer.Write(annotatingElement.Id.ToByteArray());



            writer.Flush();
        }

        /// <summary>
        /// Deserializes an <see cref="AnnotatingElement"/> DTO
        /// </summary>
        /// <param name="reader">
        /// The <see cref="MessagePackWriter"/> to deserialize the <see cref="AnnotatingElement"/> from.
        /// </param>
        /// <param name="options">
        /// The serialization settings to use.
        /// </param>
        /// <returns>
        /// The deserialized <see cref="AnnotatingElement"/>.
        /// </returns>
        public AnnotatingElement Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
        {
            if (reader.TryReadNil())
            {
                return null;
            }

            options.Security.DepthStep(ref reader);

            var annotatingElement = new AnnotatingElement();

            var propertyCounter = reader.ReadArrayHeader();

            for (var i = 0; i < propertyCounter; i++)
            {
                int valueLength;
                int valueCounter;

                switch (i)
                {
                    case 0:
                        annotatingElement.Id = reader.ReadBytes().ToGuid();
                        break;
                    default:
                        reader.Skip();
                        break;
                }
            }

            reader.Depth--;

            return annotatingElement;
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
