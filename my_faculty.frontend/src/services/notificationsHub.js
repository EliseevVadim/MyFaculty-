import {HubConnectionBuilder, LogLevel} from "@aspnet/signalr";
import {config} from "@/config/config";

const connection = new HubConnectionBuilder()
	.withUrl(config.apiUrl + '/notifications', {
		accessTokenFactory: () => localStorage.getItem('apiKey')
	})
	.configureLogging(LogLevel.Information)
	.build();

export default {
	connection,
	install(Vue) {
		const connection = new HubConnectionBuilder()
			.withUrl(config.apiUrl + '/notifications', {
				accessTokenFactory: () => localStorage.getItem('apiKey')
			})
			.configureLogging(LogLevel.Information)
			.build();
		const hub = new Vue();
		Vue.prototype.$signalRHub = hub;
		let connectionPromise = null;
		function startConnection() {
			connectionPromise = connection.start().catch((error) => {
				console.error('cannot connect to the hub', error);
				return new Promise((resolve, reject) => {
					setTimeout(() => {
						startConnection().then(resolve).catch(reject)
					}, 5000);
				})
			})
		}

		startConnection();
		connection.on('loadNotifications', () => {
			hub.$emit('loadNotifications');
		});
	}
}
