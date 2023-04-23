<template>
    <div>
        <notifications
            group="admin-actions"
            position="top center"
            :width="400"
        />
        <router-view/>
    </div>
</template>

<style lang="scss">
#app {
    font-family: Avenir, Helvetica, Arial, sans-serif;
    -webkit-font-smoothing: antialiased;
    -moz-osx-font-smoothing: grayscale;
    text-align: center;
    color: #2c3e50;
}

nav {
    padding: 30px;

    a {
        font-weight: bold;
        color: #2c3e50;

        &.router-link-exact-active {
            color: #42b983;
        }
    }
}

.notification-title {
    font-weight: bold;
    font-size: 18px;
}

.notification-content {
    font-size: 14px;
}
</style>
