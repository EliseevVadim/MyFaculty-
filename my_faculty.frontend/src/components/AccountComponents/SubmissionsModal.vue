<template>
	<v-row justify="center">
		<CreateTaskSubmissionModal
			:show="showCreationForm"
			:task-id="taskId"
			@load="loadSubmissions"
			@close="showCreationForm = false"
			ref="creationForm"
		/>
		<v-dialog
			v-model="displayModal"
			fullscreen
			persistent
			transition="dialog-top-transition"
		>
			<div class="submissions-wrapper">
				<v-toolbar
					class="submissions-header"
					color="primary"
				>
					<v-btn
						icon
						dark
						@click="displayModal = false"
					>
						<v-icon>mdi-arrow-left</v-icon>
					</v-btn>
					<v-toolbar-title class="white--text">К заданию</v-toolbar-title>
				</v-toolbar>
				<div class="submissions-list">
					<v-btn
						v-if="canSubmit"
						@click="showCreationForm = true"
						class="mt-2 mb-2 font-weight-bold"
					>
						Добавить новое решение
					</v-btn>
					<TaskSubmissionPresenter
						v-for="taskSubmission in taskSubmissions"
						:key="taskSubmission.id + taskSubmission.updated"
						:submission="taskSubmission"
						:current-user-is-task-moderator="currentUserIsTaskModerator"
						@load="loadSubmissions"
					/>
				</div>
			</div>
		</v-dialog>
	</v-row>
</template>

<script>
import {mapGetters} from "vuex";
import TaskSubmissionPresenter from "@/components/presenters/TaskSubmissionPresenter";
import CreateTaskSubmissionModal from "@/components/AccountComponents/CreateTaskSubmissionModal";

export default {
	name: "SubmissionsModal",
	components: {CreateTaskSubmissionModal, TaskSubmissionPresenter},
	props: ['taskId', 'show', 'currentUserIsTaskModerator'],
	data() {
		return {
			submissionStatuses: {
				sentForEvaluation: 1,
				evaluated: 2,
				sentBack: 3
			},
			taskSubmissions: [],
			showCreationForm: false,
			canSubmit: false
		}
	},
	methods: {
		close() {
			this.$emit('close');
		},
		loadSubmissions() {
			if (this.currentUserIsTaskModerator) {
				this.$store.dispatch('loadTaskSubmissionsByTaskId', this.taskId)
					.then(() => {
						this.taskSubmissions = JSON.parse(JSON.stringify(this.TASK_SUBMISSIONS.taskSubmissions));
						this.canSubmit = false;
					})
				return;
			}
			this.$store.dispatch('loadTaskSubmissionsByTaskIdForCurrentUser', this.taskId)
				.then(() => {
					this.taskSubmissions = JSON.parse(JSON.stringify(this.TASK_SUBMISSIONS.taskSubmissions));
					let attemptNumber = this.taskSubmissions.length;
					this.$refs.creationForm.setAttemptNumber(attemptNumber);
					this.canSubmit = attemptNumber === 0
						|| this.taskSubmissions[0].status === this.submissionStatuses.sentBack;
				})
		}
	},
	mounted() {
		document.documentElement.style.overflowY = "hidden";
		this.loadSubmissions();
	},
	destroyed() {
		document.documentElement.style.removeProperty("overflow-y");
		this.$emit('load');
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
		},
		...mapGetters(['TASK_SUBMISSIONS'])
	}
}
</script>

<style scoped>
	.submissions-wrapper {
		background: white;
		height: 100%;
		position: relative;
		overflow: hidden;
	}
	.submissions-header {
		z-index: 10;
	}
	.submissions-list {
		background: #9fdee3;
		position: absolute;
		top: 56px;
		right: 0;
		left: 0;
		bottom: 0;
		padding: 1rem;
		overflow-y: auto;
	}
	.submissions-list::-webkit-scrollbar {
		width: 10px;
	}
	.submissions-list::-webkit-scrollbar-track {
		background-color: transparent;
		border-radius: 100px;
	}
	.submissions-list::-webkit-scrollbar-thumb {
		background-color: hsla(0, 18%, 3%, 0.2);
		border-radius: 100px;
	}
</style>