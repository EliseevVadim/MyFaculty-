<template>
	<div>
		<v-divider></v-divider>
		<v-list-item
			:key="item.clubName"
		>
			<v-list-item-avatar>
				<v-img :src="item.imagePath === '' ? 'img/blank-club.png' : item.imagePath"></v-img>
			</v-list-item-avatar>

			<v-list-item-content class="club-lookup-content">
				<v-list-item-title>
					<router-link class="club-title" :to="'/clubs/' + item.id">{{item.clubName}}</router-link>
				</v-list-item-title>
				<v-list-item-subtitle>
					{{ item.membersCount }} участников
				</v-list-item-subtitle>
			</v-list-item-content>
			<v-menu
				v-if="item.currentUserInClub"
				bottom
				left
			>
				<template v-slot:activator="{ on, attrs }">
					<v-btn
						icon
						v-bind="attrs"
						v-on="on"
					>
						<v-icon>mdi-dots-horizontal</v-icon>
					</v-btn>
				</template>
				<v-list>
					<v-list-item ripple>
						<v-list-item-title
							class="club-menu-option"
							@click="leaveStudyClub"
						>
							Покинуть сообщество
						</v-list-item-title>
					</v-list-item>
				</v-list>
			</v-menu>
			<v-btn
				v-else
				color="primary"
				@click="joinStudyClub"
			>
				Вступить
			</v-btn>
		</v-list-item>
		<v-divider></v-divider>
	</div>
</template>

<script>

export default {
	name: "StudyClubLookupPresenter",
	props: ['item'],
	data() {
		return {
			userInClub: null
		}
	},
 	methods: {
		joinStudyClub() {
			this.$loading(true);
			this.$store.dispatch('joinStudyClub', {
				studyClubId: this.item.id,
				userId: this.$oidc.currentUserId
			})
				.then(() => {
					this.$store.dispatch('loadCurrentUser', this.$oidc.currentUserId)
						.then(() => {
							this.$notify({
								group: 'admin-actions',
								title: 'Успешная операция',
								text: 'Вы успешно вступили в сообщество!',
								type: 'success'
							});
							this.item.membersCount++;
							this.item.currentUserInClub = true;
							this.$loading(false);
						})
				})
				.catch(() => {
					this.$notify({
						group: 'admin-actions',
						title: 'Ошибка',
						text: 'Произошла ошибка, попробуйте заново.',
						type: 'error'
					});
					this.$loading(false);
				});
		},
		leaveStudyClub() {
			this.$loading(true);
			this.$store.dispatch('leaveStudyClub', {
				studyClubId: this.item.id,
				userId: this.$oidc.currentUserId
			})
				.then(() => {
					this.$store.dispatch('loadCurrentUser', this.$oidc.currentUserId)
						.then(() => {
							this.$notify({
								group: 'admin-actions',
								title: 'Успешная операция',
								text: 'Вы успешно покинули сообщество!',
								type: 'success'
							});
							this.item.membersCount--;
							this.item.currentUserInClub = false;
							this.$loading(false);
						})
				})
				.catch(() => {
					this.$notify({
						group: 'admin-actions',
						title: 'Ошибка',
						text: 'Произошла ошибка, попробуйте заново.',
						type: 'error'
					});
					this.$loading(false);
				});
		}
	}
}
</script>

<style scoped>
	.club-lookup-content {
		text-align: left;
	}
	.club-title {
		text-decoration: none;
		font-weight: bolder;
		font-size: 16px;
		color: #4040c0;
	}
	.club-menu-option {
		cursor: pointer;
	}
</style>