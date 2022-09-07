<template>
	<div>
		<h1 class="text-center">
			Расписание пар
		</h1>
		<h3 class="text-center">В данном разделе Вы можете просмотреть расписание пар для своей группы</h3>
		<v-container>
			<v-form
				ref="searchForm"
				lazy-validation>
				<v-col cols="12">
					<v-autocomplete
						label="Курс и группа"
						required
						:items="this.GROUPS.groups"
						:item-text="getFullGroupName"
						item-value="id"
						hide-details="auto"
						:rules="commonRules"
						v-model="searchParams.group_id"
					></v-autocomplete>
				</v-col>
				<v-btn
					color="success"
					dark
					@click="searchPairs">
					Поиск
				</v-btn>
			</v-form>
		</v-container>
		<v-container
			v-if="showResult">
			<v-row>
				<v-col col="12" elevation="20">
					<v-card class="mx-auto">
						<v-card-text>
							<h1 class="display-1 text--primary text-center">Полученное расписание:</h1>
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
											<th class="text-left">
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
											<td>{{ item.auditorium.auditoriumName }}</td>
											<td>{{ item.teachersFIO }}</td>
											<td>{{ item.pairInfo.startTime }}</td>
											<td>{{ item.pairInfo.endTime }}</td>
											<td>{{ item.pairRepeatingName }}</td>
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
											<th class="text-left">
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
											<td>{{ item.auditorium.auditoriumName }}</td>
											<td>{{ item.teachersFIO }}</td>
											<td>{{ item.pairInfo.startTime }}</td>
											<td>{{ item.pairInfo.endTime }}</td>
											<td>{{ item.pairRepeatingName }}</td>
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
											<th class="text-left">
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
											<td>{{ item.auditorium.auditoriumName }}</td>
											<td>{{ item.teachersFIO }}</td>
											<td>{{ item.pairInfo.startTime }}</td>
											<td>{{ item.pairInfo.endTime }}</td>
											<td>{{ item.pairRepeatingName }}</td>
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
											<th class="text-left">
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
											<td>{{ item.auditorium.auditoriumName }}</td>
											<td>{{ item.teachersFIO }}</td>
											<td>{{ item.pairInfo.startTime }}</td>
											<td>{{ item.pairInfo.endTime }}</td>
											<td>{{ item.pairRepeatingName }}</td>
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
											<th class="text-left">
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
											<td>{{ item.auditorium.auditoriumName }}</td>
											<td>{{ item.teachersFIO }}</td>
											<td>{{ item.pairInfo.startTime }}</td>
											<td>{{ item.pairInfo.endTime }}</td>
											<td>{{ item.pairRepeatingName }}</td>
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
											<th class="text-left">
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
											<td>{{ item.auditorium.auditoriumName }}</td>
											<td>{{ item.teachersFIO }}</td>
											<td>{{ item.pairInfo.startTime }}</td>
											<td>{{ item.pairInfo.endTime }}</td>
											<td>{{ item.pairRepeatingName }}</td>
										</tr>
										</tbody>
									</template>
								</v-simple-table>
							</div>
						</v-card-text>
						<v-card-actions>
							<v-btn dark color="deep-purple accent-4" @click="showResult = false">
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
    name: "PairsContentView",
	data() {
		return {
			formValid: true,
			showResult: false,
			searchParams: {
				course_id : null,
				group_id: null
			},
			mondayPairs: [],
			tuesdayPairs: [],
			wednesdayPairs: [],
			thursdayPairs: [],
			fridayPairs: [],
			saturdayPairs: [],
			commonRules: [
				v => !!v || 'Поле является обязательным для заполнения'
			]
		}
	},
	methods: {
		getFullGroupName(item) {
			return item.groupName + ' ' + '(' + item.courseName + ')';
		},
		searchPairs() {
			this.formValid = this.$refs.searchForm.validate();
			if(!this.formValid)
				return;
			axios.get(config.apiUrl + '/api/pairs/group/' + this.searchParams.group_id)
				.then((response) => {
					let resultPairs = response.data.pairs;
					this.mondayPairs = resultPairs
						.filter(pair => pair.dayOfWeekId === 1)
						.sort((a, b) => a.pairInfo.pairNumber - b.pairInfo.pairNumber);
					this.tuesdayPairs = resultPairs
						.filter(pair => pair.dayOfWeekId === 2)
						.sort((a, b) => a.pairInfo.pairNumber - b.pairInfo.pairNumber);
					this.wednesdayPairs = resultPairs
						.filter(pair => pair.dayOfWeekId === 3)
						.sort((a, b) => a.pairInfo.pairNumber - b.pairInfo.pairNumber);
					this.thursdayPairs = resultPairs
						.filter(pair => pair.dayOfWeekId === 4)
						.sort((a, b) => a.pairInfo.pairNumber - b.pairInfo.pairNumber);
					this.fridayPairs = resultPairs
						.filter(pair => pair.dayOfWeekId === 5)
						.sort((a, b) => a.pairInfo.pairNumber - b.pairInfo.pairNumber);
					this.saturdayPairs = resultPairs
						.filter(pair => pair.dayOfWeekId === 6)
						.sort((a, b) => a.pairInfo.pairNumber - b.pairInfo.pairNumber);
					this.showResult = true;
				})
				.catch((error) => {
					console.log(error);
				})
		}
	},
	mounted() {
		this.$store.dispatch('loadAllGroups');
	},
	computed: {
		...mapGetters(['GROUPS'])
	}
}
</script>

<style scoped>
	.output-header {
		color: black;
		font-size: 20px;
		margin-top: 10px;
		margin-bottom: 10px;
	}
</style>