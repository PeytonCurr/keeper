<template>
  <router-link title="Go to Vault Details" class="hovEffect"
    :to="{ name: 'VaultDetails', params: { vaultId: vault?.id } }">
    <div class="col-12 vaultImg rounded elevation-5 text-end"
      :class="{ 'selectable': !vault?.isPrivate || vault?.creatorId == account.id }">
      <img :src="vault?.img" class="img-fluid" style="visibility: hidden;">
      <div class="d-flex align-items-center justify-content-between px-md-3 py-2 px-1 glass">
        <h4 class="text-light keepFont m-0">{{ vault?.name }}</h4>
        <button title="Private Vault" class="btn bg-light rounded-circle" v-if="vault?.isPrivate"> <i
            class="mdi mdi-lock"></i>
        </button>
      </div>
    </div>
  </router-link>
  <!-- data-bs-toggle="modal"
    :data-bs-target="`#vaultKeepDetails-${vault?.id}`"


  <Modal class="toggleLarge" :id="`vaultKeepDetails-${vault?.id}`" size="modal-xl">
    <template #content>
      <VaultKeepDetails :vault="vault" />
    </template>
  </Modal> -->
</template>


<script lang="ts">
import { computed } from 'vue';
import { Vault } from '../models/Vault';
import VaultKeepDetails from './VaultKeepDetails.vue';
import { AppState } from '../AppState';
import { useRoute } from 'vue-router';
import Pop from '../utils/Pop';
import { vaultsService } from '../services/VaultsService';

export default {
  props: {
    vault: { type: Vault, Required: true }
  },
  setup(props) {
    const route = useRoute();
    return {
      route,
      vaultImg: computed(() => `url(${props.vault.img})`),
      account: computed(() => AppState.account),
    };
  },
  components: { VaultKeepDetails }
}
</script>


<style lang="scss" scoped>
.smallText {
  font-size: 20px;
  position: relative;
  right: 10px;
  bottom: 10px;
}

.deleteBtn {
  height: 25px;
  width: 25px;
  position: relative;
  left: 12px;
  bottom: 12px;
}

.deleteBtn:hover {
  filter: brightness(.80);
}

.deleteBtn:hover.smallText {
  font-size: 25px;
  position: relative;
  right: 12px;
  bottom: 14px;
}

.vaultImg {
  width: 100%;
  background-image: v-bind(vaultImg);
  background-position: center;
  background-size: cover;
}

.glass {
  background-color: #ddd5e019;
}

@media (min-width: 576px) {
  .creatorPic {
    height: 50px;
  }
}

@media (max-width: 576px) {
  .creatorPic {
    height: 30px;
  }
}
</style>