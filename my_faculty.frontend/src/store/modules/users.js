import axios from "axios";
import {config} from "@/config/config";

const state = {
	users : [],
	selectedUser : null,
	currentUser: {}
};

const getters = {
	USERS: state => {
		return state.users;
	},
	SELECTED_USER: state => {
		return state.selectedTeacher;
	},
	CURRENT_USER: state => {
		return state.currentUser;
	}
};

const mutations = {
	setUsers(state, payload) {
		state.users = payload;
	},
	selectUser(state, payload) {
		state.selectedUser = payload;
	},
	setCurrentUser(state, payload) {
		state.currentUser = payload;
	}
};

const actions = {
	loadAllUsers: async (context) => {
		await axios.get(config.apiUrl + '/api/users')
			.then((response) => {
				context.commit('setUsers', response.data);
			})
	},
	loadUserById: (context, id) => {
		return new Promise(async (resolve, reject) => {
			await axios.get(config.apiUrl + '/api/users/' + id)
				.then((response) => {
					resolve(response);
				})
				.catch((error) => {
					reject(error);
				})
		});
	},
	loadCurrentUser: async (context, id) => {
		await axios.get(config.apiUrl + '/api/users/' + id)
			.then((response) => {
				context.commit('setCurrentUser', response.data);
			})
	},
	updateUser: (context, payload) => {
		return new Promise(async (resolve, reject) => {
			let formData = new FormData();
			formData.append('id', payload.id);
			formData.append('firstName', payload.firstName);
			formData.append('lastName', payload.lastName);
			formData.append('email', payload.email);
			if (payload.cityId !== null)
				formData.append('cityId', payload.cityId);
			formData.append('photo', payload.photo);
			formData.append('website', payload.website ?? '');
			formData.append('vkLink', payload.vkLink ?? '');
			formData.append('telegramLink', payload.telegramLink ?? '');
			formData.append('birthDate', payload.birthDate ?? '');
			await axios.put(config.apiUrl + '/api/users', formData, {
				headers: config.headers
			})
				.then((response) => {
					resolve(response);
				})
				.catch((error) => {
					reject(error);
				})
		})
	}
};

export default {
	state,
	getters,
	mutations,
	actions,
};