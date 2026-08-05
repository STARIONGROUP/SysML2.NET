package '3a-Function-based Behavior-5' {
    public import Definitions::*;
    public import Usages::*;
    package Definitions {
        alias Torque for ISQ::TorqueValue;
        attribute def FuelCmd;
        attribute def EngineStart;
        attribute def EngineOff;
        action def 'Generate Torque' {
            in ref fuelCmd: FuelCmd;
            out ref engineTorque: Torque;
        }
        action def 'Amplify Torque' {
            in ref engineTorque: Torque;
            out ref transmissionTorque: Torque;
        }
        action def 'Transfer Torque' {
            in ref transmissionTorque: Torque;
            out ref driveshaftTorque: Torque;
        }
        action def 'Distribute Torque' {
            in ref driveShaftTorque: Torque;
            out ref wheelTorque1: Torque;
            out ref wheelTorque2: Torque;
        }
        action def 'Provide Power' {
            in ref fuelCmd: FuelCmd;
            out ref wheelTorque1: Torque;
            out ref wheelTorque2: Torque;
        }
    }
    package Usages {
        ref action 'provide power': 'Provide Power' {
            in ref fuelCmd: FuelCmd;
            out ref wheelTorque1: Torque;
            out ref wheelTorque2: Torque;
            loop {
                accept engineStart: EngineStart;
                then action {
                    action 'generate torque': 'Generate Torque' {
                        in ref fuelCmd = 'provide power'::fuelCmd;
                        out ref engineTorque: Torque;
                    }
                    flow 'generate torque'.engineTorque to 'amplify torque'.engineTorque;
                    action 'amplify torque': 'Amplify Torque' {
                        in ref engineTorque: Torque;
                        out ref transmissionTorque: Torque;
                    }
                    flow 'amplify torque'.transmissionTorque to 'transfer torque'.transmissionTorque;
                    action 'transfer torque': 'Transfer Torque' {
                        in ref transmissionTorque: Torque;
                        out ref driveshaftTorque: Torque;
                    }
                    flow 'transfer torque'.driveshaftTorque to 'distribute torque'.driveshaftTorque;
                    action 'distribute torque': 'Distribute Torque' {
                        in ref driveshaftTorque: Torque;
                        out ref wheelTorque1: Torque;
                        out ref wheelTorque2: Torque;
                    }
                }
                then accept engineOff: EngineOff;
            }
        }
    }
}
