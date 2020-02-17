<template>
  <v-app>
    <v-content>
      <GtsToolbar />
      <GtsGenerationSettings />
    </v-content>
  </v-app>
</template>

<script>
import GtsToolbar from "./components/GtsToolbar";
import GtsGenerationSettings from "./components/GtsGenerationSettings";

export default {
  name: "App",
  components: {
    GtsToolbar,
    GtsGenerationSettings
  },
  data: () => ({})
};
</script>

<style></style>
