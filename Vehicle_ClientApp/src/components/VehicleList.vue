<template>
  <div class="container">
    <div class="row">
      <div class="col-md-12">
        <h4>Vehicle List</h4>
        <div class="table-responsive">
          <table id="vehicleTable"  class="table table-bordred table-striped">
            <thead>
              <th>No.</th>
              <th>VIN</th>
              <th>Registration No.</th>
              <th>Customer</th>
              <th>Status</th>
            </thead>
            <thead>
              <th></th>
              <th></th>
              <th></th>
              <th style="padding-right: 10px;">
                <div class="form-group">
                  <select class="form-control" v-model="selectedCustomerId">
                    <option v-bind:value="-1"></option>
                    <option
                      v-for="cust in customerlist"
                      v-bind:value="cust.id"
                      v-bind:key="cust.id">{{ cust.name }}</option>
                  </select>
                </div>
              </th>
              <th>
                <div class="form-group">
                  <select class="form-control" v-model="selectedStatus">
                    <option value=""></option>
                    <option :value="true">Connected</option>
                    <option :value="false">Disconnected</option>
                  </select>
                </div>
              </th>
            </thead>

            <tbody>
              <tr  v-for="(vehicle, index) in filteredList" v-bind:key="index">
                <td>{{ index + 1 }}</td>
                <td>{{ vehicle.vin }}</td>
                <td>{{ vehicle.registrationNo }}</td>
                <td>{{ vehicle.owner.name }}</td>
                <td>{{ convertBoolToText(vehicle.isConnected) }}</td>
              </tr>
            </tbody>
          </table>
          <div v-if="!loaded">
             The list gets updated every 1 minute...
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import Vue from "vue";
import { Component, Prop, Watch } from "vue-property-decorator";
import axios from "axios";
import { isBoolean } from "util";
declare var process: {
  env: {
    NODE_ENV: string;
    VUE_APP_WEBAPI: string;
    VUE_APP_SignalR_ENABLED: string;
  };
};
@Component
export default class VehicleListComponent extends Vue {
  // $ = JQuery;

  vehicleList: IVehicle[] = [];
  customerlist: ICustomer[] = [];
  selectedCustomerId: number | null = -1;
    selectedStatus: boolean | string = "";
  loaded = false;
  get filteredList() {
    return this.vehicleList.filter(item => {
      let filtered = true;
      if (this.selectedCustomerId! > -1) {
        filtered = item.owner.id == this.selectedCustomerId;
      }

      if (filtered && this.selectedStatus !== "") {
        filtered = item.isConnected == this.selectedStatus;
      }

      return filtered;
    });
  }
  created() {
    // listen to score changes coming from SignalR events
    if (this.$vehicleHub)
      this.$vehicleHub.$on("status-changed", this.onStatusChanged);
  }
  beforeDestroy() {
    // make sure to cleanup SignalR event handlers when removing the component
    if (this.$vehicleHub)
      this.$vehicleHub.$off("status-changed", this.onStatusChanged);
  }
  // tslint:disable-next-line:typedef
  onStatusChanged({ data }: { data: any }) {
    this.vehicleList = data;
      console.log("notification from SignalR");
      this.loaded = true;
    // if (this.question.id !== questionId) return
    // object.assign(this.question, { score })
  }
  mounted() {
    this.fillcustomerlist();
    this.startMonitor();
  }
  // tslint:disable-next-line:typedef
  convertBoolToText(val: boolean) {
    if (val === true) {
      return "Connected";
    } else if (val === false) {
      return "Disconnected";
    }
  }
  fillcustomerlist() {
    var promise1 = axios.get(
      process.env.VUE_APP_WEBAPI + "/api/vehicle/getCustomers"
    );

    promise1
      .then((response: any) => {
        this.customerlist = response.data;
        return response.data;

        //console.log(response.data);
      })
      .catch((error: any) => {
        //console.log(error);
      });
    return promise1;
  }
  startMonitor() {
    if (process.env.VUE_APP_SignalR_ENABLED == "true") {
      var promise = axios.get(
        process.env.VUE_APP_WEBAPI + "/api/vehicle/monitor"
      );
      promise
        .then((response: any) => {
          console.log(response.data);
        })
        .catch((error: any) => {
          console.log(error);
        });
      return promise;
    } else {
      this.montor_noSignalR();
      setInterval(() => {
        this.montor_noSignalR();
      }, 10000);
    }
  }
  montor_noSignalR() {
    var promise = axios.get(
      process.env.VUE_APP_WEBAPI + "/api/vehicle/monitor_noSignalR"
    );
    promise
      .then((response: any) => {
        this.vehicleList = response.data;
        console.log("Ajax used, SignalR is not enabled in configurations");
      })
      .catch((error: any) => {
        console.log(error);
      });
    return promise;
  }
}

// tslint:disable-next-line:class-name
interface IVehicle {
  id: number;
  name: string;
  vin: string;
  registrationno: number;
  owner: { id: number; name: string; address: string };
  isConnected: boolean;
}
interface ICustomer {
  id: number;
  name: string;
  address: string;
}
</script>

<style>
/* Space out content a bit */
body {
  padding-top: 20px;
  padding-bottom: 20px;
}

/* Everything but the jumbotron gets side spacing for mobile first views */
.header,
.marketing,
.footer {
  padding-right: 15px;
  padding-left: 15px;
}

/* Custom page header */
.header {
  border-bottom: 1px solid #e5e5e5;
}
/* Make the masthead heading the same height as the navigation */
.header h3 {
  padding-bottom: 19px;
  margin-top: 0;
  margin-bottom: 0;
  line-height: 40px;
}

/* Custom page footer */
.footer {
  padding-top: 19px;
  color: #777;
  border-top: 1px solid #e5e5e5;
}

/* Customize container */
@media (min-width: 586px) {
  .container {
    max-width: 950px;
  }
}

.container-narrow > hr {
  margin: 30px 0;
}

/* Main marketing message and sign up button */
.jumbotron {
  text-align: center;
  border-bottom: 1px solid #e5e5e5;
}

.jumbotron .btn {
  padding: 14px 24px;
  font-size: 21px;
}

/* Supporting marketing content */
.marketing {
  margin: 40px 0;
}

.marketing p + h4 {
  margin-top: 28px;
}

/* Responsive: Portrait tablets and up */
@media screen and (min-width: 768px) {
  /* Remove the padding we set earlier */
  .header,
  .marketing,
  .footer {
    padding-right: 0;
    padding-left: 0;
  }
  /* Space out the masthead */
  .header {
    margin-bottom: 30px;
  }
  /* Remove the bottom border on the jumbotron for visual effect */
  .jumbotron {
    border-bottom: 0;
  }
}
</style>
