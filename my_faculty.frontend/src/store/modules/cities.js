import axios from "axios";
import {config} from "@/config/config";

const state = {
    cities: []
};

const getters = {
    CITIES: state => {
        return state.cities;
    }
};

const mutations = {
    setCities(state, payload) {
        state.cities = payload;
    }
};

const actions = {
    loadAllCities: async (context) => {
        await axios.get(config.apiUrl + '/api/cities')
            .then((response) => {
                context.commit('setCities', response.data);
            })
    },
    addCity: (context, payload) => {
        return new Promise(async (resolve, reject) => {
            await axios.post(config.apiUrl + '/api/cities', {
                'cityName': payload.city_name,
                'regionId': payload.region_id
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
    loadCityById: (context, id) => {
        return new Promise(async (resolve, reject) => {
            await axios.get(config.apiUrl + '/api/cities/' + id)
                .then((response) => {
                    resolve(response);
                })
                .catch((error) => {
                    reject(error);
                })
        });
    },
    loadCitiesByRegionId: async (context, id) => {
        await axios.get(config.apiUrl + '/api/cities/region/' + id)
            .then((response) => {
                context.commit('setCities', response.data);
            })
    },
    updateCity: (context, payload) => {
        return new Promise(async (resolve, reject) => {
            await axios.put(config.apiUrl + '/api/cities', {
                'id': payload.id,
                'cityName': payload.city_name,
                'regionId': payload.region_id
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
    deleteCity: (context, id) => {
        return new Promise(async (resolve, reject) => {
            await axios.delete(config.apiUrl + '/api/cities/' + id, {
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