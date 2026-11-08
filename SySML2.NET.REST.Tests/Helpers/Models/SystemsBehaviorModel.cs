// ------------------------------------------------------------------------------------------------
// <copyright file="SystemsBehaviorModel.cs" company="Starion Group S.A.">
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
    using SysML2.NET.Core.POCO.Kernel.Behaviors;
    using SysML2.NET.Core.POCO.Kernel.Connectors;
    using SysML2.NET.Core.POCO.Kernel.Expressions;
    using SysML2.NET.Core.POCO.Kernel.FeatureValues;
    using SysML2.NET.Core.POCO.Kernel.Functions;
    using SysML2.NET.Core.POCO.Kernel.Packages;
    using SysML2.NET.Core.POCO.Root.Annotations;
    using SysML2.NET.Core.POCO.Root.Namespaces;
    using SysML2.NET.Core.POCO.Systems.Actions;
    using SysML2.NET.Core.POCO.Systems.AnalysisCases;
    using SysML2.NET.Core.POCO.Systems.Calculations;
    using SysML2.NET.Core.POCO.Systems.Cases;
    using SysML2.NET.Core.POCO.Systems.Constraints;
    using SysML2.NET.Core.POCO.Systems.DefinitionAndUsage;
    using SysML2.NET.Core.POCO.Systems.Metadata;
    using SysML2.NET.Core.POCO.Systems.Occurrences;
    using SysML2.NET.Core.POCO.Systems.Parts;
    using SysML2.NET.Core.POCO.Systems.Requirements;
    using SysML2.NET.Core.POCO.Systems.States;
    using SysML2.NET.Core.POCO.Systems.UseCases;
    using SysML2.NET.Core.POCO.Systems.VerificationCases;
    using SysML2.NET.Core.POCO.Systems.Views;
    using SysML2.NET.Core.Root.Namespaces;
    using SysML2.NET.Core.Systems.Actions;
    using SysML2.NET.Core.Systems.Requirements;
    using SysML2.NET.Core.Systems.States;
    using SysML2.NET.Dal;
    using SysML2.NET.Extensions;

    using DtoElement = SysML2.NET.Core.DTO.Root.Elements.IElement;
    using PocoElement = SysML2.NET.Core.POCO.Root.Elements.IElement;
    using PocoRelationship = SysML2.NET.Core.POCO.Root.Elements.IRelationship;

    /// <summary>
    /// The model of scenario step 7: the SysML behaviour, requirement, case, view and metadata metaclasses, in a package
    /// <c>Behaviour</c> of the root package, committed on the branch <c>systems</c>
    /// </summary>
    /// <remarks>
    /// <para>
    /// An action definition <c>Act</c> holds every action and control node kind; a state definition <c>St</c> has entry,
    /// do and exit subactions and a transition with a trigger, a guard and an effect; calculation and constraint
    /// definitions own their results. <c>RBase</c> owns the subject and the result that every requirement-like and,
    /// through <c>Cs</c>, every case-like definition inherits, so that the subject and objective memberships are owned
    /// by exactly a requirement or case definition, as <c>validateSubjectMembershipOwningType</c> and
    /// <c>validateObjectiveMembershipOwningType</c> read literally (<c>oclIsType</c>); that reading is one of two the
    /// OCL admits. A view usage exposes the structure of step 6, and a metadata usage annotates its part definition.
    /// </para>
    /// <para>
    /// The rules that need the model library cannot hold: the <c>check…Specialization</c> rules, the trigger argument
    /// rules, the Boolean result of a guard, and <c>validateFeatureIsVariable</c> for the assigned feature.
    /// </para>
    /// </remarks>
    public sealed class SystemsBehaviorModel : StepModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SystemsBehaviorModel"/> class and builds the model on that of
        /// step 6
        /// </summary>
        /// <param name="root">
        /// The model of step 2
        /// </param>
        /// <param name="structure">
        /// The model of step 6
        /// </param>
        public SystemsBehaviorModel(RootModel root, SystemsStructureModel structure) : base((structure ?? throw new ArgumentNullException(nameof(structure))).Builder, "systems behaviour")
        {
            ArgumentNullException.ThrowIfNull(root);

            this.Package = this.CreateNamed<Package>("Behaviour", "beh", PackageExtensions.ToDto);
            this.OwnMember(root.Root, this.Package);

            this.Actions(structure);
            this.States();
            var (calculation, constraint) = this.Calculations();
            var requirement = this.Requirements(constraint, structure);
            this.Cases(requirement);
            this.Views(structure);
            this.Metadata(structure);

            var top = this.Member<CalculationUsage>("calc", CalculationUsageExtensions.ToDto);
            this.Typed(top, calculation);
        }

        /// <summary>
        /// Gets the package that holds the step
        /// </summary>
        public Package Package { get; }

        /// <summary>
        /// Gets the action definition <c>Act</c>, which holds every action and control node kind
        /// </summary>
        public ActionDefinition ActionDefinition { get; private set; }

        /// <summary>
        /// Gets the requirement definition <c>RBase</c>, which owns the subject and the result that requirements and cases
        /// inherit
        /// </summary>
        public RequirementDefinition RequirementBase { get; private set; }

        /// <summary>
        /// Adds a trigger-like accept action to <see cref="ActionDefinition"/>, whose payload is the value of a trigger
        /// invocation of the given kind
        /// </summary>
        /// <param name="name">
        /// The name of the accept action
        /// </param>
        /// <param name="kind">
        /// The kind of the trigger
        /// </param>
        /// <param name="triggered">
        /// The behavior the trigger invokes, with one in parameter
        /// </param>
        /// <param name="triggeredInput">
        /// The in parameter of <paramref name="triggered"/>
        /// </param>
        private void Accept(string name, TriggerKind kind, ActionDefinition triggered, ReferenceUsage triggeredInput)
        {
            var accept = this.Owned<AcceptActionUsage>(this.ActionDefinition, name, AcceptActionUsageExtensions.ToDto);
            var payload = this.Param<ReferenceUsage, ParameterMembership>(accept, $"{name}/payload", ReferenceUsageExtensions.ToDto, ParameterMembershipExtensions.ToDto);

            var trigger = this.Builder.Create<TriggerInvocationExpression>($"{name}/trigger", TriggerInvocationExpressionExtensions.ToDto);
            trigger.Kind = kind;
            this.Refer(trigger, triggered);

            var argument = this.Param<ReferenceUsage, ParameterMembership>(trigger, $"{name}/trigger/argument", ReferenceUsageExtensions.ToDto, ParameterMembershipExtensions.ToDto);
            this.Redefine(argument, triggeredInput);
            this.Value(argument, this.Literal<LiteralBoolean>($"{name}/trigger/argument/true", LiteralBooleanExtensions.ToDto, literal => literal.Value = true));
            this.Result(trigger, $"{name}/trigger");

            this.Value(payload, trigger);
        }

        /// <summary>
        /// Adds the action definition <c>Act</c> with one instance of every action and control node kind
        /// </summary>
        /// <param name="structure">
        /// The model of step 6
        /// </param>
        private void Actions(SystemsStructureModel structure)
        {
            this.ActionDefinition = this.Member<ActionDefinition>("Act", ActionDefinitionExtensions.ToDto);
            this.Param<ReferenceUsage, ParameterMembership>(this.ActionDefinition, "Act/x", ReferenceUsageExtensions.ToDto, ParameterMembershipExtensions.ToDto);

            foreach (var node in new PocoElement[]
                     {
                         this.Owned<DecisionNode>(this.ActionDefinition, "decide", DecisionNodeExtensions.ToDto),
                         this.Owned<ForkNode>(this.ActionDefinition, "fork", ForkNodeExtensions.ToDto),
                         this.Owned<JoinNode>(this.ActionDefinition, "join", JoinNodeExtensions.ToDto),
                         this.Owned<MergeNode>(this.ActionDefinition, "merge", MergeNodeExtensions.ToDto)
                     })
            {
                ((IFeature)node).IsComposite = true;
            }

            var performed = this.Owned<ActionUsage>(this.ActionDefinition, "a0", ActionUsageExtensions.ToDto);
            performed.IsComposite = true;

            var triggered = this.Member<ActionDefinition>("Trig", ActionDefinitionExtensions.ToDto);
            var triggeredInput = this.Param<ReferenceUsage, ParameterMembership>(triggered, "Trig/condition", ReferenceUsageExtensions.ToDto, ParameterMembershipExtensions.ToDto);

            this.Accept("accept when", TriggerKind.When, triggered, triggeredInput);
            this.Accept("accept at", TriggerKind.At, triggered, triggeredInput);
            this.Accept("accept after", TriggerKind.After, triggered, triggeredInput);

            var send = this.Owned<SendActionUsage>(this.ActionDefinition, "send", SendActionUsageExtensions.ToDto);
            this.Value(this.Param<ReferenceUsage, ParameterMembership>(send, "send/payload", ReferenceUsageExtensions.ToDto, ParameterMembershipExtensions.ToDto), this.Literal<LiteralInteger>("send/payload/1", LiteralIntegerExtensions.ToDto, literal => literal.Value = 1));

            var level = this.Owned<Feature>(structure.OccurrenceDefinition, "level", FeatureExtensions.ToDto);
            level.IsVariable = true;

            var limit = this.Owned<Feature>(structure.OccurrenceDefinition, "limit", FeatureExtensions.ToDto);
            limit.IsVariable = true;
            limit.IsConstant = true;
            var initialValue = this.Builder.Create<FeatureValue>($"{limit.ElementId}/value", FeatureValueExtensions.ToDto);
            initialValue.IsInitial = true;
            limit.AssignOwnership(initialValue, this.Literal<LiteralInteger>("limit/initial/0", LiteralIntegerExtensions.ToDto, literal => literal.Value = 0));

            var assign = this.Owned<AssignmentActionUsage>(this.ActionDefinition, "assign", AssignmentActionUsageExtensions.ToDto);
            this.Refer(assign, level);
            this.Value(this.Param<ReferenceUsage, ParameterMembership>(assign, "assign/target", ReferenceUsageExtensions.ToDto, ParameterMembershipExtensions.ToDto), this.Literal<LiteralInteger>("assign/target/0", LiteralIntegerExtensions.ToDto, literal => literal.Value = 0));
            this.Value(this.Param<ReferenceUsage, ParameterMembership>(assign, "assign/value", ReferenceUsageExtensions.ToDto, ParameterMembershipExtensions.ToDto), this.Literal<LiteralInteger>("assign/value/5", LiteralIntegerExtensions.ToDto, literal => literal.Value = 5));

            var ifAction = this.Owned<IfActionUsage>(this.ActionDefinition, "if", IfActionUsageExtensions.ToDto);
            this.Condition(ifAction, "if/condition");
            this.Param<ActionUsage, ParameterMembership>(ifAction, "if/then", ActionUsageExtensions.ToDto, ParameterMembershipExtensions.ToDto);

            var whileLoop = this.Owned<WhileLoopActionUsage>(this.ActionDefinition, "while", WhileLoopActionUsageExtensions.ToDto);
            this.Condition(whileLoop, "while/condition");
            this.Param<ActionUsage, ParameterMembership>(whileLoop, "while/body", ActionUsageExtensions.ToDto, ParameterMembershipExtensions.ToDto);

            var forLoop = this.Owned<ForLoopActionUsage>(this.ActionDefinition, "for", ForLoopActionUsageExtensions.ToDto);
            var loopVariable = this.Builder.Create<ReferenceUsage>("for/variable", ReferenceUsageExtensions.ToDto);
            loopVariable.DeclaredName = "n";
            this.OwnFeature(forLoop, loopVariable);
            this.Value(this.Param<ReferenceUsage, ParameterMembership>(forLoop, "for/sequence", ReferenceUsageExtensions.ToDto, ParameterMembershipExtensions.ToDto), this.Literal<LiteralInteger>("for/sequence/3", LiteralIntegerExtensions.ToDto, literal => literal.Value = 3));
            this.Param<ActionUsage, ParameterMembership>(forLoop, "for/body", ActionUsageExtensions.ToDto, ParameterMembershipExtensions.ToDto);

            var perform = this.Owned<PerformActionUsage>(this.ActionDefinition, "perform", PerformActionUsageExtensions.ToDto);
            this.Subset(perform, performed);

            this.Owned<TerminateActionUsage>(this.ActionDefinition, "terminate", TerminateActionUsageExtensions.ToDto);

            var action = this.Member<ActionUsage>("act", ActionUsageExtensions.ToDto);
            this.Typed(action, this.ActionDefinition);
        }

        /// <summary>
        /// Adds the calculation and constraint definitions and usages
        /// </summary>
        /// <returns>
        /// The calculation definition and the constraint definition
        /// </returns>
        private (CalculationDefinition Calculation, ConstraintDefinition Constraint) Calculations()
        {
            var calculation = this.Member<CalculationDefinition>("Calc", CalculationDefinitionExtensions.ToDto);
            this.Result(calculation, "Calc");
            calculation.AssignOwnership(this.Builder.Create<ResultExpressionMembership>("Calc/body", ResultExpressionMembershipExtensions.ToDto), this.Literal<LiteralInteger>("Calc/body/3", LiteralIntegerExtensions.ToDto, literal => literal.Value = 3));

            var constraint = this.Member<ConstraintDefinition>("Cons", ConstraintDefinitionExtensions.ToDto);
            this.Result(constraint, "Cons");
            constraint.AssignOwnership(this.Builder.Create<ResultExpressionMembership>("Cons/body", ResultExpressionMembershipExtensions.ToDto), this.Literal<LiteralBoolean>("Cons/body/true", LiteralBooleanExtensions.ToDto, literal => literal.Value = true));

            var asserted = this.Member<ConstraintUsage>("cTop", ConstraintUsageExtensions.ToDto);
            this.Typed(asserted, constraint);

            var assertion = this.Member<AssertConstraintUsage>("assert", AssertConstraintUsageExtensions.ToDto);
            assertion.IsNegated = true;
            this.Subset(assertion, asserted);

            return (calculation, constraint);
        }

        /// <summary>
        /// Adds the case definitions and usages: <c>Cs</c> with its subject, result and objective, which the analysis,
        /// verification and use case definitions specialize
        /// </summary>
        /// <param name="requirement">
        /// The requirement usage <c>rq</c> that the objective verifies
        /// </param>
        private void Cases(RequirementUsage requirement)
        {
            var caseDefinition = this.Member<CaseDefinition>("Cs", CaseDefinitionExtensions.ToDto);
            this.Param<ReferenceUsage, SubjectMembership>(caseDefinition, "Cs/subject", ReferenceUsageExtensions.ToDto, SubjectMembershipExtensions.ToDto);
            this.Result(caseDefinition, "Cs");

            var objective = this.Builder.Create<RequirementUsage>("Cs/objective", RequirementUsageExtensions.ToDto);
            objective.IsComposite = true;
            caseDefinition.AssignOwnership(this.Builder.Create<ObjectiveMembership>("Cs/objective/membership", ObjectiveMembershipExtensions.ToDto), objective);
            this.Typed(objective, this.RequirementBase);

            var verified = this.Builder.Create<RequirementUsage>("Cs/objective/verified", RequirementUsageExtensions.ToDto);
            verified.IsComposite = true;
            var verification = this.Builder.Create<RequirementVerificationMembership>("Cs/objective/verified/membership", RequirementVerificationMembershipExtensions.ToDto);
            verification.Kind = RequirementConstraintKind.Requirement;
            objective.AssignOwnership(verification, verified);
            this.Subset(verified, requirement);

            var analysis = this.Specialized<AnalysisCaseDefinition>("ACD", AnalysisCaseDefinitionExtensions.ToDto, caseDefinition);
            var verificationCase = this.Specialized<VerificationCaseDefinition>("VCD", VerificationCaseDefinitionExtensions.ToDto, caseDefinition);
            var useCase = this.Specialized<UseCaseDefinition>("UCD", UseCaseDefinitionExtensions.ToDto, caseDefinition);

            this.Typed(this.Member<CaseUsage>("cs", CaseUsageExtensions.ToDto), caseDefinition);
            this.Typed(this.Member<AnalysisCaseUsage>("acu", AnalysisCaseUsageExtensions.ToDto), analysis);
            this.Typed(this.Member<VerificationCaseUsage>("vcu", VerificationCaseUsageExtensions.ToDto), verificationCase);

            var useCaseUsage = this.Member<UseCaseUsage>("ucu", UseCaseUsageExtensions.ToDto);
            this.Typed(useCaseUsage, useCase);

            var include = this.Owned<IncludeUseCaseUsage>(useCase, "include", IncludeUseCaseUsageExtensions.ToDto);
            this.Subset(include, useCaseUsage);
        }

        /// <summary>
        /// Makes a Boolean literal the first in parameter of a conditional action
        /// </summary>
        /// <param name="action">
        /// The if or while action
        /// </param>
        /// <param name="name">
        /// The name the literal is derived from
        /// </param>
        private void Condition(IType action, string name)
        {
            var condition = this.Literal<LiteralBoolean>(name, LiteralBooleanExtensions.ToDto, literal => literal.Value = true);
            condition.Direction = FeatureDirectionKind.In;
            action.AssignOwnership(this.Builder.Create<ParameterMembership>($"{name}/membership", ParameterMembershipExtensions.ToDto), condition);
        }

        /// <summary>
        /// Creates a named member of <see cref="Package"/>; a usage owned by a package is referential
        /// </summary>
        /// <typeparam name="TPoco">
        /// The POCO type
        /// </typeparam>
        /// <param name="name">
        /// The declared name
        /// </param>
        /// <param name="toDto">
        /// The generated <c>ToDto</c> extension of <typeparamref name="TPoco"/>
        /// </param>
        /// <returns>
        /// The member
        /// </returns>
        private TPoco Member<TPoco>(string name, Func<TPoco, bool, DtoElement> toDto) where TPoco : PocoElement, new()
        {
            var member = this.CreateNamed(name, name.ToLowerInvariant().Replace(' ', '_'), toDto);
            this.OwnMember(this.Package, member);

            return member;
        }

        /// <summary>
        /// Adds the metadata definition and a metadata usage that annotates the part definition of step 6
        /// </summary>
        /// <param name="structure">
        /// The model of step 6
        /// </param>
        private void Metadata(SystemsStructureModel structure)
        {
            var definition = this.Member<MetadataDefinition>("MD", MetadataDefinitionExtensions.ToDto);

            var usage = this.Member<MetadataUsage>("mu", MetadataUsageExtensions.ToDto);
            this.Typed(usage, definition);

            var about = this.Builder.Create<Annotation>("mu/about P", AnnotationExtensions.ToDto);
            about.AnnotatedElement = structure.Part;
            usage.AssignOwnership(about);
        }

        /// <summary>
        /// Creates a named feature owned by a type through a <see cref="FeatureMembership"/>
        /// </summary>
        /// <typeparam name="TFeature">
        /// The feature POCO type
        /// </typeparam>
        /// <param name="owner">
        /// The owning type
        /// </param>
        /// <param name="name">
        /// The declared name
        /// </param>
        /// <param name="toDto">
        /// The generated <c>ToDto</c> extension of <typeparamref name="TFeature"/>
        /// </param>
        /// <returns>
        /// The feature
        /// </returns>
        private TFeature Owned<TFeature>(IType owner, string name, Func<TFeature, bool, DtoElement> toDto) where TFeature : IFeature, new()
        {
            var feature = this.CreateNamed(name, name.Replace(' ', '_'), toDto);
            this.OwnFeature(owner, feature);

            return feature;
        }

        /// <summary>
        /// Adds an in parameter usage to a behavior, step or requirement through a parameter membership
        /// </summary>
        /// <typeparam name="TUsage">
        /// The parameter usage POCO type
        /// </typeparam>
        /// <typeparam name="TMembership">
        /// The parameter membership POCO type
        /// </typeparam>
        /// <param name="owner">
        /// The owning type
        /// </param>
        /// <param name="name">
        /// The name the parameter is derived from
        /// </param>
        /// <param name="toDto">
        /// The generated <c>ToDto</c> extension of <typeparamref name="TUsage"/>
        /// </param>
        /// <param name="membershipToDto">
        /// The generated <c>ToDto</c> extension of <typeparamref name="TMembership"/>
        /// </param>
        /// <returns>
        /// The parameter usage
        /// </returns>
        private TUsage Param<TUsage, TMembership>(IType owner, string name, Func<TUsage, bool, DtoElement> toDto, Func<TMembership, bool, DtoElement> membershipToDto)
            where TUsage : IFeature, new()
            where TMembership : PocoRelationship, new()
        {
            var parameter = this.Builder.Create(name, toDto);
            parameter.DeclaredName = name[(name.LastIndexOf('/') + 1)..];
            parameter.Direction = FeatureDirectionKind.In;
            owner.AssignOwnership(this.Builder.Create($"{name}/membership", membershipToDto), parameter);

            return parameter;
        }

        /// <summary>
        /// Makes a feature redefine another through a <see cref="Redefinition"/>
        /// </summary>
        /// <param name="feature">
        /// The redefining feature
        /// </param>
        /// <param name="redefined">
        /// The redefined feature
        /// </param>
        private void Redefine(IFeature feature, IFeature redefined)
        {
            var redefinition = this.Builder.Create<Redefinition>($"{feature.ElementId}/redefinition", RedefinitionExtensions.ToDto);
            redefinition.RedefiningFeature = feature;
            redefinition.RedefinedFeature = redefined;
            feature.AssignOwnership(redefinition);
        }

        /// <summary>
        /// Refers a namespace to an element through a plain, non-owning <see cref="Membership"/>
        /// </summary>
        /// <param name="owner">
        /// The referring namespace
        /// </param>
        /// <param name="element">
        /// The referenced element
        /// </param>
        private void Refer(PocoElement owner, PocoElement element)
        {
            var membership = this.Builder.Create<Membership>($"{owner.ElementId}/reference {element.ElementId}", MembershipExtensions.ToDto);
            membership.MemberElement = element;
            membership.MemberName = element.DeclaredName;
            owner.AssignOwnership(membership);
        }

        /// <summary>
        /// Adds the requirement definitions and usages: <c>RBase</c> with its subject and result, <c>R</c> with its actor,
        /// stakeholder, assumed and required constraints and framed concern, and the concern and viewpoint definitions
        /// </summary>
        /// <param name="constraint">
        /// The constraint definition that types the assumed and required constraints
        /// </param>
        /// <param name="structure">
        /// The model of step 6
        /// </param>
        /// <returns>
        /// The requirement usage <c>rq</c>
        /// </returns>
        private RequirementUsage Requirements(ConstraintDefinition constraint, SystemsStructureModel structure)
        {
            this.RequirementBase = this.Member<RequirementDefinition>("RBase", RequirementDefinitionExtensions.ToDto);
            var subject = this.Param<ReferenceUsage, SubjectMembership>(this.RequirementBase, "RBase/subject", ReferenceUsageExtensions.ToDto, SubjectMembershipExtensions.ToDto);
            this.Result(this.RequirementBase, "RBase");

            var concern = this.Specialized<ConcernDefinition>("CDn", ConcernDefinitionExtensions.ToDto, this.RequirementBase);
            this.Specialized<ViewpointDefinition>("VPD", ViewpointDefinitionExtensions.ToDto, this.RequirementBase);

            var requirement = this.Specialized<RequirementDefinition>("R", RequirementDefinitionExtensions.ToDto, this.RequirementBase);
            requirement.ReqId = "R-1";

            var text = this.Builder.Create<Documentation>("R/text", DocumentationExtensions.ToDto);
            text.Body = "The coverage requirement.";
            this.OwnMember(requirement, text);

            this.Param<PartUsage, ActorMembership>(requirement, "R/actor", PartUsageExtensions.ToDto, ActorMembershipExtensions.ToDto);
            this.Param<PartUsage, StakeholderMembership>(requirement, "R/stakeholder", PartUsageExtensions.ToDto, StakeholderMembershipExtensions.ToDto);

            foreach (var (name, kind) in new[] { ("R/assumed", RequirementConstraintKind.Assumption), ("R/required", RequirementConstraintKind.Requirement) })
            {
                var constraintUsage = this.Builder.Create<ConstraintUsage>(name, ConstraintUsageExtensions.ToDto);
                constraintUsage.IsComposite = true;
                var membership = this.Builder.Create<RequirementConstraintMembership>($"{name}/membership", RequirementConstraintMembershipExtensions.ToDto);
                membership.Kind = kind;
                requirement.AssignOwnership(membership, constraintUsage);
                this.Typed(constraintUsage, constraint);
            }

            var framed = this.Builder.Create<ConcernUsage>("R/framed", ConcernUsageExtensions.ToDto);
            framed.IsComposite = true;
            var framing = this.Builder.Create<FramedConcernMembership>("R/framed/membership", FramedConcernMembershipExtensions.ToDto);
            framing.Kind = RequirementConstraintKind.Requirement;
            requirement.AssignOwnership(framing, framed);
            this.Typed(framed, concern);

            var usage = this.Member<RequirementUsage>("rq", RequirementUsageExtensions.ToDto);
            this.Typed(usage, requirement);

            var satisfy = this.Member<SatisfyRequirementUsage>("satisfy", SatisfyRequirementUsageExtensions.ToDto);
            this.Subset(satisfy, usage);

            var binding = this.Builder.Create<BindingConnector>("satisfy/binding", BindingConnectorExtensions.ToDto);
            this.OwnFeature(satisfy, binding);

            foreach (var (name, related) in new[] { ("satisfy/binding/subject", (IFeature)subject), ("satisfy/binding/satisfier", this.Builder.Find<PartUsage>("systems structure/sub1")) })
            {
                var end = this.Builder.Create<ReferenceUsage>(name, ReferenceUsageExtensions.ToDto);
                end.IsEnd = true;
                this.OwnFeature(binding, end);
                this.MultiplicityOne(end, binding);
                this.Subset(end, related);
            }

            return usage;
        }

        /// <summary>
        /// Creates a named member of <see cref="Package"/> that specializes a definition through a
        /// <see cref="Subclassification"/>
        /// </summary>
        /// <typeparam name="TDefinition">
        /// The definition POCO type
        /// </typeparam>
        /// <param name="name">
        /// The declared name
        /// </param>
        /// <param name="toDto">
        /// The generated <c>ToDto</c> extension of <typeparamref name="TDefinition"/>
        /// </param>
        /// <param name="general">
        /// The specialized definition
        /// </param>
        /// <returns>
        /// The definition
        /// </returns>
        private TDefinition Specialized<TDefinition>(string name, Func<TDefinition, bool, DtoElement> toDto, IClassifier general) where TDefinition : IClassifier, new()
        {
            var definition = this.Member(name, toDto);

            var subclassification = this.Builder.Create<Subclassification>($"{name}/subclassification", SubclassificationExtensions.ToDto);
            subclassification.Subclassifier = definition;
            subclassification.Superclassifier = general;
            definition.AssignOwnership(subclassification);

            return definition;
        }

        /// <summary>
        /// Adds the state definition <c>St</c> with entry, do and exit subactions, two states and a transition between
        /// them with a trigger, a guard and an effect, a state usage that a part definition exhibits, and a parallel state
        /// definition and usage, whose substates have no transitions
        /// </summary>
        private void States()
        {
            var state = this.Member<StateDefinition>("St", StateDefinitionExtensions.ToDto);

            foreach (var (name, kind) in new[] { ("entry", StateSubactionKind.Entry), ("do", StateSubactionKind.Do), ("exit", StateSubactionKind.Exit) })
            {
                PocoElement action = kind == StateSubactionKind.Entry
                    ? this.Builder.Create<SendActionUsage>($"St/{name}", SendActionUsageExtensions.ToDto)
                    : this.Builder.Create<ActionUsage>($"St/{name}", ActionUsageExtensions.ToDto);

                ((IFeature)action).IsComposite = true;
                var membership = this.Builder.Create<StateSubactionMembership>($"St/{name}/membership", StateSubactionMembershipExtensions.ToDto);
                membership.Kind = kind;
                state.AssignOwnership(membership, action);

                if (action is SendActionUsage send)
                {
                    this.Value(this.Param<ReferenceUsage, ParameterMembership>(send, $"St/{name}/payload", ReferenceUsageExtensions.ToDto, ParameterMembershipExtensions.ToDto), this.Literal<LiteralInteger>($"St/{name}/payload/2", LiteralIntegerExtensions.ToDto, literal => literal.Value = 2));
                }
            }

            var first = this.Owned<StateUsage>(state, "s1", StateUsageExtensions.ToDto);
            first.IsComposite = true;
            var second = this.Owned<StateUsage>(state, "s2", StateUsageExtensions.ToDto);
            second.IsComposite = true;

            var transition = this.Owned<TransitionUsage>(state, "t", TransitionUsageExtensions.ToDto);
            this.Refer(transition, first);
            this.Param<ReferenceUsage, ParameterMembership>(transition, "t/source", ReferenceUsageExtensions.ToDto, ParameterMembershipExtensions.ToDto);
            this.Param<ReferenceUsage, ParameterMembership>(transition, "t/payload", ReferenceUsageExtensions.ToDto, ParameterMembershipExtensions.ToDto);

            var succession = this.Builder.Create<Succession>("t/succession", SuccessionExtensions.ToDto);
            this.OwnFeature(transition, succession);

            foreach (var (name, related) in new[] { ("t/succession/first", first), ("t/succession/then", second) })
            {
                var end = this.Builder.Create<Feature>(name, FeatureExtensions.ToDto);
                end.IsEnd = true;
                this.OwnFeature(succession, end);
                this.MultiplicityOne(end, succession);
                this.Subset(end, related);
            }

            var trigger = this.Builder.Create<AcceptActionUsage>("t/trigger", AcceptActionUsageExtensions.ToDto);
            this.Param<ReferenceUsage, ParameterMembership>(trigger, "t/trigger/payload", ReferenceUsageExtensions.ToDto, ParameterMembershipExtensions.ToDto);
            var guard = this.Literal<LiteralBoolean>("t/guard", LiteralBooleanExtensions.ToDto, literal => literal.Value = true);
            var effect = this.Builder.Create<ActionUsage>("t/effect", ActionUsageExtensions.ToDto);

            foreach (var (kind, feature) in new[] { (TransitionFeatureKind.Trigger, (IFeature)trigger), (TransitionFeatureKind.Guard, guard), (TransitionFeatureKind.Effect, effect) })
            {
                feature.IsComposite = true;
                var membership = this.Builder.Create<TransitionFeatureMembership>($"t/{kind}/membership", TransitionFeatureMembershipExtensions.ToDto);
                membership.Kind = kind;
                transition.AssignOwnership(membership, feature);
            }

            var exhibited = this.Member<StateUsage>("stu", StateUsageExtensions.ToDto);
            this.Typed(exhibited, state);

            var exhibit = this.Owned<ExhibitStateUsage>(this.Member<PartDefinition>("Exhibitor", PartDefinitionExtensions.ToDto), "exhibit", ExhibitStateUsageExtensions.ToDto);
            this.Subset(exhibit, exhibited);

            var parallel = this.Member<StateDefinition>("Par", StateDefinitionExtensions.ToDto);
            parallel.IsParallel = true;
            this.Owned<StateUsage>(parallel, "pa1", StateUsageExtensions.ToDto);
            this.Owned<StateUsage>(parallel, "pa2", StateUsageExtensions.ToDto);

            var parallelUsage = this.Member<StateUsage>("pu", StateUsageExtensions.ToDto);
            parallelUsage.IsParallel = true;
            this.Typed(parallelUsage, parallel);
        }

        /// <summary>
        /// Makes a feature reference another through a <see cref="ReferenceSubsetting"/>
        /// </summary>
        /// <param name="feature">
        /// The referencing feature
        /// </param>
        /// <param name="referenced">
        /// The referenced feature
        /// </param>
        private void Subset(IFeature feature, IFeature referenced)
        {
            var reference = this.Builder.Create<ReferenceSubsetting>($"{feature.ElementId}/reference", ReferenceSubsettingExtensions.ToDto);
            reference.ReferencedFeature = referenced;
            feature.AssignOwnership(reference);
        }

        /// <summary>
        /// Gives a feature a value through a <see cref="FeatureValue"/>
        /// </summary>
        /// <param name="feature">
        /// The feature
        /// </param>
        /// <param name="value">
        /// The value expression
        /// </param>
        private void Value(IFeature feature, IExpression value)
        {
            feature.AssignOwnership(this.Builder.Create<FeatureValue>($"{feature.ElementId}/value", FeatureValueExtensions.ToDto), value);
        }

        /// <summary>
        /// Adds the view, viewpoint and rendering definitions and usages, and a view usage that exposes the part definition
        /// and the package of step 6
        /// </summary>
        /// <param name="structure">
        /// The model of step 6
        /// </param>
        private void Views(SystemsStructureModel structure)
        {
            var rendering = this.Member<RenderingDefinition>("RD", RenderingDefinitionExtensions.ToDto);
            this.Typed(this.Member<RenderingUsage>("rend", RenderingUsageExtensions.ToDto), rendering);

            var view = this.Member<ViewDefinition>("VD", ViewDefinitionExtensions.ToDto);
            var viewRendering = this.Builder.Create<RenderingUsage>("VD/rendering", RenderingUsageExtensions.ToDto);
            viewRendering.IsComposite = true;
            view.AssignOwnership(this.Builder.Create<ViewRenderingMembership>("VD/rendering/membership", ViewRenderingMembershipExtensions.ToDto), viewRendering);
            this.Typed(viewRendering, rendering);

            var usage = this.Member<ViewUsage>("v", ViewUsageExtensions.ToDto);
            this.Typed(usage, view);

            var viewpoint = this.Owned<ViewpointUsage>(usage, "vp", ViewpointUsageExtensions.ToDto);
            viewpoint.IsComposite = true;
            this.Typed(viewpoint, this.Builder.Find<ViewpointDefinition>("systems behaviour/VPD"));

            var membershipExpose = this.Builder.Create<MembershipExpose>("v/expose P", MembershipExposeExtensions.ToDto);
            membershipExpose.IsImportAll = true;
            membershipExpose.Visibility = VisibilityKind.Protected;
            membershipExpose.ImportedMembership = (IMembership)structure.Part.OwningRelationship;
            usage.AssignOwnership(membershipExpose);

            var namespaceExpose = this.Builder.Create<NamespaceExpose>("v/expose Systems", NamespaceExposeExtensions.ToDto);
            namespaceExpose.IsImportAll = true;
            namespaceExpose.Visibility = VisibilityKind.Protected;
            namespaceExpose.ImportedNamespace = structure.Package;
            usage.AssignOwnership(namespaceExpose);
        }
    }
}
