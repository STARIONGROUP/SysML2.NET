// -------------------------------------------------------------------------------------------------
// <copyright file="ISession.cs" company="Starion Group S.A.">
// 
//   Copyright 2022-2026 Starion Group S.A.
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
    using System.Threading;
    using System.Threading.Tasks;
    
    /// <summary>
    /// Definition of the <see cref="ISession"/> used to provide an abstraction over the SysML2 model server REST API
    /// </summary>
    public interface ISession
    {
        /// <summary>
        /// Opens a connection to the SysML2 model server REST API
        /// </summary>
        /// <param name="username">The username used to login a SysML2 model server</param>
        /// <param name="password">The password (secret) used to login a SysML2 model server</param>
        /// <param name="uri">
        /// The uri of the model-server
        /// </param>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken"/> used to cancel the operation
        /// </param>
        Task Open(string username, string password, string uri, CancellationToken cancellationToken);

        /// <summary>
        /// Closes a connection to the SysML2 model server REST API
        /// </summary>
        void Close();
    }
}
