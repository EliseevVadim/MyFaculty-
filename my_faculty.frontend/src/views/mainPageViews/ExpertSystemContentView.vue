<template>
	<div class="full-view-element">
		<div class="text-center">
			<v-dialog
				v-model="showModal"
				width="500"
			>
				<v-card>
					<v-toolbar
						color="primary"
						dark
						class="modal-title"
					>
						Справка
					</v-toolbar>
					<v-card-text class="modal-text">
						{{STATES.states === undefined || STATES.states[currentState] === undefined ? "" : STATES.states[currentState].explanation }}
					</v-card-text>
					<v-divider></v-divider>
					<v-card-actions>
						<v-spacer></v-spacer>
						<v-btn
							color="primary"
							text
							@click="showModal = false"
						>
							Закрыть
						</v-btn>
					</v-card-actions>
				</v-card>
			</v-dialog>
		</div>
		<div class="full-view-element" v-if="stateTransitionsAreLoaded && statesAreLoaded">
			<v-container fluid class="full-view-element d-flex flex-column">
				<h1>Выбирайте тот вариант ответа, который считаете наиболее подходящим</h1>
				<p class="question">
					<b>{{testWasFinished ? 'Результат' : 'Вопрос'}}:</b>
					{{STATES.states[currentState].question}}
				</p>
				<div class="answers">
					<v-radio-group v-if="!testWasFinished" v-model="selectedAnswer">
						<v-radio v-for="answer in STATES.states[currentState].answers"
								 :key="answer.id"
								 :value="answer.id"
								 v-ripple
								 class="answer"
						>
							<template v-slot:label>
								<div class="answer-text">
									{{answer.text}}
								</div>
							</template>
						</v-radio>
					</v-radio-group>
				</div>
				<v-row class="d-flex justify-center actions">
					<v-col
						class="col-12 col-sm-4 col-md-3"
					>
						<v-btn
							color="lime accent-4"
							class="action-button"
							@click="restart"
						>
							Начать сначала
						</v-btn>
					</v-col>
					<v-col
						class="col-12 col-sm-4 col-md-3"
					>
						<v-btn
							:disabled="passedStates.length <= 1"
							color="purple accent-1"
							@click="goToPreviousQuestion"
							class="action-button"
						>
							Назад
						</v-btn>
					</v-col>
					<v-col
						class="col-12 col-sm-4 col-md-3"
					>
						<v-btn
							:disabled="selectedAnswer === null"
							color="purple accent-1"
							@click="answerTheQuestion"
							class="action-button"
						>
							Далее
						</v-btn>
					</v-col>
					<v-col
						class="col-12 col-sm-4 col-md-3"
					>
						<v-btn
							color="blue"
							class="action-button"
							@click="showModal = true"
						>
							Справка
						</v-btn>
					</v-col>
				</v-row>
			</v-container>
		</div>
		<div v-else>
			<h1>Куда поступать?</h1>
			<p class="info-text">
				Данная рекомендательная система поможет еще не определившимся абитуриентам с выбором наиболее
				подоходящего согласно их предпочтениям факультета ДонНУ посредством небольшого теста. Прохождение теста
				займет менее 10 минут, но по результату абитуриент если и не сможет окончательно решиться куда поступать,
				то хотя бы получит более полную информацию как о факультетах, так и об университете в целом. Для более
				подробной информации о задаваемых вопросах и о предпосылках принятия системой тех или иных решений
				рекомендуется нажимать на кнопку "Справка" если не для каждого, то хотя бы для вопросов, представляющих
				ключевое значение.
			</p>
			<v-btn
				class="mb-9 text-center"
				color="success"
				dark
				@click="loadAllContents">
				Начать тест
			</v-btn>
		</div>
	</div>
</template>

<script>
import {mapGetters} from "vuex";
export default {
	name: "ExpertSystemContentView",
	data() {
		return {
			showModal: false,
			statesAreLoaded: false,
			stateTransitionsAreLoaded: false,
			currentState: null,
			selectedAnswer: null,
			passedStates: [0],
			testWasFinished: false
		}
	},
	methods: {
		loadAllContents() {
			this.$loading(true);
			this.$store.dispatch('loadAllStates')
				.then(() => {
					this.$loading(false);
					this.statesAreLoaded = true;
				});
			this.$loading(true);
			this.$store.dispatch('loadAllStateTransitions')
				.then(() => {
					this.$loading(false);
					this.stateTransitionsAreLoaded = true;
					this.currentState = 0;
				});
		},
		answerTheQuestion() {
			let state = this.STATE_TRANSITIONS.stateTransitions
				.find(element => element.answerId === this.selectedAnswer);
			this.testWasFinished = state.isLast;
			this.currentState = state.finalStateId - 1;
			this.passedStates.push(this.currentState);
			this.selectedAnswer = null;
		},
		goToPreviousQuestion() {
			this.testWasFinished = false;
			this.passedStates.pop();
			this.currentState = this.passedStates[this.passedStates.length - 1];
		},
		restart() {
			this.testWasFinished = false;
			this.passedStates = [0];
			this.currentState = 0;
		}
	},
	computed: {
		...mapGetters(['STATES']),
		...mapGetters(['STATE_TRANSITIONS'])
	}
}
</script>

<style scoped lang="scss">
	.full-view-element {
		height: 100%;
	}
	.info-text {
		padding: 20px;
	}
	.question {
		text-align: left;
		padding: 10px 20px 0 20px;
		font-size: 26px;
		b {
			color: red;
		}
	}
	.answers {
		flex: 1 0 auto;
		.answer {
			padding: 20px;
			.answer-text {
				color: black;
				font-weight: bolder;
			}
		}
	}
	.modal-title {
		display: flex;
		justify-content: center;
		font-weight: bolder;
		font-size: 26px;
	}
	.modal-text {
		text-align: left;
		color: black !important;
		padding: 20px !important;
		font-size: 16px;
		font-weight: bolder;
	}
	.actions {
		flex: 0 0 auto;
	}
	.action-button {
		font-weight: bolder;
	}
</style>