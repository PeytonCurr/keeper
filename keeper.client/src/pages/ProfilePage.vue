<template>
  <!-- SECTION KeepsArea -->
  <section class="row p-md-5 p-1">
    <div class="col-12 p-md-5 p-1">
      <section class="p-5 m-md-5 m-1 masonry">

        <!-- STUB KeepCard -->
        <div class="mason elevation-5 rounded" v-for="keep in keeps" :key="keep?.id">
          <BasicKeepCard :keep="keep" />
        </div>

      </section>
    </div>
  </section>
</template>


<script lang="ts">
import { computed, onMounted } from 'vue';
import { AppState } from '../AppState';
import { keepsService } from '../services/KeepsService';
import Pop from '../utils/Pop';
import BasicKeepCard from '../components/BasicKeepCard.vue'

export default {
  setup() {

    onMounted(() => {
      getKeeps()
    })

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
      account: computed(() => AppState.account),
    }
  }
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