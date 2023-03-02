<template>
	<div>
		<EditInformationPostModal
			:show="showEditingForm"
			:post="post"
			@close="showEditingForm = false"
			@load="reloadPosts"
			ref="editingForm"
		/>
		<UsersListModal
			v-if="showLikedUsers"
			:show="showLikedUsers"
			:users="post.likedUsers"
			:context-menu-authorization-checker="() => false"
			:user-has-full-access="false"
			:context-actions="likedUsersActions"
			title="Оценившие пользователи"
			@close="showLikedUsers = false"
		/>
		<CommentsModal
			v-if="showComments"
			:show="showComments"
			:post-id="post.id"
			@close="showComments = false"
			@load="reloadPosts"
		/>
		<v-container
			fluid
			class="post-container mb-3 px-0"
			:class="post.commentsAllowed ? 'pb-0' : ''"
		>
			<div class="internal-post-content">
				<v-list-item
					class="mb-3"
					:key="getFullName(post.author)"
				>
					<v-list-item-avatar>
						<v-img
							class="author-avatar"
							:src="post.author.avatarPath ? post.author.avatarPath : '../img/blank-club.png'"
						/>
					</v-list-item-avatar>
					<v-list-item-content class="post-metadata">
						<v-list-item-title>
							<router-link class="author-name" :to="'/id' + post.authorId">
								{{getFullName(post.author)}}
							</router-link>
							<wbr>
							<TeacherVerificationMark v-if="post.author.isTeacher"/>
						</v-list-item-title>
						<v-list-item-subtitle v-html="prettifyPublishDate()">
						</v-list-item-subtitle>
					</v-list-item-content>
					<v-menu
						v-if="currentUserIsPostAuthor()"
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
									class="post-context-menu-option"
									@click="startPostEditing"
								>
									Редактировать запись
								</v-list-item-title>
							</v-list-item>
							<v-list-item ripple>
								<v-list-item-title
									class="post-context-menu-option"
									@click="deleteInfoPost"
								>
									Удалить запись
								</v-list-item-title>
							</v-list-item>
						</v-list>
					</v-menu>
				</v-list-item>
				<v-row
					class="ml-3"
					v-html="post.textContent"
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
				<v-row>
					<v-col class="d-flex align-center">
						<v-btn
							ref="like"
							icon
							large
							color="primary"
							:loading="likeButtonLoading"
							@mouseover="hoverLikeButton"
							@mouseout="leaveLikeButton"
							@click="pressLikeButton"
						>
							<v-icon>
								{{ displayedLikeButtonIcon }}
							</v-icon>
						</v-btn>
						<span
							title="Посмотреть список оценивших пользователей"
							class="modal-invoker"
							@click="showLikedUsers = true"
						>
						{{ this.post.likedUsers.length !== 0 ? this.post.likedUsers.length : ''}}
					</span>
					</v-col>
				</v-row>
			</div>
			<div v-if="post.commentsAllowed">
				<v-divider></v-divider>
				<v-container
					@click="showComments = true"
					class="comments-invoker d-flex"
					v-ripple
					fluid
				>
					<span>{{prettifyCommentsCount()}}</span>
					<v-spacer></v-spacer>
					<v-icon color="primary">
						mdi-chevron-right
					</v-icon>
				</v-container>
			</div>
		</v-container>
	</div>
</template>

<script>
import EditInformationPostModal from "@/components/AccountComponents/EditInformationPostModal";
import UsersListModal from "@/components/AccountComponents/UsersListModal";
import TeacherVerificationMark from "@/components/AccountComponents/core/verificationMarks/TeacherVerificationMark";
import CommentsModal from "@/components/AccountComponents/CommentsModal";
export default {
	name: "InformationPostPresenter",
	components: {CommentsModal, TeacherVerificationMark, UsersListModal, EditInformationPostModal},
	props: ['post'],
	data() {
		return {
			showEditingForm: false,
			showLikedUsers: false,
			showComments: false,
			images: [],
			otherFiles: [],
			likedUsersActions: [],
			likeButtonLoading: false,
			likedButtonIcon: 'mdi-thumb-up',
			unlikedButtonIcon: 'mdi-thumb-up-outline',
			displayedLikeButtonIcon: ''
		}
	},
	methods: {
		getFullName(user) {
			return user.firstName + " " + user.lastName;
		},
		prettifyPublishDate() {
			let creationDate = new Date(this.post.created).toLocaleString('ru-RU', {
				year: 'numeric',
				month: 'long',
				day: 'numeric',
				hour: 'numeric',
				minute: 'numeric'
			});
			if (!this.post.updated)
				return creationDate;
			let updateDate = new Date(this.post.updated).toLocaleString('ru-RU', {
				year: 'numeric',
				month: 'long',
				day: 'numeric',
				hour: 'numeric',
				minute: 'numeric'
			});
			return `${creationDate} <wbr>(обновлено ${updateDate})`;
		},
		prettifyCommentsCount() {
			if (this.post.commentsCount === 0)
				return "Оставить комментарий...";
			let  count = this.post.commentsCount;
			let lastNumber = count % 100;
			let variants = ['комментарий', 'комментария', 'комментариев'];
			if (lastNumber > 10 && lastNumber < 20)
				return `${count} ${variants[2]}`;
			let lastDigit = lastNumber % 10;
			switch (lastDigit) {
				case 1:
					return `${count} ${variants[0]}`;
				case 2:
				case 3:
				case 4:
					return `${count} ${variants[1]}`;
				default:
					return `${count} ${variants[2]}`;
			}
		},
		currentUserLikesPost() {
			return this.post.likedUsers.find(user => user.id == this.$oidc.currentUserId) !== undefined;
		},
		processAttachments() {
			let attachments = JSON.parse(this.post.attachments);
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
		initializeLikeButton() {
			this.displayedLikeButtonIcon = this.currentUserLikesPost() ? this.likedButtonIcon : this.unlikedButtonIcon;
		},
		hoverLikeButton() {
			if (this.currentUserLikesPost())
				return;
			this.displayedLikeButtonIcon = this.likedButtonIcon;
		},
		leaveLikeButton() {
			if (this.currentUserLikesPost())
				return;
			this.displayedLikeButtonIcon = this.unlikedButtonIcon;
		},
		pressLikeButton() {
			if (!this.currentUserLikesPost())
				this.likePost();
			else
				this.unlikePost();
		},
		likePost() {
			this.likeButtonLoading = true;
			this.$store.dispatch('likeInfoPost', {
				likedUserId: this.$oidc.currentUserId,
				likedPostId: this.post.id
			})
				.then(() => {
					this.$emit('load');
					this.likeButtonLoading = false;
				})
				.catch((error) => {
					this.$notify({
						group: 'admin-actions',
						title: 'Ошибка',
						text: error.response.data.error,
						type: 'error'
					});
				})
		},
		unlikePost() {
			this.likeButtonLoading = true;
			this.$store.dispatch('unlikeInfoPost', {
				likedUserId: this.$oidc.currentUserId,
				likedPostId: this.post.id
			})
				.then(() => {
					this.$emit('load');
					this.likeButtonLoading = false;
				})
				.catch((error) => {
					this.$notify({
						group: 'admin-actions',
						title: 'Ошибка',
						text: error.response.data.error,
						type: 'error'
					});
				})
		},
		currentUserIsPostAuthor() {
			return this.post.authorId == this.$oidc.currentUserId;
		},
		startPostEditing() {
			this.$refs.editingForm.loadPost();
			this.showEditingForm = true;
		},
		deleteInfoPost() {
			this.$confirm('Вы действительно хотите удалить запись? При удалении все вложения будут ' +
				'удалены навсегда.')
				.then((result) => {
					if (result) {
						this.$loading(true);
						this.$store.dispatch('deleteInfoPost', this.post.id)
							.then(() => {
								this.$notify({
									group: 'admin-actions',
									title: 'Успешная операция',
									text: 'Запись успешно удалена',
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
		reloadPosts() {
			this.$emit('load');
		}
	},
	mounted() {
		this.processAttachments();
		this.initializeLikeButton();
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
	.post-container {
		background: white;
		border-radius: 10px;
	}
	.comments-invoker {
		border-bottom-right-radius: 10px;
		border-bottom-left-radius: 10px;
		cursor: pointer;
	}
	.internal-post-content {
		padding-right: 12px;
		padding-left: 12px;
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
	.post-context-menu-option {
		text-align: left;
		cursor: pointer;
	}
	.post-metadata {
		text-align: left;
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