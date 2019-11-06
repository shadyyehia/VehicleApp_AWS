import { HubConnectionBuilder, LogLevel } from "@aspnet/signalr";
import Vue from "vue";
declare module "vue/types/vue" {
  // tslint:disable-next-line:interface-name
  interface Vue {
    $vehicleHub: any;
  }
}
declare var Promise: any;
export default {
  install() {
    // tslint:disable-next-line:triple-equals
    if (process!.env.VUE_APP_SignalR_ENABLED === "true") {
      const connection = new HubConnectionBuilder()
        .withUrl(
          process.env.VUE_APP_WEBAPI! + process.env.VUE_APP_HUB_PATH_STRING
        )
        .configureLogging(LogLevel.Information)
        .build();
      // use new Vue instance as an event bus
      const vehicleHub = new Vue();
      // every component will use this.$vehicleHub to access the event bus
      Vue.prototype.$vehicleHub = vehicleHub;
      // forward server side SignalR events through $vehicleHub, where components will listen to them
      connection.on("VehicleStatusChange", data => {
        vehicleHub.$emit("status-changed", { data });
      });
      let startedPromise = null;
      var start = function startFn() {
        startedPromise = connection.start().catch(err => {
          console.error("Failed to connect with hub", err);
          return new Promise((resolve: any, reject: any) => {
            return setTimeout(
              () =>
                start()
                  .then(resolve)
                  .catch(reject),
              5000
            );
          });
        });
        return startedPromise;
      };
      connection.onclose(() => start());

      start();
    }
  }
};
