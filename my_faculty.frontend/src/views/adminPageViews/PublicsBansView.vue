<template>
    <div>
        <h1 class="text-center mt-3">Блокировка информационных сообществ*</h1>
        <h2 class="text-center px-1 mt-3 red--text">
            *ВНИМАНИЕ! Все действия в этом разделе записываются без возможности удаления. В случае злоупотребления
            функционалом данного раздела администратор понесет наказание!
        </h2>
        <v-card-actions>
            <v-btn
                class="mt-2 mb-3 mx-5"
                dark
                color="indigo"
                v-ripple
                left
                @click="openBanningForm"
            >
                <v-icon left>
                    mdi-account-multiple-minus
                </v-icon>
                Заблокировать сообщество
            </v-btn>
            <v-spacer/>
            <v-btn
                class="mt-2 mb-3 mx-5"
                dark
                color="indigo"
                v-ripple
                left
                @click="openUnbanningForm"
            >
                <v-icon left>
                    mdi-account-multiple-check
                </v-icon>
                Разблокировать сообщество
            </v-btn>
        </v-card-actions>
        <v-dialog
            v-model="showBanningActionForm"
            max-width="600px"
        >
            <v-card>
                <v-card-title>
                    <span
                        class="text-h5"
                    >
                        {{ banning ? 'Заблокировать сообщество' : 'Разблокировать сообщество' }}
                    </span>
                </v-card-title>
                <v-card-text>
                    <h2 class="text-center red--text">{{ errorText }}</h2>
                    <v-container>
                        <v-form
                            lazy-validation
                            ref="submitForm"
                            v-model="formValid"
                        >
                            <span>Идентификатор сообщества - часть, идущая в ссылке на пользователя после public
                                (например, для domain/public1 идентификатор 1)</span>
                            <v-col cols="12">
                                <v-text-field
                                    label="Идентификатор сообщества*"
                                    required
                                    :rules="numberRules"
                                    hide-details="auto"
                                    v-model="banRequest.publicId"
                                    type="number"
                                ></v-text-field>
                            </v-col>
                            <v-col cols="12">
                                <v-textarea
                                    label="Причина"
                                    required
                                    :rules="commonRules"
                                    hide-details="auto"
                                    v-model="banRequest.reason"
                                ></v-textarea>
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
                        @click="closeForm"
                    >
                        Закрыть
                    </v-btn>
                    <v-btn
                        color="blue darken-1"
                        text
                        @click="performAction"
                        :disabled="!formValid"
                    >
                        Сохранить
                    </v-btn>
                </v-card-actions>
            </v-card>
        </v-dialog>
        <v-card>
            <v-card-title>
                История блокировок сообществ
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
                :items="this.PUBLICS_BANS_REPORTS.publicsBansReports"
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
                            <router-link :to="item.publicLink">
                                {{item.publicLink}}
                            </router-link>
                        </td>
                        <td>
                            <router-link :to="item.administratorLink">
                                {{ item.administratorLink }}
                            </router-link>
                        </td>
                        <td>
                            {{ prettifyAction(item.performedAction) }}
                        </td>
                        <td>
                            {{ item.reason }}
                        </td>
                        <td>
                            {{ prettifyActionDate(item.created) }}
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
    name: "PublicsBansView",
    data() {
        return {
            showBanningActionForm: false,
            banning: null,
            formValid: true,
            errorText: "",
            search: '',
            banActions: {
                1: "Блокировка",
                2: "Разблокировка"
            },
            banRequest: {
                publicId: null,
                reason: ""
            },
            commonRules: [
                v => !!v || 'Поле является обязательным для заполнения'
            ],
            numberRules: [
                v => !!v || 'Поле является обязательным для заполнения',
                v => Number.isInteger(Number(v)) || "Значение должно быть целым числом",
                v => v > 0 || "Значение должно быть больше нуля"
            ],
            headers: [
                {
                    text: 'id',
                    align: 'start',
                    value: 'id',
                },
                {text: 'Сообщество', value: 'publicLink'},
                {text: 'Администратор', value: 'administratorLink'},
                {text: 'Действие', value: 'performedAction'},
                {text: 'Причина', value: 'reason'},
                {text: 'Дата', value: 'created'}
            ]
        }
    },
    methods: {
        prettifyAction(actionCode) {
            return `${this.banActions[actionCode]} (${actionCode})`;
        },
        prettifyActionDate(date) {
            return new Date(date).toLocaleString('ru-RU', {
                year: 'numeric',
                month: 'long',
                day: 'numeric',
                hour: 'numeric',
                minute: 'numeric'
            });
        },
        openBanningForm() {
            this.banning = true;
            this.showBanningActionForm = true;
        },
        openUnbanningForm() {
            this.banning = false;
            this.showBanningActionForm = true;
        },
        closeForm() {
            this.resetInput();
            this.showBanningActionForm = false;
        },
        resetInput() {
            this.banRequest.publicId = null;
            this.banRequest.reason = "";
            this.banning = null;
            this.errorText = "";
            this.$refs.submitForm.resetValidation();
        },
        performAction() {
            this.formValid = this.$refs.submitForm.validate();
            if (!this.formValid)
                return;
            this.$confirm(`Вы действительно хотите ${this.banning ? 'заблокировать' : 'разблокировать'}
                сообщество с id = ${this.banRequest.publicId}?`)
                .then((result) => {
                    if (result) {
                        this.$loading(true);
                        if (this.banning) {
                            this.$store.dispatch('banInfoPublic', this.banRequest)
                                .then(() => {
                                    this.closeForm();
                                    this.$notify({
                                        group: 'admin-actions',
                                        title: 'Успешная операция',
                                        text: 'Сообщество успешно заблокировано',
                                        type: 'success'
                                    });
                                    this.$store.dispatch('loadAllPublicsBansReports');
                                })
                                .catch((error) => {
                                    this.errorText = error.response.data.error ?? "Произошла ошибка выполнения запроса. Проверьте ввод.";
                                })
                                .finally(() => {
                                    this.$loading(false);
                                });
                            return;
                        }
                        this.$store.dispatch('unbanInfoPublic', this.banRequest)
                            .then(() => {
                                this.closeForm();
                                this.$notify({
                                    group: 'admin-actions',
                                    title: 'Успешная операция',
                                    text: 'Сообщество успешно разблокировано',
                                    type: 'success'
                                });
                                this.$store.dispatch('loadAllPublicsBansReports');
                            })
                            .catch((error) => {
                                this.errorText = error.response.data.error ?? "Произошла ошибка выполнения запроса. Проверьте ввод.";
                            })
                            .finally(() => {
                                this.$loading(false);
                            });
                    }
                });
        }
    },
    mounted() {
        this.$store.dispatch('loadAllPublicsBansReports');
    },
    computed: {
        ...mapGetters(['PUBLICS_BANS_REPORTS'])
    }
}
</script>

<style scoped>

</style>