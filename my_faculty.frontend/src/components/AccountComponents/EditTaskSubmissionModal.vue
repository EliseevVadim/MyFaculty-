<template>
    <v-dialog
        v-model="displayModal"
        max-width="800px"
    >
        <v-card>
            <v-card-title class="d-flex justify-center">
                <span class="text-h5">Редактировать решение</span>
            </v-card-title>
            <v-card-text class="pb-0">
                <h2 class="text-center red--text">{{ errorText }}</h2>
                <v-container>
                    <v-form
                        lazy-validation
                        ref="submitForm"
                        v-model="formValid"
                    >
                        <v-col cols="12">
                            <v-text-field
                                label="Имя решения*"
                                required
                                :rules="commonRules"
                                hide-details="auto"
                                v-model="editingSubmission.title"
                            ></v-text-field>
                        </v-col>
                        <v-col cols="12">
                            <tiptap-vuetify
                                class="text-left"
                                v-model="editingSubmission.textContent"
                                placeholder="Введите текстовое содержимое..."
                                :extensions="extensions"
                                :toolbar-attributes="{ color: 'primary', dark: true }"
                            />
                        </v-col>
                        <v-col cols="12">
                            <VueFileAgent
                                v-model="files"
                                resumable="true"
                                deletable="true"
                                help-text="Выберите файлы или перенесите их (максимум 10)"
                                error-text="Произошла ошибка"
                                max-files="10"
                                @beforedelete="fileDeleted($event)"
                            />
                        </v-col>
                    </v-form>
                </v-container>
            </v-card-text>
            <v-card-actions
                class="flex-column flex-sm-row"
            >
                <v-btn
                    color="red darken-1"
                    text
                    @click="displayModal = false"
                    class="ma-2 ma-sm-0"
                >
                    Закрыть
                </v-btn>
                <v-spacer></v-spacer>
                <v-btn
                    color="success"
                    @click="editSubmission"
                    :disabled="!formValid"
                >
                    Редактировать решение
                </v-btn>
            </v-card-actions>
        </v-card>
    </v-dialog>
</template>

<script>
import {
    TiptapVuetify,
    Bold,
    Italic,
    Strike,
    Underline,
    BulletList,
    OrderedList,
    ListItem,
    Link,
    HardBreak,
    History
} from "tiptap-vuetify"

export default {
    name: "EditTaskSubmissionModal",
    components: {TiptapVuetify},
    props: ['show', 'submission'],
    data() {
        return {
            commonRules: [
                v => !!v || 'Поле является обязательным для заполнения'
            ],
            formValid: false,
            errorText: "",
            files: [],
            oldAttachments: '',
            editingSubmission: {
                id: null,
                title: null,
                textContent: null,
                attachments: [],
                submissionAttachmentsUid: '',
                authorId: null
            },
            extensions: [
                History,
                Link,
                Underline,
                Strike,
                Italic,
                ListItem,
                BulletList,
                OrderedList,
                Bold,
                Link,
                HardBreak
            ]
        }
    },
    methods: {
        close() {
            this.$emit('close');
        },
        processAttachments() {
            let attachments = JSON.parse(this.submission.attachments);
            if (!attachments)
                return;
            this.files = attachments
                .map(element => {
                    return {
                        name: element.FileName,
                        ext: element.Extension,
                        size: element.Length,
                        type: element.ContentType,
                        url: element.Path
                    }
                });
        },
        fileDeleted(file) {
            let index = this.files.indexOf(file);
            if (index === -1)
                return;
            this.files.splice(index, 1);
        },
        loadSubmission() {
            this.editingSubmission = JSON.parse(JSON.stringify(this.submission));
            this.oldAttachments = this.submission.attachments;
            this.processAttachments();
        },
        editSubmission() {
            if (!this.editingSubmission.attachments && !this.editingSubmission.textContent) {
                this.$notify({
                    group: 'admin-actions',
                    title: 'Ошибка',
                    text: 'Решение должно содержать хотя бы текстовый контент или прикрепленные файлы',
                    type: 'error'
                })
                return;
            }
            this.editingSubmission.oldAttachments = this.oldAttachments;
            this.editingSubmission.attachments = JSON.stringify(this.files
                .filter(element => !(element.file instanceof File))
                .map(element => {
                    return element.file !== undefined ? {
                        FileName: element.file.name,
                        Extension: element.ext,
                        Length: element.file.size,
                        ContentType: element.type,
                        Path: element.url()
                    } : {
                        FileName: element.name(),
                        Extension: element.ext,
                        Length: element.size,
                        ContentType: element.type,
                        Path: element.url
                    }
                }));
            this.editingSubmission.newFiles = this.files
                .map(element => element.file)
                .filter(element => element instanceof File);
            this.editingSubmission.issuerId = this.$oidc.currentUserId;
            this.$loading(true);
            this.$store.dispatch('updateTaskSubmission', this.editingSubmission)
                .then(() => {
                    this.reloadSubmissionsWithMessage('Решение успешно обновлено');
                })
                .catch((error) => {
                    this.$notify({
                        group: 'admin-actions',
                        title: 'Ошибка',
                        text: error.response.data.error,
                        type: 'error'
                    })
                })
                .finally(() => {
                    this.$loading(false);
                })
        },
        reloadSubmissionsWithMessage(message) {
            this.$emit('load');
            this.$notify({
                group: 'admin-actions',
                title: 'Успешная операция',
                text: message,
                type: 'success'
            });
            this.close();
        }
    },
    computed: {
        displayModal: {
            get() {
                return this.show;
            },
            set(value) {
                if (!value)
                    this.$emit('close');
            }
        }
    }
}
</script>

<style scoped>

</style>