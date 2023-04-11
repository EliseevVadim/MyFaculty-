import axios from "axios";
import {config} from "@/config/config";

const actions = {
	addTaskSubmissionReview: (context, payload) => {
		return new Promise(async (resolve, reject) => {
			let formData = new FormData();
			formData.append('textContent', payload.textContent ? payload.textContent : '');
			formData.append('submissionId', payload.submissionId);
			formData.append('reviewerId', payload.reviewerId);
			formData.append('rate', payload.rate);
			formData.append('newStatus', payload.newStatus);
			if (payload.attachments.length === 0)
				formData.append('attachments', payload.attachments);
			else {
				for (const key in payload.attachments) {
					formData.append('attachments', payload.attachments[key]);
				}
			}
			await axios.post(config.apiUrl + '/api/tasksubmissionreviews', formData, {
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
	updateTaskSubmissionReview: (context, payload) => {
		return new Promise(async (resolve, reject) => {
			let formData = new FormData();
			formData.append('id', payload.id);
			formData.append('issuerId', payload.issuerId);
			formData.append('submissionReviewAttachmentsUid', payload.submissionReviewAttachmentsUid);
			formData.append('textContent', payload.textContent ? payload.textContent : '');
			formData.append('actualAttachments', payload.attachments ? payload.attachments : '');
			formData.append('rate', payload.rate);
			formData.append('oldAttachments', payload.oldAttachments ? payload.oldAttachments : '');
			formData.append('newStatus', payload.newStatus);
			if (payload.newFiles.length === 0)
				formData.append('newFiles', payload.newFiles);
			else
				for (const key in payload.newFiles)
					formData.append('newFiles', payload.newFiles[key]);
			await axios.put(config.apiUrl + '/api/tasksubmissionreviews', formData, {
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
	deleteTaskSubmissionReview: (context, id) => {
		return new Promise(async (resolve, reject) => {
			await axios.delete(config.apiUrl + '/api/tasksubmissionreviews/' + id, {
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
	actions
};