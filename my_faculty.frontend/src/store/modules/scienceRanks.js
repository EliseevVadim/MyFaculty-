import axios from "axios";
import {config} from "@/config/config";

const state = {
	ranks: []
};

const getters = {
	RANKS: state => {
		return state.ranks;
	}
};

const mutations = {
	setRanks(state, payload) {
		state.ranks = payload;
	}
};

const actions = {
	loadAllRanks: async(context) => {
		await axios.get(config.apiUrl + '/api/scienceRanks')
			.then((response) => {
				context.commit('setRanks', response.data);
			})
	},
	addScienceRank: (context, payload) => {
		return new Promise(async (resolve, reject) => {
			await axios.post(config.apiUrl + '/api/scienceRanks', {
				'rankName' : payload.rank_name
			}, {
				headers: config.headers
			})
				.then((response) => {
					resolve(response);
				})
				.catch((error) => {
					reject(error);
				})
		})
	},
	loadScienceRankById: (context, id) => {
		return new Promise(async (resolve, reject) => {
			await axios.get(config.apiUrl + '/api/scienceRanks/' + id)
				.then((response) => {
					resolve(response);
				})
				.catch((error) => {
					reject(error);
				})
		});
	},
	updateScienceRank: (context, payload) => {
		return new Promise(async (resolve, reject) => {
			await axios.put(config.apiUrl + '/api/scienceRanks/', {
				'id': payload.id,
				'rankName': payload.rank_name
			}, {
				headers: config.headers
			})
				.then((response) => {
					resolve(response);
				})
				.catch((error) => {
					reject(error);
				})
		})
	},
	deleteScienceRank: (context, id) => {
		return new Promise(async (resolve, reject) => {
			await axios.delete(config.apiUrl + '/api/scienceRanks/' + id, {
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