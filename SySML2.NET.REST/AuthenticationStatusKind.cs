﻿// -------------------------------------------------------------------------------------------------
// <copyright file="AuthenticationStatusKind.cs" company="Starion Group S.A.">
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

namespace SySML2.NET.REST
{
    /// <summary>
    /// Enumeration that defines possible status for the authentication process
    /// </summary>
    public enum AuthenticationStatusKind
    {
        /// <summary>
        /// Defaut status, when no authentication process has been performed 
        /// </summary>
        None,

        /// <summary>
        /// Status when the authentication process is in progress
        /// </summary>
        Authenticating,

        /// <summary>
        /// Status when the authentication process ends succesfully
        /// </summary>
        Success,

        /// <summary>
        /// Status when the authentication process failed
        /// </summary>
        Fail
    }
}