<template>
  <span class="navbar-text">
    <button class="btn bg-secondary px-2 py-0 fw-bold" @click="login" v-if="!user.isAuthenticated">
      Login
    </button>
    <div v-else>
      <div class="dropdown-center my-2 my-lg-0">
        <div type="button" class="bg-light border-0 no-select" data-bs-toggle="dropdown" aria-expanded="false">
          <div v-if="account.picture || user.picture">
            <img :src="account.picture || user.picture" alt="account photo" height="45"
              class="rounded-circle selectable" />
          </div>
        </div>


        <ul class="dropdown-menu p-0 bg-grey border-dark border-3" aria-labelledby="authDropdown">
          <li>
            <router-link :to="{ name: 'Account', params: { accountId: account?.id } }" v-if="account?.id">
              <button class="btn dropdown-item fw-bold listBtn">Manage Account</button>
            </router-link>
          </li>

          <li>
            <div class="listDiv"></div>
          </li>

          <li><button class="btn dropdown-item fw-bold listBtn" @click="logout">
              <i class="mdi mdi-logout"></i>
              logout</button></li>
        </ul>

      </div>
    </div>
  </span>
</template>

<script>
import { computed } from 'vue'
import { AppState } from '../AppState'
import { AuthService } from '../services/AuthService'
export default {
  setup() {
    return {
      user: computed(() => AppState.user),
      account: computed(() => AppState.account),
      async login() {
        AuthService.loginWithPopup()
      },
      async logout() {
        AuthService.logout({ returnTo: window.location.origin })
      }
    }
  }
}
</script>

<style lang="scss" scoped>
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
