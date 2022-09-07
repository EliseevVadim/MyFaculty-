<template>
    <div>
		<h1 class="text-center mt-3">Управление списком групп</h1>
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
					<span class="text-h5">{{updating ? 'Обновить информацию о группе' : 'Добавить группу'}}</span>
				</v-card-title>
				<v-card-text>
					<h2 class="text-center red--text">{{errorText}}</h2>
					<v-container>
						<v-form
							lazy-validation
							ref="submitForm"
							v-model="formValid">
							<v-col cols="12">
								<v-autocomplete
									label="Название курса*"
									required
									:rules="commonRules"
									:items="COURSES.courses"
									item-text="courseName"
									item-value="id"
									hide-details="auto"
									v-model="group.course_id"
								></v-autocomplete>
							</v-col>
							<v-col cols="12">
								<v-text-field
									label="Название группы*"
									required
									:rules="commonRules"
									hide-details="auto"
									v-model="group.group_name"
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
				Список групп
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
				:items="this.GROUPS.groups"
				:search="search"
				class="text-left"
			>
				<template v-slot:body="{items}">
					<tbody>
					<tr v-for="(item,index) in items" :key="index">
						<td>
							{{item.groupName}}
						</td>
						<td>
							{{item.courseName}}
						</td>
						<td>
							<v-btn
								class="mx-2"
								fab
								small
								color="yellow"
								@click="startUpdatingGroup(item.id)"
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
								@click="deleteGroup(item.id)"
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
    name: "GroupsView",
	data () {
		return {
			showAddingForm: false,
			search: '',
			formValid: true,
			updating: false,
			errorText: "",
			group: {
				course_id: null,
				group_name: ""
			},
			commonRules: [
				v => !!v || 'Поле является обязательным для заполнения'
			],
			headers: [
				{
					text: 'Название группы',
					align: 'start',
					value: 'groupName',
				},
				{ text: 'Название курса', value: 'courseName' },
				{ text: 'Действия', value: 'actions', sortable: false }
			],
		}
	},
	mounted() {
		this.$store.dispatch('loadAllCourses');
		this.$store.dispatch('loadAllGroups');
	},
	methods: {
		openAddingForm() {
			this.errorText = "";
			this.resetGroup();
			this.updating = false;
			this.showAddingForm = true;
		},
		sendData() {
			this.formValid = this.$refs.submitForm.validate();
			if(!this.formValid)
				return;
			this.errorText = "";
			if(!this.updating) {
				this.$store.dispatch('addGroup', this.group)
					.then(() => {
						this.resetGroup();
						this.showAddingForm = false;
						this.$store.dispatch('loadAllGroups');
					})
					.catch(() => {
						this.errorText = "Произошла ошибка добавления записи.";
					});
			}
			else {
				this.$store.dispatch('updateGroup', this.group)
					.then(() => {
						this.resetGroup();
						this.showAddingForm = false;
						this.$store.dispatch('loadAllGroups');
					})
					.catch(() => {
						this.errorText = "Произошла ошибка обновления записи.";
					});
			}
		},
		resetGroup() {
			this.group.group_name = "";
			this.group.course_id = null;
		},
		deleteGroup(id) {
			if (confirm("Вы действительно хотите удалить данную запись")) {
				this.$store.dispatch('deleteGroup', id)
					.then(() => {
						this.resetGroup();
						this.$store.dispatch('loadAllGroups');
					})
					.catch(() => {
						this.resetGroup();
						alert("Невозможно удалить запись");
					})
			}
		},
		startUpdatingGroup(id) {
			this.errorText = "";
			this.group.id = id;
			this.$store.dispatch('loadGroupById', id)
				.then((response) => {
					this.group.id = response.data.id;
					this.group.group_name = response.data.groupName;
					this.group.course_id = response.data.courseId;
					this.updating = true;
					this.showAddingForm = true;
				})
		}
	},
	computed: {
		...mapGetters(['COURSES']),
		...mapGetters(['GROUPS'])
	}
}
</script>

<style scoped>

</style>