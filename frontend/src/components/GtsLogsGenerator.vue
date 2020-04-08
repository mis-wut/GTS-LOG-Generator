<template>
  <v-card v-if="loaded">
    <v-card-text>
      <p class="display-1 text--primary">Generator</p>

      <v-form v-model="valid">
        <v-checkbox v-model="isActive" label="Active"></v-checkbox>

        <v-text-field
          v-model="path"
          :rules="fieldRequired"
          style="width: 800px"
          label="Logs folder path:"
          required
        ></v-text-field>

        <v-subheader class="pl-0">Generation interval (seconds):</v-subheader>
        <v-text-field
          v-model="interval"
          class="mt-0 pt-0"
          hide-details
          single-line
          type="number"
          style="width: 120px"
        ></v-text-field>

        <v-subheader class="pl-0">Logs per generation:</v-subheader>
        <v-slider v-model="logsCount" min="1" max="1000000" height="40px">
          <template v-slot:prepend>
            <v-text-field
              v-model="logsCount"
              class="mt-0 pt-0"
              hide-details
              single-line
              type="number"
              style="width: 80px"
            ></v-text-field>
          </template>
        </v-slider>

        <v-subheader class="pl-0">Hit probability:</v-subheader>
        <v-slider v-model="hitProbability" :thumb-size="36" thumb-label="always" min="0" max="100">
          <span slot="thumb-label" slot-scope="{ value }">{{ value }} %</span>
        </v-slider>

        <v-subheader class="pl-0">Number of channels:</v-subheader>
        <v-slider v-model="channelsCount" :thumb-size="24" thumb-label="always" min="1" max="10"></v-slider>

        <v-subheader class="pl-0">Number of cities:</v-subheader>
        <v-slider v-model="citiesCount" :thumb-size="24" thumb-label="always" min="1" max="10"></v-slider>

        <v-subheader class="pl-0">Number of providers:</v-subheader>
        <v-slider v-model="providersCount" :thumb-size="24" thumb-label="always" min="1" max="5"></v-slider>
      </v-form>
    </v-card-text>

    <v-card-actions>
      <v-btn :disabled="!valid" color="success" @click="save">Save</v-btn>
    </v-card-actions>

    <v-snackbar v-model="snackbar" :timeout="snackbarTimeout" bottom right :color="snackbarColor">
      {{ snackbarText }}
      <v-btn dark text @click="snackbar = false">Close</v-btn>
    </v-snackbar>
  </v-card>
</template>

<script>
import GtsLogsGeneratorApi from "../services/GtsLogsGeneratorApi";

export default {
  name: "GtsLogsGenerator",
  data: () => ({
    loaded: false,
    valid: false,
    isActive: false,
    fieldRequired: [v => !!v || "Field is required."],
    path: "",
    channelsCount: 1,
    providersCount: 1,
    citiesCount: 1,
    interval: 5,
    logsCount: 100,
    hitProbability: 50,
    snackbar: false,
    snackbarSuccessMessage: "Saved successfully!",
    snackbarSuccessColor: "success",
    snackbarErrorColor: "error",
    snackbarText: "",
    snackbarColor: "success",
    snackbarTimeout: 4000
  }),
  mounted() {
    GtsLogsGeneratorApi.getLogGenerationJobParameters().then(response => {
      if (response) {
        this.isActive = response.isActive;
        this.channelsCount = response.channelsCount;
        this.providersCount = response.providersCount;
        this.citiesCount = response.citiesCount;
        this.interval = response.interval;
        this.logsCount = response.logsCount;
        // TODO: update hit probability with response probability
        this.hitProbability = 50;
        this.path = response.path;
      }
      this.loaded = true;
    });
  },
  methods: {
    save() {
      GtsLogsGeneratorApi.updateLogGenerationJob({
        isActive: this.isActive,
        interval: this.interval,
        logsCount: this.logsCount,
        citiesCount: this.citiesCount,
        channelsCount: this.channelsCount,
        providersCount: this.providersCount,
        path: this.path
      })
        .then(() => {
          this.snackbarText = this.snackbarSuccessMessage;
          this.snackbarColor = this.snackbarSuccessColor;
          this.snackbar = true;
        })
        .catch(error => {
          this.snackbarText = error.response.data;
          this.snackbarColor = this.snackbarErrorColor;
          this.snackbar = true;
        });
    }
  }
};
</script>
