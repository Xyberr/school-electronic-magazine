import type { TokenPayload } from '@/types/auth'
import { jwtDecode } from 'jwt-decode'
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
  const token = localStorage.getItem('token')

  if (!to.meta.requiresAuth) {
    return
  }

  if (!token) {
    return '/login'
  }

  let decoded
  try {
    decoded = jwtDecode<TokenPayload>(token)
  } catch (error) {
    localStorage.removeItem('token')
    return '/login'
  }

  const expired = decoded.exp * 1000 < Date.now()
  if (expired) {
    localStorage.removeItem('token')
    return '/login?reason=expired'
  }
})

export default router
