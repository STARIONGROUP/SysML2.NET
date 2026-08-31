package '15_01-Constants' {
    private import MeasurementReferences::*;
    private import SI::*;
    private import RealFunctions::*;
    /*
     * Note: Value properties that are bound to specific values are constants and have the specified
     * values in all contexts. It is not legal to redefine them.
     */
    package 'Mathematical Constants' {
        doc
        /*
         * Standard mathematical constants
         *
         * Irrational constants cannot be represented exactly with finite precision.
         * However, they can be required to be implemented with a attribute that is accurate
         * to at least a certain precision.
         *
         * (The decimal literals here should be interpreted as being fixed point and exact.)
         */

        attribute e: Real {
            assert constraint { round(e * 1E+20) == 2.7182818284590452E+20}
        }
        attribute pi: Real {
            assert constraint { round(pi * 1E+20) == 3.1415926535897933E+20}
        }
    }
    package 'Fundamental Physical Constants' {
        doc
        /*
         * Standard fundamental physical constants
         *
         * Physical constants have a standard measured attribute to a finite precision.
         *
         * The reference source is:
         * CODATA - Task Group on Fundamental Physical Constants (TGFC) - 2018 CODATA recommended values
         * See https://codata.org/initiatives/strategic-programme/fundamental-physical-constants/
         * For the actual values see https://pml.nist.gov/cuu/Constants/
         */

        attribute 'fine structure constant': DimensionOneValue = 0.0072973525693[one];
        attribute 'electron to proton mass ratio': DimensionOneValue = 0.000544617021487[one];
        attribute 'speed of light in vacuum': SpeedValue = 299792458[m / s];
    }
    package 'Global Context' {
        attribute 'nominal earth gravitational acceleration': AccelerationValue = 9.80665['m⋅s⁻²'];
    }
    package 'Model X Context' {
        attribute 'amplifier gain': DimensionOneValue = 3.5[one];
    }
}
