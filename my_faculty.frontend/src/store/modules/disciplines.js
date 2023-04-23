import axios from "axios";
import {config} from "@/config/config";

const state = {
    disciplines: []
};

const getters = {
    DISCIPLINES: state => {
        return state.disciplines;
    }
};

const mutations = {
    setDisciplines(state, payload) {
        state.disciplines = payload;
    }
};

const actions = {
    loadAllDisciplines: async (context) => {
        await axios.get(config.apiUrl + '/api/disciplines')
            .then((response) => {
                context.commit('setDisciplines', response.data);
            })
    },
    addDiscipline: (context, payload) => {
        return new Promise(async (resolve, reject) => {
            await axios.post(config.apiUrl + '/api/disciplines', {
                'disciplineName': payload.discipline_name
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
    loadDisciplineById: (context, id) => {
        return new Promise(async (resolve, reject) => {
            await axios.get(config.apiUrl + '/api/disciplines/' + id)
                .then((response) => {
                    resolve(response);
                })
                .catch((error) => {
                    reject(error);
                })
        });
    },
    updateDiscipline: (context, payload) => {
        return new Promise(async (resolve, reject) => {
            await axios.put(config.apiUrl + '/api/disciplines', {
                'id': payload.id,
                'disciplineName': payload.discipline_name
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
    deleteDiscipline: (context, id) => {
        return new Promise(async (resolve, reject) => {
            await axios.delete(config.apiUrl + '/api/disciplines/' + id, {
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