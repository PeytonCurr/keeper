<template>
  <section class="row p-md-5 p-1">
    <div class="col-12 p-md-5 p-1">

      <!-- SECTION VaultsArea -->
      <div class="p-5 m-md-5 m-1">
        <h1>Vaults</h1>
        <section class="masonry">
          <!-- STUB BasicKeepCard -->
          <div class="mason elevation-5 rounded" v-for="vault in vaults" :key="vault?.id">
            <VaultCard :vault="vault" />
          </div>

        </section>
      </div>


      <!-- SECTION KeepsArea -->
      <div class="p-5 m-md-5 m-1">
        <h1>Keeps</h1>
        <section class="masonry">
          <!-- STUB BasicKeepCard -->
          <div class="mason elevation-5 rounded" v-for="keep in keeps" :key="keep?.id">
            <BasicKeepCard :keep="keep" />
          </div>

        </section>
      </div>


    </div>
  </section>
</template>


<script lang="ts">
import { computed, onMounted } from 'vue';
import { AppState } from '../AppState';
import { keepsService } from '../services/KeepsService';
import Pop from '../utils/Pop';
import BasicKeepCard from '../components/BasicKeepCard.vue'
import { useRoute } from 'vue-router';
import VaultCard from '../components/VaultCard.vue';
import { vaultsService } from '../services/VaultsService';

export default {
  setup() {
    const route = useRoute();
    onMounted(() => {
      getProfileKeeps();
      getProfileVaults();
    });
    async function getProfileKeeps() {
      try {
        await keepsService.getProfileKeeps(route.params.profileId);
      }
      catch (error) {
        Pop.error(error);
      }
    }
    async function getProfileVaults() {
      try {
        await vaultsService.getProfileVaults(route.params.profileId);

      } catch (error) {
        Pop.error(error);
      }
    }
    return {
      keeps: computed(() => AppState.keeps),
      account: computed(() => AppState.account),
      vaults: computed(() => AppState.vaults),
    };
  },
  components: { VaultCard, BasicKeepCard }
}
</script>


<style lang="scss" scoped>
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