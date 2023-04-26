import axios from "axios";
import {config} from "@/config/config";

const state = {
    infoPublics: []
};

const getters = {
    INFO_PUBLICS: state => {
        return state.infoPublics;
    }
};

const mutations = {
    setInfoPublics(state, payload) {
        state.infoPublics = payload;
    }
};

const actions = {
    loadAllInfoPublics: async (context) => {
        await axios.get(config.apiUrl + '/api/informationpublics')
            .then((response) => {
                context.commit('setInfoPublics', response.data);
            })
    },
    loadInfoPublicsBySearchQuery: async (context, searchQuery) => {
        await axios.get(config.apiUrl + '/api/informationpublics/search/' + searchQuery)
            .then((response) => {
                context.commit('setInfoPublics', response.data);
            })
    },
    addInfoPublic: (context, payload) => {
        return new Promise(async (resolve, reject) => {
            let formData = new FormData();
            formData.append('publicName', payload.publicName);
            formData.append('description', payload.description);
            formData.append('ownerId', payload.ownerId);
            formData.append('image', payload.image);
            formData.append('issuerId', payload.issuerId);
            await axios.post(config.apiUrl + '/api/informationpublics', formData, {
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
    joinInfoPublic: (context, payload) => {
        return new Promise(async (resolve, reject) => {
            await axios.post(config.apiUrl + '/api/informationpublics/join', {
                'publicId': payload.publicId,
                'userId': payload.userId
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
    blockUserAtInfoPublic: (context, payload) => {
        return new Promise(async (resolve, reject) => {
            await axios.post(config.apiUrl + '/api/informationpublics/block-user', {
                'publicId': payload.publicId,
                'userId': payload.userId,
                'issuerId': payload.issuerId
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
    unblockUserAtInfoPublic: (context, payload) => {
        return new Promise(async (resolve, reject) => {
            await axios.post(config.apiUrl + '/api/informationpublics/unblock-user', {
                'publicId': payload.publicId,
                'userId': payload.userId,
                'issuerId': payload.issuerId
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
    leaveInfoPublic: (context, payload) => {
        return new Promise(async (resolve, reject) => {
            await axios.post(config.apiUrl + '/api/informationpublics/leave', {
                'publicId': payload.publicId,
                'userId': payload.userId
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
    loadInfoPublicById: (context, id) => {
        return new Promise(async (resolve, reject) => {
            await axios.get(config.apiUrl + '/api/informationpublics/' + id)
                .then((response) => {
                    resolve(response);
                })
                .catch((error) => {
                    reject(error);
                })
        });
    },
    updateInfoPublic: (context, payload) => {
        return new Promise(async (resolve, reject) => {
            let formData = new FormData();
            formData.append('id', payload.id);
            formData.append('issuerId', payload.issuerId);
            formData.append('publicName', payload.publicName);
            formData.append('description', payload.description ? payload.description : '');
            formData.append('ownerId', payload.ownerId);
            formData.append('image', payload.image);
            await axios.put(config.apiUrl + '/api/informationpublics', formData, {
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
    deleteInfoPublic: (context, id) => {
        return new Promise(async (resolve, reject) => {
            await axios.delete(config.apiUrl + '/api/informationpublics/' + id, {
                headers: config.headers
            })
                .then((response) => {
                    resolve(response);
                })
                .catch((error) => {
                    reject(error);
                })
        });
    },
    banInfoPublic: (context, payload) => {
        return new Promise(async (resolve, reject) => {
            await axios.post(config.apiUrl + '/api/informationpublics/ban', {
                'bannedPublicId': payload.publicId,
                'reason': payload.reason
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
    unbanInfoPublic: (context, payload) => {
        return new Promise(async (resolve, reject) => {
            await axios.post(config.apiUrl + '/api/informationpublics/unban', {
                'unbannedPublicId': payload.publicId,
                'reason': payload.reason
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
    }
};

export default {
    state,
    getters,
    mutations,
    actions,
};