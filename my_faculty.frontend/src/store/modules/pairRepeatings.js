import axios from "axios";
import {config} from "@/config/config";

const state = {
	pairRepeatings : []
};

const getters = {
	PAIR_REPEATINGS: state => {
		return state.pairRepeatings;
	}
};

const mutations = {
	setPairRepeatings(state, payload) {
		state.pairRepeatings = payload;
	}
};

const actions = {
	loadAllPairRepeatings: async (context) => {
		await axios.get(config.apiUrl + '/api/pairRepeatings')
			.then((response) => {
				context.commit('setPairRepeatings', response.data);
			})
	}
};

export default {
	state,
	getters,
	mutations,
	actions,
};