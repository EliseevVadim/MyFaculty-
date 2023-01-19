<template>
	<v-container
		fluid
		class="h-100"
		v-if="Object.keys(myProfile).length !== 0"
	>
		<v-dialog
			v-model="showEditingForm"
			persistent
			max-width="600px"
		>
			<v-card>
				<v-card-title class="d-flex justify-center">
					<span class="text-h5">Редактировать профиль</span>
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
									label="Имя*"
									required
									:rules="commonRules"
									hide-details="auto"
									v-model="myProfile.firstName"
								></v-text-field>
							</v-col>
							<v-col cols="12">
								<v-text-field
									label="Фамилия*"
									required
									:rules="commonRules"
									hide-details="auto"
									v-model="myProfile.lastName"
								></v-text-field>
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
									v-model="myProfile.photo"
								></v-file-input>
								<div class="mt-5" v-if="previewImage !== null">
									<h4 class="black--text">Предпросмотр аватара</h4>
									<v-row class="mt-3">
										<v-col class="d-flex d-sm-block col-sm-6 col-12 justify-center">
											<v-img
												contain
												max-width="200"
												:src="this.previewImage"
											>
											</v-img>
										</v-col>
										<v-col class="d-flex d-sm-block col-sm-6 col-12 justify-center">
											<v-avatar
												class="align-self-center"
												contain
											>
												<v-img
													:src="this.previewImage"
													class="avatar-preview"
												>
												</v-img>
											</v-avatar>
										</v-col>
									</v-row>
								</div>
							</v-col>
							<v-col cols="12">
								<v-text-field
									label="Email*"
									:rules="emailRules"
									required
									v-model="myProfile.email"
								></v-text-field>
							</v-col>
							<v-col cols="12" class="text-center">
								<h3 class="black--text mb-3">Укажите свою дату рождения</h3>
								<v-date-picker
									v-model="myProfile.birthDate"
								>
								</v-date-picker>
							</v-col>
							<v-col
								cols="12"
							>
								<v-autocomplete
									:items="this.COUNTRIES.countries"
									item-text="countryName"
									item-value="id"
									label="Выберите страну"
									v-model="selectedCountryId"
									@change="loadRegionsList"
								></v-autocomplete>
							</v-col>
							<v-col
								v-if="regionsAreLoaded"
								cols="12"
							>
								<v-autocomplete
									label="Регион, область, край"
									required
									:rules="commonRules"
									:items="this.REGIONS.regions"
									item-text="regionName"
									item-value="id"
									hide-details="auto"
									@change="loadCitiesList"
									v-model="selectedRegionId"
								></v-autocomplete>
							</v-col>
							<h1 v-else-if="regionsAreLoaded === false" class="red--text">
								Для данной страны информация о регионах отсутствует.
								Дальнейшее заполнение невозможно.
							</h1>
							<v-col
								v-if="citiesAreLoaded"
								cols="12"
							>
								<v-autocomplete
									label="Город"
									required
									:rules="commonRules"
									:items="this.CITIES.cities"
									item-text="cityName"
									item-value="id"
									hide-details="auto"
									v-model="myProfile.cityId"
								></v-autocomplete>
							</v-col>
							<h1 v-else-if="citiesAreLoaded === false" class="red--text">
								Для данного региона информация о городах отсутствует.
								Дальнейшее заполнение невозможно.
							</h1>
							<v-col
								cols="12"
							>
								<v-autocomplete
									:items="this.FACULTIES.faculties"
									item-text="facultyName"
									item-value="id"
									label="Выберите факультет"
									v-model="selectedFacultyId"
									@change="loadCoursesList"
								></v-autocomplete>
							</v-col>
							<v-col
								v-if="coursesAreLoaded"
								cols="12"
							>
								<v-autocomplete
									label="Курс"
									required
									:items="this.COURSES.courses"
									item-text="courseName"
									item-value="id"
									hide-details="auto"
									v-model="selectedCourseId"
									@change="loadGroupsList"
								></v-autocomplete>
							</v-col>
							<h1 v-else-if="coursesAreLoaded === false" class="red--text">
								Для данного факультета информация о курсах отсутствует.
								Дальнейшее заполнение невозможно.
							</h1>
							<v-col
								v-if="groupsAreLoaded"
								cols="12"
							>
								<v-autocomplete
									label="Группа"
									required
									:items="this.GROUPS.groups"
									item-text="groupName"
									item-value="id"
									hide-details="auto"
									v-model="myProfile.groupId"
								></v-autocomplete>
							</v-col>
							<h1 v-else-if="groupsAreLoaded === false" class="red--text">
								Для данного курса информация о группах отсутствует.
								Дальнейшее заполнение невозможно.
							</h1>
							<v-col cols="12">
								<v-text-field
									label="Личный сайт"
									hide-details="auto"
									v-model="myProfile.website"
								></v-text-field>
							</v-col>
							<v-col cols="12">
								<v-text-field
									label="Профиль в вк"
									hide-details="auto"
									v-model="myProfile.vkLink"
								></v-text-field>
							</v-col>
							<v-col cols="12">
								<v-text-field
									label="Ссылка на телеграмм"
									hide-details="auto"
									v-model="myProfile.telegramLink"
								></v-text-field>
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
						@click="editProfile"
						:disabled="!formValid"
					>
						Сохранить
					</v-btn>
				</v-card-actions>
			</v-card>
		</v-dialog>
		<v-row class="d-flex justify-center">
			<h1
				class="pb-5"
			>
				{{getFullName()}}
				<TeacherVerificationMark v-if="CURRENT_USER.isTeacher"/>
			</h1>
			<v-btn
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
		</v-row>
		<v-row
			class="d-flex"
		>
			<v-col
				class="d-flex d-sm-block col-sm-6 col-12 justify-center"
			>
				<v-img
					contain
					max-width="200"
					:src="CURRENT_USER.avatarPath ? CURRENT_USER.avatarPath : 'img/blank-item.png'"
					class="text-center profile-avatar"
				>
				</v-img>
			</v-col>
			<v-col
				class="d-flex flex-column col-sm-6 col-12"
			>
				<h4 class="info-subheader">Общая информация</h4>
				<v-row class="d-flex justify-center flex-column">
					<v-col class="d-flex pb-0">
						<v-col class="text-left">
							<span class="parameter-name">Дата рождения:</span>
						</v-col>
						<v-col>
							<span class="parameter-value" v-text="prettifyBirthDate()"></span>
						</v-col>
					</v-col>
					<v-col class="d-flex pb-0 pt-0">
						<v-col class="text-left">
							<span class="parameter-name">Город:</span>
						</v-col>
						<v-col>
							<span class="parameter-value">
								{{CURRENT_USER.city === null ? 'не указано' : CURRENT_USER.city.cityName}}
							</span>
						</v-col>
					</v-col>
					<v-col class="d-flex pb-0 pt-0">
						<v-col class="text-left">
							<span class="parameter-name">Страна:</span>
						</v-col>
						<v-col>
							<span class="parameter-value">
								{{CURRENT_USER.city === null ? 'не указано' : CURRENT_USER.city.countryName}}
							</span>
						</v-col>
					</v-col>
				</v-row>
			</v-col>
		</v-row>
		<v-row class="d-flex justify-center mt-5">
			<v-col
				class="d-flex flex-column col-sm-6 col-12"
			>
				<h4 class="info-subheader">Контактная информация</h4>
				<v-row class="d-flex justify-center flex-column">
					<v-col class="d-flex flex-sm-row flex-column pb-0">
						<v-col class="text-left">
							<span class="parameter-name">Email:</span>
						</v-col>
						<v-col>
							<span class="parameter-value" v-if="CURRENT_USER.email === null">
								не указано
							</span>
							<a class="parameter-value" :href="'mailto:' + CURRENT_USER.email" v-else>
								{{CURRENT_USER.email}}
							</a>
						</v-col>
					</v-col>
					<v-col class="d-flex flex-sm-row flex-column pb-0 pt-0">
						<v-col class="text-left">
							<span class="parameter-name">Личный сайт:</span>
						</v-col>
						<v-col>
							<span class="parameter-value" v-if="CURRENT_USER.website === null">
								не указано
							</span>
							<a class="parameter-value" :href="CURRENT_USER.website" v-else>
								{{CURRENT_USER.website}}
							</a>
						</v-col>
					</v-col>
					<v-col class="d-flex flex-sm-row flex-column pb-0 pt-0">
						<v-col class="text-left">
							<span class="parameter-name">Профиль в вк:</span>
						</v-col>
						<v-col>
							<span class="parameter-value" v-if="CURRENT_USER.vkLink === null">
								не указано
							</span>
							<a class="parameter-value" :href="CURRENT_USER.vkLink" v-else>
								{{CURRENT_USER.vkLink}}
							</a>
						</v-col>
					</v-col>
					<v-col class="d-flex flex-sm-row flex-column pb-0 pt-0">
						<v-col class="text-left">
							<span class="parameter-name">Ссылка на телеграмм:</span>
						</v-col>
						<v-col>
							<span class="parameter-value" v-if="CURRENT_USER.telegramLink === null">
								не указано
							</span>
							<a class="parameter-value" :href="CURRENT_USER.telegramLink" v-else>
								{{CURRENT_USER.telegramLink}}
							</a>
						</v-col>
					</v-col>
				</v-row>
			</v-col>
			<v-col
				class="d-flex flex-column col-sm-6 col-12"
			>
				<h4 class="info-subheader">Студенческая информация</h4>
				<v-row class="d-flex justify-center flex-column">
					<v-col class="d-flex flex-sm-row flex-column pb-0">
						<v-col class="text-left">
							<span class="parameter-name">Факультет:</span>
						</v-col>
						<v-col>
							<span class="parameter-value">
								{{CURRENT_USER.faculty === null ? 'не указано' : CURRENT_USER.faculty.facultyName}}
							</span>
						</v-col>
					</v-col>
					<v-col class="d-flex flex-sm-row flex-column pb-0 pt-0">
						<v-col class="text-left">
							<span class="parameter-name">Курс:</span>
						</v-col>
						<v-col>
							<span class="parameter-value">
								{{CURRENT_USER.course === null ? 'не указано' : CURRENT_USER.course.courseName}}
							</span>
						</v-col>
					</v-col>
					<v-col class="d-flex flex-sm-row flex-column pb-0 pt-0">
						<v-col class="text-left">
							<span class="parameter-name">Группа:</span>
						</v-col>
						<v-col>
							<span class="parameter-value">
								{{CURRENT_USER.group === null ? 'не указано' : CURRENT_USER.group.groupName}}
							</span>
						</v-col>
					</v-col>
				</v-row>
			</v-col>
		</v-row>
	</v-container>
</template>

<script>
import {mapGetters} from "vuex";
import TeacherVerificationMark from "@/components/AccountComponents/core/verificationMarks/TeacherVerificationMark";
export default {
	name: "ProfileView",
	components: {TeacherVerificationMark},
	data() {
		return {
			myProfile : {
				firstName: null,
				lastName: null,
				birthDate: null,
				photo: null,
				cityId: null,
				facultyId: null,
				courseId: null,
				groupId: null,
				website: null,
				vkLink: null,
				telegramLink: null
			},
			showEditingForm: false,
			errorText: "",
			formValid: true,
			previewImage: null,
			selectedCountryId: null,
			selectedRegionId: null,
			selectedFacultyId: null,
			selectedCourseId: null,
			regionsAreLoaded: null,
			citiesAreLoaded: null,
			coursesAreLoaded: null,
			groupsAreLoaded: null,
			photoRules: [
				value => !value || value.size < 2000000 || 'Размер фото превышает 2 МБ!'
			],
			emailRules: [
				v => !!v || 'Поле является обязательным для заполнения',
				v => /.+@.+/.test(v) || 'Введенный email некорректен'
			],
			commonRules: [
				v => !!v || 'Поле является обязательным для заполнения'
			],
		}
	},
	methods: {
		getFullName() {
			return this.CURRENT_USER.firstName === undefined || this.CURRENT_USER.lastName === undefined ?
				'' :
				this.CURRENT_USER.firstName + ' ' + this.CURRENT_USER.lastName;
		},
		prettifyBirthDate() {
			if (this.CURRENT_USER.birthDate === null)
				return 'не указано';
			return new Date(this.CURRENT_USER.birthDate).toLocaleString('ru-RU', {
				year: 'numeric',
				month: 'long',
				day: 'numeric'
			});
		},
		openEditForm() {
			this.myProfile = JSON.parse(JSON.stringify(this.CURRENT_USER));
			this.myProfile.birthDate = this.CURRENT_USER.birthDate === null ? null : this.CURRENT_USER.birthDate.slice(0, 10);
			this.$store.dispatch('loadAllCountries');
			if (this.CURRENT_USER.city !== null) {
				this.selectedCountryId = this.CURRENT_USER.city.countryId;
				this.loadRegionsList();
				this.selectedRegionId = this.CURRENT_USER.city.regionId;
				this.loadCitiesList();
			}
			this.$store.dispatch('loadAllFaculties');
			if (this.CURRENT_USER.faculty !== null) {
				this.selectedFacultyId = this.CURRENT_USER.faculty.id;
				this.loadCoursesList();
			}
			if (this.CURRENT_USER.course !== null) {
				this.selectedCourseId = this.CURRENT_USER.course.id;
				this.loadGroupsList();
			}
			this.showEditingForm = true;
			this.previewImage = null;
			this.myProfile.photo = null;
		},
		changePreview(payload) {
			if (payload) {
				this.previewImage = URL.createObjectURL(payload);
				URL.revokeObjectURL(payload);
				return;
			}
			this.previewImage = null;
		},
		loadRegionsList() {
			this.$loading(true);
			this.$store.dispatch('loadRegionsByCountryId', this.selectedCountryId)
				.then(() => {
					this.regionsAreLoaded = this.REGIONS.regions.length > 0;
					this.$loading(false);
				});
		},
		loadCitiesList() {
			this.$loading(true);
			this.$store.dispatch('loadCitiesByRegionId', this.selectedRegionId)
				.then(() => {
					this.citiesAreLoaded = this.CITIES.cities.length > 0;
					this.$loading(false);
				});
		},
		loadCoursesList() {
			this.$loading(true);
			this.$store.dispatch('loadCoursesByFacultyId', this.selectedFacultyId)
				.then(() => {
					this.coursesAreLoaded = this.COURSES.courses.length > 0;
					this.$loading(false);
				});
		},
		loadGroupsList() {
			this.$loading(true);
			this.$store.dispatch('loadGroupsByCourseId', this.selectedCourseId)
				.then(() => {
					this.groupsAreLoaded = this.GROUPS.groups.length > 0;
					this.$loading(false);
				});
		},
		editProfile() {
			this.$loading(true);
			this.myProfile.facultyId = this.selectedFacultyId;
			this.myProfile.courseId = this.selectedCourseId;
			this.$store.dispatch('updateUser', this.myProfile)
				.then(() => {
					this.$store.dispatch('loadCurrentUser', this.$oidc.currentUserId)
						.then(() => {
							this.showEditingForm = false;
							this.$loading(false);
						})
				})
				.catch(() => {
					this.errorText = "Произошла ошибка обновления личной информации. Перепроверьте ввод.";
					this.$loading(false);
				});
		}
	},
	computed: {
		...mapGetters(['CURRENT_USER']),
		...mapGetters(['COUNTRIES']),
		...mapGetters(['REGIONS']),
		...mapGetters(['CITIES']),
		...mapGetters(['FACULTIES']),
		...mapGetters(['COURSES']),
		...mapGetters(['GROUPS'])
	}
}
</script>

<style scoped>
	.info-subheader {
		font-size: 20px;
		font-weight: bolder;
		color: #283593;
	}
	.parameter-name {
		font-weight: bold;
		font-size: 18px;
		text-align: left;
		width: 200px;
	}
	.parameter-value {
		text-align: left;
		font-size: 18px;
	}
	a.parameter-value {
		text-decoration: none;
		color: blue;
	}
	a.parameter-value:visited {
		text-decoration: none;
		color: indigo;
	}
	.avatar-preview {
		border-style: solid;
	}
	.profile-avatar {
		border: double 5px transparent;
		background-image: linear-gradient(white, white),
			radial-gradient(circle at bottom left, red 20%, blue, black);
		background-origin: border-box;
		background-clip: padding-box, border-box;
	}
</style>