<template>
    <v-app>
        <v-app-bar
                class="flex-grow-0"
                elevation="20"
                dark
                color="grey darken-4"
        >
            <v-app-bar-nav-icon
                    @click.stop="drawer = !drawer"
                    class="d-flex d-md-none">
            </v-app-bar-nav-icon>
            <a href="/">
                <img :src="'img/icons/logo.ico'" alt="#" width="50" height="50">
            </a>
            <v-toolbar-title class="ml-2">MyFaculty</v-toolbar-title>
            <v-spacer></v-spacer>
            <div v-if="userAuthenticated" class="mt-5 d-none d-sm-flex">
                <v-btn outlined @click="openAccountPage">В личный кабинет</v-btn>
                <v-btn @click="signOut" outlined color="red" class="ml-3">Выйти</v-btn>
            </div>
            <v-btn @click="signIn" v-else outlined class="d-none d-sm-block">Авторизоваться</v-btn>
            <template v-slot:extension>
                <v-tabs
                        align-with-title
                        class="d-none d-sm-flex"
                        v-model="tab"
                >
                    <v-tabs-slider color="yellow"></v-tabs-slider>
                    <v-tab
                            v-for="(item, i) in items"
                            v-bind:to="item.link"
                    >
                        {{item.text}}
                    </v-tab>
                </v-tabs>
            </template>
        </v-app-bar>
        <v-navigation-drawer
                v-model="drawer"
                absolute
                temporary
                dark
        >
            <v-list
                    nav
                    dense
            >
                <v-list-item-group
                >
                    <v-list-item v-for="(item, i) in items" :key="i" v-bind:to="item.link">
                        {{item.text}}
                    </v-list-item>
                </v-list-item-group>
            </v-list>
            <div v-if="userAuthenticated" class="pa-2 d-sm-none d-flex flex-column">
                <v-container>
                    <v-btn outlined @click="openAccountPage">В личный кабинет</v-btn>
                    <v-btn outlined @click="signOut" color="red" class="mt-3">Выйти</v-btn>
                </v-container>
            </div>
            <div v-else class="pa-2 d-sm-none d-flex flex-column">
                <v-container>
                    <v-btn @click="signIn" outlined>Авторизоваться</v-btn>
                </v-container>
            </div>
        </v-navigation-drawer>
        <v-main class="mt-4">
            <router-view/>
        </v-main>
		<PagesFooter />
    </v-app>
</template>

<script>
import PagesFooter from "@/components/AccountComponents/core/PagesFooter";
export default {
    name: "MainAppPageView",
	components: {PagesFooter},
	data() {
        return {
            drawer: false,
            userAuthenticated: false,
            tab: 0,
            items: [
                { "text" : "Главная", "link" : '/' },
                { "text" : "Преподаватели", "link" : '/teachers' },
                { "text" : "Пары", "link" : '/pairs' },
                { "text" : "О факульете", "link" : '/about' },
                { "text" : "Полезная информация", "link" : '/info' },
				{ "text" : "Куда поступать?", "link" : '/choose-your-faculty'}
            ]
        }
    },
    methods: {
        signIn() {
            this.$oidc.login();
        },
        signOut() {
            this.$oidc.logout();
        },
		openAccountPage() {
			this.$router.push('/personal');
		}
    },
    async updated() {
        this.userAuthenticated = await this.$oidc.isAuthenticated();
    },
    async mounted() {
		document.title = "Главная";
        this.userAuthenticated = await this.$oidc.isAuthenticated();
    }
}
</script>

<style scoped>
    a.header-link {
        color: black;
        text-decoration: none;
        font-size: 20px;
        font-weight: bolder;
    }
     router-link {
        color: white;
     }
</style>