<template>
  <v-card v-if="loaded">
    <v-card-text>
      <p class="display-1 text--primary">Settings</p>

      <v-form>
        <v-subheader class="pl-0">Time window (seconds):</v-subheader>
        <v-slider
          v-model="timeWindow"
          :thumb-size="24"
          thumb-label="always"
          min="10"
          max="300"
          tick-size="4" />

        <v-subheader class="pl-0">Chunk size:</v-subheader>
        <v-slider
          v-model="chunkSize"
          :thumb-size="42"
          thumb-label="always"
          min="1000"
          max="100000" />
      </v-form>
    </v-card-text>

    <v-card-actions>
      <v-btn color="success" @click="save">Save</v-btn>
    </v-card-actions>

    <v-snackbar
      v-model="snackbar"
      :timeout="snackbarTimeout"
      bottom
      right
      :color="snackbarColor"
    >
      {{ snackbarText }}
      <v-btn dark text @click="snackbar = false">Close</v-btn>
    </v-snackbar>
  </v-card>
</template>

<script>
export default {
  name: "GtsSettings",

  data: () => ({
    // TODO: change to false, set true after response
    loaded: true,
    timeWindow: 60,
    chunkSize: 20000,
    snackbar: false,
    snackbarText: "Saved successfully!",
    snackbarColor: "success",
    snackbarTimeout: 4000
  }),
  mounted() {
    // TODO: load app settings
  },
  methods: {
    save() {
      // TODO: save app settings
    }
  }
};
</script>
