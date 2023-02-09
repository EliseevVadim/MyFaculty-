<template>
	<v-dialog
		v-model="displayModal"
		max-width="800px"
	>
		<v-card>
			<v-card-title class="d-flex justify-center">
				<span class="text-h5">Редактировать задание</span>
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
								v-model="editingTask.textContent"
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
							<v-datetime-picker
								v-model="editingTask.deadLine"
								label="Укажите крайний срок выполнения задания"
								format="DD-MM-YYYY HH:mm"
								clear-text="Очистить"
							></v-datetime-picker>
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
					@click="editTask"
					:disabled="!formValid"
				>
					Редактировать задание
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
	name: "EditClubTaskModal",
	components: {TiptapVuetify},
	props: ['show', 'task'],
	data() {
		return {
			formValid: false,
			errorText: "",
			files: [],
			oldAttachments: '',
			editingTask: {
				textContent: null,
				studyClubId: null,
				attachments: [],
				postAttachmentsUid: '',
				authorId: null,
				deadLine: new Date()
			},
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
		editTask() {
			if (!this.editingTask.attachments && !this.editingTask.textContent) {
				this.$notify({
					group: 'admin-actions',
					title: 'Ошибка',
					text: 'Запись должна содержать хотя бы текстовый контент или прикрепленные файлы',
					type: 'error'
				})
				return;
			}
			this.editingTask.oldAttachments = this.oldAttachments;
			this.editingTask.attachments = JSON.stringify(this.files
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
			this.editingTask.newFiles = this.files
				.map(element => element.file)
				.filter(element => element instanceof File);
			this.editingTask.deadLine = new Date(this.editingTask.deadLine).toISOString();
			this.editingTask.issuerId = this.$oidc.currentUserId;
			this.$loading(true);
			this.$store.dispatch('updateClubTask', this.editingTask)
				.then(() => {
					this.reloadTasksWithMessage('Задание успешно обновлено');
				})
				.catch((error) => {
					this.$notify({
						group: 'admin-actions',
						title: 'Ошибка',
						text: error.response.data.error ? error.response.data.error
							: 'Неверный ввод. Возможно, Вы указали дату, предшествующую данному моменту.',
						type: 'error'
					})
				})
				.finally(() => {
					this.$loading(false);
				})
		},
		processAttachments() {
			let attachments = JSON.parse(this.task.attachments);
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
		loadTask() {
			let append = new Date().getTimezoneOffset();
			this.editingTask = JSON.parse(JSON.stringify(this.task));
			this.editingTask.deadLine = new Date(new Date(this.editingTask.deadLine).getTime() - append * 60000);
			this.oldAttachments = this.task.attachments;
			this.processAttachments();
		},
		reloadTasksWithMessage(message) {
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