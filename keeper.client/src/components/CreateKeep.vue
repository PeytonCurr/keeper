<template>
  <div class="bg-info">
    <div class="modal-header border-bottom border-dark keepFont">
      <h1>Add your Keep</h1>
      <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
    </div>
    <div class="modal-body">

      <!-- SECTION NewKeepForm -->
      <form class="border-dark border-3" @submit.prevent="createKeep()">
        <div class="modal-body">
          <section class="row">
            <div class="col-12 mb-3">
              <label class="keepFont" for="img">Img</label>
              <input type="url" class="form-control border-bottom border-dark" required id="img" v-model="selectable.img"
                placeholder="Enter Img Url Here..." maxlength="500">
            </div>
            <div class="col-12 mb-3">
              <label class="keepFont" for="name">Name</label>
              <input type="text" class="form-control border-bottom border-dark" required id="name"
                v-model="selectable.name" placeholder="Enter Name of Keep..." minlength="3" maxlength="50">
            </div>
            <div class="col-12 mb-3">
              <label class="keepFont" for="description">Description</label>
              <textarea cols="30" rows="5" minlength="3" maxlength="1000" class="form-control border-bottom border-dark"
                v-model="selectable.description" required placeholder="Enter description of Keep..."
                id="description"></textarea>
            </div>
          </section>
        </div>
        <div class="modal-footer border-top border-dark border-1 keepFont">
          <button type="button" class="btn bg-secondary" data-bs-dismiss="modal">Close</button>
          <button type="submit" class="btn bg-primary" data-bs-dismiss="modal">Submit</button>
        </div>
      </form>

    </div>
  </div>
</template>


<script lang="ts">
import { computed, ref } from 'vue';
import Pop from '../utils/Pop';
import { AppState } from '../AppState';
import { keepsService } from '../services/KeepsService';
import { useRoute } from 'vue-router';

export default {
  setup() {
    const route = useRoute();
    const selectable = ref({});

    return {
      route,
      selectable,
      account: computed(() => AppState.account),

      async createKeep() {
        try {
          await keepsService.createKeep(selectable.value, route);
        } catch (error) {
          Pop.error(error);
        }
      },

    }
  }
}
</script>


<style lang="scss" scoped></style>