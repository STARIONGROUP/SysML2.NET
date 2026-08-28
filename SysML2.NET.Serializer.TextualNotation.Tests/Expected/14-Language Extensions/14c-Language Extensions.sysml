package '14c-Language-Extensions' {
    private import ScalarValues::*;
    library package FMEALibrary {
        abstract occurrence def Situation;
        abstract occurrence situations: Situation[*] nonunique;
        occurrence def Cause :> Situation {
            attribute occurs[0..1] : Real;
        }
        abstract occurrence causes: Cause[*] nonunique;
        occurrence def FailureMode :> Situation {
            attribute detected[0..1] : Real;
        }
        abstract occurrence failureModes: FailureMode[*] nonunique;
        occurrence def Effect :> Situation {
            attribute severity[0..1] : String;
        }
        abstract occurrence effects: Effect[*] nonunique;
        item def FMEAItem :> Situation {
            attribute RPN: Real[0..1];
            occurrence :>> causes;
            occurrence :>> failureModes;
            occurrence :>> effects;
        }
        abstract item fmeaItems: FMEAItem[*] nonunique;
        connection def Causation :> Occurrences::HappensBefore {
            end[*] ref cause: Situation;
            end[*] ref effect: Situation;
        }
        abstract connection causations: Causation[*] nonunique;
        requirement def FMEARequirement;
        abstract requirement fmeaRequirements: FMEARequirement[*] nonunique;
        requirement def RequirementWithSIL :> FMEARequirement {
            attribute sil: SIL;
        }
        enum def SIL {
            enum A;
            enum B;
            enum C;
        }
        connection def Violation {
            end[*] ref sit: Situation;
            end[*] ref req: FMEARequirement;
        }
        abstract connection violations: Violation[*] nonunique;
        abstract connection def ControllingMeasure {
            end[*] ref sit: Situation;
            end[*] ref req: FMEARequirement;
        }
        connection def Prevention :> ControllingMeasure;
        abstract connection preventions: Prevention[*] nonunique;
        connection def Mitigation :> ControllingMeasure;
        abstract connection mitigations: Mitigation[*] nonunique;
    }
    library package FMEAMetadata {
        private import Metaobjects::SemanticMetadata;
        private import FMEALibrary::*;
        enum def Status {
            enum Approved;
            enum NotApproved;
        }
        metadata def StatusHolder {
            ref status: Status;
        }
        metadata def <situation> SituationMetadata :> SemanticMetadata {
            :>> baseType default = situations meta SysML::Systems::Usage;
        }
        metadata def <cause> CauseMetadata :> situation {
            :>> baseType = causes meta SysML::Systems::Usage;
        }
        metadata def <failure> FailureModeMetadata :> situation {
            :>> baseType = failureModes meta SysML::Systems::Usage;
        }
        metadata def <effect> EffectMetadata :> situation {
            :>> baseType = effects meta SysML::Systems::Usage;
        }
        metadata def <fmea> FMEAItemMetadata :> situation {
            :> annotatedElement : SysML::Systems::ItemDefinition;
            :> annotatedElement : SysML::Systems::ItemUsage;
            :>> baseType = fmeaItems meta SysML::Systems::Usage;
        }
        metadata def <causation> CausationMetadata :> SemanticMetadata {
            :>> annotatedElement : SysML::Systems::ConnectionUsage;
            :>> baseType = causations meta SysML::Systems::Usage;
        }
        metadata def <fmeaspec> FMEARequirementMetadata :> SemanticMetadata {
            :>> annotatedElement : SysML::Systems::RequirementUsage;
            :>> baseType = fmeaRequirements meta SysML::Systems::Usage;
        }
        metadata def <violation> ViolationMetadata :> SemanticMetadata {
            :>> annotatedElement : SysML::Systems::ConnectionUsage;
            :>> baseType = violations meta SysML::Systems::Usage;
        }
        abstract metadata def ControllingMeasureMetadata :> SemanticMetadata {
            :>> annotatedElement : SysML::Systems::ConnectionUsage;
        }
        metadata def <prevention> PreventionMetadata :> ControllingMeasureMetadata {
            :>> baseType = preventions meta SysML::Systems::Usage;
        }
        metadata def <mitigation> MitigationMetadata :> ControllingMeasureMetadata {
            :>> baseType = mitigations meta SysML::Systems::Usage;
        }
    }
    package FMEAUserModel {
        private import FMEALibrary::*;
        private import FMEAMetadata::*;
        #fmeaspec requirement req1 {
            doc
            /* Meter designed according to ISO00124 */
        }
        #fmeaspec requirement req2 {
            doc
            /* Device working for 1 week without the need to replace batteries */
        }
        #fmeaspec requirement req3: RequirementWithSIL {
            @ StatusHolder {
                ref :>> status = Status::Approved;
            }
            doc
            /* Alarm when battery has sank */
            :>> sil = SIL::A;
        }
        #fmea item def 'Glucose FMEA Item' {
            #prevention connect 'battery depleted' to req1;
            #cause occurrence 'battery depleted' {
                :>> Cause::occurs = 0.005;
            }
            #causation connect 'battery depleted' to 'battery cannot be charged';
            #failure occurrence 'battery cannot be charged' {
                :>> FailureMode::detected = 0.013;
            }
            #causation connect 'battery cannot be charged' to 'glucose level undetected';
            #effect occurrence 'glucose level undetected';
            #causation connect 'glucose level undetected' to 'therapy delay';
            #effect occurrence 'therapy delay' {
                :>> Effect::severity = "High";
            }
        }
        #violation connect 'Glucose Meter in Use' to req2;
        #mitigation connect 'Glucose Meter in Use' to req3;
        #fmea item 'Glucose Meter in Use': 'Glucose FMEA Item' {
            part 'glucose meter' {
                event 'glucose level undetected'[*];
                part battery {
                    event 'battery depleted'[*];
                    event 'battery cannot be charged'[*];
                }
                part pump;
                part reservoir;
            }
            part patient {
                event 'therapy delay'[*];
            }
        }
    }
}
