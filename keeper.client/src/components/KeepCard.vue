<template>
  <div class="col-12 keepImg rounded elevation-5 selectable">
    <img :src="keep?.img" class="img-fluid" style="visibility: hidden;">
    <div class="d-flex align-items-center justify-content-between px-md-3 py-2 px-1 glass">
      <h4 class="text-light keepFont m-0">{{ keep?.name }}</h4>
      <img :title="keep?.creator.name" :src="keep?.creator.picture" class="rounded-circle creatorPic">
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