import axios from "axios";
import {config} from "@/config/config";

const state = {
    clubTasks: []
};

const getters = {
    CLUB_TASKS: state => {
        return state.clubTasks;
    }
};

const mutations = {
    setClubTasks(state, payload) {
        state.clubTasks = payload;
    }
};

const actions = {
    loadClubTasksByStudyClubId: async (context, studyClubId) => {
        await axios.get(config.apiUrl + '/api/clubtasks/study-club/' + studyClubId)
            .then((response) => {
                context.commit('setClubTasks', response.data);
            })
    },
    loadClubTasksNewsfeed: async (context) => {
        await axios.get(config.apiUrl + '/api/news/tasks', {
            headers: config.headers
        })
            .then((response) => {
                context.commit('setClubTasks', response.data)
            })
    },
    loadClubTaskById: (context, id) => {
        return new Promise(async (resolve, reject) => {
            await axios.get(config.apiUrl + '/api/clubtasks/' + id, {
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
    addClubTask: (context, payload) => {
        return new Promise(async (resolve, reject) => {
            let formData = new FormData();
            formData.append('textContent', payload.textContent ? payload.textContent : '');
            formData.append('studyClubId', payload.studyClubId);
            formData.append('authorId', payload.authorId);
            formData.append('deadLine', payload.deadLine);
            formData.append('cost', payload.cost);
            formData.append('timezoneOffset', payload.timezoneOffset)
            if (payload.attachments.length === 0)
                formData.append('postAttachments', payload.attachments);
            else {
                for (const key in payload.attachments) {
                    formData.append('postAttachments', payload.attachments[key]);
                }
            }
            await axios.post(config.apiUrl + '/api/clubtasks', formData, {
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
    updateClubTask: (context, payload) => {
        return new Promise(async (resolve, reject) => {
            let formData = new FormData();
            formData.append('id', payload.id);
            formData.append('issuerId', payload.issuerId);
            formData.append('postAttachmentsUid', payload.postAttachmentsUid);
            formData.append('textContent', payload.textContent ? payload.textContent : '');
            formData.append('actualAttachments', payload.attachments ? payload.attachments : '');
            formData.append('deadLine', payload.deadLine);
            formData.append('cost', payload.cost);
            formData.append('timezoneOffset', payload.timezoneOffset)
            formData.append('oldAttachments', payload.oldAttachments ? payload.oldAttachments : '');
            if (payload.newFiles.length === 0)
                formData.append('newFiles', payload.newFiles);
            else
                for (const key in payload.newFiles)
                    formData.append('newFiles', payload.newFiles[key]);
            await axios.put(config.apiUrl + '/api/clubtasks', formData, {
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
    deleteClubTask: (context, id) => {
        return new Promise(async (resolve, reject) => {
            await axios.delete(config.apiUrl + '/api/clubtasks/' + id, {
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