<template>
  <section class="row px-md-5 pt-4 px-1">
    <div class="col-12 px-md-5 px-1">

      <!-- SECTION ProfileArea -->
      <section class="px-md-5 mx-md-5 mx-1 row profileArea">
        <div class="col-12">
          <img class="coverImg px-5 mr-5" :src="activeVault?.creator.coverImg" alt="Profile CoverImg">
        </div>
        <div class="col-12 text-center movePic">
          <img class="rounded-circle elevation-5 border border-2" :src="activeVault?.creator.picture" height="100"
            alt="Profile Pic">
          <h1 class="mt-2">{{ activeVault?.creator.name }}</h1>
          <h6>{{ vaults.length }} Vaults | {{ keeps.length }} Keeps</h6>
        </div>
      </section>

      <!-- SECTION VaultsArea -->
      <div class="px-md-5 mx-md-5 mx-1">
        <h1>Vaults</h1>
        <section class="masonry">
          <!-- STUB BasicKeepCard -->
          <div class="mason elevation-5 rounded" v-for="vault in vaults" :key="vault?.id">
            <VaultCard :vault="vault" />
          </div>

        </section>
      </div>


      <!-- SECTION KeepsArea -->
      <div class="px-md-5 mx-md-5 mx-1">
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
      route,
      keeps: computed(() => AppState.keeps),
      account: computed(() => AppState.account),
      vaults: computed(() => AppState.vaults),
      activeVault: computed(() => AppState.vaults.find(v => v.creatorId == route.params.profileId))
      // coverImg: computed(() => `url(${AppState.vaults.find(v => v.creatorId == route.params.profileId).creator.coverImg})`),
    };
  },
  components: { VaultCard, BasicKeepCard }
}
</script>


<style lang="scss" scoped>
.profileArea {
  height: 45vh;
}

.coverImg {
  height: 30vh;
  width: 100%;
  object-position: center;
}

@media(min-width: 1200px) {
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

.movePic {
  position: relative;
  bottom: 60px;
}
</style>