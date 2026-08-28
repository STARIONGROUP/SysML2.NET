package '13b-Safety and Security Features Element Group-1' {
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
        public import vehicle::**;
        filter @ Safety;
    }
    package 'Security Features' {
        /* Parts that contribute to security. */
        public import vehicle::**;
        filter @ Security;
    }
    package 'Safety & Security Features' {
        /* Parts that contribute to safety OR security. */
        public import vehicle::**;
        filter @ Safety or @ Security;
    }
    package 'Mandatory Safety Features' {
        /* Parts that contribute to safety AND are mandatory. */
        public import vehicle::**;
        filter @ Safety and (as Safety).isMandatory;
    }
}
