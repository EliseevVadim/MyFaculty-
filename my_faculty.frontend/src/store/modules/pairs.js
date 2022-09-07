import axios from "axios";
import {config} from "@/config/config";

const state = {
	pairs: []
};

const getters = {
	PAIRS: state => {
		return state.pairs;
	}
};

const mutations = {
	setPairs(state, payload) {
		state.pairs = payload;
	}
};

const actions = {
	loadAllPairs: async(context) => {
		await axios.get(config.apiUrl + '/api/pairs')
			.then((response) => {
				context.commit('setPairs', response.data);
			})
	},
	addPair: (context, payload) => {
		return new Promise(async (resolve, reject) => {
			await axios.post(config.apiUrl + '/api/pairs', {
				'pairName': payload.pair_name,
				'teacherId': payload.teacher_id,
				'auditoriumId': payload.auditorium_id,
				'groupId': payload.group_id,
				'disciplineId': payload.discipline_id,
				'dayOfWeekId': payload.day_of_week_id,
				'pairRepeatingId': payload.repeating_id,
				'pairInfoId': payload.pair_info_id
			})
				.then((response) => {
					resolve(response);
				})
				.catch((error) => {
					reject(error);
				})
		})
	},
	loadPairById: (context, id) => {
		return new Promise(async (resolve, reject) => {
			await axios.get(config.apiUrl + '/api/pairs/' + id)
				.then((response) => {
					resolve(response);
				})
				.catch((error) => {
					reject(error);
				})
		});
	},
	updatePair: (context, payload) => {
		return new Promise(async (resolve, reject) => {
			await axios.put(config.apiUrl + '/api/pairs', {
				'id': payload.id,
				'pairName': payload.pair_name,
				'teacherId': payload.teacher_id,
				'auditoriumId': payload.auditorium_id,
				'groupId': payload.group_id,
				'disciplineId': payload.discipline_id,
				'dayOfWeekId': payload.day_of_week_id,
				'pairRepeatingId': payload.repeating_id,
				'pairInfoId': payload.pair_info_id
			})
				.then((response) => {
					resolve(response);
				})
				.catch((error) => {
					reject(error);
				})
		})
	},
	deletePair: (context, id) => {
		return new Promise(async (resolve, reject) => {
			await axios.delete(config.apiUrl + '/api/pairs/' + id)
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