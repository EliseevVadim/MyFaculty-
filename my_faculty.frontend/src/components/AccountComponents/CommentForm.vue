<template>
    <div>
        <v-container
            fluid
            v-if="replyingComment"
            class="replying-comment-container"
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
                                <router-link class="author-name" :to="'/id' + replyingComment.authorId">
                                    {{ getFullName(replyingComment.author) }}
                                </router-link>
                                <wbr>
                                <TeacherVerificationMark v-if="replyingComment.author.isTeacher"/>
                            </v-list-item-title>
                            <v-list-item-subtitle
                                class="replying-comment-text"
                                v-html="replyingComment.textContent ? replyingComment.textContent
												: mediaContentMark"
                            />
                        </v-list-item-content>
                    </v-list-item>
                </v-col>
                <v-col cols="1" class="align-self-center">
                    <v-btn
                        large
                        icon
                        color="black"
                        @click="cancelReplying"
                    >
                        <v-icon>
                            mdi-close
                        </v-icon>
                    </v-btn>
                </v-col>
            </v-row>
        </v-container>
        <v-form
            lazy-validation
            ref="commentForm"
            class="d-flex flex-column-reverse flex-md-row px-1"
            v-model="formValid"
        >
            <v-col
                class="col-12 col-md-3 col-sm-12 text-left pl-0 pl-sm-3"
            >
                <VueFileAgent
                    v-model="files"
                    resumable="true"
                    deletable="true"
                    max-files="10"
                    help-text="Прикрепить файлы (максимум 10)"
                    error-text="Произошла ошибка загрузки"
                    theme="list"
                    @beforedelete="fileDeleted($event)"
                />
            </v-col>
            <v-col class="col-12 col-md-9 d-flex flex-row py-0 pl-0 pl-sm-3">
                <v-col
                    class="col-11 pl-0"
                >
                    <tiptap-vuetify
                        class="text-left"
                        v-model="comment.textContent"
                        placeholder="Введите текст комментария..."
                        :extensions="extensions"
                        :toolbar-attributes="{ color: 'primary', dark: true }"
                    />
                </v-col>
                <v-col
                    class="col-1 px-0 text-left align-self-end"
                >
                    <v-btn
                        fab
                        dark
                        :small="$vuetify.breakpoint.mobile"
                        :loading="buttonLoading"
                        color="indigo"
                        @click="send"
                    >
                        <v-icon dark>
                            mdi-send
                        </v-icon>
                    </v-btn>
                </v-col>
            </v-col>
        </v-form>
    </div>
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
import TeacherVerificationMark from "@/components/AccountComponents/core/verificationMarks/TeacherVerificationMark";

export default {
    name: "CommentForm",
    props: ['postId'],
    components: {TeacherVerificationMark, TiptapVuetify},
    data() {
        return {
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
                '</span>',
            formValid: false,
            files: [],
            buttonLoading: false,
            comment: {
                postId: null,
                textContent: null,
                authorId: null,
                parentCommentId: '',
                attachments: []
            },
            replyingComment: null,
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
        getFullName(user) {
            return user.firstName + " " + user.lastName;
        },
        send() {
            this.comment.attachments = this.files.map(item => item.file);
            if (this.comment.attachments.length === 0 && !this.comment.textContent) {
                this.$notify({
                    group: 'admin-actions',
                    title: 'Ошибка',
                    text: 'Комментарий должен содержать хотя бы текстовый контент или прикрепленные файлы',
                    type: 'error'
                })
                return;
            }
            this.comment.authorId = this.$oidc.currentUserId;
            this.comment.postId = this.postId;
            this.buttonLoading = true;
            this.$store.dispatch('addComment', this.comment)
                .then(() => {
                    this.$emit('load');
                    this.resetInput();
                    this.cancelReplying();
                    document.getElementsByClassName('comments-list')[0].scrollTo(0, 0)
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
                    this.buttonLoading = false;
                })
        },
        fileDeleted(file) {
            let index = this.files.indexOf(file);
            if (index === -1)
                return;
            this.files.splice(index, 1);
        },
        resetInput() {
            this.comment.postId = null;
            this.comment.textContent = null;
            this.comment.attachments = [];
            this.comment.authorId = null;
            this.comment.parentCommentId = '';
            this.files = [];
        },
        setReplyingComment(comment) {
            document.getElementsByClassName('comment-form')[0].setAttribute('got-reply', '');
            this.replyingComment = comment;
            this.comment.parentCommentId = comment.id;
        },
        cancelReplying() {
            document.getElementsByClassName('comment-form')[0].removeAttribute('got-reply');
            this.replyingComment = null;
            this.comment.parentCommentId = '';
        }
    }
}
</script>

<style scoped>
.replying-comment-container {
    background: #53b6be;
    position: relative;
    bottom: 0;
}

.replying-comment-content {
    text-align: left;
    background: white;
    border-radius: 10px;
}

.replying-comment-text {
    color: rgba(0, 0, 0, .87) !important;
}

.author-name {
    font-weight: bolder;
    font-size: 16px;
    color: #4040c0;
}
</style>