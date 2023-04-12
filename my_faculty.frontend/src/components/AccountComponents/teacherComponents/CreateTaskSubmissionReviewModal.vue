<template>
	<v-dialog
		v-model="displayModal"
		max-width="800px"
	>
		<v-card>
			<v-card-title class="d-flex justify-center">
				<span class="text-h5">Добавить отзыв на решение</span>
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
								v-model="review.textContent"
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
							<v-text-field
								type="number"
								:rules="rateRules"
								:label="`Укажите оценку (максимум ${maxRate})*`"
								v-model.number="review.rate"
							>
							</v-text-field>
						</v-col>
						<v-col cols="12">
							<v-select
								:items="submissionStatuses"
								:rules="selectRules"
								item-text="text"
								item-value="value"
								label="Выберите итоговый статус решения*"
								v-model="review.newStatus"
								required
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
					@click="send"
					:disabled="!formValid"
				>
					Добавить отзыв
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
	name: "CreateTaskSubmissionReviewModal",
	components: {TiptapVuetify},
	props: ['submissionId', 'show', 'maxRate'],
	data() {
		return {
			formValid: false,
			files: [],
			errorText: "",
			selectRules: [
				v => this.submissionStatuses
					.map(status => status.value)
					.indexOf(v) >= 0 || 'Поле является обязательным для заполнения'
			],
			rateRules: [
				v => v !== null || "Поле является обязательным для заполнения",
				v => v >= 0 || "Оценка должна быть не меньше 0",
				v => v <= this.maxRate || `Оценка должна быть не больше ${this.maxRate}`,
			],
			review: {
				submissionId: null,
				reviewerId: null,
				textContent: null,
				attachments: [],
				rate: null,
				newStatus: 2
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
			],
			submissionStatuses: [
				{
					text: "Оценено",
					value: 2
				},
				{
					text: "Возвращено для доработки",
					value: 3
				}
			]
		}
	},
	methods: {
		send() {
			this.formValid = this.$refs.submitForm.validate();
			if (!this.formValid)
				return;
			this.review.attachments = this.files.map(item => item.file);
			this.review.reviewerId = this.$oidc.currentUserId;
			this.review.submissionId = this.submissionId;
			this.$store.dispatch('addTaskSubmissionReview', this.review)
				.then(() => {
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
					this.resetInput();
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
			this.review.submissionId = null;
			this.review.textContent = null;
			this.review.attachments = [];
			this.review.reviewerId = null;
			this.review.newStatus = 2;
			this.review.rate = null;
			this.files = [];
		}
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