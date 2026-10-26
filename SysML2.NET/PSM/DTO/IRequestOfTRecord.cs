// -------------------------------------------------------------------------------------------------
// <copyright file="IRequestOfTRecord.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.PSM.DTO
{
    /// <summary>
    /// Marker interface implemented by the request models that declare a Platform Independent Model counterpart
    /// </summary>
    /// <typeparam name="TRecord">
    /// The Platform Independent Model type that this request model corresponds to.
    /// </typeparam>
    /// <remarks>
    /// Requests whose payload the Platform Independent Model cannot carry implement the non generic <see cref="IRequest"/> only.
    /// </remarks>
    public interface IRequest<TRecord> : IRequest, IPimMappable<TRecord>
    {
    }
}
