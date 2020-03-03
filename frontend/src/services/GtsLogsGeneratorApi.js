import axios from "axios";

export default {
  getTerminalUrl() {
    return `http://${process.env.VUE_APP_HOST}:2222/ssh/host/gpunode3`;
  },

  getInfluxDbUrl() {
    return `http://${process.env.VUE_APP_HOST}:9999/`;
  },

  getLogGenerationJobParameters() {
    return axios
      .get("LogsGeneration/GetLogGenerationJobParameters")
      .then(response => {
        return response.data;
      });
  },

  updateLogGenerationJob(updateLogGenerationJobRequest) {
    return axios.put(
      "LogsGeneration/UpdateLogGenerationJob",
      updateLogGenerationJobRequest
    );
  }
};
