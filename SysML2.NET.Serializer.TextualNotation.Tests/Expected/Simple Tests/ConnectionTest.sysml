package ConnectionTest {
    part p {
        part x {
            part x1;
        }
    }
    part def P {
        part y;
        connect p to y;
        part p1 :> p;
        connect p1.x to y;
        connect p1.x.x1 to y;
        part a;
        part b;
        bind a = b;
        binding ab bind a = b;
        binding ab1: AB bind a = b;
        first a then b;
        succession s first a then b;
        succession s1: AB first a then b;
    }
    abstract connection def C {
        part p;
        end ref end1;
        end ref end2;
        end ref end3;
    }
    part d1;
    part d2;
    part d3;
    part d4;
    connection bus: C connect (d1, d2, d3, d4);
    connection : C {
        end ref :>> end1 ::> d1;
        end ref end2 ::> d2;
        end ref end3 ::> d3;
    }
    connection {
        part q;
        end ref end1 ::> d1 :> q;
        end ref end2 ::> d2;
    }
    abstract flow def F;
    abstract message : F from p to p;
    part def A {
        ref b: B;
    }
    part def B;
    connection def AB {
        end[1] item a: A {
            @ M;
        }
        end ref b: B;
    }
    metadata def M;
}
