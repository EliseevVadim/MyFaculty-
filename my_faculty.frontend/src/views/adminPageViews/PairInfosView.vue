<template>
    <div>
        <h1 class="text-center mt-3">Управление мета-информацией о парах</h1>
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
                    <span class="text-h5">{{updating ? 'Обновить мета-информацию о паре' : 'Добавить мета-информацию о паре'}}</span>
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
                                    label="Номер пары*"
                                    required
                                    :rules="numberRules"
                                    hide-details="auto"
                                    v-model="pairInfo.pair_number"
                                    type="number"
                                ></v-text-field>
                            </v-col>
                            <v-col cols="12">
                                <v-text-field
                                    label="Начало*"
                                    required
                                    :rules="commonRules"
                                    hide-details="auto"
                                    v-model="pairInfo.start_time"
                                ></v-text-field>
                            </v-col>
                            <v-col cols="12">
                                <v-text-field
                                    label="Конец*"
                                    required
                                    :rules="commonRules"
                                    hide-details="auto"
                                    v-model="pairInfo.end_time"
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
                Мета-информация о парах
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
                :items="this.PAIR_INFOS.pairInfos"
                :search="search"
                class="text-left"
            >
                <template v-slot:body="{items}">
                    <tbody>
                    <tr v-for="(item,index) in items" :key="index">
                        <td>
                            {{item.pairNumber}}
                        </td>
                        <td>
                            {{item.startTime}}
                        </td>
                        <td>
                            {{item.endTime}}
                        </td>
                        <td>
                            <v-btn
                                class="mx-2"
                                fab
                                small
                                color="yellow"
                                @click="startUpdatingPairInfo(item.id)"
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
                                @click="deletePairInfo(item.id)"
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
    name: "PairInfosView",
    data() {
		return {
			showAddingForm: false,
			search: '',
			formValid: true,
			updating: false,
			errorText: "",
			pairInfo: {
				pair_number: null,
				start_time: "",
				end_time: ""
			},
			commonRules: [
				v => !!v || 'Поле является обязательным для заполнения'
			],
			numberRules: [
				v => !!v || 'Поле является обязательным для заполнения',
				v => Number.isInteger(Number(v)) || "Значение должно быть целым числом"
			],
			headers: [
				{
					text: 'Номер пары',
					align: 'start',
					value: 'pairNumber',
				},
				{ text: 'Начало', value: 'startTime' },
				{ text: 'Конец', value: 'endTime' },
				{ text: 'Действия ', value: 'actions', sortable: false }
			]
        }
    },
	mounted() {
		this.$store.dispatch('loadAllPairInfos');
	},
	methods: {
		openAddingForm() {
			this.errorText = "";
			this.resetPairInfo();
			this.updating = false;
			this.showAddingForm = true;
		},
		sendData() {
			this.formValid = this.$refs.submitForm.validate();
			if(!this.formValid)
				return;
			this.errorText = "";
			if(!this.updating) {
				this.$store.dispatch('addPairInfo', this.pairInfo)
					.then(() => {
						this.resetPairInfo();
						this.showAddingForm = false;
						this.$store.dispatch('loadAllPairInfos');
					})
					.catch(() => {
						this.errorText = "Произошла ошибка добавления записи.";
					});
			}
			else {
				this.$store.dispatch('updatePairInfo', this.pairInfo)
					.then(() => {
						this.resetPairInfo();
						this.showAddingForm = false;
						this.$store.dispatch('loadAllPairInfos');
					})
					.catch(() => {
						this.errorText = "Произошла ошибка обновления записи.";
					});
			}
		},
		resetPairInfo() {
			this.pairInfo.pair_number = null;
			this.pairInfo.start_time = "";
			this.pairInfo.end_time = "";
		},
		deletePairInfo(id) {
			if (confirm("Вы действительно хотите удалить данную запись")) {
				this.$store.dispatch('deletePairInfo', id)
					.then(() => {
						this.resetPairInfo();
						this.$store.dispatch('loadAllPairInfos');
					})
					.catch(() => {
						this.resetPairInfo();
						alert("Невозможно удалить запись");
					})
			}
		},
		startUpdatingPairInfo(id) {
			this.errorText = "";
			this.pairInfo.id = id;
			this.$store.dispatch('loadPairInfoById', id)
				.then((response) => {
					this.pairInfo.pair_number = response.data.pairNumber;
					this.pairInfo.start_time = response.data.startTime;
					this.pairInfo.end_time = response.data.endTime;
					this.updating = true;
					this.showAddingForm = true;
				})
		}
	},
    computed: {
		...mapGetters(['PAIR_INFOS'])
    }
}
</script>

<style scoped>

</style>