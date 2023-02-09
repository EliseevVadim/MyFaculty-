<template>
	<v-dialog
		v-model="displayModal"
		max-width="800px"
	>
		<v-card>
			<v-card-title class="d-flex justify-center">
				<span class="text-h5">Новая запись</span>
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
								v-model="infoPost.textContent"
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
						<v-col cols="12">
							<v-checkbox
								v-model="infoPost.commentsAllowed"
								required
								label="Разрешить комментарии"
							>
							</v-checkbox>
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
					@click="createPost"
					:disabled="!formValid"
				>
					Опубликовать запись
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
	name: "CreateInformationPostModal",
	props: ['show', 'publicId', 'studyClubId'],
	components: {TiptapVuetify},
	data() {
		return {
			formValid: false,
			errorText: "",
			files: [],
			infoPost: {
				textContent: null,
				infoPublicId: null,
				studyClubId: null,
				attachments: [],
				authorId: null,
				commentsAllowed: true
			},
			photoRules: [
				value => !value || value.size < 2000000 || 'Размер фото превышает 2 МБ!'
			],
			commonRules: [
				v => !!v || 'Поле является обязательным для заполнения'
			],
			extensions: [
				History,
				Blockquote,
				Link,
				Underline,
				Strike,
				Italic,
				ListItem,
				BulletList,
				OrderedList,
				[
					Heading,
					{
						options: {
							levels: [1, 2, 3]
						}
					}
				],
				Bold,
				Link,
				Code,
				HardBreak
			]
		}
	},
	methods: {
		close() {
			this.$emit('close');
		},
		createPost() {
			this.infoPost.attachments = this.files.map(item => item.file);
			if (this.infoPost.attachments.length === 0 && !this.infoPost.textContent) {
				this.$notify({
					group: 'admin-actions',
					title: 'Ошибка',
					text: 'Запись должна содержать хотя бы текстовый контент или прикрепленные файлы',
					type: 'error'
				})
				return;
			}
			this.infoPost.studyClubId = this.studyClubId;
			this.infoPost.infoPublicId = this.publicId;
			this.infoPost.authorId = this.$oidc.currentUserId;
			this.$loading(true);
			this.$store.dispatch('addInfoPost', this.infoPost)
				.then(() => {
					this.reloadPostsWithMessage('Запись успешно опубликована');
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
		reloadPostsWithMessage(message) {
			this.$emit('load');
			this.$notify({
				group: 'admin-actions',
				title: 'Успешная операция',
				text: message,
				type: 'success'
			});
			this.resetInput();
			this.close();
		},
		resetInput() {
			this.infoPost.textContent = null;
			this.infoPost.infoPublicId = null;
			this.infoPost.studyClubId = null;
			this.infoPost.attachments = [];
			this.infoPost.authorId = null;
			this.infoPost.commentsAllowed = true;
			this.files = [];
		},
		fileDeleted(file) {
			let index = this.files.indexOf(file);
			if (index === -1)
				return;
			this.files.splice(index, 1);
		},
	},
	computed: {
		displayModal: {
			get() {
				return this.show;
			},
			set(value) {
				if (!value) {
					this.resetInput();
					this.$emit('close');
				}
			}
		}
	}
}
</script>

<style scoped>

</style>