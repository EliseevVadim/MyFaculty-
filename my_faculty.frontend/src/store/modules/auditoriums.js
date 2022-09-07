import axios from "axios";
import {config} from "@/config/config";

const state = {
	auditoriums : []
};

const getters = {
	AUDITORIUMS: state => {
		return state.auditoriums;
	}
};

const mutations = {
	setAuditoriums(state, payload) {
		state.auditoriums = payload;
	}
};

const actions = {
	loadAllAuditoriums: async (context) => {
		await axios.get(config.apiUrl + '/api/auditoriums')
			.then((response) => {
				context.commit('setAuditoriums', response.data);
			})
	},
	addAuditorium: (context, payload) => {
		return new Promise(async (resolve, reject) => {
			await axios.post(config.apiUrl + '/api/auditoriums', {
				'auditoriumName': payload.auditorium_name,
				'positionInfo': JSON.stringify(payload.position_info),
				'floorId': payload.floor_id,
				'teacherId': payload.holder_id
			})
				.then((response) => {
					resolve(response);
				})
				.catch((error) => {
					reject(error);
				})
		})
	},
	loadAuditoriumById: (context, id) => {
		return new Promise(async (resolve, reject) => {
			await axios.get(config.apiUrl + '/api/auditoriums/' + id)
				.then((response) => {
					resolve(response);
				})
				.catch((error) => {
					reject(error);
				})
		});
	},
	updateAuditorium: (context, payload) => {
		return new Promise(async (resolve, reject) => {
			await axios.put(config.apiUrl + '/api/auditoriums', {
				'id': payload.id,
				'auditoriumName': payload.auditorium_name,
				'positionInfo': JSON.stringify(payload.position_info),
				'floorId': payload.floor_id,
				'teacherId': payload.holder_id
			})
				.then((response) => {
					resolve(response);
				})
				.catch((error) => {
					reject(error);
				})
		})
	},
	deleteAuditorium: (context, id) => {
		return new Promise(async (resolve, reject) => {
			await axios.delete(config.apiUrl + '/api/auditoriums/' + id)
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