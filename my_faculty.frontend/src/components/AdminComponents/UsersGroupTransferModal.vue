<template>
    <v-dialog
        v-model="displayModal"
        max-width="800px"
    >
        <v-card>
            <v-card-title class="d-flex justify-center">
                <span class="text-h5">Перевести пользователей</span>
            </v-card-title>
            <v-card-text class="pb-0">
                <v-container>
                    <v-form
                        lazy-validation
                        ref="submitForm"
                        v-model="formValid"
                    >
                        <h2>Группа, из которой переводятся пользователи</h2>
                        <v-col cols="12">
                            <v-select
                                :items="sourceData.faculties"
                                item-text="facultyName"
                                item-value="id"
                                label="Выберите интересующий факультет*"
                                @change="loadSourceCoursesList"
                                v-model="sourceData.selectedFacultyId"
                            ></v-select>
                        </v-col>
                        <v-col cols="12">
                            <v-select
                                v-if="sourceData.coursesAreLoaded"
                                :items="sourceData.courses"
                                item-text="courseName"
                                item-value="id"
                                label="Выберите курс*"
                                @change="loadSourceGroupsList"
                                v-model="sourceData.selectedCourseId"
                            ></v-select>
                            <h3 v-else-if="sourceData.coursesAreLoaded === false" class="red--text">
                                Для данного факультета информация о курсах отсутствует.
                                Дальнейшее взаимодействие невозможно.
                            </h3>
                        </v-col>
                        <v-col cols="12">
                            <v-select
                                v-if="sourceData.groupsAreLoaded"
                                :items="sourceData.groups"
                                item-text="groupName"
                                item-value="id"
                                label="Выберите группу*"
                                v-model="sourceData.selectedGroupId"
                            ></v-select>
                            <h3 v-else-if="sourceData.groupsAreLoaded === false" class="red--text">
                                Для данного курса информация о группах отсутствует.
                                Дальнейшее взаимодействие невозможно.
                            </h3>
                        </v-col>
                        <h2>Группа, в которую переводятся пользователи</h2>
                        <v-col cols="12">
                            <v-select
                                :items="destinationData.faculties"
                                item-text="facultyName"
                                item-value="id"
                                label="Выберите интересующий факультет*"
                                @change="loadDestinationCoursesList"
                                v-model="destinationData.selectedFacultyId"
                            ></v-select>
                        </v-col>
                        <v-col cols="12">
                            <v-select
                                v-if="destinationData.coursesAreLoaded"
                                :items="destinationData.courses"
                                item-text="courseName"
                                item-value="id"
                                label="Выберите курс*"
                                @change="loadDestinationGroupsList"
                                v-model="destinationData.selectedCourseId"
                            ></v-select>
                            <h3 v-else-if="destinationData.coursesAreLoaded === false" class="red--text">
                                Для данного факультета информация о курсах отсутствует.
                                Дальнейшее взаимодействие невозможно.
                            </h3>
                        </v-col>
                        <v-col cols="12">
                            <v-select
                                v-if="destinationData.groupsAreLoaded"
                                :items="destinationData.groups"
                                item-text="groupName"
                                item-value="id"
                                label="Выберите группу*"
                                v-model="destinationData.selectedGroupId"
                            ></v-select>
                            <h3 v-else-if="destinationData.groupsAreLoaded === false" class="red--text">
                                Для данного курса информация о группах отсутствует.
                                Дальнейшее взаимодействие невозможно.
                            </h3>
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
                    @click="transferUsers"
                    :disabled="!formValid"
                >
                    Перевести пользователей
                </v-btn>
            </v-card-actions>
        </v-card>
    </v-dialog>
</template>

<script>
export default {
    name: "UsersGroupTransferModal",
    props: ['show'],
    data() {
        return {
            formValid: false,
            sourceData: {
                selectedFacultyId: null,
                selectedCourseId: null,
                selectedGroupId: null,
                coursesAreLoaded: null,
                groupsAreLoaded: null,
                faculties: null,
                courses: null,
                groups: null
            },
            destinationData: {
                selectedFacultyId: null,
                selectedCourseId: null,
                selectedGroupId: null,
                coursesAreLoaded: null,
                groupsAreLoaded: null,
                faculties: null,
                courses: null,
                groups: null
            }
        }
    },
    methods: {
        loadSourceCoursesList() {
            this.$loading(true);
            this.$store.dispatch('loadCoursesByFacultyIdStateless', this.sourceData.selectedFacultyId)
                .then((response) => {
                    this.sourceData.courses = response.data.courses;
                    this.sourceData.coursesAreLoaded = this.sourceData.courses.length > 0;
                    this.$loading(false);
                })
        },
        loadSourceGroupsList() {
            this.$loading(true);
            this.$store.dispatch('loadGroupsByCourseIdStateless', this.sourceData.selectedCourseId)
                .then((response) => {
                    this.sourceData.groups = response.data.groups;
                    this.sourceData.groupsAreLoaded = this.sourceData.groups.length > 0;
                    this.$loading(false);
                })
        },
        loadDestinationCoursesList() {
            this.$loading(true);
            this.$store.dispatch('loadCoursesByFacultyIdStateless', this.destinationData.selectedFacultyId)
                .then((response) => {
                    this.destinationData.courses = response.data.courses;
                    this.destinationData.coursesAreLoaded = this.destinationData.courses.length > 0;
                    this.$loading(false);
                })
        },
        loadDestinationGroupsList() {
            this.$loading(true);
            this.$store.dispatch('loadGroupsByCourseIdStateless', this.destinationData.selectedCourseId)
                .then((response) => {
                    this.destinationData.groups = response.data.groups;
                    this.destinationData.groupsAreLoaded = this.destinationData.groups.length > 0;
                    this.$loading(false);
                })
        },
        transferUsers() {
            let sourceId = this.sourceData.selectedGroupId;
            let destinationId = this.destinationData.selectedGroupId;
            this.$confirm(`Вы действительно хотите перевести всех пользователей из группы с id =
				${sourceId} в группу с id = ${destinationId}?`)
                .then((result) => {
                    if (result) {
                        this.$loading(true);
                        this.$store.dispatch('transferUsersToAnotherGroup', {
                            sourceGroupId: sourceId,
                            destinationGroupId: destinationId
                        })
                            .then(() => {
                                this.$notify({
                                    group: 'admin-actions',
                                    title: 'Успешная операция',
                                    text: 'Пользователи успешно переведены',
                                    type: 'success'
                                });
                                this.$emit('load');
                                this.resetInput();
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
                                this.$loading(false);
                                this.$emit('close');
                            });
                    }
                })
        },
        resetInput() {
            this.sourceData = {
                selectedFacultyId: null,
                selectedCourseId: null,
                selectedGroupId: null,
                coursesAreLoaded: null,
                groupsAreLoaded: null,
                faculties: null,
                courses: null,
                groups: null
            };
            this.destinationData = {
                selectedFacultyId: null,
                selectedCourseId: null,
                selectedGroupId: null,
                coursesAreLoaded: null,
                groupsAreLoaded: null,
                faculties: null,
                courses: null,
                groups: null
            };
            this.loadFacultiesInfo();
        },
        loadFacultiesInfo() {
            this.$store.dispatch('loadAllFacultiesStateless')
                .then((response) => {
                    this.sourceData.faculties = response.data.faculties;
                    this.destinationData.faculties = response.data.faculties;
                })
        }
    },
    mounted() {
        this.loadFacultiesInfo();
    },
    computed: {
        displayModal: {
            get() {
                return this.show;
            },
            set(value) {
                if (!value) {
                    this.$emit('close');
                }
            }
        }
    }
}
</script>

<style scoped>

</style>