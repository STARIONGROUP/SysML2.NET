// -------------------------------------------------------------------------------------------------
// <copyright file="PortionKind.cs" company="RHEA System S.A.">
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

namespace SysML2.NET
{
    /// <summary>
    /// PortionKind is an enumeration of the possible special kinds of Occurrence portions that can be 
    /// represented by an OccurrenceUsage. 
    /// </summary>
    public enum PortionKind
    {
        /// <summary>
        /// A time slice of an Occurrence (a portion over time). 
        /// </summary>
        Timeslice = 0,

        /// <summary>
        /// A snapshot of an Occurrence (a time slice with zero duration). 
        /// </summary>
        Snapshot = 1,

    }
}
