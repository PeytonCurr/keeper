<template>
  <section class="row px-md-5 pt-4 px-1">
    <div class="col-12 px-md-5 px-1">

      <!-- SECTION VaultArea -->
      <section class="px-md-5 mx-md-5 mx-1 row profileArea">
        <div class="col-12">
          <img class="coverImg px-5 mr-5" :src="activeVault?.img" alt="Profile CoverImg">
        </div>
        <div class="col-12 text-center movePic">
          <section class="row justify-content-center">
            <div class="col-4"></div>

            <div class="col-3 movePic">
              <img class="rounded-circle elevation-5 border border-2" :src="activeVault?.creator.picture" height="100"
                alt="Profile Pic">
            </div>

            <div class="col-4">
              <div class="dropdown text-center justify-self-end" v-if="account?.id == activeVault?.creatorId">
                <div class="py-1 ps-xl-5 ms-xl-5 fw-bold fs-3 no-select" type="button" data-bs-toggle="dropdown"
                  aria-expanded="false">
                  ...
                </div>

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

              </div>
            </div>

          </section>
          <h1 class="mt-2">{{ activeVault?.creator.name }}</h1>
          <span class="rounded bg-primary p-1">{{ keeps?.length }} Keeps</span>
        </div>
      </section>

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
import { vaultsService } from '../services/VaultsService';
import { computed, onMounted } from 'vue';
import { AppState } from '../AppState';
import { keepsService } from '../services/KeepsService';
import BasicKeepCard from '../components/BasicKeepCard.vue'
import { useRoute } from 'vue-router';
import Pop from '../utils/Pop';

export default {
  setup() {
    const route = useRoute();

    onMounted(() => {
      getVaultKeeps();
      getActiveVault();
      getKeeps();
    });
    async function getVaultKeeps() {
      try {
        await keepsService.getVaultKeeps(route.params.vaultId);
      }
      catch (error) {
        Pop.error(error);
      }
    }
    async function getActiveVault() {
      try {
        await vaultsService.getActiveVault(route.params.vaultId);

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
      route,
      keeps: computed(() => AppState.keeps),
      account: computed(() => AppState.account),
      activeVault: computed(() => AppState.activeVault),


      async deleteVault(vaultId) {
        try {
          if (await Pop.confirm("Are you sure you want to Delete This Vault?")) {
            await vaultsService.deleteVault(vaultId);
            Pop.toast("Your Vault was Successfully Deleted")
          }
        } catch (error) {
          Pop.error(error);
        }
      },
    }
  }
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