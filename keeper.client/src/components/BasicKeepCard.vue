<template>
  <div title="See Details" class="col-12 keepImg rounded elevation-5 text-end selectable" @click="openModal()">
    <button title="Delete Keep" class="btn bg-danger rounded-circle deleteBtn elevation-5"
      v-if="account?.id != null && account?.id == route.params.accountId && activeVault?.id == null"
      @click.stop="deleteKeep(keep?.id)"> <i class="mdi mdi-alpha-x smallText"></i>
    </button>
    <img :src="keep?.img" class="img-fluid" style="visibility: hidden;">
    <div class="d-flex align-items-center justify-content-between px-md-3 py-2 px-1 glass">
      <h4 class="text-start text-light keepFont m-0">{{ keep?.name }}</h4>
    </div>
  </div>

  <Modal class="toggleLarge" :id="`vaultedKeepDetails-${keep?.id}`" size="modal-xl">
    <template #content>
      <VaultKeepDetails :keep="keep" />
    </template>
  </Modal>
</template>


<script lang="ts">
import { computed } from 'vue';
import { Keep } from '../models/Keep';
import { useRoute } from 'vue-router';
import { AppState } from '../AppState';
import Pop from '../utils/Pop';
import { keepsService } from '../services/KeepsService';
import VaultKeepDetails from '../components/VaultKeepDetails.vue'
import { Modal } from 'bootstrap';

export default {

  props: {
    keep: { type: Keep, Required: true }
  },

  setup(props) {
    const route = useRoute()
    return {
      route,
      keepImg: computed(() => `url(${props.keep.img})`),
      account: computed(() => AppState.account),
      activeVault: computed(() => AppState.activeVault),

      async openModal() {
        Modal.getOrCreateInstance(`#vaultedKeepDetails-${props.keep.id}`).show()
        this.increaseViews();
      },

      async deleteKeep(keepId) {
        try {
          if (await Pop.confirm("Are you sure you want to Delete This Keep?")) {
            await keepsService.deleteKeep(keepId);
            Pop.toast("Your Keep was Successfully Deleted")
          }
        } catch (error) {
          Pop.error(error);
        }
      },
      async increaseViews() {
        try {
          props.keep.views++
          await keepsService.increaseViews(props.keep);
        } catch (error) {
          Pop.error(error);
        }
      },
    }
  }
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

.keepImg {
  width: 100%;
  background-image: v-bind(keepImg);
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