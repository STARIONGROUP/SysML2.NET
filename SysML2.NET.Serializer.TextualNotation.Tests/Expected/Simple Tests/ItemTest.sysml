package ItemTest {
    item f: A;
    item def A {
        item b: B;
        protected ref part c: C;
    }
    abstract item def B {
        abstract part a: A;
    }
    private part def C {
        private in y: A, B;
    }
    port def P {
        in item a1: A;
        out item a2: A;
    }
}
