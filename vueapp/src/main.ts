import { createApp } from "vue";
import App from "./App.vue";
import router from "./router";
import Axios from 'axios';

//global setting
Axios.defaults.withCredentials = true;

createApp(App).use(router).mount("#app");