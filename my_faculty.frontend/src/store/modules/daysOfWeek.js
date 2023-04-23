import axios from "axios";
import {config} from "@/config/config";

const state = {
    daysOfWeek: []
};

const getters = {
    DAYS_OF_WEEK: state => {
        return state.daysOfWeek;
    }
};

const mutations = {
    setDaysOfWeek(state, payload) {
        state.daysOfWeek = payload;
    }
};

const actions = {
    loadAllDaysOfWeek: async (context) => {
        await axios.get(config.apiUrl + '/api/daysOfWeek')
            .then((response) => {
                context.commit('setDaysOfWeek', response.data);
            })
    }
};

export default {
    state,
    getters,
    mutations,
    actions,
};