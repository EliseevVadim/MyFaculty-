<template>
	<div>
		<EditClubTaskModal
			:show="showEditingForm"
			:task="task"
			@close="showEditingForm = false"
			@load="reloadTasks"
			ref="editingForm"
		/>
		<v-container fluid class="task-container mb-3">
			<v-list-item
				class="mb-3"
				:key="task.studyClubName"
			>
				<v-list-item-avatar>
					<v-img
						class="author-avatar"
						:src="task.studyClubImage ? task.studyClubImage : '../img/blank-club.png'"
					/>
				</v-list-item-avatar>
				<v-list-item-content class="club-lookup-content">
					<v-list-item-title>
						<router-link class="author-name" :to="'/clubs/' + task.studyClubId">
							{{task.studyClubName}}
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
			<v-row
				class="ml-3"
				v-html="task.textContent"
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
		</v-container>
	</div>
</template>

<script>
import EditInformationPostModal from "@/components/AccountComponents/EditInformationPostModal";
import EditClubTaskModal from "@/components/AccountComponents/EditClubTaskModal";

export default {
	name: "ClubTaskPresenter",
	components: {EditClubTaskModal, EditInformationPostModal},
	props: ['task'],
	data() {
		return {
			showEditingForm: false,
			images: [],
			otherFiles: [],
			criticalHoursToDeadLine: 24
		}
	},
	methods: {
		prettifyPublishDate() {
			let creationDate = new Date(this.task.created).toLocaleString('ru-RU', {
				year: 'numeric',
				month: 'long',
				day: 'numeric',
				hour: 'numeric',
				minute: 'numeric'
			});
			if (!this.task.updated)
				return creationDate;
			let updateDate = new Date(this.task.updated).toLocaleString('ru-RU', {
				year: 'numeric',
				month: 'long',
				day: 'numeric',
				hour: 'numeric',
				minute: 'numeric'
			});
			return `${creationDate} <wbr>(обновлено ${updateDate})`;
		},
		prettifyDeadLine() {
			return new Date(new Date(this.task.deadLine).getTime() - new Date().getTimezoneOffset() * 60000)
					.toLocaleString('ru-RU', {
						year: 'numeric',
						month: 'long',
						day: 'numeric',
						hour: 'numeric',
						minute: 'numeric'
					});
		},
		processAttachments() {
			let attachments = JSON.parse(this.task.attachments);
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
			return this.task.authorId == this.$oidc.currentUserId;
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
		reloadTasks() {
			this.$emit('load');
		},
		isDeadLineClose() {
			let now = new Date();
			let deadLine = new Date(new Date(this.task.deadLine).getTime() - new Date().getTimezoneOffset() * 60000);
			let difference = (deadLine - now) / 36e5;
			return difference < this.criticalHoursToDeadLine;
		}
	},
	mounted() {
		this.processAttachments();
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
		/*img:hover {
			z-index: 1000;
			transform: scale(1.3);
			transition: transform ease 0.5s;
		}*/
	}
	.task-context-menu-option {
		text-align: left;
		cursor: pointer;
	}
	.modal-invoker {
		cursor: pointer;
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