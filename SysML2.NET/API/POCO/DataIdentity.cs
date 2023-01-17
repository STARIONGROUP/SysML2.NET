// -------------------------------------------------------------------------------------------------
// <copyright file="DataIdentity.cs" company="RHEA System S.A.">
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

namespace SysML2.NET.API.POCO
{
    /// <summary>
    /// A subclass of <see cref="Record"/> that represents a unique, version-independent representation of <see cref="IData"/>
    /// through its lifecycle. A <see cref="DataIdentity"/> is associated with 1 or more <see cref="IData"/> Version
    /// records that represent different versions of the same <see cref="IData"/>.
    /// </summary>
    public class DataIdentity : Record
    {
    }
}
