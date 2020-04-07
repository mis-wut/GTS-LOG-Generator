<template>
  <v-card v-if="loaded">
    <v-card-text>
      <p class="display-1 text--primary">Settings</p>

      <v-form>
        <v-text-field
          v-model="settingsData.configFilePath"
          label="Configuration file path:"
          required
        ></v-text-field>

        <v-text-field
          v-model="settingsData.logsInputFolder"
          label="Logs input folder path:"
          required
        ></v-text-field>

        <v-text-field
          v-model="settingsData.errorLogsFolder"
          label="Error logs folder path:"
          required
        ></v-text-field>

        <v-text-field
          v-model="settingsData.loggerOutputFileLocation"
          label="Logger output file location:"
          required
        ></v-text-field>

        <v-text-field
          v-model="settingsData.initialTimestampRoundBase"
          label="Initial timestamp for rounding:"
          required
        ></v-text-field>

        <v-text-field
          v-model="settingsData.timewindowSendCount"
          label="Timewindow count to send:"
          required
        ></v-text-field>

        <v-text-field v-model="settingsData.influxdbHost" label="Influxdb host:" required></v-text-field>

        <v-text-field
          v-model="settingsData.influxdbLogsMetricsBucket"
          label="Influxdb logs metrics bucket:"
          required
        ></v-text-field>

        <v-text-field
          v-model="settingsData.influxdbSystemMetricsBucket"
          label="Influxdb system metrics bucket:"
          required
        ></v-text-field>

        <v-text-field
          v-model="settingsData.influxdbAuthToken"
          label="Influxdb auth token:"
          required
        ></v-text-field>
      </v-form>
    </v-card-text>

    <v-card-actions>
      <v-btn color="success" @click="save">Save</v-btn>
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
  name: "GtsSettings",

  data: () => ({
    // TODO: change to false, set true after response
    loaded: true,
    settingsData: {
      configFilePath: "",
      logsInputFolder: "",
      errorLogsFolder: "",
      loggerOutputFileLocation: "",
      initialTimestampRoundBase: 0,
      timewindowSendCount: 1,
      influxdbHost: "",
      influxdbLogsMetricsBucket: "",
      influxdbSystemMetricsBucket: "",
      influxdbAuthToken: ""
    },
    snackbar: false,
    snackbarSuccessMessage: "Saved successfully!",
    snackbarSuccessColor: "success",
    snackbarErrorColor: "error",
    snackbarText: "",
    snackbarColor: "success",
    snackbarTimeout: 4000
  }),
  mounted() {
    GtsLogsGeneratorApi.getSettings().then(response => {
      if (response) {
        this.settingsData = response;
      }
      this.loaded = true;
    });
  },
  methods: {
    save() {
      GtsLogsGeneratorApi.updateSettings(this.settingsData)
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
