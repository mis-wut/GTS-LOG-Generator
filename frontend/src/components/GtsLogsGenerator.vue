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
          single-line
          type="number"
          min="1"
          style="width: 120px"
        ></v-text-field>

        <p>Estimated logs size: {{ getLogsSize }} MB</p>

        <v-subheader class="pl-0">Number of logs files:</v-subheader>
        <v-text-field
          v-model="logsFilesCount"
          class="mt-0 pt-0"
          single-line
          type="number"
          style="width: 120px"
          min="1"
          :rules="fieldRequired"
        ></v-text-field>

        <v-subheader class="pl-0">Logs per generation:</v-subheader>
        <v-slider v-model="logsCount" min="1" max="1000000" height="40px">
          <template v-slot:prepend>
            <v-text-field
              v-model="logsCount"
              class="mt-0 pt-0"
              single-line
              type="number"
              style="width: 80px"
              min="1"
              :rules="fieldRequired"
            ></v-text-field>
          </template>
        </v-slider>

        <v-subheader class="pl-0">Number of providers:</v-subheader>
        <v-slider v-model="providersCount" :thumb-size="24" thumb-label="always" min="1" max="10"></v-slider>

        <v-subheader class="pl-0">Number of server addresses:</v-subheader>
        <v-slider v-model="serverAddressesCount" :thumb-size="24" thumb-label="always" min="1" max="120"></v-slider>

        <v-subheader class="pl-0">Number of hostnames:</v-subheader>
        <v-slider v-model="hostnamesCount" :thumb-size="24" thumb-label="always" min="1" max="20"></v-slider>

        <v-subheader class="pl-0">Number of upstream fqdn:</v-subheader>
        <v-slider v-model="upstreamFqdnsCount" :thumb-size="24" thumb-label="always" min="1" max="20"></v-slider>

        <v-subheader class="pl-0">Number of http codes:</v-subheader>
        <v-slider v-model="httpCodesCount" :thumb-size="24" thumb-label="always" min="1" max="16"></v-slider>

        <v-subheader class="pl-0">Number of communities:</v-subheader>
        <v-slider v-model="communitiesCount" :thumb-size="24" thumb-label="always" min="1" max="20"></v-slider>
      </v-form>
    </v-card-text>

    <v-card-actions>
      <v-btn :disabled="!valid" color="success" @click="save">Run generation</v-btn>
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
    communitiesCount: 1,
    interval: 5,
    logsFilesCount: 20,
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
    GtsLogsGeneratorApi.getLogGenerationJobLastParameters().then(response => {
      if (response) {
        this.isActive = response.isActive;
        this.providersCount = response.providersCount;
        this.serverAddressesCount = response.serverAddressesCount;
        this.upstreamFqdnsCount = response.upstreamFqdnsCount;
        this.hostnamesCount = response.hostnamesCount;
        this.httpCodesCount = response.httpCodesCount;
        this.communitiesCount = response.communitiesCount;
        this.interval = response.interval;
        this.logsFilesCount = response.logsFilesCount;
        this.logsCount = response.logsCount;
        this.path = response.path;
      }
      this.loaded = true;
    });
  },
  methods: {
    save() {
      GtsLogsGeneratorApi.runLogGenerationJob({
        isActive: this.isActive,
        interval: this.interval,
        logsFilesCount: this.logsFilesCount,
        logsCount: this.logsCount,
        providersCount: this.providersCount,
        serverAddressesCount: this.serverAddressesCount,
        upstreamFqdnsCount: this.upstreamFqdnsCount,
        hostnamesCount: this.hostnamesCount,
        httpCodesCount: this.httpCodesCount,
        communitiesCount: this.communitiesCount,
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
  },
  computed: {
    getLogsSize() {
      return Math.round(this.logsFilesCount * this.logsCount * 0.75) / 1000;
    }
  }
};
</script>
