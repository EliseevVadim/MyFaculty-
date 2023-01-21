<template>
    <div>
		<NotAuthorizedAdminPage v-if="!userAuthorized"/>
        <v-app v-else>
            <v-app-bar
                color="deep-purple accent-4"
                elevation="24"
                dense
                dark
                class="flex-grow-0"
            >
                <v-app-bar-nav-icon @click.stop="drawer = !drawer"></v-app-bar-nav-icon>
                <v-spacer></v-spacer>
                <v-menu
                    bottom
                    left
                >
                    <template v-slot:activator="{ on, attrs }">
                        <v-btn
                                tile
                                v-bind="attrs"
                                v-on="on"
                        >
                            <v-icon left>
                                mdi-account-circle
                            </v-icon>
                            Добро пожаловать, {{userName}}
                        </v-btn>
                    </template>
                    <v-list>
                        <v-list-item-group
                                active-class="deep-purple--text text--accent-4"
                        >
                            <v-list-item @click="logout">
                                <v-list-item-icon>
                                    <v-icon>mdi-logout</v-icon>
                                </v-list-item-icon>
                                <v-list-item-content>
                                    <v-list-item-title class="text-left">Выход</v-list-item-title>
                                </v-list-item-content>
                            </v-list-item>
                        </v-list-item-group>
                    </v-list>
                </v-menu>
            </v-app-bar>
            <v-navigation-drawer
                v-model="drawer"
                absolute
                bottom
                temporary
            >
                <v-list
                    flat
                    nav
                    dense
                >
                    <v-subheader>Управление приложением</v-subheader>
                    <v-divider></v-divider>
                    <v-list-item-group
                        active-class="deep-purple--text text--accent-4"
                    >
                        <v-list-item @click="hideSidebar"
                            v-for="(item, i) in items"
                            :key="i"
                            :to="item.path">
                            <v-list-item-icon>
                                <v-icon v-text="item.icon"></v-icon>
                            </v-list-item-icon>
                            <v-list-item-content>
                                <v-list-item-title class="text-left" v-text="item.text"></v-list-item-title>
                            </v-list-item-content>
                        </v-list-item>
                    </v-list-item-group>
                    <v-divider></v-divider>
                    <v-list-item-group
                        :value="false"
                        prepend-icon="mdi-tools"
                    >
                            <v-subheader>Служебные</v-subheader>
                        <v-list-item
                                v-for="([title, icon, path], i) in services"
                                :key="i"
                                link
                                :to="path"
                                @click="hideSidebar"
                        >
                            <v-list-item-icon>
                                <v-icon v-text="icon"></v-icon>
                            </v-list-item-icon>
                            <v-list-item-title class="text-left" v-text="title"></v-list-item-title>
                        </v-list-item>
                    </v-list-item-group>
                </v-list>
            </v-navigation-drawer>
            <v-main>
                <router-view/>
            </v-main>
        </v-app>
    </div>
</template>

<script>
import NotAuthorizedAdminPage from "@/components/AccountComponents/core/service-pages/NotAuthorizedAdminPage";
export default {
    name: "AdminPageView",
	components: {NotAuthorizedAdminPage},
	data() {
        return {
            userAuthorized: null,
			userName: null,
            drawer: false,
            items: [
				{text : "Факультеты", icon : "mdi-floor-plan", path: '/adminPanel/faculties'},
                {text : "Этажи", icon : "mdi-floor-plan", path: '/adminPanel/floors'},
                {text : "Преподаватели", icon : "mdi-account-school", path: '/adminPanel/teachers'},
                {text : "Аудитории", icon : "mdi-cellphone-link", path: '/adminPanel/auditoriums'},
                {text : "Вторичные объекты", icon : "mdi-stairs", path: '/adminPanel/secondaryObjects'},
                {text : "Пары", icon : "mdi-calendar-check", path: '/adminPanel/pairs'},
                {text : "Дисциплины", icon : "mdi-playlist-check", path: '/adminPanel/disciplines'},
                {text : "Курсы", icon : "mdi-star", path: '/adminPanel/courses'},
                {text : "Группы", icon : "mdi-account-multiple", path: '/adminPanel/groups'}
            ],
            services: [
                ['Ученые звания', 'mdi-trophy-award', '/adminPanel/scienceRanks'],
                ['Информация о парах', 'mdi-information-variant', '/adminPanel/pairInfos'],
                ['Типы вторичных объектов', 'mdi-map-legend', '/adminPanel/secondaryObjectTypes'],
                ['Назначение дисциплин', 'mdi-clipboard-check', '/adminPanel/teachersDisciplines'],
				['Страны', 'mdi-earth', '/adminPanel/countries'],
				['Регионы', 'mdi-earth-box', '/adminPanel/regions'],
				['Города', 'mdi-city', '/adminPanel/cities'],
            ]
        }
    },
    async mounted() {
		document.title = "Страница администратора";
        this.userAuthorized = await this.$oidc.isAdmin();
		let profile = await this.$oidc.getUserProfile();
		this.userName = profile.FirstName;
    },
    async updated() {
        this.userAuthorized = await this.$oidc.isAdmin();
    },
    methods: {
        logout() {
            this.$oidc.logout();
        },
        hideSidebar() {
            this.drawer = false;
        }
    }
}
</script>

<style scoped>
    v-list-item-title {
        text-align: left;
    }
</style>