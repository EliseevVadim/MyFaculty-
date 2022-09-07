<template>
    <div>
        <h1 class="text-center mt-3">Управление списком типов вторичных объектов</h1>
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
                    <span class="text-h5">{{updating ? 'Обновить информацию о типе объекта' : 'Добавить тип объекта'}}</span>
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
                                    label="Название типа*"
                                    required
                                    :rules="commonRules"
                                    hide-details="auto"
                                    v-model="object_type.object_type_name"
                                ></v-text-field>
                            </v-col>
                            <v-col cols="12">
                                <v-text-field
                                    label="Содержимое svg-изображения*"
                                    required
                                    :rules="commonRules"
                                    hide-details="auto"
                                    v-model="object_type.type_path"
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
                Список типов вторичных объектов
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
                :items="this.OBJECT_TYPES.secondaryObjectTypes"
                :search="search"
                class="text-left"
            >
                <template v-slot:body="{items}">
                    <tbody>
                    <tr v-for="(item,index) in items" :key="index">
                        <td>
                            {{item.objectTypeName}}
                        </td>
                        <td>
                            {{item.typePath}}
                        </td>
                        <td>
                            <v-btn
                                class="mx-2"
                                fab
                                small
                                color="yellow"
                                @click="startUpdatingObjectType(item.id)"
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
                                @click="deleteObjectType(item.id)"
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
    name: "SecondaryObjectTypesView",
    data() {
		return {
			showAddingForm: false,
			search: '',
			formValid: true,
			updating: false,
			errorText: "",
			object_type: {
				object_type_name: "",
				type_path: ""
			},
			commonRules: [
				v => !!v || 'Поле является обязательным для заполнения'
			],
			headers: [
				{
					text: 'Название типа',
					align: 'start',
					value: 'objectTypeName',
				},
				{ text: 'Содержимое svg-изображения', value: 'typePath' },
				{ text: 'Действия', value: 'actions', sortable: false }
			],
        }
    },
	mounted() {
		this.$store.dispatch('loadAllObjectTypes');
	},
    methods: {
		openAddingForm() {
			this.errorText = "";
			this.resetObjectType();
			this.updating = false;
			this.showAddingForm = true;
		},
		sendData() {
			this.formValid = this.$refs.submitForm.validate();
			if(!this.formValid)
				return;
			this.errorText = "";
			if(!this.updating) {
				this.$store.dispatch('addObjectType', this.object_type)
					.then(() => {
						this.resetObjectType();
						this.showAddingForm = false;
						this.$store.dispatch('loadAllObjectTypes');
					})
					.catch(() => {
						this.errorText = "Произошла ошибка добавления записи.";
					});
			}
			else {
				this.$store.dispatch('updateObjectType', this.object_type)
					.then(() => {
						this.resetObjectType();
						this.showAddingForm = false;
						this.$store.dispatch('loadAllObjectTypes');
					})
					.catch(() => {
						this.errorText = "Произошла ошибка обновления записи.";
					});
			}
		},
		resetObjectType() {
			this.object_type.object_type_name = "";
			this.object_type.type_path = "";
		},
		deleteObjectType(id) {
			if (confirm("Вы действительно хотите удалить данную запись")) {
				this.$store.dispatch('deleteObjectType', id)
					.then(() => {
						this.resetObjectType();
						this.$store.dispatch('loadAllObjectTypes');
					})
					.catch(() => {
						this.resetObjectType();
						alert("Невозможно удалить запись");
					})
			}
		},
		startUpdatingObjectType(id) {
			this.errorText = "";
			this.object_type.id = id;
			this.$store.dispatch('loadObjectTypeById', id)
				.then((response) => {
					this.object_type.id = response.data.id;
					this.object_type.object_type_name = response.data.objectTypeName;
					this.object_type.type_path = response.data.typePath;
					this.updating = true;
					this.showAddingForm = true;
				})
		}
    },
    computed: {
        ...mapGetters(['OBJECT_TYPES'])
    }
}
</script>

<style scoped>

</style>