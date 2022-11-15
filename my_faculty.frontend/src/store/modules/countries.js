import axios from "axios";
import {config} from "@/config/config";

const state = {
	countries: []
};

const getters = {
	COUNTRIES: state => {
		return state.countries;
	}
};

const mutations = {
	setCountries(state, payload) {
		state.countries = payload;
	}
};

const actions = {
	loadAllCountries: async(context) => {
		await axios.get(config.apiUrl + '/api/countries')
			.then((response) => {
				context.commit('setCountries', response.data);
			})
	},
	addCountry: (context, payload) => {
		return new Promise(async (resolve, reject) => {
			await axios.post(config.apiUrl + '/api/countries', {
				'countryName' : payload.country_name
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
	loadCountryById: (context, id) => {
		return new Promise(async (resolve, reject) => {
			await axios.get(config.apiUrl + '/api/countries/' + id)
				.then((response) => {
					resolve(response);
				})
				.catch((error) => {
					reject(error);
				})
		});
	},
	updateCountry: (context, payload) => {
		return new Promise(async (resolve, reject) => {
			await axios.put(config.apiUrl + '/api/countries/', {
				'id': payload.id,
				'countryName' : payload.country_name
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
	deleteCountry: (context, id) => {
		return new Promise(async (resolve, reject) => {
			await axios.delete(config.apiUrl + '/api/countries/' + id, {
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