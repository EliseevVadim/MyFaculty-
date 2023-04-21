<template>
	<v-menu
		bottom
		left
		offset-y
		origin="top right"
		transition="scale-transition"
	>
		<template
			v-slot:activator="{ attrs, on }"
		>
			<v-btn
				v-if="notifications.length > 0"
				class="ml-2"
				min-width="0"
				text
				v-bind="attrs"
				v-on="on"
			>
				<v-badge
					color="red"
					overlap
					bordered
				>
					<template
						v-slot:badge
					>
						<span>{{notifications.length}}</span>
					</template>
					<v-icon>mdi-bell</v-icon>
				</v-badge>
			</v-btn>
			<v-btn
				v-else
				class="ml-2"
				min-width="0"
				text
				v-bind="attrs"
				v-on="on"
			>
				<v-icon>mdi-bell</v-icon>
			</v-btn>
		</template>
		<v-list
			v-if="notifications.length > 0"
			:tile="false"
			nav
		>
			<div>
				<NotificationPresenter
					v-for="(notification, key) in notifications"
					:key="`notification-${key}`"
					:notification="notification"
				/>
			</div>
			<v-btn
				class="mt-2"
				color="error"
				@click="clearAllNotifications"
			>
				Очистить
			</v-btn>
		</v-list>
	</v-menu>
</template>

<script>
import {mapGetters} from "vuex";
import NotificationPresenter from "@/components/presenters/NotificationPresenter";
export default {
	name: "NotificationsList",
	components: {
		NotificationPresenter
	},
	data() {
		return {
			notifications: []
		}
	},
	methods: {
		loadNotifications() {
			this.$store.dispatch('loadAllNotifications')
				.then(() => {
					this.notifications = this.NOTIFICATIONS.notifications;
				});
		},
		receiveNewNotification() {
			this.loadNotifications();
			this.$notify({
				group: 'admin-actions',
				title: 'Информация',
				text: 'Получено новое уведомление!',
				type: 'success'
			});
		},
		clearAllNotifications() {
			this.$store.dispatch('deleteNotifications')
				.then(() => {
					this.loadNotifications();
				})
		}
	},
	mounted() {
		this.loadNotifications();
		this.$signalRHub.$on('loadNotifications', this.receiveNewNotification);
	},
	computed: {
		...mapGetters(['NOTIFICATIONS'])
	}
}
</script>

<style scoped>

</style>