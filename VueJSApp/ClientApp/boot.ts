import './css/site.css';
import 'bootstrap';
import vehicle from './vehicleHub'
import Vue from 'vue';

import VueRouter from 'vue-router';
Vue.use(VueRouter);

const routes = [
    { path: '/', component: require('./components/vehicle/vehicle.vue.html') }
];


Vue.use(vehicle);
new Vue({
    el: '#app-root',
    router: new VueRouter({ mode: 'history', routes: routes }),
    render: h => h(require('./components/app/app.vue.html'))
});
