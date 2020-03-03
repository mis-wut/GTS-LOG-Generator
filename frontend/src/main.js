import Vue from "vue";
import App from "./App.vue";
import vuetify from "./plugins/vuetify";
import axios from "axios";
import router from "./router";

Vue.prototype.$http = axios;
Vue.config.productionTip = false;
axios.defaults.baseURL = `http://${process.env.VUE_APP_HOST}:5000/api/`;

new Vue({
  vuetify,
  router,
  render: h => h(App)
}).$mount("#app");
