<template>
	<div>
		<h1 class="text-center mt-3">Управление списком этажей</h1>
		<v-btn
			class="mt-2 mb-3 mx-5"
			fab
			dark
			color="indigo"
			v-ripple
			@click="openAddingForm"
		>
			<v-icon dark>
				mdi-plus
			</v-icon>
		</v-btn>
		<v-dialog
			v-model="showAddingForm"
			persistent
			fullscreen
			hide-overlay
			transition="dialog-bottom-transition"
		>
			<v-card>
				<v-card-actions>
					<v-btn
						class="mx-2"
						fab
						dark
						small
						color="red"
						@click="showAddingForm = false"
					>
						<v-icon dark>
							mdi-close
						</v-icon>
					</v-btn>
				</v-card-actions>
				<v-card-title>
					<span class="text-h5">{{updating ? 'Обновить информацию об этаже' : 'Добавить этаж'}}</span>
				</v-card-title>
				<v-card-text>
					<h2 class="text-center red--text">{{errorText}}</h2>
					<v-container>
						<v-form
							lazy-validation
							ref="submitForm"
							v-model="formValid">
							<v-col cols="12">
								<v-text-field
									label="Имя этажа*"
									required
									:rules="commonRules"
									hide-details="auto"
									v-model="floor.name"
								></v-text-field>
							</v-col>
							<p>Нарисуйте границы этажа (в последствии, Вы сможете изменить координаты нарисованных ранее точек)</p>
							<FloorDrawer ref="drawingElement" v-bind:floor="floor"></FloorDrawer>
						</v-form>
					</v-container>
					<small>Поля, помеченные * обязательны к заполнению</small>
				</v-card-text>
				<v-card-actions>
					<v-spacer></v-spacer>
					<v-btn
						color="blue darken-1"
						text
						@click="showAddingForm = false"
					>
						Закрыть
					</v-btn>
					<v-btn
						color="blue darken-1"
						text
						@click="sendData"
						:disabled="!formValid"
					>
						Сохранить
					</v-btn>
				</v-card-actions>
			</v-card>
		</v-dialog>
		<v-card>
			<v-card-title>
				Список этажей
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
				:items="this.FLOORS.floors"
				:search="search"
				class="text-left"
			>
				<template v-slot:body="{items}">
					<tbody>
					<tr v-for="(item,index) in items" :key="index">
						<td>
							{{item.name}}
						</td>
						<td>
							{{item.bounds}}
						</td>
						<td>
							<v-btn
								class="mx-2"
								fab
								small
								color="blue"
								@click="preview(item.id)"
							>
								<v-icon dark>
									mdi-eye-circle
								</v-icon>
							</v-btn>
							<v-btn
								class="mx-2"
								fab
								small
								color="yellow"
								@click="startUpdatingFloor(item.id)"
							>
								<v-icon dark>
									mdi-pencil
								</v-icon>
							</v-btn>
							<v-btn
								class="mx-2"
								fab
								small
								dark
								color="red"
								@click="deleteFloor(item.id)"
							>
								<v-icon dark>
									mdi-delete
								</v-icon>
							</v-btn>
						</td>
					</tr>
					</tbody>
				</template>
			</v-data-table>
		</v-card>
		<h1 class="text-center mt-3">Предпросмотр контура выбранного этажа</h1>
		<div id="preview-container-area">
			<svg id="preview-container">

			</svg>
		</div>
		<v-col
			cols="12"
			class="text-center">
			<v-btn
				color="red"
				dark
				@click="clearPreviewArea">
				Очистить область предпросмотра
			</v-btn>
		</v-col>
	</div>
</template>

<script>
import {mapGetters} from "vuex";
import FloorDrawer from "@/components/FloorDrawer";
export default {
    name: "FloorsView",
	components: {
		FloorDrawer
	},
	data () {
		return {
			showAddingForm: false,
			search: '',
			formValid: true,
			updating: false,
			errorText: "",
			floor: {
				name: "",
				bounds: null
			},
			commonRules: [
				v => !!v || 'Поле является обязательным для заполнения'
			],
			headers: [
				{
					text: 'Название этажа',
					align: 'start',
					value: 'name',
				},
				{ text: 'Границы', value: 'bounds' },
				{ text: 'Действия', value: 'actions', sortable: false }
			],
		}
	},
	mounted() {
		this.$store.dispatch('loadAllFloors');
	},
	methods: {
		openAddingForm() {
			this.errorText = "";
			this.resetFloor();
			this.updating = false;
			try {
				this.$refs.drawingElement.clearDrawingArea();
			}
			catch (e) {}
			finally {
				this.showAddingForm = true;
			}
		},
		sendData() {
			let coordinates = this.$refs.drawingElement.result;
			if (coordinates.length === 0) {
				this.errorText = "Ошибка: границы этажа не указаны.";
				return;
			}
			this.floor.bounds = JSON.stringify(coordinates);
			this.formValid = this.$refs.submitForm.validate();
			if(!this.formValid)
				return;
			this.errorText = "";
			if(!this.updating) {
				this.$store.dispatch('addFloor', this.floor)
					.then(() => {
						this.resetFloor();
						this.showAddingForm = false;
						this.$store.dispatch('loadAllFloors');
					})
					.catch(() => {
						this.errorText = "Произошла ошибка добавления записи.";
					});
			}
			else {
				this.$store.dispatch('updateFloor', this.floor)
					.then(() => {
						this.resetFloor();
						this.showAddingForm = false;
						this.$store.dispatch('loadAllFloors');
					})
					.catch(() => {
						this.errorText = "Произошла ошибка обновления записи.";
					});
			}
		},
		resetFloor() {
			this.floor.name = "";
			this.floor.bounds = null;
		},
		deleteFloor(id) {
			if (confirm("Вы действительно хотите удалить данную запись")) {
				this.$store.dispatch('deleteFloor', id)
					.then(() => {
						this.resetFloor();
						this.$store.dispatch('loadAllFloors');
					})
					.catch(() => {
						this.resetFloor();
						alert("Невозможно удалить запись");
					})
			}
		},
		startUpdatingFloor(id) {
			this.errorText = "";
			this.floor.id = id;
			this.$store.dispatch('loadFloorById', id)
				.then((response) => {
					this.floor.name = response.data.name;
					this.floor.bounds = JSON.parse(response.data.bounds);
					this.updating = true;
					try {
						this.$refs.drawingElement.clearDrawingArea();
						this.$refs.drawingElement.closePolygon(this.floor.bounds);
						this.$refs.drawingElement.result = this.floor.bounds;
					}
					catch (e) {}
					finally {
						this.showAddingForm = true;
					}
				})
		},
		preview(id) {
			this.clearPreviewArea();
			this.$store.dispatch('loadFloorById', id)
				.then((response) => {
					this.drawFloorForPreview(JSON.parse(response.data.bounds));
				});
		},
		drawFloorForPreview(data) {
			let svg = this.$d3.select('#preview-container');
			svg.select('g.drawPoly').remove();
			let g = svg.append('g');
			g.append('polygon')
				.attr('points', data)
				.style('fill', 'none')
				.style("stroke", "black")
				.style("stroke-width", "2");
		},
		clearPreviewArea() {
			this.$d3.selectAll('#preview-container > g').remove();
		}
	},
	computed: {
		...mapGetters(['FLOORS'])
	}
}
</script>

<style scoped>
	#preview-container-area {
		margin: 10px auto;
		border: 1px solid black;
		width: 851px;
		height: 606px;
		overflow: hidden;
	}
	#preview-container {
		height: 100%;
		width: 100%;
		touch-action: none;
		overflow: hidden;
	}
</style>