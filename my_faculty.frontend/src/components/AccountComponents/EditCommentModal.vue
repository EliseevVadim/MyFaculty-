<template>
	<v-dialog
		v-model="displayModal"
		max-width="800px"
	>
		<v-card>
			<v-card-title class="d-flex justify-center">
				<span class="text-h5">Редактировать комментарий</span>
			</v-card-title>
			<v-card-text class="pb-0">
				<h2 class="text-center red--text">{{errorText}}</h2>
				<v-container>
					<v-form
						lazy-validation
						ref="submitForm"
						v-model="formValid"
					>
						<v-col cols="12">
							<tiptap-vuetify
								class="text-left"
								v-model="editingComment.textContent"
								placeholder="Введите текстовое содержимое..."
								:extensions="extensions"
								:toolbar-attributes="{ color: 'primary', dark: true }"
							/>
						</v-col>
						<v-col cols="12">
							<VueFileAgent
								v-model="files"
								resumable="true"
								deletable="true"
								help-text="Выберите файлы или перенесите их (максимум 10)"
								error-text="Произошла ошибка"
								max-files="10"
								@beforedelete="fileDeleted($event)"
							/>
						</v-col>
						<v-col cols="12" v-if="commentsToReply">
							<v-select
								clearable
								:items="commentsToReply"
								:item-text="prettifyReplyVariant"
								item-value="id"
								label="Выберите комментарий, на который хотите ответить"
								v-model="editingComment.parentCommentId"
							>
							</v-select>
						</v-col>
					</v-form>
				</v-container>
			</v-card-text>
			<v-card-actions
				class="flex-column flex-sm-row"
			>
				<v-btn
					color="red darken-1"
					text
					@click="displayModal = false"
					class="ma-2 ma-sm-0"
				>
					Закрыть
				</v-btn>
				<v-spacer></v-spacer>
				<v-btn
					color="success"
					@click="editComment"
					:disabled="!formValid"
				>
					Редактировать комментарий
				</v-btn>
			</v-card-actions>
		</v-card>
	</v-dialog>
</template>

<script>
import {
	TiptapVuetify,
	Heading,
	Bold,
	Italic,
	Strike,
	Underline,
	Code,
	BulletList,
	OrderedList,
	ListItem,
	Link,
	Blockquote,
	HardBreak,
	History
} from "tiptap-vuetify"
export default {
	name: "EditCommentModal",
	components: {TiptapVuetify},
	props: ['show', 'comment'],
	data() {
		return {
			formValid: false,
			errorText: "",
			files: [],
			oldAttachments: '',
			editingComment: {
				textContent: null,
				attachments: [],
				postAttachmentsUid: '',
				authorId: null,
				parentCommentId: '',
				postId: null
			},
			commentsToReply: null,
			extensions: [
				History,
				Link,
				Underline,
				Strike,
				Italic,
				ListItem,
				BulletList,
				OrderedList,
				Bold,
				Link,
				HardBreak
			]
		}
	},
	methods: {
		close() {
			this.$emit('close');
		},
		prettifyReplyVariant(comment) {
			let commentText = comment.textContent ? new DOMParser()
				.parseFromString(comment.textContent, "text/html")
				.body.textContent : "*Медиа контент*";
			return `${comment.author.firstName} ${comment.author.lastName}: ${commentText}`;
		},
		processAttachments() {
			let attachments = JSON.parse(this.comment.attachments);
			if (!attachments)
				return;
			this.files = attachments
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
		fileDeleted(file) {
			let index = this.files.indexOf(file);
			if (index === -1)
				return;
			this.files.splice(index, 1);
		},
		loadComment(allComments) {
			this.commentsToReply = allComments.filter(item => item.created < this.comment.created);
			this.editingComment = JSON.parse(JSON.stringify(this.comment));
			this.oldAttachments = this.comment.attachments;
			this.processAttachments();
		},
		editComment() {
			if (!this.editingComment.attachments && !this.editingComment.textContent) {
				this.$notify({
					group: 'admin-actions',
					title: 'Ошибка',
					text: 'Комментарий должен содержать хотя бы текстовый контент или прикрепленные файлы',
					type: 'error'
				})
				return;
			}
			this.editingComment.oldAttachments = this.oldAttachments;
			this.editingComment.attachments = JSON.stringify(this.files
				.filter(element => !(element.file instanceof File))
				.map(element => {
					return element.file !== undefined ? {
						FileName: element.file.name,
						Extension: element.ext,
						Length: element.file.size,
						ContentType: element.type,
						Path: element.url()
					} : {
						FileName: element.name(),
						Extension: element.ext,
						Length: element.size,
						ContentType: element.type,
						Path: element.url
					}
				}));
			this.editingComment.newFiles = this.files
				.map(element => element.file)
				.filter(element => element instanceof File);
			this.editingComment.issuerId = this.$oidc.currentUserId;
			this.$loading(true);
			this.$store.dispatch('updateComment', this.editingComment)
				.then(() => {
					this.reloadCommentsWithMessage('Комментарий успешно обновлен');
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
		},
		reloadCommentsWithMessage(message) {
			this.$emit('load');
			this.$notify({
				group: 'admin-actions',
				title: 'Успешная операция',
				text: message,
				type: 'success'
			});
			this.close();
		},
	},
	computed: {
		displayModal: {
			get() {
				return this.show;
			},
			set(value) {
				if (!value)
					this.$emit('close');
			}
		}
	}
}
</script>

<style scoped>

</style>