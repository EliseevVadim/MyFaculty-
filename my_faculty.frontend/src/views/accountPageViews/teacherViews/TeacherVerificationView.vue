<template>
    <div>
        <h1>Верификация аккаунта преподавателя</h1>
        <div v-if="rulesHaveBeenRead" class="pt-4">
            <v-col class="text-left">
                <v-btn
                    color="blue darken-3"
                    class="white--text"
                    @click="goBackToRules"
                >
                    <v-icon left>
                        mdi-arrow-left-bold
                    </v-icon>
                    Назад
                </v-btn>
            </v-col>
            <v-form
                lazy-validation
                v-model="formValid"
            >
                <v-text-field
                    v-model="token"
                    label="Введите верификационный токен..."
                    :rules="tokenInputRules"
                >
                </v-text-field>
                <v-btn
                    color="green"
                    class="white--text font-weight-bold"
                    :disabled="!formValid"
                    @click="sendVerificationRequest"
                >
                    Отправить запрос на верификацию
                </v-btn>
            </v-form>
        </div>
        <div v-else>
            <p class="pt-4">
                Для верификации аккаунта преподавателя необходимо чтобы верифицируемый пользователь был
                зарегистрирован с email, находящимся в списке преподавательских email в системе.
            </p>
            <p>
                Если Вы зарегистрированы с подходящим email, запросите у администратора системы верификационный токен,
                после чего данный токен будет отправлен на Вашу преподавательскую почту, указанную в списке
                преподавателей. Полученный токен необходимо ввести в текстовое поле ниже и отправить запрос на
                верификацию. В случае успешной верификации, Вам будут доступны новые возможности системы и специальный
                значок в профиле.
            </p>
            <p class="red--text">
                <b>
                    Важно! Никому не передавайте Ваш верификационный токен, поскольку при потере доступа к почте Вы
                    рискуете передать злоумышленникам возможность быть верифицированным от Вашего имени, а
                    соответственно они получат доступ к возможностям сайта, нахождение которых в "неправильных руках"
                    чревато неприятными последствиями!
                </b>
            </p>
            <v-btn
                color="green"
                class="white--text font-weight-bold"
                @click="readVerificationRules"
            >
                Я прочитал(а) правила и принимаю их
            </v-btn>
        </div>

    </div>
</template>

<script>
export default {
    name: "TeacherVerificationView",
    data() {
        return {
            rulesHaveBeenRead: false,
            tokenInputRules: [
                value => !!value || 'Значение не может быть пустым.'
            ],
            formValid: false,
            token: ""
        }
    },
    methods: {
        readVerificationRules() {
            this.rulesHaveBeenRead = true;
        },
        goBackToRules() {
            this.rulesHaveBeenRead = false;
        },
        sendVerificationRequest() {
            if (this.token === "")
                return;
            let requestBody = {
                userId: this.$oidc.currentUserId,
                verificationToken: this.token
            }
            this.$loading(true);
            this.$store.dispatch('sendVerificationRequest', requestBody)
                .then(() => {
                    this.$store.dispatch('loadCurrentUser', this.$oidc.currentUserId)
                        .then(() => {
                            this.$loading(false);
                            this.$notify({
                                group: 'admin-actions',
                                title: 'Успешная операция',
                                text: 'Верификация прошла успешно',
                                type: 'success'
                            });
                            this.rulesHaveBeenRead = false;
                        })
                })
                .catch(() => {
                    this.$loading(false);
                    this.$notify({
                        group: 'admin-actions',
                        title: 'Ошибка верификации',
                        text: 'Верификация прошла неуспешно. Возможно Вы были уже верифицированы. При необходимости ' +
                            'повторите позже.',
                        type: 'error'
                    });
                });
        }
    },
    mounted() {
        document.title = this.$route.name;
    }
}
</script>

<style scoped>
p {
    text-align: left;
    text-indent: 25px;
    font-weight: bolder;
    color: black;
}
</style>