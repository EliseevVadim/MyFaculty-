import axios from "axios";
import {config} from "@/config/config";

const state = {
	pairInfos : []
};

const getters = {
	PAIR_INFOS: state => {
		return state.pairInfos;
	}
};

const mutations = {
	setPairInfos(state, payload) {
		state.pairInfos = payload;
	}
};

const actions = {
	loadAllPairInfos: async (context) => {
		await axios.get(config.apiUrl + '/api/pairInfos')
			.then((response) => {
				context.commit('setPairInfos', response.data);
			})
	},
	addPairInfo: (context, payload) => {
		return new Promise(async (resolve, reject) => {
			await axios.post(config.apiUrl + '/api/pairInfos', {
				'pairNumber': payload.pair_number,
				'startTime': payload.start_time,
				'endTime': payload.end_time
			})
				.then((response) => {
					resolve(response);
				})
				.catch((error) => {
					reject(error);
				})
		})
	},
	loadPairInfoById: (context, id) => {
		return new Promise(async (resolve, reject) => {
			await axios.get(config.apiUrl + '/api/pairInfos/' + id)
				.then((response) => {
					resolve(response);
				})
				.catch((error) => {
					reject(error);
				})
		});
	},
	updatePairInfo: (context, payload) => {
		return new Promise(async (resolve, reject) => {
			await axios.put(config.apiUrl + '/api/pairInfos', {
				'id': payload.id,
				'pairNumber': payload.pair_number,
				'startTime': payload.start_time,
				'endTime': payload.end_time
			})
				.then((response) => {
					resolve(response);
				})
				.catch((error) => {
					reject(error);
				})
		})
	},
	deletePairInfo: (context, id) => {
		return new Promise(async (resolve, reject) => {
			await axios.delete(config.apiUrl + '/api/pairInfos/' + id)
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