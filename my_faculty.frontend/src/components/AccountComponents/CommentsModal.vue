<template>
    <v-row justify="center">
        <v-dialog
            v-model="displayModal"
            fullscreen
            persistent
            transition="dialog-top-transition"
        >
            <div class="comments-wrapper">
                <v-toolbar
                    class="comments-header"
                    color="primary"
                >
                    <v-btn
                        icon
                        dark
                        @click="displayModal = false"
                    >
                        <v-icon>mdi-arrow-left</v-icon>
                    </v-btn>
                    <v-toolbar-title class="white--text">К записи</v-toolbar-title>
                </v-toolbar>
                <div class="comments-list">
                    <CommentPresenter
                        v-for="comment in comments"
                        :key="comment.id + comment.updated"
                        :comment="comment"
                        :all-comments="comments"
                        @load="loadComments"
                        @select="setReplyingComment"
                    />
                </div>
                <div class="comment-form">
                    <CommentForm
                        ref="commentForm"
                        :post-id="postId"
                        @load="loadComments"
                    />
                </div>
            </div>
        </v-dialog>
    </v-row>
</template>

<script>
import CommentForm from "@/components/AccountComponents/CommentForm";
import {mapGetters} from "vuex";
import CommentPresenter from "@/components/presenters/CommentPresenter";

export default {
    name: "CommentsModal",
    components: {CommentPresenter, CommentForm},
    props: ['postId', 'show'],
    data() {
        return {
            comments: []
        }
    },
    methods: {
        close() {
            this.$emit('close');
        },
        loadComments() {
            this.$store.dispatch('loadCommentsByPostId', this.postId)
                .then(() => {
                    this.comments = JSON.parse(JSON.stringify(this.COMMENTS.comments));
                })
        },
        setReplyingComment(comment) {
            this.$refs.commentForm.setReplyingComment(comment);
        }
    },
    mounted() {
        document.documentElement.style.overflowY = "hidden";
        this.loadComments();
        this.$notificationsHub.$on('loadNotifications', () => {
            this.loadComments();
        });
    },
    destroyed() {
        document.documentElement.style.removeProperty("overflow-y");
        this.$emit('load');
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
        },
        ...mapGetters(['COMMENTS'])
    }
}
</script>

<style scoped>
.comments-wrapper {
    background: white;
    height: 100%;
    position: relative;
    overflow: hidden;
}

.comments-header {
    z-index: 10;
}

.comments-list {
    background: #9fdee3;
    position: absolute;
    top: 56px;
    right: 0;
    left: 0;
    bottom: 140px;
    padding: 1rem;
    overflow-y: auto;
}

.comments-list::-webkit-scrollbar {
    width: 10px;
}

.comments-list::-webkit-scrollbar-track {
    background-color: transparent;
    border-radius: 100px;
}

.comments-list::-webkit-scrollbar-thumb {
    background-color: hsla(0, 18%, 3%, 0.2);
    border-radius: 100px;
}

.comment-form {
    position: absolute;
    bottom: 0;
    left: 0;
    right: 0;
    height: 140px;
    overflow: auto;
    background: whitesmoke;
}

.comment-form[got-reply] {
    height: 230px;
}

.comment-form::-webkit-scrollbar {
    width: 10px;
}

.comment-form::-webkit-scrollbar-track {
    background-color: transparent;
    border-radius: 100px;
}

.comment-form::-webkit-scrollbar-thumb {
    background-color: hsla(0, 18%, 3%, 0.2);
    border-radius: 100px;
}
</style>