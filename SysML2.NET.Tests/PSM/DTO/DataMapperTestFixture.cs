// ------------------------------------------------------------------------------------------------
// <copyright file="DataMapperTestFixture.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Tests.PSM.DTO
{
    using System;

    using NUnit.Framework;

    using SysML2.NET.Common;
    using SysML2.NET.Core.DTO.Root.Namespaces;
    using SysML2.NET.PSM.DTO;

    [TestFixture]
    public class DataMapperTestFixture
    {
        [Test]
        public void VerifyToPim()
        {
            Assert.That(DataMapper.ToPim(null), Is.Null);
            Assert.That(() => DataMapper.ToPim(new UnmodelledDataRequest()), Throws.TypeOf<NotSupportedException>());

            var element = new Namespace { Id = Guid.NewGuid() };
            var externalData = new ExternalDataRequest { Id = Guid.NewGuid() };
            var externalRelationship = new ExternalRelationshipRequest { Id = Guid.NewGuid(), Language = "the language" };
            var projectUsage = new ProjectUsageRequest { Id = Guid.NewGuid(), UsedProject = Guid.NewGuid() };

            using (Assert.EnterMultipleScope())
            {
                Assert.That(DataMapper.ToPim(element), Is.SameAs(element));
                Assert.That(DataMapper.ToPim(externalData), Is.TypeOf<SysML2.NET.PIM.DTO.ExternalData>().With.Property("Id").EqualTo(externalData.Id));
                Assert.That(DataMapper.ToPim(externalRelationship), Is.TypeOf<SysML2.NET.PIM.DTO.ExternalRelationship>().With.Property("Language").EqualTo("the language"));
                Assert.That(DataMapper.ToPim(projectUsage), Is.TypeOf<SysML2.NET.PIM.DTO.ProjectUsage>().With.Property("Id").EqualTo(projectUsage.Id));
            }
        }

        [Test]
        public void VerifyToRequest()
        {
            Assert.That(DataMapper.ToRequest(null), Is.Null);
            Assert.That(() => DataMapper.ToRequest(new ExternalData { Id = Guid.NewGuid() }), Throws.TypeOf<NotSupportedException>());

            var element = new Namespace { Id = Guid.NewGuid() };
            var externalData = new SysML2.NET.PIM.DTO.ExternalData { Id = Guid.NewGuid() };
            var externalRelationship = new SysML2.NET.PIM.DTO.ExternalRelationship { Id = Guid.NewGuid(), Language = "the language" };
            var projectUsage = new SysML2.NET.PIM.DTO.ProjectUsage { Id = Guid.NewGuid() };

            using (Assert.EnterMultipleScope())
            {
                Assert.That(DataMapper.ToRequest(element), Is.SameAs(element));
                Assert.That(DataMapper.ToRequest(externalData), Is.TypeOf<ExternalDataRequest>().With.Property("Id").EqualTo(externalData.Id));
                Assert.That(DataMapper.ToRequest(externalRelationship), Is.TypeOf<ExternalRelationshipRequest>().With.Property("Language").EqualTo("the language"));
                Assert.That(DataMapper.ToRequest(projectUsage), Is.TypeOf<ProjectUsageRequest>().With.Property("Id").EqualTo(projectUsage.Id));
            }
        }

        private sealed class UnmodelledDataRequest : IDataRequest
        {
        }
    }
}
