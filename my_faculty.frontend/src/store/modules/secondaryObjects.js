import axios from "axios";
import {config} from "@/config/config";

const state = {
    secondaryObjects: []
};

const getters = {
    SECONDARY_OBJECTS: state => {
        return state.secondaryObjects;
    }
};

const mutations = {
    setSecondaryObjects(state, payload) {
        state.secondaryObjects = payload;
    }
};

const actions = {
    loadAllSecondaryObjects: async (context) => {
        await axios.get(config.apiUrl + '/api/secondaryObjects')
            .then((response) => {
                context.commit('setSecondaryObjects', response.data);
            })
    },
    addSecondaryObject: (context, payload) => {
        return new Promise(async (resolve, reject) => {
            await axios.post(config.apiUrl + '/api/secondaryObjects', {
                'objectName': payload.object_name,
                'positionInfo': JSON.stringify(payload.position_info),
                'secondaryObjectTypeId': payload.object_type_id,
                'floorId': payload.floor_id
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
    loadSecondaryObjectById: (context, id) => {
        return new Promise(async (resolve, reject) => {
            await axios.get(config.apiUrl + '/api/secondaryObjects/' + id)
                .then((response) => {
                    resolve(response);
                })
                .catch((error) => {
                    reject(error);
                })
        });
    },
    updateSecondaryObject: (context, payload) => {
        return new Promise(async (resolve, reject) => {
            await axios.put(config.apiUrl + '/api/secondaryObjects', {
                'id': payload.id,
                'objectName': payload.object_name,
                'positionInfo': JSON.stringify(payload.position_info),
                'secondaryObjectTypeId': payload.object_type_id,
                'floorId': payload.floor_id
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
    deleteSecondaryObject: (context, id) => {
        return new Promise(async (resolve, reject) => {
            await axios.delete(config.apiUrl + '/api/secondaryObjects/' + id, {
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