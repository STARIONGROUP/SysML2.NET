package '2c-Parts Interconnection-Multiple Decompositions' {
    part def A1;
    part def B11 {
        ref port pe;
    }
    part def B12 {
        ref port pf;
    }
    part def B21 {
        ref port pg;
    }
    part def B22 {
        ref port ph;
    }
    part def C1 {
        ref port pa;
        ref port pb;
    }
    part def C2 {
        ref port pc;
    }
    part def C3 {
        ref port pd;
    }
    part def C4;
    ref part a11: A1 {
        doc
        /*
         * Decomposition 1 - Subsystems b11, b12
         */

        part b11: B11 {
            part c1: C1;
            part c2: C2;
            connect c1.pa to c2.pc;
            ref port :>> pe = c1.pb {
                doc
                /*
                 * This combines the definition of a port with a binding
                 * connector. (It is the same notation used to bind a
                 * attribute to a attribute property or a reference to a reference
                 * property.)
                 */

            }
        }
        part b12: B12 {
            part c3: C3;
            part c4: C4;
            ref port :>> pf = c3.pd;
        }
        connect b11.pe to b12.pf;
    }
    ref part a12: A1 {
        doc
        /*
         * Decomposition 2 - Assemblies b21, b22
         */

        part b21: B21 {
            /*
             * The c-level entities are already composite parts within
             * a11, so they cannot also be composite parts within a12.
             */
            ref c1: C1 = a11.b11.c1;
            ref c3: C3 = a11.b12.c3;
            connect c1.pb to c3.pd;
            ref port :>> pg = c1.pa;
        }
        part b22: B22 {
            ref c2: C2 = a11.b11.c2;
            ref c4: C4 = a11.b12.c4;
            ref port :>> ph = c2.pc;
        }
    }
}
