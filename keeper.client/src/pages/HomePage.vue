<template>
  <!-- SECTION KeepsArea -->
  <section class="p-md-5 p-1 masonry">

    <!-- STUB KeepCard -->
    <div class="mason elevation-5 rounded" v-for="keep in keeps">
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

@media (min-width: 1200px) {
  .masonry {
    columns: 4;
    column-gap: 40px;
  }
}

@media (max-width: 1200px) {
  .masonry {
    columns: 3;
    column-gap: 40px;
  }
}

@media (max-width: 992px) {
  .masonry {
    columns: 2;
    column-gap: 35px;
  }
}

.mason {
  margin-bottom: 15px;
  break-inside: avoid;
}
</style>
