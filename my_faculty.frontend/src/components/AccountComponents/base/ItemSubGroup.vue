<template>
	<ItemSubGroup
		:item="item"
		text
		sub-group
	/>
</template>

<script>
export default {
	name: "ItemSubGroup",
	props: {
		item: {
			type: Object,
			default: () => ({
				avatar: undefined,
				group: undefined,
				title: undefined,
				children: [],
			}),
		},
	},
}
</script>

<style scoped>

</style>