import axios from "axios";
import {config} from "@/config/config";

const state = {
	teachers : [],
	selectedTeacher : null
};

const getters = {
	TEACHERS: state => {
		return state.teachers;
	},
	SELECTED_TEACHER: state => {
		return state.selectedTeacher;
	}
};

const mutations = {
	setTeachers(state, payload) {
		state.teachers = payload;
	},
	selectTeacher(state, payload) {
		state.selectedTeacher = payload;
	}
};

const actions = {
	loadAllTeachers: async (context) => {
		await axios.get(config.apiUrl + '/api/teachers')
			.then((response) => {
				context.commit('setTeachers', response.data);
			})
	},
	addTeacher: (context, payload) => {
		return new Promise(async (resolve, reject) => {
			let formData = new FormData();
			formData.append('fio', payload.fio);
			formData.append('birthDate', payload.birth_date);
			formData.append('email', payload.email);
			formData.append('scienceRankId', payload.science_rank_id);
			formData.append('photo', payload.photo);
			await axios.post(config.apiUrl + '/api/teachers', formData, {
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
	loadTeacherById: (context, id) => {
		return new Promise(async (resolve, reject) => {
			await axios.get(config.apiUrl + '/api/teachers/' + id)
				.then((response) => {
					resolve(response);
				})
				.catch((error) => {
					reject(error);
				})
		});
	},
	updateTeacher: (context, payload) => {
		return new Promise(async (resolve, reject) => {
			let formData = new FormData();
			formData.append('id', payload.id);
			formData.append('fio', payload.fio);
			formData.append('birthDate', payload.birth_date);
			formData.append('email', payload.email);
			formData.append('scienceRankId', payload.science_rank_id);
			formData.append('photo', payload.photo);
			await axios.put(config.apiUrl + '/api/teachers', formData, {
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
	deleteTeacher: (context, id) => {
		return new Promise(async (resolve, reject) => {
			await axios.delete(config.apiUrl + '/api/teachers/' + id, {
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
	sendVerificationTokenToTeacher: (context, id) => {
		return new Promise(async (resolve, reject) => {
			await axios.get(config.apiUrl + '/api/teachers/' + id + '/send-verification-token', {
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
	sendVerificationRequest: (context, payload) => {
		return new Promise(async (resolve, reject) => {
			await axios.post(config.apiUrl + '/api/teachers/verify', payload, {
				headers: config.headers
			})
				.then((response) => {
					resolve(response);
				})
				.catch((error) => {
					reject(error)
				});
		})
	}
};

export default {
	state,
	getters,
	mutations,
	actions,
};