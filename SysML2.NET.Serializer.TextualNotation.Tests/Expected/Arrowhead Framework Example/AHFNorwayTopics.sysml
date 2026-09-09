package AHFNorway {
    doc
    /* This is the Norwegian use-case for Arrowhead Framework */
    private import AHFProfileLib::*;
    private import AHFProfileMetadata::*;
    private import AHFCoreLib::**;
    private import ScalarValues::*;
    #service def APISService {
        doc
        /* Service design */
        attribute :>> SD::serviceDefinition = "APISPullService";
        attribute :>> SD::intrfce_protocol = "{JSON}";
        attribute :>> SD::serviceURL = "pull";
    }
    #servicedd port def APIS_DD :> APISService {
        doc
        /* Service design description with nested protocol-specific ports */
        #idd port APIS_HTTP {
            out cll: CallGiveItems;
            in retrn: ResultGiveItems;
        }
        #idd port APIS_MQTT {
            out pub: Publish;
            out retall: Return_AllItems;
            in subscr: Subscribe;
        }
    }
    attribute def Publish {
        ref nametopic: String;
    }
    attribute def Subscribe {
        ref nametopic: String;
    }
    attribute def Return_AllItems {
        ref itms: String;
    }
    attribute def Subscribe_giveItems {
        ref itms: String;
    }
    attribute def Return_Ack {
        ref ack: Boolean;
    }
    attribute def CallGiveItems {
        ref itms: String;
    }
    attribute def ResultGiveItems {
        ref ack: Boolean;
    }
    #clouddd AHFNorway_LocalCloudDD :> ArrowheadCore {
        #systemdd TellUConsumer {
            #servicedd serviceDiscovery: ~ServiceDiscoveryDD;
            #servicedd apisp: APIS_DD;
            attribute :>> SysD::systemname = "UngerApisClient";
            attribute :>> SysD::address = "Unger_network_ip";
            attribute :>> SysD::portno = 0;
            state TellUbehavior {
                entry send new CallGiveItems("All the items") via apisp.APIS_HTTP;
                then Wait;
                state Wait;
                accept rs: ResultGiveItems then Wait;
            }
        }
        #systemdd APISProducer {
            #servicedd serviceDiscovery: ~ServiceDiscoveryDD;
            #servicedd tellu: ~APIS_DD;
            #servicedd apisc: APIS_DD;
            :>> SysD::systemname = "PrediktorApisServer";
            :>> SysD::address = "Prediktor_network_ip";
            :>> SysD::portno = 6565;
            attribute x: Boolean;
            action giveItems :> SysDD::ServiceMethod {
                in itms: String;
                out ack: Boolean;
                /* Forward itms and return an ack */
                first start;
                then send new Return_AllItems() via apisc.APIS_MQTT;
                ref success = true;
                bind ack = success;
            }
            state APISPbehavior {
                entry send new Publish("Return_AllItems") via apisc.APIS_MQTT;
                then WaitOnData;
                state WaitOnData;
                accept cl: CallGiveItems via tellu.APIS_HTTP do action {
                    first start;
                    then action giveItems {
                        in itms = cl.itms;
                        out ack = x;
                    }
                    then send new ResultGiveItems(x) via tellu.APIS_HTTP;
                }
                then WaitOnData;
            }
        }
        #systemdd APISConsumer {
            #servicedd serviceDiscovery: ~ServiceDiscovery;
            #servicedd apisp: ~APIS_DD;
            :>> SysD::systemname = "TellUClient";
            :>> SysD::address = "Prediktor_network_ip";
            :>> SysD::portno = 1;
            state MQTT_APISP {
                entry send new Subscribe("Return_AllItems") via apisp.APIS_MQTT;
                then Idle;
                state Idle;
                accept Return_AllItems via apisp.APIS_MQTT then Idle;
            }
        }
        part MQTTServer {
            port getTopic: ~APIS_DD;
            port giveTopic: APIS_DD;
            state Serve {
                entry;
                then Publ;
                state Publ;
                accept pub: Publish via getTopic.APIS_MQTT then Subsr;
                state Subsr;
                accept Subscribe via giveTopic.APIS_MQTT then Idle;
                state Idle;
                accept retrnall: Return_AllItems via getTopic.APIS_MQTT do send retrnall via giveTopic.APIS_MQTT then Idle;
            }
        }
        connect APISProducer.apisc to MQTTServer.getTopic;
        connect MQTTServer.giveTopic to APISConsumer.apisp;
        connect TellUConsumer.apisp to APISProducer.tellu;
        connect APISProducer.serviceDiscovery to service_registry.serviceDiscovery;
        connect TellUConsumer.serviceDiscovery to service_registry.serviceDiscovery;
        connect APISConsumer.serviceDiscovery to service_registry.serviceDiscovery;
    }
}
