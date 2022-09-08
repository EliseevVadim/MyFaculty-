import axios from "axios";
import {config} from "@/config/config";

const state = {
	object_types: []
};

const getters = {
	OBJECT_TYPES: state => {
		return state.object_types;
	}
};

const mutations = {
	setObjectTypes(state, payload) {
		state.object_types = payload;
	}
};

const actions = {
	loadAllObjectTypes: async(context) => {
		await axios.get(config.apiUrl + '/api/secondaryObjectTypes')
			.then((response) => {
				context.commit('setObjectTypes', response.data);
			})
	},
	addObjectType: (context, payload) => {
		return new Promise(async (resolve, reject) => {
			await axios.post(config.apiUrl + '/api/secondaryObjectTypes', {
				'objectTypeName': payload.object_type_name,
				'typePath': payload.type_path
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
	loadObjectTypeById: (context, id) => {
		return new Promise(async (resolve, reject) => {
			await axios.get(config.apiUrl + '/api/secondaryObjectTypes/' + id)
				.then((response) => {
					resolve(response);
				})
				.catch((error) => {
					reject(error);
				})
		});
	},
	updateObjectType: (context, payload) => {
		return new Promise(async (resolve, reject) => {
			await axios.put(config.apiUrl + '/api/secondaryObjectTypes', {
				'id': payload.id,
				'objectTypeName': payload.object_type_name,
				'typePath': payload.type_path
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
	deleteObjectType: (context, id) => {
		return new Promise(async (resolve, reject) => {
			await axios.delete(config.apiUrl + '/api/secondaryObjectTypes/' + id, {
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