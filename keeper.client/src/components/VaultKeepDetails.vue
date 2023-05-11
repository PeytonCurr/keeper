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
                keep?.kept }}</div>
        </div>

        <div class="keepFont longText">
          <h1 class="text-center text-dark">{{ keep?.name }}</h1>
          <p class="text-success m-0">{{ keep?.description }}</p>
        </div>

        <div class="d-flex align-items-center justify-content-between">
          <button class="btn bg-secondary px-2 py-0 fw-bold ms-2" @click.stop="removeKeepFromVault()"
            v-if="account?.id != null && activeVault?.id"> <i class="mdi mdi-circle-off-outline"></i>
            Remove From Vault</button>


          <router-link :title="`Go to ${keep?.creator.name}'s Page`" class="hovEffect"
            :to="{ name: page, params: paramId }">
            <div class="creator align-items-center p-2" data-bs-dismiss="modal" aria-label="Close">
              <img :src="keep?.creator.picture" height="50" class="rounded-circle creatorImg">
              <h6 class="text-end">{{ keep?.creator.name }}</h6>
            </div>
          </router-link>

        </div>

      </div>
    </section>
  </main>
</template>


<script lang="ts">
import { computed, ref, watch, watchEffect } from 'vue';
import { Keep } from '../models/Keep';
import { AppState } from '../AppState';
import Pop from '../utils/Pop';
import { logger } from '../utils/Logger';
import { vaultsService } from '../services/VaultsService';
import { keepsService } from '../services/KeepsService';
import { useRoute } from 'vue-router';
import { Modal } from 'bootstrap';

export default {

  props: {
    keep: { type: Keep, Required: true }
  },

  setup(props) {
    const selectable = ref("")
    const route = useRoute();
    // const signedIn = ref("")
    // const signedOut = ref("")

    // watchEffect(() => {
    //   if (AppState.account) {
    //     signedIn.value = "justify-content-between"
    //     signedOut.value = "justify-content-center"
    //   }
    // })
    return {
      // signedIn,
      // signedOut,
      route,
      selectable,
      account: computed(() => AppState.account),
      vaults: computed(() => AppState.vaults),
      page: computed(() => AppState.account.id == props.keep.creatorId ? "Account" : "Profile"),
      paramId: computed(() => AppState.account.id == props.keep.creatorId ? { accountId: AppState.account.id } : { profileId: props.keep.creatorId }),
      activeVault: computed(() => AppState.activeVault),

      async saveToVault(keepId) {
        try {
          await vaultsService.saveToVault(selectable.value, keepId);
          props.keep.kept++
          Pop.success(`Added to Vault`)
        } catch (error) {
          Pop.error(error);
        }
      },

      async removeKeepFromVault() {
        try {
          if (await Pop.confirm("Are you sure you want to Remove This Keep From your Vault?")) {
            Modal.getOrCreateInstance(`#vaultedKeepDetails-${props.keep.id}`).hide()
            await keepsService.DeleteVaultKeep(props.keep.id);
            Pop.toast("The Keep was Successfully Removed")
          }
        } catch (error) {
          Pop.error(error);
        }
      }

    }
  }
}
</script>

<style scoped>
.hovEffect:hover {
  filter: brightness(1.80);
  filter: drop-shadow(1px 0px);
}

.hovEffect:active {
  transform: scale(.90);
}

.HeroImg {
  min-height: 43vh;
  max-height: 53vh;
  height: 100%;
  width: 100%;
  object-fit: cover;
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