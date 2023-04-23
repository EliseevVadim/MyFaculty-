<template>
    <div>
        <UsersGroupTransferModal
            :show="showTransferModal"
            @close="showTransferModal = false"
            @load="resetState"
        />
        <h1>Выберите интересующий факультет для просмотра пользователей:</h1>
        <v-container>
            <v-select
                :items="this.FACULTIES.faculties"
                item-text="facultyName"
                item-value="id"
                label="Выберите интересующий факультет*"
                @change="loadCoursesList"
                v-model="selectedFacultyId"
            ></v-select>
            <v-select
                v-if="coursesAreLoaded"
                :items="this.COURSES.courses"
                item-text="courseName"
                item-value="id"
                label="Выберите курс*"
                @change="loadGroupsList"
                v-model="selectedCourseId"
            ></v-select>
            <h1 v-else-if="coursesAreLoaded === false" class="red--text">
                Для данного факультета информация о курсах отсутствует.
                Дальнейшее взаимодействие невозможно.
            </h1>
            <v-select
                v-if="groupsAreLoaded"
                :items="this.GROUPS.groups"
                item-text="groupName"
                item-value="id"
                label="Выберите группу*"
                @change="loadUsersList"
                v-model="selectedGroupId"
            ></v-select>
            <h1 v-else-if="groupsAreLoaded === false" class="red--text">
                Для данного курса информация о группах отсутствует.
                Дальнейшее взаимодействие невозможно.
            </h1>
        </v-container>
        <v-card v-if="usersAraLoaded">
            <v-card-title>
                Список пользователей из указанной группы
                <v-spacer></v-spacer>
                <v-text-field
                    v-model="search"
                    append-icon="mdi-magnify"
                    label="Поиск"
                    single-line
                    hide-details
                ></v-text-field>
            </v-card-title>
            <v-data-table
                :headers="headers"
                :items="this.USERS.users"
                :search="search"
                class="text-left"
            >
                <template v-slot:body="{items}">
                    <tbody>
                    <tr v-for="(item,index) in items" :key="index">
                        <td>
                            <a
                                :href="`/id${item.id}`"
                            >
                                id{{ item.id }}
                            </a>
                        </td>
                        <td>
                            {{ item.firstName }}
                        </td>
                        <td>
                            {{ item.lastName }}
                        </td>
                        <td>
                            {{ item.countryName }}
                        </td>
                        <td>
                            {{ item.cityName }}
                        </td>
                    </tr>
                    </tbody>
                </template>
            </v-data-table>
        </v-card>
        <EmptySearchResult
            v-else-if="usersAraLoaded === false"
        />
        <v-btn
            color="primary"
            class="my-2"
            @click="showTransferModal = true"
        >
            Перевести пользователей из одной группы в другую
        </v-btn>
    </div>
</template>

<script>
import {mapGetters} from "vuex";
import EmptySearchResult from "@/components/AccountComponents/core/service-pages/EmptySearchResult";
import UsersGroupTransferModal from "@/components/AdminComponents/UsersGroupTransferModal";

export default {
    name: "UsersGroupsView",
    components: {UsersGroupTransferModal, EmptySearchResult},
    data() {
        return {
            showTransferModal: false,
            search: '',
            selectedFacultyId: null,
            selectedCourseId: null,
            selectedGroupId: null,
            coursesAreLoaded: null,
            groupsAreLoaded: null,
            usersAraLoaded: null,
            headers: [
                {
                    text: 'id',
                    align: 'start',
                    value: 'id',
                },
                {text: 'Фамилия', value: 'lastName'},
                {text: 'Имя', value: 'firstName'},
                {text: 'Страна', value: 'countryName'},
                {text: 'Город', value: 'cityName'}
            ]
        }
    },
    methods: {
        loadCoursesList() {
            this.$loading(true);
            this.$store.dispatch('loadCoursesByFacultyId', this.selectedFacultyId)
                .then(() => {
                    this.coursesAreLoaded = this.COURSES.courses.length > 0;
                    this.$loading(false);
                })
        },
        loadGroupsList() {
            this.$loading(true);
            this.$store.dispatch('loadGroupsByCourseId', this.selectedCourseId)
                .then(() => {
                    this.groupsAreLoaded = this.GROUPS.groups.length > 0;
                    this.$loading(false);
                })
        },
        loadUsersList() {
            this.$loading(true);
            this.$store.dispatch('loadUsersByGroupId', this.selectedGroupId)
                .then(() => {
                    this.usersAraLoaded = this.USERS.users.length > 0;
                    this.$loading(false);
                })
        },
        resetState() {
            this.selectedFacultyId = null;
            this.selectedCourseId = null;
            this.selectedGroupId = null;
            this.coursesAreLoaded = null;
            this.groupsAreLoaded = null;
            this.usersAraLoaded = null;
        }
    },
    mounted() {
        this.$store.dispatch('loadAllFaculties');
    },
    computed: {
        ...mapGetters(['FACULTIES']),
        ...mapGetters(['COURSES']),
        ...mapGetters(['GROUPS']),
        ...mapGetters(['USERS'])
    }
}
</script>

<style scoped>

</style>