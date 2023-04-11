import axios from "axios";
import {config} from "@/config/config";

const state = {
	taskSubmissions : []
};

const getters = {
	TASK_SUBMISSIONS: state => {
		return state.taskSubmissions;
	}
};

const mutations = {
	setTaskSubmissions(state, payload) {
		state.taskSubmissions = payload;
	}
};

const actions = {
	loadTaskSubmissionsByTaskId: async (context, postId) => {
		await axios.get(config.apiUrl + '/api/tasksubmissions/task/' + postId, {
			headers: config.headers
		})
			.then((response) => {
				context.commit('setTaskSubmissions', response.data);
			})
	},
	loadTaskSubmissionsByTaskIdForCurrentUser: async (context, postId) => {
		await axios.get(config.apiUrl + '/api/tasksubmissions/mine/task/' + postId, {
			headers: config.headers
		})
			.then((response) => {
				context.commit('setTaskSubmissions', response.data);
			})
	},
	addTaskSubmission: (context, payload) => {
		return new Promise(async (resolve, reject) => {
			let formData = new FormData();
			formData.append('title', payload.title);
			formData.append('textContent', payload.textContent ? payload.textContent : '');
			formData.append('taskId', payload.taskId);
			formData.append('authorId', payload.authorId);
			if (payload.attachments.length === 0)
				formData.append('submissionAttachments', payload.attachments);
			else {
				for (const key in payload.attachments) {
					formData.append('submissionAttachments', payload.attachments[key]);
				}
			}
			await axios.post(config.apiUrl + '/api/tasksubmissions', formData, {
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
	updateTaskSubmission: (context, payload) => {
		return new Promise(async (resolve, reject) => {
			let formData = new FormData();
			formData.append('id', payload.id);
			formData.append('issuerId', payload.issuerId);
			formData.append('submissionAttachmentsUid', payload.submissionAttachmentsUid);
			formData.append('title', payload.title);
			formData.append('textContent', payload.textContent ? payload.textContent : '');
			formData.append('actualAttachments', payload.attachments ? payload.attachments : '');
			formData.append('oldAttachments', payload.oldAttachments ? payload.oldAttachments : '');
			if (payload.newFiles.length === 0)
				formData.append('newFiles', payload.newFiles);
			else
				for (const key in payload.newFiles)
					formData.append('newFiles', payload.newFiles[key]);
			await axios.put(config.apiUrl + '/api/tasksubmissions', formData, {
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
	deleteTaskSubmission: (context, id) => {
		return new Promise(async (resolve, reject) => {
			await axios.delete(config.apiUrl + '/api/tasksubmissions/' + id, {
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