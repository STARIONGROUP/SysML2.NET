// -------------------------------------------------------------------------------------------------
// <copyright file="TextualNotationRule.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.CodeGenerator.Grammar.Model
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Describe the content of a rule 
    /// </summary>
    public class TextualNotationRule
    {
        /// <summary>
        /// Get or set rule's name
        /// </summary>
        public string RuleName { get; set; }

        /// <summary>
        /// Gets or set the name of the KerML Element that the rule target
        /// </summary>
        public string TargetElementName { get; set; }
        
        /// <summary>
        /// Gets or set an optional <see cref="RuleParameter"/> 
        /// </summary>
        public RuleParameter Parameter { get; set; }
        
        /// <summary>
        /// Gets or sets the raw string that declares the rule
        /// </summary>
        public string RawRule { get; set; }

        /// <summary>
        /// Gets or the collection of <see cref="Alternatives"/> defined by the rule
        /// </summary>
        public List<Alternatives> Alternatives { get; } = [];
        
        /// <summary>
        /// Asserts that the current Rule acts has a dispatcher or not. A dispatcher needs to have index access to access element into a collection.
        /// </summary>
        public bool IsDispatcherRule => this.ComputeIsDispatcherRule();

        /// <summary>
        /// Asserts that the rule described assignment of multiple collection
        /// </summary>
        public bool IsMultiCollectionAssignment => this.ComputeIsMultiCollectionAssigment();

        /// <summary>
        /// Gets names of property that a multicollection assignment defines
        /// </summary>
        /// <returns>The collection of property names</returns>
        public IReadOnlyCollection<string> QueryMultiCollectionPropertiesName()
        {
            if (!this.IsMultiCollectionAssignment)
            {
                return Enumerable.Empty<string>().ToList();
            }
            
            var assignments = this.Alternatives.SelectMany(x => x.Elements).OfType<AssignmentElement>();
            return assignments.Where(x => x.Operator == "+=").DistinctBy(x => x.Property).Select(x => x.Property).ToList();
        }

        /// <summary>
        /// Computes the value of the <see cref="IsMultiCollectionAssignment"/> 
        /// </summary>
        /// <returns>The result of the assertion computation</returns>
        private bool ComputeIsMultiCollectionAssigment()
        {
            if (this.Alternatives.Count == 1)
            {
                return false;
            }

            var assignments = this.Alternatives.SelectMany(x => x.Elements).OfType<AssignmentElement>();
            return assignments.Where(x => x.Operator == "+=").DistinctBy(x => x.Property).Count() > 1;
        }

        /// <summary>
        /// Computes the assertion that the <see cref="TextualNotationRule"/> is a dispatcher or not
        /// </summary>
        /// <returns>The result of the assertion computation</returns>
        private bool ComputeIsDispatcherRule()
        {
            return this.Alternatives.Count > 1 && this.Alternatives.Any(x => x.Elements.OfType<AssignmentElement>().Any(a => a.Operator == "+="))
                   || this.Alternatives.Count == 1 && this.Alternatives[0].Elements.Count == 1 && this.Alternatives[0].Elements[0] is AssignmentElement { Operator: "+="};
        }

        /// <summary>
        /// Gets the <see cref="AssignmentElement"/> that requires a dispatcher 
        /// </summary>
        /// <returns>The <see cref="AssignmentElement"/></returns>
        public AssignmentElement GetAssignmentElementNeedingDispatcher()
        {
            if (!this.IsDispatcherRule)
            {
                return null;
            }

            if (this.Alternatives.Count == 1)
            {
                return this.Alternatives[0].Elements[0] as AssignmentElement;
            }

            return this.Alternatives.SelectMany(x => x.Elements).OfType<AssignmentElement>().FirstOrDefault(x => x.Operator == "+=");
        }
    }
}
