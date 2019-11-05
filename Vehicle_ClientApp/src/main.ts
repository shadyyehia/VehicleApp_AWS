import Vue from "vue";
import App from "./App.vue";
import vehicleHub from "./vehicleHub";
import BootstrapVue from "bootstrap-vue";
import "bootstrap/dist/css/bootstrap.css";
import "bootstrap-vue/dist/bootstrap-vue.css";

Vue.use(BootstrapVue);
Vue.config.productionTip = false;
Vue.use(vehicleHub);
new Vue({
  render: h => h(App)
}).$mount("#app");
