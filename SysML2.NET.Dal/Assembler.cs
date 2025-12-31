// -------------------------------------------------------------------------------------------------
// <copyright file="Assembler.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Dal
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Logging.Abstractions;

    /// <summary>
    /// The purpose of the <see cref="IAssembler"/> is to assemble a SysML2.NET POCO object graph from a
    /// list of data-transfer-objects (DTO)
    /// </summary>
    public class Assembler : IAssembler
    {
        /// <summary>
        /// The <see cref="ILogger"/> used to log
        /// </summary>
        private readonly ILogger<Assembler> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="Assembler"/> class.
        /// </summary>
        public Assembler(ILoggerFactory loggerFactory = null)
        {
            this.logger = loggerFactory == null ? NullLogger<Assembler>.Instance : loggerFactory.CreateLogger<Assembler>();

            this.Cache = new ConcurrentDictionary<Guid, Lazy<Core.POCO.Root.Elements.IElement>>();
        }

        /// <summary>
        /// Gets the Cache that contains all the <see cref="Core.POCO.Root.Elements.IElement"/>s
        /// </summary>
        public ConcurrentDictionary<Guid, Lazy<Core.POCO.Root.Elements.IElement>> Cache { get; private set; }

        /// <summary>
        /// Synchronize the Cache based on the provided <paramref name="dtos"/>
        /// </summary>
        /// <param name="dtos">
        /// the DTOs used to update the cache with
        /// </param>
        public void Synchronize(IEnumerable<Core.DTO.Root.Elements.IElement> dtos)
        {
            if (dtos == null)
            {
                throw new ArgumentNullException(nameof(dtos), $"The {nameof(dtos)} may not be null");
            }

            var sw = Stopwatch.StartNew();

            var deletedIdentifiers = new List<Guid>();

            // update all POCOs based on provided DTOs, the result is a list unique identifiers of objects that may be removed
            this.logger.LogDebug("Update Value properties of POCO and Removed deleted Reference Properties");
            foreach (var dto in dtos)
            {
                if (this.Cache.TryGetValue(dto.Id, out var lazyPoco))
                {
                    var poco = lazyPoco.Value;
                    var deletedPocos = poco.UpdateValueAndRemoveDeletedReferenceProperties(dto).ToList();
                    deletedIdentifiers.AddRange(deletedPocos);
                }
            }
            this.logger.LogDebug("A total of {0} identifiers have been processed in {1} [ms] and ready to be deleted", deletedIdentifiers, sw.ElapsedMilliseconds);

            // removed POCOs that are up for deletion
            foreach (var identifier in deletedIdentifiers)
            {
                Lazy<Core.POCO.Root.Elements.IElement> lazyPoco;
                if (this.Cache.TryRemove(identifier, out lazyPoco))
                {
                    this.logger.LogTrace("{0} with identifier {1} was deleted", lazyPoco.Value.GetType().Name, identifier);
                }
                else
                {
                    this.logger.LogWarning("{0} with identifier {1} was not deleted as it could not be found in the cache", lazyPoco.Value.GetType().Name, identifier);
                }
            }

            sw.Restart();
            this.logger.LogDebug("Add new POCOs to dictionary based on DTOs");
            var elementFactory = new ElementFactory();
            var existingIdentifiers = this.Cache.Keys.ToList();
            var dtoIdentifiers = dtos.Select(x => x.Id).ToList();
            var newIdentifiers = dtoIdentifiers.Except(existingIdentifiers);
            foreach (var identifier in newIdentifiers)
            {
                var dto = dtos.Single(x => x.Id == identifier);
                var poco = elementFactory.Create(dto);
                this.Cache.AddOrUpdate(poco.Id, new Lazy<Core.POCO.Root.Elements.IElement>(() => poco), (key, oldValue) => oldValue);
                this.logger.LogTrace("{0}:{1} added to Cache", poco.GetType().Name, poco.Id);
            }
            this.logger.LogDebug("A total of {0} POCOs have been added to the Cache in {1} [ms]", newIdentifiers.Count(), sw.ElapsedMilliseconds);

            sw.Restart();
            this.logger.LogDebug("Update POCO reference properties");
            foreach (var dto in dtos)
            {
                Lazy<Core.POCO.Root.Elements.Element> lazyPoco;
                if (this.Cache.TryGetValue(dto.Id, out lazyPoco))
                {
                    var poco = lazyPoco.Value;
                    poco.UpdateReferenceProperties(dto, this.Cache);
                }
            }
            this.logger.LogDebug("POCO reference properties updated in {0} [ms]", sw.ElapsedMilliseconds);
        }
    }
}
