// -------------------------------------------------------------------------------------------------
// <copyright file="GeneratorPayload.cs" company="RHEA System S.A.">
// 
//   Copyright 2022-2023 RHEA System S.A.
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

namespace SysML2.NET.CodeGenerator.Generators
{
    using System.Collections.Generic;
    using System.Linq;

    using ECoreNetto;

    /// <summary>
    /// represents the payload for the generators that require all <see cref="EEnum"/>,
    /// <see cref="EDataType"/> and <see cref="EClass"/>
    /// </summary>
    public class GeneratorPayload
    {
        /// <summary>
        /// initializes an instance of the <see cref="GeneratorPayload"/> class.
        /// </summary>
        /// <param name="enums">
        /// the <see cref="EEnum"/>s in the data model
        /// </param>
        /// <param name="dataTypes">
        /// the <see cref="EDataType"/>s in the data model
        /// </param>
        /// <param name="classes">
        /// the <see cref="EClass"/>es in the data model
        /// </param>
        public GeneratorPayload(IEnumerable<EEnum> enums, IEnumerable<EDataType> dataTypes, IEnumerable<EClass> classes)
        {
            this.Enums = enums.ToArray();
            this.DataTypes = dataTypes.ToArray();
            this.Classes = classes.ToArray();
        }

        /// <summary>
        /// Gets the array of <see cref="EEnum"/>
        /// </summary>
        public EEnum[] Enums { get; private set; }

        /// <summary>
        /// Gets the array of <see cref="EDataType"/>
        /// </summary>
        public EDataType[] DataTypes { get; private set; }

        /// <summary>
        /// Gets the array of <see cref="EClass"/>
        /// </summary>
        public EClass[] Classes { get; private set; }
    }
}
