<template>
    <div>
        <UsersListModal
            :show="showMembers"
            :users="watchingPublic.members"
            :context-menu-authorization-checker="currentUserIsPublicOwner"
            :user-has-full-access="currentUserIsPublicOwner()"
            :context-actions="membersActions"
            title="Участники сообщества"
            @close="showMembers = false"
        />
        <UsersListModal
            :show="showBanned"
            :users="watchingPublic.blockedUsers"
            :context-menu-authorization-checker="currentUserIsPublicOwner"
            :user-has-full-access="currentUserIsPublicOwner()"
            :context-actions="bannedUsersActions"
            title="Заблокированные пользователи"
            @close="showBanned = false"
        />
        <CreateInformationPostModal
            :show="showPostCreationForm"
            :public-id="watchingPublic.id"
            :study-club-id="''"
            @close="showPostCreationForm = false"
            @load="loadInfoPosts"
        />
        <v-dialog
            v-model="showEditingForm"
            persistent
            max-width="600px"
        >
            <v-card>
                <v-card-title class="d-flex justify-center">
                    <span class="text-h5">Редактировать информацию о сообществе</span>
                </v-card-title>
                <v-card-text>
                    <h2 class="text-center red--text">{{ errorText }}</h2>
                    <v-container>
                        <v-form
                            lazy-validation
                            ref="submitForm"
                            v-model="formValid">
                            <v-col cols="12">
                                <v-text-field
                                    label="Название информационного сообщества*"
                                    required
                                    :rules="commonRules"
                                    hide-details="auto"
                                    v-model="updatingPublic.publicName"
                                ></v-text-field>
                            </v-col>
                            <v-col cols="12">
                                <v-textarea
                                    label="Описание"
                                    hide-details="auto"
                                    v-model="updatingPublic.description"
                                ></v-textarea>
                            </v-col>
                            <v-col cols="12">
                                <v-file-input
                                    :rules="photoRules"
                                    show-size
                                    accept="image/png, image/jpeg, image/bmp"
                                    placeholder="Выберите фото"
                                    prepend-icon="mdi-camera"
                                    hide-details="auto"
                                    label="Фото"
                                    @change="changePreview"
                                    v-model="updatingPublic.image"
                                ></v-file-input>
                                <div class="mt-5" v-if="previewImage !== null">
                                    <h4 class="black--text">Предпросмотр изображения сообщества</h4>
                                    <v-row class="mt-3">
                                        <v-col class="d-flex d-sm-block col-sm-6 col-12 justify-center">
                                            <v-img
                                                contain
                                                max-width="200"
                                                :src="previewImage"
                                            >
                                            </v-img>
                                        </v-col>
                                        <v-col class="d-flex d-sm-block col-sm-6 col-12 justify-center">
                                            <v-avatar
                                                class="align-self-center"
                                                contain
                                            >
                                                <v-img
                                                    :src="previewImage"
                                                    class="updating-image-preview"
                                                >
                                                </v-img>
                                            </v-avatar>
                                        </v-col>
                                    </v-row>
                                </div>
                            </v-col>
                            <v-col
                                cols="12"
                            >
                                <v-autocomplete
                                    :items="this.watchingPublic.members"
                                    :item-text="getUserFullName"
                                    item-value="id"
                                    label="Выберите нового владельца"
                                    :rules="commonRules"
                                    v-model="updatingPublic.ownerId"
                                >
                                </v-autocomplete>
                                <b class="red--text">
                                    ОСТОРОЖНО! При изменении последнего поля
                                    Вы потеряете управляющий доступ к сообществу.
                                </b>
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
                        @click="showEditingForm = false"
                    >
                        Закрыть
                    </v-btn>
                    <v-btn
                        color="blue darken-1"
                        text
                        @click="editPublic"
                        :disabled="!formValid"
                    >
                        Сохранить
                    </v-btn>
                </v-card-actions>
            </v-card>
        </v-dialog>
        <ErrorPage v-if="publicNotFound" error-code="404" message="Информационное сообщество не найдено"/>
        <v-container
            fluid
            v-if="Object.keys(watchingPublic).length !== 0 && !watchingPublic.isBanned"
        >
            <v-row class="d-flex justify-start ma-1 mt-3">
                <h1
                    class="public-name"
                >
                    {{ watchingPublic.publicName }}
                </h1>
                <v-btn
                    v-if="currentUserIsPublicOwner()"
                    class="mx-2"
                    fab
                    small
                    color="white"
                    @click="openEditForm"
                >
                    <v-icon>
                        mdi-pencil
                    </v-icon>
                </v-btn>
                <v-spacer class="d-block"></v-spacer>
                <v-menu
                    v-if="currentUserIsMember()"
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
                                class="public-menu-option"
                                @click="leaveInfoPublic"
                            >
                                Покинуть сообщество
                            </v-list-item-title>
                        </v-list-item>
                    </v-list>
                </v-menu>
                <v-btn
                    v-else
                    color="primary"
                    @click="joinInfoPublic"
                    :disabled="currentUserIsBlocked()"
                >
                    Вступить
                </v-btn>
            </v-row>
            <v-divider></v-divider>
            <h3
                v-if="currentUserIsBlocked()"
                class="black--text mt-5"
            >
                Вы были заблокированы в этом сообществе. Просмотр контента недоступен.
            </h3>
            <v-row
                v-else
                class="d-flex mt-2 mb-2 flex-column-reverse flex-sm-row"
            >
                <v-col
                    class="text-left col-12 col-sm-6"
                >
                    <v-col class="mt-0">
                        <b>Информация:</b>
                        <p class="mt-2 public-description">
                            {{ watchingPublic.description }}
                        </p>
                    </v-col>
                    <v-col class="mt-0 modal-invoker" @click="showMembers = true">
                        <b>Участников: </b>
                        <span>{{ watchingPublic.membersCount }}</span>
                    </v-col>
                    <v-col
                        v-if="currentUserIsPublicOwner()"
                        class="mt-0 modal-invoker"
                        @click="showBanned = true"
                    >
                        <b>Заблокированных пользователей: </b>
                        <span>{{ watchingPublic.blockedUsers.length }}</span>
                    </v-col>
                    <v-col class="mt-0">
                        <b>Владелец: </b>
                        <router-link
                            :to="'/id' + watchingPublic.ownerId"
                            class="user-link"
                        >
                            <v-list-item class="pl-0">
                                <v-list-item-avatar
                                    class="align-self-center"
                                    color="white"
                                    contain
                                >
                                    <v-img
                                        v-if="watchingPublic.owner.isBanned"
                                        class="user-image"
                                        src="../img/banned.jpg"
                                    />
                                    <v-img
                                        v-else
                                        class="user-image"
                                        :src="watchingPublic.owner.avatarPath ? watchingPublic.owner.avatarPath : '../img/blank-item.png'"
                                    />
                                </v-list-item-avatar>
                                <v-list-item-content>
                                    <v-list-item-title
                                        class="profile-name"
                                    >
                                        {{ getOwnersFullName() }}
                                        <TeacherVerificationMark v-if="watchingPublic.owner.isTeacher"/>
                                    </v-list-item-title>
                                </v-list-item-content>
                            </v-list-item>
                        </router-link>
                    </v-col>
                </v-col>
                <v-col
                    class="d-flex col-12 col-sm-6 justify-center justify-sm-end"
                >
                    <v-img
                        aspect-ratio="1"
                        max-width="150"
                        max-height="150"
                        :src="watchingPublic.imagePath ? watchingPublic.imagePath : '../img/blank-club.png'"
                        class="public-photo"
                    >
                    </v-img>
                </v-col>
            </v-row>
            <v-divider v-if="!currentUserIsBlocked()"></v-divider>
            <v-row class="mt-1">
                <v-card-actions>
                    <v-btn
                        v-if="currentUserIsPublicOwner()"
                        color="primary"
                        @click="showPostCreationForm = true"
                    >
                        Новая запись
                        <v-icon right>
                            mdi-pencil
                        </v-icon>
                    </v-btn>
                </v-card-actions>
            </v-row>
            <v-container class="mt-4 text-left mx-0">
                <InformationPostPresenter
                    v-for="infoPost in informationPosts"
                    :key="infoPost.id + infoPost.updated + infoPost.likedUsers.length + infoPost.commentsCount"
                    :post="infoPost"
                    @load="loadInfoPosts"
                />
            </v-container>
        </v-container>
        <v-container
            fluid
            v-else-if="watchingPublic.isBanned"
        >
            <v-row class="d-flex justify-start ma-1 mt-3">
                <h1
                    class="public-name"
                >
                    {{ watchingPublic.publicName }}
                </h1>
                <v-spacer class="d-block"></v-spacer>
                <v-menu
                    v-if="currentUserIsMember()"
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
                                class="public-menu-option"
                                @click="leaveInfoPublic"
                            >
                                Покинуть сообщество
                            </v-list-item-title>
                        </v-list-item>
                    </v-list>
                </v-menu>
            </v-row>
            <v-divider></v-divider>
            <h3
                v-if="currentUserIsBlocked()"
                class="black--text mt-5"
            >
                Вы были заблокированы в этом сообществе. Просмотр контента недоступен.
            </h3>
            <v-row
                v-else
                class="d-flex mt-2 mb-2 flex-column-reverse flex-sm-row"
            >
                <v-col
                    class="d-flex flex-column justify-center col-sm-9 col-12"
                >
                    <h2
                        class="mt-2 black--text"
                    >
                        Сообщество было заблокировано за нарушение правил сайта.
                    </h2>
                </v-col>
                <v-col
                    class="d-flex col-12 col-sm-3 justify-center justify-sm-end"
                >
                    <v-img
                        aspect-ratio="1"
                        max-width="150"
                        max-height="150"
                        src="../img/banned.jpg"
                        class="public-photo"
                    >
                    </v-img>
                </v-col>
            </v-row>
        </v-container>
    </div>
</template>

<script>
import MassStudyClubDeletingForm from "@/components/AccountComponents/teacherComponents/MassStudyClubDeletingForm";
import MassStudyClubAddingForm from "@/components/AccountComponents/teacherComponents/MassStudyClubAddingForm";
import UsersListModal from "@/components/AccountComponents/UsersListModal";
import UserInClubLookupPresenter from "@/components/presenters/UserInClubLookupPresenter";
import TeacherVerificationMark from "@/components/AccountComponents/core/verificationMarks/TeacherVerificationMark";
import ErrorPage from "@/components/AccountComponents/core/service-pages/ErrorPage";
import CreateInformationPostModal from "@/components/AccountComponents/CreateInformationPostModal";
import {mapGetters} from "vuex";
import InformationPostPresenter from "@/components/presenters/InformationPostPresenter";

export default {
    name: "InformationPublicView",
    components: {
        InformationPostPresenter,
        CreateInformationPostModal,
        MassStudyClubDeletingForm,
        MassStudyClubAddingForm, UsersListModal, UserInClubLookupPresenter, TeacherVerificationMark, ErrorPage
    },
    data() {
        return {
            watchingPublic: {},
            updatingPublic: {
                publicName: "",
                publicDescription: "",
                publicImage: null,
                ownerId: null
            },
            errorText: "",
            photoRules: [
                value => !value || value.size < 2000000 || 'Размер фото превышает 2 МБ!'
            ],
            commonRules: [
                v => !!v || 'Поле является обязательным для заполнения'
            ],
            previewImage: null,
            formValid: true,
            publicNotFound: false,
            showMembers: false,
            showBanned: false,
            showEditingForm: false,
            showPostCreationForm: false,
            membersActions: [
                {
                    title: "Удалить из сообщества и заблокировать",
                    method: this.removeAndBlockMember,
                    requireFullAccess: false
                }
            ],
            bannedUsersActions: [
                {
                    title: "Разблокировать пользователя",
                    method: this.unblockUser,
                    requireFullAccess: false
                }
            ],
            informationPosts: []
        }
    },
    methods: {
        getOwnersFullName() {
            return this.watchingPublic.owner.firstName + " " + this.watchingPublic.owner.lastName;
        },
        currentUserIsPublicOwner() {
            return this.watchingPublic.ownerId == this.$oidc.currentUserId;
        },
        currentUserIsMember() {
            return this.watchingPublic.members.find(user => user.id == this.$oidc.currentUserId) !== undefined;
        },
        currentUserIsBlocked() {
            return this.watchingPublic.blockedUsers.find(user => user.id == this.$oidc.currentUserId) !== undefined;
        },
        getUserFullName(user) {
            return user.firstName + " " + user.lastName;
        },
        removeAndBlockMember(userId) {
            this.$loading(true);
            this.$store.dispatch('blockUserAtInfoPublic', {
                publicId: this.watchingPublic.id,
                issuerId: this.$oidc.currentUserId,
                userId: userId
            })
                .then(() => {
                    this.reloadInfoPublicWithMessage('Пользователь успешно исключен из сообщества!');
                })
                .catch((error) => {
                    this.$notify({
                        group: 'admin-actions',
                        title: 'Ошибка',
                        text: error.response.data.error,
                        type: 'error'
                    });
                    this.$loading(false);
                })
        },
        unblockUser(userId) {
            this.$loading(true);
            this.$store.dispatch('unblockUserAtInfoPublic', {
                publicId: this.watchingPublic.id,
                issuerId: this.$oidc.currentUserId,
                userId: userId
            })
                .then(() => {
                    this.reloadInfoPublicWithMessage('Пользователь успешно разблокирован!');
                })
                .catch((error) => {
                    this.$notify({
                        group: 'admin-actions',
                        title: 'Ошибка',
                        text: error.response.data.error,
                        type: 'error'
                    });
                    this.$loading(false);
                })
        },
        joinInfoPublic() {
            this.$loading(true);
            this.$store.dispatch('joinInfoPublic', {
                publicId: this.watchingPublic.id,
                userId: this.$oidc.currentUserId
            })
                .then(() => {
                    this.reloadInfoPublicWithMessage('Вы успешно вступили в сообщество!');
                })
                .catch((error) => {
                    this.$notify({
                        group: 'admin-actions',
                        title: 'Ошибка',
                        text: error.response.data.error,
                        type: 'error'
                    });
                    this.$loading(false);
                })
        },
        leaveInfoPublic() {
            this.$loading(true);
            this.$store.dispatch('leaveInfoPublic', {
                publicId: this.watchingPublic.id,
                userId: this.$oidc.currentUserId
            })
                .then(() => {
                    this.reloadInfoPublicWithMessage('Вы успешно покинули сообщество!');
                })
                .catch((error) => {
                    this.$notify({
                        group: 'admin-actions',
                        title: 'Ошибка',
                        text: error.response.data.error,
                        type: 'error'
                    });
                    this.$loading(false);
                })
        },
        reloadInfoPublicWithMessage(message) {
            this.$store.dispatch('loadInfoPublicById', this.watchingPublic.id)
                .then((response) => {
                    this.watchingPublic = response.data;
                    this.$notify({
                        group: 'admin-actions',
                        title: 'Успешная операция',
                        text: message,
                        type: 'success'
                    });
                    this.$loading(false);
                })
        },
        changePreview(payload) {
            if (payload) {
                this.previewImage = URL.createObjectURL(payload);
                URL.revokeObjectURL(payload);
                return;
            }
            this.previewImage = null;
        },
        openEditForm() {
            this.updatingPublic = JSON.parse(JSON.stringify(this.watchingPublic));
            this.showEditingForm = true;
        },
        editPublic() {
            this.formValid = this.$refs.submitForm.validate();
            if (!this.formValid)
                return;
            this.errorText = "";
            this.$loading(true);
            this.updatingPublic.issuerId = this.$oidc.currentUserId;
            this.$store.dispatch('updateInfoPublic', this.updatingPublic)
                .then(() => {
                    this.$store.dispatch('loadInfoPublicById', this.watchingPublic.id)
                        .then((response) => {
                            this.watchingPublic = response.data;
                            this.showEditingForm = false;
                            this.$loading(false);
                            this.resetUpdatingPublic();
                        })
                })
                .catch(() => {
                    this.errorText = "Произошла ошибка обновления информации о сообществе. Перепроверьте ввод.";
                    this.$loading(false);
                });
        },
        resetUpdatingPublic() {
            this.updatingPublic.publicImage = null;
            this.updatingPublic.publicName = "";
            this.updatingPublic.publicDescription = "";
            this.updatingPublic.ownerId = null;
            this.updatingPublic.image = null;
            this.previewImage = null;
            this.updatingPublic.issuerId = null;
        },
        loadInfoPosts() {
            this.$store.dispatch('loadInfoPostsByInfoPublicId', this.watchingPublic.id)
                .then(() => {
                    this.informationPosts = JSON.parse(JSON.stringify(this.INFO_POSTS.infoPosts));
                });
        }
    },
    mounted() {
        let id = this.$route.params.id;
        this.$loading(true);
        this.$store.dispatch('loadInfoPublicById', id)
            .then((response) => {
                this.watchingPublic = response.data;
                document.title = this.watchingPublic.publicName;
                this.loadInfoPosts();
            })
            .catch((error) => {
                if (error.response.status === 404) {
                    document.title = "Сообщество не найдено";
                    this.publicNotFound = true;
                }
            })
            .finally(() => {
                this.$loading(false);
            })
    },
    computed: {
        ...mapGetters(['INFO_POSTS'])
    }
}
</script>

<style scoped>
.public-name {
    text-align: left;
    color: black;
    font-size: 24px;
}

.public-photo {
    border: double 8px transparent;
    border-radius: 50%;
    background-image: linear-gradient(white, white),
    radial-gradient(circle at bottom left, red 20%, blue, black);
    background-origin: border-box;
    background-clip: padding-box, border-box;
}

p.public-description {
    text-indent: 25px;
    font-size: 18px;
    margin-bottom: 0;
    color: #1e1e1e;
}

b {
    color: black;
}

.user-image {
    margin: 0 auto;
    border-radius: 50%;
    border: double 3px transparent;
    background-image: linear-gradient(white, white),
    radial-gradient(circle at bottom left, red 20%, blue, black);
    background-origin: border-box;
    background-clip: padding-box, border-box;
}

.user-link {
    text-decoration: none;
    font-weight: bolder;
    color: black;
}

.public-menu-option {
    cursor: pointer;
}

.modal-invoker {
    cursor: pointer;
}

.updating-image-preview {
    border-style: solid;
}
</style>