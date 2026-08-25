// -------------------------------------------------------------------------------------------------
// <copyright file="Assembler.cs" company="Starion Group S.A.">
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
        /// <remarks>
        /// When <paramref name="dtos"/> carries more than one DTO with the same identifier, the first occurrence
        /// is the one that is added to the Cache and every subsequent duplicate is ignored.
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="dtos"/> is null
        /// </exception>
        public void Synchronize(IEnumerable<Core.DTO.Root.Elements.IElement> dtos)
        {
            if (dtos == null)
            {
                throw new ArgumentNullException(nameof(dtos), $"The {nameof(dtos)} may not be null");
            }

            // the DTOs are walked three times, materialize once so that a lazy sequence is not re-evaluated on every pass
            var elements = dtos as IReadOnlyList<Core.DTO.Root.Elements.IElement> ?? dtos.ToList();

            var isTraceEnabled = this.logger.IsEnabled(LogLevel.Trace);

            var sw = Stopwatch.StartNew();

            var deletedIdentifiers = new List<Guid>();

            // update all POCOs based on provided DTOs, the result is a list unique identifiers of objects that may be removed
            this.logger.LogDebug("Update Value properties of POCO and Removed deleted Reference Properties");

            foreach (var dto in elements)
            {
                if (this.Cache.TryGetValue(dto.Id, out var lazyPoco))
                {
                    deletedIdentifiers.AddRange(lazyPoco.Value.UpdateValueAndRemoveDeletedReferenceProperties(dto));
                }
            }

            this.logger.LogDebug("A total of {DeletedCount} identifiers have been processed in {Elapsed} [ms] and ready to be deleted", deletedIdentifiers.Count, sw.ElapsedMilliseconds);

            // removed POCOs that are up for deletion
            foreach (var identifier in deletedIdentifiers)
            {
                if (!this.Cache.TryRemove(identifier, out var deletedLazyPoco))
                {
                    this.logger.LogWarning("The element with identifier {Identifier} was not deleted as it could not be found in the cache", identifier);
                    continue;
                }

                if (isTraceEnabled)
                {
                    this.logger.LogTrace("{PocoType} with identifier {Identifier} was deleted", deletedLazyPoco.Value.GetType().Name, identifier);
                }
            }

            sw.Restart();
            this.logger.LogDebug("Add new POCOs to dictionary based on DTOs");

            var elementFactory = new ElementFactory();
            var addedCount = 0;

            foreach (var dto in elements.Where(element => !this.Cache.ContainsKey(element.Id)))
            {
                var poco = elementFactory.Create(dto);

                this.Cache.AddOrUpdate(poco.Id, new Lazy<Core.POCO.Root.Elements.IElement>(() => poco), (key, oldValue) => oldValue);

                addedCount++;

                if (isTraceEnabled)
                {
                    this.logger.LogTrace("{PocoType}:{Identifier} added to Cache", poco.GetType().Name, poco.Id);
                }
            }

            this.logger.LogDebug("A total of {AddedCount} POCOs have been added to the Cache in {Elapsed} [ms]", addedCount, sw.ElapsedMilliseconds);

            sw.Restart();
            this.logger.LogDebug("Update POCO reference properties");

            foreach (var dto in elements)
            {
                if (this.Cache.TryGetValue(dto.Id, out var lazyPoco))
                {
                    lazyPoco.Value.UpdateReferenceProperties(dto, this.Cache);
                }
            }

            this.logger.LogDebug("POCO reference properties updated in {Elapsed} [ms]", sw.ElapsedMilliseconds);

            sw.Stop();
        }
    }
}
