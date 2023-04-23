<template>
    <ErrorPage
        v-if="!userAuthorized"
        error-code="403"
        message="Укажите в профиле свою группу, чтобы получить доступ к расписанию"
    />
    <div v-else>
        <h1>Ваше расписание</h1>
        <v-select
            label="Фильтровать пары по повторяемости"
            :items="filterOptions"
            item-text="text"
            item-value="value"
            @change="filterPairs"
        >

        </v-select>
        <v-container
            v-if="showResult">
            <v-row>
                <v-col col="12" elevation="20">
                    <v-card class="mx-auto">
                        <v-card-text>
                            <div v-if="mondayPairs.length > 0">
                                <h3 class="text-center output-header">Понедельник</h3>
                                <v-simple-table dark class="text-left">
                                    <template v-slot:default>
                                        <thead>
                                        <tr>
                                            <th class="text-left">
                                                Номер пары
                                            </th>
                                            <th class="text-left">
                                                Название
                                            </th>
                                            <th class="text-left">
                                                Факультет
                                            </th>
                                            <th class="text-left">
                                                Аудитория
                                            </th>
                                            <th class="text-left">
                                                Преподаватель
                                            </th>
                                            <th class="text-left">
                                                Начало
                                            </th>
                                            <th class="text-left">
                                                Конец
                                            </th>
                                            <th class="text-left" v-if="!filterWasApplied">
                                                Повторение
                                            </th>
                                        </tr>
                                        </thead>
                                        <tbody>
                                        <tr
                                            v-for="(item, i) in mondayPairs"
                                            :key="i"
                                        >
                                            <td>{{ item.pairInfo.pairNumber }}</td>
                                            <td>{{ item.pairName }}</td>
                                            <td>{{ item.auditorium.facultyName }}</td>
                                            <td>{{ item.auditorium.auditoriumName }}</td>
                                            <td>{{ item.teachersFIO }}</td>
                                            <td>{{ item.pairInfo.startTime }}</td>
                                            <td>{{ item.pairInfo.endTime }}</td>
                                            <td v-if="!filterWasApplied">{{ item.pairRepeatingName }}</td>
                                        </tr>
                                        </tbody>
                                    </template>
                                </v-simple-table>
                            </div>
                            <div v-if="tuesdayPairs.length > 0">
                                <h3 class="text-center output-header">Вторник</h3>
                                <v-simple-table dark class="text-left">
                                    <template v-slot:default>
                                        <thead>
                                        <tr>
                                            <th class="text-left">
                                                Номер пары
                                            </th>
                                            <th class="text-left">
                                                Название
                                            </th>
                                            <th class="text-left">
                                                Факультет
                                            </th>
                                            <th class="text-left">
                                                Аудитория
                                            </th>
                                            <th class="text-left">
                                                Преподаватель
                                            </th>
                                            <th class="text-left">
                                                Начало
                                            </th>
                                            <th class="text-left">
                                                Конец
                                            </th>
                                            <th class="text-left" v-if="!filterWasApplied">
                                                Повторение
                                            </th>
                                        </tr>
                                        </thead>
                                        <tbody>
                                        <tr
                                            v-for="(item, i) in tuesdayPairs"
                                            :key="i"
                                        >
                                            <td>{{ item.pairInfo.pairNumber }}</td>
                                            <td>{{ item.pairName }}</td>
                                            <td>{{ item.auditorium.facultyName }}</td>
                                            <td>{{ item.auditorium.auditoriumName }}</td>
                                            <td>{{ item.teachersFIO }}</td>
                                            <td>{{ item.pairInfo.startTime }}</td>
                                            <td>{{ item.pairInfo.endTime }}</td>
                                            <td v-if="!filterWasApplied">{{ item.pairRepeatingName }}</td>
                                        </tr>
                                        </tbody>
                                    </template>
                                </v-simple-table>
                            </div>
                            <div v-if="wednesdayPairs.length > 0">
                                <h3 class="text-center output-header">Среда</h3>
                                <v-simple-table dark class="text-left">
                                    <template v-slot:default>
                                        <thead>
                                        <tr>
                                            <th class="text-left">
                                                Номер пары
                                            </th>
                                            <th class="text-left">
                                                Название
                                            </th>
                                            <th class="text-left">
                                                Факультет
                                            </th>
                                            <th class="text-left">
                                                Аудитория
                                            </th>
                                            <th class="text-left">
                                                Преподаватель
                                            </th>
                                            <th class="text-left">
                                                Начало
                                            </th>
                                            <th class="text-left">
                                                Конец
                                            </th>
                                            <th class="text-left" v-if="!filterWasApplied">
                                                Повторение
                                            </th>
                                        </tr>
                                        </thead>
                                        <tbody>
                                        <tr
                                            v-for="(item, i) in wednesdayPairs"
                                            :key="i"
                                        >
                                            <td>{{ item.pairInfo.pairNumber }}</td>
                                            <td>{{ item.pairName }}</td>
                                            <td>{{ item.auditorium.facultyName }}</td>
                                            <td>{{ item.auditorium.auditoriumName }}</td>
                                            <td>{{ item.teachersFIO }}</td>
                                            <td>{{ item.pairInfo.startTime }}</td>
                                            <td>{{ item.pairInfo.endTime }}</td>
                                            <td v-if="!filterWasApplied">{{ item.pairRepeatingName }}</td>
                                        </tr>
                                        </tbody>
                                    </template>
                                </v-simple-table>
                            </div>
                            <div v-if="thursdayPairs.length > 0">
                                <h3 class="text-center output-header">Четверг</h3>
                                <v-simple-table dark class="text-left">
                                    <template v-slot:default>
                                        <thead>
                                        <tr>
                                            <th class="text-left">
                                                Номер пары
                                            </th>
                                            <th class="text-left">
                                                Название
                                            </th>
                                            <th class="text-left">
                                                Факультет
                                            </th>
                                            <th class="text-left">
                                                Аудитория
                                            </th>
                                            <th class="text-left">
                                                Преподаватель
                                            </th>
                                            <th class="text-left">
                                                Начало
                                            </th>
                                            <th class="text-left">
                                                Конец
                                            </th>
                                            <th class="text-left" v-if="!filterWasApplied">
                                                Повторение
                                            </th>
                                        </tr>
                                        </thead>
                                        <tbody>
                                        <tr
                                            v-for="(item, i) in thursdayPairs"
                                            :key="i"
                                        >
                                            <td>{{ item.pairInfo.pairNumber }}</td>
                                            <td>{{ item.pairName }}</td>
                                            <td>{{ item.auditorium.facultyName }}</td>
                                            <td>{{ item.auditorium.auditoriumName }}</td>
                                            <td>{{ item.teachersFIO }}</td>
                                            <td>{{ item.pairInfo.startTime }}</td>
                                            <td>{{ item.pairInfo.endTime }}</td>
                                            <td v-if="!filterWasApplied">{{ item.pairRepeatingName }}</td>
                                        </tr>
                                        </tbody>
                                    </template>
                                </v-simple-table>
                            </div>
                            <div v-if="fridayPairs.length > 0">
                                <h3 class="text-center output-header">Пятница</h3>
                                <v-simple-table dark class="text-left">
                                    <template v-slot:default>
                                        <thead>
                                        <tr>
                                            <th class="text-left">
                                                Номер пары
                                            </th>
                                            <th class="text-left">
                                                Название
                                            </th>
                                            <th class="text-left">
                                                Факультет
                                            </th>
                                            <th class="text-left">
                                                Аудитория
                                            </th>
                                            <th class="text-left">
                                                Преподаватель
                                            </th>
                                            <th class="text-left">
                                                Начало
                                            </th>
                                            <th class="text-left">
                                                Конец
                                            </th>
                                            <th class="text-left" v-if="!filterWasApplied">
                                                Повторение
                                            </th>
                                        </tr>
                                        </thead>
                                        <tbody>
                                        <tr
                                            v-for="(item, i) in fridayPairs"
                                            :key="i"
                                        >
                                            <td>{{ item.pairInfo.pairNumber }}</td>
                                            <td>{{ item.pairName }}</td>
                                            <td>{{ item.auditorium.facultyName }}</td>
                                            <td>{{ item.auditorium.auditoriumName }}</td>
                                            <td>{{ item.teachersFIO }}</td>
                                            <td>{{ item.pairInfo.startTime }}</td>
                                            <td>{{ item.pairInfo.endTime }}</td>
                                            <td v-if="!filterWasApplied">{{ item.pairRepeatingName }}</td>
                                        </tr>
                                        </tbody>
                                    </template>
                                </v-simple-table>
                            </div>
                            <div v-if="saturdayPairs.length > 0">
                                <h3 class="text-center output-header">Суббота</h3>
                                <v-simple-table dark class="text-left">
                                    <template v-slot:default>
                                        <thead>
                                        <tr>
                                            <th class="text-left">
                                                Номер пары
                                            </th>
                                            <th class="text-left">
                                                Название
                                            </th>
                                            <th class="text-left">
                                                Факультет
                                            </th>
                                            <th class="text-left">
                                                Аудитория
                                            </th>
                                            <th class="text-left">
                                                Преподаватель
                                            </th>
                                            <th class="text-left">
                                                Начало
                                            </th>
                                            <th class="text-left">
                                                Конец
                                            </th>
                                            <th class="text-left" v-if="!filterWasApplied">
                                                Повторение
                                            </th>
                                        </tr>
                                        </thead>
                                        <tbody>
                                        <tr
                                            v-for="(item, i) in saturdayPairs"
                                            :key="i"
                                        >
                                            <td>{{ item.pairInfo.pairNumber }}</td>
                                            <td>{{ item.pairName }}</td>
                                            <td>{{ item.auditorium.facultyName }}</td>
                                            <td>{{ item.auditorium.auditoriumName }}</td>
                                            <td>{{ item.teachersFIO }}</td>
                                            <td>{{ item.pairInfo.startTime }}</td>
                                            <td>{{ item.pairInfo.endTime }}</td>
                                            <td v-if="!filterWasApplied">{{ item.pairRepeatingName }}</td>
                                        </tr>
                                        </tbody>
                                    </template>
                                </v-simple-table>
                            </div>
                        </v-card-text>
                    </v-card>
                </v-col>
            </v-row>
        </v-container>
    </div>
</template>

<script>
import {mapGetters} from "vuex";
import ErrorPage from "@/components/AccountComponents/core/service-pages/ErrorPage";

export default {
    name: "ScheduleView",
    components: {ErrorPage},
    data() {
        return {
            userAuthorized: null,
            showResult: false,
            mondayPairs: [],
            tuesdayPairs: [],
            wednesdayPairs: [],
            thursdayPairs: [],
            fridayPairs: [],
            saturdayPairs: [],
            filterOptions: [
                {
                    text: "Отображать пары верхней недели",
                    value: 2
                },
                {
                    text: "Отображать пары нижней недели",
                    value: 3
                }
            ],
            filterWasApplied: false
        }
    },
    methods: {
        clearResults() {
            this.mondayPairs = [];
            this.tuesdayPairs = [];
            this.wednesdayPairs = [];
            this.thursdayPairs = [];
            this.fridayPairs = [];
            this.saturdayPairs = [];
            this.showResult = false;
        },
        searchPairs() {
            let groupId = this.CURRENT_USER.groupId;
            this.$loading(true);
            this.$store.dispatch('loadPairsByGroupId', groupId)
                .then(() => {
                    let resultPairs = this.PAIRS.pairs;
                    this.processPairs(resultPairs);
                    this.showResult = true;
                })
                .catch((error) => {
                    console.log(error);
                })
                .finally(() => {
                    this.$loading(false);
                })
        },
        filterPairs(value) {
            let allPairs = this.PAIRS.pairs;
            let filteredPairs = allPairs
                .filter(pair => pair.pairRepeatingId === value || pair.pairRepeatingId === 1);
            this.clearResults();
            this.filterWasApplied = true;
            this.processPairs(filteredPairs);
            this.showResult = true;
        },
        processPairs(pairs) {
            this.mondayPairs = pairs
                .filter(pair => pair.dayOfWeekId === 1)
                .sort((a, b) => a.pairInfo.pairNumber - b.pairInfo.pairNumber);
            this.tuesdayPairs = pairs
                .filter(pair => pair.dayOfWeekId === 2)
                .sort((a, b) => a.pairInfo.pairNumber - b.pairInfo.pairNumber);
            this.wednesdayPairs = pairs
                .filter(pair => pair.dayOfWeekId === 3)
                .sort((a, b) => a.pairInfo.pairNumber - b.pairInfo.pairNumber);
            this.thursdayPairs = pairs
                .filter(pair => pair.dayOfWeekId === 4)
                .sort((a, b) => a.pairInfo.pairNumber - b.pairInfo.pairNumber);
            this.fridayPairs = pairs
                .filter(pair => pair.dayOfWeekId === 5)
                .sort((a, b) => a.pairInfo.pairNumber - b.pairInfo.pairNumber);
            this.saturdayPairs = pairs
                .filter(pair => pair.dayOfWeekId === 6)
                .sort((a, b) => a.pairInfo.pairNumber - b.pairInfo.pairNumber);
        }
    },
    mounted() {
        document.title = "Просмотр расписания";
        this.userAuthorized = this.CURRENT_USER.groupId !== null;
        if (this.userAuthorized)
            this.searchPairs();
    },
    computed: {
        ...mapGetters(['CURRENT_USER']),
        ...mapGetters(['PAIRS'])
    }
}
</script>

<style scoped>

</style>