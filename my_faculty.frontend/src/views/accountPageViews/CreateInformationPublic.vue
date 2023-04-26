<template>
    <div>
        <h1>Создание информационного сообщества</h1>
        <v-card-text>
            <h2 class="text-center red--text">{{ errorText }}</h2>
            <v-container>
                <v-form
                    lazy-validation
                    ref="submitForm"
                    v-model="formValid">
                    <v-col cols="12">
                        <v-text-field
                            label="Название информационного сообщества*"
                            required
                            :rules="commonRules"
                            hide-details="auto"
                            v-model="infoPublic.publicName"
                        ></v-text-field>
                    </v-col>
                    <v-col cols="12">
                        <v-textarea
                            label="Описание"
                            hide-details="auto"
                            v-model="infoPublic.description"
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
                            v-model="infoPublic.image"
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
                @click="createInfoPublic"
                :disabled="!formValid"
            >
                Сохранить
            </v-btn>
        </v-card-actions>
    </div>
</template>

<script>
import ErrorPage from "@/components/AccountComponents/core/service-pages/ErrorPage";

export default {
    name: "CreateInformationPublic",
    components: {ErrorPage},
    data() {
        return {
            formValid: true,
            errorText: "",
            infoPublic: {
                publicName: "",
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
        createInfoPublic() {
            this.formValid = this.$refs.submitForm.validate();
            if (!this.formValid)
                return;
            this.errorText = "";
            this.infoPublic.ownerId = this.$oidc.currentUserId;
            this.infoPublic.issuerId = this.$oidc.currentUserId;
            this.$loading(true);
            this.$store.dispatch('addInfoPublic', this.infoPublic)
                .then(() => {
                    this.resetInfoPublic();
                    this.$loading(false);
                    this.$notify({
                        group: 'admin-actions',
                        title: 'Успешная операция',
                        text: 'Сообщество курса успешно создано. Модерация сообщества доступна в разделе ' +
                            '"Сообщества"',
                        type: 'success'
                    });
                    this.$router.push('/communities');
                })
                .catch(() => {
                    this.$loading(false);
                    this.errorText = "Произошла ошибка создания информационного сообщества. Попробуйте заново."
                })
        },
        resetInfoPublic() {
            this.infoPublic.publicName = "";
            this.infoPublic.description = "";
            this.infoPublic.image = null;
            this.infoPublic.ownerId = null;
        }
    },
    mounted() {
        document.title = this.$route.name;
    }
}
</script>

<style scoped>

</style>