// ------------------------------------------------------------------------------------------------
// <copyright file="ArchiveSessionTestFixture.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SysML2.NET.Kpar.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.IO.Compression;
    
    using NUnit.Framework;

    using SysML2.NET.ModelInterchange;
    
    [TestFixture]
    public class ArchiveSessionTestFixture
    {
        private ArchiveSession archiveSession;
        
        private static string GetKparPath()
        {
            return Directory.GetFiles(Path.Combine(TestContext.CurrentContext.TestDirectory, "TestData"), "Kernel_Semantic_Library-*.kpar")[0];
        }
        
        [SetUp]
        public void SetUp()
        {
            var kparPath = GetKparPath();
            using var fileStream = File.OpenRead(kparPath);
            
            var zip = new ZipArchive(fileStream, ZipArchiveMode.Read, false);
            var archive = new Archive();
            archive.Metadata = new InterchangeProjectMetadata(); 
            archive.Metadata.Index.Add("123","456");
                
            this.archiveSession = new ArchiveSession(fileStream, zip, archive);
        }

        [Test]
        public void Verify_that_when_on_OpenModel_index_is_null_or_whitespace_exception_is_thrown()
        {
            Assert.That(() =>this.archiveSession.OpenModel(null), Throws.TypeOf<ArgumentException>());
            Assert.That(() =>this.archiveSession.OpenModel(""), Throws.TypeOf<ArgumentException>());
        }
        
        [Test]
        public void Verify_that_when_on_OpenModel_index_is_does_not_exist_exception_is_thrown()
        {
            Assert.That(() =>this.archiveSession.OpenModel("starion"), Throws.TypeOf<KeyNotFoundException>());
        }
        
        [Test]
        public void Verify_that_when_on_OpenEntry_path_is_null_or_whitespace_exception_is_thrown()
        {
            Assert.That(() =>this.archiveSession.OpenEntry(null), Throws.TypeOf<ArgumentException>());
            Assert.That(() =>this.archiveSession.OpenEntry(""), Throws.TypeOf<ArgumentException>());
        }
    }
}
