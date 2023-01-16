<template>
	<router-link
		:to="'/id' + user.id"
		class="text-decoration-none"
	>
		<v-img
			aspect-ratio="1"
			min-width="150"
			min-height="150"
			max-width="150"
			max-height="150"
			class="user-image"
			:src="user.avatarPath ? user.avatarPath : '../img/blank-item.png'"
		>

		</v-img>
		<h2 class="member-name">
			{{getUserFullName(user)}}
		</h2>
	</router-link>
</template>

<script>
export default {
	name: "UserInClubLookupPresenter",
	props: ['user'],
	methods: {
		getUserFullName(user) {
			return user.firstName + " " + user.lastName;
		},
	}
}
</script>

<style scoped>
	.member-name {
		text-decoration: none !important;
		color: black;
		font-size: 16px;
	}
	.user-image {
		margin: 0 auto;
		border-radius: 50%;
		border: double 3px transparent;
		background-image: linear-gradient(white, white),
		radial-gradient(circle at bottom left, red 20%, blue, black);
		background-origin: border-box;
		background-clip: padding-box, border-box;
	}
</style>