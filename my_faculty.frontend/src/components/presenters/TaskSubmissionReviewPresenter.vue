<template>
	<div>
		<EditTaskSubmissionReviewModal
			:review="review"
			:show="showEditingForm"
			:max-rate="maxRate"
			:submission-status="submissionStatus"
			@close="showEditingForm = false"
			@load="reloadSubmissions"
			ref="editingForm"
		/>
		<v-container
			fluid
			class="review-container px-0 mb-0"
		>
			<div class="review-content">
				<h2 class="review-title">
					Отзыв к решению
				</h2>
				<v-list-item
					class="mb-3"
					:key="getFullName(review.reviewer)"
				>
					<v-list-item-content class="review-metadata">
						<v-list-item-title>
							<router-link class="reviewer-name" :to="'/id' + review.reviewer.id">
								{{getFullName(review.reviewer)}}
							</router-link>
						</v-list-item-title>
						<v-list-item-subtitle v-html="prettifyPublishDate()">
						</v-list-item-subtitle>
					</v-list-item-content>
					<v-menu
						v-if="currentUserIsTaskModerator"
						bottom
						left
					>
						<template v-slot:activator="{ on, attrs }">
							<v-btn
								icon
								v-bind="attrs"
								v-on="on"
							>
								<v-icon>mdi-dots-horizontal</v-icon>
							</v-btn>
						</template>
						<v-list>
							<v-list-item
								ripple
							>
								<v-list-item-title
									class="review-context-menu-option"
									@click="startReviewEditing"
								>
									Редактировать отзыв
								</v-list-item-title>
							</v-list-item>
							<v-list-item
								ripple
							>
								<v-list-item-title
									class="review-context-menu-option"
									@click="deleteSubmissionReview"
								>
									Удалить отзыв
								</v-list-item-title>
							</v-list-item>
						</v-list>
					</v-menu>
				</v-list-item>
				<v-row
					class="mx-1 text-left pt-2 pb-2 mb-1"
				>
				<span class="mx-2 review-information-mark">
					Оценка:
				</span>
					<span class="mx-2 review-information-value">
					{{`${review.rate} / ${maxRate}`}}
				</span>
				</v-row>
				<h3
					v-if="getPureText(review.textContent) || review.attachments"
					class="black--text text-left ml-3 mb-5"
				>
					Комментарий преподавателя:
				</h3>
				<v-row
					class="ml-3 text-left"
					v-html="review.textContent"
				>
				</v-row>
				<viewer
					class="image-gallery"
					:images="images"
				>
					<img
						v-for="src in images"
						:key="src"
						:src="src"
						alt="#"
					>
				</viewer>
				<div class="theme-list" v-if="otherFiles.length > 0">
					<div class="vue-file-agent grid-block-wrapper" style="padding: 0;">
						<template v-for="(fileRecord, i) in otherFiles">
							<VueFilePreview
								:key="i"
								:value="fileRecord"
								:linkable="true"
								class="file-preview-wrapper grid-box-item grid-block"
								style="z-index: 0"
							></VueFilePreview>
						</template>
					</div>
				</div>
			</div>
		</v-container>
	</div>
</template>

<script>
import EditTaskSubmissionReviewModal
	from "@/components/AccountComponents/teacherComponents/EditTaskSubmissionReviewModal";
export default {
	name: "TaskSubmissionReviewPresenter",
	components: {EditTaskSubmissionReviewModal},
	props: ['review', 'currentUserIsTaskModerator', 'maxRate', 'submissionStatus'],
	data() {
		return {
			images: [],
			otherFiles: [],
			showEditingForm: false
		}
	},
	methods: {
		getPureText(htmlContent) {
			return htmlContent ? new DOMParser()
				.parseFromString(htmlContent, "text/html")
				.body.textContent : htmlContent;
		},
		reloadSubmissions() {
			this.$emit('load');
		},
		getFullName(user) {
			return `${user.firstName} ${user.lastName}`;
		},
		prettifyPublishDate() {
			let creationDate = new Date(this.review.created).toLocaleString('ru-RU', {
				year: 'numeric',
				month: 'long',
				day: 'numeric',
				hour: 'numeric',
				minute: 'numeric'
			});
			if (!this.review.updated)
				return creationDate;
			let updateDate = new Date(this.review.updated).toLocaleString('ru-RU', {
				year: 'numeric',
				month: 'long',
				day: 'numeric',
				hour: 'numeric',
				minute: 'numeric'
			});
			return `${creationDate} <wbr>(обновлено ${updateDate})`;
		},
		processAttachments() {
			let attachments = JSON.parse(this.review.attachments);
			if (!attachments)
				return;
			this.images = attachments
				.filter(element => element.ContentType.startsWith('image'))
				.map(element => element.Path);
			this.otherFiles = attachments
				.filter(element => !element.ContentType.startsWith('image'))
				.map(element => {
					return {
						name: element.FileName,
						ext: element.Extension,
						size: element.Length,
						type: element.ContentType,
						url: element.Path
					}
				});
		},
		startReviewEditing() {
			this.$refs.editingForm.loadReview();
			this.showEditingForm = true;
		},
		deleteSubmissionReview() {
			this.$confirm("Вы действительно хотите удалить этот отзыв? Все вложения будут" +
				" потеряны навсегда")
				.then((result) => {
					if (result) {
						this.$loading(true);
						this.$store.dispatch('deleteTaskSubmissionReview', this.review.id)
							.then(() => {
								this.$notify({
									group: 'admin-actions',
									title: 'Успешная операция',
									text: 'Отзыв успешно удален',
									type: 'success'
								});
								this.$emit('load');
							})
							.catch((error) => {
								this.$notify({
									group: 'admin-actions',
									title: 'Ошибка',
									text: error.response.data.error,
									type: 'error'
								})
							})
							.finally(() => {
								this.$loading(false);
							})
					}
				})
		}
	},
	mounted() {
		this.processAttachments();
	}
}
</script>

<style scoped>
	.review-container {
		background: #e7e0e0;
		border-bottom-left-radius: 10px;
		border-bottom-right-radius: 10px;
	}
	.image-gallery {
		display: grid;
		grid-template-columns: repeat(3, 1fr);
		grid-gap: 20px;
	}
	img {
		width: 100%;
		padding: 10px;
		object-fit: contain;
		cursor: pointer;
		position: relative;
		border-radius: 8%;
	}
	.reviewer-name {
		font-weight: bolder;
		font-size: 16px;
		color: #4040c0;
	}
	.review-context-menu-option {
		text-align: left;
		cursor: pointer;
	}
	.review-metadata {
		text-align: left;
	}
	.review-content {
		padding-right: 12px;
		padding-left: 12px;
	}
	.review-title {
		color: black;
	}
	.review-information-mark {
		color: black;
		opacity: 0.5;
		font-size: 22px;
	}
	.review-information-value {
		color: black;
		font-style: italic;
		font-size: 22px;
	}
	@media only screen and (max-width: 600px) {
		.image-gallery {
			grid-template-columns: repeat(2, 1fr);
		}
	}
	@media only screen and (max-width: 400px) {
		.image-gallery {
			grid-template-columns: repeat(1, 1fr);
		}
	}
</style>