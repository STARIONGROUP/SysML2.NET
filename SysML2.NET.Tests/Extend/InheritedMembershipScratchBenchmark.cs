namespace SysML2.NET.Tests.Extend
{
    using System.Collections.Generic;
    using System.Diagnostics;

    using NUnit.Framework;

    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Core.Root.Namespaces;
    using SysML2.NET.Extensions;

    using Type = SysML2.NET.Core.POCO.Core.Types.Type;

    [TestFixture]
    [Explicit("Scratch measurement, not a regression test.")]
    public class InheritedMembershipScratchBenchmark
    {
        [Test]
        public void MeasureBranchingHierarchy()
        {
            foreach (var depth in new[] { 6, 8, 10, 12, 16, 20, 24, 32 })
            {
                var root = BuildHierarchy(depth, branching: 2);

                var stopwatch = Stopwatch.StartNew();
                var count = root.inheritedMembership.Count;
                stopwatch.Stop();

                TestContext.WriteLine($"depth={depth} branching=2 -> {count} memberships in {stopwatch.ElapsedMilliseconds} ms");
            }
        }

        private static IType BuildHierarchy(int depth, int branching)
        {
            var level = new List<IType>();

            for (var leafIndex = 0; leafIndex < branching; leafIndex++)
            {
                level.Add(BuildTypeWithMember($"leaf{leafIndex}"));
            }

            for (var currentDepth = 0; currentDepth < depth; currentDepth++)
            {
                var next = new List<IType>();

                for (var siblingIndex = 0; siblingIndex < branching; siblingIndex++)
                {
                    var type = BuildTypeWithMember($"d{currentDepth}s{siblingIndex}");

                    foreach (var general in level)
                    {
                        type.AssignOwnership(new Specialization { Specific = type, General = general });
                    }

                    next.Add(type);
                }

                level = next;
            }

            var apex = BuildTypeWithMember("apex");

            foreach (var general in level)
            {
                apex.AssignOwnership(new Specialization { Specific = apex, General = general });
            }

            return apex;
        }

        private static IType BuildTypeWithMember(string memberName)
        {
            var type = new Type();
            var member = new Feature { DeclaredName = memberName };
            type.AssignOwnership(new FeatureMembership { Visibility = VisibilityKind.Public }, member);

            return type;
        }
    }
}
