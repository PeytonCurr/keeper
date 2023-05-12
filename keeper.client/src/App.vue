<template>
  <header class="sticky-top">
    <Navbar />
  </header>
  <main class="container-fluid px-md-5">
    <router-view />
  </main>
  <footer class="foot">
    <Footbar />
  </footer>

  <Modal id="newKeep" size="modal-md">
    <template #content>
      <CreateKeep />
    </template>
  </Modal>

  <Modal id="newVault" size="modal-md">
    <template #content>
      <CreateVault />
    </template>
  </Modal>
</template>

<script>
import { computed } from 'vue'
import { AppState } from './AppState'
import Navbar from './components/Navbar.vue'
import Footbar from './components/Footbar.vue'

export default {
  setup() {
    return {
      appState: computed(() => AppState)
    }
  },
  components: { Navbar, Footbar }
}
</script>
<style lang="scss">
@import "./assets/scss/main.scss";

:root {
  --main-height: calc(100vh - 32px - 64px);
}

@media (max-width: 768px) {
  header {
    display: none;
  }
}

@media (min-width: 768px) {
  footer {
    display: none;
  }
}

.foot {
  height: fit-content;
  position: fixed;
  bottom: 0px;
}

.keepFont {
  font-family: 'Shrikhand', cursive;
}
</style>
