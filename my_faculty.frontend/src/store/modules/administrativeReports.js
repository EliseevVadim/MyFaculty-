import axios from "axios";
import {config} from "@/config/config";

const state = {
    usersBansReports: []
};

const getters = {
    USERS_BANS_REPORTS: state => {
        return state.usersBansReports;
    }
};

const mutations = {
    setUsersBansReports(state, payload) {
        state.usersBansReports = payload;
    }
};

const actions = {
    loadAllUsersBansReports: async (context) => {
        await axios.get(config.apiUrl + '/api/administrativereports/usersbans', {
            headers: config.headers
        })
            .then((response) => {
                context.commit('setUsersBansReports', response.data);
            })
    }
};

export default {
    state,
    getters,
    mutations,
    actions,
};