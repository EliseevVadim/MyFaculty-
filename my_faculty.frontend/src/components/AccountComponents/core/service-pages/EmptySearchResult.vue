<template>
	<div>
		<h2
			class="empty-result-title"
		>
			{{message}}
		</h2>
		<v-row class="d-flex justify-center ma-2">
			<v-img
				src="img/empty-search-result.jpg"
				min-width="150"
				max-width="600"
			>
			</v-img>
		</v-row>
	</div>
</template>

<script>
export default {
	name: "EmptySearchResult",
	props: ['message']
}
</script>

<style scoped>
	.empty-result-title {
		color: black;
		font-size: 20px;
	}
</style>