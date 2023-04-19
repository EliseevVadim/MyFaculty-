import axios from "axios";
import {config} from "@/config/config";

const state = {
	infoPosts : []
};

const getters = {
	INFO_POSTS: state => {
		return state.infoPosts;
	}
};

const mutations = {
	setInfoPosts(state, payload) {
		state.infoPosts = payload;
	}
};

const actions = {
	loadInfoPostsByInfoPublicId: async (context, infoPublicId) => {
		await axios.get(config.apiUrl + '/api/infoposts/info-public/' + infoPublicId, {
			headers: config.headers
		})
			.then((response) => {
				context.commit('setInfoPosts', response.data);
			})
	},
	loadInfoPostsByStudyClubId: async (context, studyClubId) => {
		await axios.get(config.apiUrl + '/api/infoposts/study-club/' + studyClubId)
			.then((response) => {
				context.commit('setInfoPosts', response.data);
			})
	},
	loadInfoPostsNewsfeed: async (context) => {
		await axios.get(config.apiUrl + '/api/news/posts', {
			headers: config.headers
		})
			.then((response) => {
				context.commit('setInfoPosts', response.data)
			})
	},
	loadInfoPostById: (context, id) => {
		return new Promise(async (resolve, reject) => {
			await axios.get(config.apiUrl + '/api/infoposts/' + id, {
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
	addInfoPost: (context, payload) => {
		return new Promise(async (resolve, reject) => {
			let formData = new FormData();
			formData.append('textContent', payload.textContent ? payload.textContent : '');
			formData.append('infoPublicId', payload.infoPublicId);
			formData.append('studyClubId', payload.studyClubId);
			formData.append('authorId', payload.authorId);
			formData.append('commentsAllowed', payload.commentsAllowed);
			if (payload.attachments.length === 0)
				formData.append('postAttachments', payload.attachments);
			else {
				for (const key in payload.attachments) {
					formData.append('postAttachments', payload.attachments[key]);
				}
			}
			await axios.post(config.apiUrl + '/api/infoposts', formData, {
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
	likeInfoPost: (context, payload) => {
		return new Promise(async (resolve, reject) => {
			await axios.post(config.apiUrl + '/api/infoposts/like', {
				'likedUserId': payload.likedUserId,
				'likedPostId': payload.likedPostId
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
	unlikeInfoPost: (context, payload) => {
		return new Promise(async (resolve, reject) => {
			await axios.post(config.apiUrl + '/api/infoposts/unlike', {
				'likedUserId': payload.likedUserId,
				'likedPostId': payload.likedPostId
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
	updateInfoPost: (context, payload) => {
		return new Promise(async (resolve, reject) => {
			let formData = new FormData();
			formData.append('infoPostId', payload.id);
			formData.append('issuerId', payload.issuerId);
			formData.append('postAttachmentsUid', payload.postAttachmentsUid);
			formData.append('textContent', payload.textContent ? payload.textContent : '');
			formData.append('commentsAllowed', payload.commentsAllowed);
			formData.append('actualAttachments', payload.attachments ? payload.attachments : '');
			formData.append('oldAttachments', payload.oldAttachments ? payload.oldAttachments : '');
			if (payload.newFiles.length === 0)
				formData.append('newFiles', payload.newFiles);
			else
				for (const key in payload.newFiles)
					formData.append('newFiles', payload.newFiles[key]);
			await axios.put(config.apiUrl + '/api/infoposts', formData, {
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
	deleteInfoPost: (context, id) => {
		return new Promise(async (resolve, reject) => {
			await axios.delete(config.apiUrl + '/api/infoposts/' + id, {
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