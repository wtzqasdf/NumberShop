<template>
    <Navbar></Navbar>
    <router-view />
</template>

<script lang="ts">
import { Options, Vue } from "vue-class-component";
import Navbar from "./components/Navbar.vue";

@Options({
  components:{
    Navbar
  }
})
export default class App extends Vue {
}
</script>

<style scoped>

</style>
