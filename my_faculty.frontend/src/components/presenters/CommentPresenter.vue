<template>
    <div>
        <EditCommentModal
            :show="showEditingForm"
            :comment="comment"
            @close="showEditingForm = false"
            @load="reloadComments"
            ref="editingForm"
        />
        <div class="d-flex" :id="'comment_' + comment.id">
            <v-list-item-avatar class="align-self-end">
                <v-img
                    class="author-avatar"
                    :src="comment.author.avatarPath ? comment.author.avatarPath : '../img/blank-club.png'"
                />
            </v-list-item-avatar>
            <v-container
                fluid
                class="comment-container mb-3 px-0"
                :class="comment.parentComment ? 'pt-0' : ''"
            >
                <v-container
                    fluid
                    v-if="comment.parentComment"
                    class="replying-comment-container"
                    @click="jumpToComment(comment.parentCommentId)"
                >
                    <v-row class="d-flex justify-start">
                        <v-col cols="1" class="align-self-center">
                            <v-icon large>
                                mdi-reply
                            </v-icon>
                        </v-col>
                        <v-col cols="11" sm="10" class="align-self-center">
                            <v-list-item class="replying-comment-content">
                                <v-list-item-content class="pb-0">
                                    <v-list-item-title>
                                        <router-link class="author-name" :to="'/id' + comment.parentComment.authorId">
                                            {{ getFullName(comment.parentComment.author) }}
                                        </router-link>
                                        <wbr>
                                        <TeacherVerificationMark v-if="comment.parentComment.author.isTeacher"/>
                                    </v-list-item-title>
                                    <v-list-item-subtitle
                                        class="replying-comment-text"
                                        v-html="comment.parentComment.textContent ? comment.parentComment.textContent
												: mediaContentMark"
                                    />
                                </v-list-item-content>
                            </v-list-item>
                        </v-col>
                    </v-row>
                </v-container>
                <div class="comment-content">
                    <v-list-item
                        class="mb-3"
                        :key="getFullName(comment.author)"
                    >
                        <v-list-item-content class="comment-metadata">
                            <v-list-item-title>
                                <router-link class="author-name" :to="'/id' + comment.authorId">
                                    {{ getFullName(comment.author) }}
                                </router-link>
                                <wbr>
                                <TeacherVerificationMark v-if="comment.author.isTeacher"/>
                            </v-list-item-title>
                            <v-list-item-subtitle v-html="prettifyPublishDate()">
                            </v-list-item-subtitle>
                        </v-list-item-content>
                        <v-menu
                            v-if="currentUserIsCommentAuthor()"
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
                                <v-list-item ripple>
                                    <v-list-item-title
                                        class="comment-context-menu-option"
                                        @click="startCommentEditing"
                                    >
                                        Редактировать комментарий
                                    </v-list-item-title>
                                </v-list-item>
                                <v-list-item ripple>
                                    <v-list-item-title
                                        class="comment-context-menu-option"
                                        @click="deleteComment"
                                    >
                                        Удалить комментарий
                                    </v-list-item-title>
                                </v-list-item>
                                <v-list-item ripple>
                                    <v-list-item-title
                                        class="comment-context-menu-option"
                                        @click="selectReplyingComment"
                                    >
                                        Ответить
                                    </v-list-item-title>
                                </v-list-item>
                            </v-list>
                        </v-menu>
                        <p
                            v-else
                            class="reply-option"
                            @click="selectReplyingComment"
                        >
                            Ответить
                        </p>
                    </v-list-item>
                    <v-row
                        class="ml-3 text-left"
                        v-html="comment.textContent"
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
            </v-container>
        </div>
    </div>
</template>

<script>
import TeacherVerificationMark from "@/components/AccountComponents/core/verificationMarks/TeacherVerificationMark";
import EditCommentModal from "@/components/AccountComponents/EditCommentModal";

export default {
    name: "CommentPresenter",
    components: {EditCommentModal, TeacherVerificationMark},
    props: ['comment', 'allComments'],
    data() {
        return {
            showEditingForm: false,
            images: [],
            otherFiles: [],
            mediaContentMark: '<span ' +
                'style="' +
                '	color: lightgray; ' +
                '	text-align: center;' +
                '	border: dashed;' +
                '	display: block;' +
                '	font-size: 16px;' +
                '	margin-bottom: 12px;' +
                '	margin-top: 5px"' +
                '>' +
                'Медиа контент' +
                '</span>'
        }
    },
    methods: {
        getFullName(user) {
            return user.firstName + " " + user.lastName;
        },
        prettifyPublishDate() {
            let creationDate = new Date(this.comment.created).toLocaleString('ru-RU', {
                year: 'numeric',
                month: 'long',
                day: 'numeric',
                hour: 'numeric',
                minute: 'numeric'
            });
            if (!this.comment.updated)
                return creationDate;
            let updateDate = new Date(this.comment.updated).toLocaleString('ru-RU', {
                year: 'numeric',
                month: 'long',
                day: 'numeric',
                hour: 'numeric',
                minute: 'numeric'
            });
            return `${creationDate} <wbr>(обновлено ${updateDate})`;
        },
        currentUserIsCommentAuthor() {
            return this.comment.authorId == this.$oidc.currentUserId;
        },
        processAttachments() {
            let attachments = JSON.parse(this.comment.attachments);
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
        selectReplyingComment() {
            this.$emit('select', this.comment);
        },
        jumpToComment(id) {
            document.getElementById('comment_' + id).scrollIntoView();
        },
        startCommentEditing() {
            this.$refs.editingForm.loadComment(this.allComments);
            this.showEditingForm = true;
        },
        deleteComment() {
            this.$confirm("Вы действительно хотите удалить этот комментарий? Все вложения будут" +
                " потеряны навсегда")
                .then((result) => {
                    if (result) {
                        this.$loading(true);
                        this.$store.dispatch('deleteComment', this.comment.id)
                            .then(() => {
                                this.$notify({
                                    group: 'admin-actions',
                                    title: 'Успешная операция',
                                    text: 'Комментарий успешно удален',
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
        reloadComments() {
            this.$emit('load');
        }
    },
    mounted() {
        this.processAttachments();
    }
}
</script>

<style scoped lang="scss">
.comment-container {
    background: white;
    border-radius: 10px;
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

.author-avatar {
    margin: 0 auto;
    border-radius: 50%;
    border: double 3px transparent;
    background-image: linear-gradient(white, white),
    radial-gradient(circle at bottom left, red 20%, blue, black);
    background-origin: border-box;
    background-clip: padding-box, border-box;
}

.author-name {
    font-weight: bolder;
    font-size: 16px;
    color: #4040c0;
}

.comment-context-menu-option {
    text-align: left;
    cursor: pointer;
}

.comment-metadata {
    text-align: left;
}

.comment-content {
    padding-right: 12px;
    padding-left: 12px;
}

.reply-option {
    color: #a669d9;
    font-weight: bolder;
    cursor: pointer;
    text-decoration: underline;
    text-decoration-color: transparent;
    text-decoration-style: dotted;
    transition: cubic-bezier(1, 0, 0, 1) 1s;
}

.reply-option:hover {
    text-decoration-color: #a669d9;
}

.replying-comment-container {
    background: #53b6be;
    position: relative;
    bottom: 0;
    border-top-left-radius: 10px;
    border-top-right-radius: 10px;
    cursor: pointer;
}

.replying-comment-content {
    text-align: left;
    background: white;
    border-radius: 10px;
}

.replying-comment-text {
    color: rgba(0, 0, 0, .87) !important;
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