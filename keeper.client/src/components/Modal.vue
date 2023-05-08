<template>
  <div class="modal fade" tabindex="-1" role="dialog" aria-labelledby="modalTitleId" aria-hidden="true">
    <div class="modal-dialog modal-dialog-scrollable modal-dialog-centered" :class="size" role="document">
      <div class="modal-content">
        <slot name="content"></slot>
      </div>
    </div>
  </div>
</template>


<script lang="ts">
export default {
  props: {
    size: { type: String, default: 'modal-md' }
  },
  setup() {
    return {
    }
  }
}
</script>


<style lang="scss" scoped></style>