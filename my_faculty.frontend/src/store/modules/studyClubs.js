import axios from "axios";
import {config} from "@/config/config";

const state = {
	studyClubs : []
};

const getters = {
	STUDY_CLUBS: state => {
		return state.studyClubs;
	}
};

const mutations = {
	setStudyClubs(state, payload) {
		state.studyClubs = payload;
	}
};

const actions = {
	loadAllStudyClubs: async (context) => {
		await axios.get(config.apiUrl + '/api/studyclubs')
			.then((response) => {
				context.commit('setStudyClubs', response.data);
			})
	},
	loadStudyClubsForSpecificUser: async (context, userId) => {
		await axios.get(config.apiUrl + '/api/studyclubs/user/' + userId)
			.then((response) => {
				context.commit('setStudyClubs', response.data);
			})
	},
	loadStudyClubsBySearchQuery: async (context, searchQuery) => {
		await axios.get(config.apiUrl + '/api/studyclubs/search/' + searchQuery)
			.then((response) => {
				context.commit('setStudyClubs', response.data);
			})
	},
	addStudyClub: (context, payload) => {
		return new Promise(async (resolve, reject) => {
			let formData = new FormData();
			formData.append('studyClubName', payload.clubName);
			formData.append('description', payload.description);
			formData.append('ownerId', payload.ownerId);
			formData.append('image', payload.image);
			console.log(payload);
			await axios.post(config.apiUrl + '/api/studyclubs', formData, {
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
	joinStudyClub: (context, payload) => {
		return new Promise(async (resolve, reject) => {
			await axios.post(config.apiUrl + '/api/studyclubs/join', {
				'studyClubId': payload.studyClubId,
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
	addModeratorToStudyClub: (context, payload) => {
		return new Promise(async (resolve, reject) => {
			await axios.post(config.apiUrl + '/api/studyclubs/add-moderator', {
				'issuerId': payload.issuerId,
				'studyClubId': payload.studyClubId,
				'moderatorId': payload.moderatorId
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
	demoteModeratorAtStudyClub: (context, payload) => {
		return new Promise(async (resolve, reject) => {
			await axios.post(config.apiUrl + '/api/studyclubs/demote-moderator', {
				'issuerId': payload.issuerId,
				'studyClubId': payload.studyClubId,
				'moderatorId': payload.moderatorId
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
	removeUserFromStudyClub: (context, payload) => {
		return new Promise(async (resolve, reject) => {
			await axios.post(config.apiUrl + '/api/studyclubs/remove-user', {
				'issuerId': payload.issuerId,
				'studyClubId': payload.studyClubId,
				'removingUserId': payload.removingUserId
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
	addUsersFromGroupToStudyClub: (context, payload) => {
		return new Promise(async (resolve, reject) => {
			await axios.post(config.apiUrl + '/api/studyclubs/add-users-from-group', {
				'issuerId': payload.issuerId,
				'studyClubId': payload.studyClubId,
				'groupId': payload.groupId
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
	addUsersFromCourseToStudyClub: (context, payload) => {
		return new Promise(async (resolve, reject) => {
			await axios.post(config.apiUrl + '/api/studyclubs/add-users-from-course', {
				'issuerId': payload.issuerId,
				'studyClubId': payload.studyClubId,
				'courseId': payload.courseId
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
	removeUsersByGroupFromStudyClub: (context, payload) => {
		return new Promise(async (resolve, reject) => {
			await axios.post(config.apiUrl + '/api/studyclubs/remove-users-from-group', {
				'issuerId': payload.issuerId,
				'studyClubId': payload.studyClubId,
				'groupId': payload.groupId
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
	removeUsersByCourseFromStudyClub: (context, payload) => {
		return new Promise(async (resolve, reject) => {
			await axios.post(config.apiUrl + '/api/studyclubs/remove-users-from-course', {
				'issuerId': payload.issuerId,
				'studyClubId': payload.studyClubId,
				'courseId': payload.courseId
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
	leaveStudyClub: (context, payload) => {
		return new Promise(async (resolve, reject) => {
			await axios.post(config.apiUrl + '/api/studyclubs/leave', {
				'studyClubId': payload.studyClubId,
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
	loadStudyClubById: (context, id) => {
		return new Promise(async (resolve, reject) => {
			await axios.get(config.apiUrl + '/api/studyclubs/' + id)
				.then((response) => {
					resolve(response);
				})
				.catch((error) => {
					reject(error);
				})
		});
	},
	updateStudyClub: (context, payload) => {
		return new Promise(async (resolve, reject) => {
			let formData = new FormData();
			formData.append('id', payload.id);
			formData.append('issuerId', payload.issuerId);
			formData.append('studyClubName', payload.clubName);
			formData.append('description', payload.description ? payload.description : '');
			formData.append('ownerId', payload.ownerId);
			formData.append('image', payload.image);
			await axios.put(config.apiUrl + '/api/studyclubs', formData, {
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
	deleteStudyClub: (context, id) => {
		return new Promise(async (resolve, reject) => {
			await axios.delete(config.apiUrl + '/api/studyclubs/' + id, {
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