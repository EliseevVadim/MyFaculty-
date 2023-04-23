<template>
    <v-row justify="center">
        <v-dialog
            v-model="displayModal"
            scrollable
            max-width="850px"
            max-height="500px"
        >
            <v-card>
                <v-card-title>
					<span>
						Массовое исключение студентов из сообщества
					</span>
                    <v-spacer></v-spacer>
                    <v-btn
                        icon
                        @click="close"
                    >
                        <v-icon>
                            mdi-close
                        </v-icon>
                    </v-btn>
                </v-card-title>
                <v-divider></v-divider>
                <v-card-text>
                    <v-container>
                        <h3 class="black--text">
                            В данном разделе Вы можете массово исключить студентов из своего
                            сообщества курса, выбрав исключение либо определенной группы студентов, либо целого
                            курса (будут исключены те пользователи, в чьих профилях указаны соответствующие
                            курс / группа).
                        </h3>
                        <v-form
                            lazy-validation
                            ref="submitForm"
                            v-model="formValid"
                        >
                            <v-col
                                cols="12"
                            >
                                <v-autocomplete
                                    :items="this.FACULTIES.faculties"
                                    item-text="facultyName"
                                    item-value="id"
                                    :rules="commonRules"
                                    label="Выберите факультет*"
                                    v-model="selectedFacultyId"
                                    @change="loadCoursesList"
                                ></v-autocomplete>
                            </v-col>
                            <v-col
                                v-if="coursesAreLoaded"
                                cols="12"
                            >
                                <v-autocomplete
                                    label="Курс"
                                    required
                                    :items="this.COURSES.courses"
                                    item-text="courseName"
                                    item-value="id"
                                    hide-details="auto"
                                    v-model="selectedCourseId"
                                    @change="loadGroupsList"
                                ></v-autocomplete>
                            </v-col>
                            <h3 v-else-if="coursesAreLoaded === false" class="red--text">
                                Для данного факультета информация о курсах отсутствует.
                                Дальнейшее взаимодействие невозможно.
                            </h3>
                            <v-col
                                v-if="groupsAreLoaded"
                                cols="12"
                            >
                                <v-autocomplete
                                    label="Группа"
                                    required
                                    :items="this.GROUPS.groups"
                                    item-text="groupName"
                                    item-value="id"
                                    hide-details="auto"
                                    v-model="selectedGroupId"
                                ></v-autocomplete>
                            </v-col>
                            <h3 v-else-if="groupsAreLoaded === false" class="red--text">
                                Для данного курса информация о группах отсутствует.
                                Возможно только исключение всего курса
                            </h3>
                        </v-form>
                    </v-container>
                    <small>Поля, помеченные * обязательны к заполнению</small>
                </v-card-text>
                <v-card-actions>
                    <v-btn
                        color="red"
                        class="white--text"
                        @click="close"
                    >
                        Закрыть
                    </v-btn>
                    <v-spacer></v-spacer>
                    <v-btn
                        color="success"
                        class="white--text"
                        :disabled="buttonShouldBeDisabled()"
                        @click="removeStudents"
                    >
                        {{ generateButtonText() }}
                    </v-btn>
                </v-card-actions>
            </v-card>
        </v-dialog>
    </v-row>
</template>

<script>
import {mapGetters} from "vuex";

export default {
    name: "MassStudyClubDeletingForm",
    props: ['show', 'studyClubId'],
    data() {
        return {
            selectedFacultyId: null,
            selectedCourseId: null,
            selectedGroupId: null,
            coursesAreLoaded: null,
            groupsAreLoaded: null,
            formValid: false,
            commonRules: [
                v => !!v || 'Поле является обязательным для заполнения'
            ],
        }
    },
    methods: {
        close() {
            this.$emit('close');
        },
        reloadStudyClub() {
            this.$emit('load');
        },
        loadData() {
            this.$store.dispatch('loadAllFaculties');
        },
        resetData() {
            this.selectedFacultyId = null;
            this.selectedCourseId = null;
            this.selectedGroupId = null;
            this.coursesAreLoaded = null;
            this.groupsAreLoaded = null;
            this.formValid = true;
        },
        buttonShouldBeDisabled() {
            return !this.formValid || this.selectedCourseId === null;
        },
        generateButtonText() {
            if (this.buttonShouldBeDisabled())
                return 'Данные введены не до конца';
            if (this.selectedCourseId !== null && this.selectedGroupId === null)
                return 'Исключить студентов выбранного курса';
            return 'Исключить студентов выбранной группы';
        },
        loadCoursesList() {
            this.$loading(true);
            this.$store.dispatch('loadCoursesByFacultyId', this.selectedFacultyId)
                .then(() => {
                    this.$loading(false);
                    this.coursesAreLoaded = this.COURSES.courses.length > 0;
                    if (!this.coursesAreLoaded) {
                        this.selectedCourseId = null;
                        this.selectedGroupId = null;
                        this.groupsAreLoaded = null;
                    }
                })
        },
        loadGroupsList() {
            this.$loading(true);
            this.$store.dispatch('loadGroupsByCourseId', this.selectedCourseId)
                .then(() => {
                    this.groupsAreLoaded = this.GROUPS.groups.length > 0;
                    if (!this.groupsAreLoaded)
                        this.selectedGroupId = null;
                    this.$loading(false);
                });
        },
        removeStudents() {
            this.formValid = this.$refs.submitForm.validate();
            if (!this.formValid)
                return;
            if (this.selectedCourseId !== null && this.selectedGroupId === null) {
                this.$confirm('Внимание! Данное действие приведет к добавлению в сообщество ' +
                    'всех пользователей, на страницах которых указан выбранный вами курс. ' +
                    'Вы хотите продолжить?', {
                    color: 'error'
                })
                    .then((result) => {
                        if (result)
                            this.removeCourse();
                    })
                return;
            }
            this.$confirm('Внимание! Данное действие приведет к добавлению в сообщество ' +
                'всех пользователей, на страницах которых указан выбранная вами группа. ' +
                'Вы хотите продолжить?', {
                color: 'error'
            })
                .then((result) => {
                    if (result)
                        this.removeGroup();
                })
        },
        removeCourse() {
            this.$loading(true);
            this.$store.dispatch('removeUsersByCourseFromStudyClub', {
                issuerId: this.$oidc.currentUserId,
                studyClubId: this.studyClubId,
                courseId: this.selectedCourseId
            })
                .then(() => {
                    this.reloadStudyClub();
                })
                .catch((error) => {
                    this.$notify({
                        group: 'admin-actions',
                        title: 'Ошибка',
                        text: error.response.data.error,
                        type: 'error'
                    });
                    this.$loading(false);
                })
        },
        removeGroup() {
            this.$loading(true);
            this.$store.dispatch('removeUsersByGroupFromStudyClub', {
                issuerId: this.$oidc.currentUserId,
                studyClubId: this.studyClubId,
                groupId: this.selectedGroupId
            })
                .then(() => {
                    this.reloadStudyClub();
                })
                .catch((error) => {
                    this.$notify({
                        group: 'admin-actions',
                        title: 'Ошибка',
                        text: error.response.data.error,
                        type: 'error'
                    });
                    this.$loading(false);
                })
        }
    },
    computed: {
        displayModal: {
            get() {
                return this.show;
            },
            set(value) {
                if (!value)
                    this.$emit('close')
            }
        },
        ...mapGetters(['FACULTIES']),
        ...mapGetters(['COURSES']),
        ...mapGetters(['GROUPS'])
    }
}
</script>

<style scoped>

</style>