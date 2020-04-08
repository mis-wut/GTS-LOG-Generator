<template>
  <v-card v-if="loaded">
    <v-card-text>
      <p class="display-1 text--primary">Settings</p>

      <v-form>
        <v-text-field
          v-model="settingsData.configFilePath"
          :rules="fieldRequired"
          label="Configuration file path:"
          style="width: 800px"
          required
        ></v-text-field>

        <v-text-field
          v-model="settingsData.logsInputFolder"
          :rules="fieldRequired"
          label="Logs input folder path:"
          style="width: 800px"
          required
        ></v-text-field>

        <v-text-field
          v-model="settingsData.errorLogsFolder"
          label="Error logs folder path:"
          style="width: 800px"
          required
        ></v-text-field>

        <v-text-field
          v-model="settingsData.loggerOutputFileLocation"
          label="Logger output file location:"
          style="width: 800px"
          required
        ></v-text-field>

        <v-text-field
          v-model="settingsData.initialTimestampRoundBase"
          :rules="fieldRequired"
          label="Initial timestamp for rounding:"
          type="number"
          style="width: 200px"
          required
        ></v-text-field>

        <v-text-field
          v-model="settingsData.timewindow"
          :rules="fieldRequired"
          label="Timewindow (in seconds):"
          type="number"
          style="width: 200px"
          required
        ></v-text-field>

        <v-text-field
          v-model="settingsData.influxdbHost"
          :rules="fieldRequired"
          label="Influxdb host:"
          style="width: 800px"
          required
        ></v-text-field>

        <v-text-field
          v-model="settingsData.influxdbLogsMetricsBucket"
          :rules="fieldRequired"
          label="Influxdb logs metrics bucket:"
          style="width: 800px"
          required
        ></v-text-field>

        <v-text-field
          v-model="settingsData.influxdbSystemMetricsBucket"
          :rules="fieldRequired"
          label="Influxdb system metrics bucket:"
          style="width: 800px"
          required
        ></v-text-field>

        <v-text-field
          v-model="settingsData.influxdbAuthToken"
          :rules="fieldRequired"
          label="Influxdb auth token:"
          style="width: 800px"
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
    fieldRequired: [v => !!v || "Field is required."],
    settingsData: {
      configFilePath: "",
      logsInputFolder: "",
      errorLogsFolder: "",
      loggerOutputFileLocation: "",
      initialTimestampRoundBase: 0,
      timewindow: 10,
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
