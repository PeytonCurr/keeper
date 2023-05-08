<template>
  <div class="col-12 keepImg rounded elevation-5">
    <img :src="keep.img" class="img-fluid" style="visibility: hidden;">
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
</style>