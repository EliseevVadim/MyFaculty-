import axios from "axios";
import {config} from "@/config/config";

const state = {
    teachers_disciplines: []
};

const getters = {
    TEACHERS_DISCIPLINES: state => {
        return state.teachers_disciplines;
    }
};

const mutations = {
    setTeachersDisciplines(state, payload) {
        state.teachers_disciplines = payload;
    }
};

const actions = {
    loadAllTeachersDisciplines: async (context) => {
        await axios.get(config.apiUrl + '/api/teachersDisciplines')
            .then((response) => {
                context.commit('setTeachersDisciplines', response.data);
            })
    },
    addTeacherDiscipline: (context, payload) => {
        return new Promise(async (resolve, reject) => {
            await axios.post(config.apiUrl + '/api/teachersDisciplines', {
                'teacherId': payload.teacher_id,
                'disciplineId': payload.discipline_id
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
    loadTeacherDisciplineById: (context, id) => {
        return new Promise(async (resolve, reject) => {
            await axios.get(config.apiUrl + '/api/teachersDisciplines/' + id)
                .then((response) => {
                    resolve(response);
                })
                .catch((error) => {
                    reject(error);
                })
        });
    },
    updateTeacherDiscipline: (context, payload) => {
        return new Promise(async (resolve, reject) => {
            await axios.put(config.apiUrl + '/api/teachersDisciplines', {
                'id': payload.id,
                'teacherId': payload.teacher_id,
                'disciplineId': payload.discipline_id
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
    deleteTeacherDiscipline: (context, id) => {
        return new Promise(async (resolve, reject) => {
            await axios.delete(config.apiUrl + '/api/teachersDisciplines/' + id, {
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