<template>
    <div>
        <EditInformationPostModal
            :show="showEditingForm"
            :post="watchingPost"
            @close="showEditingForm = false"
            @load="loadPost(watchingPost.id)"
            ref="editingForm"
        />
        <UsersListModal
            v-if="showLikedUsers"
            :show="showLikedUsers"
            :users="watchingPost.likedUsers"
            :context-menu-authorization-checker="() => false"
            :user-has-full-access="false"
            :context-actions="likedUsersActions"
            title="Оценившие пользователи"
            @close="showLikedUsers = false"
        />
        <ErrorPage
            v-if="postNotFound"
            error-code="404"
            message="Запись не найдена"
        />
        <h3
            v-else-if="postOwnerBlockedUser"
            class="black--text mt-5"
        >
            Вы были заблокированы в сообществе, содержащем эту запись. Просмотр контента недоступен.
        </h3>
        <div
            v-else-if="Object.keys(watchingPost).length !== 0 && !watchingPost.owner.isBanned"
        >
            <v-container
                fluid
                class="post-container mb-3 px-0"
                :class="watchingPost.commentsAllowed ? 'pb-0' : ''"
            >
                <div class="internal-post-content">
                    <v-list-item
                        class="mb-3"
                        :key="getFullName(watchingPost.author)"
                    >
                        <v-list-item-avatar>
                            <v-img
                                class="owner-avatar"
                                :src="watchingPost.owner.ownerAvatar ? watchingPost.owner.ownerAvatar : '../img/blank-club.png'"
                            />
                        </v-list-item-avatar>
                        <v-list-item-content class="post-metadata">
                            <v-list-item-title>
                                <router-link class="owner-name" :to="watchingPost.owner.ownerLink">
                                    {{ watchingPost.owner.ownerName }}
                                </router-link>
                            </v-list-item-title>
                            <v-list-item-subtitle>
                                <span>Автор:</span>
                                <router-link class="author-name" :to="'/id' + watchingPost.authorId">
                                    {{ getFullName(watchingPost.author) }}
                                </router-link>
                                <wbr>
                                <TeacherVerificationMark v-if="watchingPost.author.isTeacher"/>
                            </v-list-item-subtitle>
                            <v-list-item-subtitle v-html="prettifyPublishDate()">
                            </v-list-item-subtitle>
                        </v-list-item-content>
                        <v-menu
                            v-if="currentUserIsPostAuthor()"
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
                                        class="post-context-menu-option"
                                        @click="startPostEditing"
                                    >
                                        Редактировать запись
                                    </v-list-item-title>
                                </v-list-item>
                                <v-list-item ripple>
                                    <v-list-item-title
                                        class="post-context-menu-option"
                                        @click="deleteInfoPost"
                                    >
                                        Удалить запись
                                    </v-list-item-title>
                                </v-list-item>
                            </v-list>
                        </v-menu>
                    </v-list-item>
                    <v-row
                        class="ml-3"
                        v-html="watchingPost.textContent"
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
                    <v-row>
                        <v-col class="d-flex align-center">
                            <v-btn
                                ref="like"
                                icon
                                large
                                color="primary"
                                :loading="likeButtonLoading"
                                @mouseover="hoverLikeButton"
                                @mouseout="leaveLikeButton"
                                @click="pressLikeButton"
                            >
                                <v-icon>
                                    {{ displayedLikeButtonIcon }}
                                </v-icon>
                            </v-btn>
                            <span
                                title="Посмотреть список оценивших пользователей"
                                class="modal-invoker"
                                @click="showLikedUsers = true"
                            >
						{{ watchingPost.likedUsers.length !== 0 ? watchingPost.likedUsers.length : '' }}
					</span>
                        </v-col>
                    </v-row>
                </div>
            </v-container>
            <div class="comments-wrapper pt-2">
                <div class="comment-form">
                    <CommentForm
                        ref="commentForm"
                        :post-id="watchingPost.id"
                        @load="loadComments(watchingPost.id)"
                    />
                </div>
                <div class="comments-list">
                    <CommentPresenter
                        v-for="comment in comments"
                        :key="comment.id + comment.updated"
                        :comment="comment"
                        :all-comments="comments"
                        @load="loadComments(watchingPost.id)"
                        @select="setReplyingComment"
                    />
                </div>
            </div>
        </div>
    </div>
</template>

<script>
import TeacherVerificationMark from "@/components/AccountComponents/core/verificationMarks/TeacherVerificationMark";
import EditInformationPostModal from "@/components/AccountComponents/EditInformationPostModal";
import UsersListModal from "@/components/AccountComponents/UsersListModal";
import {mapGetters} from "vuex";
import CommentPresenter from "@/components/presenters/CommentPresenter";
import CommentForm from "@/components/AccountComponents/CommentForm";
import ErrorPage from "@/components/AccountComponents/core/service-pages/ErrorPage";

export default {
    name: "InformationPostView",
    components: {
        ErrorPage,
        CommentForm, CommentPresenter, UsersListModal, EditInformationPostModal, TeacherVerificationMark
    },
    data() {
        return {
            watchingPost: {},
            comments: [],
            images: [],
            otherFiles: [],
            postNotFound: null,
            postOwnerBlockedUser: null,
            showLikedUsers: false,
            showEditingForm: false,
            likedUsersActions: [],
            likeButtonLoading: false,
            likedButtonIcon: 'mdi-thumb-up',
            unlikedButtonIcon: 'mdi-thumb-up-outline',
            displayedLikeButtonIcon: ''
        }
    },
    methods: {
        loadPostData(id) {
            this.$store.dispatch('loadInfoPostById', id)
                .then((response) => {
                    this.watchingPost = response.data;
                    this.loadComments(id);
                    this.initializeLikeButton();
                    this.processAttachments();
                    document.title = "Просмотр записи";
                })
                .catch((error) => {
                    switch (error.response.status) {
                        case 404:
                            document.title = "Запись не найдена";
                            this.postNotFound = true;
                            break;
                        case 409:
                            document.title = "Ошибка доступа";
                            this.postOwnerBlockedUser = true;
                            break;
                    }
                })
                .finally(() => {
                    this.$loading(false);
                });
        },
        loadPost(id) {
            this.$loading(true);
            this.loadPostData(id);
        },
        loadComments(postId) {
            this.$store.dispatch('loadCommentsByPostId', postId)
                .then(() => {
                    this.comments = JSON.parse(JSON.stringify(this.COMMENTS.comments));
                })
        },
        jumpToReply() {
            try {
                let jumpToId = this.$route.hash;
                if (jumpToId)
                    document.getElementById(jumpToId.substring(1)).scrollIntoView();
            } catch (e) {
            }
        },
        getFullName(user) {
            return user.firstName + " " + user.lastName;
        },
        prettifyPublishDate() {
            let creationDate = new Date(this.watchingPost.created).toLocaleString('ru-RU', {
                year: 'numeric',
                month: 'long',
                day: 'numeric',
                hour: 'numeric',
                minute: 'numeric'
            });
            if (!this.watchingPost.updated)
                return creationDate;
            let updateDate = new Date(this.watchingPost.updated).toLocaleString('ru-RU', {
                year: 'numeric',
                month: 'long',
                day: 'numeric',
                hour: 'numeric',
                minute: 'numeric'
            });
            return `${creationDate} <wbr>(обновлено ${updateDate})`;
        },
        currentUserLikesPost() {
            return this.watchingPost.likedUsers.find(user => user.id == this.$oidc.currentUserId) !== undefined;
        },
        processAttachments() {
            let attachments = JSON.parse(this.watchingPost.attachments);
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
        initializeLikeButton() {
            this.displayedLikeButtonIcon = this.currentUserLikesPost() ? this.likedButtonIcon : this.unlikedButtonIcon;
        },
        hoverLikeButton() {
            if (this.currentUserLikesPost())
                return;
            this.displayedLikeButtonIcon = this.likedButtonIcon;
        },
        leaveLikeButton() {
            if (this.currentUserLikesPost())
                return;
            this.displayedLikeButtonIcon = this.unlikedButtonIcon;
        },
        pressLikeButton() {
            if (!this.currentUserLikesPost())
                this.likePost();
            else
                this.unlikePost();
        },
        likePost() {
            this.likeButtonLoading = true;
            this.$store.dispatch('likeInfoPost', {
                likedUserId: this.$oidc.currentUserId,
                likedPostId: this.watchingPost.id
            })
                .then(() => {
                    this.loadPostData(this.watchingPost.id);
                    this.likeButtonLoading = false;
                })
                .catch((error) => {
                    this.$notify({
                        group: 'admin-actions',
                        title: 'Ошибка',
                        text: error.response.data.error,
                        type: 'error'
                    });
                })
        },
        unlikePost() {
            this.likeButtonLoading = true;
            this.$store.dispatch('unlikeInfoPost', {
                likedUserId: this.$oidc.currentUserId,
                likedPostId: this.watchingPost.id
            })
                .then(() => {
                    this.loadPostData(this.watchingPost.id);
                    this.likeButtonLoading = false;
                })
                .catch((error) => {
                    this.$notify({
                        group: 'admin-actions',
                        title: 'Ошибка',
                        text: error.response.data.error,
                        type: 'error'
                    });
                })
        },
        currentUserIsPostAuthor() {
            return this.watchingPost.authorId == this.$oidc.currentUserId;
        },
        setReplyingComment(comment) {
            this.$refs.commentForm.setReplyingComment(comment);
        },
        startPostEditing() {
            this.$refs.editingForm.loadPost();
            this.showEditingForm = true;
        },
        deleteInfoPost() {
            this.$confirm('Вы действительно хотите удалить запись? При удалении все вложения будут ' +
                'удалены навсегда.')
                .then((result) => {
                    if (result) {
                        this.$loading(true);
                        this.$store.dispatch('deleteInfoPost', this.post.id)
                            .then(() => {
                                this.$notify({
                                    group: 'admin-actions',
                                    title: 'Успешная операция',
                                    text: 'Запись успешно удалена',
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
    },
    mounted() {
        let id = this.$route.params.id;
        this.loadPost(id);
        this.$notificationsHub.$on('loadNotifications', () => {
            this.loadComments(id);
        });
    },
    updated() {
        this.jumpToReply();
    },
    computed: {
        ...mapGetters(['COMMENTS'])
    }
}
</script>

<style scoped lang="scss">
.owner-avatar {
    margin: 0 auto;
    border-radius: 50%;
    border: double 3px transparent;
    background-image: linear-gradient(white, white),
    radial-gradient(circle at bottom left, red 20%, blue, black);
    background-origin: border-box;
    background-clip: padding-box, border-box;
}

.post-container {
    background: white;
    border-radius: 10px;
}

.internal-post-content {
    padding-right: 12px;
    padding-left: 12px;
}

.owner-name {
    font-weight: bolder;
    font-size: 16px;
    color: #4040c0;
}

.author-name {
    color: rgba(0, 0, 0, .6);
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

.post-context-menu-option {
    text-align: left;
    cursor: pointer;
}

.post-metadata {
    text-align: left;
}

.modal-invoker {
    cursor: pointer;
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

.comments-wrapper {
    overflow: auto;
}
</style>