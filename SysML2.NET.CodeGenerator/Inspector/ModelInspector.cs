// -------------------------------------------------------------------------------------------------
// <copyright file="ModelInspector.cs" company="RHEA System S.A.">
//
//   Copyright 2022-2023 RHEA System S.A.
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//       http://www.apache.org/licenses/LICENSE-2.0
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.CodeGenerator.Inspector
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    
    using ECoreNetto;
    using ECoreNetto.Extensions;

    /// <summary>
    /// The purpose of the <see cref="ModelInspector"/> is to iterate through the model and report on the various kinds of
    /// patters that exist in the ECore model that need to be taken into account for code-generation
    /// </summary>
    public class ModelInspector
    {
        private readonly HashSet<EClass> interestingClasses = new HashSet<EClass>();

        private readonly HashSet<string> referenceTypes = new HashSet<string>();

        private readonly HashSet<string> valueTypes = new HashSet<string>();

        private readonly HashSet<string> enums = new HashSet<string>();

        public void Inspect()
        {
            var rootPackage = DataModelLoader.Load();

            foreach (var eClass in rootPackage.EClassifiers.OfType<EClass>())
            {
                foreach (var structuralFeature in eClass.EStructuralFeatures)
                {
                    if (structuralFeature is EReference reference)
                    {
                        var referenceType = $"{reference.LowerBound}:{reference.UpperBound}";

                        if (!this.referenceTypes.Contains(referenceType))
                        {
                            if (this.referenceTypes.Add(referenceType))
                            {
                                this.interestingClasses.Add(eClass);
                            }
                        }
                    }
                    
                    if (structuralFeature is EAttribute attribute)
                    {
                        if (structuralFeature.QueryIsEnum())
                        {
                            var @enum = $"{attribute.LowerBound}:{attribute.UpperBound}";
                            if (this.enums.Add(@enum))
                            {
                                this.interestingClasses.Add(eClass);
                            }
                        }
                        else
                        {
                            var valueType = $"{attribute.EType.Name}:{attribute.LowerBound}:{attribute.UpperBound}";

                            if (this.valueTypes.Add(valueType))
                            {
                                this.interestingClasses.Add(eClass);
                            }
                        }
                    }
                }
            }


            var orderedReferenceTypes = this.referenceTypes.Order();

            foreach (var referenceType in orderedReferenceTypes)
            {
                Console.WriteLine($"reference type: {referenceType}");
            }

            var orderedEnums = this.enums.Order();

            foreach (var @enum in orderedEnums)
            {
                Console.WriteLine($"enum type: {@enum}");
            }

            var orderedValueTypes = this.valueTypes.Order();

            foreach (var valueType in orderedValueTypes)
            {
                Console.WriteLine($"value type: {valueType}");
            }

            foreach (var @class in this.interestingClasses.OrderBy(x => x.Name))
            {
                Console.WriteLine($"class: {@class.Name}");
            }
        }
    }
}
