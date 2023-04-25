<template>
    <div>
        <router-link
            :to="'/id' + user.id"
            class="text-decoration-none"
        >
            <v-img
                v-if="user.isBanned"
                aspect-ratio="1"
                min-width="150"
                min-height="150"
                max-width="150"
                max-height="150"
                class="user-image"
                src="../img/banned.jpg"
            />
            <v-img
                v-else
                aspect-ratio="1"
                min-width="150"
                min-height="150"
                max-width="150"
                max-height="150"
                class="user-image"
                :src="user.avatarPath ? user.avatarPath : '../img/blank-item.png'"
            >
            </v-img>
            <h2 class="member-name">
                {{ getUserFullName(user) }}
            </h2>
        </router-link>
        <v-menu
            v-if="contextMenuAuthorizationChecker() && !userIsMe(user)"
            bottom
            right
            offset-x
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
                    v-for="(item, i) in contextActions"
                    :key="i"
                    v-if="!item.requireFullAccess || userHasFullAccess"
                >
                    <v-list-item-title
                        class="context-option"
                        @click="item.method(user.id)"
                    >
                        {{ item.title }}
                    </v-list-item-title>
                </v-list-item>
            </v-list>
        </v-menu>
    </div>
</template>

<script>
export default {
    name: "UserInClubLookupPresenter",
    props: ['user', 'contextMenuAuthorizationChecker', 'userHasFullAccess', 'contextActions'],
    methods: {
        getUserFullName(user) {
            return user.firstName + " " + user.lastName;
        },
        userIsMe(user) {
            return user.id == this.$oidc.currentUserId;
        }
    }
}
</script>

<style scoped>
.member-name {
    text-decoration: none !important;
    color: black;
    font-size: 16px;
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

.context-option {
    cursor: pointer;
}
</style>