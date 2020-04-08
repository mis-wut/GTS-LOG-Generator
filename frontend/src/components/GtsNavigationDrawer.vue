<template>
  <v-navigation-drawer :clipped="$vuetify.breakpoint.lgAndUp" app>
    <v-list dense>
      <v-list-item to="/generator" exact>
        <v-list-item-action>
          <v-icon>data_usage</v-icon>
        </v-list-item-action>
        <v-list-item-content>
          <v-list-item-title>Generator</v-list-item-title>
        </v-list-item-content>
      </v-list-item>
      <v-list-item :href="getTerminalUrl" target="_blank" exact>
        <v-list-item-action>
          <v-icon>computer</v-icon>
        </v-list-item-action>
        <v-list-item-content>
          <v-list-item-title>Terminal</v-list-item-title>
        </v-list-item-content>
      </v-list-item>
      <v-list-item :href="getInfluxDbUrl" target="_blank" exact>
        <v-list-item-action>
          <v-icon>storage</v-icon>
        </v-list-item-action>
        <v-list-item-content>
          <v-list-item-title>InfluxDb</v-list-item-title>
        </v-list-item-content>
      </v-list-item>
      <v-list-item to="/settings" exact>
        <v-list-item-action>
          <v-icon>settings</v-icon>
        </v-list-item-action>
        <v-list-item-content>
          <v-list-item-title>Settings</v-list-item-title>
        </v-list-item-content>
      </v-list-item>
    </v-list>
  </v-navigation-drawer>
</template>

<script>
import gtsLogsGeneratorApi from "@/services/GtsLogsGeneratorApi";

export default {
  computed: {
    getInfluxDbUrl() {
      return gtsLogsGeneratorApi.getInfluxDbUrl();
    },
    getTerminalUrl() {
      return gtsLogsGeneratorApi.getTerminalUrl();
    }
  }
};
</script>
