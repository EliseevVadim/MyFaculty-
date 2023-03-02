import axios from "axios";
import {config} from "@/config/config";

const state = {
	comments : []
};

const getters = {
	COMMENTS: state => {
		return state.comments;
	}
};

const mutations = {
	setComments(state, payload) {
		state.comments = payload;
	}
};

const actions = {
	loadCommentsByPostId: async (context, postId) => {
		await axios.get(config.apiUrl + '/api/comments/post/' + postId, {
			headers: config.headers
		})
			.then((response) => {
				context.commit('setComments', response.data);
			})
	},
	addComment: (context, payload) => {
		return new Promise(async (resolve, reject) => {
			let formData = new FormData();
			formData.append('textContent', payload.textContent ? payload.textContent : '');
			formData.append('postId', payload.postId);
			formData.append('parentCommentId', payload.parentCommentId);
			formData.append('authorId', payload.authorId);
			if (payload.attachments.length === 0)
				formData.append('commentAttachments', payload.attachments);
			else {
				for (const key in payload.attachments) {
					formData.append('commentAttachments', payload.attachments[key]);
				}
			}
			await axios.post(config.apiUrl + '/api/comments', formData, {
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
	updateComment: (context, payload) => {
		return new Promise(async (resolve, reject) => {
			let formData = new FormData();
			formData.append('commentId', payload.id);
			formData.append('issuerId', payload.issuerId);
			formData.append('parentCommentId', payload.parentCommentId ? payload.parentCommentId : '');
			formData.append('commentAttachmentsUid', payload.commentAttachmentsUid);
			formData.append('textContent', payload.textContent ? payload.textContent : '');
			formData.append('actualAttachments', payload.attachments ? payload.attachments : '');
			formData.append('oldAttachments', payload.oldAttachments ? payload.oldAttachments : '');
			if (payload.newFiles.length === 0)
				formData.append('newFiles', payload.newFiles);
			else
				for (const key in payload.newFiles)
					formData.append('newFiles', payload.newFiles[key]);
			await axios.put(config.apiUrl + '/api/comments', formData, {
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
	deleteComment: (context, id) => {
		return new Promise(async (resolve, reject) => {
			await axios.delete(config.apiUrl + '/api/comments/' + id, {
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