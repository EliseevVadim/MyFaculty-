<template>
	<div>
		<ErrorPage v-if="userNotFound" errorCode="404" message="Пользователь не найден"></ErrorPage>
		<v-container
			v-else
			fluid
			class="h-100"
			v-if="Object.keys(watchingProfile).length !== 0"
		>
			<v-row class="d-flex justify-center">
				<h1
					class="pb-5"
				>
					{{getFullName()}}
					<TeacherVerificationMark v-if="watchingProfile.isTeacher"/>
				</h1>
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
						:src="watchingProfile.avatarPath ? watchingProfile.avatarPath : 'img/blank-item.png'"
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
								{{watchingProfile.city === null ? 'не указано' : watchingProfile.city.cityName}}
							</span>
							</v-col>
						</v-col>
						<v-col class="d-flex pb-0 pt-0">
							<v-col class="text-left">
								<span class="parameter-name">Страна:</span>
							</v-col>
							<v-col>
							<span class="parameter-value">
								{{watchingProfile.city === null ? 'не указано' : watchingProfile.city.countryName}}
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
							<span class="parameter-value" v-if="watchingProfile.email === null">
								не указано
							</span>
								<a class="parameter-value" :href="'mailto:' + watchingProfile.email" v-else>
									{{watchingProfile.email}}
								</a>
							</v-col>
						</v-col>
						<v-col class="d-flex flex-sm-row flex-column pb-0 pt-0">
							<v-col class="text-left">
								<span class="parameter-name">Личный сайт:</span>
							</v-col>
							<v-col>
							<span class="parameter-value" v-if="watchingProfile.website === null">
								не указано
							</span>
								<a class="parameter-value" :href="watchingProfile.website" v-else>
									{{watchingProfile.website}}
								</a>
							</v-col>
						</v-col>
						<v-col class="d-flex flex-sm-row flex-column pb-0 pt-0">
							<v-col class="text-left">
								<span class="parameter-name">Профиль в вк:</span>
							</v-col>
							<v-col>
							<span class="parameter-value" v-if="watchingProfile.vkLink === null">
								не указано
							</span>
								<a class="parameter-value" :href="watchingProfile.vkLink" v-else>
									{{watchingProfile.vkLink}}
								</a>
							</v-col>
						</v-col>
						<v-col class="d-flex flex-sm-row flex-column pb-0 pt-0">
							<v-col class="text-left">
								<span class="parameter-name">Ссылка на телеграмм:</span>
							</v-col>
							<v-col>
							<span class="parameter-value" v-if="watchingProfile.telegramLink === null">
								не указано
							</span>
								<a class="parameter-value" :href="watchingProfile.telegramLink" v-else>
									{{watchingProfile.telegramLink}}
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
								{{watchingProfile.faculty === null ? 'не указано' : watchingProfile.faculty.facultyName}}
							</span>
							</v-col>
						</v-col>
						<v-col class="d-flex flex-sm-row flex-column pb-0 pt-0">
							<v-col class="text-left">
								<span class="parameter-name">Курс:</span>
							</v-col>
							<v-col>
							<span class="parameter-value">
								{{watchingProfile.course === null ? 'не указано' : watchingProfile.course.courseName}}
							</span>
							</v-col>
						</v-col>
						<v-col class="d-flex flex-sm-row flex-column pb-0 pt-0">
							<v-col class="text-left">
								<span class="parameter-name">Группа:</span>
							</v-col>
							<v-col>
							<span class="parameter-value">
								{{watchingProfile.group === null ? 'не указано' : watchingProfile.group.groupName}}
							</span>
							</v-col>
						</v-col>
					</v-row>
				</v-col>
			</v-row>
		</v-container>
	</div>
</template>

<script>
import ErrorPage from "@/components/AccountComponents/core/service-pages/ErrorPage";
import TeacherVerificationMark from "@/components/AccountComponents/core/verificationMarks/TeacherVerificationMark";
export default {
	name: "UserView",
	components: {TeacherVerificationMark, ErrorPage},
	data() {
		return {
			watchingProfile: {},
			userNotFound: false
		}
	},
	methods: {
		getFullName() {
			return this.watchingProfile.firstName === undefined || this.watchingProfile.lastName === undefined ?
				'' :
				this.watchingProfile.firstName + ' ' + this.watchingProfile.lastName;
		},
		prettifyBirthDate() {
			if (this.watchingProfile.birthDate === null)
				return 'не указано';
			return new Date(this.watchingProfile.birthDate).toLocaleString('ru-RU', {
				year: 'numeric',
				month: 'long',
				day: 'numeric'
			});
		}
	},
	mounted() {
		let id = this.$route.params.id
		if (id === this.$oidc.currentUserId)
			this.$router.push('/personal');
		this.$loading(true);
		this.$store.dispatch('loadUserById', id)
			.then((response) => {
				this.watchingProfile = response.data;
				document.title = this.getFullName();
			})
			.catch((error) => {
				if (error.response.status === 404) {
					document.title = "Пользователь не найден";
					this.userNotFound = true;
				}
			})
			.finally(() => {
				this.$loading(false);
			})
	}
}
</script>

<style scoped>
	.profile-avatar {
		border: double 5px transparent;
		background-image: linear-gradient(white, white),
			radial-gradient(circle at bottom left, red 20%, blue, black);
		background-origin: border-box;
		background-clip: padding-box, border-box;
	}
</style>