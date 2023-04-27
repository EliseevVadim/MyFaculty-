<template>
    <div>
        <EditTaskSubmissionModal
            :show="showEditingForm"
            :submission="submission"
            @close="showEditingForm = false"
            @load="reloadSubmissions"
            ref="editingForm"
        />
        <CreateTaskSubmissionReviewModal
            :show="showReviewForm"
            :submission-id="submission.id"
            :max-rate="submission.maxRate"
            @close="showReviewForm = false"
            @load="reloadSubmissions"
            ref="reviewForm"
        />
        <div class="d-flex flex-column" :id="'submission_' + submission.id">
            <v-container
                fluid
                class="submission-container mb-3 px-0 pb-0"
            >
                <div class="review-content">
                    <h2 class="submission-title">
                        {{ submission.title }}
                    </h2>
                    <v-list-item
                        class="mb-3"
                        :key="getFullName(submission.author)"
                    >
                        <v-list-item-content class="submission-metadata">
                            <v-list-item-title>
                                <router-link class="author-name" :to="'/id' + submission.authorId">
                                    {{ getFullName(submission.author) }}
                                </router-link>
                            </v-list-item-title>
                            <v-list-item-subtitle v-html="prettifyPublishDate()">
                            </v-list-item-subtitle>
                        </v-list-item-content>
                        <v-menu
                            v-if="!(currentUserIsTaskModerator && submission.review)"
                            bottom
                            left
                        >
                            <template v-slot:activator="{ on, attrs }">
                                <v-btn
                                    icon
                                    v-bind="attrs"
                                    v-on="on"
                                >
                                    <v-icon>mdi-dots-horizontal</v-icon>
                                </v-btn>
                            </template>
                            <v-list>
                                <v-list-item
                                    ripple
                                    v-for="(action, key) in currentUserActions"
                                    :key="key"
                                >
                                    <v-list-item-title
                                        class="submission-context-menu-option"
                                        @click="action.method"
                                    >
                                        {{ action.title }}
                                    </v-list-item-title>
                                </v-list-item>
                            </v-list>
                        </v-menu>
                    </v-list-item>
                    <v-row
                        class="mx-1 text-left pt-2 pb-2 mb-1"
                        :class="this.submissionStatusExplanations[submission.status].color"
                    >
						<span class="mx-2 submission-status-mark">
							Статус решения:
						</span>
                        <span class="mx-2 submission-status-value">
							{{ this.submissionStatusExplanations[submission.status].text }}
						</span>
                    </v-row>
                    <v-row
                        class="ml-3 text-left"
                        v-html="submission.textContent"
                    >
                    </v-row>
                    <viewer
                        class="image-gallery"
                        :images="images"
                    >
                        <img
                            v-for="src in images"
                            :key="src"
                            :src="src"
                            alt="#"
                        >
                    </viewer>
                    <div class="theme-list" v-if="otherFiles.length > 0">
                        <div class="vue-file-agent grid-block-wrapper" style="padding: 0;">
                            <template v-for="(fileRecord, i) in otherFiles">
                                <VueFilePreview
                                    :key="i"
                                    :value="fileRecord"
                                    :linkable="true"
                                    class="file-preview-wrapper grid-box-item grid-block"
                                    style="z-index: 0"
                                ></VueFilePreview>
                            </template>
                        </div>
                    </div>
                </div>
                <TaskSubmissionReviewPresenter
                    v-if="submission.review"
                    v-for="review in [submission.review]"
                    :key="review.id + review.updated"
                    :review="submission.review"
                    :max-rate="submission.maxRate"
                    :submission-status="submission.status"
                    @load="reloadSubmissions"
                />
            </v-container>
        </div>
    </div>
</template>

<script>
import EditTaskSubmissionModal from "@/components/AccountComponents/EditTaskSubmissionModal";
import CreateTaskSubmissionReviewModal
    from "@/components/AccountComponents/teacherComponents/CreateTaskSubmissionReviewModal";
import TaskSubmissionReviewPresenter from "@/components/presenters/TaskSubmissionReviewPresenter";

export default {
    name: "TaskSubmissionPresenter",
    components: {TaskSubmissionReviewPresenter, CreateTaskSubmissionReviewModal, EditTaskSubmissionModal},
    props: ['submission', 'currentUserIsTaskModerator'],
    data() {
        return {
            currentUserActions: [],
            userActions: [
                {
                    title: "Редактировать решение",
                    method: this.startSubmissionEditing
                },
                {
                    title: "Удалить решение",
                    method: this.deleteSubmission
                }
            ],
            moderatorActions: [
                {
                    title: "Добавить отзыв",
                    method: this.startReviewAdding
                }
            ],
            showEditingForm: false,
            showReviewForm: false,
            images: [],
            otherFiles: [],
            submissionStatusExplanations: {
                1: {
                    text: "Отправлено для оценивания",
                    color: "grey lighten-2"
                },
                2: {
                    text: "Оценено",
                    color: "green accent-2"
                },
                3: {
                    text: "Возвращено для доработки",
                    color: "red accent-2"
                }
            }
        }
    },
    methods: {
        getFullName(user) {
            return `${user.firstName} ${user.lastName}`;
        },
        prettifyPublishDate() {
            let creationDate = new Date(this.submission.created).toLocaleString('ru-RU', {
                year: 'numeric',
                month: 'long',
                day: 'numeric',
                hour: 'numeric',
                minute: 'numeric'
            });
            if (!this.submission.updated)
                return creationDate;
            let updateDate = new Date(this.submission.updated).toLocaleString('ru-RU', {
                year: 'numeric',
                month: 'long',
                day: 'numeric',
                hour: 'numeric',
                minute: 'numeric'
            });
            return `${creationDate} <wbr>(обновлено ${updateDate})`;
        },
        processAttachments() {
            let attachments = JSON.parse(this.submission.attachments);
            if (!attachments)
                return;
            this.images = attachments
                .filter(element => element.ContentType.startsWith('image'))
                .map(element => element.Path);
            this.otherFiles = attachments
                .filter(element => !element.ContentType.startsWith('image'))
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
        startSubmissionEditing() {
            this.$refs.editingForm.loadSubmission();
            this.showEditingForm = true;
        },
        startReviewAdding() {
            this.showReviewForm = true;
        },
        deleteSubmission() {
            this.$confirm("Вы действительно хотите удалить это решение? Все вложения будут" +
                " потеряны навсегда")
                .then((result) => {
                    if (result) {
                        this.$loading(true);
                        this.$store.dispatch('deleteTaskSubmission', this.submission.id)
                            .then(() => {
                                this.$notify({
                                    group: 'admin-actions',
                                    title: 'Успешная операция',
                                    text: 'Решение успешно удалено',
                                    type: 'success'
                                });
                                this.$emit('load');
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
                    }
                })
        },
        reloadSubmissions() {
            this.$emit('load');
        }
    },
    mounted() {
        this.currentUserActions = this.currentUserIsTaskModerator ? this.moderatorActions : this.userActions;
        this.processAttachments();
    }
}
</script>

<style scoped>
.submission-container {
    background: white;
    border-radius: 10px;
}

.image-gallery {
    display: grid;
    grid-template-columns: repeat(3, 1fr);
    grid-gap: 20px;
}

img {
    width: 100%;
    padding: 10px;
    object-fit: contain;
    cursor: pointer;
    position: relative;
    border-radius: 8%;
}

.author-name {
    font-weight: bolder;
    font-size: 16px;
    color: #4040c0;
}

.submission-context-menu-option {
    text-align: left;
    cursor: pointer;
}

.submission-metadata {
    text-align: left;
}

.review-content {
    padding-right: 12px;
    padding-left: 12px;
}

.submission-title {
    color: black;
}

.submission-status-mark {
    color: black;
    opacity: 0.5;
    font-size: 22px;
}

.submission-status-value {
    color: black;
    font-style: italic;
    font-size: 22px;
}

@media only screen and (max-width: 600px) {
    .image-gallery {
        grid-template-columns: repeat(2, 1fr);
    }
}

@media only screen and (max-width: 400px) {
    .image-gallery {
        grid-template-columns: repeat(1, 1fr);
    }
}
</style>