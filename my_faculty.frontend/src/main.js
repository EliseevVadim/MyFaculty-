import Vue from 'vue'
import App from './App.vue'
import axios from "axios";
import './registerServiceWorker'
import router from './router'
import store from './store'
import vuetify from "@/plugins/vuetify";
import 'material-design-icons-iconfont/dist/material-design-icons.css';
import SecurityService from './services/authService';
import * as d3 from "d3";

Vue.config.productionTip = false;
Vue.prototype.$oidc = SecurityService;
Vue.prototype.$d3 = d3;

new Vue({
  router,
  store,
  vuetify,
  render: h => h(App)
}).$mount('#app')
