package PartTest {
    part f: A;
    part def A {
        part <'1'> b: xx;
        protected port c: C;
        constant attribute x[0..2];
        derived constant attribute y :> x;
        ref z: ScalarValues::Integer;
    }
    item def S;
    abstract part def <xx> B {
        abstract part a: A[1..2];
        abstract part b :> a;
        abstract part c[0..1] :> a;
        port x: ~C {
            port p;
            ref port q;
        }
        package P;
        succession flow x.p to a1.aa.receiver;
        action a1 {
            accept S via x;
            action aa accept S;
        }
        perform action a2;
        state s1;
        exhibit state s2;
    }
    private port def C {
        private in y: A, xx {
            part B_b :>> xx::b;
            part B_c :>> xx::c;
            port B_x :>> xx::x;
        }
        alias z1 for y;
        alias z2 for y;
        port c1: C;
        ref port c2: C;
    }
    part p1 :> p2;
    part p2 :> p3;
    part p3 :> p1;
    part p4 :> p4;
}
