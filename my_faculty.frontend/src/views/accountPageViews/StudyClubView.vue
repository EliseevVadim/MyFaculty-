<template>
	<div>
		<UsersListModal
			:show="showMembers"
			:users="watchingClub.members"
			title="Участники сообщества"
			@close="showMembers = false"
		/>
		<UsersListModal
			:show="showModerators"
			:users="watchingClub.moderators"
			title="Модераторы сообщества"
			@close="showModerators = false"
		/>
		<ErrorPage v-if="clubNotFound" error-code="404" message="Сообщество курса не найдено"/>
		<v-container
			fluid
			v-if="Object.keys(watchingClub).length !== 0"
		>
			<v-row class="d-flex justify-start ma-1">
				<h1
					class="club-name"
				>
					{{watchingClub.clubName}}
				</h1>
				<v-btn
					v-if="currentUserIsGroupOwner()"
					class="mx-2"
					fab
					small
					color="white"
				>
					<v-icon>
						mdi-pencil
					</v-icon>
				</v-btn>
				<v-spacer class="d-none d-sm-block"></v-spacer>
				<v-menu
					v-if="currentUserIsMember()"
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
			</v-row>
			<v-divider></v-divider>
			<v-row
				class="d-flex mt-2 mb-2 flex-column-reverse flex-sm-row"
			>
				<v-col
					class="text-left col-12 col-sm-6"
				>
					<v-col class="mt-0">
						<b>Информация:</b>
						<p class="mt-2 club-description">
							{{watchingClub.description}}
						</p>
					</v-col>
					<v-col class="mt-0 modal-invoker" @click="showMembers = true">
						<b>Участников: </b>
						<span>{{watchingClub.membersCount}}</span>
					</v-col>
					<v-col class="mt-0 modal-invoker" @click="showModerators = true">
						<b>Модераторов: </b>
						<span>{{watchingClub.moderators.length}}</span>
					</v-col>
					<v-col class="mt-0" to="/">
						<b>Владелец: </b>
						<router-link
							:to="'/id' + watchingClub.ownerId"
							class="user-link"
						>
							<v-list-item class="pl-0">
								<v-list-item-avatar
									class="align-self-center"
									color="white"
									contain
								>
									<v-img
										class="user-image"
										:src="watchingClub.owner.avatarPath ? watchingClub.owner.avatarPath : '../img/blank-item.png'"
									/>
								</v-list-item-avatar>
								<v-list-item-content>
									<v-list-item-title
										class="profile-name"
									>
										{{getOwnersFullName()}}
										<TeacherVerificationMark v-if="watchingClub.owner.isTeacher"/>
									</v-list-item-title>
								</v-list-item-content>
							</v-list-item>
						</router-link>
					</v-col>
				</v-col>
				<v-col
					class="d-flex col-12 col-sm-6 justify-center justify-sm-end"
				>
					<v-img
						max-width="150"
						max-height="150"
						:src="watchingClub.imagePath ? watchingClub.imagePath : '../img/blank-club.png'"
						class="club-photo"
					>
					</v-img>
				</v-col>
			</v-row>
			<v-divider></v-divider>
		</v-container>
	</div>
</template>

<script>
import ErrorPage from "@/components/AccountComponents/core/service-pages/ErrorPage";
import TeacherVerificationMark from "@/components/AccountComponents/core/verificationMarks/TeacherVerificationMark";
import UserInClubLookupPresenter from "@/components/presenters/UserInClubLookupPresenter";
import UsersListModal from "@/components/UsersListModal";
export default {
	name: "StudyClubView",
	components: {UsersListModal, UserInClubLookupPresenter, TeacherVerificationMark, ErrorPage},
	data() {
		return {
			watchingClub: {},
			clubNotFound: false,
			showMembers: false,
			showModerators: false
		}
	},
	methods: {
		getOwnersFullName() {
			return this.watchingClub.owner.firstName + " " + this.watchingClub.owner.lastName;
		},
 		currentUserIsGroupOwner() {
			return this.watchingClub.ownerId === this.$oidc.currentUserId;
		},
		currentUserIsMember() {
			return this.watchingClub.members.find(user => user.id == this.$oidc.currentUserId) !== undefined;
		},
		currentUserIsModerator() {
			return this.watchingClub.moderators.find(user => user.id == this.$oidc.currentUserId) !== undefined;
		},
		joinStudyClub() {
			this.$loading(true);
			this.$store.dispatch('joinStudyClub', {
				studyClubId: this.watchingClub.id,
				userId: this.$oidc.currentUserId
			})
				.then(() => {
					this.$store.dispatch('loadStudyClubById', this.watchingClub.id)
						.then((response) => {
							this.reloadStudyClubWithMessage(response, 'Вы успешно вступили в сообщество!');
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
				})
		},
		leaveStudyClub() {
			this.$loading(true);
			this.$store.dispatch('leaveStudyClub', {
				studyClubId: this.watchingClub.id,
				userId: this.$oidc.currentUserId
			})
				.then(() => {
					this.$store.dispatch('loadStudyClubById', this.watchingClub.id)
						.then((response) => {
							this.reloadStudyClubWithMessage(response, 'Вы успешно покинули сообщество!')
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
				})
		},
		reloadStudyClubWithMessage(response, message) {
			this.watchingClub = response.data;
			this.$notify({
				group: 'admin-actions',
				title: 'Успешная операция',
				text: message,
				type: 'success'
			});
			this.$loading(false);
		}
	},
	mounted() {
		let id = this.$route.params.id;
		this.$loading(true);
		this.$store.dispatch('loadStudyClubById', id)
			.then((response) => {
				this.watchingClub = response.data;
				console.log(this.watchingClub);
				document.title = this.watchingClub.clubName;
			})
			.catch((error) => {
				if (error.response.status === 404) {
					document.title = "Сообщество не найдено";
					this.clubNotFound = true;
				}
			})
			.finally(() => {
				this.$loading(false);
			})
	}
}
</script>

<style scoped>
	.club-name {
		text-align: left;
		color: black;
		font-size: 24px;
	}
	.club-photo {
		border: double 8px transparent;
		border-radius: 50%;
		background-image: linear-gradient(white, white),
			radial-gradient(circle at bottom left, red 20%, blue, black);
		background-origin: border-box;
		background-clip: padding-box, border-box;
	}
	p.club-description {
		text-indent: 25px;
		font-size: 18px;
		margin-bottom: 0;
		color: #1e1e1e;
	}
	b {
		color: black;
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
	.user-link {
		text-decoration: none;
		font-weight: bolder;
		color: black;
	}
	.club-menu-option {
		cursor: pointer;
	}
	.modal-invoker {
		cursor: pointer;
	}
</style>