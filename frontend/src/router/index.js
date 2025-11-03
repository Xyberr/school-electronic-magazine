import { useUserStore } from '@/stores/userStore'
import LoginPage from '@/views/LoginPage/LoginPage.vue'
import PrivateRoute from '@/views/PrivateRoute/PrivateRoute.vue'
import { createRouter, createWebHistory } from 'vue-router'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {path: '/', redirect: '/login'},
    {path: '/login', component: LoginPage},
    {path: '/privateRoute', component: PrivateRoute, meta: { requiresAuth: true }}
  ],
})

// auth check
router.beforeEach((to, from, next) => {
  const userStore = useUserStore()

  if (to.meta.requiresAuth && !userStore.isLogin) {
    next('/login')
  } else {
    next()
  }
})

export default router
