// ------------------------------------------------------------------------------------------------
// <copyright file="AuthenticationStatusKind.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
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