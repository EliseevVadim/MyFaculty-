<template>
    <div>
        <v-divider></v-divider>
        <v-list-item
            :key="item.clubName"
        >
            <v-list-item-avatar>
                <v-img
                    class="public-image"
                    :src="item.imagePath ? item.imagePath : 'img/blank-club.png'"
                />
            </v-list-item-avatar>

            <v-list-item-content class="public-lookup-content">
                <v-list-item-title>
                    <router-link class="public-title" :to="'/public' + item.id">{{ item.publicName }}</router-link>
                </v-list-item-title>
                <v-list-item-subtitle>
                    {{ prettifyMembersCount() }}
                </v-list-item-subtitle>
            </v-list-item-content>
            <v-menu
                v-if="item.currentUserInPublic"
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
            >
                Вступить
            </v-btn>
        </v-list-item>
        <v-divider></v-divider>
    </div>
</template>

<script>
export default {
    name: "InfoPublicLookupPresenter",
    props: ['item'],
    data() {
        return {
            userInPublic: null
        }
    },
    methods: {
        joinInfoPublic() {
            this.$loading(true);
            this.$store.dispatch('joinInfoPublic', {
                publicId: this.item.id,
                userId: this.$oidc.currentUserId
            })
                .then(() => {
                    this.$store.dispatch('loadCurrentUser', this.$oidc.currentUserId)
                        .then(() => {
                            this.$notify({
                                group: 'admin-actions',
                                title: 'Успешная операция',
                                text: 'Вы успешно вступили в сообщество!',
                                type: 'success'
                            });
                            this.item.membersCount++;
                            this.item.currentUserInPublic = true;
                            this.$loading(false);
                        })
                })
                .catch((error) => {
                    this.$notify({
                        group: 'admin-actions',
                        title: 'Ошибка',
                        text: error.response.data.error,
                        type: 'error'
                    });
                    this.$loading(false);
                });
        },
        leaveInfoPublic() {
            this.$loading(true);
            this.$store.dispatch('leaveInfoPublic', {
                publicId: this.item.id,
                userId: this.$oidc.currentUserId
            })
                .then(() => {
                    this.$store.dispatch('loadCurrentUser', this.$oidc.currentUserId)
                        .then(() => {
                            this.$notify({
                                group: 'admin-actions',
                                title: 'Успешная операция',
                                text: 'Вы успешно покинули сообщество!',
                                type: 'success'
                            });
                            this.item.membersCount--;
                            this.item.currentUserInPublic = false;
                            this.$loading(false);
                        })
                })
                .catch(() => {
                    this.$notify({
                        group: 'admin-actions',
                        title: 'Ошибка',
                        text: 'Произошла ошибка, попробуйте заново.',
                        type: 'error'
                    });
                    this.$loading(false);
                });
        },
        prettifyMembersCount() {
            let count = this.item.membersCount;
            let lastNumber = count % 100;
            let variants = ['подписчик', 'подписчика', 'подписчиков'];
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
        }
    }
}
</script>

<style scoped>
.public-lookup-content {
    text-align: left;
}

.public-title {
    text-decoration: none;
    font-weight: bolder;
    font-size: 16px;
    color: #4040c0;
}

.public-menu-option {
    cursor: pointer;
}

.public-image {
    margin: 0 auto;
    border-radius: 50%;
    border: double 3px transparent;
    background-image: linear-gradient(white, white),
    radial-gradient(circle at bottom left, red 20%, blue, black);
    background-origin: border-box;
    background-clip: padding-box, border-box;
}
</style>