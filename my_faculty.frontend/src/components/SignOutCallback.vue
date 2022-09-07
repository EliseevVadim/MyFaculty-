<template>

</template>

<script>
export default {
    name: "SignOutCallback",
    created() {
        this.$oidc.completeLogout()
            .then(() => {
                this.$router.push('/');
            })
            .catch((error) => {
                console.log(error);
            })
    }
}
</script>

<style scoped>

</style>