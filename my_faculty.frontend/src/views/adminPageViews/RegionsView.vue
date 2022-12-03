<template>
	<div>
		<h1 class="text-center mt-3">Управление списком регионов</h1>
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
			max-width="600px"
		>
			<v-card>
				<v-card-title>
					<span class="text-h5">{{updating ? 'Обновить информацию о регионе' : 'Добавить регион'}}</span>
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
									label="Название региона*"
									required
									:rules="commonRules"
									hide-details="auto"
									v-model="region.region_name"
								></v-text-field>
							</v-col>
							<v-col cols="12">
								<v-autocomplete
									label="Название страны*"
									required
									:rules="commonRules"
									:items="this.COUNTRIES.countries"
									item-text="countryName"
									item-value="id"
									hide-details="auto"
									v-model="region.country_id"
								></v-autocomplete>
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
				Список регионов / областей
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
				:items="this.REGIONS.regions"
				:search="search"
				class="text-left"
			>
				<template v-slot:body="{items}">
					<tbody>
					<tr v-for="(item,index) in items" :key="index">
						<td>
							{{item.id}}
						</td>
						<td>
							{{item.countryName}}
						</td>
						<td>
							{{item.regionName}}
						</td>
						<td>
							<v-btn
								class="mx-2"
								fab
								small
								color="yellow"
								@click="startUpdatingRegion(item.id)"
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
								@click="deleteRegion(item.id)"
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
	name: "RegionsView",
	data () {
		return {
			showAddingForm: false,
			search: '',
			formValid: true,
			updating: false,
			errorText: "",
			region: {
				region_name: "",
				country_id: null
			},
			numberRules: [
				v => !!v || 'Поле является обязательным для заполнения',
				v => Number.isInteger(Number(v)) || "Значение должно быть целым числом"
			],
			commonRules: [
				v => !!v || 'Поле является обязательным для заполнения'
			],
			headers: [
				{
					text: 'Id региона',
					align: 'start',
					value: 'id',
				},
				{ text: 'Название страны', value: 'countryName' },
				{ text: 'Название региона', value: 'regionName' },
				{ text: 'Действия', value: 'actions', sortable: false }
			],
		}
	},
	mounted() {
		this.$store.dispatch('loadAllCountries');
		this.$store.dispatch('loadAllRegions');
	},
	methods: {
		openAddingForm() {
			this.errorText = "";
			this.resetRegion();
			this.updating = false;
			this.showAddingForm = true;
		},
		sendData() {
			this.formValid = this.$refs.submitForm.validate();
			if(!this.formValid)
				return;
			this.errorText = "";
			if(!this.updating) {
				this.$store.dispatch('addRegion', this.region)
					.then(() => {
						this.resetRegion();
						this.showAddingForm = false;
						this.$store.dispatch('loadAllRegions');
					})
					.catch(() => {
						this.errorText = "Произошла ошибка добавления записи.";
					});
			}
			else {
				this.$store.dispatch('updateRegion', this.region)
					.then(() => {
						this.resetRegion();
						this.showAddingForm = false;
						this.$store.dispatch('loadAllRegions');
					})
					.catch(() => {
						this.errorText = "Произошла ошибка обновления записи.";
					});
			}
		},
		resetRegion() {
			this.region.region_name = "";
			this.region.country_id = null;
		},
		deleteRegion(id) {
			this.$confirm("Вы действительно хотите удалить данную запись?")
				.then((result) => {
					if (result) {
						this.$store.dispatch('deleteRegion', id)
							.then(() => {
								this.resetRegion();
								this.$store.dispatch('loadAllRegions');
							})
							.catch(() => {
								this.resetRegion();
								this.$notify({
									group: 'admin-actions',
									title: 'Ошибка',
									text: 'Невозможно удалить запись',
									type: 'error'
								});
							})
					}
				});
		},
		startUpdatingRegion(id) {
			this.errorText = "";
			this.region.id = id;
			this.$store.dispatch('loadRegionById', id)
				.then((response) => {
					this.region.id = response.data.id;
					this.region.region_name = response.data.regionName;
					this.region.country_id = response.data.countryId;
					this.updating = true;
					this.showAddingForm = true;
				})
		}
	},
	computed: {
		...mapGetters(['COUNTRIES']),
		...mapGetters(['REGIONS'])
	}
}
</script>

<style scoped>

</style>