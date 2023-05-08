<template>
  <div class="col-12 keepImg rounded elevation-5 selectable">
    <img :src="keep?.img" class="img-fluid" style="visibility: hidden;">
    <div class="d-flex align-items-center justify-content-between px-3 py-2 glass">
      <h3 class="text-light keepFont m-0">{{ keep?.name }}</h3>
      <img :title="keep?.creator.name" :src="keep?.creator.picture" height="40" class="rounded-circle">
    </div>
  </div>
</template>


<script lang="ts">
import { computed } from 'vue';
import { Keep } from '../models/Keep';

export default {

  props: {
    keep: { type: Keep, Required: true }
  },

  setup(props) {
    return {
      keepImg: computed(() => `url(${props.keep.img})`)
    }
  }
}
</script>


<style lang="scss" scoped>
.keepImg {
  width: 100%;
  background-image: v-bind(keepImg);
  background-position: center;
  background-size: cover;
}

.glass {
  background-color: #ddd5e019;
}
</style>