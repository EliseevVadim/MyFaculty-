<template>
	<div>
		<h1 class="text-center mt-3">Управление списком вторичных объектов</h1>
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
						@click="closeModal"
					>
						<v-icon dark>
							mdi-close
						</v-icon>
					</v-btn>
				</v-card-actions>
				<v-card-title>
					<span class="text-h5">{{updating ? 'Обновить информацию о вторичном объекте' : 'Добавить вторичный объект'}}</span>
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
									label="Название вторичного объекта*"
									required
									:rules="commonRules"
									hide-details="auto"
									v-model="secondary_object.object_name"
								></v-text-field>
							</v-col>
							<v-col
								cols="12"
							>
								<v-select
									:items="this.FACULTIES.faculties"
									item-text="facultyName"
									item-value="id"
									:rules="commonRules"
									label="Выберите факультет*"
									@change="loadFloorsList"
									v-model="selectedFacultyId"
								></v-select>
							</v-col>
							<v-col
								v-if="floorsAreLoaded"
								cols="12"
							>
								<v-select
									:items="this.FLOORS.floors"
									item-text="name"
									item-value="id"
									:rules="commonRules"
									label="Выберите этаж*"
									@change="drawFloorBoundsById()"
									v-model="secondary_object.floor_id"
								></v-select>
							</v-col>
							<h1 v-else-if="floorsAreLoaded === false" class="red--text">
								Для данного факультета информация об этажах отсутствует.
								Дальнейшее заполнение невозможно.
							</h1>
							<v-col
								cols="12"
							>
								<v-select
									:items="this.OBJECT_TYPES.secondaryObjectTypes"
									item-text="objectTypeName"
									item-value="id"
									:rules="commonRules"
									label="Выберите тип объекта"
									v-model="secondary_object.object_type_id"
								></v-select>
							</v-col>
							<h4 class="text-center">Данные о расположении:</h4>
							<v-row justify="space-around">
								<v-col cols="3" md="4" class="mt-3">
									<v-text-field
										label="X-координата*"
										required
										:rules="numberRules"
										hide-details="auto"
										v-model.number="secondary_object.position_info.x"
										type="number"
									></v-text-field>
									<v-text-field
										label="Y-координата*"
										required
										:rules="numberRules"
										hide-details="auto"
										v-model.number="secondary_object.position_info.y"
										type="number"
									></v-text-field>
								</v-col>
								<v-col cols="3" md="4" class="mt-3">
									<v-text-field
										label="Относительная ширина*"
										required
										:rules="numberRules"
										hide-details="auto"
										v-model.number="secondary_object.position_info.width"
										type="number"
									></v-text-field>
									<v-text-field
										label="Относительная высота*"
										required
										:rules="numberRules"
										hide-details="auto"
										v-model.number="secondary_object.position_info.height"
										type="number"
									></v-text-field>
								</v-col>
							</v-row>
							<v-col cols="12" class="text-center">
								<v-btn
									v-ripple
									color="success"
									dark
									right
									@click="previewObjectRect"
								>
									Предпросмотр
								</v-btn>
							</v-col>
							<div id="floor-preview-area">
								<svg id="floor-preview-container">

								</svg>
							</div>
							<v-col cols="12" class="text-center">
								<v-btn
									v-ripple
									@click="clearWholePreviewArea"
									color="red"
									dark
								>
									Очистить
								</v-btn>
							</v-col>
						</v-form>
					</v-container>
					<small>Поля, помеченные * обязательны к заполнению</small>
				</v-card-text>
				<v-card-actions>
					<v-spacer></v-spacer>
					<v-btn
						color="blue darken-1"
						text
						@click="closeModal"
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
				Список вторичных объектов
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
				:items="this.SECONDARY_OBJECTS.secondaryObjects"
				:search="search"
				class="text-left"
			>
				<template v-slot:body="{items}">
					<tbody>
					<tr v-for="(item,index) in items" :key="index">
						<td>
							{{item.objectName}}
						</td>
						<td>
							{{item.positionInfo}}
						</td>
						<td>
							{{item.facultyName}}
						</td>
						<td>
							{{item.floorName}}
						</td>
						<td>
							{{item.secondaryObjectType.objectTypeName}}
						</td>
						<td>
							<v-btn
								class="mx-2"
								fab
								small
								color="yellow"
								@click="startUpdatingSecondaryObject(item.id)"
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
								@click="deleteSecondaryObject(item.id)"
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
	</div>
</template>

<script>
import {mapGetters} from "vuex";
export default {
    name: "SecondaryObjectsView",
	data () {
		return {
			selectedFacultyId: null,
			floorsAreLoaded: null,
			showAddingForm: false,
			search: '',
			formValid: true,
			updating: false,
			errorText: "",
			secondary_object: {
				id: "",
				object_name: "",
				position_info: {
					x: "",
					y: "",
					width: "",
					height: ""
				},
				floor_id: "",
				object_type_id: ""
			},
			commonRules: [
				v => !!v || 'Поле является обязательным для заполнения'
			],
			numberRules: [
				v => !!v || 'Поле является обязательным для заполнения',
				v => Number.isFinite(Number(v)) || "Значение должно быть числом"
			],
			headers: [
				{
					text: 'Название',
					align: 'start',
					value: 'objectName',
				},
				{ text: 'Информация о расположении', value: 'positionInfo' },
				{ text: 'Факультет', value: 'facultyName' },
				{ text: 'Этаж', value: 'floorName' },
				{ text: 'Тип', value: 'secondaryObjectType.objectTypeName' },
				{ text: 'Действия', value: 'actions', sortable: false }
			],
		}
	},
	mounted() {
		this.$store.dispatch('loadAllFaculties');
		this.$store.dispatch('loadAllSecondaryObjects');
		this.$store.dispatch('loadAllObjectTypes');
	},
	methods: {
		openAddingForm() {
			this.errorText = "";
			this.resetSecondaryObject();
			this.updating = false;
			this.showAddingForm = true;
		},
		loadFloorsList() {
			this.$loading(true);
			this.$store.dispatch('loadFloorsByFacultyId', this.selectedFacultyId)
				.then(() => {
					this.$loading(false);
					this.floorsAreLoaded = this.FLOORS.floors.length > 0;
				})
		},
		sendData() {
			this.formValid = this.$refs.submitForm.validate();
			if(!this.formValid)
				return;
			this.errorText = "";
			if(!this.updating) {
				this.$store.dispatch('addSecondaryObject', this.secondary_object)
					.then(() => {
						this.resetSecondaryObject();
						this.closeModal();
						this.$store.dispatch('loadAllSecondaryObjects');
					})
					.catch(() => {
						this.errorText = "Произошла ошибка добавления записи.";
					});
			}
			else {
				this.$store.dispatch('updateSecondaryObject', this.secondary_object)
					.then(() => {
						this.resetSecondaryObject();
						this.closeModal();
						this.$store.dispatch('loadAllSecondaryObjects');
					})
					.catch(() => {
						this.errorText = "Произошла ошибка обновления записи.";
					});
			}
		},
		resetSecondaryObject() {
			this.secondary_object.object_name = "";
			this.secondary_object.floor_id = null;
			this.secondary_object.object_type_id = null;
			this.selectedFacultyId = null;
			this.floorsAreLoaded = null;
			this.secondary_object.position_info = {
				x: "",
				y: "",
				width: "",
				height: ""
			};
		},
		deleteSecondaryObject(id) {
			if (confirm("Вы действительно хотите удалить данную запись")) {
				this.$store.dispatch('deleteSecondaryObject', id)
					.then(() => {
						this.resetSecondaryObject();
						this.$store.dispatch('loadAllSecondaryObjects');
					})
					.catch(() => {
						this.resetSecondaryObject();
						alert("Невозможно удалить запись");
					})
			}
		},
		startUpdatingSecondaryObject(id) {
			this.errorText = "";
			this.secondary_object.id = id;
			this.$store.dispatch('loadSecondaryObjectById', id)
				.then((response) => {
					this.selectedFacultyId = response.data.floor.facultyId;
					this.secondary_object.object_name = response.data.objectName;
					this.secondary_object.object_type_id = response.data.secondaryObjectTypeId;
					this.secondary_object.floor_id = response.data.floor.id;
					this.loadFloorsList();
					this.secondary_object.position_info = JSON.parse(response.data.positionInfo);
					this.updating = true;
					this.showAddingForm = true;
					this.drawFloorBoundsById();
				})
		},
		drawFloorBoundsById() {
			this.clearWholePreviewArea();
			this.$store.dispatch('loadFloorById', this.secondary_object.floor_id)
				.then((response) => {
					this.drawBounds(JSON.parse(response.data.bounds));
					this.drawFloorObjects(JSON.parse(JSON.stringify(response.data.auditoriums)), JSON.parse(JSON.stringify(response.data.secondaryObjects)));
				})
		},
		drawBounds(data) {
			let svg = this.$d3.select('#floor-preview-container');
			svg.select('g.drawPoly').remove();
			let g = svg.append('g');
			g.append('polygon')
				.attr('points', data)
				.style('fill', 'none')
				.style("stroke", "black")
				.style("stroke-width", "2");
		},
		drawFloorObjects(auditoriums, secondaryObjects) {
			if (this.updating) {
				let index = secondaryObjects.findIndex((item) => item.id === this.secondary_object.id);
				secondaryObjects.splice(index, 1);
				this.previewObjectRect();
			}
			let data = [];
			for (let i = 0; i < auditoriums.length; i++) {
				data = JSON.parse(auditoriums[i].positionInfo);
				let gAppend = this.$d3.select('#floor-preview-container')
					.append('g')
					.attr('class', 'auditorium');
				gAppend
					.append('rect')
					.style("fill", "white")
					.style("stroke", "black")
					.style("stroke-width", "2")
					.attr("x", data.x)
					.attr("y", data.y)
					.attr("width", data.width)
					.attr("height", data.height);
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
				let secondaryObjectAppend = this.$d3.select('#floor-preview-container')
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
		previewObjectRect() {
			try {
				this.removePreviewRect();
				let dragHandler = this.$d3.drag();
				dragHandler
					.subject(() => {
						let me = this.$d3.select('#floor-preview-container > rect[preview=true]');
						return { x: me.attr('x'), y: me.attr('y') }
					})
					.on("drag", (event, d) => {
						this.moveRect(event, d);
					});
				this.$d3.select('#floor-preview-container')
					.append("rect")
					.style("fill", "white")
					.style("stroke", "red")
					.style("stroke-width", "2")
					.attr("x", this.secondary_object.position_info.x)
					.attr("y", this.secondary_object.position_info.y)
					.attr("width", this.secondary_object.position_info.width)
					.attr("height", this.secondary_object.position_info.height)
					.attr("preview", true)
					.call(dragHandler);
			}
			catch (e) {}
		},
		clearPreviewBounds() {
			this.$d3.selectAll('#floor-preview-container > g').remove();
		},
		removePreviewRect() {
			this.$d3.selectAll('#floor-preview-container > rect[preview=true]').remove();
		},
		clearWholePreviewArea() {
			this.clearPreviewBounds();
			this.removePreviewRect();
			this.$d3.selectAll('rect').remove();
		},
		moveRect(event, d) {
			let x = event.x;
			let y = event.y;
			this.$d3.select('#floor-preview-container > rect[preview=true]')
				.attr("x", x)
				.attr("y", y);
			this.secondary_object.position_info.x = x;
			this.secondary_object.position_info.y = y;
		},
		closeModal() {
			this.clearWholePreviewArea();
			this.showAddingForm = false;
		}
	},
	computed: {
		...mapGetters(['SECONDARY_OBJECTS']),
		...mapGetters(['OBJECT_TYPES']),
		...mapGetters(['FLOORS']),
		...mapGetters(['FACULTIES'])
	}
}
</script>

<style scoped>
	#floor-preview-area {
		margin: 10px auto;
		border: 1px solid black;
		width: 851px;
		height: 606px;
		overflow: hidden;
	}
	#floor-preview-container {
		height: 100%;
		width: 100%;
		touch-action: none;
		overflow: hidden;
	}
</style>