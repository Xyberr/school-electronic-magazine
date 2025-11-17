import { ref } from 'vue'
import { createGlobalState } from '@vueuse/core'

export const useUserStore = createGlobalState(() => {
  let user = ref<any>(null)

  function logIn(newUser: any, token: any) {
    user.value = newUser
    localStorage.setItem('token', token)
  }

  function logOut() {
    user.value = null
    localStorage.removeItem('token')
  }

  return { user, logIn, logOut }
})
