<template>
	<div>
		<h1>Мои информационные сообщества</h1>
		<v-card-actions>
			<v-btn
				color="primary"
				class="ma-2 create-club-button"
				to="/create-info-public"
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
						label="Поиск информационных сообществ"
						type="text"
						clear-icon="mdi-close-circle"
						clearable
						@click:clear="clearRequest"
						@click:append="searchPublics"
						@keypress.enter.prevent="searchPublics"
					></v-text-field>
				</v-col>
			</v-row>
		</v-container>
		<EmptySearchResult v-if="publicsListAreEmpty" :message="emptyResultMessage" />
		<v-list v-else two-line>
			<template v-for="(item, index) in infoPublics">
				<InfoPublicLookupPresenter :item="item"/>
			</template>
		</v-list>
	</div>
</template>

<script>
import EmptySearchResult from "@/components/AccountComponents/core/service-pages/EmptySearchResult";
import InfoPublicLookupPresenter from "@/components/presenters/InfoPublicLookupPresenter";
import {mapGetters} from "vuex";
export default {
	name: "PublicsView",
	components: {InfoPublicLookupPresenter, EmptySearchResult},
	data() {
		return {
			publicsListAreEmpty: false,
			infoPublics: [],
			searchRequest: "",
			emptyResultMessage: "Здесь пока ничего нет. Попробуйте поиск по информационным сообществам, чтобы " +
				"ознакомиться с существующими или вступить в интересующие."
		}
	},
	methods: {
		currentUserInPublic(publicId) {
			return this.CURRENT_USER.informationPublicsAtMembership
				.find(infoPublic => infoPublic.id === publicId) !== undefined;
		},
		searchPublics() {
			if (this.searchRequest === "" || this.searchRequest === null)
				return;
			this.$loading(true);
			this.$store.dispatch('loadInfoPublicsBySearchQuery', this.searchRequest)
				.then(() => {
					this.infoPublics = [];
					this.infoPublics = this.INFO_PUBLICS.informationPublics;
					this.infoPublics.map((item) => item.currentUserInPublic = this.currentUserInPublic(item.id));
					this.publicsListAreEmpty = this.infoPublics.length === 0;
					this.$loading(false);
				})
		},
		clearRequest() {
			this.searchRequest = "";
			this.$store.dispatch('loadCurrentUser', this.$oidc.currentUserId)
				.then(() => {
					this.infoPublics = this.CURRENT_USER.informationPublicsAtMembership;
					this.infoPublics.map((item) => item.currentUserInPublic = this.currentUserInPublic(item.id))
					this.publicsListAreEmpty = this.infoPublics.length === 0;
				})
		}
	},
	mounted() {
		document.title = "Информационные сообщества";
		this.$loading(true);
		this.$store.dispatch('loadCurrentUser', this.$oidc.currentUserId)
			.then(() => {
				this.infoPublics = this.CURRENT_USER.informationPublicsAtMembership;
				this.infoPublics.map((item) => item.currentUserInPublic = this.currentUserInPublic(item.id))
				this.publicsListAreEmpty = this.infoPublics.length === 0;
				this.$loading(false);
			})
	},
	computed: {
		...mapGetters(['INFO_PUBLICS']),
		...mapGetters(['CURRENT_USER'])
	}
}
</script>

<style scoped>

</style>