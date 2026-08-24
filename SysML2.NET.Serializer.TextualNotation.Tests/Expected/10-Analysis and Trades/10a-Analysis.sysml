package '10a-Analysis' {
    private import ISQ::*;
    private import SI::*;
    private import NumericalFunctions::*;
    package VehicleDesignModel {
        part def Vehicle {
            ref mass: MassValue;
        }
        part vehicle {
            :>> mass : MassValue = sum((vehicle.engine.mass, vehicle.transmission.mass, vehicle.frontAxleAssembly.mass, vehicle.rearAxleAssembly.mass));
            part engine {
                ref mass: MassValue;
            }
            part transmission {
                ref mass: MassValue;
            }
            part frontAxleAssembly {
                ref mass: MassValue;
            }
            part rearAxleAssembly {
                ref mass: MassValue;
            }
        }
    }
    package VehicleAnalysisModel {
        private import VehicleDesignModel::Vehicle;
        requirement def MassAnalysisObjective {
            subject mass: MassValue;
            doc
            /* ... */
        }
        analysis def MassAnalysisCase {
            subject vehicle: Vehicle;
            objective : MassAnalysisObjective {
                subject = Cases::Case::result;
            }
            vehicle.mass }
        analysis def AnalysisPlan {
            subject vehicle: Vehicle;
            objective {
                doc
                /* ... */
            }
            analysis massAnalysisCase: MassAnalysisCase {
                /*
                 * By default, the subject of a nested analysis case bound to that
                 * of its containing analysis case or analysis case definition.
                 */
                return mass;
            }
        }
        part massAnalysisContext {
            analysis analysisPlan: AnalysisPlan {
                subject vehicle = VehicleDesignModel::vehicle;
            }
        }
    }
}
