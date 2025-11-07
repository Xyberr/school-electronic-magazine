import { useUserStore } from '@/stores/userStore'
import { createRouter, createWebHistory } from 'vue-router'
import { routes, handleHotUpdate } from 'vue-router/auto-routes'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes,
})

if (import.meta.hot) {
  handleHotUpdate(router)
}

// auth check
router.beforeEach((to) => {
  const { isLogin } = useUserStore()

  if (to.meta.requiresAuth && !isLogin) {
    return '/'
  }
})

export default router
