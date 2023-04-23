<template>
    <v-tooltip
        max-width="200"
        bottom
        color="primary"
    >
        <template v-slot:activator="{ on, attrs }">
            <v-btn
                icon
                dark
                v-bind="attrs"
                v-on="on"
                to="/teacher-verification"
            >
                <v-icon
                    color="blue darken-2"
                >
                    mdi-account-school
                </v-icon>
            </v-btn>
        </template>
        <div>
            <p>
                Данная иконка означает что пользователь - верифицированный преподаватель.
            </p>
            <p>
                Если Вы тоже преподаватель и хотите получить такой же значок - нажмите на эту иконку и
                выполните соответствующие инструкции.
            </p>
        </div>
    </v-tooltip>
</template>

<script>
export default {
    name: "TeacherVerificationMark"
}
</script>

<style scoped>
p {
    text-align: left;
}
</style>