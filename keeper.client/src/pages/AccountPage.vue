<template>
  <section class="row px-md-5 pt-4 px-1">
    <div class="col-12 px-md-5 px-1">

      <!-- SECTION AccountArea -->
      <section class="px-md-5 mx-md-5 mx-1 row profileArea">
        <div class="col-12 text-center">
          <img class="coverImg shadow" :src="account?.coverImg" alt="Profile CoverImg">

        </div>
        <div class="col-12 text-center">

          <section class="row justify-content-center">
            <div class="col-3"></div>

            <div class="col-6  movePic">
              <img class="rounded-circle elevation-5 border border-2" :src="account?.picture" height="100"
                alt="Profile Pic">
            </div>

            <div class="col-3">
              <div class="dropdown text-center justify-self-end d-flex" v-if="account?.id == route.params.accountId">
                <span title="Edit Account" class="ms-xl-2 ms-0 ps-lg-5 ps-md-4 ps-sm-5 ps-4 fw-bold fs-3 no-select"
                  type="button" data-bs-toggle="dropdown" aria-expanded="false">
                  ...
                </span>

                <form class="dropdown-menu bg-grey border-dark border-3 p-2 elevation-5 no-select"
                  @submit.prevent="editAccount()">
                  <section class="row">
                    <div class="col-12 text-center">
                      <h3 class="keepFont">Edit Account</h3>
                    </div>
                    <div class="col-12 mb-3">
                      <label class="keepFont" for="name">Name</label>
                      <input type="text" class="form-control border-bottom border-dark" required id="name"
                        v-model="selectable.name" placeholder="Enter Name" minlength="3" maxlength="50">
                    </div>
                    <div class="col-12 mb-3">
                      <label class="keepFont" for="picture">Picture</label>
                      <input type="url" class="form-control border-bottom border-dark" required id="picture"
                        v-model="selectable.picture" placeholder="Enter Picture" maxlength="500">
                    </div>
                    <div class="col-12 mb-3">
                      <label class="keepFont" for="coverImg">CoverImg</label>
                      <input type="url" class="form-control border-bottom border-dark" required id="coverImg"
                        v-model="selectable.coverImg" placeholder="Enter CoverImg" maxlength="500">
                    </div>
                  </section>
                  <button type="submit" class="btn btn-primary keepFont">Submit</button>
                </form>

              </div>
            </div>

          </section>

          <h1 class="mt-2 movePic">{{ account?.name }}</h1>
          <h6 class="movePic">{{ vaults.length }} Vaults | {{ keeps.length }} Keeps</h6>
        </div>
      </section>

      <!-- SECTION VaultsArea -->
      <div class="px-md-5 mx-md-5 mx-1">
        <h1>Vaults</h1>
        <h2 v-if="!vaults[0]?.id" class="my-5">You Do Not Have any Vaults</h2>
        <section class="masonry">
          <!-- STUB BasicKeepCard -->
          <div class="mason elevation-5 rounded no-select" v-for="vault in vaults" :key="vault?.id">
            <VaultCard :vault="vault" />
          </div>
        </section>
      </div>


      <!-- SECTION KeepsArea -->
      <div class="px-md-5 mx-md-5 mx-1">
        <h1>Keeps</h1>
        <h2 v-if="!keeps[0]?.id" class="my-5">You Do Not Have any Keeps</h2>
        <section class="masonry">
          <!-- STUB BasicKeepCard -->
          <div class="mason elevation-5 rounded" v-for="keep in keeps" :key="keep?.id">
            <BasicKeepCard :keep="keep" />
          </div>
        </section>
      </div>
      <br>


    </div>
  </section>
</template>


<script lang="ts">
import { computed, onMounted, ref, watchEffect } from 'vue';
import { AppState } from '../AppState';
import { keepsService } from '../services/KeepsService';
import Pop from '../utils/Pop';
import BasicKeepCard from '../components/BasicKeepCard.vue'
import { useRoute } from 'vue-router';
import VaultCard from '../components/VaultCard.vue';
import { vaultsService } from '../services/VaultsService';
import { accountService } from '../services/AccountService';

export default {
  setup() {
    const selectable = ref({});
    const route = useRoute();

    onMounted(() => {
      getProfileKeeps();
      getMyVaults();
    });

    watchEffect(() => {
      if (AppState.account.id) {
        selectable.value = { ...AppState.account }
      }
    })
    async function getProfileKeeps() {
      try {
        await keepsService.getProfileKeeps(route.params.accountId);
      }
      catch (error) {
        Pop.error(error);
      }
    }
    async function getMyVaults() {
      try {
        AppState.activeVault = null
        await vaultsService.getMyVaults();
      } catch (error) {
        Pop.error(error);
      }
    }
    return {
      selectable,
      route,
      keeps: computed(() => AppState.keeps),
      account: computed(() => AppState.account),
      vaults: computed(() => AppState.vaults),

      async editAccount() {
        try {
          await accountService.editAccount(selectable.value);
        } catch (error) {
          Pop.error(error);
        }
      }
    };
  },
  components: { VaultCard, BasicKeepCard }
}
</script>


<style lang="scss" scoped>
.profileArea {
  height: 45vh;
  position: relative;
}

.coverImg {
  height: 30vh;
  width: 70%;
  object-fit: cover;
  object-position: center;
  border-radius: 5px;
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

  .coverImg {
    height: 30vh;
    width: 100%;
    object-fit: cover;
    object-position: center;
  }
}

.mason {
  margin-bottom: 15px;
  break-inside: avoid;
}

.movePic {
  position: relative;
  bottom: 50px;
}
</style>
