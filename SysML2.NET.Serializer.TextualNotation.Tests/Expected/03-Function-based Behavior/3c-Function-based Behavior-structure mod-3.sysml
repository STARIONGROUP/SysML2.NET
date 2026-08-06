package '3c-Function-based Behavior-structure mod-3' {
    part def Vehicle;
    part def VehicleFrame;
    part def HitchBall;
    part def Trailer;
    part def TrailerFrame;
    part def TrailerCoupler;
    part vehicle: Vehicle {
        part vehicleFrame: VehicleFrame {
            part hitch: HitchBall;
        }
    }
    part trailer: Trailer {
        part trailerFrame: TrailerFrame {
            part coupler: TrailerCoupler {
                ref part hitch: HitchBall;
            }
        }
    }
    action {
        action 'connect trailer to vehicle' assign trailer.trailerFrame.coupler.hitch := vehicle.vehicleFrame.hitch;
        then action 'disconnect trailer from vehicle' assign trailer.trailerFrame.coupler.hitch := null;
    }
}
