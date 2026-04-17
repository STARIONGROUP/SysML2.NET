// -------------------------------------------------------------------------------------------------
// <copyright file="ElementExtensionsTestFixture.cs" company="Starion Group S.A.">
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

    using SysML2.NET.Core.POCO.Kernel.Packages;
    using SysML2.NET.Core.POCO.Root.Annotations;
    using SysML2.NET.Core.POCO.Root.Elements;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Core.POCO.Systems.DefinitionAndUsage;
    using SysML2.NET.Extensions;

    [TestFixture]
    public class ElementExtensionsTestFixture
    {
        [Test]
        public void VerifyComputeDocumentation()
        {
            Assert.That(() => ((IElement)null).ComputeDocumentation(), Throws.TypeOf<ArgumentNullException>());

            var element = new Definition();
            var documentation = new Documentation();
            var annotation = new Annotation();
            
            Assert.That(element.ComputeDocumentation, Has.Count.EqualTo(0));

            element.AssignOwnership(annotation, documentation);

            Assert.That(element.ComputeDocumentation(), Is.EquivalentTo([documentation]));
        }
        
        [Test]
        public void VerifyComputeIsLibraryElement()
        {
            Assert.That(() => ((IElement)null).ComputeIsLibraryElement(), Throws.TypeOf<ArgumentNullException>());
            
            var element = new Definition();
            var documentation = new Documentation();
            var annotation = new Annotation();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(element.ComputeIsLibraryElement, Is.False);
                Assert.That(documentation.ComputeIsLibraryElement, Is.False);
            }

            element.AssignOwnership(annotation, documentation);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(element.ComputeIsLibraryElement, Is.False);
                Assert.That(documentation.ComputeIsLibraryElement, Is.False);
            }
        }
        
        [Test]
        public void VerifyComputeName()
        {
            Assert.That(() => ((IElement)null).ComputeName(), Throws.TypeOf<ArgumentNullException>());
            
            var element = new Definition();

            Assert.That(element.ComputeName, Is.Null);
            
            element.DeclaredName = "definitionName";
            Assert.That(element.ComputeName, Is.EqualTo("definitionName"));
        }
        
        [Test]
        public void VerifyComputeOwnedAnnotation()
        {
            Assert.That(() => ((IElement)null).ComputeOwnedAnnotation(), Throws.TypeOf<ArgumentNullException>());
            
            var element = new Definition();
            var documentation = new Documentation();
            var annotation = new Annotation();
            element.AssignOwnership(annotation, documentation);
            Assert.That(element.ComputeOwnedAnnotation, Has.Count.EqualTo(0));
            
            annotation.AnnotatedElement =  element;
            Assert.That(element.ComputeOwnedAnnotation, Is.EquivalentTo([annotation]));
        }
        
        [Test]
        public void VerifyComputeOwnedElement()
        {
            Assert.That(() => ((IElement)null).ComputeOwnedElement(), Throws.TypeOf<ArgumentNullException>());
            
            var element = new Definition();
            var documentation = new Documentation();
            var annotation = new Annotation();
            Assert.That(element.ComputeOwnedElement, Has.Count.EqualTo(0));
            
            element.AssignOwnership(annotation, documentation);
            Assert.That(element.ComputeDocumentation, Is.EquivalentTo([documentation]));
        }
        
        [Test]
        public void VerifyComputeOwner()
        {
            Assert.That(() => ((IElement)null).ComputeOwner(), Throws.TypeOf<ArgumentNullException>());
            
            var element = new Definition();
            var documentation = new Documentation();
            var annotation = new Annotation();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(element.ComputeOwner, Is.Null);
                Assert.That(documentation.ComputeOwner, Is.Null);
            }
            
            element.AssignOwnership(annotation, documentation);
            
            using (Assert.EnterMultipleScope())
            {
                Assert.That(element.ComputeOwner, Is.Null);
                Assert.That(documentation.ComputeOwner, Is.EqualTo(element));
            }        
        }
        
        [Test]
        public void VerifyComputeOwningMembership()
        {
            Assert.That(() => ((IElement)null).ComputeOwningMembership(), Throws.TypeOf<ArgumentNullException>());
            
            var element = new Definition();
            var documentation = new Documentation();
            var annotation = new Annotation();
            
            Assert.That(documentation.ComputeOwningMembership, Is.Null);
            
            element.AssignOwnership(annotation, documentation);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(documentation.ComputeOwningMembership, Is.Null);
                Assert.That(element.ComputeOwningMembership, Is.Null);
            }

            var membership = new OwningMembership();
            var namespaceElement = new Namespace();
            
            namespaceElement.AssignOwnership(membership, element);
            Assert.That(element.ComputeOwningMembership, Is.EqualTo(membership));
        }
        
        [Test]
        public void VerifyComputeOwningNamespace()
        {
            Assert.That(() => ((IElement)null).ComputeOwningNamespace(), Throws.TypeOf<ArgumentNullException>());
            
            var element = new Definition();
            var documentation = new Documentation();
            var annotation = new Annotation();
            element.AssignOwnership(annotation, documentation);
            
            Assert.That(documentation.ComputeOwningNamespace, Is.Null);
            var membership = new OwningMembership();
            var namespaceElement = new Namespace();
            
            namespaceElement.AssignOwnership(membership, element);
            Assert.That(element.ComputeOwningNamespace, Is.EqualTo(namespaceElement));
        }
        
        [Test]
        public void VerifyComputeQualifiedName()
        {
            Assert.That(() => ((IElement)null).ComputeQualifiedName(), Throws.TypeOf<ArgumentNullException>());
            
            var element = new Definition();

            Assert.That(element.ComputeQualifiedName, Is.Null);
            
            var membership = new OwningMembership();
            var secondMembership = new OwningMembership();

            var namespaceElement = new Namespace();
            
            namespaceElement.AssignOwnership(membership, element);
            element.DeclaredName = "name";
            
            Assert.That(element.ComputeQualifiedName, Is.EqualTo("name"));
            
            var secondElement = new Definition()
            {
                DeclaredName = "name"
            };
            
            namespaceElement.AssignOwnership(secondMembership, secondElement);
            
            using (Assert.EnterMultipleScope())
            {
                Assert.That(element.ComputeQualifiedName, Is.EqualTo("name"));
                Assert.That(secondElement.ComputeQualifiedName, Is.Null);
            }

            var packageOwner = new Package()
            {
                DeclaredName = "owner"
            };

            var packageMembership = new OwningMembership();
            packageOwner.AssignOwnership(packageMembership, namespaceElement);
            
            Assert.That(element.ComputeQualifiedName, Is.Null);

            namespaceElement.DeclaredName = "namespace";
            Assert.That(element.ComputeQualifiedName, Is.EqualTo("namespace::name"));
        }
        
        [Test]
        public void VerifyComputeShortName()
        {
            Assert.That(() => ((IElement)null).ComputeShortName(), Throws.TypeOf<ArgumentNullException>());
            
            var element = new Definition();

            Assert.That(element.ComputeShortName, Is.Null);

            element.DeclaredShortName = "shortName";
            Assert.That(element.ComputeShortName, Is.EqualTo("shortName"));
        }
        
        [Test]
        public void VerifyComputeTextualRepresentation()
        {
            Assert.That(() => ((IElement)null).ComputeTextualRepresentation(), Throws.TypeOf<ArgumentNullException>());
            
            var element = new Definition();
            var textualRepresentation = new TextualRepresentation();
            var annotation = new Annotation();
            
            Assert.That(element.ComputeTextualRepresentation, Has.Count.EqualTo(0));

            element.AssignOwnership(annotation, textualRepresentation);

            Assert.That(element.ComputeTextualRepresentation(), Is.EquivalentTo([textualRepresentation]));
        }

        [Test]
        public void VerifyComputePathOperation()
        {
            Assert.That(() => ((IElement)null).ComputePathOperation(), Throws.TypeOf<ArgumentNullException>());
            
            var element = new Definition();

            Assert.That(element.ComputePathOperation, Is.Empty);
            element.DeclaredName = "name";

            Assert.That(element.ComputePathOperation, Is.Empty);
            
            var membership = new OwningMembership();
            var secondMembership = new OwningMembership();
            var namespaceElement = new Namespace();
            
            namespaceElement.AssignOwnership(membership, element);
            
            Assert.That(element.ComputePathOperation, Is.EqualTo("name"));

            var secondElement = new Definition()
            {
                DeclaredName = "name"
            };
            
            namespaceElement.AssignOwnership(secondMembership, secondElement);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(element.ComputePathOperation, Is.EqualTo("name"));
                Assert.That(secondElement.ComputePathOperation, Throws.TypeOf<NotSupportedException>());
            }
            
            namespaceElement.DeclaredName = "namespace";
            membership.DeclaredName = "firstMember";
            secondMembership.DeclaredName = "secondMember";
            
            using (Assert.EnterMultipleScope())
            {
                Assert.That(element.ComputePathOperation, Is.EqualTo("name"));
                Assert.That(secondElement.ComputePathOperation, Throws.TypeOf<NotSupportedException>());
            }
        }

        [Test]
        public void VerifyComputeEscapedNameOperation()
        {
            Assert.That(() => ((IElement)null).ComputeEscapedNameOperation(), Throws.TypeOf<ArgumentNullException>());

            var element = new Definition()
            {
                DeclaredName = "basic"
            };

            Assert.That(element.ComputeEscapedNameOperation, Is.EqualTo("basic"));

            element.DeclaredName = "non basic";
            Assert.That(element.ComputeEscapedNameOperation, Is.EqualTo("\'non basic\'"));
        }

        [Test]
        public void VerifyValidateIsImpliedIncluded()
        {
            Assert.That(() => ((IElement)null).ValidateIsImpliedIncluded(), Throws.TypeOf<ArgumentNullException>());

            var element = new Definition();

            Assert.That(element.ValidateIsImpliedIncluded, Is.True);

            var annotation = new Annotation();
            var documentation = new Documentation();
            element.AssignOwnership(annotation, documentation);

            Assert.That(element.ValidateIsImpliedIncluded, Is.True);

            annotation.IsImplied = true;

            Assert.That(element.ValidateIsImpliedIncluded, Is.False);

            element.IsImpliedIncluded = true;

            Assert.That(element.ValidateIsImpliedIncluded, Is.True);
        }
    }
}
