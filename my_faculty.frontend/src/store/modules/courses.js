import axios from "axios";
import {config} from "@/config/config";

const state = {
	courses : []
};

const getters = {
	COURSES: state => {
		return state.courses;
	}
};

const mutations = {
	setCourses(state, payload) {
		state.courses = payload;
	}
};

const actions = {
	loadAllCourses: async (context) => {
		await axios.get(config.apiUrl + '/api/courses')
			.then((response) => {
				context.commit('setCourses', response.data);
			})
	},
	addCourse: (context, payload) => {
		return new Promise(async (resolve, reject) => {
			await axios.post(config.apiUrl + '/api/courses', {
				'courseName': payload.course_name,
				'courseNumber': payload.course_number,
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
	loadCourseById: (context, id) => {
		return new Promise(async (resolve, reject) => {
			await axios.get(config.apiUrl + '/api/courses/' + id)
				.then((response) => {
					resolve(response);
				})
				.catch((error) => {
					reject(error);
				})
		});
	},
	loadCoursesByFacultyId: async (context, id) => {
		await axios.get(config.apiUrl + '/api/courses/faculty/' + id)
			.then((response) => {
				context.commit('setCourses', response.data);
			})
	},
	updateCourse: (context, payload) => {
		return new Promise(async (resolve, reject) => {
			await axios.put(config.apiUrl + '/api/courses', {
				'id': payload.id,
				'courseName': payload.course_name,
				'courseNumber': payload.course_number,
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
	deleteCourse: (context, id) => {
		return new Promise(async (resolve, reject) => {
			await axios.delete(config.apiUrl + '/api/courses/' + id, {
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