import axios from "axios";
import {config} from "@/config/config";

const state = {
	regions : []
};

const getters = {
	REGIONS: state => {
		return state.regions;
	}
};

const mutations = {
	setRegions(state, payload) {
		state.regions = payload;
	}
};

const actions = {
	loadAllRegions: async (context) => {
		await axios.get(config.apiUrl + '/api/regions')
			.then((response) => {
				context.commit('setRegions', response.data);
			})
	},
	addRegion: (context, payload) => {
		return new Promise(async (resolve, reject) => {
			await axios.post(config.apiUrl + '/api/regions', {
				'regionName': payload.region_name,
				'countryId': payload.country_id
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
	loadRegionById: (context, id) => {
		return new Promise(async (resolve, reject) => {
			await axios.get(config.apiUrl + '/api/regions/' + id)
				.then((response) => {
					resolve(response);
				})
				.catch((error) => {
					reject(error);
				})
		});
	},
	loadRegionsByCountryId: async (context, id) => {
		await axios.get(config.apiUrl + '/api/regions/country/' + id)
			.then((response) => {
				context.commit('setRegions', response.data);
			})
	},
	updateRegion: (context, payload) => {
		return new Promise(async (resolve, reject) => {
			await axios.put(config.apiUrl + '/api/regions', {
				'id': payload.id,
				'regionName': payload.region_name,
				'countryId': payload.country_id
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
	deleteRegion: (context, id) => {
		return new Promise(async (resolve, reject) => {
			await axios.delete(config.apiUrl + '/api/regions/' + id, {
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