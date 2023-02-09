import Vue from "vue";
import Vuetify from "vuetify";
import 'vuetify/dist/vuetify.min.css';
import '@mdi/font/css/materialdesignicons.css';
import ru from 'vuetify/lib/locale/ru.js'

Vue.use(Vuetify);

const options = {
	lang: {
		locales: {
			ru
		},
		current: 'ru'
	}
};

export default new Vuetify(options);