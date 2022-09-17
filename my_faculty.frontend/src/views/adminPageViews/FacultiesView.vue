<template>
	<div>
		<h1 class="text-center mt-3">Управление списком факультетов</h1>
		<v-btn
			class="mt-2 mb-3 mx-5"
			fab
			dark
			color="indigo"
			v-ripple
			left
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
					<span class="text-h5">{{updating ? 'Обновить информацию о факультете' : 'Добавить факультет'}}</span>
				</v-card-title>
				<v-card-text>
					<h2 class="text-center red--text">{{errorText}}</h2>
					<v-container>
						<v-form
							lazy-validation
							ref="submitForm"
							v-model="formValid"
						>
							<v-col cols="12">
								<v-text-field
									label="Название факультета*"
									required
									:rules="commonRules"
									hide-details="auto"
									v-model="faculty.faculty_name"
								></v-text-field>
							</v-col>
							<v-col cols="12">
								<v-text-field
									label="Официальный сайт"
									required
									:rules="commonRules"
									hide-details="auto"
									v-model="faculty.official_website"
								></v-text-field>
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
				Список факультетов
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
				:items="this.FACULTIES.faculties"
				:search="search"
				class="text-left"
			>
				<template v-slot:body="{items}">
					<tbody>
					<tr v-for="(item,index) in items" :key="index">
						<td>
							{{item.id}}
						</td>
						<td>
							{{item.facultyName}}
						</td>
						<td>
							{{item.officialWebsite}}
						</td>
						<td>
							<v-btn
								class="mx-2"
								fab
								small
								color="yellow"
								@click="startUpdatingFaculty(item.id)"
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
								@click="deleteFaculty(item.id)"
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
import {mapGetters} from "vuex"
export default {
	name: "FacultiesView",
	data() {
		return {
			showAddingForm: false,
			search: '',
			formValid: true,
			updating: false,
			errorText: "",
			faculty: {
				id: null,
				faculty_name: "",
				official_website: ""
			},
			commonRules: [
				v => !!v || 'Поле является обязательным для заполнения'
			],
			headers: [
				{
					text: 'id',
					align: 'start',
					value: 'id',
				},
				{ text: 'Название факультета', value: 'facultyName' },
				{ text: 'Официальный сайт', value: 'officialWebsite'},
				{ text: 'Действия', value: 'actions', sortable: false }
			],
		}
	},
	mounted() {
		this.$store.dispatch('loadAllFaculties');
	},
	methods: {
		openAddingForm() {
			this.errorText = "";
			this.resetFaculty();
			this.updating = false;
			this.showAddingForm = true;
		},
		sendData() {
			this.formValid = this.$refs.submitForm.validate();
			if(!this.formValid)
				return;
			this.errorText = "";
			if(!this.updating) {
				this.$store.dispatch('addFaculty', this.faculty)
					.then(() => {
						this.resetFaculty();
						this.showAddingForm = false;
						this.$store.dispatch('loadAllFaculties');
					})
					.catch(() => {
						this.errorText = "Произошла ошибка добавления записи.";
					});
			}
			else {
				this.$store.dispatch('updateFaculty', this.faculty)
					.then(() => {
						this.resetFaculty();
						this.showAddingForm = false;
						this.$store.dispatch('loadAllFaculties');
					})
					.catch(() => {
						this.errorText = "Произошла ошибка обновления записи.";
					});
			}
		},
		resetFaculty() {
			this.faculty.faculty_name = "";
			this.faculty.official_website = "";
			this.faculty.id = null;
		},
		deleteFaculty(id) {
			if (confirm("Вы действительно хотите удалить данную запись")) {
				this.$store.dispatch('deleteFaculty', id)
					.then(() => {
						this.resetFaculty();
						this.$store.dispatch('loadAllFaculties');
					})
					.catch(() => {
						this.resetFaculty();
						alert("Невозможно удалить запись");
					})
			}
		},
		startUpdatingFaculty(id) {
			this.errorText = "";
			this.faculty.id = id;
			this.$store.dispatch('loadFacultyById', id)
				.then((response) => {
					this.faculty.id = response.data.id;
					this.faculty.faculty_name = response.data.facultyName;
					this.faculty.official_website = response.data.officialWebsite;
					this.updating = true;
					this.showAddingForm = true;
				})
		}
	},
	computed: {
		...mapGetters(['FACULTIES'])
	}
}
</script>

<style scoped>

</style>