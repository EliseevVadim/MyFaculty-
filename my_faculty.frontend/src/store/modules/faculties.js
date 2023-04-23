import axios from "axios";
import {config} from "@/config/config";

const state = {
    faculties: []
};

const getters = {
    FACULTIES: state => {
        return state.faculties;
    }
};

const mutations = {
    setFaculties(state, payload) {
        state.faculties = payload;
    }
};

const actions = {
    loadAllFaculties: async (context) => {
        await axios.get(config.apiUrl + '/api/faculties')
            .then((response) => {
                context.commit('setFaculties', response.data);
            })
    },
    loadAllFacultiesStateless: (context) => {
        return new Promise(async (resolve, reject) => {
            await axios.get(config.apiUrl + '/api/faculties')
                .then((response) => {
                    resolve(response);
                })
                .catch((error) => {
                    reject(error);
                })
        })
    },
    addFaculty: (context, payload) => {
        return new Promise(async (resolve, reject) => {
            await axios.post(config.apiUrl + '/api/faculties', {
                'facultyName': payload.faculty_name,
                'officialWebsite': payload.official_website
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
    loadFacultyById: (context, id) => {
        return new Promise(async (resolve, reject) => {
            await axios.get(config.apiUrl + '/api/faculties/' + id)
                .then((response) => {
                    resolve(response);
                })
                .catch((error) => {
                    reject(error);
                })
        });
    },
    updateFaculty: (context, payload) => {
        return new Promise(async (resolve, reject) => {
            await axios.put(config.apiUrl + '/api/faculties/', {
                'id': payload.id,
                'facultyName': payload.faculty_name,
                'officialWebsite': payload.official_website
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
    deleteFaculty: (context, id) => {
        return new Promise(async (resolve, reject) => {
            await axios.delete(config.apiUrl + '/api/faculties/' + id, {
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