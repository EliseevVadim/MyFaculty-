<template>
    <div>
        <h1 class="text-center mt-3">Очистка ненужных файлов*</h1>
        <h2 class="text-center px-3 mt-3 red--text">
            *ВНИМАНИЕ! Раздел рекомендуется использовать ТОЛЬКО при полной уверенности в ненужности
            удаляемого содержимого (перманентный бан сообщества и т. д.). Удаленные вместе с файлами записи,
            комментарии и т. д. возврату не подлежат!
        </h2>
        <v-card>
            <v-card-title>
                Заблокированные сообщества
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
                :items="this.INFO_PUBLICS.informationPublics"
                :search="search"
                class="text-left"
            >
                <template v-slot:body="{items}">
                    <tbody>
                    <tr v-for="(item,index) in items" :key="index">
                        <td>
                            <router-link :to="`/public${item.id}`">
                                {{item.id}}
                            </router-link>
                        </td>
                        <td>
                            {{ item.publicName }}
                        </td>
                        <td>
                            <v-btn
                                class="mx-2"
                                fab
                                small
                                color="error"
                                @click="deleteInfoPublicMedia(item.id)"
                            >
                                <v-icon dark>
                                    mdi-delete-forever
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
    name: "ClearMediaView",
    data() {
        return {
            search: '',
            headers: [
                {
                    text: 'id',
                    align: 'start',
                    value: 'id',
                },
                {text: 'Название сообщества', value: 'publicName'},
                {text: 'Действия', value: 'actions', sortable: false}
            ]
        }
    },
    methods: {
        deleteInfoPublicMedia(id) {
            this.$confirm(`Вы действительно хотите удалить все компоненты (записи, комментарии,
                их вложения), связанные с сообществом с id = ${id}?`, {
                color: 'error'
            })
                .then((result) => {
                    if (result) {
                        this.$loading(true);
                        this.$store.dispatch('deleteInfoPublicContent', id)
                            .then(() => {
                                this.$notify({
                                    group: 'admin-actions',
                                    title: 'Успешная операция',
                                    text: 'Содержимое сообщества успешно удалено',
                                    type: 'success'
                                });
                            })
                            .catch((error) => {
                                this.$notify({
                                    group: 'admin-actions',
                                    title: 'Ошибка',
                                    text: error.response.data.error,
                                    type: 'error'
                                });
                            })
                            .finally(() => {
                                this.$loading(false);
                            })
                    }
                })
        }
    },
    mounted() {
        this.$store.dispatch('loadBannedInfoPublics');
    },
    computed: {
        ...mapGetters(['INFO_PUBLICS'])
    }
}
</script>

<style scoped>

</style>