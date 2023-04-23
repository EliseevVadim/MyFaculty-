<template>
    <div>
        <h1 class="text-center">Посмотрите на карту интересующего Вас факультета</h1>
        <v-container>
            <v-select
                :items="this.FACULTIES.faculties"
                item-text="facultyName"
                item-value="id"
                label="Выберите интересующий факультет*"
                @change="loadFloorsList"
                v-model="facultyId"
            ></v-select>
            <v-select
                v-if="floorsAreLoaded"
                :items="this.FLOORS.floors"
                item-text="name"
                item-value="id"
                label="Выберите этаж*"
                @change="drawFloor"
                v-model="currentFloorId"
            ></v-select>
        </v-container>
        <v-container fluid>
            <div id="floor-area">
                <svg id="floor-container"></svg>
            </div>
        </v-container>
        <v-container v-if="showAuditoriumDetails">
            <v-row>
                <v-col col="12" elevation="20">
                    <v-card class="mx-auto">
                        <v-card-text>
                            <h1 class="display-1 text--primary text-center">Информация об аудитории</h1>
                            <v-row d-flex class="mt-3">
                                <v-col class="black--text font-weight-black">
                                    <span class="card-key">
                                        Название аудитории:
                                    </span>
                                </v-col>
                                <v-col>
                                    <span class="card-value">
                                        {{ selectedAuditorium.auditoriumName }}
                                    </span>
                                </v-col>
                            </v-row>
                            <v-row>
                                <v-col class="black--text font-weight-black">
                                    <span class="card-key">
                                        Ответственный:
                                    </span>
                                </v-col>
                                <v-col>
                                    <span class="card-value">
                                        {{ selectedAuditorium.teachersFIO }}
                                    </span>
                                </v-col>
                            </v-row>
                            <h3 class="display-1 text--primary text-center mt-3">Информация о занятости</h3>
                            <v-simple-table>
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
                                            Начало
                                        </th>
                                        <th class="text-left">
                                            Конец
                                        </th>
                                        <th class="text-left">
                                            Группа
                                        </th>
                                        <th class="text-left">
                                            Курс
                                        </th>
                                        <th class="text-left">
                                            День недели
                                        </th>
                                        <th class="text-left">
                                            Повторение
                                        </th>
                                    </tr>
                                    </thead>
                                    <tbody>
                                    <tr
                                        v-for="(item, i) in selectedAuditorium.pairs"
                                        :key="i"
                                    >
                                        <td class="text-left">{{ item.pairInfo.pairNumber }}</td>
                                        <td class="text-left">{{ item.pairName }}</td>
                                        <td class="text-left">{{ item.pairInfo.startTime }}</td>
                                        <td class="text-left">{{ item.pairInfo.endTime }}</td>
                                        <td class="text-left">{{ item.group.groupName }}</td>
                                        <td class="text-left">{{ item.group.courseName }}</td>
                                        <td class="text-left">{{ item.dayOfWeek }}</td>
                                        <td class="text-left">{{ item.pairRepeatingName }}</td>
                                    </tr>
                                    </tbody>
                                </template>
                            </v-simple-table>
                        </v-card-text>
                        <v-card-actions>
                            <v-btn text color="deep-purple accent-4" @click="showAuditoriumDetails = false">
                                Закрыть
                            </v-btn>
                        </v-card-actions>
                    </v-card>
                </v-col>
            </v-row>
        </v-container>
    </div>
</template>

<script>
import {mapGetters} from "vuex";
import axios from "axios";
import {config} from "@/config/config";

export default {
    name: "MainContentView",
    data() {
        return {
            facultyId: null,
            currentFloorId: null,
            floorsAreLoaded: false,
            showAuditoriumDetails: false,
            selectedAuditorium: {
                auditorium_name: "",
                teacher_name: "",
                pairs: null
            }
        }
    },
    mounted() {
        this.$store.dispatch('loadAllFaculties');
    },
    methods: {
        loadFloorsList() {
            this.clearResults();
            this.$loading(true);
            this.$store.dispatch('loadFloorsByFacultyId', this.facultyId)
                .then(() => {
                    this.$loading(false);
                    this.floorsAreLoaded = this.FLOORS.floors.length > 0;
                })
        },
        drawFloor() {
            this.clearFloorArea();
            let svg = this.$d3.select('#floor-container');
            let mainG = svg.append('g')
                .attr('main', true);
            let zoom = this.$d3.zoom()
                .on('zoom', (e) => {
                    this.$d3.select('#floor-container>g[main=true]').attr('transform', e.transform)
                });
            this.$loading(true);
            this.$d3.select('#floor-container').call(zoom);
            this.$store.dispatch('loadFloorById', this.currentFloorId)
                .then((response) => {
                    this.$loading(false);
                    this.drawBounds(JSON.parse(response.data.bounds));
                    this.drawFloorObjects(JSON.parse(JSON.stringify(response.data.auditoriums)), JSON.parse(JSON.stringify(response.data.secondaryObjects)));
                })
        },
        drawBounds(data) {
            let svg = this.$d3.select('#floor-container>g[main=true]');
            svg.select('g.drawPoly').remove();
            let g = svg.append('g');
            g.append('polygon')
                .attr('points', data)
                .style('fill', 'none')
                .style("stroke", "black")
                .style("stroke-width", "2");
        },
        drawFloorObjects(auditoriums, secondaryObjects) {
            let data = [];
            for (let i = 0; i < auditoriums.length; i++) {
                data = JSON.parse(auditoriums[i].positionInfo);
                let gAppend = this.$d3.select('#floor-container>g[main=true]')
                    .append('g')
                    .attr('class', 'auditorium');
                gAppend
                    .append('rect')
                    .style("fill", "#cfd5c3")
                    .style("stroke", "black")
                    .style("stroke-width", "2")
                    .style("cursor", "pointer")
                    .attr("x", data.x)
                    .attr("y", data.y)
                    .attr("width", data.width)
                    .attr("height", data.height)
                    .on("click", () => {
                        this.showAuditoriumInfo(auditoriums[i].id);
                    });
                gAppend
                    .append('text')
                    .style("font-size", "14px")
                    .style("color", "black")
                    .style("position", "relative")
                    .style("text-anchor", "middle")
                    .attr("x", data.x + data.width / 2)
                    .attr("y", data.y + data.height / 2 + 5)
                    .text(auditoriums[i].auditoriumName);
            }
            for (let i = 0; i < secondaryObjects.length; i++) {
                data = JSON.parse(secondaryObjects[i].positionInfo);
                let secondaryObjectAppend = this.$d3.select('#floor-container>g[main=true]')
                    .append('g')
                    .attr('class', 'secondaryObject')
                    .attr("width", data.width)
                    .attr("height", data.height);
                secondaryObjectAppend
                    .append('rect')
                    .style("fill", "white")
                    .style("stroke", "black")
                    .style("stroke-width", "2")
                    .attr("x", data.x)
                    .attr("y", data.y)
                    .attr("width", data.width)
                    .attr("height", data.height);
                secondaryObjectAppend
                    .append('text')
                    .style("font-size", "14px")
                    .style("color", "black")
                    .style("position", "relative")
                    .style("text-anchor", "middle")
                    .attr("x", data.x + data.width / 2)
                    .attr("y", data.y + Math.min(20, data.height * 0.4))
                    .text(secondaryObjects[i].objectName);
                secondaryObjectAppend
                    .append('image')
                    .attr("x", data.x + data.width * 0.1)
                    .attr("y", data.y + data.height * 0.4)
                    .attr("width", data.width * 0.8)
                    .attr("height", data.height * 0.6)
                    .attr("href", secondaryObjects[i].secondaryObjectType.typePath);
            }
        },
        clearFloorArea() {
            this.$d3.selectAll('#floor-container > g').remove();
        },
        showAuditoriumInfo(id) {
            this.$loading(true);
            axios.get(config.apiUrl + '/api/auditoriums/' + id)
                .then((response) => {
                    this.selectedAuditorium = response.data;
                    this.$loading(false);
                })
                .catch((error) => {
                    console.log(error.response.data);
                })
            this.showAuditoriumDetails = true;
        },
        clearResults() {
            this.clearFloorArea();
            this.showAuditoriumDetails = false;
            this.floorsAreLoaded = false;
        }
    },
    computed: {
        ...mapGetters(['FLOORS']),
        ...mapGetters(['FACULTIES'])
    }
}
</script>

<style scoped>
#floor-area {
    background: #f3f2f1;
    margin: 10px auto;
    border: 1px solid black;
    width: 100%;
    max-width: 851px;
    height: 606px;
    overflow: hidden;
}

#floor-container {
    background: #f3f2f1;
    height: 100%;
    width: 100%;
    touch-action: none;
    overflow: hidden;
}

.card-key {
    font-size: 20px;
}

.card-value {
    color: black;
    font-size: 20px;
    font-weight: bolder;
}
</style>