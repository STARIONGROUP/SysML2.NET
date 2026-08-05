package '3d-Function-based Behavior-item' {
    private import ScalarValues::Real;
    public import Definitions::*;
    public import Usages::*;
    package Definitions {
        item def Fuel;
        port def FuelPort {
            out ref item fuel: Fuel;
        }
        part def Pump {
            ref port fuelInPort: ~FuelPort;
            ref port fuelOutPort: FuelPort;
        }
        part def StorageTank {
            ref port fuelOutPort: FuelPort;
        }
        part def FuelTank {
            ref port fuelInPort: ~FuelPort;
        }
        part def Vehicle {
            ref port fuelInPort: ~FuelPort;
        }
        action def PumpFuel {
            in ref fuelIn: Fuel;
            out ref fuelOut: Fuel;
        }
    }
    package Usages {
        ref part context {
            /* Storage Element */
            part storageTank: StorageTank;
            flow of fuel: Fuel from storageTank.fuelOutPort.fuel to pump.fuelInPort.fuel {
                /*
                 * Note: Explicitly notating that the flow is "of fuel : Fuel" is optional.
                 */
            }
            part pump: Pump {
                perform action pumpFuel: PumpFuel {
                    in ref fuelIn = fuelInPort.fuel;
                    out ref fuelOut = fuelOutPort.fuel;
                }
            }
            flow of fuel: Fuel from pump.fuelOutPort.fuel to vehicle.fuelInPort.fuel;
            part vehicle: Vehicle {
                flow fuelInPort.fuel to fuelTank.fuel {
                    /*
                     * Note: The semantics of flowing to a "stored item" is tentative.
                     */
                }
                /* Storage Element */
                part fuelTank: FuelTank {
                    attribute volumeMax: Real;
                    attribute fuelLevel: Real = fuel.volume / volumeMax;
                    /* Stored Item */
                    item fuel: Fuel {
                        attribute volume: Real;
                        /* isConserved = true */
                    }
                }
            }
        }
    }
}
