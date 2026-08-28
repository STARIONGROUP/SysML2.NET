package '13b-Safety and Security Features Element Group-2' {
    private import ScalarValues::*;
    private import AnnotationDefinitions::*;
    private import PartsTree::*;
    package AnnotationDefinitions {
        metadata def Safety {
            attribute isMandatory: Boolean;
        }
        metadata def Security;
    }
    package PartsTree {
        part vehicle {
            part interior {
                #Security part alarm;
                part seatBelt[2] {
                    @ Safety {
                        ref :>> isMandatory = true;
                    }
                }
                part frontSeat[2];
                part driverAirBag {
                    @ Safety {
                        ref :>> isMandatory = false;
                    }
                }
            }
            part bodyAssy {
                part body;
                part bumper {
                    @ Safety {
                        ref :>> isMandatory = true;
                    }
                }
                #Security part keylessEntry;
            }
            part wheelAssy {
                part wheel[2];
                part antilockBrakes[2] {
                    @ Safety {
                        ref :>> isMandatory = false;
                    }
                }
            }
        }
    }
    package 'Safety Features' {
        /* Parts that contribute to safety. */
        public import vehicle::**[@ '13b-Safety and Security Features Element Group-2'::AnnotationDefinitions::Safety];
    }
    package 'Security Features' {
        /* Parts that contribute to security. */
        public import vehicle::**[@ '13b-Safety and Security Features Element Group-2'::AnnotationDefinitions::Security];
    }
    package 'Safety & Security Features' {
        /* Parts that contribute to safety OR security. */
        public import vehicle::**[@ '13b-Safety and Security Features Element Group-2'::AnnotationDefinitions::Safety or @ '13b-Safety and Security Features Element Group-2'::AnnotationDefinitions::Security];
    }
    package 'Mandatory Saftey Features' {
        /* Parts that contribute to safety AND are mandatory. */
        public import vehicle::**[@ '13b-Safety and Security Features Element Group-2'::AnnotationDefinitions::Safety and (as '13b-Safety and Security Features Element Group-2'::AnnotationDefinitions::Safety).isMandatory];
    }
}
