<template>

</template>

<script>
export default {
    name: "SignInCallback",
    created() {
		this.$loading(true);
        this.$oidc.completeLogin()
            .then(() => {
				this.$loading(false);
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