import axios from "axios";
import {config} from "@/config/config";

const state = {
	notifications : []
};

const getters = {
	NOTIFICATIONS: state => {
		return state.notifications;
	}
};

const mutations = {
	setNotifications(state, payload) {
		state.notifications = payload;
	}
};

const actions = {
	loadAllNotifications: async (context) => {
		await axios.get(config.apiUrl + '/api/notifications', {
			headers: config.headers
		})
			.then((response) => {
				context.commit('setNotifications', response.data);
			})
	},
	deleteNotifications: (context, id) => {
		return new Promise(async (resolve, reject) => {
			await axios.delete(config.apiUrl + '/api/notifications', {
				headers: config.headers
			})
				.then((response) => {
					resolve(response);
				})
				.catch((error) => {
					reject(error);
				})
		});
	}
};

export default {
	state,
	getters,
	mutations,
	actions,
};