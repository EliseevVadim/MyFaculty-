<template>
	<div>
		<h1>Мои новости</h1>
		<v-row class="mt-1">
			<v-tabs
				:vertical="$vuetify.breakpoint.mobile"
				background-color="transparent"
				color="black"
				grow
			>
				<v-tabs-slider color="purple accent-4"></v-tabs-slider>
				<v-tab
					class="font-weight-bold"
					@click="selectInfoPostsDisplay"
				>
					Информационные записи
				</v-tab>
				<v-tab
					class="font-weight-bold"
					@click="selectClubTasksDisplay"
				>
					Задания
				</v-tab>
			</v-tabs>
		</v-row>
		<v-container class="mt-4 text-left mx-0">
			<div v-if="displayingContent === newsfeedContentType.InfoPosts">
				<EmptySearchResult
					v-if="informationPosts.length === 0"
					message="Записей пока нет"
				/>
				<InformationPostPresenter
					v-else
					v-for="infoPost in informationPosts"
					:key="infoPost.id + infoPost.updated + infoPost.likedUsers.length"
					:post="infoPost"
					@load="loadInfoPosts"
				/>
			</div>
			<div v-else-if="displayingContent === newsfeedContentType.ClubTasks">
				<EmptySearchResult
					v-if="clubTasks.length === 0"
					message="Заданий пока нет"
				/>
				<ClubTaskPresenter
					v-else
					v-for="clubTask in clubTasks"
					:key="clubTask.id + clubTask.updated"
					:task="clubTask"
					@load="loadClubTasks"
				/>
			</div>
			<h1
				v-else
				class="red--text text-center"
			>
				Ошибка состояния
			</h1>
		</v-container>
	</div>
</template>

<script>
import {mapGetters} from "vuex";
import ClubTaskPresenter from "@/components/presenters/ClubTaskPresenter";
import EmptySearchResult from "@/components/AccountComponents/core/service-pages/EmptySearchResult";
import InformationPostPresenter from "@/components/presenters/InformationPostPresenter";

export default {
	name: "NewsView",
	components: {InformationPostPresenter, EmptySearchResult, ClubTaskPresenter},
	data() {
		return {
			newsfeedContentType: {
				InfoPosts: 0,
				ClubTasks: 1
			},
			displayingContent: null,
			informationPosts: [],
			clubTasks: []
		}
	},
	methods: {
		selectInfoPostsDisplay() {
			this.displayingContent = this.newsfeedContentType.InfoPosts;
			this.loadInfoPosts();
		},
		selectClubTasksDisplay() {
			this.displayingContent = this.newsfeedContentType.ClubTasks;
			this.loadClubTasks();
		},
		loadInfoPosts() {
			this.$store.dispatch('loadInfoPostsNewsfeed')
				.then(() => {
					this.informationPosts = JSON.parse(JSON.stringify(this.INFO_POSTS.infoPosts));
				});
		},
		loadClubTasks() {
			this.$store.dispatch('loadClubTasksNewsfeed')
				.then(() => {
					this.clubTasks = JSON.parse(JSON.stringify(this.CLUB_TASKS.clubTasks));
				})
		}
	},
	mounted() {
		this.displayingContent = this.newsfeedContentType.InfoPosts;
		this.loadInfoPosts();
	},
	computed: {
		...mapGetters(['INFO_POSTS']),
		...mapGetters(['CLUB_TASKS'])
	}
}
</script>

<style scoped>

</style>