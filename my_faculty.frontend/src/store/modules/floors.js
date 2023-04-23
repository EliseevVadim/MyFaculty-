import axios from "axios";
import {config} from "@/config/config";

const state = {
    floors: []
};

const getters = {
    FLOORS: state => {
        return state.floors;
    }
};

const mutations = {
    setFloors(state, payload) {
        state.floors = payload;
    }
};

const actions = {
    loadAllFloors: async (context) => {
        await axios.get(config.apiUrl + '/api/floors')
            .then((response) => {
                context.commit('setFloors', response.data);
            })
    },
    addFloor: (context, payload) => {
        return new Promise(async (resolve, reject) => {
            await axios.post(config.apiUrl + '/api/floors', {
                'name': payload.name,
                'bounds': payload.bounds,
                'facultyId': payload.faculty_id
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
    loadFloorById: (context, id) => {
        return new Promise(async (resolve, reject) => {
            await axios.get(config.apiUrl + '/api/floors/' + id)
                .then((response) => {
                    resolve(response);
                })
                .catch((error) => {
                    reject(error);
                })
        });
    },
    loadFloorsByFacultyId: async (context, id) => {
        await axios.get(config.apiUrl + '/api/floors/faculty/' + id)
            .then((response) => {
                context.commit('setFloors', response.data);
            })
    },
    updateFloor: (context, payload) => {
        return new Promise(async (resolve, reject) => {
            await axios.put(config.apiUrl + '/api/floors', {
                'id': payload.id,
                'name': payload.name,
                'bounds': payload.bounds,
                'facultyId': payload.faculty_id
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
    deleteFloor: (context, id) => {
        return new Promise(async (resolve, reject) => {
            await axios.delete(config.apiUrl + '/api/floors/' + id, {
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