import {HubConnectionBuilder, LogLevel} from "@aspnet/signalr";
import {config} from "@/config/config";

function createConnection() {
    return new HubConnectionBuilder()
        .withUrl(config.apiUrl + '/notifications', {
            accessTokenFactory: () => localStorage.getItem('apiKey')
        })
        .configureLogging(LogLevel.Information)
        .build();
}

const connection = createConnection();

export default {
    connection,
    install(Vue) {
        const hub = new Vue();
        Vue.prototype.$notificationsHub = hub;
        this.startConnection();
        connection.on('loadNotifications', () => {
            hub.$emit('loadNotifications');
        });
    },
    startConnection() {
        let connectionPromise = null;
        connectionPromise = connection.start().catch((error) => {
            console.log('cannot connect to the hub', error);
            console.log('Reconnecting...');
            return new Promise((resolve, reject) => {
                setTimeout(() => {
                    this.startConnection().then(resolve).catch(reject);
                }, 5000);
            })
        });
        return connectionPromise;
    },
    recreateConnection() {
        try {
            connection.stop()
                .then(() => {
                    this.connection = createConnection();
                    this.startConnection();
                });
        } catch (e) {
            console.log(e);
        }
    }
}
