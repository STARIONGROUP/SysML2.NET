// -------------------------------------------------------------------------------------------------
// <copyright file="CommitReference.cs" company="RHEA System S.A.">
// 
//   Copyright 2022 RHEA System S.A.
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

namespace SysML2.NET.API.POCO
{
    /// <summary>
    /// An abstract subclass of <see cref="Record"/> that references a specific <see cref="Commit"/> (Commit Reference.referencedCommit). Project.commit is the set of all the Commit records for a given Project.
    /// <see cref="Project.CommitRefererence"/> identifies specific <see cref="Commit"/> records in a <see cref="Project"/> that provide the context for navigating the
    /// <see cref="IData"/> in a Project. Two special types of <see cref="CommitReference"/> are <see cref="Branch"/> and <see cref="Tag"/>
    /// </summary>
    public abstract class CommitReference : Record
    {
    }
}
