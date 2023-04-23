<template>
    <div>
        <h1 class="text-center mt-3">Управление списком городов</h1>
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
                    <span class="text-h5">{{ updating ? 'Обновить информацию о городе' : 'Добавить город' }}</span>
                </v-card-title>
                <v-card-text>
                    <h2 class="text-center red--text">{{ errorText }}</h2>
                    <v-container>
                        <v-form
                            lazy-validation
                            ref="submitForm"
                            v-model="formValid">
                            <v-col
                                cols="12"
                            >
                                <v-select
                                    :items="this.COUNTRIES.countries"
                                    item-text="countryName"
                                    item-value="id"
                                    :rules="commonRules"
                                    label="Выберите страну*"
                                    @change="loadRegionsList"
                                    v-model="selectedCountryId"
                                ></v-select>
                            </v-col>
                            <v-col
                                v-if="regionsAreLoaded"
                                cols="12"
                            >
                                <v-autocomplete
                                    label="Название региона*"
                                    required
                                    :rules="commonRules"
                                    :items="REGIONS.regions"
                                    item-text="regionName"
                                    item-value="id"
                                    hide-details="auto"
                                    v-model="city.region_id"
                                ></v-autocomplete>
                            </v-col>
                            <h1 v-else-if="regionsAreLoaded === false" class="red--text">
                                Для данной страны информация о регионах отсутствует.
                                Дальнейшее заполнение невозможно.
                            </h1>
                            <v-col cols="12">
                                <v-text-field
                                    label="Название города*"
                                    required
                                    :rules="commonRules"
                                    hide-details="auto"
                                    v-model="city.city_name"
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
                Список групп
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
                :items="this.CITIES.cities"
                :search="search"
                class="text-left"
            >
                <template v-slot:body="{items}">
                    <tbody>
                    <tr v-for="(item,index) in items" :key="index">
                        <td>
                            {{ item.cityName }}
                        </td>
                        <td>
                            {{ item.regionName }}
                        </td>
                        <td>
                            {{ item.countryName }}
                        </td>
                        <td>
                            <v-btn
                                class="mx-2"
                                fab
                                small
                                color="yellow"
                                @click="startUpdatingCity(item.id)"
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
                                @click="deleteCity(item.id)"
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
    name: "CitiesView",
    data() {
        return {
            selectedCountryId: null,
            regionsAreLoaded: null,
            showAddingForm: false,
            search: '',
            formValid: true,
            updating: false,
            errorText: "",
            city: {
                region_id: null,
                city_name: ""
            },
            commonRules: [
                v => !!v || 'Поле является обязательным для заполнения'
            ],
            headers: [
                {
                    text: 'Название города',
                    align: 'start',
                    value: 'cityName',
                },
                {text: 'Название региона', value: 'regionName'},
                {text: 'Название страны', value: 'countryName'},
                {text: 'Действия', value: 'actions', sortable: false}
            ],
        }
    },
    mounted() {
        this.$store.dispatch('loadAllCountries');
        this.$store.dispatch('loadAllCities');
    },
    methods: {
        openAddingForm() {
            this.errorText = "";
            this.resetCity();
            this.updating = false;
            this.showAddingForm = true;
        },
        loadRegionsList() {
            this.$loading(true);
            this.$store.dispatch('loadRegionsByCountryId', this.selectedCountryId)
                .then(() => {
                    this.$loading(false);
                    this.regionsAreLoaded = this.REGIONS.regions.length > 0;
                })
        },
        sendData() {
            this.formValid = this.$refs.submitForm.validate();
            if (!this.formValid)
                return;
            this.errorText = "";
            if (!this.updating) {
                this.$store.dispatch('addCity', this.city)
                    .then(() => {
                        this.resetCity();
                        this.showAddingForm = false;
                        this.$store.dispatch('loadAllCities');
                    })
                    .catch(() => {
                        this.errorText = "Произошла ошибка добавления записи.";
                    });
            } else {
                this.$store.dispatch('updateCity', this.city)
                    .then(() => {
                        this.resetCity();
                        this.showAddingForm = false;
                        this.$store.dispatch('loadAllCities');
                    })
                    .catch(() => {
                        this.errorText = "Произошла ошибка обновления записи.";
                    });
            }
        },
        resetCity() {
            this.city.city_name = "";
            this.city.region_id = null;
            this.selectedCountryId = null;
            this.regionsAreLoaded = null;
        },
        deleteCity(id) {
            this.$confirm("Вы действительно хотите удалить данную запись?")
                .then((result) => {
                    if (result) {
                        this.$store.dispatch('deleteCity', id)
                            .then(() => {
                                this.resetCity();
                                this.$store.dispatch('loadAllCities');
                            })
                            .catch(() => {
                                this.resetCity();
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
        startUpdatingCity(id) {
            this.errorText = "";
            this.city.id = id;
            this.$store.dispatch('loadCityById', id)
                .then((response) => {
                    this.selectedCountryId = response.data.region.countryId;
                    this.city.id = response.data.id;
                    this.city.city_name = response.data.cityName;
                    this.city.region_id = response.data.regionId;
                    this.loadRegionsList();
                    this.updating = true;
                    this.showAddingForm = true;
                })
        }
    },
    computed: {
        ...mapGetters(['COUNTRIES']),
        ...mapGetters(['REGIONS']),
        ...mapGetters(['CITIES'])
    }
}
</script>

<style scoped>

</style>