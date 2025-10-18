import { getUserByToken } from '@/API/authAPI'
import { defineStore } from 'pinia'
import { ref } from 'vue'

export const useUserStore = defineStore('userInfo', () => {
  let isLogin = ref(false)
  let user = ref(null)

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

  function logIn(newUser, token) {
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
