// -------------------------------------------------------------------------------------------------
// <copyright file="TypeTextualNotationBuilder.cs" company="Starion Group S.A.">
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

namespace SysML2.NET.TextualNotation
{
    using System.Text;

    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Root.Namespaces;

    /// <summary>
    /// Hand-coded part of the <see cref="TypeTextualNotationBuilder"/>
    /// </summary>
    public static partial class TypeTextualNotationBuilder
    {
        /// <summary>
        /// Build the TypeBodyElement alternatives part since used NonTerminal target same type
        /// </summary>
        /// <param name="poco">The <see cref="IType"/></param>
        /// <param name="stringBuilder">The <see cref="StringBuilder"/></param>
        private static void BuildTypeBodyElementAlternatives(IType poco, StringBuilder stringBuilder)
        {
            foreach (var ownedRelationship in poco.OwnedRelationship)
            {
                switch (ownedRelationship)
                {
                    case OwningMembership owningMembership:
                        OwningMembershipTextualNotationBuilder.BuildNonFeatureMember(owningMembership, stringBuilder);
                        break;
                    case FeatureMembership featureMembership:
                        OwningMembershipTextualNotationBuilder.BuildFeatureMember(featureMembership, stringBuilder);
                        break;
                    case Membership membership:
                        MembershipTextualNotationBuilder.BuildAliasMember(membership, stringBuilder);
                        break;
                    case IImport import:
                        ImportTextualNotationBuilder.BuildImport(import, stringBuilder);
                        break;
                }
            }
        }
    }
}
