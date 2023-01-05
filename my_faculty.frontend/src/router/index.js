import Vue from 'vue'
import VueRouter from 'vue-router'
import MainAppPageView from "@/views/MainAppPageView";
import AdminPageView from "@/views/AdminPageView";
import SignInCallback from "@/components/SignInCallback";
import SignOutCallback from "@/components/SignOutCallback";
import RedirectSilentRenew from "@/components/RedirectSilentRenew";
import AccountPageView from "@/views/AccountPageView";

Vue.use(VueRouter)

const routes = [
    {
        path: '/',
        name: 'home',
        component: MainAppPageView,
        children: [
            {
                path: '/',
                name: 'mainContent',
                component: () => import('../views/mainPageViews/MainContentView')
            },
            {
                path: '/teachers',
                name: 'teachersContent',
                component: () => import('../views/mainPageViews/TeachersContentView')
            },
            {
                path: '/pairs',
                name: 'pairsContent',
                component: () => import('../views/mainPageViews/PairsContentView')
            },
            {
                path: '/about',
                name: 'aboutContent',
                component: () => import('../views/mainPageViews/AboutContentView')
            },
            {
                path: '/info',
                name: 'infoContent',
                component: () => import('../views/mainPageViews/InfoContentView')
            },
            {
                path: '/choose-your-faculty',
                name: 'expert system content',
                component: () => import('../views/mainPageViews/ExpertSystemContentView')
            }
        ]
    },
    {
        path: '/silent-renew',
        name: 'silentRenew',
        component: RedirectSilentRenew
    },
    {
        path: '/adminPanel',
        name: 'admin',
        component: AdminPageView,
        children: [
            {
                path: '/adminPanel/faculties',
                name: 'faculties',
                component: () => import('../views/adminPageViews/FacultiesView')
            },
            {
                path: '/adminPanel/floors',
                name: 'floors',
                component: () => import('../views/adminPageViews/FloorsView')
            },
            {
                path: '/adminPanel/teachers',
                name: 'teachers',
                component: () => import('../views/adminPageViews/TeachersView')
            },
            {
                path: '/adminPanel/auditoriums',
                name: 'auditoriums',
                component: () => import('../views/adminPageViews/AuditoriumsView')
            },
            {
                path: '/adminPanel/secondaryObjects',
                name: 'secondaryObjects',
                component: () => import('../views/adminPageViews/SecondaryObjectsView')
            },
            {
                path: '/adminPanel/pairs',
                name: 'pairs',
                component: () => import('../views/adminPageViews/PairsView')
            },
            {
                path: '/adminPanel/disciplines',
                name: 'disciplines',
                component: () => import('../views/adminPageViews/DisciplinesView')
            },
            {
                path: '/adminPanel/courses',
                name: 'courses',
                component: () => import('../views/adminPageViews/CoursesView')
            },
            {
                path: '/adminPanel/groups',
                name: 'groups',
                component: () => import('../views/adminPageViews/GroupsView')
            },
            {
                path: '/adminPanel/scienceRanks',
                name: 'scienceRanks',
                component: () => import('../views/adminPageViews/ScienceRanksView')
            },
            {
                path: '/adminPanel/pairInfos',
                name: 'pairInfos',
                component: () => import('../views/adminPageViews/PairInfosView')
            },
            {
                path: '/adminPanel/secondaryObjectTypes',
                name: 'secondaryObjectTypes',
                component: () => import('../views/adminPageViews/SecondaryObjectTypesView')
            },
            {
                path: '/adminPanel/teachersDisciplines',
                name: 'teachersDisciplines',
                component: () => import('../views/adminPageViews/TeachersDisciplinesView')
            },
            {
                path: '/adminPanel/countries',
                name: 'countries',
                component: () => import('../views/adminPageViews/CountriesView')
            },
            {
                path: '/adminPanel/regions',
                name: 'regions',
                component: () => import('../views/adminPageViews/RegionsView')
            },
            {
                path: '/adminPanel/cities',
                name: 'cities',
                component: () => import('../views/adminPageViews/CitiesView')
            }
        ]
    },
    {
        path: '/personal',
        name: 'Мой профиль',
        component: AccountPageView,
        children: [
            {
                path: '/',
                name: 'Мой профиль',
                component: () => import('../views/accountPageViews/ProfileView')
            },
            {
                path: '/news',
                name: 'Новости',
                component: () => import('../views/accountPageViews/NewsView')
            },
            {
                path: '/clubs',
                name: 'Сообщества курсов',
                component: () => import('../views/accountPageViews/ClubsView')
            },
            {
                path: '/teacher-verification',
                name: 'Верификация преподавателя',
                component: () => import('../views/accountPageViews/TeacherVerificationView')
            },
            {
                path: '/id:id',
                name: 'Просмотр профиля пользователя',
                component: () => import('../views/accountPageViews/UserView')
            }
        ]
    },
    {
        path: '/signin-oidc',
        name: 'signin-callback',
        component: SignInCallback
    },
    {
        path: '/signout-oidc',
        name: 'signout-callback',
        component: SignOutCallback
    }
]

const router = new VueRouter({
    mode: 'history',
    base: process.env.BASE_URL,
    routes
})

export default router
