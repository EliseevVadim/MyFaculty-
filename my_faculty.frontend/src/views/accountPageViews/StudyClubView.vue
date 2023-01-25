<template>
	<div>
		<UsersListModal
			:show="showMembers"
			:users="watchingClub.members"
			:context-menu-authorization-checker="currentUserIsModerator"
			:user-has-full-access="currentUserIsGroupOwner()"
			:context-actions="membersActions"
			title="Участники сообщества"
			@close="showMembers = false"
		/>
		<UsersListModal
			:show="showModerators"
			:users="watchingClub.moderators"
			:context-menu-authorization-checker="currentUserIsGroupOwner"
			:user-has-full-access="currentUserIsGroupOwner()"
			:context-actions="moderatorsActions"
			title="Модераторы сообщества"
			@close="showModerators = false"
		/>
		<MassStudyClubAddingForm
			ref="massAddForm"
			:show="showMassAddingForm"
			:study-club-id="watchingClub.id"
			@close="closeMassAddingForm"
			@load="reloadStudyClubWithMessage('Добавление прошло успешно')"
		/>
		<MassStudyClubDeletingForm
			ref="massDeleteForm"
			:show="showMassDeletingForm"
			:study-club-id="watchingClub.id"
			@close="closeMassDeletingForm"
			@load="reloadStudyClubWithMessage('Исключение прошло успешно')"
		/>
		<v-dialog
			v-model="showEditingForm"
			persistent
			max-width="600px"
		>
			<v-card>
				<v-card-title class="d-flex justify-center">
					<span class="text-h5">Редактировать информацию о сообществе</span>
				</v-card-title>
				<v-card-text>
					<h2 class="text-center red--text">{{errorText}}</h2>
					<v-container>
						<v-form
							lazy-validation
							ref="submitForm"
							v-model="formValid">
							<v-col cols="12">
								<v-text-field
									label="Название сообщества курса*"
									required
									:rules="commonRules"
									hide-details="auto"
									v-model="updatingClub.clubName"
								></v-text-field>
							</v-col>
							<v-col cols="12">
								<v-textarea
									label="Описание"
									hide-details="auto"
									v-model="updatingClub.description"
								></v-textarea>
							</v-col>
							<v-col cols="12">
								<v-file-input
									:rules="photoRules"
									show-size
									accept="image/png, image/jpeg, image/bmp"
									placeholder="Выберите фото"
									prepend-icon="mdi-camera"
									hide-details="auto"
									label="Фото"
									@change="changePreview"
									v-model="updatingClub.image"
								></v-file-input>
								<div class="mt-5" v-if="previewImage !== null">
									<h4 class="black--text">Предпросмотр изображения сообщества</h4>
									<v-row class="mt-3">
										<v-col class="d-flex d-sm-block col-sm-6 col-12 justify-center">
											<v-img
												contain
												max-width="200"
												:src="previewImage"
											>
											</v-img>
										</v-col>
										<v-col class="d-flex d-sm-block col-sm-6 col-12 justify-center">
											<v-avatar
												class="align-self-center"
												contain
											>
												<v-img
													:src="previewImage"
													class="updating-image-preview"
												>
												</v-img>
											</v-avatar>
										</v-col>
									</v-row>
								</div>
							</v-col>
							<v-col
								cols="12"
							>
								<v-autocomplete
									:items="this.watchingClub.moderators"
									:item-text="getUserFullName"
									item-value="id"
									label="Выберите нового владельца"
									:rules="commonRules"
									v-model="updatingClub.ownerId"
								>
								</v-autocomplete>
								<b class="red--text">
									ОСТОРОЖНО! При изменении последнего поля
									Вы потеряете управляющий доступ к сообществу. Выбор доступен только из
									модераторов.
								</b>
							</v-col>
						</v-form>
					</v-container>
					<small>Поля, помеченные * обязательны к заполнению</small>
				</v-card-text>
				<v-card-actions>
					<v-spacer></v-spacer>
					<v-btn
						color="blue darken-1"
						text
						@click="showEditingForm = false"
					>
						Закрыть
					</v-btn>
					<v-btn
						color="blue darken-1"
						text
						@click="editClub"
						:disabled="!formValid"
					>
						Сохранить
					</v-btn>
				</v-card-actions>
			</v-card>
		</v-dialog>
		<ErrorPage v-if="clubNotFound" error-code="404" message="Сообщество курса не найдено"/>
		<v-container
			fluid
			v-if="Object.keys(watchingClub).length !== 0"
		>
			<v-row class="d-flex justify-start ma-1 mt-3">
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
					@click="openEditForm"
				>
					<v-icon>
						mdi-pencil
					</v-icon>
				</v-btn>
				<v-btn
					v-if="currentUserIsGroupOwner()"
					class="mx-2"
					color="primary"
					@click="openMassAddingForm"
				>
					<v-icon left>
						mdi-account-multiple-plus
					</v-icon>
					Массовое добавление
				</v-btn>
				<v-btn
					v-if="currentUserIsGroupOwner()"
					class="mx-2 white--text"
					color="red"
					@click="openMassDeletingForm"
				>
					<v-icon left>
						mdi-account-multiple-remove
					</v-icon>
					Массовое исключение
				</v-btn>
				<v-spacer class="d-block"></v-spacer>
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
					<v-col class="mt-0">
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
						aspect-ratio="1"
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
import MassStudyClubAddingForm from "@/components/AccountComponents/teacherComponents/MassStudyClubAddingForm";
import MassStudyClubDeletingForm from "@/components/AccountComponents/teacherComponents/MassStudyClubDeletingForm";
export default {
	name: "StudyClubView",
	components: {
		MassStudyClubDeletingForm,
		MassStudyClubAddingForm, UsersListModal, UserInClubLookupPresenter, TeacherVerificationMark, ErrorPage},
	data() {
		return {
			watchingClub: {},
			updatingClub: {
				clubName: "",
				clubDescription: "",
				clubImage: null,
				ownerId: null
			},
			errorText: "",
			photoRules: [
				value => !value || value.size < 2000000 || 'Размер фото превышает 2 МБ!'
			],
			commonRules: [
				v => !!v || 'Поле является обязательным для заполнения'
			],
			previewImage: null,
			formValid: true,
			clubNotFound: false,
			showMembers: false,
			showModerators: false,
			showEditingForm: false,
			showMassAddingForm: false,
			showMassDeletingForm: false,
			membersActions: [
				{
					title: "Удалить из сообщества",
					method: this.removeFromClub,
					requireFullAccess: false
				},
				{
					title: "Сделать модератором",
					method: this.promoteToModerator,
					requireFullAccess: true
				}
			],
			moderatorsActions: [
				{
					title: "Понизить до обычного пользователя",
					method: this.demoteFromModerator,
					requireFullAccess: false
				}
			]
		}
	},
	methods: {
		getOwnersFullName() {
			return this.watchingClub.owner.firstName + " " + this.watchingClub.owner.lastName;
		},
 		currentUserIsGroupOwner() {
			return this.watchingClub.ownerId == this.$oidc.currentUserId;
		},
		currentUserIsMember() {
			return this.watchingClub.members.find(user => user.id == this.$oidc.currentUserId) !== undefined;
		},
		currentUserIsModerator() {
			return this.watchingClub.moderators.find(user => user.id == this.$oidc.currentUserId) !== undefined;
		},
		getUserFullName(user) {
			return user.firstName + " " + user.lastName;
		},
		joinStudyClub() {
			this.$loading(true);
			this.$store.dispatch('joinStudyClub', {
				studyClubId: this.watchingClub.id,
				userId: this.$oidc.currentUserId
			})
				.then(() => {
					this.reloadStudyClubWithMessage('Вы успешно вступили в сообщество!');
				})
				.catch((error) => {
					this.$notify({
						group: 'admin-actions',
						title: 'Ошибка',
						text: error.response.data.error,
						type: 'error'
					});
					this.$loading(false);
				})
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
				studyClubId: this.watchingClub.id,
				userId: this.$oidc.currentUserId
			})
				.then(() => {
					this.reloadStudyClubWithMessage('Вы успешно покинули сообщество!');
				})
				.catch((error) => {
					this.$notify({
						group: 'admin-actions',
						title: 'Ошибка',
						text: error.response.data.error,
						type: 'error'
					});
					this.$loading(false);
				})
		},
		reloadStudyClubWithMessage(message) {
			this.$store.dispatch('loadStudyClubById', this.watchingClub.id)
				.then((response) => {
					this.watchingClub = response.data;
					this.$notify({
						group: 'admin-actions',
						title: 'Успешная операция',
						text: message,
						type: 'success'
					});
					this.$loading(false);
				})
		},
		changePreview(payload) {
			if (payload) {
				this.previewImage = URL.createObjectURL(payload);
				URL.revokeObjectURL(payload);
				return;
			}
			this.previewImage = null;
		},
		openEditForm() {
			this.updatingClub = JSON.parse(JSON.stringify(this.watchingClub));
			this.showEditingForm = true;
		},
		editClub() {
			this.formValid = this.$refs.submitForm.validate();
			if(!this.formValid)
				return;
			this.errorText = "";
			this.$loading(true);
			this.updatingClub.issuerId = this.$oidc.currentUserId;
			this.$store.dispatch('updateStudyClub', this.updatingClub)
				.then(() => {
					this.$store.dispatch('loadStudyClubById', this.watchingClub.id)
						.then((response) => {
							this.watchingClub = response.data;
							this.showEditingForm = false;
							this.$loading(false);
							this.resetUpdatingClub();
						})
				})
				.catch(() => {
					this.errorText = "Произошла ошибка обновления информации о сообществе. Перепроверьте ввод.";
					this.$loading(false);
				});
		},
		resetUpdatingClub() {
			this.updatingClub.clubImage = null;
			this.updatingClub.clubName = "";
			this.updatingClub.clubDescription = "";
			this.updatingClub.ownerId = null;
			this.updatingClub.image = null;
			this.previewImage = null;
			this.updatingClub.issuerId = null;
		},
		removeFromClub(userId) {
			this.$loading(true);
			this.$store.dispatch('removeUserFromStudyClub', {
				studyClubId: this.watchingClub.id,
				issuerId: this.$oidc.currentUserId,
				removingUserId: userId
			})
				.then(() => {
					this.reloadStudyClubWithMessage('Пользователь успешно исключен из сообщества!');
				})
				.catch((error) => {
					this.$notify({
						group: 'admin-actions',
						title: 'Ошибка',
						text: error.response.data.error,
						type: 'error'
					});
					this.$loading(false);
				})
		},
		promoteToModerator(userId) {
			this.$loading(true);
			this.$store.dispatch('addModeratorToStudyClub', {
				studyClubId: this.watchingClub.id,
				issuerId: this.$oidc.currentUserId,
				moderatorId: userId
			})
				.then(() => {
					this.reloadStudyClubWithMessage('Пользователь был успешно повышен до модератора!');
				})
				.catch((error) => {
					this.$notify({
						group: 'admin-actions',
						title: 'Ошибка',
						text: error.response.data.error,
						type: 'error'
					});
					this.$loading(false);
				})
		},
		demoteFromModerator(userId) {
			this.$loading(true);
			this.$store.dispatch('demoteModeratorAtStudyClub', {
				studyClubId: this.watchingClub.id,
				issuerId: this.$oidc.currentUserId,
				moderatorId: userId
			})
				.then(() => {
					this.reloadStudyClubWithMessage('Пользователь был успешно снят с должности модератора!');
				})
				.catch((error) => {
					this.$notify({
						group: 'admin-actions',
						title: 'Ошибка',
						text: error.response.data.error,
						type: 'error'
					});
					this.$loading(false);
				})
		},
		openMassAddingForm() {
			this.showMassAddingForm = true;
			this.$refs.massAddForm.loadData();
		},
		openMassDeletingForm() {
			this.showMassDeletingForm = true;
			this.$refs.massDeleteForm.loadData();
		},
		closeMassAddingForm() {
			this.showMassAddingForm = false;
			this.$refs.massAddForm.resetData();
		},
		closeMassDeletingForm() {
			this.showMassDeletingForm = false;
			this.$refs.massDeleteForm.resetData();
		}
	},
	mounted() {
		let id = this.$route.params.id;
		this.$loading(true);
		this.$store.dispatch('loadStudyClubById', id)
			.then((response) => {
				this.watchingClub = response.data;
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
	.updating-image-preview {
		border-style: solid;
	}
</style>