// ------------------------------------------------------------------------------------------------
// <copyright file="SysMLMessagePackSerializerOptions.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Serializer.MessagePack
{
    using global::MessagePack;

    /// <summary>
    /// The <see cref="MessagePackSerializerOptions"/> that tell the generated formatters whether derived properties are exchanged.
    /// </summary>
    public class SysMLMessagePackSerializerOptions : MessagePackSerializerOptions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SysMLMessagePackSerializerOptions"/> class from existing options.
        /// </summary>
        /// <param name="copyFrom">The <see cref="MessagePackSerializerOptions"/> to copy</param>
        /// <param name="includeDerivedProperties">Whether derived properties are written and read</param>
        public SysMLMessagePackSerializerOptions(MessagePackSerializerOptions copyFrom, bool includeDerivedProperties)
            : base(copyFrom)
        {
            this.IncludeDerivedProperties = includeDerivedProperties;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SysMLMessagePackSerializerOptions"/> class as a copy of other options.
        /// </summary>
        /// <param name="copyFrom">The <see cref="SysMLMessagePackSerializerOptions"/> to copy</param>
        protected SysMLMessagePackSerializerOptions(SysMLMessagePackSerializerOptions copyFrom)
            : base(copyFrom)
        {
            this.IncludeDerivedProperties = copyFrom.IncludeDerivedProperties;
        }

        /// <summary>
        /// Gets a value indicating whether derived properties are written and read.
        /// </summary>
        public bool IncludeDerivedProperties { get; }

        /// <summary>
        /// Creates a copy of these options that keeps <see cref="IncludeDerivedProperties"/>.
        /// </summary>
        /// <returns>The copy</returns>
        protected override MessagePackSerializerOptions Clone()
        {
            return new SysMLMessagePackSerializerOptions(this);
        }
    }
}
