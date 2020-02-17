<template>
  <v-container>
    <v-card outlined>
      <v-card-text>
        <p class="display-1 text--primary">
          Generation Settings
        </p>

        <v-form v-model="valid">
          <v-checkbox v-model="isActive" label="Active"></v-checkbox>

          <v-text-field
            v-model="path"
            :rules="pathRules"
            label="Logs folder path:"
            required
          ></v-text-field>
        </v-form>
      </v-card-text>

      <v-card-actions>
        <v-btn :disabled="!valid" color="success" @click="validate">
          Save
        </v-btn>
      </v-card-actions>
    </v-card>
  </v-container>
</template>

<script>
export default {
  name: "GtsGenerationSettings",

  data: () => ({
    valid: false,
    active: false,
    pathRules: [v => !!v || "Path is required"]
  })
};
</script>
