// -------------------------------------------------------------------------------------------------
// <copyright file="ArchiveSessionTestFixture.cs" company="Starion Group S.A.">
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
            return Path.Combine(TestContext.CurrentContext.TestDirectory, "TestData", "Kernel_Semantic_Library-1.0.0.kpar");
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
