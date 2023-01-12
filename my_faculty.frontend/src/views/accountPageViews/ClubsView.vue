<template>
	<div>
		<h1>Мои сообщества курсов</h1>
		<v-card-actions>
			<v-btn
				v-if="isTeacher"
				color="primary"
				class="ma-2 create-club-button"
				to="/create-study-club"
			>
				<v-icon left>
					mdi-plus-circle-outline
				</v-icon>
				Создать новое сообщество
			</v-btn>
		</v-card-actions>
		<v-container>
			<v-row>
				<v-col cols="12">
					<v-text-field
						v-model="searchRequest"
						append-icon="mdi-magnify"
						label="Поиск сообществ курсов"
						type="text"
						clear-icon="mdi-close-circle"
						clearable
						@click:clear="clearRequest"
						@click:append="searchClubs"
						@keypress.enter.prevent="searchClubs"
					></v-text-field>
				</v-col>
			</v-row>
		</v-container>
		<EmptySearchResult v-if="clubsListAreEmpty" :message="emptyResultMessage" />
		<v-list v-else two-line>
			<template v-for="(item, index) in clubs">
				<StudyClubLookupPresenter :item="item"/>
			</template>
		</v-list>
	</div>
</template>

<script>
import StudyClubLookupPresenter from "@/components/presenters/StudyClubLookupPresenter";
import {mapGetters} from "vuex";
import EmptySearchResult from "@/components/AccountComponents/core/service-pages/EmptySearchResult";
export default {
	name: "ClubsView",
	components: {EmptySearchResult, StudyClubLookupPresenter},
	data() {
		return {
			isTeacher: false,
			clubsListAreEmpty: false,
			clubs: [],
			searchRequest: "",
			emptyResultMessage: "Здесь пока ничего нет. Попробуйте поиск по сообществам курсов, чтобы ознакомиться с " +
				"существующими или вступить в интересующие."
		}
	},
	methods: {
		currentUserInClub(clubId) {
			return this.CURRENT_USER.studyClubsAtMembership.find(club => club.id === clubId) !== undefined;
		},
		searchClubs() {
			if (this.searchRequest === "" || this.searchRequest === null)
				return;
			this.$loading(true);
			this.$store.dispatch('loadStudyClubsBySearchQuery', this.searchRequest)
				.then(() => {
					this.clubs = [];
					this.clubs = this.STUDY_CLUBS.studyClubs;
					this.clubs.map((item) => item.currentUserInClub = this.currentUserInClub(item.id));
					this.clubsListAreEmpty = this.clubs.length === 0;
					this.$loading(false);
				})
		},
		clearRequest() {
			this.searchRequest = "";
			this.$store.dispatch('loadCurrentUser', this.$oidc.currentUserId)
				.then(() => {
					this.clubs = this.CURRENT_USER.studyClubsAtMembership;
					this.clubs.map((item) => item.currentUserInClub = this.currentUserInClub(item.id))
					this.clubsListAreEmpty = this.clubs.length === 0;
				})
		}
	},
	async mounted() {
		document.title = "Сообщества курсов";
		this.isTeacher = await this.$oidc.isTeacher();
		this.$loading(true);
		this.$store.dispatch('loadCurrentUser', this.$oidc.currentUserId)
			.then(() => {
				this.clubs = this.CURRENT_USER.studyClubsAtMembership;
				this.clubs.map((item) => item.currentUserInClub = this.currentUserInClub(item.id))
				this.clubsListAreEmpty = this.clubs.length === 0;
				this.$loading(false);
			})
	},
	async updated() {
		this.isTeacher = await this.$oidc.isTeacher();
	},
	computed: {
		...mapGetters(['STUDY_CLUBS']),
		...mapGetters(['CURRENT_USER'])
	}
}
</script>

<style scoped>
	.create-club-button {
		font-weight: bolder;
	}
</style>