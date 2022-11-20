<template>
	<div>
		<h1 v-if="!userAuthorized">
			hell off
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
				<v-menu
					bottom
					left
					offset-y
					origin="top right"
					transition="scale-transition"
				>
					<template v-slot:activator="{ attrs, on }">
						<v-btn
							class="ml-2"
							min-width="0"
							text
							v-bind="attrs"
							v-on="on"
						>
							<v-badge
								color="red"
								overlap
								bordered
							>
								<template v-slot:badge>
									<span>5</span>
								</template>

								<v-icon>mdi-bell</v-icon>
							</v-badge>
						</v-btn>
					</template>
					<v-list
						:tile="false"
						nav
					>
						<div>
							<app-bar-item
								v-for="(n, i) in notifications"
								:key="`item-${i}`"
							>
								<v-list-item-title v-text="n" />
							</app-bar-item>
						</div>
					</v-list>
				</v-menu>
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
								:src="CURRENT_USER.avatarPath === null ? 'img/blank-item.jpg' : CURRENT_USER.avatarPath"
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
						/>
					</template>
					<div />
				</v-list>
				<template v-slot:append>
					<v-btn outlined @click="signOut" color="red" class="mt-3 mb-4">Выйти</v-btn>
				</template>
			</v-navigation-drawer>
			<v-main>
				<v-container
					fluid
					class="personal-page-content"
				>
					<router-view v-if="isFullyMounted" />
				</v-container>
				<PagesFooter />
			</v-main>
		</v-app>
	</div>
</template>
<script>
import ItemGroup from "@/components/AccountComponents/base/ItemGroup";
import Item from "@/components/AccountComponents/base/Item";
import {VHover, VListItem} from "vuetify/lib/components";
import PagesFooter from "@/components/AccountComponents/core/PagesFooter";
import {mapGetters} from "vuex";
export default {
	name: "AccountPageView",
	components: {
		PagesFooter,
		Item, ItemGroup,
		AppBarItem: {
			render(h) {
				return h(VHover, {
					scopedSlots: {
						default: ({hover}) => {
							return h(VListItem, {
								attrs: this.$attrs,
								class: {
									'black--text': !hover,
									'white--text secondary elevation-12': hover,
								},
								props: {
									activeClass: '',
									dark: hover,
									link: true,
									...this.$attrs,
								},
							}, this.$slots.default)
						},
					},
				})
			},
		},
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
			notifications: [
				'Mike John Responded to your email',
				'You have 5 new tasks',
				'You\'re now friends with Andrew',
				'Another Notification',
				'Another one',
			],
			items: [
				{
					icon: 'mdi-home',
					title: 'На главную',
					to: '/',
				},
				{
					icon: 'mdi-account',
					title: 'Профиль',
					to: '/personal',
				},
				{
					title: 'Новости',
					icon: 'mdi-clipboard-outline',
					to: '/news',
				},
				{
					title: 'Сообщества курсов',
					icon: 'mdi-cast-education',
					to: '/clubs',
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
		...mapGetters(['CURRENT_USER'])
	}
}
</script>

<style lang="sass">
@import '~vuetify/src/styles/tools/_rtl.sass'
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
</style>