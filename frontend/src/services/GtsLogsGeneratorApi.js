import axios from "axios";

export default {
  getTerminalUrl() {
    return `http://${process.env.VUE_APP_HOST}:2222/`;
  },

  getInfluxDbUrl() {
    return `http://${process.env.VUE_APP_HOST}:9999/`;
  },

  getLogGenerationJobLastParameters() {
    return axios
      .get("LogsGeneration/GetLogGenerationJobLastParameters")
      .then((response) => {
        return response.data;
      });
  },

  runLogGenerationJob(runLogGenerationJobRequest) {
    return axios.post(
      "LogsGeneration/RunLogGenerationJob",
      runLogGenerationJobRequest
    );
  },

  getSettings() {
    return axios.get("Config/GetConfig").then((response) => {
      return response.data;
    });
  },

  updateSettings(updateConfigRequest) {
    return axios.put("Config/UpdateConfig", updateConfigRequest);
  },
};
