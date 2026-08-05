package '3c-Function-based Behavior-structure mod-1' {
    part def Vehicle;
    part def VehicleFrame;
    part def HitchBall;
    part def TrailerCoupler;
    part def Trailer;
    part def TrailerFrame;
    connection def TrailerHitch {
        end ref hitch: HitchBall;
        end ref coupler: TrailerCoupler;
    }
    ref part 'vehicle-trailer system' {
        part vehicle: Vehicle {
            part vehicleFrame: VehicleFrame {
                part hitch: HitchBall;
            }
        }
        connection trailerHitch: TrailerHitch[0..1] connect vehicle.vehicleFrame.hitch to trailer.trailerFrame.coupler;
        part trailer: Trailer {
            part trailerFrame: TrailerFrame {
                part coupler: TrailerCoupler;
            }
        }
        action {
            action 'connect trailer to vehicle' assign 'vehicle-trailer system'.trailerHitch := new TrailerHitch();
            then action 'destroy connection of trailer to vehicle': OccurrenceFunctions::destroy {
                inout ref occ = 'vehicle-trailer system'.trailerHitch;
            }
            then action 'disconnect trailer from vehicle' assign 'vehicle-trailer system'.trailerHitch := null;
        }
    }
}
