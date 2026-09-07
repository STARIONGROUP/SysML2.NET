package EnumerationTest {
    attribute def Color {
        attribute val: ScalarValues::Natural;
    }
    enum def ColorKind :> Color {
        doc
        /*
         * An EnumerationDefinition can contain only EnumerationUsages. However,
         * it can specialize an AttributeDefinition in order to inherit
         * common features for its enumeration values.
         */

        enum red {
            :>> val = 0;
        }
        enum blue {
            :>> val = 1;
        }
        enum green {
            :>> val = 2;
        }
    }
    enum color: ColorKind;
    enum color1 = ColorKind::blue;
    attribute color2: ColorKind = color1;
    enum def E1 {
        enum a;
        enum b;
        enum c;
        doc
        /*
         * The "enum" keyword is optional for EnumerationUsages used to define the
         * enumerated values of an EnumerationDefinition.
         */

    }
    enum def E2;
    attribute def Size :> ScalarValues::Real {
        doc
        /*
         * An EnumerationDefinition can also be used to restrict a supertype to
         * specific values.
         */

    }
    enum def SizeChoice :> Size {
        enum = 60.0;
        enum = 70.0;
        enum = 80.0;
    }
    enum size: SizeChoice = 60.0;
}
