package '14a-Language Extensions' {
    private import 'User Defined Extensions'::*;
    package 'User Defined Extensions' {
        enum def ClassificationLevel {
            enum uncl;
            enum conf;
            enum secret;
        }
        metadata def Classified {
            :>> annotatedElement : SysML::Systems::PartUsage;
            attribute classificationLevel: ClassificationLevel[1];
        }
    }
    part part_X {
        @ Classified {
            ref :>> classificationLevel = ClassificationLevel::conf;
        }
    }
    part part_Y {
        @ Classified {
            ref :>> classificationLevel = ClassificationLevel::conf;
        }
    }
}
