<template>
	<div>
		<h1 class="text-center mt-3">Управление списком преподавателей</h1>
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
					<span class="text-h5">{{updating ? 'Обновить информацию о преподавателе' : 'Добавить преподавателя'}}</span>
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
									label="ФИО*"
									required
									:rules="commonRules"
									hide-details="auto"
									v-model="teacher.fio"
								></v-text-field>
							</v-col>
							<v-col cols="12">
								<v-file-input
									:rules="photoRules"
									show-size
									accept="image/png, image/jpeg, image/bmp"
									placeholder="Выберите фото"
									prepend-icon="mdi-camera"
									hide-details="auto"
									label="Фото"
									v-model="teacher.photo"
								></v-file-input>
							</v-col>
							<v-col cols="12">
								<v-text-field
									label="Email*"
									:rules="emailRules"
									required
									v-model="teacher.email"
								></v-text-field>
							</v-col>
							<v-col cols="12" class="text-center">
								<h3 class="black--text mb-3">Укажите дату рождения преподавателя</h3>
								<v-date-picker
									v-model="teacher.birth_date"
									required
								>
								</v-date-picker>
							</v-col>
							<v-col
								cols="12"
							>
								<v-select
									:items="this.RANKS.scienceRanks"
									item-text="rankName"
									item-value="id"
									:rules="commonRules"
									label="Выберите ученую степень*"
									v-model="teacher.science_rank_id"
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
				Список преподавателей
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
				:items="this.TEACHERS.teachers"
				:search="search"
				class="text-left"
			>
				<template v-slot:body="{items}">
					<tbody>
					<tr v-for="(item,index) in items" :key="index">
						<td>
							<img width="100" height="100" :src="item.photoPath === null || item.photoPath === '' ? '../img/blank-item.jpg' : item.photoPath" alt="#"/>
						</td>
						<td>
							{{item.fio}}
						</td>
						<td>
							{{item.scienceRankName}}
						</td>
						<td>
							{{item.birthDate.slice(0, 10)}}
						</td>
						<td>
							{{item.email}}
						</td>
						<td>
							<v-btn
								class="mx-2"
								fab
								small
								color="yellow"
								@click="startUpdatingTeacher(item.id)"
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
								@click="deleteTeacher(item.id)"
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
    name: "TeachersView",
	data () {
		return {
			showAddingForm: false,
			search: '',
			formValid: true,
			updating: false,
			errorText: "",
			teacher: {
				fio: "",
				birth_date: new Date().toISOString().slice(0, 10),
				email: "",
				science_rank_id: null,
				photo: null
			},
			photoRules: [
				value => !value || value.size < 2000000 || 'Avatar size should be less than 2 MB!'
			],
			emailRules: [
				v => !!v || 'Поле является обязательным для заполнения',
				v => /.+@.+/.test(v) || 'Введенный email некорректен'
			],
			commonRules: [
				v => !!v || 'Поле является обязательным для заполнения'
			],
			headers: [
				{
					text: 'Фото',
					align: 'start',
					sortable: false,
					value: 'photoPath',
				},
				{ text: 'ФИО', value: 'fio' },
				{ text: 'Ученая степень', value: 'scienceRankName' },
				{ text: 'Дата рождения', value: 'birthDate' },
				{ text: 'Email', value: 'email' },
				{ text: 'Действия', value: 'actions', sortable: false }
			],
		}
	},
	mounted() {
		this.$store.dispatch('loadAllRanks');
		this.$store.dispatch('loadAllTeachers');
	},
	methods: {
		openAddingForm() {
			this.errorText = "";
			this.resetTeacher();
			this.updating = false;
			this.showAddingForm = true;
		},
		sendData() {
			this.formValid = this.$refs.submitForm.validate();
			if(!this.formValid)
				return;
			this.errorText = "";
			if(!this.updating) {
				this.$store.dispatch('addTeacher', this.teacher)
					.then(() => {
						this.resetTeacher();
						this.showAddingForm = false;
						this.$store.dispatch('loadAllTeachers');
					})
					.catch(() => {
						this.errorText = "Произошла ошибка добавления записи. Возможно, введенный email уже использован.";
					});
			}
			else {
				this.$store.dispatch('updateTeacher', this.teacher)
					.then(() => {
						this.resetTeacher();
						this.showAddingForm = false;
						this.$store.dispatch('loadAllTeachers');
					})
					.catch(() => {
						this.errorText = "Произошла ошибка обновления записи. Возможно, введенный email уже использован.";
					});
			}
		},
		resetTeacher() {
			this.teacher.birth_date = new Date().toISOString().slice(0, 10);
			this.teacher.email = "";
			this.teacher.fio = "";
			this.teacher.science_rank_id = null;
			this.teacher.photo = null;
		},
		deleteTeacher(id) {
			if (confirm("Вы действительно хотите удалить данную запись")) {
				this.$store.dispatch('deleteTeacher', id)
					.then(() => {
						this.resetTeacher();
						this.$store.dispatch('loadAllTeachers');
					})
					.catch(() => {
						this.resetTeacher();
						alert("Невозможно удалить запись");
					})
			}
		},
		startUpdatingTeacher(id) {
			this.errorText = "";
			this.teacher.id = id;
			this.$store.dispatch('loadTeacherById', id)
				.then((response) => {
					this.teacher.email = response.data.email;
					this.teacher.birth_date = response.data.birthDate.slice(0, 10);
					this.teacher.fio = response.data.fio;
					this.teacher.science_rank_id = response.data.scienceRankId;
					this.updating = true;
					this.showAddingForm = true;
				})
		}
	},
	computed: {
		...mapGetters(['RANKS']),
		...mapGetters(['TEACHERS'])
	}
}
</script>

<style scoped>

</style>