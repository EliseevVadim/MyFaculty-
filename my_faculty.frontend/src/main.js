import Vue from 'vue'
import VueLoading from "vuejs-loading-plugin";
import Notifications from "vue-notification";
import VuetifyConfirm from "vuetify-confirm";
import App from './App.vue'
import './registerServiceWorker'
import router from './router'
import store from './store'
import vuetify from "@/plugins/vuetify";
import 'material-design-icons-iconfont/dist/material-design-icons.css';
import SecurityService from './services/authService';
import * as d3 from "d3";
import LoadingScreen from "@/components/LoadingScreen";
import BackgroundColorService from './services/backgroundColorService';
import { TiptapVuetifyPlugin } from 'tiptap-vuetify'
import 'tiptap-vuetify/dist/main.css';
import VueFileAgent from 'vue-file-agent';
import VueFileAgentStyles from 'vue-file-agent/dist/vue-file-agent.css';
import 'viewerjs/dist/viewer.css';
import VueViewer from "v-viewer";
import DateTimePicker from 'vuetify2-datetime-picker';
import NotificationsHub from './services/notificationsHub';

Vue.config.productionTip = false;
Vue.prototype.$oidc = SecurityService;
Vue.prototype.$d3 = d3;
Vue.prototype.$backgroundColorService = BackgroundColorService;
Vue.prototype.$notificationsConnection = NotificationsHub;

Vue.use(VueLoading, {
    customLoader: LoadingScreen,
    background: 'rgba(0, 0, 0, 0.5)'
});

Vue.use(Notifications);

Vue.use(VuetifyConfirm, {
    vuetify,
    buttonTrueText: "Да",
    buttonFalseText: "Нет",
    title: "Подтверждение действия",
    width: 350,
    color: "warning",
    icon: "warning",
    property: "$confirm"
});

Vue.use(TiptapVuetifyPlugin, {
    vuetify,
    iconsGroup: 'md'
});

Vue.use(VueFileAgent);

Vue.use(VueViewer);

Vue.use(DateTimePicker);

Vue.use(NotificationsHub);

new Vue({
    router,
    store,
    vuetify,
    render: h => h(App)
}).$mount('#app')
