// -------------------------------------------------------------------------------------------------
// <copyright file="Serializer.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Serializer.Xmi
{
    using System.Collections.Generic;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;
    
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Logging.Abstractions;
    
    using SysML2.NET.Common;
    using SysML2.NET.Core.POCO.Root.Namespaces;

    /// <summary>
    /// The purpose of the <see cref="ISerializer"/> is to write an <see cref="INamespace"/>
    /// as XMI to a <see cref="Stream"/>
    /// </summary>
    public class Serializer : ISerializer
    {
        /// <summary>
        /// The injected <see cref="ILogger{Serializer}" /> to produce logs statement
        /// </summary>
        private readonly ILogger<Serializer> logger;

        /// <summary>
        /// The injected <see cref="ILoggerFactory " /> used to set up logging
        /// </summary>
        private readonly ILoggerFactory loggerFactory;
        
        /// <summary>Initializes a new instance of the <see cref="Serializer"></see> class.</summary>
        /// <param name="loggerFactory">The injected <see cref="ILoggerFactory " /> used to set up logging</param>
        public Serializer(ILoggerFactory loggerFactory)
        {
            this.loggerFactory = loggerFactory ?? NullLoggerFactory.Instance;
            this.logger = this.loggerFactory.CreateLogger<Serializer>();
        }
        
        /// <summary>
        /// Serialize an <see cref="INamespace"/> as XMI to a target <see cref="Stream"/>
        /// </summary>
        /// <param name="namespace">
        /// The <see cref="INamespace"/> that shall be serialized
        /// </param>
        /// <param name="includeDerivedProperties">
        /// Asserts that derived properties should also be part of the serialization
        /// </param>
        /// <param name="stream">
        /// The target <see cref="Stream"/>
        /// </param>
        public void Serialize(INamespace @namespace, bool includeDerivedProperties, Stream stream)
        {
            // if the namespace is not an anonymouse namespace, then first create that and make the
            // provide namespace owned by the anonymouse
            
            throw new System.NotImplementedException();
        }   

        /// <summary>
        /// Asynchronously serialize an <see cref="INamespace"/> as XMI to a target <see cref="Stream"/>
        /// </summary>
        /// <param name="namespace">
        /// The <see cref="INamespace"/> that shall be serialized
        /// </param>
        /// <param name="stream">
        /// The target <see cref="Stream"/>
        /// </param>
        /// <param name="includeDerivedProperties">
        /// Asserts that derived properties should also be part of the serialization
        /// </param>
        /// <param name="cancellationToken">
        /// The <see cref="CancellationToken"/> used to cancel the operation
        /// </param>
        public Task SerializeAsync(INamespace @namespace, bool includeDerivedProperties, Stream stream, CancellationToken cancellationToken)
        {
            // if the namespace is not an anonymouse namespace, then first create that and make the
            // provide namespace owned by the anonymouse
            
            throw new System.NotImplementedException();
        }
    }
}
