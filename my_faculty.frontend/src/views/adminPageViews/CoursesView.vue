<template>
    <div>
        <h1 class="text-center mt-3">Управление списком курсов</h1>
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
                    <span class="text-h5">{{updating ? 'Обновить информацию о курсе' : 'Добавить курс'}}</span>
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
                                    label="Номер курса*"
                                    required
                                    :rules="numberRules"
                                    hide-details="auto"
                                    v-model="course.course_number"
                                    type="number"
                                ></v-text-field>
                            </v-col>
                            <v-col cols="12">
                                <v-text-field
                                    label="Название курса*"
                                    required
                                    :rules="commonRules"
                                    hide-details="auto"
                                    v-model="course.course_name"
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
                Список курсов
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
                :items="this.COURSES.courses"
                :search="search"
                class="text-left"
            >
                <template v-slot:body="{items}">
                    <tbody>
                    <tr v-for="(item,index) in items" :key="index">
                        <td>
                            {{item.courseNumber}}
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
                                @click="startUpdatingCourse(item.id)"
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
                                @click="deleteCourse(item.id)"
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
    name: "CoursesView",
	data () {
		return {
			showAddingForm: false,
			search: '',
			formValid: true,
			updating: false,
			errorText: "",
			course: {
				course_number: null,
				course_name: ""
			},
			numberRules: [
				v => !!v || 'Поле является обязательным для заполнения',
				v => Number.isInteger(Number(v)) || "Значение должно быть целым числом"
			],
			commonRules: [
				v => !!v || 'Поле является обязательным для заполнения'
			],
			headers: [
				{
					text: 'Номер курса',
					align: 'start',
					value: 'courseNumber',
				},
				{ text: 'Название курса', value: 'courseName' },
				{ text: 'Действия', value: 'actions', sortable: false }
			],
		}
	},
	mounted() {
		this.$store.dispatch('loadAllCourses');
	},
	methods: {
		openAddingForm() {
			this.errorText = "";
			this.resetCourse();
			this.updating = false;
			this.showAddingForm = true;
		},
		sendData() {
			this.formValid = this.$refs.submitForm.validate();
			if(!this.formValid)
				return;
			this.errorText = "";
			if(!this.updating) {
				this.$store.dispatch('addCourse', this.course)
					.then(() => {
						this.resetCourse();
						this.showAddingForm = false;
						this.$store.dispatch('loadAllCourses');
					})
					.catch(() => {
						this.errorText = "Произошла ошибка добавления записи.";
					});
			}
			else {
				this.$store.dispatch('updateCourse', this.course)
					.then(() => {
						this.resetCourse();
						this.showAddingForm = false;
						this.$store.dispatch('loadAllCourses');
					})
					.catch(() => {
						this.errorText = "Произошла ошибка обновления записи.";
					});
			}
		},
		resetCourse() {
			this.course.course_name = "";
			this.course.course_number = null;
		},
		deleteCourse(id) {
			if (confirm("Вы действительно хотите удалить данную запись")) {
				this.$store.dispatch('deleteCourse', id)
					.then(() => {
						this.resetCourse();
						this.$store.dispatch('loadAllCourses');
					})
					.catch(() => {
						this.resetCourse();
						alert("Невозможно удалить запись");
					})
			}
		},
		startUpdatingCourse(id) {
			this.errorText = "";
			this.course.id = id;
			this.$store.dispatch('loadCourseById', id)
				.then((response) => {
					this.course.id = response.data.id;
					this.course.course_name = response.data.courseName;
					this.course.course_number = response.data.courseNumber;
					this.updating = true;
					this.showAddingForm = true;
				})
		}
	},
	computed: {
		...mapGetters(['COURSES'])
	}
}
</script>

<style scoped>

</style>