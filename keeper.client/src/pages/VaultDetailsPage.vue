<template>
  <section class="row px-md-5 pt-4 px-1">
    <div class="col-12 px-md-5 px-1">

      <!-- SECTION VaultArea -->
      <section class="px-md-5 mx-md-5 mx-1 row profileArea">
        <div class="col-12 text-center">
          <img class="coverImg shadow" :src="activeVault?.img" alt="Profile CoverImg">
        </div>
        <div class="col-12 text-center">
          <section class="row justify-content-center short">
            <div class="col-3"></div>

            <div class="col-6 movePic keepFont text-light">
              <h1 class="mt-2">{{ activeVault?.name }}</h1>
              <h4 class="mt-2">By: {{ activeVault?.creator.name }}</h4>
            </div>


            <div class="col-3">
              <div class="dropdown text-center justify-self-end d-flex" v-if="account?.id == activeVault?.creatorId">
                <span class="ms-xl-2 ms-0 ps-lg-5 ps-md-4 ps-sm-5 ps-4 fw-bold fs-3 no-select" type="button"
                  data-bs-toggle="dropdown" aria-expanded="false">
                  ...
                </span>

                <ul class="dropdown-menu bg-grey border-dark border-3 p-1 elevation-5 no-select text-center">
                  <li><button class="btn dropdown-item fw-bold listBtn" @click.stop="deleteVault()">Delete Vault</button>
                  </li>
                </ul>

              </div>
            </div>

          </section>
          <span class="keepCount bg-primary p-1 px-3 elevation-1 fs-3">{{ keeps?.length }} Keeps</span>
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
      <br>


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
import { router } from '../router';

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
      keeps: computed(() => AppState.vaultedKeeps),
      account: computed(() => AppState.account),
      activeVault: computed(() => AppState.activeVault),


      async deleteVault() {
        try {
          if (await Pop.confirm("Are you sure you want to Delete This Vault?")) {
            await vaultsService.deleteVault(route.params.vaultId);
            router.push({ name: 'Account', params: { accountId: AppState.account.id } })
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
.keepCount {
  border-radius: 10px;
  position: relative;
  bottom: 50px;
}

.profileArea {
  height: 45vh;
  position: relative;
}

.coverImg {
  height: 30vh;
  width: 70%;
  object-position: center;
  border-radius: 5px;
}

.fitImg {
  height: 30vh;
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
    object-position: center;
  }
}

.mason {
  margin-bottom: 15px;
  break-inside: avoid;
}

.movePic {
  position: relative;
  bottom: 120px;
}

.short {
  max-height: 15vh;
}
</style>