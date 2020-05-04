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

        <v-subheader class="pl-0">Number of providers:</v-subheader>
        <v-slider v-model="providersCount" :thumb-size="24" thumb-label="always" min="1" max="5"></v-slider>

        <v-subheader class="pl-0">Number of server addresses:</v-subheader>
        <v-slider v-model="serverAddressesCount" :thumb-size="24" thumb-label="always" min="1" max="5"></v-slider>

        <v-subheader class="pl-0">Number of hostnames:</v-subheader>
        <v-slider v-model="hostnamesCount" :thumb-size="24" thumb-label="always" min="1" max="5"></v-slider>

        <v-subheader class="pl-0">Number of upstream fqdn:</v-subheader>
        <v-slider v-model="upstreamFqdnsCount" :thumb-size="24" thumb-label="always" min="1" max="5"></v-slider>

        <v-subheader class="pl-0">Number of http codes:</v-subheader>
        <v-slider v-model="httpCodesCount" :thumb-size="24" thumb-label="always" min="1" max="5"></v-slider>
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
    providersCount: 1,
    serverAddressesCount: 1,
    upstreamFqdnsCount: 1,
    hostnamesCount: 1,
    httpCodesCount: 1,
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
        this.providersCount = response.providersCount;
        this.serverAddressesCount = response.serverAddressesCount;
        this.upstreamFqdnsCount = response.upstreamFqdnsCount;
        this.hostnamesCount = response.hostnamesCount;
        this.httpCodesCount = response.httpCodesCount;
        this.interval = response.interval;
        this.logsCount = response.logsCount;
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
        providersCount: this.providersCount,
        serverAddressesCount: this.serverAddressesCount,
        upstreamFqdnsCount: this.upstreamFqdnsCount,
        hostnamesCount: this.hostnamesCount,
        httpCodesCount: this.httpCodesCount,
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
