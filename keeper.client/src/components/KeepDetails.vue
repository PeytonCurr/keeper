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

        <div class="d-flex align-items-center justify-content-between">

          <!-- <div class="dropdown" @submit.prevent="@SaveToVault()"> -->
          <div class="dropdown">
            <button class="btn dropdown-toggle px-2 py-1 fw-bold" type="button" data-bs-toggle="dropdown"
              aria-expanded="false">
              VaultName**
            </button>
            <ul class="dropdown-menu bg-grey border-dark border-3">
              <li><button class="btn dropdown-item pb-2 fw-bold listBtn" data-bs-toggle="modal"
                  data-bs-target="#newKeep">New
                  Keep</button></li>
              <li>
                <div class="listDiv"></div>
              </li>
              <li><button class="btn dropdown-item pt-2 fw-bold listBtn" data-bs-toggle="modal"
                  data-bs-target="#newVault">New
                  Vault</button></li>
            </ul>
            <button type="submit" class="btn bg-secondary px-2 py-0 fw-bold">Save</button>
          </div>

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
import { Keep } from '../models/Keep';

export default {

  props: {
    keep: { type: Keep, Required: true }
  },

  setup() {
    return {}
  }
}
</script>


<style lang="scss" scoped>
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
}

@media (max-width: 576px) {
  .creator {
    display: block;
  }

  .creatorImg {
    margin-left: 8em;
  }

  .longText {
    inline-size: 19em;
    overflow-wrap: break-word;
    padding-top: .5em;
  }
}
</style>