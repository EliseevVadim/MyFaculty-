<template>

</template>

<script>
export default {
    name: "SignOutCallback",
    created() {
		this.$loading(true);
        this.$oidc.completeLogout()
            .then(() => {
				this.$loading(false);
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