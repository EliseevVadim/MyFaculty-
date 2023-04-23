<template>
    <v-row justify="center">
        <v-dialog
            v-model="displayModal"
            scrollable
            max-width="850px"
            max-height="500px"
        >
            <v-card>
                <v-card-title>
					<span>
						{{ title }}
					</span>
                    <v-spacer></v-spacer>
                    <v-btn
                        icon
                        @click="close"
                    >
                        <v-icon>
                            mdi-close
                        </v-icon>
                    </v-btn>
                </v-card-title>
                <v-divider></v-divider>
                <v-card-text>
                    <v-container>
                        <v-row>
                            <v-col
                                class="text-center col-xs-12 col-sm-4 col-md-3" v-for="user in users"
                            >
                                <UserInClubLookupPresenter
                                    :user="user"
                                    :context-menu-authorization-checker="contextMenuAuthorizationChecker"
                                    :user-has-full-access="userHasFullAccess"
                                    :context-actions="contextActions"
                                />
                            </v-col>
                        </v-row>
                    </v-container>
                </v-card-text>
            </v-card>
        </v-dialog>
    </v-row>
</template>

<script>
import UserInClubLookupPresenter from "@/components/presenters/UserInClubLookupPresenter";

export default {
    name: "UsersListModal",
    components: {UserInClubLookupPresenter},
    props: ['users', 'show', 'title', 'contextMenuAuthorizationChecker', 'userHasFullAccess', 'contextActions'],
    methods: {
        close() {
            this.$emit('close');
        }
    },
    computed: {
        displayModal: {
            get() {
                return this.show
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