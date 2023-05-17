import {HubConnectionBuilder, HubConnectionState, LogLevel} from "@microsoft/signalr";
import {config} from "@/config/config";

function createConnection() {
    return new HubConnectionBuilder()
        .withUrl(config.apiUrl + '/notifications', {
            accessTokenFactory: () => localStorage.getItem('apiKey')
        })
        .configureLogging(LogLevel.None)
        .build();
}

let connection = createConnection();
let hub = null;

export default {
    install(Vue) {
        hub = new Vue();
        Vue.prototype.$notificationsHub = hub;
        this.startConnection(hub);
    },
    startConnection() {
        let connectionPromise = null;
        connectionPromise = connection.start()
            .then(() => {
                this.initializeListeners();
            })
            .catch((error) => {
                return new Promise((resolve, reject) => {
                    if (connection.state === HubConnectionState.Connected)
                        return;
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
                    connection = createConnection();
                    this.startConnection();
                });
        } catch (e) {
            console.log(e);
        }
    },
    initializeListeners() {
        connection.on('loadNotifications', () => {
            hub.$emit('loadNotifications');
        });
    }
}
