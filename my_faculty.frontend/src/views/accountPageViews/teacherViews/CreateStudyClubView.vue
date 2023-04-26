<template>
    <div>
        <ErrorPage v-if="!userAuthorized" errorCode="401" message="Данное действие Вам не разрешено"/>
        <div v-else>
            <h1>Создание сообщества курса</h1>
            <v-card-text>
                <h2 class="text-center red--text">{{ errorText }}</h2>
                <v-container>
                    <v-form
                        lazy-validation
                        ref="submitForm"
                        v-model="formValid">
                        <v-col cols="12">
                            <v-text-field
                                label="Название сообщества курса*"
                                required
                                :rules="commonRules"
                                hide-details="auto"
                                v-model="studyClub.clubName"
                            ></v-text-field>
                        </v-col>
                        <v-col cols="12">
                            <v-textarea
                                label="Описание"
                                hide-details="auto"
                                v-model="studyClub.description"
                            ></v-textarea>
                        </v-col>
                        <v-col cols="12">
                            <v-file-input
                                :rules="imageRules"
                                show-size
                                accept="image/png, image/jpeg, image/bmp"
                                placeholder="Выберите фото"
                                prepend-icon="mdi-camera"
                                hide-details="auto"
                                label="Фото"
                                v-model="studyClub.image"
                            ></v-file-input>
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
                    @click="createStudyClub"
                    :disabled="!formValid"
                >
                    Сохранить
                </v-btn>
            </v-card-actions>
        </div>
    </div>
</template>

<script>
import ErrorPage from "@/components/AccountComponents/core/service-pages/ErrorPage";

export default {
    name: "CreateStudyClubView",
    components: {ErrorPage},
    data() {
        return {
            userAuthorized: false,
            formValid: true,
            errorText: "",
            studyClub: {
                clubName: "",
                description: "",
                image: null,
                ownerId: null
            },
            commonRules: [
                v => !!v || 'Поле является обязательным для заполнения'
            ],
            imageRules: [
                value => !value || value.size < 2000000 || 'Image size should be less than 2 MB!'
            ],
        }
    },
    methods: {
        createStudyClub() {
            this.formValid = this.$refs.submitForm.validate();
            if (!this.formValid)
                return;
            this.errorText = "";
            this.studyClub.ownerId = this.$oidc.currentUserId;
            this.$loading(true);
            this.$store.dispatch('addStudyClub', this.studyClub)
                .then(() => {
                    this.resetStudyClub();
                    this.$loading(false);
                    this.$notify({
                        group: 'admin-actions',
                        title: 'Успешная операция',
                        text: 'Сообщество курса успешно создано. Модерация сообщества доступна в разделе ' +
                            '"Сообщества курсов"',
                        type: 'success'
                    });
                    this.$router.push('/clubs');
                })
                .catch(() => {
                    this.$loading(false);
                    this.errorText = "Произошла ошибка создания сообщества курса. Попробуйте заново."
                })
        },
        resetStudyClub() {
            this.studyClub.clubName = "";
            this.studyClub.description = "";
            this.studyClub.image = null;
            this.studyClub.ownerId = null;
        }
    },
    async mounted() {
        document.title = this.$route.name;
        this.userAuthorized = await this.$oidc.isTeacher();
    },
    async updated() {
        this.userAuthorized = await this.$oidc.isTeacher();
    }
}
</script>

<style scoped>

</style>