<template>
  <header class="mb-3 sticky-top">
    <Navbar />
  </header>
  <main class="container-fluid">
    <router-view />
  </main>
  <footer class="foot sticky-bottom">
    <Footbar />
  </footer>
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
}
</style>
