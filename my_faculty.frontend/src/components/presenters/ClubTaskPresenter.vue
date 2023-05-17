<template>
    <div>
        <EditClubTaskModal
            :show="showEditingForm"
            :task="task"
            @close="showEditingForm = false"
            @load="reloadTasks"
            ref="editingForm"
        />
        <CommentsModal
            v-if="showComments"
            :show="showComments"
            :post-id="task.id"
            :current-user-can-moderate-comments="currentUserIsTaskModerator"
            @close="showComments = false"
            @load="reloadTasks"
        />
        <SubmissionsModal
            v-if="showSubmissions"
            :show="showSubmissions"
            :task-id="task.id"
            :current-user-is-task-moderator="currentUserIsTaskModerator"
            @close="showSubmissions = false"
            @load="reloadTasks"
        />
        <v-container fluid class="task-container mb-3 px-0 pb-0">
            <v-list-item
                class="mb-1"
                :key="task.owningStudyClub.clubName"
            >
                <v-list-item-avatar>
                    <v-img
                        class="author-avatar"
                        :src="task.owningStudyClub.imagePath ? task.owningStudyClub.imagePath : '../img/blank-club.png'"
                    />
                </v-list-item-avatar>
                <v-list-item-content class="club-lookup-content">
                    <v-list-item-title>
                        <router-link class="author-name" :to="'/clubs/' + task.owningStudyClub.id">
                            {{ task.owningStudyClub.clubName }}
                        </router-link>
                    </v-list-item-title>
                    <router-link
                        :to="`/task${task.id}`"
                        class="publish-date"
                    >
                        <v-list-item-subtitle v-html="prettifyPublishDate()">
                        </v-list-item-subtitle>
                    </router-link>
                </v-list-item-content>
                <v-menu
                    v-if="currentUserIsTaskAuthor() || currentUserCanDeleteTheTask()"
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
                            v-if="currentUserIsTaskAuthor()"
                            ripple
                        >
                            <v-list-item-title
                                class="task-context-menu-option"
                                @click="startTaskEditing"
                            >
                                Редактировать задание
                            </v-list-item-title>
                        </v-list-item>
                        <v-list-item ripple>
                            <v-list-item-title
                                class="task-context-menu-option"
                                @click="deleteTask"
                            >
                                Удалить задание
                            </v-list-item-title>
                        </v-list-item>
                    </v-list>
                </v-menu>
            </v-list-item>
            <v-col cols="12"
                   class="pt-0 mb-2"
            >
				<span class="task-cost">
					максимальное количество баллов - {{ task.cost }}
				</span>
            </v-col>
            <v-row
                class="ml-3"
                v-html="task.textContent"
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
            <h3
                class="text-center"
                :class="isDeadLineClose() ? 'red--text' : 'black--text'"
            >
                Задание необходимо выполнить до
                <wbr>
                {{ prettifyDeadLine() }}
            </h3>
            <div>
                <v-divider></v-divider>
                <v-container
                    @click="showSubmissions = true"
                    class="submissions-invoker d-flex"
                    v-ripple
                    fluid
                >
					<span>
						{{ currentUserIsTaskModerator ? "Доступные решения" : "Мои решения" }}
					</span>
                    <v-spacer></v-spacer>
                    <v-icon color="primary">
                        mdi-chevron-right
                    </v-icon>
                </v-container>
            </div>
            <div>
                <v-divider></v-divider>
                <v-container
                    @click="showComments = true"
                    class="comments-invoker d-flex"
                    v-ripple
                    fluid
                >
                    <span>{{ prettifyCommentsCount() }}</span>
                    <v-spacer></v-spacer>
                    <v-icon color="primary">
                        mdi-chevron-right
                    </v-icon>
                </v-container>
            </div>
        </v-container>
    </div>
</template>

<script>
import EditInformationPostModal from "@/components/AccountComponents/EditInformationPostModal";
import EditClubTaskModal from "@/components/AccountComponents/EditClubTaskModal";
import CommentsModal from "@/components/AccountComponents/CommentsModal";
import SubmissionsModal from "@/components/AccountComponents/SubmissionsModal";

export default {
    name: "ClubTaskPresenter",
    components: {
        SubmissionsModal, CommentsModal, EditClubTaskModal, EditInformationPostModal
    },
    props: ['task'],
    data() {
        return {
            currentUserIsTaskModerator: false,
            showEditingForm: false,
            showComments: false,
            showSubmissions: false,
            images: [],
            otherFiles: [],
            criticalHoursToDeadLine: 24
        }
    },
    methods: {
        prettifyPublishDate() {
            let creationDate = new Date(this.task.created).toLocaleString('ru-RU', {
                year: 'numeric',
                month: 'long',
                day: 'numeric',
                hour: 'numeric',
                minute: 'numeric'
            });
            if (!this.task.updated)
                return creationDate;
            let updateDate = new Date(this.task.updated).toLocaleString('ru-RU', {
                year: 'numeric',
                month: 'long',
                day: 'numeric',
                hour: 'numeric',
                minute: 'numeric'
            });
            return `${creationDate} <wbr>(обновлено ${updateDate})`;
        },
        prettifyCommentsCount() {
            if (this.task.commentsCount === 0)
                return "Оставить комментарий...";
            let count = this.task.commentsCount;
            let lastNumber = count % 100;
            let variants = ['комментарий', 'комментария', 'комментариев'];
            if (lastNumber > 10 && lastNumber < 20)
                return `${count} ${variants[2]}`;
            let lastDigit = lastNumber % 10;
            switch (lastDigit) {
                case 1:
                    return `${count} ${variants[0]}`;
                case 2:
                case 3:
                case 4:
                    return `${count} ${variants[1]}`;
                default:
                    return `${count} ${variants[2]}`;
            }
        },
        prettifyDeadLine() {
            return new Date(new Date(this.task.deadLine).getTime() - new Date().getTimezoneOffset() * 60000)
                .toLocaleString('ru-RU', {
                    year: 'numeric',
                    month: 'long',
                    day: 'numeric',
                    hour: 'numeric',
                    minute: 'numeric'
                });
        },
        processAttachments() {
            let attachments = JSON.parse(this.task.attachments);
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
        currentUserIsTaskAuthor() {
            return this.task.authorId == this.$oidc.currentUserId;
        },
        currentUserCanDeleteTheTask() {
            return this.task.authorId == this.$oidc.currentUserId ||
                this.task.owningStudyClub.ownerId == this.$oidc.currentUserId;
        },
        startTaskEditing() {
            this.isDeadLineClose();
            this.$refs.editingForm.loadTask();
            this.showEditingForm = true;
        },
        deleteTask() {
            this.$confirm('Вы действительно хотите удалить задание? При удалении все вложения будут ' +
                'удалены навсегда.')
                .then((result) => {
                    if (result) {
                        this.$loading(true);
                        this.$store.dispatch('deleteClubTask', this.task.id)
                            .then(() => {
                                this.$notify({
                                    group: 'admin-actions',
                                    title: 'Успешная операция',
                                    text: 'Задание успешно удалено',
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
        reloadTasks() {
            this.$emit('load');
        },
        isDeadLineClose() {
            let now = new Date();
            let deadLine = new Date(new Date(this.task.deadLine).getTime() - new Date().getTimezoneOffset() * 60000);
            let difference = (deadLine - now) / 36e5;
            return difference < this.criticalHoursToDeadLine;
        }
    },
    mounted() {
        this.currentUserIsTaskModerator = this.task
            .owningStudyClub
            .moderators.find(user => user.id == this.$oidc.currentUserId) !== undefined;
        this.processAttachments();
    }
}
</script>

<style scoped lang="scss">
.author-avatar {
    margin: 0 auto;
    border-radius: 50%;
    border: double 3px transparent;
    background-image: linear-gradient(white, white),
    radial-gradient(circle at bottom left, red 20%, blue, black);
    background-origin: border-box;
    background-clip: padding-box, border-box;
}

.task-container {
    background: white;
    border-radius: 10px;
}

.author-name {
    font-weight: bolder;
    font-size: 16px;
    color: #4040c0;
}

.image-gallery {
    display: grid;
    grid-template-columns: repeat(3, 1fr);
    grid-gap: 20px;

    img {
        width: 100%;
        padding: 10px;
        object-fit: contain;
        cursor: pointer;
        position: relative;
        border-radius: 8%;
    }
}

.task-context-menu-option {
    text-align: left;
    cursor: pointer;
}

.modal-invoker {
    cursor: pointer;
}

.comments-invoker {
    border-bottom-right-radius: 10px;
    border-bottom-left-radius: 10px;
    cursor: pointer;
}

.submissions-invoker {
    cursor: pointer;
}

.task-cost {
    font-weight: bolder;
    font-style: italic;
    color: black;
}

.publish-date {
    text-decoration: none;
    color: rgba(0, 0, 0, .6);
}

.publish-date:hover {
    text-decoration: underline;
    color: rgba(0, 0, 0, .6);
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