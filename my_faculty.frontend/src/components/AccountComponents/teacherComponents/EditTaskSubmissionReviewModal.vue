<template>
    <v-dialog
        v-model="displayModal"
        max-width="800px"
    >
        <v-card>
            <v-card-title class="d-flex justify-center">
                <span class="text-h5">Редактировать отзыв на решение</span>
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
                            <tiptap-vuetify
                                class="text-left"
                                v-model="editingReview.textContent"
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
                        <v-col cols="12">
                            <v-text-field
                                type="number"
                                :rules="rateRules"
                                :label="`Укажите оценку (максимум ${maxRate})*`"
                                v-model.number="editingReview.rate"
                            >
                            </v-text-field>
                        </v-col>
                        <v-col cols="12">
                            <v-select
                                :items="submissionStatuses"
                                :rules="selectRules"
                                item-text="text"
                                item-value="value"
                                label="Выберите итоговый статус решения*"
                                v-model="editingReview.newStatus"
                                required
                            >
                            </v-select>
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
                    @click="editReview"
                    :disabled="!formValid"
                >
                    Редактировать отзыв
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
    name: "EditTaskSubmissionReviewModal",
    components: {TiptapVuetify},
    props: ['show', 'review', 'maxRate', 'submissionStatus'],
    data() {
        return {
            formValid: false,
            errorText: "",
            files: [],
            oldAttachments: '',
            editingReview: {
                id: null,
                reviewerId: null,
                textContent: null,
                attachments: [],
                rate: null,
                newStatus: 2,
                submissionReviewAttachmentsUid: ''
            },
            selectRules: [
                v => this.submissionStatuses
                    .map(status => status.value)
                    .indexOf(v) >= 0 || 'Поле является обязательным для заполнения'
            ],
            rateRules: [
                v => v !== null || "Поле является обязательным для заполнения",
                v => v >= 0 || "Оценка должна быть не меньше 0",
                v => v <= this.maxRate || `Оценка должна быть не больше ${this.maxRate}`,
            ],
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
            ],
            submissionStatuses: [
                {
                    text: "Оценено",
                    value: 2
                },
                {
                    text: "Возвращено для доработки",
                    value: 3
                }
            ]
        }
    },
    methods: {
        close() {
            this.$emit('close');
        },
        processAttachments() {
            let attachments = JSON.parse(this.review.attachments);
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
        loadReview() {
            this.editingReview = JSON.parse(JSON.stringify(this.review));
            this.editingReview.newStatus = this.submissionStatus;
            this.oldAttachments = this.review.attachments;
            this.processAttachments();
        },
        editReview() {
            this.editingReview.oldAttachments = this.oldAttachments;
            this.editingReview.attachments = JSON.stringify(this.files
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
            this.editingReview.newFiles = this.files
                .map(element => element.file)
                .filter(element => element instanceof File);
            this.editingReview.issuerId = this.$oidc.currentUserId;
            this.$loading(true);
            this.$store.dispatch('updateTaskSubmissionReview', this.editingReview)
                .then(() => {
                    this.reloadReviewsWithMessage('Отзыв успешно обновлен');
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
        reloadReviewsWithMessage(message) {
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