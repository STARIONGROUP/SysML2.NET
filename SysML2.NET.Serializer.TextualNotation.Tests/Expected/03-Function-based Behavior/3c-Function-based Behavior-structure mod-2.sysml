package '3c-Function-based Behavior-structure mod-2' {
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
    part 'vehicle-trailer system' {
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
        perform action {
            action 'connect trailer to vehicle' {
                abstract ref :>> trailerHitch[1];
            }
            then action 'disconnect trailer from vehicle' {
                abstract ref :>> trailerHitch[0];
            }
        }
    }
}
