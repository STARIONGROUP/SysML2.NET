package 'Redefinition Example' {
    part def Vehicle {
        part eng: Engine;
    }
    part def SmallVehicle :> Vehicle {
        part smallEng: SmallEngine :>> eng;
    }
    part def BigVehicle :> Vehicle {
        part bigEng: BigEngine :>> eng;
    }
    part def Engine {
        part cyl: Cylinder[4..6];
    }
    part def SmallEngine :> Engine {
        part :>> cyl[4];
    }
    part def BigEngine :> Engine {
        part :>> cyl[6];
    }
    part def Cylinder;
}
