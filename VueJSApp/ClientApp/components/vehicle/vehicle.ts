import Vue from "vue";
import { Component, Prop, Watch } from "vue-property-decorator";
import axios from 'axios';
import * as $ from 'jquery';



@Component
export default class VehicleComponent extends Vue {
    settings:any=  require('../../params.json');
    // $ = JQuery;
   

    vehicleList: vehicle[] = [];
    //@Prop() selectedCustomer: string;
    //@Prop() selectedStatus: string;
    //@Watch('selectedStatus') onselectedStatus(new_s, old_s) {

    //    this.vehicleList  = this.vehicleList.filter(x => x.isConnected == new_s);
    //}
    //@Watch('selectedCustomer') onselectedCustomer(new_s, old_s) {

    //    this.vehicleList = this.vehicleList.filter(x => x.owner.name == new_s);
    //}
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
    owner: { id: number, name: string, address: string };
    isConnected: boolean;
}