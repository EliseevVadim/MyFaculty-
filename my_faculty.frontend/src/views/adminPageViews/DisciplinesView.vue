<template>
    <div>
        <h1 class="text-center mt-3">Управление списком дисциплин</h1>
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
                    <span
                        class="text-h5">{{ updating ? 'Обновить информацию о дисциплине' : 'Добавить дисциплину' }}</span>
                </v-card-title>
                <v-card-text>
                    <h2 class="text-center red--text">{{ errorText }}</h2>
                    <v-container>
                        <v-form
                            lazy-validation
                            ref="submitForm"
                            v-model="formValid">
                            <v-col cols="12">
                                <v-text-field
                                    label="Название дисциплины*"
                                    required
                                    :rules="commonRules"
                                    hide-details="auto"
                                    v-model="discipline.discipline_name"
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
                Список дисциплин
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
                :items="this.DISCIPLINES.disciplines"
                :search="search"
                class="text-left"
            >
                <template v-slot:body="{items}">
                    <tbody>
                    <tr v-for="(item,index) in items" :key="index">
                        <td>
                            {{ item.id }}
                        </td>
                        <td>
                            {{ item.disciplineName }}
                        </td>
                        <td>
                            <v-btn
                                class="mx-2"
                                fab
                                small
                                color="yellow"
                                @click="startUpdatingDiscipline(item.id)"
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
                                @click="deleteDiscipline(item.id)"
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
    name: "DisciplinesView",
    data() {
        return {
            showAddingForm: false,
            search: '',
            formValid: true,
            updating: false,
            errorText: "",
            discipline: {
                id: null,
                discipline_name: ""
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
                {text: 'Название дисциплины', value: 'disciplineName'},
                {text: 'Действия', value: 'actions', sortable: false}
            ],
        }
    },
    mounted() {
        this.$store.dispatch('loadAllDisciplines');
    },
    methods: {
        openAddingForm() {
            this.errorText = "";
            this.resetDiscipline();
            this.updating = false;
            this.showAddingForm = true;
        },
        sendData() {
            this.formValid = this.$refs.submitForm.validate();
            if (!this.formValid)
                return;
            this.errorText = "";
            if (!this.updating) {
                this.$store.dispatch('addDiscipline', this.discipline)
                    .then(() => {
                        this.resetDiscipline();
                        this.showAddingForm = false;
                        this.$store.dispatch('loadAllDisciplines');
                    })
                    .catch(() => {
                        this.errorText = "Произошла ошибка добавления записи.";
                    });
            } else {
                this.$store.dispatch('updateDiscipline', this.discipline)
                    .then(() => {
                        this.resetDiscipline();
                        this.showAddingForm = false;
                        this.$store.dispatch('loadAllDisciplines');
                    })
                    .catch(() => {
                        this.errorText = "Произошла ошибка обновления записи.";
                    });
            }
        },
        resetDiscipline() {
            this.discipline.discipline_name = "";
            this.discipline.id = null;
        },
        deleteDiscipline(id) {
            this.$confirm("Вы действительно хотите удалить данную запись?")
                .then((result) => {
                    if (result) {
                        this.$store.dispatch('deleteDiscipline', id)
                            .then(() => {
                                this.resetDiscipline();
                                this.$store.dispatch('loadAllDisciplines');
                            })
                            .catch(() => {
                                this.resetDiscipline();
                                this.$notify({
                                    group: 'admin-actions',
                                    title: 'Ошибка',
                                    text: 'Невозможно удалить запись',
                                    type: 'error'
                                });
                            })
                    }
                });
        },
        startUpdatingDiscipline(id) {
            this.errorText = "";
            this.discipline.id = id;
            this.$store.dispatch('loadDisciplineById', id)
                .then((response) => {
                    this.discipline.id = response.data.id;
                    this.discipline.discipline_name = response.data.disciplineName;
                    this.updating = true;
                    this.showAddingForm = true;
                })
        }
    },
    computed: {
        ...mapGetters(['DISCIPLINES'])
    }
}
</script>

<style scoped>

</style>