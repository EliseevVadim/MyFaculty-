import axios from "axios";
import {config} from "@/config/config";

const state = {
    usersBansReports: [],
    publicsBansReports: []
};

const getters = {
    USERS_BANS_REPORTS: state => {
        return state.usersBansReports;
    },
    PUBLICS_BANS_REPORTS: state => {
        return state.publicsBansReports;
    }
};

const mutations = {
    setUsersBansReports(state, payload) {
        state.usersBansReports = payload;
    },
    setPublicsBansReports(state, payload) {
        state.publicsBansReports = payload;
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
    },
    loadAllPublicsBansReports: async (context) => {
        await axios.get(config.apiUrl + '/api/administrativereports/publicsbans', {
            headers: config.headers
        })
            .then((response) => {
                context.commit('setPublicsBansReports', response.data);
            })
    }
};

export default {
    state,
    getters,
    mutations,
    actions,
};