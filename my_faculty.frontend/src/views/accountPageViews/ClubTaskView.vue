<template>
	<div>
		<EditClubTaskModal
			:show="showEditingForm"
			:task="watchingTask"
			@close="showEditingForm = false"
			@load="loadClubTask(watchingTask.id)"
			ref="editingForm"
		/>
		<SubmissionsModal
			v-if="showSubmissions"
			:show="showSubmissions"
			:task-id="watchingTask.id"
			:current-user-is-task-moderator="currentUserIsTaskModerator"
			@close="showSubmissions = false"
			@load="loadClubTask(watchingTask.id)"
		/>
		<ErrorPage
			v-if="taskNotFound"
			error-code="404"
			message="Задание не найдено"
		/>
		<div
			v-else-if="Object.keys(watchingTask).length !== 0"
		>
			<v-container fluid class="task-container mb-3 px-0 pb-0">
				<v-list-item
					class="mb-1"
					:key="watchingTask.owningStudyClub.clubName"
				>
					<v-list-item-avatar>
						<v-img
							class="author-avatar"
							:src="watchingTask.owningStudyClub.imagePath ? watchingTask.owningStudyClub.imagePath : '../img/blank-club.png'"
						/>
					</v-list-item-avatar>
					<v-list-item-content class="club-lookup-content">
						<v-list-item-title>
							<router-link class="author-name" :to="'/clubs/' + watchingTask.owningStudyClub.id">
								{{watchingTask.owningStudyClub.clubName}}
							</router-link>
						</v-list-item-title>
						<v-list-item-subtitle v-html="prettifyPublishDate()">
						</v-list-item-subtitle>
					</v-list-item-content>
					<v-menu
						v-if="currentUserIsTaskAuthor()"
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
							<v-list-item ripple>
								<v-list-item-title
									class="task-context-menu-option"
									@click="startTaskEditing"
								>
									Редактировать задание
								</v-list-item-title>
							</v-list-item>
							<v-list-item ripple>
								<v-list-item-title
									class="task-context-menu-option"
									@click="deleteTask"
								>
									Удалить задание
								</v-list-item-title>
							</v-list-item>
						</v-list>
					</v-menu>
				</v-list-item>
				<v-col cols="12"
					   class="pt-0 mb-2 text-left"
				>
				<span class="task-cost">
					максимальное количество баллов - {{watchingTask.cost}}
				</span>
				</v-col>
				<v-row
					class="ml-3"
					v-html="watchingTask.textContent"
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
				<h3
					class="text-center"
					:class="isDeadLineClose() ? 'red--text' : 'black--text'"
				>
					Задание необходимо выполнить до<wbr> {{prettifyDeadLine()}}
				</h3>
				<div>
					<v-divider></v-divider>
					<v-container
						@click="showSubmissions = true"
						class="submissions-invoker d-flex"
						v-ripple
						fluid
					>
					<span>
						{{currentUserIsTaskModerator ? "Доступные решения" : "Мои решения"}}
					</span>
						<v-spacer></v-spacer>
						<v-icon color="primary">
							mdi-chevron-right
						</v-icon>
					</v-container>
				</div>
			</v-container>
			<div class="comments-wrapper pt-2">
				<div class="comment-form">
					<CommentForm
						ref="commentForm"
						:post-id="watchingTask.id"
						@load="loadComments(watchingTask.id)"
					/>
				</div>
				<div class="comments-list">
					<CommentPresenter
						v-for="comment in comments"
						:key="comment.id + comment.updated"
						:comment="comment"
						:all-comments="comments"
						@load="loadComments(watchingTask.id)"
						@select="setReplyingComment"
					/>
				</div>
			</div>
		</div>
	</div>
</template>

<script>
import EditClubTaskModal from "@/components/AccountComponents/EditClubTaskModal";
import SubmissionsModal from "@/components/AccountComponents/SubmissionsModal";
import {mapGetters} from "vuex";
import CommentForm from "@/components/AccountComponents/CommentForm";
import CommentPresenter from "@/components/presenters/CommentPresenter";
import ErrorPage from "@/components/AccountComponents/core/service-pages/ErrorPage";
export default {
	name: "ClubTaskView",
	components: {ErrorPage, CommentPresenter, CommentForm, SubmissionsModal, EditClubTaskModal},
	data() {
		return {
			taskNotFound: false,
			currentUserIsTaskModerator: false,
			watchingTask: {},
			images: [],
			otherFiles: [],
			comments: [],
			showEditingForm: false,
			showSubmissions: false,
			criticalHoursToDeadLine: 24
		}
	},
	methods: {
		loadClubTask(id) {
			this.$loading(true);
			this.$store.dispatch('loadClubTaskById', id)
				.then((response) => {
					this.watchingTask = response.data;
					this.currentUserIsTaskModerator = this.watchingTask
						.owningStudyClub
						.moderators.find(user => user.id == this.$oidc.currentUserId) !== undefined;
					this.loadComments(id);
					this.processAttachments();
					document.title = "Просмотр задания";
				})
				.catch((error) => {
					if (error.response.status === 404) {
						document.title = "Задание не найдено";
						this.taskNotFound = true;
					}
				})
				.finally(() => {
					this.$loading(false);
				});
		},
		loadComments(taskId) {
			this.$store.dispatch('loadCommentsByPostId', taskId)
				.then(() => {
					this.comments = JSON.parse(JSON.stringify(this.COMMENTS.comments));
				})
		},
		prettifyPublishDate() {
			let creationDate = new Date(this.watchingTask.created).toLocaleString('ru-RU', {
				year: 'numeric',
				month: 'long',
				day: 'numeric',
				hour: 'numeric',
				minute: 'numeric'
			});
			if (!this.watchingTask.updated)
				return creationDate;
			let updateDate = new Date(this.watchingTask.updated).toLocaleString('ru-RU', {
				year: 'numeric',
				month: 'long',
				day: 'numeric',
				hour: 'numeric',
				minute: 'numeric'
			});
			return `${creationDate} <wbr>(обновлено ${updateDate})`;
		},
		prettifyDeadLine() {
			return new Date(new Date(this.watchingTask.deadLine).getTime() - new Date().getTimezoneOffset() * 60000)
				.toLocaleString('ru-RU', {
					year: 'numeric',
					month: 'long',
					day: 'numeric',
					hour: 'numeric',
					minute: 'numeric'
				});
		},
		processAttachments() {
			let attachments = JSON.parse(this.watchingTask.attachments);
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
		currentUserIsTaskAuthor() {
			return this.watchingTask.authorId == this.$oidc.currentUserId;
		},
		setReplyingComment(comment) {
			this.$refs.commentForm.setReplyingComment(comment);
		},
		startTaskEditing() {
			this.isDeadLineClose();
			this.$refs.editingForm.loadTask();
			this.showEditingForm = true;
		},
		deleteTask() {
			this.$confirm('Вы действительно хотите удалить задание? При удалении все вложения будут ' +
				'удалены навсегда.')
				.then((result) => {
					if (result) {
						this.$loading(true);
						this.$store.dispatch('deleteClubTask', this.task.id)
							.then(() => {
								this.$notify({
									group: 'admin-actions',
									title: 'Успешная операция',
									text: 'Задание успешно удалено',
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
		},
		isDeadLineClose() {
			let now = new Date();
			let deadLine = new Date(new Date(this.watchingTask.deadLine).getTime() - new Date().getTimezoneOffset() * 60000);
			let difference = (deadLine - now) / 36e5;
			return difference < this.criticalHoursToDeadLine;
		}
	},
	mounted() {
		let id = this.$route.params.id;
		this.loadClubTask(id);
	},
	computed: {
		...mapGetters(['COMMENTS'])
	}
}
</script>

<style scoped lang="scss">
	.author-avatar {
		margin: 0 auto;
		border-radius: 50%;
		border: double 3px transparent;
		background-image: linear-gradient(white, white),
		radial-gradient(circle at bottom left, red 20%, blue, black);
		background-origin: border-box;
		background-clip: padding-box, border-box;
	}
	.task-container {
		background: white;
		border-radius: 10px;
	}
	.author-name {
		font-weight: bolder;
		font-size: 16px;
		color: #4040c0;
	}
	.image-gallery {
		display: grid;
		grid-template-columns: repeat(3, 1fr);
		grid-gap: 20px;
	img {
		width: 100%;
		padding: 10px;
		object-fit: contain;
		cursor: pointer;
		position: relative;
		border-radius: 8%;
	}
	}
	.task-context-menu-option {
		text-align: left;
		cursor: pointer;
	}
	.modal-invoker {
		cursor: pointer;
	}
	.comments-invoker {
		border-bottom-right-radius: 10px;
		border-bottom-left-radius: 10px;
		cursor: pointer;
	}
	.submissions-invoker {
		cursor: pointer;
	}
	.task-cost {
		font-weight: bolder;
		font-style: italic;
		color: black;
	}
	.club-lookup-content {
		text-align: left;
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
	.comments-wrapper {
		overflow: auto;
	}
</style>