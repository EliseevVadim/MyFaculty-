<template>

</template>

<script>
export default {
    name: "SignInCallback",
    created() {
        this.$oidc.completeLogin()
            .then((redirectPath) => {
                this.$router.push('/personal');
            })
            .catch((error) => {
                console.log(error);
            })
    }
}
</script>

<style scoped>

</style>