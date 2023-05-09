<template>
  <!-- SECTION KeepsArea -->
  <section class="p-md-5 p-1 masonry">

    <!-- STUB KeepCard -->
    <div class="mason elevation-5 rounded" v-for="keep in keeps" :key="keep?.id">
      <KeepCard :keep="keep" />
    </div>

  </section>



  <Modal id="newKeep" size="modal-md">
    <template #content>
      <CreateKeep />
    </template>
  </Modal>

  <Modal id="newVault" size="modal-md">
    <template #content>
      <CreateVault />
    </template>
  </Modal>
</template>

<script>
import { computed, onMounted } from "vue";
import { keepsService } from "../services/KeepsService.js"
import { AppState } from "../AppState.js";
import KeepCard from "../components/KeepCard.vue";
import CreateKeep from "../components/CreateKeep.vue";
import KeepDetails from "../components/KeepDetails.vue";
import CreateVault from "../components/CreateVault.vue";
import Pop from "../utils/Pop.js";
import { logger } from "../utils/Logger.js";
import { vaultsService } from "../services/VaultsService.js";

export default {
  setup() {
    onMounted(() => {
      getKeeps()
      getMyVaults()
    })

    async function getMyVaults() {
      try {
        await vaultsService.getMyVaults()
      } catch (error) {
        Pop.error(error);
      }
    }
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
  components: { KeepCard, CreateKeep, KeepDetails, CreateVault }
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
