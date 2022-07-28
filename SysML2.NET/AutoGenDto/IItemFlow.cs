// -------------------------------------------------------------------------------------------------
// <copyright file="IItemFlow.cs" company="RHEA System S.A.">
//
// Copyright 2022 RHEA System S.A.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.DTO
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// An ItemFlow is a Step that represents the transfer of objects or values from one Feature to another.
    /// ItemFlows can take non-zero time to complete.An ItemFlow must be typed by the Interaction Transfer
    /// from the Kernel library, or a specialization of it.
    /// </summary>
    public partial interface IItemFlow : IConnector, IStep
    {
    }
}
