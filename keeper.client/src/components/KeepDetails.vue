<template>
  <main class="container-fluid">
    <section class="row">
      <div class="col-12 col-xl-6 p-0 m-0">
        <img :src="keep?.img" class="HeroImg">
      </div>
      <div class="col-12 col-xl-6 d-flex flex-column justify-content-between p-4">

        <div class="d-flex justify-content-center">
          <div class="mx-3 fs-5 text-success d-flex align-items-center"><i class="mdi mdi-eye-outline fs-3 me-1 pt-1"></i>
            {{
              keep?.views }}</div>
          <div class="mx-3 fs-5 text-success d-flex align-items-center"> <i
              class="mdi mdi-alpha-k-box-outline fs-3 me-1 pt-1"></i> {{
                keep?.views }}</div>
        </div>

        <div class="keepFont longText">
          <h1 class="text-center text-dark">{{ keep?.name }}</h1>
          <p class="text-success m-0">{{ keep?.description }}</p>
        </div>

        <div class="d-flex align-items-center justify-content-between" v-if="account?.id">

          <form id="vaultPicker" class="creator" @submit.prevent="">
            <select class="form-select form-select-sm border-dark border-3" aria-label=".form-select-sm"
              v-model="selectable">
              ,<option value="" selected disabled hidden>Save to a Vault...</option>
              <option class="bg-grey" v-for="vault in vaults">{{ vault.name }}</option>
            </select>
            <button type="submit" class="btn bg-secondary px-2 py-0 fw-bold ms-2">Save</button>
          </form>

          <div class="creator align-items-center p-2">
            <img :title="keep?.creator.name" :src="keep?.creator.picture" height="50" class="rounded-circle creatorImg">
            <h6 class="">{{ keep?.creator.name }}</h6>
          </div>

        </div>

      </div>
    </section>
  </main>
</template>


<script lang="ts">
import { computed, ref } from 'vue';
import { Keep } from '../models/Keep';
import { AppState } from '../AppState';

export default {

  props: {
    keep: { type: Keep, Required: true }
  },

  setup(props) {
    const selectable = ref("")
    return {
      selectable,
      account: computed(() => AppState.account),
      vaults: computed(() => AppState.vaults),
    }
  }
}
</script>

<style scoped>
.HeroImg {
  min-height: 43vh;
  max-height: 53vh;
  width: 100%;
  object-position: center;
}

@media (min-width: 1200px) {
  .HeroImg {
    min-height: 43vh;
    max-height: 90vh;
    width: 100%;
    object-fit: cover;
    object-position: center;
  }
}

.creator {
  display: flex;
}

@media (min-width: 576px) {
  .longText {
    padding: 1.5em;
  }

  .creatorImg {
    margin-left: 0;
    margin-right: .5em;
  }
}

@media (max-width: 576px) {
  .creator {
    display: block;
  }

  .creatorImg {
    margin-left: 8em;
    margin-right: 0em;
  }

  .longText {
    inline-size: 19em;
    overflow-wrap: break-word;
    padding-top: .5em;
  }
}
</style>