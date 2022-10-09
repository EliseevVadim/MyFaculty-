<template>
	<div>
		<h1 class="text-center mt-3">Управление списком пар</h1>
		<v-btn
			class="mt-2 mb-3 mx-5"
			fab
			dark
			color="indigo"
			v-ripple
			@click="openAddingForm"
		>
			<v-icon dark>
				mdi-plus
			</v-icon>
		</v-btn>
		<v-dialog
			v-model="showAddingForm"
			persistent
			max-width="600px"
		>
			<v-card>
				<v-card-title>
					<span class="text-h5">{{updating ? 'Обновить информацию о паре' : 'Добавить пару'}}</span>
				</v-card-title>
				<v-card-text>
					<h2 class="text-center red--text">{{errorText}}</h2>
					<v-container>
						<v-form
							lazy-validation
							ref="submitForm"
							v-model="formValid">
							<v-col cols="12">
								<v-text-field
									label="Название пары*"
									required
									:rules="commonRules"
									hide-details="auto"
									v-model="pair.pair_name"
								></v-text-field>
							</v-col>
							<v-col cols="12">
								<v-select
									:items="this.PAIR_INFOS.pairInfos"
									item-text="pairNumber"
									item-value="id"
									:rules="commonRules"
									label="Выберите номер пары*"
									v-model="pair.pair_info_id"
								></v-select>
							</v-col>
							<v-col
								cols="12"
							>
								<v-select
									:items="this.FACULTIES.faculties"
									item-text="facultyName"
									item-value="id"
									:rules="commonRules"
									label="Выберите факультет для группы*"
									@change="loadGroupsList"
									v-model="selectedFacultyForGroupsId"
								></v-select>
							</v-col>
							<v-col
								v-if="groupsAreLoaded"
								cols="12"
							>
								<v-autocomplete
									label="Группа*"
									required
									:rules="commonRules"
									:items="this.GROUPS.groups"
									:item-text="getFullGroupName"
									item-value="id"
									hide-details="auto"
									v-model="pair.group_id"
								></v-autocomplete>
							</v-col>
							<h1 v-else-if="groupsAreLoaded === false" class="red--text">
								Для данного факультета информация о группах отсутствует.
								Дальнейшее заполнение невозможно.
							</h1>
							<v-col
								cols="12"
							>
								<v-select
									:items="this.FACULTIES.faculties"
									item-text="facultyName"
									item-value="id"
									:rules="commonRules"
									label="Выберите факультет для аудитории*"
									@change="loadAuditoriumsList"
									v-model="selectedFacultyForAuditoriumsId"
								></v-select>
							</v-col>
							<v-col
								v-if="auditoriumsAreLoaded"
								cols="12"
							>
								<v-autocomplete
									label="Аудитория*"
									required
									:rules="commonRules"
									:items="AUDITORIUMS.auditoriums"
									item-text="auditoriumName"
									item-value="id"
									hide-details="auto"
									v-model="pair.auditorium_id"
								></v-autocomplete>
							</v-col>
							<h1 v-else-if="auditoriumsAreLoaded === false" class="red--text">
								Для данного факультета информация об аудиториях отсутствует.
								Дальнейшее заполнение невозможно.
							</h1>
							<v-col cols="12">
								<v-autocomplete
									label="ФИО преподавателя*"
									required
									:rules="commonRules"
									:items="TEACHERS.teachers"
									item-text="fio"
									item-value="id"
									hide-details="auto"
									v-model="pair.teacher_id"
								></v-autocomplete>
							</v-col>
							<v-col cols="12">
								<v-autocomplete
									label="Дисциплина*"
									required
									:rules="commonRules"
									:items="DISCIPLINES.disciplines"
									item-text="disciplineName"
									item-value="id"
									hide-details="auto"
									v-model="pair.discipline_id"
								></v-autocomplete>
							</v-col>
							<v-col cols="12">
								<v-autocomplete
									label="День недели*"
									required
									:rules="commonRules"
									:items="this.DAYS_OF_WEEK.workDaysOfWeek"
									item-text="daysName"
									item-value="id"
									hide-details="auto"
									v-model="pair.day_of_week_id"
								></v-autocomplete>
							</v-col>
							<v-col cols="12">
								<v-select
									:items="this.PAIR_REPEATINGS.pairRepeatings"
									item-text="repeatingName"
									item-value="id"
									:rules="commonRules"
									label="Выберите повторяемость пары*"
									v-model="pair.repeating_id"
								></v-select>
							</v-col>
						</v-form>
					</v-container>
					<small>Поля, помеченные * обязательны к заполнению</small>
				</v-card-text>
				<v-card-actions>
					<v-spacer></v-spacer>
					<v-btn
						color="blue darken-1"
						text
						@click="showAddingForm = false"
					>
						Закрыть
					</v-btn>
					<v-btn
						color="blue darken-1"
						text
						@click="sendData"
						:disabled="!formValid"
					>
						Сохранить
					</v-btn>
				</v-card-actions>
			</v-card>
		</v-dialog>
		<v-card>
			<v-card-title>
				Список пар
				<v-spacer></v-spacer>
				<v-text-field
					v-model="search"
					append-icon="mdi-magnify"
					label="Поиск"
					single-line
					hide-details
				></v-text-field>
			</v-card-title>
			<v-data-table
				:headers="headers"
				:items="this.PAIRS.pairs"
				:search="search"
				class="text-left"
			>
				<template v-slot:body="{items}">
					<tbody>
					<tr v-for="(item,index) in items" :key="index">
						<td>
							{{item.pairInfo.pairNumber}}
						</td>
						<td>
							{{item.pairName}}
						</td>
						<td>
							{{item.pairInfo.startTime}}
						</td>
						<td>
							{{item.pairInfo.endTime}}
						</td>
						<td>
							{{item.group.facultyName}}
						</td>
						<td>
							{{item.group.groupName}}
						</td>
						<td>
							{{item.group.courseName}}
						</td>
						<td>
							{{item.auditorium.facultyName}}
						</td>
						<td>
							{{item.auditorium.auditoriumName}}
						</td>
						<td>
							{{item.teachersFIO}}
						</td>
						<td>
							{{item.dayOfWeek}}
						</td>
						<td>
							{{item.pairRepeatingName}}
						</td>
						<td>
							<v-btn
								class="mx-2"
								fab
								small
								color="yellow"
								@click="startUpdatingPair(item.id)"
							>
								<v-icon dark>
									mdi-pencil
								</v-icon>
							</v-btn>
							<v-btn
								class="mx-2"
								fab
								small
								dark
								color="red"
								@click="deletePair(item.id)"
							>
								<v-icon dark>
									mdi-delete
								</v-icon>
							</v-btn>
						</td>
					</tr>
					</tbody>
				</template>
			</v-data-table>
		</v-card>
	</div>
</template>

<script>
import {mapGetters} from "vuex";
export default {
    name: "PairsView",
	data () {
		return {
			selectedFacultyForGroupsId: null,
			groupsAreLoaded: null,
			selectedFacultyForAuditoriumsId: null,
			auditoriumsAreLoaded: null,
			showAddingForm: false,
			search: '',
			formValid: true,
			updating: false,
			errorText: "",
			pair: {
				id: null,
				pair_name: "",
				pair_info_id: null,
				teacher_id: null,
				auditorium_id: null,
				discipline_id: null,
				day_of_week_id: null,
				repeating_id: null,
				group_id: null
			},
			commonRules: [
				v => !!v || 'Поле является обязательным для заполнения'
			],
			headers: [
				{
					text: 'Номер пары',
					align: 'start',
					value: 'pairInfo.pairNumber',
				},
				{ text: 'Название пары', value: 'pairName' },
				{ text: 'Начало пары', value: 'pairInfo.startTime' },
				{ text: 'Конец пары', value: 'pairInfo.endTime' },
				{ text: 'Факультет (группа)', value: 'group.facultyName' },
				{ text: 'Группа', value: 'group.groupName' },
				{ text: 'Курс', value: 'group.courseName' },
				{ text: 'Факультет (аудитория)', value: 'auditorium.facultyName' },
				{ text: 'Аудитория', value: 'auditorium.auditoriumName' },
				{ text: 'Преподаватель', value: 'teachersFIO' },
				{ text: 'День недели', value: 'dayOfWeek' },
				{ text: 'Повторение', value: 'pairRepeatingName' },
				{ text: 'Действия', value: 'actions', sortable: false }
			],
		}
	},
	mounted() {
		this.$store.dispatch('loadAllPairs');
		this.$store.dispatch('loadAllPairInfos');
		this.$store.dispatch('loadAllTeachers');
		this.$store.dispatch('loadAllDaysOfWeek');
		this.$store.dispatch('loadAllPairRepeatings');
		this.$store.dispatch('loadAllDisciplines');
		this.$store.dispatch('loadAllFaculties');
	},
	methods: {
		getFullGroupName(item) {
			return item.groupName + ' ' + '(' + item.courseName + ')';
		},
		openAddingForm() {
			this.errorText = "";
			this.resetPair();
			this.updating = false;
			this.showAddingForm = true;
		},
		loadGroupsList() {
			this.$loading(true);
			this.$store.dispatch('loadGroupsByFacultyId', this.selectedFacultyForGroupsId)
				.then(() => {
					this.$loading(false);
					this.groupsAreLoaded = this.GROUPS.groups.length > 0;
				})
		},
		loadAuditoriumsList() {
			this.$loading(true);
			this.$store.dispatch('loadAuditoriumsByFacultyId', this.selectedFacultyForAuditoriumsId)
				.then(() => {
					this.$loading(false);
					this.auditoriumsAreLoaded = this.AUDITORIUMS.auditoriums.length > 0;
				})
		},
		sendData() {
			this.formValid = this.$refs.submitForm.validate();
			if(!this.formValid)
				return;
			this.errorText = "";
			if(!this.updating) {
				this.$store.dispatch('addPair', this.pair)
					.then(() => {
						this.resetPair();
						this.showAddingForm = false;
						this.$store.dispatch('loadAllPairs');
					})
					.catch(() => {
						this.errorText = "Произошла ошибка добавления записи.";
					});
			}
			else {
				this.$store.dispatch('updatePair', this.pair)
					.then(() => {
						this.resetPair();
						this.showAddingForm = false;
						this.$store.dispatch('loadAllPairs');
					})
					.catch(() => {
						this.errorText = "Произошла ошибка обновления записи.";
					});
			}
		},
		resetPair() {
			this.pair.id = null;
			this.pair.pair_name = "";
			this.pair.pair_info_id = null;
			this.pair.teacher_id = null;
			this.pair.auditorium_id = null;
			this.pair.discipline_id = null;
			this.pair.day_of_week_id = null;
			this.pair.repeating_id = null;
			this.pair.group_id = null;
			this.auditoriumsAreLoaded = null;
			this.groupsAreLoaded = null;
			this.selectedFacultyForAuditoriumsId = null;
			this.selectedFacultyForGroupsId = null;
		},
		deletePair(id) {
			if (confirm("Вы действительно хотите удалить данную запись")) {
				this.$store.dispatch('deletePair', id)
					.then(() => {
						this.resetPair();
						this.$store.dispatch('loadAllPairs');
					})
					.catch(() => {
						this.resetPair();
						alert("Невозможно удалить запись");
					})
			}
		},
		startUpdatingPair(id) {
			this.errorText = "";
			this.pair.id = id;
			this.$store.dispatch('loadPairById', id)
				.then((response) => {
					console.log(response.data);
					this.selectedFacultyForAuditoriumsId = response.data.auditorium.facultyId;
					this.loadAuditoriumsList();
					this.selectedFacultyForGroupsId = response.data.group.facultyId;
					this.loadGroupsList();
					this.pair.id = response.data.id;
					this.pair.pair_name = response.data.pairName;
					this.pair.pair_info_id = response.data.pairInfoId;
					this.pair.teacher_id = response.data.teacherId;
					this.pair.auditorium_id = response.data.auditoriumId;
					this.pair.discipline_id = response.data.disciplineId;
					this.pair.day_of_week_id = response.data.dayOfWeekId;
					this.pair.repeating_id = response.data.pairRepeatingId;
					this.pair.group_id = response.data.groupId;
					this.updating = true;
					this.showAddingForm = true;
				})
		}
	},
	computed: {
		...mapGetters(['DISCIPLINES']),
		...mapGetters(['PAIR_INFOS']),
		...mapGetters(['TEACHERS']),
		...mapGetters(['AUDITORIUMS']),
		...mapGetters(['PAIR_REPEATINGS']),
		...mapGetters(['DAYS_OF_WEEK']),
		...mapGetters(['PAIRS']),
		...mapGetters(['GROUPS']),
		...mapGetters(['FACULTIES'])
	}
}
</script>

<style scoped>

</style>