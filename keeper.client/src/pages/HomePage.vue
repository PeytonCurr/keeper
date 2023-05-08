<template>
  <!-- SECTION KeepsArea -->
  <section class="row p-4">

    <!-- STUB KeepCard -->
    <div class="col-md-4 col-3 p-3" v-for="keep in keeps">
      <KeepCard :keep="keep" />
    </div>

  </section>
</template>

<script>
import { computed, onMounted } from "vue";
import { keepsService } from "../services/KeepsService.js"
import { AppState } from "../AppState.js";
import KeepCard from "../components/KeepCard.vue";

export default {
  setup() {
    onMounted(() => {
      getKeeps();
    });
    async function getKeeps() {
      try {
        await keepsService.getKeeps();
      }
      catch (error) {
        Pop.error(error);
      }
    }
    return {
      keeps: computed(() => AppState.keeps),
    };
  },
  components: { KeepCard }
}
</script>

<style scoped lang="scss">
.home {
  display: grid;
  height: 80vh;
  place-content: center;
  text-align: center;
  user-select: none;

  .home-card {
    width: 50vw;

    >img {
      height: 200px;
      max-width: 200px;
      width: 100%;
      object-fit: contain;
      object-position: center;
    }
  }
}
</style>
