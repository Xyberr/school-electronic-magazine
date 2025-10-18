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

export default router
