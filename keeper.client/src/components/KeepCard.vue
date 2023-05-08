<template>
  <section class="row keepImg rounded">

  </section>
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
  min-height: 30vh;
  width: 30vw;
  background-image: v-bind(keepImg);
  background-position: center;
  background-size: cover;
}
</style>