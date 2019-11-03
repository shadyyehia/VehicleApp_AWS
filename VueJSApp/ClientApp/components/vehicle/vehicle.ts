import Vue from "vue";
import { Component, Prop, Watch } from "vue-property-decorator";
import axios from 'axios';
import * as $ from 'jquery';



@Component
export default class VehicleComponent extends Vue {
    settings:any=  require('../../params.json');
    // $ = JQuery;
   

    vehicleList: vehicle[] = [];
    CustomerList: customer[] = [];
    @Prop() selectedCustomerId: number;
    @Prop() selectedStatus: any;
    @Watch('selectedStatus') onselectedStatus(new_s, old_s) {

        this.filterData();
        
    }
    @Watch('selectedCustomerId') onselectedCustomer(new_s, old_s) {
        this.filterData();
       
    }
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
        console.log("notification from SignalR");
        //if (this.question.id !== questionId) return
        //Object.assign(this.question, { score })
    }
    mounted() {  
        this.fillCustomerList();
        this.startMonitor();
    }
    convertBoolToText(val) {
        if (val == true)
            return "Connected"

        else if (val == false)
            return "Disconnected"
    }
    fillCustomerList() {
        axios({
            method: 'GET',
            url: this.settings.apiURL + '/api/vehicle/getCustomers'
        }).then((response: any) => {
            this.CustomerList = response.data;
            

            console.log(response.data);
        })
            .catch((error: any) => {
                console.log(error);
            });
    }
    filterData() {
        var status = null
        if (this.selectedStatus == 1)
            status = true;
        else if (this.selectedStatus == 0)
            status = false;


        axios({
            method: 'POST',
            url: this.settings.apiURL + '/api/vehicle/filterData',
            data: {
                customerId: this.selectedCustomerId,
                status: status
            }
        }).then((response: any) => {
            this.vehicleList = response.data;
            console.log("data filtered");
        })
            .catch((error: any) => {
                console.log(error);
            });
    }
    startMonitor() {
        axios({
            method: 'GET',
            url: this.settings.apiURL + '/api/vehicle/monitor' 
        }).then((response: any) => {
            console.log(response.data);
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
interface customer{
    id: number;
    name: string;
    address:string
}