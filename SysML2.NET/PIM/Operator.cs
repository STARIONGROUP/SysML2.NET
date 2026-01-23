// -------------------------------------------------------------------------------------------------
// <copyright file="Operator.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.PIM
{
    /// <summary>
    /// Enumeration whose literals are mathematical operators
    /// </summary>
    public enum Operator
    {
        /// <summary>
        /// To assert that it is an instance of a specific type
        /// </summary>
        instanceOf,

        /// <summary>
        /// To assert that it is equal to 
        /// </summary>
        equalto,

        /// <summary>
        /// To assert that it is less than
        /// </summary>
        lessthan,

        /// <summary>
        /// To assert that it is less than or equal to 
        /// </summary>
        lessthanorequalto,

        /// <summary>
        /// To assert that it is greater than 
        /// </summary>
        greaterthan,

        /// <summary>
        /// To assert that it is greater than or equal to 
        /// </summary>
        greaterthanorequalto,

        /// <summary>
        /// To assert that it is in
        /// </summary>
        @in
    }
}
