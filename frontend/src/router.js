import Vue from "vue";
import Router from "vue-router";
import GtsGeneratorView from "./views/GtsGeneratorView.vue";
import GtsSettingsView from "./views/GtsSettingsView.vue";

Vue.use(Router);

export default new Router({
  mode: "history",
  routes: [
    {
      path: "/",
      redirect: "generator"
    },
    {
      path: "/generator",
      name: "generator",
      component: GtsGeneratorView
    },
    {
      path: "/settings",
      name: "settings",
      component: GtsSettingsView
    }
  ]
});
