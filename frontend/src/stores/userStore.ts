import { getUserByToken } from '@/API/authAPI.ts'
import { ref } from 'vue'
import { createGlobalState } from '@vueuse/core'

export const useUserStore = createGlobalState(() => {
  let isLogin = ref(false)
  let user = ref<any>(null)

  async function checkAuth() {
    const token = localStorage.getItem('token')
    if (!token) return

    // returns 200 or 401 depends on token
    const res = await getUserByToken(token)
    if (res.status === 200) {
      isLogin.value = true
      user.value = res.data.user
    } else {
      localStorage.removeItem('token')
    }
  }

  function logIn(newUser: any, token: any) {
    isLogin.value = true
    user.value = newUser
    localStorage.setItem('token', token)
  }

  function logOut() {
    isLogin.value = false
    user.value = null
    localStorage.removeItem('token')
  }

  return { isLogin, user, checkAuth, logIn, logOut }
})
