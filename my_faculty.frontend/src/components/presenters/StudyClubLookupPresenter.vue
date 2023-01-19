<template>
	<div>
		<v-divider></v-divider>
		<v-list-item
			:key="item.clubName"
		>
			<v-list-item-avatar>
				<v-img
					class="club-image"
					:src="item.imagePath ? item.imagePath : 'img/blank-club.png'"
				/>
			</v-list-item-avatar>

			<v-list-item-content class="club-lookup-content">
				<v-list-item-title>
					<router-link class="club-title" :to="'/clubs/' + item.id">{{item.clubName}}</router-link>
				</v-list-item-title>
				<v-list-item-subtitle>
					{{prettifyMembersCount()}}
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
		currentUserIsModerator() {
			return this.item.moderators.find(user => user.id == this.$oidc.currentUserId) !== undefined;
		},
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
			if (this.currentUserIsModerator())
				this.$confirm('В данном сообществе Вы являетесь модератором. Выход из него повлечет ' +
					'потерю этой должности. Продолжить?')
					.then((result) => {
						if (result)
							this.continueStudyClubLeaving();
					})
			else
				this.continueStudyClubLeaving()
		},
		continueStudyClubLeaving() {
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
		},
		prettifyMembersCount() {
			let  count = this.item.membersCount;
			let lastNumber = count % 100;
			let variants = ['участник', 'участника', 'участников'];
			if (lastNumber > 10 && lastNumber < 20)
				return `${count} ${variants[2]}`;
			let lastDigit = lastNumber % 10;
			switch (lastDigit) {
				case 1:
					return `${count} ${variants[0]}`;
				case 2:
				case 3:
				case 4:
					return `${count} ${variants[1]}`;
				default:
					return `${count} ${variants[2]}`;
			}
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
	.club-image {
		margin: 0 auto;
		border-radius: 50%;
		border: double 3px transparent;
		background-image: linear-gradient(white, white),
		radial-gradient(circle at bottom left, red 20%, blue, black);
		background-origin: border-box;
		background-clip: padding-box, border-box;
	}
</style>