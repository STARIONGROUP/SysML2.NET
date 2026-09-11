// -------------------------------------------------------------------------------------------------
// <copyright file="CompositeConstraintMessagePackFormatter.cs" company="Starion Group S.A.">
//
//    Copyright (C) 2022-2026 Starion Group S.A.
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

namespace SysML2.NET.Serializer.MessagePack.PSM
{
    using System;
    using System.Collections.Generic;

    using SysML2.NET.Extensions.PSM;
    using SysML2.NET.PSM.DTO;
    using SysML2.NET.Serializer.MessagePack.Core;

    using global::MessagePack;
    using global::MessagePack.Formatters;

    /// <summary>
    /// The purpose of the <see cref="CompositeConstraintMessagePackFormatter"/> is to provide
    /// the contract for MessagePack serialization of the <see cref="CompositeConstraint"/> type
    /// </summary>
    public class CompositeConstraintMessagePackFormatter : MessagePackFormatterBase, IMessagePackFormatter<CompositeConstraint>
    {
        /// <summary>
        /// Serializes a <see cref="CompositeConstraint"/> DTO.
        /// </summary>
        /// <param name="writer">
        /// The <see cref="MessagePackWriter"/> to use when serializing the value.
        /// </param>
        /// <param name="compositeConstraint">
        /// The <see cref="CompositeConstraint"/> that is to be serialized.
        /// </param>
        /// <param name="options">
        /// The serialization settings to use.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when the DTO is null
        /// </exception>
        public void Serialize(ref MessagePackWriter writer, CompositeConstraint compositeConstraint, MessagePackSerializerOptions options)
        {
            if (compositeConstraint == null)
            {
                throw new ArgumentNullException(nameof(compositeConstraint), "The CompositeConstraint may not be null");
            }

            writer.WriteArrayHeader(2);

            if (compositeConstraint.Constraint == null)
            {
                writer.WriteArrayHeader(0);
            }
            else
            {
                writer.WriteArrayHeader(compositeConstraint.Constraint.Count);

                foreach (var item in compositeConstraint.Constraint)
                {
                    PsmMessagePackDispatcher.Write(ref writer, item, options);
                }
            }

            writer.WriteString(CompositeConstraintOperatorProvider.ToUtf8Bytes(compositeConstraint.Operator));

            writer.Flush();
        }

        /// <summary>
        /// Deserializes a <see cref="CompositeConstraint"/> DTO.
        /// </summary>
        /// <param name="reader">
        /// The <see cref="MessagePackReader"/> to deserialize the <see cref="CompositeConstraint"/> from.
        /// </param>
        /// <param name="options">
        /// The serialization settings to use.
        /// </param>
        /// <returns>
        /// The deserialized <see cref="CompositeConstraint"/>.
        /// </returns>
        public CompositeConstraint Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
        {
            if (reader.TryReadNil())
            {
                return null;
            }

            options.Security.DepthStep(ref reader);

            var compositeConstraint = new CompositeConstraint();

            var propertyCounter = reader.ReadArrayHeader();

            for (var i = 0; i < propertyCounter; i++)
            {
                switch (i)
                {
                    case 0:
                        var constraintLength = reader.ReadArrayHeader();
                        compositeConstraint.Constraint = new List<IConstraint>(constraintLength);

                        for (var valueCounter = 0; valueCounter < constraintLength; valueCounter++)
                        {
                            compositeConstraint.Constraint.Add((IConstraint)PsmMessagePackDispatcher.Read(ref reader, options));
                        }

                        break;

                    case 1:
                        compositeConstraint.Operator = CompositeConstraintOperatorProvider.Parse(reader.ReadStringSequence().Value);
                        break;

                    default:
                        reader.Skip();
                        break;
                }
            }

            reader.Depth--;

            return compositeConstraint;
        }
    }
}

// ------------------------------------------------------------------------------------------------
// --------THIS IS AN AUTOMATICALLY GENERATED FILE. ANY MANUAL CHANGES WILL BE OVERWRITTEN!--------
// ------------------------------------------------------------------------------------------------
