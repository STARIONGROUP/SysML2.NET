// -------------------------------------------------------------------------------------------------
// <copyright file="OccurrenceUsageExtensionsTestFixture.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.Tests.Extend
{
    using System;

    using NUnit.Framework;

    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Kernel.Classes;
    using SysML2.NET.Core.POCO.Kernel.DataTypes;
    using SysML2.NET.Core.POCO.Systems.Occurrences;
    using SysML2.NET.Extensions;

    [TestFixture]
    public class OccurrenceUsageExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeOccurrenceDefinition()
        {
            Assert.That(() => ((IOccurrenceUsage)null).ComputeOccurrenceDefinition(), Throws.TypeOf<ArgumentNullException>());

            // No typings → empty list.
            var emptySubject = new OccurrenceUsage();

            Assert.That(emptySubject.ComputeOccurrenceDefinition(), Is.Empty);

            // A mix of Class-kind types (Class + OccurrenceDefinition, which IS an IClass) and a
            // non-Class type (DataType) → only the Class-kind types survive the selectByKind(Class) filter.
            var occurrenceUsage = new OccurrenceUsage();
            var bareClass = new Class();
            var occurrenceDefinition = new OccurrenceDefinition();
            var dataType = new DataType();
            occurrenceUsage.AssignOwnership(new FeatureTyping { Type = bareClass });
            occurrenceUsage.AssignOwnership(new FeatureTyping { Type = occurrenceDefinition });
            occurrenceUsage.AssignOwnership(new FeatureTyping { Type = dataType });

            var result = occurrenceUsage.ComputeOccurrenceDefinition();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(result, Has.Count.EqualTo(2));
                Assert.That(result, Does.Contain(bareClass));
                Assert.That(result, Does.Contain(occurrenceDefinition));
                Assert.That(result, Does.Not.Contain(dataType));
            }
        }

        [Test]
        public void VerifyComputeIndividualDefinition()
        {
            Assert.That(() => ((IOccurrenceUsage)null).ComputeIndividualDefinition(), Throws.TypeOf<ArgumentNullException>());

            // No occurrenceDefinition → null.
            var emptySubject = new OccurrenceUsage();

            Assert.That(emptySubject.ComputeIndividualDefinition(), Is.Null);

            // An OccurrenceDefinition typing that is NOT individual → null (isIndividual filter).
            var nonIndividualSubject = new OccurrenceUsage();
            nonIndividualSubject.AssignOwnership(new FeatureTyping { Type = new OccurrenceDefinition { IsIndividual = false } });

            Assert.That(nonIndividualSubject.ComputeIndividualDefinition(), Is.Null);

            // A single individual OccurrenceDefinition among a non-individual one → returns the individual one.
            var singleIndividualSubject = new OccurrenceUsage();
            var individualDefinition = new OccurrenceDefinition { IsIndividual = true };
            singleIndividualSubject.AssignOwnership(new FeatureTyping { Type = new OccurrenceDefinition { IsIndividual = false } });
            singleIndividualSubject.AssignOwnership(new FeatureTyping { Type = individualDefinition });

            Assert.That(singleIndividualSubject.ComputeIndividualDefinition(), Is.SameAs(individualDefinition));

            // Two individual OccurrenceDefinitions → returns the FIRST (OCL ->first() contract).
            var twoIndividualSubject = new OccurrenceUsage();
            var firstIndividual = new OccurrenceDefinition { IsIndividual = true };
            var secondIndividual = new OccurrenceDefinition { IsIndividual = true };
            twoIndividualSubject.AssignOwnership(new FeatureTyping { Type = firstIndividual });
            twoIndividualSubject.AssignOwnership(new FeatureTyping { Type = secondIndividual });

            Assert.That(twoIndividualSubject.ComputeIndividualDefinition(), Is.SameAs(firstIndividual));
        }
    }
}
