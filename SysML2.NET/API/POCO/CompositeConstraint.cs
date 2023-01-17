// -------------------------------------------------------------------------------------------------
// <copyright file="CompositeConstraint.cs" company="RHEA System S.A.">
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
    using System.Collections.Generic;

    /// <summary>
    /// a subtype of <see cref="Constraint"/> that represents complex conditions composed of two or more
    /// <see cref="CompositeConstraint"/>s or <see cref="PrimitiveConstraint"/>s using logical AND or OR operators
    /// </summary>
    public class CompositeConstraint
    {
        public List<Constraint> Constraint { get; set; }
    }
}
