import axios from "axios";
import {config} from "@/config/config";

const state = {
    groups: []
};

const getters = {
    GROUPS: state => {
        return state.groups;
    }
};

const mutations = {
    setGroups(state, payload) {
        state.groups = payload;
    }
};

const actions = {
    loadAllGroups: async (context) => {
        await axios.get(config.apiUrl + '/api/groups')
            .then((response) => {
                context.commit('setGroups', response.data);
            })
    },
    addGroup: (context, payload) => {
        return new Promise(async (resolve, reject) => {
            await axios.post(config.apiUrl + '/api/groups', {
                'groupName': payload.group_name,
                'courseId': payload.course_id
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
    loadGroupById: (context, id) => {
        return new Promise(async (resolve, reject) => {
            await axios.get(config.apiUrl + '/api/groups/' + id)
                .then((response) => {
                    resolve(response);
                })
                .catch((error) => {
                    reject(error);
                })
        });
    },
    loadGroupsByFacultyId: async (context, id) => {
        await axios.get(config.apiUrl + '/api/groups/faculty/' + id)
            .then((response) => {
                context.commit('setGroups', response.data);
            })
    },
    loadGroupsByCourseId: async (context, id) => {
        await axios.get(config.apiUrl + '/api/groups/course/' + id)
            .then((response) => {
                context.commit('setGroups', response.data);
            })
    },
    loadGroupsByCourseIdStateless: (context, id) => {
        return new Promise(async (resolve, reject) => {
            await axios.get(config.apiUrl + '/api/groups/course/' + id)
                .then((response) => {
                    resolve(response);
                })
                .catch((error) => {
                    reject(error);
                })
        })
    },
    updateGroup: (context, payload) => {
        return new Promise(async (resolve, reject) => {
            await axios.put(config.apiUrl + '/api/groups', {
                'id': payload.id,
                'groupName': payload.group_name,
                'courseId': payload.course_id
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
    deleteGroup: (context, id) => {
        return new Promise(async (resolve, reject) => {
            await axios.delete(config.apiUrl + '/api/groups/' + id, {
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