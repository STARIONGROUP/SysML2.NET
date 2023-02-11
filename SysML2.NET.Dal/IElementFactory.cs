// -------------------------------------------------------------------------------------------------
// <copyright file="IElementFactory.cs" company="RHEA System S.A.">
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

namespace SysML2.NET.Dal
{
	using System; 

	/// <summary>
	/// The purpose of the <see cref="IElementFactory"/> is to create a <see cref="Core.POCO.IElement"/> POCO
	/// based on a <see cref="Core.DTO.IElement"/> DTO
	/// </summary>
	public interface IElementFactory
	{
		/// <summary>
		/// Creates a <see cref="Core.POCO.IElement"/> POCO based on a <see cref="Core.DTO.IElement"/>
		/// </summary>
		/// <param name="dto">
		/// the source <see cref="Core.DTO.IElement"/> DTO
		/// </param>
		/// <returns>
		/// a <see cref="Core.POCO.IElement"/> POCO
		/// </returns>
		/// <exception cref="ArgumentNullException">
		/// thrown when <paramref name="dto"/> is null
		/// </exception>
		Core.POCO.IElement Create(Core.DTO.IElement dto);
	}
}
