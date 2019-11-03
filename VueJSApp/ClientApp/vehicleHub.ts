import { HubConnectionBuilder, LogLevel } from '@aspnet/signalr'
import Vue from 'vue';
declare module 'vue/types/vue' {
    interface Vue {
        $vehicleHub: any;
    }
}
declare var Promise: any;
export default {
    
    install() {
        let setting : any =require('./params.json');
        
        const connection = new HubConnectionBuilder()
            .withUrl(setting.apiURL + setting.hubPathString)
            .configureLogging(LogLevel.Information)
            .build()
        // use new Vue instance as an event bus
        const vehicleHub = new Vue()
        // every component will use this.$vehicleHub to access the event bus
        Vue.prototype.$vehicleHub = vehicleHub
        // Forward server side SignalR events through $vehicleHub, where components will listen to them
        connection.on('VehicleStatusChange', (data) => {
            vehicleHub.$emit('status-changed', { data})
        })
        let startedPromise = null
        function start() {
            startedPromise = connection.start().catch(err => {
                console.error('Failed to connect with hub', err)
                return new Promise((resolve, reject) =>
                    setTimeout(() => start().then(resolve).catch(reject), 5000))
            })
            return startedPromise
        }
        connection.onclose(() => start())

        start()
    }
}