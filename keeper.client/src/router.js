import { createRouter, createWebHashHistory } from 'vue-router'
import { authGuard, authSettled } from '@bcwdev/auth0provider-client'

function loadPage(page) {
  return () => import(`./pages/${page}.vue`)
}

const routes = [
  {
    path: '/',
    name: 'Home',
    component: loadPage('HomePage'),
    beforeEnter: authSettled
  },
  {
    path: '/profile/:profileId',
    name: 'Profile',
    component: loadPage('ProfilePage'),
    beforeEnter: authSettled
  },
  {
    path: '/vaultDetails/:vaultId',
    name: 'VaultDetails',
    component: loadPage('VaultDetailsPage'),
    beforeEnter: authSettled
  },
  {
    path: '/account/:accountId',
    name: 'Account',
    component: loadPage('AccountPage'),
    beforeEnter: authGuard
  }
]

export const router = createRouter({
  linkActiveClass: 'router-link-active',
  linkExactActiveClass: 'router-link-exact-active',
  history: createWebHashHistory(),
  routes
})
