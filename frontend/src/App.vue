<template>
  <v-app>
    <GtsNavigationDrawer />
    <GtsToolbar />
    <v-content>
      <v-container fluid>
        <v-fade-transition mode="out-in">
          <router-view></router-view>
        </v-fade-transition>
      </v-container>
    </v-content>
  </v-app>
</template>

<script>
import GtsToolbar from "./components/GtsToolbar";
import GtsNavigationDrawer from "./components/GtsNavigationDrawer";

export default {
  name: "App",
  components: {
    GtsToolbar,
    GtsNavigationDrawer
  }
};
</script>

<style></style>
