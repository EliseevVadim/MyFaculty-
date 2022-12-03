<template>
	<div>
		<h1 class="text-center mt-3">Управление списком стран</h1>
		<v-btn
			class="mt-2 mb-3 mx-5"
			fab
			dark
			color="indigo"
			v-ripple
			left
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
					<span class="text-h5">{{updating ? 'Обновить информацию о стране' : 'Добавить страну'}}</span>
				</v-card-title>
				<v-card-text>
					<h2 class="text-center red--text">{{errorText}}</h2>
					<v-container>
						<v-form
							lazy-validation
							ref="submitForm"
							v-model="formValid"
						>
							<v-col cols="12">
								<v-text-field
									label="Название страны*"
									required
									:rules="commonRules"
									hide-details="auto"
									v-model="country.country_name"
								></v-text-field>
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
				Список стран
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
				:items="this.COUNTRIES.countries"
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
							<v-btn
								class="mx-2"
								fab
								small
								color="yellow"
								@click="startUpdatingCountry(item.id)"
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
								@click="deleteCountry(item.id)"
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
	name: "CountriesView",
	data() {
		return {
			showAddingForm: false,
			search: '',
			formValid: true,
			updating: false,
			errorText: "",
			country: {
				id: null,
				country_name: ""
			},
			commonRules: [
				v => !!v || 'Поле является обязательным для заполнения'
			],
			headers: [
				{
					text: 'id',
					align: 'start',
					value: 'id',
				},
				{ text: 'Название страны', value: 'countryName' },
				{ text: 'Действия', value: 'actions', sortable: false }
			],
		}
	},
	mounted() {
		this.$store.dispatch('loadAllCountries');
	},
	methods: {
		openAddingForm() {
			this.errorText = "";
			this.resetCountry();
			this.updating = false;
			this.showAddingForm = true;
		},
		sendData() {
			this.formValid = this.$refs.submitForm.validate();
			if(!this.formValid)
				return;
			this.errorText = "";
			if(!this.updating) {
				this.$store.dispatch('addCountry', this.country)
					.then(() => {
						this.resetCountry();
						this.showAddingForm = false;
						this.$store.dispatch('loadAllCountries');
					})
					.catch(() => {
						this.errorText = "Произошла ошибка добавления записи.";
					});
			}
			else {
				this.$store.dispatch('updateCountry', this.country)
					.then(() => {
						this.resetCountry();
						this.showAddingForm = false;
						this.$store.dispatch('loadAllCountries');
					})
					.catch(() => {
						this.errorText = "Произошла ошибка обновления записи.";
					});
			}
		},
		resetCountry() {
			this.country.country_name = "";
			this.country.id = null;
		},
		deleteCountry(id) {
			this.$confirm("Вы действительно хотите удалить данную запись?")
				.then((result) => {
					if (result) {
						this.$store.dispatch('deleteCountry', id)
							.then(() => {
								this.resetCountry();
								this.$store.dispatch('loadAllCountries');
							})
							.catch(() => {
								this.resetCountry();
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
		startUpdatingCountry(id) {
			this.errorText = "";
			this.country.id = id;
			this.$store.dispatch('loadCountryById', id)
				.then((response) => {
					this.country.id = response.data.id;
					this.country.country_name = response.data.countryName;
					this.updating = true;
					this.showAddingForm = true;
				})
		}
	},
	computed: {
		...mapGetters(['COUNTRIES'])
	}
}
</script>

<style scoped>

</style>