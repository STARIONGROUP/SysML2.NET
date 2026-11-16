// ------------------------------------------------------------------------------------------------
// <copyright file="KernelModel.cs" company="Starion Group S.A.">
//
//   Copyright (C) 2022-2026 Starion Group S.A.
//   SPDX-License-Identifier: Apache-2.0
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace SySML2.NET.REST.Tests.Helpers.Models
{
    using System;

    using SysML2.NET.Core.Core.Types;
    using SysML2.NET.Core.POCO.Core.Classifiers;
    using SysML2.NET.Core.POCO.Core.Features;
    using SysML2.NET.Core.POCO.Core.Types;
    using SysML2.NET.Core.POCO.Kernel.Associations;
    using SysML2.NET.Core.POCO.Kernel.Behaviors;
    using SysML2.NET.Core.POCO.Kernel.Classes;
    using SysML2.NET.Core.POCO.Kernel.Connectors;
    using SysML2.NET.Core.POCO.Kernel.DataTypes;
    using SysML2.NET.Core.POCO.Kernel.Expressions;
    using SysML2.NET.Core.POCO.Kernel.FeatureValues;
    using SysML2.NET.Core.POCO.Kernel.Functions;
    using SysML2.NET.Core.POCO.Kernel.Interactions;
    using SysML2.NET.Core.POCO.Kernel.Metadata;
    using SysML2.NET.Core.POCO.Kernel.Multiplicities;
    using SysML2.NET.Core.POCO.Kernel.Packages;
    using SysML2.NET.Core.POCO.Kernel.Structures;
    using SysML2.NET.Core.POCO.Root.Annotations;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Dal;
    using SysML2.NET.Extensions;

    using DtoElement = SysML2.NET.Core.DTO.Root.Elements.IElement;
    using PocoElement = SysML2.NET.Core.POCO.Root.Elements.IElement;
    using PocoRelationship = SysML2.NET.Core.POCO.Root.Elements.IRelationship;

    /// <summary>
    /// The model of scenario step 4: the KerML Kernel metaclasses, in a package <c>Kernel</c> of the root package, and
    /// the multiplicity <c>[1]</c> that the end features of step 3 require
    /// </summary>
    /// <remarks>
    /// <code>
    /// package Kernel {
    ///     datatype D;  class Cl { feature a : D [1] = 42 default; feature b : D [1]; feature c : D [0..*];
    ///                             connector k from a to b; binding bc of a = b; }
    ///     struct S;  metaclass M specializes S;
    ///     behavior B2 { in x : D; out y : D; }
    ///     behavior B { step s1 : B2 [1]; step s2 : B2 [1]; succession su first s1 then s2;
    ///                  flow fl of payload : D from s1.y to s2.x; succession flow sf of payload : D from s1.y to s2.x;
    ///                  expressions: e, be, inv, literals, invocation, constructor, operators, metadata access }
    ///     assoc As { end e1 : Cl [1]; end e2 : S [1]; }  assoc struct AsS { … }  interaction I { … }
    ///     function F { in p : D; return r; p }  predicate P { true }
    ///     metadata md : M about Cl;
    /// }
    /// </code>
    /// Every expression owns its result parameter. Without the model library, the operator expressions have no
    /// instantiated type and the multiplicity bounds no library result types; those rules cannot hold.
    /// </remarks>
    public sealed class KernelModel : StepModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="KernelModel"/> class and builds the model on that of step 3
        /// </summary>
        /// <param name="root">
        /// The model of step 2
        /// </param>
        /// <param name="core">
        /// The model of step 3
        /// </param>
        public KernelModel(RootModel root, CoreModel core) : base((core ?? throw new ArgumentNullException(nameof(core))).Builder, "kernel")
        {
            ArgumentNullException.ThrowIfNull(root);

            this.MultiplicityOne(core.FirstEnd, core.C);
            this.MultiplicityOne(core.SecondEnd, core.C);

            this.Package = this.CreateNamed<Package>("Kernel", "kernel", PackageExtensions.ToDto);
            this.OwnMember(root.Root, this.Package);

            var dataType = this.CreateNamed<DataType>("D", "d", DataTypeExtensions.ToDto);
            dataType.IsSufficient = true;
            this.OwnMember(this.Package, dataType);

            this.Class = this.CreateNamed<Class>("Cl", "cl", ClassExtensions.ToDto);
            this.OwnMember(this.Package, this.Class);

            var a = this.TypedFeature(this.Class, "a", dataType);
            this.MultiplicityOne(a, this.Class);
            var b = this.TypedFeature(this.Class, "b", dataType);
            this.MultiplicityOne(b, this.Class);
            var c = this.TypedFeature(this.Class, "c", dataType);
            c.IsOrdered = true;
            c.IsUnique = false;
            this.MultiplicityRange(c, this.Class, this.Literal<LiteralInteger>("c/lower", LiteralIntegerExtensions.ToDto, literal => literal.Value = 0), this.Literal<LiteralInfinity>("c/upper", LiteralInfinityExtensions.ToDto, _ => { }));

            var defaultValue = this.Builder.Create<FeatureValue>("a/value", FeatureValueExtensions.ToDto);
            defaultValue.IsDefault = true;
            a.AssignOwnership(defaultValue, this.Literal<LiteralInteger>("a/value/42", LiteralIntegerExtensions.ToDto, literal => literal.Value = 42));

            var connector = this.CreateNamed<Connector>("k", "k", ConnectorExtensions.ToDto);
            this.OwnFeature(this.Class, connector);
            this.ConnectorEnd(connector, "k/source", a);
            this.ConnectorEnd(connector, "k/target", b);

            var binding = this.CreateNamed<BindingConnector>("bc", "bc", BindingConnectorExtensions.ToDto);
            this.OwnFeature(this.Class, binding);
            this.ConnectorEnd(binding, "bc/source", a);
            this.ConnectorEnd(binding, "bc/target", b);

            var structure = this.CreateNamed<Structure>("S", "s", StructureExtensions.ToDto);
            this.OwnMember(this.Package, structure);

            this.Metaclass = this.CreateNamed<Metaclass>("M", "m", MetaclassExtensions.ToDto);
            this.OwnMember(this.Package, this.Metaclass);

            var metaclassGeneral = this.Builder.Create<Subclassification>("M/subclassification", SubclassificationExtensions.ToDto);
            metaclassGeneral.Subclassifier = this.Metaclass;
            metaclassGeneral.Superclassifier = structure;
            this.Metaclass.AssignOwnership(metaclassGeneral);

            var called = this.CreateNamed<Behavior>("B2", "b2", BehaviorExtensions.ToDto);
            this.OwnMember(this.Package, called);

            var input = this.Parameter<ParameterMembership>(called, "x", FeatureDirectionKind.In, ParameterMembershipExtensions.ToDto);
            this.Typed(input, dataType);
            var output = this.CreateNamed<Feature>("y", "y", FeatureExtensions.ToDto);
            output.Direction = FeatureDirectionKind.Out;
            this.OwnFeature(called, output);
            this.Typed(output, dataType);

            this.Behavior = this.CreateNamed<Behavior>("B", "bh", BehaviorExtensions.ToDto);
            this.OwnMember(this.Package, this.Behavior);

            var first = this.TypedFeature<Step>(this.Behavior, "s1", called, StepExtensions.ToDto);
            this.MultiplicityOne(first, this.Behavior);
            var second = this.TypedFeature<Step>(this.Behavior, "s2", called, StepExtensions.ToDto);
            this.MultiplicityOne(second, this.Behavior);

            var succession = this.CreateNamed<Succession>("su", "su", SuccessionExtensions.ToDto);
            this.OwnFeature(this.Behavior, succession);
            this.ConnectorEnd(succession, "su/first", first);
            this.ConnectorEnd(succession, "su/then", second);

            this.Flow<Flow>("fl", FlowExtensions.ToDto, first, second, output, input, dataType);
            this.Flow<SuccessionFlow>("sf", SuccessionFlowExtensions.ToDto, first, second, output, input, dataType);

            this.Association<Association>("As", AssociationExtensions.ToDto, structure);
            this.Association<AssociationStructure>("AsS", AssociationStructureExtensions.ToDto, dataType);
            this.Association<Interaction>("I", InteractionExtensions.ToDto, structure);

            this.Function = this.CreateNamed<Function>("F", "f", FunctionExtensions.ToDto);
            this.OwnMember(this.Package, this.Function);
            this.FunctionParameter = this.Parameter<ParameterMembership>(this.Function, "p", FeatureDirectionKind.In, ParameterMembershipExtensions.ToDto);
            this.Typed(this.FunctionParameter, dataType);
            this.Result(this.Function, "F");
            this.Body(this.Function, "F", this.FeatureReference("F/body", this.FunctionParameter));

            var predicate = this.CreateNamed<Predicate>("P", "p", PredicateExtensions.ToDto);
            this.OwnMember(this.Package, predicate);
            this.Result(predicate, "P");
            this.Body(predicate, "P", this.Literal<LiteralBoolean>("P/body", LiteralBooleanExtensions.ToDto, literal => literal.Value = true));

            this.Expressions(dataType, a, b, c, predicate);

            var metadata = this.CreateNamed<MetadataFeature>("md", "md", MetadataFeatureExtensions.ToDto);
            this.OwnMember(this.Package, metadata);
            this.Typed(metadata, this.Metaclass);

            var about = this.Builder.Create<Annotation>("md/about Cl", AnnotationExtensions.ToDto);
            about.AnnotatedElement = this.Class;
            metadata.AssignOwnership(about);
        }

        /// <summary>
        /// Gets the package that holds the step
        /// </summary>
        public Package Package { get; }

        /// <summary>
        /// Gets the class <c>Cl</c>, which owns the connectors and which the associations, the constructor expression
        /// and the metadata relate to
        /// </summary>
        public Class Class { get; }

        /// <summary>
        /// Gets the behavior <c>B</c>, which owns the steps, flows and expressions
        /// </summary>
        public Behavior Behavior { get; }

        /// <summary>
        /// Gets the function <c>F</c>
        /// </summary>
        public Function Function { get; }

        /// <summary>
        /// Gets the in parameter <c>p</c> of <see cref="Function"/>
        /// </summary>
        public Feature FunctionParameter { get; }

        /// <summary>
        /// Gets the metaclass <c>M</c>
        /// </summary>
        public Metaclass Metaclass { get; }

        /// <summary>
        /// Adds an in parameter to an invocation-like expression, whose value is an expression
        /// </summary>
        /// <param name="expression">
        /// The expression that takes the argument
        /// </param>
        /// <param name="name">
        /// The name of the argument
        /// </param>
        /// <param name="value">
        /// The value of the argument
        /// </param>
        /// <returns>
        /// The argument feature
        /// </returns>
        private Feature Argument(IExpression expression, string name, IExpression value)
        {
            var argument = this.Parameter<ParameterMembership>(expression, name, FeatureDirectionKind.In, ParameterMembershipExtensions.ToDto);
            var argumentValue = this.Builder.Create<FeatureValue>($"{argument.ElementId}/value", FeatureValueExtensions.ToDto);
            argument.AssignOwnership(argumentValue, value);

            return argument;
        }

        /// <summary>
        /// Creates an association-like classifier with two end features, typed by <see cref="Class"/> and by
        /// <paramref name="other"/>, each with the multiplicity <c>[1]</c>
        /// </summary>
        /// <typeparam name="TAssociation">
        /// The association POCO type
        /// </typeparam>
        /// <param name="name">
        /// The name of the association
        /// </param>
        /// <param name="toDto">
        /// The generated <c>ToDto</c> extension of <typeparamref name="TAssociation"/>
        /// </param>
        /// <param name="other">
        /// The type of the second end
        /// </param>
        private void Association<TAssociation>(string name, Func<TAssociation, bool, DtoElement> toDto, IType other) where TAssociation : IAssociation, new()
        {
            var association = this.CreateNamed(name, name.ToLowerInvariant(), toDto);
            this.OwnMember(this.Package, association);

            foreach (var (endName, endType) in new[] { ($"{name}/e1", (IType)this.Class), ($"{name}/e2", other) })
            {
                var end = this.CreateNamed<Feature>(endName, endName.Replace('/', '_'), FeatureExtensions.ToDto);
                end.IsEnd = true;
                this.OwnFeature(association, end);
                this.Typed(end, endType);
                this.MultiplicityOne(end, association);
            }
        }

        /// <summary>
        /// Makes an expression the result expression of a function or predicate
        /// </summary>
        /// <param name="function">
        /// The function or predicate
        /// </param>
        /// <param name="name">
        /// The name the membership is derived from
        /// </param>
        /// <param name="body">
        /// The result expression
        /// </param>
        private void Body(IFunction function, string name, IExpression body)
        {
            var membership = this.Builder.Create<ResultExpressionMembership>($"{name}/result expression", ResultExpressionMembershipExtensions.ToDto);
            function.AssignOwnership(membership, body);
        }

        /// <summary>
        /// Creates a feature reference expression whose referent is an expression owned through a
        /// <see cref="FeatureMembership"/>, as the body of a select or collect expression
        /// </summary>
        /// <param name="name">
        /// The name the elements are derived from
        /// </param>
        /// <returns>
        /// The feature reference expression
        /// </returns>
        private FeatureReferenceExpression BodyReference(string name)
        {
            var reference = this.Builder.Create<FeatureReferenceExpression>(name, FeatureReferenceExpressionExtensions.ToDto);
            var body = this.Literal<LiteralBoolean>($"{name}/true", LiteralBooleanExtensions.ToDto, literal => literal.Value = true);
            var membership = this.Builder.Create<FeatureMembership>($"{name}/membership", FeatureMembershipExtensions.ToDto);
            reference.AssignOwnership(membership, body);
            this.Result(reference, name);

            return reference;
        }

        /// <summary>
        /// Adds an end feature to a connector, referencing a related feature
        /// </summary>
        /// <param name="connector">
        /// The connector
        /// </param>
        /// <param name="name">
        /// The name the end is derived from
        /// </param>
        /// <param name="related">
        /// The related feature
        /// </param>
        /// <returns>
        /// The end feature
        /// </returns>
        private Feature ConnectorEnd(IConnector connector, string name, IFeature related)
        {
            var end = this.Builder.Create<Feature>(name, FeatureExtensions.ToDto);
            end.IsEnd = true;
            this.OwnFeature(connector, end);

            var reference = this.Builder.Create<ReferenceSubsetting>($"{name}/reference", ReferenceSubsettingExtensions.ToDto);
            reference.ReferencedFeature = related;
            end.AssignOwnership(reference);

            return end;
        }

        /// <summary>
        /// Adds one instance of every expression metaclass to <see cref="Behavior"/>
        /// </summary>
        /// <param name="dataType">
        /// The data type <c>D</c>
        /// </param>
        /// <param name="a">
        /// The feature <c>Cl::a</c>
        /// </param>
        /// <param name="b">
        /// The feature <c>Cl::b</c>
        /// </param>
        /// <param name="c">
        /// The feature <c>Cl::c</c>
        /// </param>
        /// <param name="predicate">
        /// The predicate <c>P</c>
        /// </param>
        private void Expressions(IType dataType, IFeature a, IFeature b, IFeature c, IFunction predicate)
        {
            var expression = this.OwnExpression<Expression>("e", ExpressionExtensions.ToDto);
            this.Typed(expression, this.Function);

            var booleanExpression = this.OwnExpression<BooleanExpression>("be", BooleanExpressionExtensions.ToDto);
            this.Typed(booleanExpression, predicate);

            var invariant = this.OwnExpression<Invariant>("inv", InvariantExtensions.ToDto);
            invariant.IsNegated = true;
            this.Typed(invariant, predicate);

            var rational = this.OwnExpression<LiteralRational>("rational", LiteralRationalExtensions.ToDto);
            rational.Value = 2.5;

            var text = this.OwnExpression<LiteralString>("text", LiteralStringExtensions.ToDto);
            text.Value = "s";

            this.OwnExpression<LiteralExpression>("literal", LiteralExpressionExtensions.ToDto);
            this.OwnExpression<NullExpression>("null", NullExpressionExtensions.ToDto);

            var metadataAccess = this.OwnExpression<MetadataAccessExpression>("metadata access", MetadataAccessExpressionExtensions.ToDto);
            this.Reference(metadataAccess, this.Class);

            var invocation = this.OwnExpression<InvocationExpression>("invocation", InvocationExpressionExtensions.ToDto);
            this.Reference(invocation, this.Function);
            var invocationArgument = this.Argument(invocation, "invocation/p", this.Literal<LiteralInteger>("invocation/p/7", LiteralIntegerExtensions.ToDto, literal => literal.Value = 7));
            var redefinition = this.Builder.Create<Redefinition>("invocation/p/redefinition", RedefinitionExtensions.ToDto);
            redefinition.RedefiningFeature = invocationArgument;
            redefinition.RedefinedFeature = this.FunctionParameter;
            invocationArgument.AssignOwnership(redefinition);

            var constructor = this.OwnExpression<ConstructorExpression>("constructor", ConstructorExpressionExtensions.ToDto);
            this.Reference(constructor, this.Class);

            var sum = this.OwnExpression<OperatorExpression>("sum", OperatorExpressionExtensions.ToDto);
            sum.Operator = "+";
            this.Argument(sum, "sum/left", this.Literal<LiteralInteger>("sum/left/1", LiteralIntegerExtensions.ToDto, literal => literal.Value = 1));
            this.Argument(sum, "sum/right", this.Literal<LiteralInteger>("sum/right/2", LiteralIntegerExtensions.ToDto, literal => literal.Value = 2));

            var index = this.OwnExpression<IndexExpression>("index", IndexExpressionExtensions.ToDto);
            index.Operator = "#";
            this.Argument(index, "index/sequence", this.FeatureReference("index/sequence/c", c));
            this.Argument(index, "index/position", this.Literal<LiteralInteger>("index/position/1", LiteralIntegerExtensions.ToDto, literal => literal.Value = 1));

            var select = this.OwnExpression<SelectExpression>("select", SelectExpressionExtensions.ToDto);
            select.Operator = "select";
            this.Argument(select, "select/sequence", this.FeatureReference("select/sequence/c", c));
            this.Argument(select, "select/body", this.BodyReference("select/body/reference"));

            var collect = this.OwnExpression<CollectExpression>("collect", CollectExpressionExtensions.ToDto);
            collect.Operator = "collect";
            this.Argument(collect, "collect/sequence", this.FeatureReference("collect/sequence/c", c));
            this.Argument(collect, "collect/body", this.BodyReference("collect/body/reference"));

            var chain = this.OwnExpression<FeatureChainExpression>("chain", FeatureChainExpressionExtensions.ToDto);
            chain.Operator = ".";
            this.Argument(chain, "chain/source", this.FeatureReference("chain/source/a", a));
            this.Reference(chain, b);

            var typed = this.Builder.Create<FeatureTyping>("rational/typing", FeatureTypingExtensions.ToDto);
            typed.TypedFeature = rational;
            typed.Type = dataType;
            rational.AssignOwnership(typed);
        }

        /// <summary>
        /// Creates a feature reference expression that refers to a feature
        /// </summary>
        /// <param name="name">
        /// The name the elements are derived from
        /// </param>
        /// <param name="referent">
        /// The referenced feature
        /// </param>
        /// <returns>
        /// The feature reference expression
        /// </returns>
        private FeatureReferenceExpression FeatureReference(string name, IFeature referent)
        {
            var reference = this.Builder.Create<FeatureReferenceExpression>(name, FeatureReferenceExpressionExtensions.ToDto);
            this.Reference(reference, referent);
            this.Result(reference, name);

            return reference;
        }

        /// <summary>
        /// Creates a flow-like connector in <see cref="Behavior"/> from an output of one step to an input of another,
        /// with its payload feature
        /// </summary>
        /// <typeparam name="TFlow">
        /// The flow POCO type
        /// </typeparam>
        /// <param name="name">
        /// The name of the flow
        /// </param>
        /// <param name="toDto">
        /// The generated <c>ToDto</c> extension of <typeparamref name="TFlow"/>
        /// </param>
        /// <param name="source">
        /// The source step
        /// </param>
        /// <param name="target">
        /// The target step
        /// </param>
        /// <param name="output">
        /// The output parameter that the source end redefines
        /// </param>
        /// <param name="input">
        /// The input parameter that the target end redefines
        /// </param>
        /// <param name="payloadType">
        /// The type of the payload
        /// </param>
        private void Flow<TFlow>(string name, Func<TFlow, bool, DtoElement> toDto, IFeature source, IFeature target, IFeature output, IFeature input, IType payloadType) where TFlow : IFlow, new()
        {
            var flow = this.CreateNamed(name, name, toDto);
            this.OwnFeature(this.Behavior, flow);

            var payload = this.Builder.Create<PayloadFeature>($"{name}/payload", PayloadFeatureExtensions.ToDto);
            this.OwnFeature(flow, payload);
            this.Typed(payload, payloadType);

            foreach (var (endName, related, redefined, direction) in new[] { ($"{name}/source", source, output, FeatureDirectionKind.Out), ($"{name}/target", target, input, FeatureDirectionKind.In) })
            {
                var end = this.Builder.Create<FlowEnd>(endName, FlowEndExtensions.ToDto);
                end.IsEnd = true;
                this.OwnFeature(flow, end);

                var reference = this.Builder.Create<ReferenceSubsetting>($"{endName}/reference", ReferenceSubsettingExtensions.ToDto);
                reference.ReferencedFeature = related;
                end.AssignOwnership(reference);

                var flowFeature = this.Builder.Create<Feature>($"{endName}/feature", FeatureExtensions.ToDto);
                flowFeature.Direction = direction;
                this.OwnFeature(end, flowFeature);

                var redefinition = this.Builder.Create<Redefinition>($"{endName}/feature/redefinition", RedefinitionExtensions.ToDto);
                redefinition.RedefiningFeature = flowFeature;
                redefinition.RedefinedFeature = redefined;
                flowFeature.AssignOwnership(redefinition);
            }
        }

        /// <summary>
        /// Creates an expression with its result parameter as a feature of <see cref="Behavior"/>
        /// </summary>
        /// <typeparam name="TExpression">
        /// The expression POCO type
        /// </typeparam>
        /// <param name="name">
        /// The declared name of the expression
        /// </param>
        /// <param name="toDto">
        /// The generated <c>ToDto</c> extension of <typeparamref name="TExpression"/>
        /// </param>
        /// <returns>
        /// The expression
        /// </returns>
        private TExpression OwnExpression<TExpression>(string name, Func<TExpression, bool, DtoElement> toDto) where TExpression : IExpression, new()
        {
            var expression = this.CreateNamed(name, name.Replace(' ', '_'), toDto);
            this.OwnFeature(this.Behavior, expression);
            this.Result(expression, name);

            return expression;
        }

        /// <summary>
        /// Refers an expression to an element through a plain, non-owning <see cref="Membership"/>
        /// </summary>
        /// <param name="expression">
        /// The expression
        /// </param>
        /// <param name="element">
        /// The referenced element
        /// </param>
        private void Reference(IExpression expression, PocoElement element)
        {
            var membership = this.Builder.Create<Membership>($"{expression.ElementId}/reference {element.ElementId}", MembershipExtensions.ToDto);
            membership.MemberElement = element;
            membership.MemberName = element.DeclaredName;
            expression.AssignOwnership(membership);
        }

        /// <summary>
        /// Creates a feature of a type, typed by another type
        /// </summary>
        /// <param name="owner">
        /// The owning type
        /// </param>
        /// <param name="name">
        /// The name of the feature
        /// </param>
        /// <param name="type">
        /// The type of the feature
        /// </param>
        /// <returns>
        /// The feature
        /// </returns>
        private Feature TypedFeature(IType owner, string name, IType type) => this.TypedFeature<Feature>(owner, name, type, FeatureExtensions.ToDto);

        /// <summary>
        /// Creates a feature of a given metaclass that a type owns, typed by another type
        /// </summary>
        /// <typeparam name="TFeature">
        /// The feature POCO type
        /// </typeparam>
        /// <param name="owner">
        /// The owning type
        /// </param>
        /// <param name="name">
        /// The name of the feature
        /// </param>
        /// <param name="type">
        /// The type of the feature
        /// </param>
        /// <param name="toDto">
        /// The generated <c>ToDto</c> extension of <typeparamref name="TFeature"/>
        /// </param>
        /// <returns>
        /// The feature
        /// </returns>
        private TFeature TypedFeature<TFeature>(IType owner, string name, IType type, Func<TFeature, bool, DtoElement> toDto) where TFeature : IFeature, new()
        {
            var feature = this.CreateNamed(name, name, toDto);
            this.OwnFeature(owner, feature);
            this.Typed(feature, type);

            return feature;
        }
    }
}
