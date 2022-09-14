<template>
	<div>silent renew component</div>
</template>

<script>
export default {
	name: "RedirectSilentRenew",
	created() {
		this.$oidc.handleSilentCallback();
	}
}
</script>

<style scoped>

</style>