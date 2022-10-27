import Oidc from "oidc-client";

let oidcClient = new Oidc.UserManager({
    userStore: new Oidc.WebStorageStateStore(),
    client_id: "my-faculty-web-api",
    client_secret: "2103476B-4916-40D4-A1F5-320214273BD5",
    response_type: "code",
    scope: "openid profile MyFacultyWebAPI",
    authority: "https://localhost:44395/",
    redirect_uri: "http://localhost:8080/signin-oidc",
    post_logout_redirect_uri: "http://localhost:8080/signout-oidc",
    filterProtocolClaims: true,
    loadUserInfo: true,
    automaticSilentRenew: true,
    silent_redirect_uri: "http://localhost:8080/silent-renew.html",
    monitorSession: false,
    silentRequestTimeout: 10000000000
});

Oidc.Log.logger = console;
Oidc.Log.level = Oidc.Log.INFO;

oidcClient.events.addUserLoaded((user) => {
    localStorage.setItem('apiKey', user.access_token);
});

oidcClient.events.addAccessTokenExpiring(() => {
    console.log('token expiring: ', arguments);
});

oidcClient.events.addUserSignedOut(() => {
    alert('Going out!');
    oidcClient.signoutRedirect()
        .then((response) => {
            console.log(response);
        })
        .catch((error) => {
            console.log(error);
        });
});

export default new class SecurityService {
    login() {
        return oidcClient.signinRedirect();
    }

    async isAuthenticated() {
        let user = await oidcClient.getUser();
        return !!user && !user.expired;
    }

    async isAdmin() {
        let user = await oidcClient.getUser();
        return !!user && !user.expired && user.profile.role.includes('Admin');
    }

    async isUser() {
        let user = await oidcClient.getUser();
        return !!user && !user.expired && user.profile.role.includes('User');
    }

    async completeLogin() {
        return await oidcClient.signinRedirectCallback();
    }

    logout() {
        oidcClient.signoutRedirect();
    }

    completeLogout() {
        return oidcClient.signoutRedirectCallback();
    }

    async handleSilentCallback() {
        return oidcClient.signinSilentCallback();
    }

    async getToken() {
        let user = await oidcClient.getUser();
        return !!user && !user.expired ? user.access_token : null;
    }

    async getUserProfile() {
        let user = await oidcClient.getUser();
        return user.profile;
    }
}
