<template>
	<v-dialog
		v-model="displayModal"
		max-width="800px"
	>
		<v-card>
			<v-card-title class="d-flex justify-center">
				<span class="text-h5">Добавить решение</span>
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
							<v-text-field
								label="Имя решения*"
								required
								:rules="commonRules"
								hide-details="auto"
								v-model="submission.title"
							></v-text-field>
						</v-col>
						<v-col cols="12">
							<tiptap-vuetify
								class="text-left"
								v-model="submission.textContent"
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
					@click="send"
					:disabled="!formValid"
				>
					Добавить решение
				</v-btn>
			</v-card-actions>
		</v-card>
	</v-dialog>
</template>

<script>
import {
	TiptapVuetify,
	Bold,
	Italic,
	Strike,
	Underline,
	BulletList,
	OrderedList,
	ListItem,
	Link,
	HardBreak,
	History
} from "tiptap-vuetify"
export default {
	name: "CreateTaskSubmissionModal",
	props: ['taskId', 'show'],
	components: {TiptapVuetify},
	data() {
		return {
			formValid: false,
			files: [],
			errorText: "",
			commonRules: [
				v => !!v || 'Поле является обязательным для заполнения'
			],
			submission: {
				title: null,
				taskId: null,
				textContent: null,
				authorId: null,
				attachments: []
			},
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
		send() {
			this.submission.attachments = this.files.map(item => item.file);
			if (this.submission.attachments.length === 0 && !this.submission.textContent) {
				this.$notify({
					group: 'admin-actions',
					title: 'Ошибка',
					text: 'Комментарий должен содержать хотя бы текстовый контент или прикрепленные файлы',
					type: 'error'
				})
				return;
			}
			this.submission.authorId = this.$oidc.currentUserId;
			this.submission.taskId = this.taskId;
			this.$loading(true);
			this.$store.dispatch('addTaskSubmission', this.submission)
				.then(() => {
					this.$emit('load');
					this.resetInput();
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
					this.$emit('close');
				})
		},
		fileDeleted(file) {
			let index = this.files.indexOf(file);
			if (index === -1)
				return;
			this.files.splice(index, 1);
		},
		resetInput() {
			this.submission.taskId = null;
			this.submission.textContent = null;
			this.submission.attachments = [];
			this.submission.authorId = null;
			this.files = [];
		},
		setAttemptNumber(attemptNumber) {
			this.submission.title = `Попытка № ${attemptNumber + 1}`
		}
	},
	computed: {
		displayModal: {
			get() {
				return this.show;
			},
			set(value) {
				if (!value) {
					this.$emit('close');
					this.resetInput();
				}
			}
		}
	}
}
</script>

<style scoped>

</style>