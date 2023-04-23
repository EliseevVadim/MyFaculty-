import axios from "axios";
import {config} from "@/config/config";

const state = {
    states: [],
    stateTransitions: []
};

const getters = {
    STATES: state => {
        return state.states;
    },
    STATE_TRANSITIONS: state => {
        return state.stateTransitions;
    }
};

const mutations = {
    setStates(state, payload) {
        state.states = payload;
    },
    setStateTransitions(state, payload) {
        state.stateTransitions = payload;
    }
};

const actions = {
    loadAllStates: async (context) => {
        await axios.get(config.apiUrl + '/api/ExpertSystem/states')
            .then((response) => {
                context.commit('setStates', response.data);
            })
    },
    loadAllStateTransitions: async (context) => {
        await axios.get(config.apiUrl + '/api/ExpertSystem/state-transitions')
            .then((response) => {
                context.commit('setStateTransitions', response.data);
            })
    }
};

export default {
    state,
    getters,
    mutations,
    actions,
};