<template>
	<div>
		<h1 v-if="!userAuthorized">
			Для просмотра содержимого необходимо авторизоваться. Перенаправляем Вас...
		</h1>
		<v-app v-else>
			<v-app-bar
				id="app-bar"
				app
				height="75"
				color="white"
			>
				<v-btn
					class="mr-3"
					elevation="1"
					fab
					small
					@click="setDrawer(!drawer)"
				>
					<v-icon>
						mdi-dots-vertical
					</v-icon>
				</v-btn>
				<v-toolbar-title
					class="hidden-sm-and-down font-weight-light"
					v-text="$route.name"
				/>
				<v-spacer />
				<div class="mx-3" />
				<v-btn
					class="ml-2"
					min-width="0"
					text
					to="/"
				>
					<v-icon>mdi-home</v-icon>
				</v-btn>
				<NotificationsList/>
				<v-btn
					class="ml-2"
					min-width="0"
					text
					to="/personal"
				>
					<v-icon>mdi-account</v-icon>
				</v-btn>
			</v-app-bar>
			<v-navigation-drawer
				id="core-navigation-drawer"
				app
				v-model="drawer"
				:expand-on-hover="false"
				:right="$vuetify.rtl"
				dark
				width="280"
				v-bind="$attrs"
			>
				<template v-slot:img="props">
					<v-img
						:gradient="`to bottom, red`"
						v-bind="props"
					/>
				</template>
				<v-divider class="mb-1" />
				<v-list
					dense
					nav
				>
					<v-list-item>
						<v-list-item-avatar
							class="align-self-center"
							color="white"
							contain
						>
							<v-img
								class="user-avatar"
								:src="CURRENT_USER.avatarPath ? CURRENT_USER.avatarPath : '../img/blank-item.png'"
							/>
						</v-list-item-avatar>
						<v-list-item-content>
							<v-list-item-title
								v-text="getFullName()"
								class="profile-name"
							/>
						</v-list-item-content>
					</v-list-item>
				</v-list>
				<v-divider class="mb-2" />
				<v-list
					expand
					nav
				>
					<template v-for="(item, i) in items">
						<ItemGroup
							v-if="item.children"
							:key="`group-${i}`"
							:item="item"
						>
						</ItemGroup>
						<Item
							v-else
							:key="`item-${i}`"
							:item="item"
							:restricted="item.restricted"
							:restrict-text="item.restrictExplanation"
						/>
					</template>
					<Item
						:item="{
							title: 'Мое расписание',
							icon: 'mdi-list-box-outline',
							to: '/schedule'
						}"
						:restricted="!CURRENT_USER.groupId"
						restrict-text="Раздел доступен только при указанной группе в профиле."
					/>
					<div />
				</v-list>
				<template v-slot:append>
					<div class="pa-1">
						<router-link
							to="/teacher-verification"
							class="verification_suggestion"
						>
							Преподаватель? Верифицируйте свой аккаунт!
						</router-link>
					</div>
					<v-btn outlined @click="signOut" color="red" class="mt-3 mb-4">Выйти</v-btn>
				</template>
			</v-navigation-drawer>
			<v-main>
				<v-container
					fluid
					class="personal-page-content main-content-page"
				>
					<router-view :key="$route.path + $route.hash" v-if="isFullyMounted" />
				</v-container>
				<PagesFooter />
			</v-main>
		</v-app>
	</div>
</template>
<script>
import ItemGroup from "@/components/AccountComponents/base/ItemGroup";
import Item from "@/components/AccountComponents/base/Item";
import PagesFooter from "@/components/AccountComponents/core/PagesFooter";
import {mapGetters} from "vuex";
import ErrorPage from "@/components/AccountComponents/core/service-pages/ErrorPage";
import NotificationsList from "@/components/AccountComponents/NotificationsList";
export default {
	name: "AccountPageView",
	components: {
		NotificationsList,
		ErrorPage,
		PagesFooter,
		Item, ItemGroup
	},
	data() {
		return {
			drawer: true,
			userAuthorized: true,
			isFullyMounted : false,
			userProfile: {
				firstName: 'Загрузка',
				lastName: '...',
			},
			items: [
				{
					icon: 'mdi-home',
					title: 'На главную',
					to: '/',
					restricted: false,
					restrictExplanation: ''
				},
				{
					icon: 'mdi-account',
					title: 'Профиль',
					to: '/personal',
					restricted: false,
					restrictExplanation: ''
				},
				{
					title: 'Новости',
					icon: 'mdi-clipboard-outline',
					to: '/news',
					restricted: false,
					restrictExplanation: ''
				},
				{
					title: 'Сообщества курсов',
					icon: 'mdi-cast-education',
					to: '/clubs',
					restricted: false,
					restrictExplanation: ''
				},
				{
					title: 'Сообщества',
					icon: 'mdi-account-group',
					to: '/communities',
					restricted: false,
					restrictExplanation: ''
				}
			]
		}
	},
	methods: {
		setDrawer(value) {
			this.drawer = value;
		},
		getFullName() {
			return this.CURRENT_USER.firstName === undefined || this.CURRENT_USER.lastName === undefined ?
				'Загрузка...' :
				this.CURRENT_USER.firstName + ' ' + this.CURRENT_USER.lastName;
		},
		signOut() {
			this.$oidc.logout();
		}
	},
	async mounted() {
		try {
			document.title = "Личный кабинет";
			this.userAuthorized = await this.$oidc.isUser();
			if (!this.userAuthorized)
				await this.$oidc.login();
			let profile = await this.$oidc.getUserProfile();
			let id = profile.sub;
			this.$loading(true);
			await this.$store.dispatch('loadCurrentUser', id)
				.then(() => {
					this.isFullyMounted = true;
					this.$oidc.currentUserId = id;
					this.$loading(false);
				})
		}
		catch (error) {}
	},
	computed: {
		...mapGetters(['CURRENT_USER']),
	}
}
</script>

<style lang="sass">
@import '~vuetify/src/styles/tools/_rtl.sass'
.user-avatar
	border: double 3px transparent
	background-image: linear-gradient(white, white), radial-gradient(circle at bottom left, red 20%, blue, black)
	background-origin: border-box
	background-clip: padding-box, border-box
.v-main__wrap
	display: flex
	flex-direction: column
	height: 100%
	.personal-page-content
		flex: 1 0 auto
#core-navigation-drawer
	padding: 0
	.profile-name
		font-weight: bold
		font-size: 18px
		line-height: 30px
	.v-list-item
		text-align: left
	.v-list-group__header.v-list-item--active:before
		opacity: .24
	.v-list-item
		&__icon--text,
		&__icon:first-child
			justify-content: center
			text-align: center
			width: 20px
			+ltr()
				margin-right: 24px
				margin-left: 12px !important
			+rtl()
				margin-left: 24px
				margin-right: 12px !important
	.v-list--dense
		.v-list-item
			&__icon--text,
			&__icon:first-child
				margin-top: 10px
	.v-list-group--sub-group
		.v-list-item
			+ltr()
				padding-left: 8px
			+rtl()
				padding-right: 8px
		.v-list-group__header
			+ltr()
				padding-right: 0
			+rtl()
				padding-right: 0
			.v-list-item__icon--text
				margin-top: 19px
				order: 0
			.v-list-group__header__prepend-icon
				order: 2
				+ltr()
					margin-right: 8px
				+rtl()
					margin-left: 8px
		#dashboard-core-footer
			a
				font-size: .825rem
				font-weight: 500
				text-decoration: none
				text-transform: uppercase
.verification_suggestion
	color: white !important
	font-size: 12px
.main-content-page
	background: #edeef0
</style>