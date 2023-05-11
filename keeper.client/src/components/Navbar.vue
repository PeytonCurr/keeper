<template>
  <div class="bg-light px-5 row text-dark pb-2 border-bottom border-3 m-0">
    <div class="col-4 d-flex align-items-center">
      <router-link class="d-flex" :to="{ name: 'Home' }">
        <button class="btn bg-secondary px-2 py-0 fw-bold">Home</button>
      </router-link>
      <div class="dropdown" v-if="account?.id">
        <button class="btn dropdown-toggle px-2 py-1 fw-bold" type="button" data-bs-toggle="dropdown"
          aria-expanded="false">
          Create
        </button>
        <ul class="dropdown-menu bg-grey border-dark border-3">
          <li><button class="btn dropdown-item pb-2 fw-bold listBtn" data-bs-toggle="modal" data-bs-target="#newKeep">New
              Keep</button></li>
          <li>
            <div class="listDiv"></div>
          </li>
          <li><button class="btn dropdown-item pt-2 fw-bold listBtn" data-bs-toggle="modal" data-bs-target="#newVault">New
              Vault</button></li>
        </ul>
      </div>
    </div>

    <div class="col-4 d-flex align-items-center justify-content-center py-2">
      <img alt="logo" src="../assets/img/Keepr logo.png" height="60" />
    </div>

    <div class="col-4 d-flex align-items-center justify-content-end">
      <!-- LOGIN COMPONENT HERE -->
      <Login title="Account Dropdown" />
    </div>
  </div>
</template>

<script>
import { computed } from 'vue';
import { AppState } from '../AppState.js';
import Login from './Login.vue'

export default {
  setup() {
    return {
      account: computed(() => AppState.account)
    }
  },
  components: { Login }
}
</script>

<style scoped>
a:hover {
  text-decoration: none;
}

.nav-link {
  text-transform: uppercase;
}

.navbar-nav .router-link-exact-active {
  border-bottom: 2px solid var(--bs-success);
  border-bottom-left-radius: 0;
  border-bottom-right-radius: 0;
}

@media screen and (min-width: 768px) {
  nav {
    height: 64px;
  }
}

.listDiv {
  border: 1px solid
}

.listBtn:hover {
  filter: brightness(1.80);
  filter: drop-shadow(1px 0px);
}

.listBtn:active {
  transform: scale(.90);
}
</style>
