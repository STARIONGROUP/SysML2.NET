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

        private readonly List<string> referenceTypes = new List<string>();

        private readonly List<string> valueTypes = new List<string>();

        private readonly List<string> enums = new List<string>();

        /// <summary>
        /// Inspect the Ecore Model and write the variation of data-types, enums and multiplicity to the console
        /// </summary>
        public void Inspect()
        {
            var rootPackage = DataModelLoader.Load();

            Console.WriteLine("----- ANALYSIS ------");

            foreach (var eClass in rootPackage.EClassifiers.OfType<EClass>().OrderBy(x => x.Name))
            {
                foreach (var structuralFeature in eClass.EStructuralFeatures)
                {
                    if (structuralFeature.Derived || structuralFeature.Transient)
                    {
                        continue;
                    }

                    if (structuralFeature is EReference reference)
                    {
                        var referenceType = $"{reference.LowerBound}:{reference.UpperBound}";
                        
                        if (!this.referenceTypes.Contains(referenceType))
                        {
                            this.referenceTypes.Add(referenceType);
                            this.interestingClasses.Add(eClass);

                            Console.WriteLine($"{eClass.Name} -- REF {referenceType}");
                        }
                        
                    }
                    
                    if (structuralFeature is EAttribute attribute)
                    {
                        if (structuralFeature.QueryIsEnum())
                        {
                            var @enum = $"{attribute.LowerBound}:{attribute.UpperBound}";
                            
                            if (!this.enums.Contains(@enum))
                            {
                                this.enums.Add(@enum);
                                this.interestingClasses.Add(eClass);

                                Console.WriteLine($"{eClass.Name} -- ENUM {@enum}");
                            }
                            
                        }
                        else
                        {
                            var valueType = $"{attribute.EType.Name}:{attribute.LowerBound}:{attribute.UpperBound}";
                            
                            if (!this.valueTypes.Contains(valueType))
                            {
                                this.valueTypes.Add(valueType);
                                this.interestingClasses.Add(eClass);

                                Console.WriteLine($"{eClass.Name} -- VAL {valueType}");
                            }
                        }
                    }
                }
            }

            Console.WriteLine("----- MULTIPLICITY RESULTS ------");

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

            Console.WriteLine("----- INTERESTING CLASSES ------");

            foreach (var @class in this.interestingClasses.OrderBy(x => x.Name))
            {
                Console.WriteLine($"class: {@class.Name}");
            }
        }

        /// <summary>
        /// Writes the references and attributes of the specified class to the console
        /// </summary>
        /// <param name="className">
        /// the name of the class that is to be inspected
        /// </param>
        public void Inspect(string className)
        {
            var rootPackage = DataModelLoader.Load();
            
            var eClass = rootPackage.EClassifiers.OfType<EClass>().Single(x => x.Name == className);

            Console.WriteLine($"{className}:");
            Console.WriteLine("----------------------------------");

            foreach (var structuralFeature in eClass.AllEStructuralFeaturesOrderByName)
            {
                if (structuralFeature.Derived || structuralFeature.Transient)
                {
                    continue;
                }

                if (structuralFeature is EReference reference)
                {
                    var referenceType = $"{reference.Name}:{reference.EType.Name} [{reference.LowerBound}..{reference.UpperBound}] - REFERENCE TYPE";
                    Console.WriteLine(referenceType);
                }

                if (structuralFeature is EAttribute attribute)
                {
                    if (structuralFeature.QueryIsEnum())
                    {
                        var @enum = $"{attribute.Name}:{attribute.EType.Name} [{attribute.LowerBound}..{attribute.UpperBound}] - ENUM TYPE";
                        Console.WriteLine(@enum);
                    }
                    else
                    {
                        var valueType = $"{attribute.Name}:{attribute.EType.Name} [{attribute.LowerBound}..{attribute.UpperBound}] - VALUETYPE";
                        Console.WriteLine(valueType);
                    }
                }
            }
            Console.WriteLine("-DERIVED--------------------------");
            foreach (var structuralFeature in eClass.AllEStructuralFeaturesOrderByName)
            {
                if (structuralFeature.Derived)
                {
                    if (structuralFeature is EReference reference)
                    {
                        var referenceType =
                            $"{reference.Name}:{reference.EType.Name} [{reference.LowerBound}..{reference.UpperBound}] - REFERENCE TYPE";
                        Console.WriteLine(referenceType);
                    }

                    if (structuralFeature is EAttribute attribute)
                    {
                        if (structuralFeature.QueryIsEnum())
                        {
                            var @enum =
                                $"{attribute.Name}:{attribute.EType.Name} [{attribute.LowerBound}..{attribute.UpperBound}] - ENUM TYPE";
                            Console.WriteLine(@enum);
                        }
                        else
                        {
                            var valueType =
                                $"{attribute.Name}:{attribute.EType.Name} [{attribute.LowerBound}..{attribute.UpperBound}] - VALUETYPE";
                            Console.WriteLine(valueType);
                        }
                    }
                }
            }

        }
    }
}
