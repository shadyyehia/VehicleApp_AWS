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
                                    <option v-for="cust in CustomerList" v-bind:value="cust.id" v-bind:key="cust.id">
                                        {{cust.name}}
                                    </option>
                                </select>
                            </div>
                        </th>
                        <th>
                            <div class="form-group">
                                <select class="form-control" v-model="selectedStatus">
                                    <option :value="-1"></option>
                                    <option :value="1">Connected</option>
                                    <option :value="0">Disconnected</option>
                                </select>
                            </div>
                        </th>
                        </thead>

                        <tbody>
                            <tr v-for="(vehicle, index) in vehicleList" v-bind:key="index" >
                                <td> {{index + 1}}</td>
                                <td> {{ vehicle.vin }}</td>
                                <td> {{ vehicle.registrationNo}}</td>
                                <td> {{ vehicle.owner.name}}</td>
                                <td> {{ convertBoolToText(vehicle.isConnected) }}</td>
                            </tr>
                        </tbody>
                    </table>
                   
                </div>
            </div>
        </div>
    </div>



</template>

<script src="./vehicle.ts"></script>
<style src="./vehicle.css" />