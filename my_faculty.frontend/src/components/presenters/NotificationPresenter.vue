<template>
    <router-link
        :to="this.notification.metaInfo ? {path: notification.returnUrl, hash: JSON.parse(this.notification.metaInfo).JumpToId}
			: notification.returnUrl"
        class="notification"
    >
        <app-bar-item>
            <v-list-item-title
                v-text="notification.textContent"
            />
        </app-bar-item>
    </router-link>
</template>

<script>
import {VHover, VListItem} from "vuetify/lib/components";

export default {
    name: "NotificationPresenter",
    components: {
        AppBarItem: {
            render(h) {
                return h(VHover, {
                    scopedSlots: {
                        default: ({hover}) => {
                            return h(VListItem, {
                                attrs: this.$attrs,
                                class: {
                                    'text-left': true,
                                    'black--text': !hover,
                                    'white--text secondary elevation-12': hover,
                                },
                                props: {
                                    activeClass: '',
                                    dark: hover,
                                    link: true,
                                    ...this.$attrs,
                                },
                            }, this.$slots.default)
                        },
                    },
                })
            },
        }
    },
    props: ['notification']
}
</script>

<style scoped>
.notification {
    text-decoration: none;
}
</style>