import Vue from "vue";
import { Component } from "vue-property-decorator";
import axios from 'axios';
import * as $ from 'jquery';


@Component
export default class VehicleComponent extends Vue {
    settings:any=  require('../../params.json');
    
   
    // $ = JQuery;
    vehicle: vehicle = <vehicle>{
        id: 0,
        name: "s",
        customerid: 34,
        registrationno: 1,
        vin: "sdf",
        isConnected: false
    };

    vehicleList: vehicle[] = [];
   
    created() {
        // Listen to score changes coming from SignalR events
        this.$vehicleHub.$on('status-changed', this.onStatusChanged)
    }
    beforeDestroy() {
        // Make sure to cleanup SignalR event handlers when removing the component
        this.$vehicleHub.$off('status-changed', this.onStatusChanged)
    }
    onStatusChanged({ data }) {
        this.vehicleList = data;
        //if (this.question.id !== questionId) return
        //Object.assign(this.question, { score })
    }
    mounted() {
        this.getvehicleList();
    }
  
    getvehicleList() {
        axios({
            method: 'get',
            url: this.settings.apiURL +'/api/vehicle/getvehiclesList'
        }).then((response: any) => {
            //console.log(response.data);
            //this.vehicleList = response.data;
        })
            .catch((error: any) => {
                console.log(error);
            });
    }

}

interface vehicle {
    id: number;
    name: string;
    vin: string;
    registrationno: number;
    customerid: number;
    isConnected: boolean;
}