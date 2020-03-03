<template>
  <v-app-bar
    :clipped-left="$vuetify.breakpoint.lgAndUp"
    app
    color="blue darken-3"
    dark
  >
    <v-toolbar-title>
      <span class="toolbar-title">GTS-Log</span>
    </v-toolbar-title>
  </v-app-bar>
</template>

<script>
export default {
  name: "GtsToolbar"
};
</script>

<style scoped>
.toolbar-title {
  font-weight: bold;
}
</style>
