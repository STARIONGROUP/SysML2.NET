// -------------------------------------------------------------------------------------------------
// <copyright file="GuidComparerTestFixture.cs" company="Starion Group S.A.">
//
//    Copyright 2022-2026 Starion Group S.A.
//
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
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

namespace SysML2.NET.Extensions.Tests.Comparers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using NUnit.Framework;

    using SysML2.NET.Extensions.Comparers;

    [TestFixture]
    public class GuidComparerTestFixture
    {
        [Test]
        public void Verify_that_List_of_Guid_is_ordered()
        {
            var id_1 = Guid.Parse("622d8e0f-ed5e-4dde-92b8-97ff06e69110");
            var id_2 = Guid.Parse("90e43d0c-edf8-4630-963b-90e6530ac9db");
            var id_3 = Guid.Parse("47b3abc1-ce06-40ef-8ea6-466e7eaccccd");

            Assert.That(-1, Is.EqualTo(id_1.CompareTo(id_2)));

            var ids = new List<Guid> { id_1, id_2, id_3 };
            var ordered = new List<Guid> { id_3, id_1, id_2 };

            var result = ids.OrderBy(x => x, new GuidComparer());

            Assert.That(ordered, Is.EqualTo(result));
        }
    }
}
