package '13a-Model Containment' {
    private import '2a-Parts Interconnection'::*;
    private import '8-Requirements'::*;
    requirement BodyAndInteriorRequirements {
        public import '1';
    }
    requirement PowerTrainRequirements;
    package 'Vehicle Model' {
        doc
        /*
         * This package is used to represent a top-level "model".
         * There is no specific syntax for identifying a package
         * used in this way.
         */

        package 'Vehicle1-Configuration' {
            alias 'Sport Sedan' for Usages::vehicle1_c1;
            public import 'vehicle1_c1 Specification Context'::'vehicle1-c1 Specification';
        }
        package 'Vehicle Reference Model' {
            doc
            /*
             * This package is used to represent a "model library".
             * There is no specific syntax for identifying a package
             * used in this way.
             */

            public import VehicleA;
            public import VehicleSubsystems;
        }
        package VehicleSubsystems {
            public import 'Body&Interior';
            public import PowerTrain;
        }
        package 'Body&Interior' {
            public import BodyAndInteriorRequirements;
        }
        package PowerTrain {
            public import Definitions::Engine;
            public import Definitions::Transmission;
            public import PowerTrainRequirements;
        }
    }
}
